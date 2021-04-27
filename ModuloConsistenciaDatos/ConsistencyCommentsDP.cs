using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModuloConsistenciaDatos
{
    public class ConsistencyCommentsDP
    {
        public List<List<string>> GetComments(string namePDF)
        {
            string path = Directory.GetCurrentDirectory() + "\\PluginsConsistencia\\";

            string extensionsPath = path + "ConsistencyLibraryCommentsDP.dll";

            //JObject figurasObtenidas = new JObject();
            var assembly = Assembly.LoadFile(extensionsPath);

            List<List<string>> aux_res = new List<List<string>>();

            try
            {
                // load the assembly or type
                // foreach (var type in assembly.GetTypes())
                //{

                Type type = assembly.GetTypes()[0];
                Console.WriteLine("tipossss555 " + type);
                Console.WriteLine("Cargo correctamente el archivo de tipo " + type);
                object obj = Activator.CreateInstance(type);
                Console.WriteLine("cargo objeto  " + obj.ToString());
                var result = type.GetMethod("GetComments");
                Console.WriteLine("cargo metodo " + result.Name);
                object[] parametros = new object[] { namePDF };
                Console.WriteLine("parametrosss  " + parametros[0].ToString());
                var respuesta_comments = result.Invoke(obj, new object[] { namePDF });
                aux_res = (List<List<string>>)respuesta_comments;
                return aux_res;
                // }



            }
            catch (Exception ex)
            {



                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    var typeLoadException = ex as ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    foreach (var item in loaderExceptions)
                    {

                        Console.WriteLine("Error " + item.Message);
                    }
                }

                if (ex.InnerException != null)
                {
                    string err = ex.InnerException.Message;
                    Console.WriteLine("Error de carga de bibliotecas 1 " + err);
                }
            }

            return aux_res;
        }
    }
}
