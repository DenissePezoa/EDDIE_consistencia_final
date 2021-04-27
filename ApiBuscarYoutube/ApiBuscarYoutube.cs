using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBuscarYoutube
{
    public class ApiBuscarYoutube : InterfacesModuloWeb.IBusquedaVideos
    {
        public string buscarVideo(string palabraClave)
        {
            string palabraClaveLista = palabraClave.Replace(" ", "+");
            var completeURL = "https://www.youtube.com/results?search_query=" + palabraClaveLista;
            return completeURL;
        }
        public string getName()
        {
            return "ApiBuscarYoutube";
        }
        public string getVersion()
        {
            return "1.0.0";
        }
    }
}
