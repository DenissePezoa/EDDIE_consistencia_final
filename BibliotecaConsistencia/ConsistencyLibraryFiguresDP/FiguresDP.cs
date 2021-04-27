using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using InterfacesModuloConsistencia;

using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;

namespace ConsistencyLibraryFiguresDP
{
    public class FiguresDP : IFiguresDP
    {
        public List<System.Drawing.Rectangle> rectList = new List<System.Drawing.Rectangle>();
        public List<CircleF> circleList = new List<CircleF>();
        public List<Ellipse> ellipseList = new List<Ellipse>();
        
        public string GetName()
        {
            string name = "FiguresDP";
            return name;
        }

        public string GetVersion()
        {
            string version = "1.0";
            return version;
        }

    

        public JObject ObtenerFiguras(string nombrePDF, int pagePDF)
        {
            string oldFile = nombrePDF;
            string newFile = "C:\\Users\\Denisse\\Desktop\\EDDIE-Augmented-Reading-master\\AugmentedReadingApp\\bin\\x86\\Debug\\temporal.pdf";
            PdfReader reader = new PdfReader(oldFile);
            PdfDictionary pageDict = reader.GetPageN(1);
           



            iTextSharp.text.Rectangle pagesize = reader.GetPageSize(1);
            
            double anchoPDF = pagesize.Width-30;
            double altoPDF = pagesize.Height-30;
            // MessageBox.Show("Tamano pagina pdf --> ancho :" + anchoPDF + " y alto : " + altoPDF);
            float anchoImagen = 400;
            float altoImagen = 450;
            //MessageBox.Show("Tamano imagen --> ancho :" + anchoImagen+ " y alto : " + altoImagen);

            // TODO: Si annotArray es null se arroja una excepción
            PdfArray annotArray = pageDict.GetAsArray(PdfName.ANNOTS);

            try { 
                for (int i = 0; i < annotArray.Size; ++i)
                {
                    PdfDictionary curAnnot = annotArray.GetAsDict(i);
                    var subtype = curAnnot.Get(PdfName.SUBTYPE);
                    var rect = curAnnot.Get(PdfName.RECT);
                    var contents = curAnnot.Get(PdfName.CONTENTS);
                    var page = curAnnot.Get(PdfName.P);
                    //MessageBox.Show("Subtipo "+subtype + " coordenadas: "+rect + " y contenido "+contents);
                    if (subtype.ToString() == "/Square")
                    {

                        Console.WriteLine("Encontro un rectangulo/cuadrado ");

                        //MessageBox.Show("Figura rectangular detectada coor "+rect);
                        var aux_rect = rect.ToString().Split(',');
                        aux_rect[0] = aux_rect[0].Remove(0, 1);
                        aux_rect[3] = aux_rect[3].Remove(aux_rect[3].Length - 1);
                        aux_rect[0] = aux_rect[0].Replace(".", ",");
                        aux_rect[1] = aux_rect[1].Replace(".", ",");
                        aux_rect[2] = aux_rect[2].Replace(".", ",");
                        aux_rect[3] = aux_rect[3].Replace(".", ",");
                        //MessageBox.Show("el split primero " + aux_rect[0]+ " el split ultimo "+ aux_rect[3]);
                        // MessageBox.Show("Esto es rect " + aux_rect[0] + " " + aux_rect[1] + " " + aux_rect[2] + " " + aux_rect[3]);
                        int puntox1 = Convert.ToInt32(Convert.ToSingle(aux_rect[0]));
                        int puntoy1 = Convert.ToInt32(Convert.ToSingle(aux_rect[1]));
                        int puntox2 = Convert.ToInt32(Convert.ToSingle(aux_rect[2]));
                        int puntoy2 = Convert.ToInt32(Convert.ToSingle(aux_rect[3]));
                        // MessageBox.Show("puntos "+ puntox1+" ,"+ puntoy1+ " , "+ puntox2+" , "+ puntoy2);

                        // TRANSFORMAR COORDENADAS DESDE PDF
                        int nuevoX1 = (Convert.ToInt32(anchoImagen) * (puntox1 * 100 / Convert.ToInt32(anchoPDF))) / 100;
                        int nuevoX2 = (Convert.ToInt32(anchoImagen) * (puntox2 * 100 / Convert.ToInt32(anchoPDF))) / 100;
                        int nuevoY1 =((Convert.ToInt32(altoImagen) * (puntoy1 * 100 / Convert.ToInt32(altoPDF))) / 100);
                        int nuevoY2 = ((Convert.ToInt32(altoImagen) * (puntoy2 * 100 / Convert.ToInt32(altoPDF))) / 100);
                        // MessageBox.Show("puntos " + nuevoX1 + " ," + nuevoY1 + " , " + nuevoX2 + " , " + nuevoY2);

                        Rectangle rectangulo = new Rectangle(puntox1, puntoy1, puntox1 + puntox2, puntoy1 + puntoy2);

                        //Rectangle rectangulo = new Rectangle(0, 0, 100, 200);
                        //fondo.Draw(rectangulo, new Bgr(Color.Cyan), 1);
                        Rectangle rectangulo2 = new Rectangle(nuevoX1, (int)altoImagen- nuevoY1, nuevoX2 - nuevoX1, nuevoY2 - nuevoY1);
                        //Rectangle rectangulo = new Rectangle(0, 0, 100, 200);
                        //fondo.Draw(rectangulo2, new Bgr(Color.Red), 1);
                        rectList.Add(rectangulo2);

                    }


                    if (subtype.ToString() == "/Circle")
                    {
                    

                        var aux_rect = rect.ToString().Split(',');
                        aux_rect[0] = aux_rect[0].Remove(0, 1);
                        aux_rect[3] = aux_rect[3].Remove(aux_rect[3].Length - 1);
                        aux_rect[0] = aux_rect[0].Replace(".", ",");
                        aux_rect[1] = aux_rect[1].Replace(".", ",");
                        aux_rect[2] = aux_rect[2].Replace(".", ",");
                        aux_rect[3] = aux_rect[3].Replace(".", ",");
                        //MessageBox.Show("el split primero " + aux_rect[0]+ " el split ultimo "+ aux_rect[3]);
                        // MessageBox.Show("Esto es rect " + aux_rect[0] + " " + aux_rect[1] + " " + aux_rect[2] + " " + aux_rect[3]);
                        int puntox1 = Convert.ToInt32(Convert.ToSingle(aux_rect[0]));
                        int puntoy1 = Convert.ToInt32(Convert.ToSingle(aux_rect[1]));
                        int puntox2 = Convert.ToInt32(Convert.ToSingle(aux_rect[2]));
                        int puntoy2 = Convert.ToInt32(Convert.ToSingle(aux_rect[3]));
                        // MessageBox.Show("puntos "+ puntox1+" ,"+ puntoy1+ " , "+ puntox2+" , "+ puntoy2);

                        // TRANSFORMAR COORDENADAS DESDE PDF
                        int nuevoX1 = (Convert.ToInt32(anchoImagen) * (puntox1 * 100 / Convert.ToInt32(anchoPDF))) / 100;
                        int nuevoX2 = (Convert.ToInt32(anchoImagen) * (puntox2 * 100 / Convert.ToInt32(anchoPDF))) / 100;
                        int nuevoY1 = ((Convert.ToInt32(altoImagen) * (puntoy1 * 100 / Convert.ToInt32(altoPDF))) / 100);
                        int nuevoY2 = ((Convert.ToInt32(altoImagen) * (puntoy2 * 100 / Convert.ToInt32(altoPDF))) / 100);
                        // MessageBox.Show("puntos " + nuevoX1 + " ," + nuevoY1 + " , " + nuevoX2 + " , " + nuevoY2);

                        Rectangle rectangulo = new Rectangle(puntox1, puntoy1, puntox1 + puntox2, puntoy1 + puntoy2);
                        //Rectangle rectangulo = new Rectangle(0, 0, 100, 200);
                        //fondo.Draw(rectangulo, new Bgr(Color.Cyan), 1);
                        Rectangle rectangulo2 = new Rectangle(nuevoX1, nuevoY1, nuevoX2 - nuevoX1, nuevoY2 - nuevoY1);
                        float mitadX = nuevoX1 + (rectangulo2.Width / 2)-30;
                        float mitadY = (int)altoImagen - nuevoY1 + (rectangulo2.Height / 2);
                        Console.WriteLine("x1 " + nuevoX1 + "y x2 " + nuevoX2);
                        Console.WriteLine("Mitades " + mitadX + " y " + mitadY);
                        Console.WriteLine("tamanos " + rectangulo2.Width + " y " + rectangulo2.Height);

                        Ellipse elip = new Ellipse(new PointF(mitadX, mitadY), new Size(rectangulo2.Width-30, rectangulo2.Height), 90);
                        //Rectangle rectangulo = new Rectangle(0, 0, 100, 200);

                        //fondo.Draw(elip, new Bgr(Color.Blue), 1);
                        //pictureBox1.Image = fondo.Bitmap;
                        ellipseList.Add(elip);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            //Guarda figuras en formato json
            string resultado = "{\"figuras\" : {\"rectangulos\" : [";
            string auxRes = "";
            int j = 0;
            if (rectList.Count == 0)
            {
                auxRes = auxRes + "],";
            }
            else
            {
                foreach (var item in rectList)
                {

                    if (rectList.Count - 1 == j)
                    {
                       // auxRes = auxRes + "\"rect" + j + "\":{";
                        auxRes = auxRes + "{\"puntoX\":";
                        auxRes = auxRes + "\"" + item.X + "\",";
                        auxRes = auxRes + "\"puntoY\":";
                        auxRes = auxRes + "\"" + item.Y + "\",";
                        auxRes = auxRes + "\"width\":";
                        auxRes = auxRes + "\"" + item.Width + "\",";
                        auxRes = auxRes + "\"height\":";
                        auxRes = auxRes + "\"" + item.Height + "\"}]";
                    }
                    else
                    {
                       // auxRes = auxRes + "\"rect" + j + "\":{";
                        auxRes = auxRes + "{\"puntoX\":";
                        auxRes = auxRes + "\"" + item.X + "\",";
                        auxRes = auxRes + "\"puntoY\":";
                        auxRes = auxRes + "\"" + item.Y + "\",";
                        auxRes = auxRes + "\"width\":";
                        auxRes = auxRes + "\"" + item.Width + "\",";
                        auxRes = auxRes + "\"height\":";
                        auxRes = auxRes + "\"" + item.Height + "\"},";
                    }

                    j = j + 1;
                }
            }


            auxRes = auxRes + " ,\"circulos\": [";
            //Agregar circulos y elipses
            j = 0;
            if (ellipseList.Count == 0)
            {
                auxRes = auxRes + "]";
            }
            else
            {
                foreach (var item in ellipseList)
                {
                    RotatedRect aux_ellipse = item.RotatedRect;

                    if (ellipseList.Count - 1 == j)
                    {
                       // auxRes = auxRes + "\"circ" + j + "\":{";
                        auxRes = auxRes + "{\"puntoX\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Center.X + "\",";
                        auxRes = auxRes + "\"puntoY\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Center.Y + "\",";
                        auxRes = auxRes + "\"width\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Size.Width + "\",";
                        auxRes = auxRes + "\"height\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Size.Height + "\"}]";
                    }
                    else
                    {
                        //auxRes = auxRes + "\"circ" + j + "\":{";
                        auxRes = auxRes + "{\"puntoX\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Center.X + "\",";
                        auxRes = auxRes + "\"puntoY\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Center.Y + "\",";
                        auxRes = auxRes + "\"width\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Size.Width + "\",";
                        auxRes = auxRes + "\"height\":";
                        auxRes = auxRes + "\"" + aux_ellipse.Size.Height + "\"},";
                    }

                    j = j + 1;
                }
            }

            auxRes = auxRes + "}}";
            resultado = resultado + auxRes;
            Console.WriteLine("Este es el resultado " + resultado);
            var final = JObject.Parse(resultado);
            

            return final;



        }


        public void GuardarFiguras(string nombrePDF)
        {
            throw new NotImplementedException();
        }

    }
}
