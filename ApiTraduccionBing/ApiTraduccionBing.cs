using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTraduccionBing
{
    public class ApiTraduccionBing:InterfacesModuloWeb.ITraducciones
    {
        public string TraducirTexto(string textoTraducir, string idiomaTraducir)
        {

            string host = "https://api.cognitive.microsofttranslator.com";
            const string subscriptionKey = "7ca3ee07d6b9437b82d0e65b100dfa57";
            System.Object[] body = new System.Object[] { new { Text = textoTraducir } };

            ////Código para detectar el idioma de la palabra.
            string route1 = "/detect?api-version=3.0";
            var requestBody1 = JsonConvert.SerializeObject(body);
            string lenguaje = string.Empty;
            using (var client1 = new HttpClient())
            using (var request1 = new HttpRequestMessage())
            {
                request1.Method = HttpMethod.Post;
                request1.RequestUri = new Uri(host + route1);
                request1.Content = new StringContent(requestBody1, Encoding.UTF8, "application/json");
                request1.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                var response = client1.SendAsync(request1).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var data = (JArray)JsonConvert.DeserializeObject(jsonResponse);
                foreach (var item in data.Children())
                {
                    var itemProperties = item.Children<JProperty>();
                    var element = itemProperties.FirstOrDefault(x => x.Name == "language");
                    lenguaje = (string)element.Value;
                }
            }

            //Código para traducir la palabra a español
            string route2 = "/translate?api-version=3.0&to=" + lenguaje + "&to=" + idiomaTraducir;
            var requestBody2 = JsonConvert.SerializeObject(body);
            using (var client2 = new HttpClient())
            using (var request2 = new HttpRequestMessage())
            {
                request2.Method = HttpMethod.Post;
                request2.RequestUri = new Uri(host + route2);
                request2.Content = new StringContent(requestBody2, Encoding.UTF8, "application/json");
                request2.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                var response = client2.SendAsync(request2).Result;
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                var test = jsonResponse.Split('[');
                var test2 = test[2].Split(',');
                var test3 = test2[2].Split(':');
                var traduccion = test3[1];

                return traduccion;
            }
        }

        public string getName()
        {
            return "ApiTraduccionBing";
        }
        public string getVersion()
        {
            return "1.0.0";
        }
    }
}
