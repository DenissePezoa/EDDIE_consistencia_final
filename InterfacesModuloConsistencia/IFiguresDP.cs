using System;
using System.Collections.Generic;
using System.Text;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Newtonsoft.Json.Linq;

namespace InterfacesModuloConsistencia
{
    public interface IFiguresDP
    {
        //Método principal que obtiene las figuras desde el doc digital
        JObject ObtenerFiguras(string nombrePDF, int pagePDF);

        //Método que retorna el nombre de la biblioteca 
        string GetName();
        //Método que retorna la versión de la biblioteca 
        string GetVersion();

      
    }
}



