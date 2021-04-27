using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuloBusquedaWeb
{
    public class BuscarDefinicion
    {
        public List<string> buscarDefinicion(string palabraBuscada, string apiSeleccionada) {

            List<string> definicionesObtenidas = new List<string>();

            string path = Directory.GetCurrentDirectory() + "/Apis/";
            string extensionsPath = path + apiSeleccionada + ".dll";
            
            //string extensionsPath = @"C:\Users\Dania\source\repos\1IntegrandoModuloConSoftware2\Apis\" + apiSeleccionada + ".dll";
            var assembly = Assembly.LoadFile(extensionsPath);
            Type type = assembly.GetTypes()[0];
            object obj = Activator.CreateInstance(type);
            var result = type.GetMethod("buscarDefiniciones");
            var respuesta = result.Invoke(obj, new object[] { palabraBuscada });

            definicionesObtenidas = (List<string>)respuesta;

            return definicionesObtenidas;
        }
    }
}
