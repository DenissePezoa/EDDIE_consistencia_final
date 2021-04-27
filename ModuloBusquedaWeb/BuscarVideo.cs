using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuloBusquedaWeb
{
    public class BuscarVideo
    {
        public string buscarVideo(string videoBuscar, string apiSeleccionada)
        {
            string path = Directory.GetCurrentDirectory() + "/Apis/";
            string extensionsPath = path + apiSeleccionada + ".dll";
            //string extensionsPath = @"C:\Users\Dania\source\repos\1IntegrandoModuloConSoftware2\Apis\" + apiSeleccionada + ".dll";
            var assembly = Assembly.LoadFile(extensionsPath);
            Type type = assembly.GetTypes()[0];
            object obj = Activator.CreateInstance(type);
            var result = type.GetMethod("buscarVideo");
            var respuesta = result.Invoke(obj, new object[] { videoBuscar });
            var strVideoBuscado = respuesta.ToString();
            return strVideoBuscado;
        }
    }
}
