using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesModuloWeb
{
    public interface IDefiniciones
    {
        List<string> buscarDefiniciones(string textoBuscado);
        string getName();
        string getVersion();
    }
}
