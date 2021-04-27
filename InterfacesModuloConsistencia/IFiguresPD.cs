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
    public interface IFiguresPD
    {
        int ObtenerFiguras(Mat imagen, string nombrePDF, int page);

        void GuardarFiguras(string nombrePDF, int page);
        string GetName();
        string GetVersion();
    }
}
