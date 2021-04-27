using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuloBusquedaWeb
{
    public class BuscarImagen
    {
        public List<string> buscarImagen(byte[] bytefile, string apiSeleccionada) {

            string path = Directory.GetCurrentDirectory() + "/Apis/";
            string extensionsPath = path + apiSeleccionada + ".dll";

            //string extensionsPath = @"C:\Users\Dania\source\repos\1IntegrandoModuloConSoftware2\Apis\" + apiSeleccionada + ".dll";
            var assembly = Assembly.LoadFile(extensionsPath);
            Type type = assembly.GetTypes()[0];
            object obj = Activator.CreateInstance(type);
            var result = type.GetMethod("buscarImagen");
            var respuesta = result.Invoke(obj, new object[] {bytefile });
            List<string> resultadosObtenidos = new List<string>();
            resultadosObtenidos = (List<string>)respuesta;
            return resultadosObtenidos;
        }
    }
}
