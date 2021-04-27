using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuloBusquedaWeb
{
    public class TraducirTexto
    {
        public string traducirTexto(string textoTraducir, string idiomaTraducir, string apiSeleccionada) {

            string traduccion = null;

            string path = Directory.GetCurrentDirectory() + "/Apis/";
            string extensionsPath = path + apiSeleccionada + ".dll";


            var assembly = Assembly.LoadFile(extensionsPath);

            foreach (Type type in assembly.GetExportedTypes())
            {
                object obj = Activator.CreateInstance(type);
                var result = type.GetMethod("TraducirTexto");
                var respuesta = result.Invoke(obj, new object[] { textoTraducir, idiomaTraducir });
                traduccion = respuesta.ToString();
            }

            return traduccion;

        }
        
         
    }
}
