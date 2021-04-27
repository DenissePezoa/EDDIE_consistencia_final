using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using ModuloRastreoOcular;

namespace AugmentedReadingApp
{
    public partial class EyeTrackingConfiguration : Form
    {
        public Type assemblyType;
        public object assemblyInstance;

        private string pluginsCurrentRoute  = Directory.GetCurrentDirectory();
        private string reticlesCurrentRoute = Directory.GetCurrentDirectory();
        private string dataSaveCurrentRoute = Directory.GetCurrentDirectory();

        public EyeTrackingConfiguration()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            if (!Directory.Exists(pluginsCurrentRoute))     Directory.CreateDirectory(pluginsCurrentRoute);
            if (!Directory.Exists(reticlesCurrentRoute))    Directory.CreateDirectory(reticlesCurrentRoute);
            if (!Directory.Exists(dataSaveCurrentRoute))    Directory.CreateDirectory(dataSaveCurrentRoute);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  Combobox population
            InitializeComboBox(trackingPlugins, pluginsCurrentRoute, "*.dll");
            InitializeComboBox(reticleSelected, reticlesCurrentRoute, "*.png");

            reticleExample.SizeMode = PictureBoxSizeMode.StretchImage;
            reticleDimensions.Text  = "Ancho: 0px\nAlto: 0px";

            pluginsRoute.Text   = pluginsCurrentRoute;
            reticlesRoute.Text  = reticlesCurrentRoute;
            saveFileRoute.Text  = dataSaveCurrentRoute;

            IntermediateClass intermediate = IntermediateClass.GetInstance();
        }

        /// <summary>
        /// Initializes a combobox in a winform, using the names of the files of a certain format,
        /// of a specific directory, as the items of said combobox.
        /// </summary>
        /// <param name="target">Combobox to be initialized</param>
        /// <param name="route">Route of the files to be used to fill the combobox</param>
        /// <param name="fileFormat">Format of the files to be used to fill the combobox</param>
        private void InitializeComboBox(ComboBox target, string route, string fileFormat)
        {
            if (target.Items.Count > 0) target.Items.Clear();
            string[] items = Directory.GetFiles(route, fileFormat).Select(p => Path.GetFileName(p)).ToArray();
            target.Items.Add("None");
            target.Items.AddRange(items);
            target.SelectedIndex = 0;
        }

        /// <summary>
        /// Method to use the current eyetracking configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_MouseClick(object sender, MouseEventArgs e)
        {
            //  Gets the instance of the intermediate class
            IntermediateClass interClass    = IntermediateClass.GetInstance();
            //  Plugin and reticle selection, if "None" has been selected, the string is null
            string selectedPlugin           = ((trackingPlugins.Text == "None") ? null : pluginsCurrentRoute + "\\" + trackingPlugins.Text);
            string selectedReticle          = ((reticleSelected.Text == "None") ? null : reticlesCurrentRoute + "\\" + reticleSelected.Text);
        
            if (selectedPlugin != null)
            {
                try
                {
                    //  If a plugin was selected, then the intermediate class is initializated with the current configuration
                    interClass.InitializeClass( selectedPlugin, 
                                                selectedReticle, 
                                                controlMouse.Checked, 
                                                Decimal.ToInt32(clickTimer.Value), 
                                                saveData.Checked, 
                                                dataSaveCurrentRoute, 
                                                fileNameTextBox.Text);
                    MessageBox.Show("Configuración seleccionada con éxito");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un plugin");
            }
        }

        /// <summary>
        /// Method to cancel the eyetracking configuration.
        /// </summary>
        /// <param name="sender"></param>   
        /// <param name="e"></param>
        private void CancelChanges_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Updates the display used to show how a reticle looks like.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReticleSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReticle = reticlesCurrentRoute + "\\" + reticleSelected.Text;
            if (reticleSelected.Text != "None")
            {
                Image reticle = Image.FromFile(selectedReticle);
                reticleExample.Image    = reticle;
                reticleDimensions.Text  = "Ancho: " + reticle.Width + "px\nAlto: " + reticle.Width + "px";
            }
        }

        private void SaveData_CheckedChanged(object sender, EventArgs e)
        {
            if (saveData.Checked)
            {
                fileNameTextBox.Enabled     = true;
                saveFileRouteBrowse.Enabled = true;
            }
            else
            {
                fileNameTextBox.Enabled     = false;
                saveFileRouteBrowse.Enabled = false;
            }
        }

        private void ControlMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (controlMouse.Checked)
            {
                clickTimer.Enabled = true;
            }
            else
            {
                clickTimer.Enabled = false;
            }
        }

        /// <summary>
        /// Updates the combobox that lists every plugin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PluginsRoute_Click(object sender, EventArgs e)
        {
            routeBrowser.Description = "Seleccione la carpeta donde se ubican los plugins de rastreo ocular";
            if (routeBrowser.ShowDialog() == DialogResult.OK)
            {
                pluginsRoute.Text   = routeBrowser.SelectedPath;
                pluginsCurrentRoute = routeBrowser.SelectedPath;
                InitializeComboBox(trackingPlugins, pluginsCurrentRoute, "*.dll");
            }
        }

        /// <summary>
        /// Updates the combobox that lists every reticle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReticlesRoute_Click(object sender, EventArgs e)
        {
            routeBrowser.Description = "Seleccione la carpeta donde se ubican las retículas";
            if (routeBrowser.ShowDialog() == DialogResult.OK)
            {
                reticlesRoute.Text      = routeBrowser.SelectedPath;
                reticlesCurrentRoute    = routeBrowser.SelectedPath;
                InitializeComboBox(reticleSelected, reticlesCurrentRoute, "*.png");
            }
        }

        /// <summary>
        /// Sets the route for the file used for eye tracking logging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileRoute_Click(object sender, EventArgs e)
        {
            routeBrowser.Description = "Seleccione la carpeta donde se desean almacenar los archivos con los datos generados por el eye tracker";
            if (routeBrowser.ShowDialog() == DialogResult.OK)
            {
                saveFileRoute.Text      = routeBrowser.SelectedPath;
                dataSaveCurrentRoute    = routeBrowser.SelectedPath;
            }
        }

        
        /// <summary>
        /// Used to select a file to load a configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadConfig_Click(object sender, EventArgs e)
        {
            openFileConfig.FileName = "";
            openFileConfig.Title = "Seleccione un archivo para cargar una configuración";
            openFileConfig.Filter = "Archivo JSON (*.json)|*.json";
            SettingsManager settings = new SettingsManager();
            if (openFileConfig.ShowDialog() == DialogResult.OK)
            {
                
                //  Opening settings
                var filePath = openFileConfig.FileName;
                FormAttributes settingsAttribute = settings.LoadSettings(filePath);
                if (VerifyConfig(settingsAttribute))
                {
                    pluginsCurrentRoute     = settingsAttribute.pluginsRoute;
                    reticlesCurrentRoute    = settingsAttribute.reticlesRoute;
                    dataSaveCurrentRoute    = settingsAttribute.fileRoute;
                    pluginsRoute.Text       = settingsAttribute.pluginsRoute;                    
                    reticlesRoute.Text      = settingsAttribute.reticlesRoute;
                    controlMouse.Checked    = settingsAttribute.mouseControl;
                    clickTimer.Value        = settingsAttribute.clickTime;
                    saveData.Checked        = settingsAttribute.saveData;
                    fileNameTextBox.Text    = settingsAttribute.fileName;
                    saveFileRoute.Text      = settingsAttribute.fileRoute;
                    InitializeComboBox(trackingPlugins, pluginsRoute.Text, "*.dll");
                    InitializeComboBox(reticleSelected, reticlesRoute.Text, "*.png");
                    trackingPlugins.SelectedIndex = trackingPlugins.FindStringExact(settingsAttribute.pluginName);
                    reticleSelected.SelectedIndex = reticleSelected.FindStringExact(settingsAttribute.reticleName);
                    MessageBox.Show("Configuración cargada con éxito");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la configuración seleccionada.\n" +
                                    "El archivo pudo ser modificado o los directorios\n" +
                                    "y archivos señalados ya no existen o han sido   \n" +
                                    "movidos a otra ubicación.");
                }
            }

        }

        /// <summary>
        /// Verifies a configuration (validates routes in case these don't exist anymore)
        /// </summary>
        /// <param name="configuration">FormAttributes class with every setting of a configuration</param>
        /// <returns>True if configuration is valid, false if not.</returns>
        private bool VerifyConfig(FormAttributes configuration)
        {
            if (configuration != null && 
                Directory.Exists(configuration.pluginsRoute) &&
                Directory.Exists(configuration.reticlesRoute) && 
                Directory.Exists(configuration.fileRoute))
            {
                if (File.Exists(configuration.pluginsRoute + "\\" + configuration.pluginName) &&
                    File.Exists(configuration.reticlesRoute + "\\" + configuration.reticleName))
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Used to select a directory where a configuration will be stored as a json file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConfig_Click(object sender, EventArgs e)
        {
            // dialog configuration
            saveFileConfig.Title        = "Seleccione un directorio y un nombre para guardar su configuración";
            saveFileConfig.DefaultExt   = "config - " + DateTime.Now.ToString("dd-M-yyyy_HH-mm-ss");
            saveFileConfig.Filter       = "Archivo JSON (*.json)|*.json";

            // preparation of data to be saved
            object[] configData = { pluginsRoute.Text, trackingPlugins.Text, 
                                    reticlesRoute.Text, reticleSelected.Text,
                                    controlMouse.Checked, Decimal.ToInt32(clickTimer.Value), 
                                    saveData.Checked, fileNameTextBox.Text, saveFileRoute.Text };
            List<object> config = new List<object>();
            config.AddRange(configData);

            SettingsManager settings = new SettingsManager();
            if (saveFileConfig.ShowDialog() == DialogResult.OK) 
            {
                if (settings.SaveSettings(saveFileConfig.FileName, config))
                {
                    MessageBox.Show("Configuración guardada con éxito");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la configuración seleccionada.");
                }
            }
        }

        // Tooltips
        private void PluginsRouteBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTipBrowsePlugin.Show("Presione el botón para seleccionar una carpeta desde la cual se cargarán\nlos plugins de rastreo ocular.", pluginsRouteBrowse);
        }

        private void ReticlesRouteBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTipBrowseReticle.Show("Presione el botón para seleccionar una carpeta desde la cual se cargarán\nlas distintas retículas.", reticlesRouteBrowse);
        }

        private void SaveFileRouteBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTipBrowseSaveFile.Show("Presione el botón para seleccionar una carpeta en la cual se guardarán los archivos\n" +
                                        "generados a partir de los datos capturados por el dispositivo de rastreo ocular.", saveFileRouteBrowse);
        }

        private void ControlMouse_MouseHover(object sender, EventArgs e)
        {
            toolTipMouseControl.Show("Si se selecciona, el usuario podrá controlar el \npuntero con sus movimientos oculares.", controlMouse);
        }

        private void SaveData_MouseHover(object sender, EventArgs e)
        {
            toolTipSaveData.Show("Si se selecciona, los datos capturados por el eye tracker\nson guardados en un archivo .csv", saveData);
        }
    }
}