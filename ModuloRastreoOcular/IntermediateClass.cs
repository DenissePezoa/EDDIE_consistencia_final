using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using ModuloLog;
using System.Diagnostics;
using System.Security.Policy;
using System.IO;

namespace ModuloRastreoOcular
{
    /// <summary>
    /// Singleton class used for managing functionalities related to eye tracking.
    /// </summary>
    public class IntermediateClass
    {
        //  Attributes for implementing the class as a thread safe singleton
        private static IntermediateClass _Instance  =   null;
        private static readonly object padlock      =   new object();

        //  Proxy class for reflection, used so it's possible to change assemblies on runtime
        private Proxy proxyInstance;
            
        //  Attributes for handling new eye tracking data
        private Dictionary<string, string> _Data = new Dictionary<string, string>();
        public event PropertyChangedEventHandler PropertyChanged, TimerChanged;

        //  Classes controling mouse movement, data logging, reticle drawing and click generation.
        private MouseControl    controller;
        private StandardLogging logging;
        private ReticleDrawing  drawingClass;
        //  Configuration attributes
        public  bool            mouseControl;
        public  bool            saveData;
        public  int             clickRegister;
        private int             _ClickTimer;

        public int ClickTimer
        {
            get { return _ClickTimer; }
            set
            {
                _ClickTimer = value;
                OnTimerChanged(this, new PropertyChangedEventArgs(nameof(ClickTimer)));
            }
        }

        public IntermediateClass()
        {
            proxyInstance = new Proxy();
            proxyInstance.PropertyChanged += ChangeCoordinates;
        }

        //  Dictionary where the eye tracking data is kept
        public Dictionary<string, string> Data 
        {
            get { return _Data; }
            set 
            { 
                _Data = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
            }
        }

        public static IntermediateClass GetInstance() 
        {
            if (_Instance == null) 
            {
                lock (padlock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new IntermediateClass();
                    }
                }
            }
            return _Instance;
        }


        /// <summary>
        /// Method for initializating IntermediateClasss, receives the parameters contained in a configuration.
        /// </summary>
        /// <param name="pluginName">Full route to the plugin to be loaded as an assembly</param>
        /// <param name="reticleRoute">Full route to the image to be used as a reticle</param>
        /// <param name="mouseControl">Bool value that indicates if the user will control the mouse with its gaze</param>
        /// <param name="countdown">Number of seconds to be used as the countdown for generating a click</param>
        /// <param name="saveData">Bool value that indicates if the eye tracking data will be saved to a file</param>
        /// <param name="saveRoute">Route where the files will be saved</param>
        /// <param name="fileName">Name of the file to be generated</param>
        public void InitializeClass(string pluginName, string reticleRoute, bool mouseControl, int countdown, bool saveData, string saveRoute, string fileName)
        {
            proxyInstance.CreateAppDomain(pluginName);
            ClickTimer          = countdown*1000;
            this.mouseControl   = mouseControl;
            this.saveData       = saveData;

            //  If a reticle is selected, the class for managing its drawing is created
            if (reticleRoute != null)
            {
                if (drawingClass != null)
                {
                    drawingClass.ClearUp();
                }
                drawingClass = new ReticleDrawing(reticleRoute);
            }
            else if (reticleRoute == null && drawingClass != null)
            {
                drawingClass.ClearUp();
                drawingClass = null;
            }

            //  If saveData was selected, a file where contents will be written is created, after checking if a file has been
            //  created before (in which case, it's closed before creating the new file).
            if (saveData)
            {
                if (logging != null)
                {
                    logging.CloseLogTarget();
                }
                logging     = new StandardLogging();
                fileName    = fileName + " - " + DateTime.Now.ToString("dd-M-yyyy_HH-mm-ss");
                logging.CreateLogTarget(saveRoute, fileName + ".csv");
            }

            //  If mouseControl was selected, the class for controlling the mouse through eye movements is instantiated
            if (mouseControl)
            {
                if (controller == null)
                {
                    controller = new MouseControl();
                }
                clickRegister = 0;
            }

        }

        /// <summary>
        /// Method for cleaning up the class current configuration 
        /// </summary>
        public void ClearClass() 
        {
            //  Every variable related to eye tracking gets reinitialized
            mouseControl    =   false;
            saveData        =   false;
            if (controller != null)
            {
                controller = null;
            }
            if (logging != null)
            {
                logging.CloseLogTarget();
                logging = null;
            }
            if (drawingClass != null)
            {
                drawingClass.ClearUp();
                drawingClass = null;
            }
        }

        /// <summary>
        /// Method used in the subscription to "PropertyChanged" of the eye tracking plugin, it manages all actions
        /// to be performed once the eye tracking device generates new data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeCoordinates(object sender, PropertyChangedEventArgs e)
        {
            //  The new data is extracted from the plugin currently loaded
            Data = proxyInstance.newData;

            //  Then, for each action linked to the new eye tracking coordinates, the program determines what to execute and what not to.
            //  These actions beings (up until now), drawing the reticle, saving the new data to a file and controlling the mouse's position.
            if (drawingClass != null)
            {
                drawingClass.UpdateData(Data["X_Coordinate"], Data["Y_Coordinate"]);
            }
            if (saveData)
            {
                string trackingData = Data["X_Coordinate"] + ", " + Data["Y_Coordinate"] + ", " + Data["Timestamp"] + "," + clickRegister.ToString();
                logging.WriteToLog(trackingData);
                clickRegister = 0;
            }
            if (mouseControl)
            {
                controller.MoveCursor(Data["X_Coordinate"], Data["Y_Coordinate"]);
            }
        }

        /// <summary>
        /// Fired when the property Data of the class is modified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        
        public void OnTimerChanged(object sender, PropertyChangedEventArgs e)
        {
            TimerChanged?.Invoke(this, e);
        }
    }


    //  Proxy class used to load different assemblies without having trouble with dependencies
    public class Proxy : MarshalByRefObject
    {
        //  The assemblies are loaded inside an AppDomain, which is unloaded each time a new assembly is
        //  selected (this only happens if there was another assembly had been loaded previously).
        AppDomain appDomainPlugins = null;
        bool loadedAppDomain = false;
        AppDomainPlugin AppDomainInstance = null;

        //  Event for handling the new data generated by an eye tracking device
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, string> _newData;

        public Dictionary<string, string> newData
        {
            get { return _newData; }
            set
            {
                _newData = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(newData)));
            }
        }

        /// <summary>
        /// Method in charge of creating an AppDomain and loading a plugin (assembly), inside it.
        /// </summary>
        /// <param name="pluginRoute">Full route of the assembly to be loaded</param>
        public void CreateAppDomain(string pluginRoute)
        {
            if (loadedAppDomain)
            {
                AppDomainInstance.ClearAssembly();
                AppDomain.Unload(appDomainPlugins);
            }

            AppDomainSetup domainInfo = new AppDomainSetup
            {
                ApplicationBase = System.Environment.CurrentDirectory,
            };

            Evidence adevidence = AppDomain.CurrentDomain.Evidence;
            appDomainPlugins    = AppDomain.CreateDomain("pluginsDomain", adevidence, domainInfo);
            loadedAppDomain     = true;
            Type type           = typeof(AppDomainPlugin);
            object[] arguments  = { this };
            AppDomainInstance   = (AppDomainPlugin)appDomainPlugins.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName, true, BindingFlags.Default, null, arguments, null, null);
            try
            {
                AppDomainInstance.LoadFrom(pluginRoute);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Class used to instantiate assemblies
    /// </summary>
    public class AppDomainPlugin : MarshalByRefObject
    {
        public Assembly assemblyLoaded;
        public Type     assemblyType;
        public object   assemblyInstance;
        private Proxy   proxyInstance;

        public AppDomainPlugin(Proxy proxyInstance)
        {
            this.proxyInstance  = proxyInstance;
            assemblyLoaded      = null;
        }

        public void LoadFrom(string path)
        {
            ValidatePath(path);
            if (assemblyLoaded != null) ClearAssembly();
            assemblyLoaded = Assembly.LoadFrom(path);
            SetUpAssembly();
        }

        public void ValidatePath(string path)
        {
            if (path == null) throw new ArgumentException("null");
            if (!File.Exists(path))
                throw new ArgumentException(String.Format("path \"{0}\" does not exist", path));
        }

        public void ClearAssembly()
        {
            EventInfo evPropChange  = assemblyType.GetEvent("PropertyChanged");
            Type delegateType       = evPropChange.EventHandlerType;
            Delegate propChangeDel  = Delegate.CreateDelegate(delegateType, this, "ChangeCoordinates");
            evPropChange.RemoveEventHandler(assemblyInstance, propChangeDel);
        }

        public void SetUpAssembly()
        {
            // Creation of a new instance from the assembly
            assemblyType        = assemblyLoaded.GetTypes()[0];
            assemblyInstance    = Activator.CreateInstance(assemblyType);

            // Event subscription to PropertyChanged
            EventInfo evPropChange  = assemblyType.GetEvent("PropertyChanged");
            Type delegateType       = evPropChange.EventHandlerType;
            Delegate propChangeDel  = Delegate.CreateDelegate(delegateType, this, "ChangeCoordinates");
            MethodInfo addHandler   = evPropChange.GetAddMethod();
            Object[] addHandlerArgs = { propChangeDel };
            addHandler.Invoke(assemblyInstance, addHandlerArgs);

            // Call to the OpenConnection method of the assembly/plugin
            MethodInfo assemblyConn = assemblyType.GetMethod("OpenConnection");
            assemblyConn.Invoke(assemblyInstance, new object[] { });
        }

        public void ChangeCoordinates(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo assemblyData = assemblyType.GetProperty("Data");
            Dictionary<string, string> Coordinates = (Dictionary<string, string>)assemblyData.GetValue(sender, null);
            try
            {
                proxyInstance.newData = Coordinates;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}