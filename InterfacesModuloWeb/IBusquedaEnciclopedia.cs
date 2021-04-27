using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesModuloWeb
{
    public interface IBusquedaEnciclopedia
    {
        List<string> buscarEnciclopedia(string articulo);
        string getName();
        string getVersion();
    }
}
