using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ModuloLog;


namespace ModuloRastreoOcular
{
    /// <summary>
    /// Class for loading and saving settings as files.
    /// </summary>
    public class SettingsManager
    {
        // Logger used for writing t files
        StandardLogging logSettings;

        public SettingsManager()
        {
            logSettings = new StandardLogging();
        }

        /// <summary>
        /// Reads eye tracking settings from a file, loading its contents to a FormAttributes class.
        /// </summary>
        /// <param name="fileRoute">Full route of the file to be read</param>
        /// <returns>FormAttributes with the settings loaded from a file. Null in case of failure.</returns>
        public FormAttributes LoadSettings(string fileRoute)
        {
            try
            {
                string jsonConfig           = File.ReadAllText(fileRoute);
                FormAttributes fAttributes  = JsonConvert.DeserializeObject<FormAttributes>(jsonConfig);
                return fAttributes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// Writes a list of settings to a file
        /// </summary>
        /// <param name="fileRoute">Full route of the file to be written</param>
        /// <param name="controls">List of objects, settings to be written on the file</param>
        /// <returns>True if written successfully. False if not.</returns>
        public bool SaveSettings(string fileRoute, List<object> controls)
        {
            if (File.Exists(fileRoute))
            {
                File.Delete(fileRoute);
            }
            FormAttributes fAttributes  = new FormAttributes();
            try
            {
                fAttributes.pluginsRoute    = (string)  controls.ElementAt(0);
                fAttributes.pluginName      = (string)  controls.ElementAt(1);
                fAttributes.reticlesRoute   = (string)  controls.ElementAt(2);
                fAttributes.reticleName     = (string)  controls.ElementAt(3);
                fAttributes.mouseControl    = (bool)    controls.ElementAt(4);
                fAttributes.clickTime       = (int)     controls.ElementAt(5);
                fAttributes.saveData        = (bool)    controls.ElementAt(6);
                fAttributes.fileName        = (string)  controls.ElementAt(7);
                fAttributes.fileRoute       = (string)  controls.ElementAt(8);
                string jsonConfig = JsonConvert.SerializeObject(fAttributes);
                logSettings.CreateLogTarget(fileRoute, ""); 
                logSettings.WriteToLog(jsonConfig);
                logSettings.CloseLogTarget();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
    }


    /// <summary>
    /// Class that contains a set of settings used in the configuration of eye tracking in EDDIE.
    /// </summary>
    public class FormAttributes
    {
        public string   pluginsRoute { get; set; }
        public string   pluginName { get; set; }
        public string   reticlesRoute { get; set; }
        public string   reticleName { get; set; }
        public bool     mouseControl { get; set; }
        public int      clickTime { get; set; }
        public bool     saveData { get; set; }
        public string   fileName { get; set; }
        public string   fileRoute { get; set; }
    }
}
