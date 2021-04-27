using System;
using iTextSharp.text.pdf;
using InterfacesModuloConsistencia;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;
using System.IO;

namespace ConsistencyLibraryPageMarkerPD
{
    public class PageMarkerPD : IPageMarkerPD
    {


        public int GetPageMarker(Mat _frame, string namePDF, int page)
        {
            int res = 0;
            int marker=0;
            Mat m = new Mat();
            Mat n = new Mat();
            Mat o = new Mat();
            Mat aux = new Mat();
            Mat binaryDiffFrame = new Mat();
            Mat denoisedDiffFrame = new Mat();
            Mat finalFrame = new Mat();




            //
            //OBTENER COLOR
            //
            Image<Bgr, Byte> imge = _frame.ToImage<Bgr, Byte>();
            Image<Bgr, byte> ret = imge.Copy();
            Image<Bgr, byte> auxImge = imge.Copy();
            Image<Bgr, byte> auxImge2 = imge.Copy();
            Image<Bgr, byte> auxImge3 = imge.Copy();
            Image<Bgr, byte> resultadoFinal = imge.Copy();
            //Transformar a espacio de color HSV
            Image<Hsv, Byte> hsvimg = auxImge.Convert<Hsv, Byte>();

            //extract the hue and value channels
            Image<Gray, Byte>[] channels = hsvimg.Split();  //separar en componentes
            Image<Gray, Byte> imghue = channels[0];            //hsv, channels[0] es hue.
            Image<Gray, Byte> imgval = channels[2];            //hsv, channels[2] es value.

            //Filtro AZUL --> 90 a 120
            //Verde --> 40 a 70
            Image<Gray, byte> huefilter = imghue.InRange(new Gray(90), new Gray(120));
            //Filtro colores menos brillantes
            Image<Gray, byte> valfilter = imgval.InRange(new Gray(100), new Gray(255));
            //Filtro de saturación - quitar blancos 
            channels[1]._ThresholdBinary(new Gray(10), new Gray(255)); // Saturacion

            //Unir los filtros para obtener la imagen
            Image<Gray, byte> colordetimg = huefilter.And(valfilter).And(channels[1]);//aqui habia un Not()

            //Colorear imagen

            var mat = auxImge2.Mat;
            mat.SetTo(new MCvScalar(0, 0, 255), colordetimg);
            mat.CopyTo(ret);
            //Image<Bgr, byte> imgout = ret.CopyBlank();//imagen sin fondo negro

            ret._Or(auxImge2);
            //Muestra imagen con los rojos destacados
            //pictureBox2.Image = ret.Bitmap;

            Mat SE2 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 2), new Point(-1, -1));
            CvInvoke.MorphologyEx(colordetimg, colordetimg, MorphOp.Erode, SE2, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            Mat SE3 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 2), new Point(-1, -1));
            CvInvoke.MorphologyEx(colordetimg, colordetimg, MorphOp.Dilate, SE3, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));


            Mat SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(colordetimg, aux, Emgu.CV.CvEnum.MorphOp.Close, SE, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));

            //pictureBox2.Image = aux.Bitmap;
            Image<Bgr, byte> temp = aux.ToImage<Bgr, byte>();

            var temp2 = temp.SmoothGaussian(5).Convert<Gray, byte>().ThresholdBinary(new Gray(230), new Gray(255));
            VectorOfVectorOfPoint contorno = new VectorOfVectorOfPoint();
            Mat matAux = new Mat();
            CvInvoke.FindContours(temp2, contorno, matAux, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            if (contorno.Size > 0)
            {
                for (int i = 0; i < contorno.Size; i++)
                {

                    VectorOfPoint approxContour = new VectorOfPoint();
                    double perimetro = CvInvoke.ArcLength(contorno[i], true);
                    VectorOfPoint approx = new VectorOfPoint();

                    double area = CvInvoke.ContourArea(contorno[i]);
                    if (area > 1000)
                    {
                        var moments = CvInvoke.Moments(contorno[i]);
                        int x = (int)(moments.M10 / moments.M00);
                        int y = (int)(moments.M01 / moments.M00);

                        CvInvoke.ApproxPolyDP(contorno[i], approx, 0.04 * perimetro, true);
                        CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(0, 255, 255), 2);
                        RotatedRect rectangle = CvInvoke.MinAreaRect(approx);
                        marker = 1; 

                        //CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 2, LineType.AntiAlias);
                        //MessageBox.Show("Tamano figura " + rectangle.Size.Width * rectangle.Size.Height);
                        ////resultadoFinal.Draw(rectangle, new Bgr(Color.Cyan), 1);
                        ////CvInvoke.PutText(resultadoFinal, "Marcador Pagina", new Point(x, y),
                        /////Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 255), 2);
                        //pictureBox3.Image = resultadoFinal.ToBitmap();

                    }
                }
                if (marker == 1)
                {
                    res = 10;
                    int res2 = SavePageMarker(namePDF,page);
                    return res;
                }
                else
                {
                    res = 0;
                    return res;
                }
            }

            return res;


        }

        public int SavePageMarker(string namePDF, int page)
        {
            int res = 0;
            string oldFile = namePDF;
            string newFile = "temporal.pdf";
            PdfReader reader = new PdfReader(oldFile);
            IList<Dictionary<String, Object>> bmProperties = SimpleBookmark.GetBookmark(reader);
            PdfStamper stamp = new PdfStamper(reader, new FileStream(newFile, FileMode.Create));
            if (bmProperties != null)
            {
                //MessageBox.Show("Cantidad marcadores " + bmProperties.Count());
                
                Dictionary<String, Object> marcador = new Dictionary<String, Object>();
                marcador.Add("Action", "GoTo");
                marcador.Add("Title", "Esto es otro marcador");
                marcador.Add("Page", "2 XYZ 100 100 0"); // use height of 1st page
                bmProperties.Add(marcador);
                stamp.Outlines = bmProperties;
                stamp.Close();
                //System.Diagnostics.Process.Start(newFile);
                //Console.Read();
                reader.Close();
                File.Replace(newFile, oldFile, @"backup.pdf.bac");

                //File.Replace(@"temporal.pdf", @"ejemploOK.pdf", @"backup.pdf.bac");
               Console.WriteLine("Pdf modificado con exito (tenia marc), se ha guardado un backup de la versión anterior ");
            }
            else
            {


                //stamp.GetUnderContent(1);
                //var h= stamp.GetImportedPage(reader, 1).Height;
               
                IList<Dictionary<String, Object>> marcadores = new List<Dictionary<String, Object>>();
                Dictionary<String, Object> marcador = new Dictionary<String, Object>();
                //marcadores.Add(marcador);
                marcador.Add("Action", "GoTo");
                marcador.Add("Title", "Page1 0 H 0");
                marcador.Add("Page", "1 XYZ 100 100 0"); // use height of 1st page
               // MessageBox.Show("marcador " + marcador.Count);

                //MessageBox.Show("marcador " + marcador.Keys);
                marcadores.Add(marcador);


                stamp.Outlines = marcadores;
                stamp.Close();
                //System.Diagnostics.Process.Start(newFile);
                //Console.Read();
                reader.Close();
                File.Replace(newFile, oldFile, @"backup.pdf.bac");

                //File.Replace(@"temporal.pdf", @"ejemploOK.pdf", @"backup.pdf.bac");
                Console.WriteLine("Pdf modificado con exito (no tenia marc), se ha guardado un backup de la versión anterior ");
            }



            return res;

    }

        public string GetName()
        {
            string name = "PageMarkerPD";
            return name;
        }
        public string GetVersion()
        {
            string version = "1.0";
            return version;
        }

       
    }
}
