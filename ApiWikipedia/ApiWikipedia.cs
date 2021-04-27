using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ApiWikipedia
{
    public class ApiWikipedia : InterfacesModuloWeb.IBusquedaEnciclopedia
    {

        public List<string> buscarEnciclopedia(string articulo)
        {
            var data = new WebClient().DownloadString("https://es.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&callback=&utf8=1&formatversion=latest&exlimit=1&exintro=1&explaintext=1&exsectionformat=plain&exvariant=pl&titles=" + articulo);
            var json = data.Replace("/", "").Replace("*", "").Replace("(", "").Replace(")", "");
            List<string> resultados = new List<string>();
            try
            {
                JObject jresult = JObject.Parse(json);

                var salida = from p in jresult["query"]["pages"]
                             select p["extract"].ToString();

                foreach (var elemento in salida)
                {
                    if (elemento == "")
                    {
                        resultados.Add("No se ha encontrado el termino buscado");
                    }
                    else
                    {
                        resultados.Add(elemento);
                    }
                }

            }
            catch (Exception e) { resultados.Add("No se ha encontrado el termino buscado"); }
            List<string> resultadosListos = new List<string>();
            foreach (var i in resultados)
            {
                byte[] bytes = Encoding.Default.GetBytes(i);
                var textoObtenido = Encoding.UTF8.GetString(bytes);
                Regex rx = new Regex("\\<[^\\><]*\\>");
                textoObtenido = rx.Replace(textoObtenido, "");
                resultadosListos.Add(textoObtenido.ToString());
            }
            return resultadosListos;
        }

        //public List<string> buscarEnciclopedia(string articulo)
        //{
        //    WebClient wc = new WebClient();
        //    string archivo = wc.DownloadString("https://es.wikipedia.org/w/api.php?action=opensearch&search=" + articulo + "&limit=5&format=xml");
        //    XmlDocument xml = new XmlDocument();
        //    xml.LoadXml(archivo);
        //    int cont = 0;
        //    List<string> resultados = new List<string>();

        //    try {
        //        while (cont <= 4)
        //        {
        //            XmlNode nodo = xml.GetElementsByTagName("Description")[cont];
        //            resultados.Add(nodo.InnerText);
        //            cont += 1;
        //        }
        //    }
        //    catch (Exception ex) { resultados.Add("No se ha encontrado el termino buscado"); }


        //    List<string> resultadosListos = new List<string>();

        //    foreach (var i in resultados)
        //    {
        //        byte[] bytes = Encoding.Default.GetBytes(i);
        //        var textoObtenido = Encoding.UTF8.GetString(bytes);
        //        Regex rx = new Regex("\\<[^\\><]*\\>");
        //        textoObtenido = rx.Replace(textoObtenido, "");
        //        resultadosListos.Add(textoObtenido.ToString());
        //    }
        //    return resultadosListos;
        //}
        public string getName()
        {
            return "ApiWikipedia";
        }
        public string getVersion()
        {
            return "1.0.0";
        }
    }
}
