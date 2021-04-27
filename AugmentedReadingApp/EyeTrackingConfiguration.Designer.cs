namespace AugmentedReadingApp
{
    partial class EyeTrackingConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackingPlugins = new System.Windows.Forms.ComboBox();
            this.saveChanges = new System.Windows.Forms.Button();
            this.cancelChanges = new System.Windows.Forms.Button();
            this.reticleExample = new System.Windows.Forms.PictureBox();
            this.reticleSelected = new System.Windows.Forms.ComboBox();
            this.controlMouse = new System.Windows.Forms.CheckBox();
            this.reticleDimensions = new System.Windows.Forms.Label();
            this.toolTipMouseControl = new System.Windows.Forms.ToolTip(this.components);
            this.saveData = new System.Windows.Forms.CheckBox();
            this.toolTipSaveData = new System.Windows.Forms.ToolTip(this.components);
            this.pluginsRouteBrowse = new System.Windows.Forms.Button();
            this.reticlesRouteBrowse = new System.Windows.Forms.Button();
            this.saveFileRouteBrowse = new System.Windows.Forms.Button();
            this.clickTimer = new System.Windows.Forms.NumericUpDown();
            this.saveFileRoute = new System.Windows.Forms.TextBox();
            this.routeBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.reticleGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.reticlesRoute = new System.Windows.Forms.TextBox();
            this.pluginGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pluginsRoute = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTipBrowsePlugin = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBrowseReticle = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBrowseSaveFile = new System.Windows.Forms.ToolTip(this.components);
            this.openFileConfig = new System.Windows.Forms.OpenFileDialog();
            this.saveConfig = new System.Windows.Forms.Button();
            this.loadConfig = new System.Windows.Forms.Button();
            this.saveFileConfig = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.reticleExample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clickTimer)).BeginInit();
            this.reticleGroupBox.SuspendLayout();
            this.pluginGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackingPlugins
            // 
            this.trackingPlugins.FormattingEnabled = true;
            this.trackingPlugins.Location = new System.Drawing.Point(94, 59);
            this.trackingPlugins.Name = "trackingPlugins";
            this.trackingPlugins.Size = new System.Drawing.Size(235, 21);
            this.trackingPlugins.TabIndex = 0;
            // 
            // saveChanges
            // 
            this.saveChanges.Location = new System.Drawing.Point(62, 497);
            this.saveChanges.Name = "saveChanges";
            this.saveChanges.Size = new System.Drawing.Size(112, 23);
            this.saveChanges.TabIndex = 2;
            this.saveChanges.Text = "Aplicar";
            this.saveChanges.UseVisualStyleBackColor = true;
            this.saveChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SaveChanges_MouseClick);
            // 
            // cancelChanges
            // 
            this.cancelChanges.Location = new System.Drawing.Point(204, 497);
            this.cancelChanges.Name = "cancelChanges";
            this.cancelChanges.Size = new System.Drawing.Size(114, 23);
            this.cancelChanges.TabIndex = 3;
            this.cancelChanges.Text = "Cerrar";
            this.cancelChanges.UseVisualStyleBackColor = true;
            this.cancelChanges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CancelChanges_MouseClick);
            // 
            // reticleExample
            // 
            this.reticleExample.BackColor = System.Drawing.Color.Transparent;
            this.reticleExample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reticleExample.Location = new System.Drawing.Point(94, 88);
            this.reticleExample.Name = "reticleExample";
            this.reticleExample.Size = new System.Drawing.Size(44, 44);
            this.reticleExample.TabIndex = 4;
            this.reticleExample.TabStop = false;
            // 
            // reticleSelected
            // 
            this.reticleSelected.FormattingEnabled = true;
            this.reticleSelected.Location = new System.Drawing.Point(94, 61);
            this.reticleSelected.Name = "reticleSelected";
            this.reticleSelected.Size = new System.Drawing.Size(235, 21);
            this.reticleSelected.TabIndex = 7;
            this.reticleSelected.SelectedIndexChanged += new System.EventHandler(this.ReticleSelected_SelectedIndexChanged);
            // 
            // controlMouse
            // 
            this.controlMouse.AutoSize = true;
            this.controlMouse.Location = new System.Drawing.Point(16, 26);
            this.controlMouse.Name = "controlMouse";
            this.controlMouse.Size = new System.Drawing.Size(103, 17);
            this.controlMouse.TabIndex = 8;
            this.controlMouse.Text = "Controlar Mouse";
            this.controlMouse.UseVisualStyleBackColor = true;
            this.controlMouse.CheckedChanged += new System.EventHandler(this.ControlMouse_CheckedChanged);
            this.controlMouse.MouseHover += new System.EventHandler(this.ControlMouse_MouseHover);
            // 
            // reticleDimensions
            // 
            this.reticleDimensions.AutoSize = true;
            this.reticleDimensions.Location = new System.Drawing.Point(144, 103);
            this.reticleDimensions.Name = "reticleDimensions";
            this.reticleDimensions.Size = new System.Drawing.Size(48, 13);
            this.reticleDimensions.TabIndex = 6;
            this.reticleDimensions.Text = "Retícula";
            // 
            // saveData
            // 
            this.saveData.AutoSize = true;
            this.saveData.Location = new System.Drawing.Point(16, 28);
            this.saveData.Name = "saveData";
            this.saveData.Size = new System.Drawing.Size(95, 17);
            this.saveData.TabIndex = 10;
            this.saveData.Text = "Guardar Datos";
            this.saveData.UseVisualStyleBackColor = true;
            this.saveData.CheckedChanged += new System.EventHandler(this.SaveData_CheckedChanged);
            this.saveData.MouseHover += new System.EventHandler(this.SaveData_MouseHover);
            // 
            // pluginsRouteBrowse
            // 
            this.pluginsRouteBrowse.Location = new System.Drawing.Point(301, 22);
            this.pluginsRouteBrowse.Name = "pluginsRouteBrowse";
            this.pluginsRouteBrowse.Size = new System.Drawing.Size(29, 20);
            this.pluginsRouteBrowse.TabIndex = 13;
            this.pluginsRouteBrowse.Text = "...";
            this.pluginsRouteBrowse.UseVisualStyleBackColor = true;
            this.pluginsRouteBrowse.Click += new System.EventHandler(this.PluginsRoute_Click);
            this.pluginsRouteBrowse.MouseHover += new System.EventHandler(this.PluginsRouteBrowse_MouseHover);
            // 
            // reticlesRouteBrowse
            // 
            this.reticlesRouteBrowse.Location = new System.Drawing.Point(301, 23);
            this.reticlesRouteBrowse.Name = "reticlesRouteBrowse";
            this.reticlesRouteBrowse.Size = new System.Drawing.Size(29, 20);
            this.reticlesRouteBrowse.TabIndex = 14;
            this.reticlesRouteBrowse.Text = "...";
            this.reticlesRouteBrowse.UseVisualStyleBackColor = true;
            this.reticlesRouteBrowse.Click += new System.EventHandler(this.ReticlesRoute_Click);
            this.reticlesRouteBrowse.MouseHover += new System.EventHandler(this.ReticlesRouteBrowse_MouseHover);
            // 
            // saveFileRouteBrowse
            // 
            this.saveFileRouteBrowse.Enabled = false;
            this.saveFileRouteBrowse.Location = new System.Drawing.Point(299, 84);
            this.saveFileRouteBrowse.Name = "saveFileRouteBrowse";
            this.saveFileRouteBrowse.Size = new System.Drawing.Size(31, 20);
            this.saveFileRouteBrowse.TabIndex = 15;
            this.saveFileRouteBrowse.Text = "...";
            this.saveFileRouteBrowse.UseVisualStyleBackColor = true;
            this.saveFileRouteBrowse.Click += new System.EventHandler(this.SaveFileRoute_Click);
            this.saveFileRouteBrowse.MouseHover += new System.EventHandler(this.SaveFileRouteBrowse_MouseHover);
            // 
            // clickTimer
            // 
            this.clickTimer.Enabled = false;
            this.clickTimer.Location = new System.Drawing.Point(259, 25);
            this.clickTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.clickTimer.Name = "clickTimer";
            this.clickTimer.Size = new System.Drawing.Size(70, 20);
            this.clickTimer.TabIndex = 16;
            this.clickTimer.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // saveFileRoute
            // 
            this.saveFileRoute.BackColor = System.Drawing.SystemColors.Window;
            this.saveFileRoute.Location = new System.Drawing.Point(116, 84);
            this.saveFileRoute.Name = "saveFileRoute";
            this.saveFileRoute.ReadOnly = true;
            this.saveFileRoute.Size = new System.Drawing.Size(179, 20);
            this.saveFileRoute.TabIndex = 17;
            // 
            // reticleGroupBox
            // 
            this.reticleGroupBox.Controls.Add(this.label4);
            this.reticleGroupBox.Controls.Add(this.label3);
            this.reticleGroupBox.Controls.Add(this.reticlesRoute);
            this.reticleGroupBox.Controls.Add(this.reticlesRouteBrowse);
            this.reticleGroupBox.Controls.Add(this.reticleSelected);
            this.reticleGroupBox.Controls.Add(this.reticleDimensions);
            this.reticleGroupBox.Controls.Add(this.reticleExample);
            this.reticleGroupBox.Location = new System.Drawing.Point(18, 115);
            this.reticleGroupBox.Name = "reticleGroupBox";
            this.reticleGroupBox.Size = new System.Drawing.Size(336, 141);
            this.reticleGroupBox.TabIndex = 18;
            this.reticleGroupBox.TabStop = false;
            this.reticleGroupBox.Text = "Opciones de retícula";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 26);
            this.label4.TabIndex = 17;
            this.label4.Text = "Retículas\r\ndisponibles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Ruta retículas";
            // 
            // reticlesRoute
            // 
            this.reticlesRoute.BackColor = System.Drawing.SystemColors.Window;
            this.reticlesRoute.Location = new System.Drawing.Point(94, 23);
            this.reticlesRoute.Name = "reticlesRoute";
            this.reticlesRoute.ReadOnly = true;
            this.reticlesRoute.Size = new System.Drawing.Size(201, 20);
            this.reticlesRoute.TabIndex = 15;
            // 
            // pluginGroupBox
            // 
            this.pluginGroupBox.Controls.Add(this.label2);
            this.pluginGroupBox.Controls.Add(this.label1);
            this.pluginGroupBox.Controls.Add(this.pluginsRoute);
            this.pluginGroupBox.Controls.Add(this.pluginsRouteBrowse);
            this.pluginGroupBox.Controls.Add(this.trackingPlugins);
            this.pluginGroupBox.Location = new System.Drawing.Point(18, 12);
            this.pluginGroupBox.Name = "pluginGroupBox";
            this.pluginGroupBox.Size = new System.Drawing.Size(336, 97);
            this.pluginGroupBox.TabIndex = 19;
            this.pluginGroupBox.TabStop = false;
            this.pluginGroupBox.Text = "Opciones de plugin de rastreo ocular";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 26);
            this.label2.TabIndex = 16;
            this.label2.Text = "Plugins\r\ndisponibles";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Ruta plugins";
            // 
            // pluginsRoute
            // 
            this.pluginsRoute.BackColor = System.Drawing.SystemColors.Window;
            this.pluginsRoute.Location = new System.Drawing.Point(94, 22);
            this.pluginsRoute.Name = "pluginsRoute";
            this.pluginsRoute.ReadOnly = true;
            this.pluginsRoute.Size = new System.Drawing.Size(201, 20);
            this.pluginsRoute.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.controlMouse);
            this.groupBox1.Controls.Add(this.clickTimer);
            this.groupBox1.Location = new System.Drawing.Point(18, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 59);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones de control de mouse";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tiempo de click";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fileNameTextBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.saveFileRoute);
            this.groupBox3.Controls.Add(this.saveFileRouteBrowse);
            this.groupBox3.Controls.Add(this.saveData);
            this.groupBox3.Location = new System.Drawing.Point(18, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 121);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones de guardado de datos";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Enabled = false;
            this.fileNameTextBox.Location = new System.Drawing.Point(118, 55);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(211, 20);
            this.fileNameTextBox.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Nombre del archivo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Ruta de guardado";
            // 
            // openFileConfig
            // 
            this.openFileConfig.FileName = "openFileDialog1";
            // 
            // saveConfig
            // 
            this.saveConfig.Location = new System.Drawing.Point(60, 454);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(114, 37);
            this.saveConfig.TabIndex = 22;
            this.saveConfig.Text = "Guardar Configuración";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.SaveConfig_Click);
            // 
            // loadConfig
            // 
            this.loadConfig.Location = new System.Drawing.Point(204, 454);
            this.loadConfig.Name = "loadConfig";
            this.loadConfig.Size = new System.Drawing.Size(114, 37);
            this.loadConfig.TabIndex = 23;
            this.loadConfig.Text = "Cargar \r\nConfiguración";
            this.loadConfig.UseVisualStyleBackColor = true;
            this.loadConfig.Click += new System.EventHandler(this.LoadConfig_Click);
            // 
            // EyeTrackingConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 532);
            this.Controls.Add(this.saveChanges);
            this.Controls.Add(this.loadConfig);
            this.Controls.Add(this.saveConfig);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pluginGroupBox);
            this.Controls.Add(this.reticleGroupBox);
            this.Controls.Add(this.cancelChanges);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EyeTrackingConfiguration";
            this.Text = "Configuración Rastreo Ocular";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reticleExample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clickTimer)).EndInit();
            this.reticleGroupBox.ResumeLayout(false);
            this.reticleGroupBox.PerformLayout();
            this.pluginGroupBox.ResumeLayout(false);
            this.pluginGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox trackingPlugins;
        private System.Windows.Forms.Button saveChanges;
        private System.Windows.Forms.Button cancelChanges;
        private System.Windows.Forms.PictureBox reticleExample;
        private System.Windows.Forms.ComboBox reticleSelected;
        private System.Windows.Forms.CheckBox controlMouse;
        private System.Windows.Forms.Label reticleDimensions;
        private System.Windows.Forms.ToolTip toolTipMouseControl;
        private System.Windows.Forms.CheckBox saveData;
        private System.Windows.Forms.ToolTip toolTipSaveData;
        private System.Windows.Forms.Button pluginsRouteBrowse;
        private System.Windows.Forms.Button reticlesRouteBrowse;
        private System.Windows.Forms.Button saveFileRouteBrowse;
        private System.Windows.Forms.NumericUpDown clickTimer;
        private System.Windows.Forms.TextBox saveFileRoute;
        private System.Windows.Forms.FolderBrowserDialog routeBrowser;
        private System.Windows.Forms.GroupBox reticleGroupBox;
        private System.Windows.Forms.GroupBox pluginGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pluginsRoute;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox reticlesRoute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTipBrowsePlugin;
        private System.Windows.Forms.ToolTip toolTipBrowseReticle;
        private System.Windows.Forms.ToolTip toolTipBrowseSaveFile;
        private System.Windows.Forms.OpenFileDialog openFileConfig;
        private System.Windows.Forms.Button saveConfig;
        private System.Windows.Forms.Button loadConfig;
        private System.Windows.Forms.SaveFileDialog saveFileConfig;
    }
}