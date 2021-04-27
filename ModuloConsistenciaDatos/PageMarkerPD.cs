using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
namespace ModuloConsistenciaDatos
{
    public class PageMarkerPD
    {
        public int GetPageMarker(Mat _frame,string nombrePDF, int page)
        {

            string path = Directory.GetCurrentDirectory() + "\\PluginsConsistencia\\";

            string extensionsPath = path + "ConsistencyLibraryPageMarkerPD.dll";

            //JObject figurasObtenidas = new JObject();
            var assembly = Assembly.LoadFile(extensionsPath);

            int aux_res=0;

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
                var result = type.GetMethod("GetPageMarker");
                Console.WriteLine("cargo metodo " + result.Name);
                object[] parametros = new object[] {_frame,nombrePDF };
                Console.WriteLine("parametrosss  " + parametros[0].ToString());
                var respuesta = result.Invoke(obj, new object[] { _frame,nombrePDF, page });
                aux_res = (int)respuesta;
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
                /* if (ex is System.Reflection.TargetInvocationException)
                 {
                     var exception = ex as TargetInvocationException;
                     var loaderExceptions = exception.Message;

                     Console.WriteLine("Error de carga de bibliotecas 2 " + loaderExceptions);

                 }*/
            }
            
            return aux_res;

        }
    }
}
