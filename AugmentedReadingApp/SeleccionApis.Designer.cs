namespace AugmentedReadingApp
{
    partial class SeleccionApis
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
            this.label5 = new System.Windows.Forms.Label();
            this.cbx_idiomaTraducir = new System.Windows.Forms.ComboBox();
            this.btn_guardarConfiguraciones = new System.Windows.Forms.Button();
            this.cbx_apisImagenes = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_apisTraducciones = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_apisDefiniciones = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_apisVideos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Enciclopedias = new System.Windows.Forms.Label();
            this.cbx_apisEnciclopedia = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(343, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 20);
            this.label5.TabIndex = 47;
            this.label5.Text = "Seleccione el idioma al cual traducir:";
            // 
            // cbx_idiomaTraducir
            // 
            this.cbx_idiomaTraducir.FormattingEnabled = true;
            this.cbx_idiomaTraducir.Location = new System.Drawing.Point(455, 249);
            this.cbx_idiomaTraducir.Name = "cbx_idiomaTraducir";
            this.cbx_idiomaTraducir.Size = new System.Drawing.Size(145, 21);
            this.cbx_idiomaTraducir.TabIndex = 46;
            // 
            // btn_guardarConfiguraciones
            // 
            this.btn_guardarConfiguraciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_guardarConfiguraciones.Location = new System.Drawing.Point(673, 383);
            this.btn_guardarConfiguraciones.Name = "btn_guardarConfiguraciones";
            this.btn_guardarConfiguraciones.Size = new System.Drawing.Size(115, 57);
            this.btn_guardarConfiguraciones.TabIndex = 45;
            this.btn_guardarConfiguraciones.Text = "Guardar";
            this.btn_guardarConfiguraciones.UseVisualStyleBackColor = true;
            this.btn_guardarConfiguraciones.Click += new System.EventHandler(this.btn_guardarConfiguraciones_Click);
            // 
            // cbx_apisImagenes
            // 
            this.cbx_apisImagenes.FormattingEnabled = true;
            this.cbx_apisImagenes.Location = new System.Drawing.Point(16, 329);
            this.cbx_apisImagenes.Name = "cbx_apisImagenes";
            this.cbx_apisImagenes.Size = new System.Drawing.Size(156, 21);
            this.cbx_apisImagenes.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Seleccione una api para búsqueda de imágenes:";
            // 
            // cbx_apisTraducciones
            // 
            this.cbx_apisTraducciones.FormattingEnabled = true;
            this.cbx_apisTraducciones.Location = new System.Drawing.Point(16, 249);
            this.cbx_apisTraducciones.Name = "cbx_apisTraducciones";
            this.cbx_apisTraducciones.Size = new System.Drawing.Size(156, 21);
            this.cbx_apisTraducciones.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 20);
            this.label3.TabIndex = 41;
            this.label3.Text = "Seleccione una api para traducciones:";
            // 
            // cbx_apisDefiniciones
            // 
            this.cbx_apisDefiniciones.FormattingEnabled = true;
            this.cbx_apisDefiniciones.Location = new System.Drawing.Point(16, 106);
            this.cbx_apisDefiniciones.Name = "cbx_apisDefiniciones";
            this.cbx_apisDefiniciones.Size = new System.Drawing.Size(156, 21);
            this.cbx_apisDefiniciones.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 20);
            this.label2.TabIndex = 39;
            this.label2.Text = "Seleccione una api para definiciones:";
            // 
            // cbx_apisVideos
            // 
            this.cbx_apisVideos.FormattingEnabled = true;
            this.cbx_apisVideos.Location = new System.Drawing.Point(16, 178);
            this.cbx_apisVideos.Name = "cbx_apisVideos";
            this.cbx_apisVideos.Size = new System.Drawing.Size(156, 21);
            this.cbx_apisVideos.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 20);
            this.label1.TabIndex = 37;
            this.label1.Text = "Seleccione una api para videos:";
            // 
            // lbl_Enciclopedias
            // 
            this.lbl_Enciclopedias.AutoSize = true;
            this.lbl_Enciclopedias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Enciclopedias.Location = new System.Drawing.Point(12, 11);
            this.lbl_Enciclopedias.Name = "lbl_Enciclopedias";
            this.lbl_Enciclopedias.Size = new System.Drawing.Size(275, 20);
            this.lbl_Enciclopedias.TabIndex = 36;
            this.lbl_Enciclopedias.Text = "Seleccione una api para enciclopedia:";
            // 
            // cbx_apisEnciclopedia
            // 
            this.cbx_apisEnciclopedia.FormattingEnabled = true;
            this.cbx_apisEnciclopedia.Location = new System.Drawing.Point(16, 34);
            this.cbx_apisEnciclopedia.Name = "cbx_apisEnciclopedia";
            this.cbx_apisEnciclopedia.Size = new System.Drawing.Size(156, 21);
            this.cbx_apisEnciclopedia.TabIndex = 35;
            // 
            // SeleccionApis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbx_idiomaTraducir);
            this.Controls.Add(this.btn_guardarConfiguraciones);
            this.Controls.Add(this.cbx_apisImagenes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_apisTraducciones);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_apisDefiniciones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbx_apisVideos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Enciclopedias);
            this.Controls.Add(this.cbx_apisEnciclopedia);
            this.Name = "SeleccionApis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeleccionApis";
            this.Load += new System.EventHandler(this.SeleccionApis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbx_idiomaTraducir;
        private System.Windows.Forms.Button btn_guardarConfiguraciones;
        private System.Windows.Forms.ComboBox cbx_apisImagenes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_apisTraducciones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_apisDefiniciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_apisVideos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Enciclopedias;
        private System.Windows.Forms.ComboBox cbx_apisEnciclopedia;
    }
}