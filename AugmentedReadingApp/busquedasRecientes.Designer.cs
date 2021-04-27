namespace AugmentedReadingApp
{
    partial class busquedasRecientes
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Api = new System.Windows.Forms.Label();
            this.lbl_palabraBuscada = new System.Windows.Forms.Label();
            this.lbl_fecha_hora = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "En:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Búsqueda:";
            // 
            // lbl_Api
            // 
            this.lbl_Api.AutoSize = true;
            this.lbl_Api.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Api.Location = new System.Drawing.Point(101, 70);
            this.lbl_Api.Name = "lbl_Api";
            this.lbl_Api.Size = new System.Drawing.Size(87, 20);
            this.lbl_Api.TabIndex = 13;
            this.lbl_Api.Text = "En que Api";
            // 
            // lbl_palabraBuscada
            // 
            this.lbl_palabraBuscada.AutoSize = true;
            this.lbl_palabraBuscada.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_palabraBuscada.Location = new System.Drawing.Point(93, 36);
            this.lbl_palabraBuscada.Name = "lbl_palabraBuscada";
            this.lbl_palabraBuscada.Size = new System.Drawing.Size(128, 20);
            this.lbl_palabraBuscada.TabIndex = 12;
            this.lbl_palabraBuscada.Text = "Palabra buscada";
            // 
            // lbl_fecha_hora
            // 
            this.lbl_fecha_hora.AutoSize = true;
            this.lbl_fecha_hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fecha_hora.Location = new System.Drawing.Point(1, 5);
            this.lbl_fecha_hora.Name = "lbl_fecha_hora";
            this.lbl_fecha_hora.Size = new System.Drawing.Size(96, 20);
            this.lbl_fecha_hora.TabIndex = 11;
            this.lbl_fecha_hora.Text = "fecha y hora";
            // 
            // busquedasRecientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Api);
            this.Controls.Add(this.lbl_palabraBuscada);
            this.Controls.Add(this.lbl_fecha_hora);
            this.Name = "busquedasRecientes";
            this.Size = new System.Drawing.Size(244, 93);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Api;
        private System.Windows.Forms.Label lbl_palabraBuscada;
        private System.Windows.Forms.Label lbl_fecha_hora;
    }
}
