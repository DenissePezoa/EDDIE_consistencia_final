using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AugmentedReadingApp
{
    public partial class busquedasRecientes : UserControl
    {
        public busquedasRecientes()
        {
            InitializeComponent();
        }
        public string fechaYhora
        {
            get { return lbl_fecha_hora.Text; }
            set { lbl_fecha_hora.Text = value; }
        }

        public string terminoBuscado
        {
            get { return lbl_palabraBuscada.Text; }
            set { lbl_palabraBuscada.Text = value; }
        }

        public string servicioApi
        {
            get { return lbl_Api.Text; }
            set { lbl_Api.Text = value; }
        }
    }
}
