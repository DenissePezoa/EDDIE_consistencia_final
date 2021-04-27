using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AugmentedReadingApp
{
    public partial class SeleccionInteraccionPorVoz : Form
    {
        public static string activarBusquedaVoz;
        public static string mostrarBotonesconVoz;

        public SeleccionInteraccionPorVoz()
        {
            InitializeComponent();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (rbtn_voz_si.Checked && rbtn_Si_botones.Checked)
            {
                activarBusquedaVoz = rbtn_voz_si.Text;
                mostrarBotonesconVoz = rbtn_Si_botones.Text;
                MessageBox.Show("Ha seleccionado la opción de interacción por voz");
                this.Hide();
            }
            if (rbtn_voz_si.Checked && rbtn_no_botones.Checked)
            {
                activarBusquedaVoz = rbtn_voz_si.Text;
                mostrarBotonesconVoz = rbtn_no_botones.Text;
                MessageBox.Show("Ha seleccionado la opción de interacción por voz");
                this.Hide();
            }
            if (rbtn_voz_no.Checked)
            {
                activarBusquedaVoz = rbtn_voz_no.Text;
                MessageBox.Show("Ha seleccionado la opción de interacción por botones");
                this.Hide();
            }

        }

    }
}
