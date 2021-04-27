using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuloBusquedaWeb
{
    public class BuscarEnciclopedia
    {
        public List<string> buscarEnciclopedia(string textoBuscar, string apiSeleccionada)
        {
            string path = Directory.GetCurrentDirectory() + "/Apis/";
            string extensionsPath = path + apiSeleccionada + ".dll";

            //string extensionsPath = @"C:\Users\Dania\source\repos\1IntegrandoModuloConSoftware2\Apis\" + apiSeleccionada+".dll";
            var assembly = Assembly.LoadFile(extensionsPath);
            Type type = assembly.GetTypes()[0];
            object obj = Activator.CreateInstance(type);
            var result = type.GetMethod("buscarEnciclopedia");
            var respuesta = result.Invoke(obj, new object[] { textoBuscar });
            List<string> resultadoWikipedia = new List<string>();
            resultadoWikipedia = (List<string>)respuesta;
            return resultadoWikipedia;
        }
    }
}
