using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiBusquedaImagenCloudVision 
{
    public class ApiBusquedaImagenCloudVision : InterfacesModuloWeb.IBusquedaImagenes
    {
        public List<string> buscarImagen(byte[] bytefile) {
            const string apiKey = "AIzaSyAVcc4LrDqYlqWP-l2H9DM7Bo8FpaB4fE0";
            string base64 = Convert.ToBase64String(bytefile);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://vision.googleapis.com/v1/images:annotate?key=" + apiKey);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            string jsonText = "'" + base64 + "'";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{'requests':[{" +
                    "'image': {" +
                    "'content': " + jsonText +
                    "}," +
                    "'features':[{" +
                    "'type':'WEB_DETECTION'}]" +
                    "}]" +
                    "}";

                streamWriter.Write(json);
                Console.WriteLine("El valor del Json Obtenido es:", json);
                streamWriter.Flush();
                streamWriter.Close();
            }



            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            List<string> listaDescripcionesDuplicadas = new List<string>();
            List<string> listaDescripciones = new List<string>();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JObject jresult = JObject.Parse(result);
                var descripciones = from p in jresult["responses"][0]["webDetection"]["webEntities"]
                                    select (string)p["description"];
                foreach (var item in descripciones)
                {
                    if (item != null)
                    {
                        listaDescripcionesDuplicadas.Add(item);
                        listaDescripciones = listaDescripcionesDuplicadas.Distinct().ToList();
                    }
                }
            }
            return listaDescripciones;
        }
        public string getName()
        {
            return "ApiBusquedaImagenCloudVision";
        }
        public string getVersion()
        {
            return "1.0.0";
        }
    }
}
