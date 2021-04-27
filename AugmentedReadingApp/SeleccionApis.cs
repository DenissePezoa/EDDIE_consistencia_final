using InterfacesModuloWeb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AugmentedReadingApp
{
    public partial class SeleccionApis : Form
    {
        public static string apiSeleccionadaEnciclopedia;
        public static string apiSeleccionadaDefinicion;
        public static string apiSeleccionadaTraduccion;
        public static string apiSeleccionadaVideo;
        public static string apiSeleccionadaImagen;
        public static string idiomaSeleccionadoTraduccion;

        public static string path;

        ProjectionScreenActivity projectionScreenActivity;

        public SeleccionApis(ProjectionScreenActivity formProjection)
        {
            projectionScreenActivity = formProjection;
            InitializeComponent();

            path = Directory.GetCurrentDirectory() + "\\Apis";
        }

        public void SeleccionApis_Load(object sender, EventArgs e)
        {

            List<string> apisEnciclopedia = new List<string>();
            apisEnciclopedia = obtenerListaApisEnciclopedia();
            foreach (var api in apisEnciclopedia)
            {
                cbx_apisEnciclopedia.Items.Add(api);
            }

            List<string> apisDefinicion = new List<string>();
            apisDefinicion = obtenerListaApisDefiniciones();
            foreach (var api in apisDefinicion)
            {
                cbx_apisDefiniciones.Items.Add(api);
            }

            List<string> apisTraduccion = new List<string>();
            apisTraduccion = obtenerListaApisTraduccion();
            foreach (var api in apisTraduccion)
            {
                cbx_apisTraducciones.Items.Add(api);
            }

            List<string> apisVideos = new List<string>();
            apisVideos = obtenerListaApisVideos();
            foreach (var api in apisVideos)
            {
                cbx_apisVideos.Items.Add(api);
            }

            List<string> apisImagenes = new List<string>();
            apisImagenes = obtenerListaApisImagenes();
            foreach (var api in apisImagenes)
            {
                cbx_apisImagenes.Items.Add(api);
            }

            Dictionary<string, string> idiomas = new Dictionary<string, string>();
            idiomas = crearLenguajesTraduccion();
            cbx_idiomaTraducir.DataSource = new BindingSource(idiomas, null);
            cbx_idiomaTraducir.ValueMember = "Key";
            cbx_idiomaTraducir.DisplayMember = "Value";

        }

        protected Dictionary<String, String> crearLenguajesTraduccion()
        {
            Dictionary<string, string> idiomasTraductor = new Dictionary<string, string>();
            idiomasTraductor.Add("es", "Español");
            idiomasTraductor.Add("en", "Inglés");
            return idiomasTraductor;

        }

        protected List<String> obtenerListaApisEnciclopedia()
        {
            string extensionsPath = path;
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");
            var loaders = (
                    from file in pluginFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetTypes()
                    where typeof(IBusquedaEnciclopedia).IsAssignableFrom(type)
                    select (IBusquedaEnciclopedia)Activator.CreateInstance(type)
                    ).ToArray();

            var nombresApiEnciclopedia = new List<String>();
            foreach (var loader in loaders)
            {
                nombresApiEnciclopedia.Add(loader.getName());
            }
            return nombresApiEnciclopedia;
        }

        protected List<String> obtenerListaApisDefiniciones()
        {
            string extensionsPath = path;
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");
            var loaders = (
                    from file in pluginFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetTypes()
                    where typeof(IDefiniciones).IsAssignableFrom(type)
                    select (IDefiniciones)Activator.CreateInstance(type)
                    ).ToArray();

            var nombresApiDefiniciones = new List<String>();
            foreach (var loader in loaders)
            {
                nombresApiDefiniciones.Add(loader.getName());
            }
            return nombresApiDefiniciones;
        }

        protected List<String> obtenerListaApisVideos()
        {
            string extensionsPath = path;
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");
            var loaders = (
                    from file in pluginFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetTypes()
                    where typeof(IBusquedaVideos).IsAssignableFrom(type)
                    select (IBusquedaVideos)Activator.CreateInstance(type)
                    ).ToArray();

            var nombresApiVideos = new List<String>();
            foreach (var loader in loaders)
            {
                nombresApiVideos.Add(loader.getName());
            }
            return nombresApiVideos;
        }

        protected List<String> obtenerListaApisTraduccion()
        {
            string extensionsPath = path;
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");
            var loaders = (
                    from file in pluginFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetTypes()
                    where typeof(ITraducciones).IsAssignableFrom(type)
                    select (ITraducciones)Activator.CreateInstance(type)
                    ).ToArray();

            var nombresApiTraduccion = new List<String>();
            foreach (var loader in loaders)
            {
                nombresApiTraduccion.Add(loader.getName());
            }
            return nombresApiTraduccion;
        }

        protected List<String> obtenerListaApisImagenes()
        {
            string extensionsPath = path;
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");
            var loaders = (
                    from file in pluginFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetTypes()
                    where typeof(IBusquedaImagenes).IsAssignableFrom(type)
                    select (IBusquedaImagenes)Activator.CreateInstance(type)
                    ).ToArray();

            var nombresApiImagenes = new List<String>();
            foreach (var loader in loaders)
            {
                nombresApiImagenes.Add(loader.getName());
            }
            return nombresApiImagenes;
        }

        private void btn_guardarConfiguraciones_Click(object sender, EventArgs e)
        {
            apiSeleccionadaEnciclopedia = cbx_apisEnciclopedia.GetItemText(cbx_apisEnciclopedia.SelectedItem);
            apiSeleccionadaDefinicion = cbx_apisDefiniciones.GetItemText(cbx_apisDefiniciones.SelectedItem);
            apiSeleccionadaTraduccion = cbx_apisTraducciones.GetItemText(cbx_apisTraducciones.SelectedItem);
            apiSeleccionadaVideo = cbx_apisVideos.GetItemText(cbx_apisVideos.SelectedItem);
            apiSeleccionadaImagen = cbx_apisImagenes.GetItemText(cbx_apisImagenes.SelectedItem);
            idiomaSeleccionadoTraduccion = cbx_idiomaTraducir.GetItemText(cbx_idiomaTraducir.SelectedValue);
            MessageBox.Show("Apis seleccionadas con éxito");
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
