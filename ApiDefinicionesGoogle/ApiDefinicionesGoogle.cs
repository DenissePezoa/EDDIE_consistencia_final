using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiDefinicionesGoogle
{
    public class ApiDefinicionesGoogle : InterfacesModuloWeb.IDefiniciones
    {
        public List<string> buscarDefiniciones(string textoBuscado)
        {
            var url = "https://googledictionaryapi.eu-gb.mybluemix.net/?define=" + textoBuscado + "&lang=en";
            WebClient wc = new WebClient();
            string definicion = wc.DownloadString(url);
            definicion = definicion.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            JObject Jdefinicion = JObject.Parse(definicion);
            var _dataResponse = JToken.Parse(JsonConvert.SerializeObject(Jdefinicion));
            var test = _dataResponse.Children();
            //var _dataResponseWord = _dataResponse["word"];
            var _dataResponseMeaning = _dataResponse["meaning"];
            var test2 = _dataResponseMeaning.Children();
            var test3 = test2.Children();
            var test4 = test3.Children();
            var test5 = test4.Children();
            var test6 = test4["definition"];

            List<string> definiciones = new List<string>();

            foreach (var valor in test6)
            {
                definiciones.Add(valor.ToString());
            }

            List<string> definiciones2 = new List<string>();
            foreach (var prueba in definiciones)
            {
                byte[] bytes = Encoding.Default.GetBytes(prueba);
                var prueba2 = Encoding.UTF8.GetString(bytes);
                Regex rx = new Regex("\\<[^\\><]*\\>");
                prueba2 = rx.Replace(prueba2, "");
                definiciones2.Add(prueba2.ToString());

            }

            return definiciones2;
        }
        public string getName()
        {
            return "ApiDefinicionesGoogle";
        }
        public string getVersion()
        {
            return "1.0.0";
        }
    }
}
