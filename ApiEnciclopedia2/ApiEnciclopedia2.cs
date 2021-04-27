using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEnciclopedia2
{
    public class ApiEnciclopedia2:InterfacesModuloWeb.IBusquedaEnciclopedia
    {
        public List<string> buscarEnciclopedia(string articulo)
        {
            List<string> resultadosObtenidos = new List<string>();
            var resultado = "Esta es una prueba para ver cómo se comporta el sistema cuando se tienen 2 apis que implementan una misma interfaz";
            resultadosObtenidos.Add(resultado);
            return resultadosObtenidos;
        }
        public string getName()
        {
            return "ApiEnciclopedia2";
        }

        public string getVersion()
        {
            return "1.0.0";
        }
    }
}
