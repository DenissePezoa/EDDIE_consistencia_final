using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesModuloWeb
{
    public interface IBusquedaVideos
    {
        string buscarVideo(string palabraClave);
        string getName();
        string getVersion();
    }
}
