using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

//using iText.Kernel.Colors;
//using iText.Kernel.Pdf;
//using iText.Kernel.Pdf.Canvas;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using InterfacesModuloConsistencia;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ConsistencyLibraryFiguresPD
{
    public class FiguresPD : IFiguresPD
    {
        private Mat _frame;
        //public string nombrePDF;
        public string framee;
        public List<VectorOfPoint> resultados = new List<VectorOfPoint>();
        public List<System.Drawing.Rectangle> rect = new List<System.Drawing.Rectangle>();
        public List<CircleF> circleList = new List<CircleF>();
        public List<Ellipse> ellipseList = new List<Ellipse>();

        //Mat _frame
        public int ObtenerFiguras(Mat _frame, string nombrePdf, int page)
        {
            Console.WriteLine("Entro en Metodo obtener figuras del dll");
            int resultado=0;
            //resultados.Clear();

            //if (_frame.IsEmpty)
            //{
               //return;
            //}
            
                Mat finalFrame = new Mat();
                Mat aux = new Mat();
                Mat aux2 = new Mat();
                Mat aux3 = new Mat();
                Image<Bgr, byte> img = _frame.ToImage<Bgr, byte>();
                //Transformar a espacio de color HSV
                Image<Hsv, Byte> hsvimg = img.Convert<Hsv, Byte>();

                //extract the hue and value channels
                Image<Gray, Byte>[] channels = hsvimg.Split();  //separar en componentes
                Image<Gray, Byte> imghue = channels[0];            //hsv, channels[0] es hue.
                Image<Gray, Byte> imgval = channels[2];            //hsv, channels[2] es value.

                //Filtro color
                //140 en adelante 
                //funciona  160
                Image<Gray, byte> huefilter = imghue.InRange(new Gray(150), new Gray(255));
                //Filtro colores menos brillantes
                Image<Gray, byte> valfilter = imgval.InRange(new Gray(100), new Gray(255));
                //Filtro de saturación - quitar blancos 
                channels[1]._ThresholdBinary(new Gray(20), new Gray(255)); // Saturacion

                //Unir los filtros para obtener la imagen
                Image<Gray, byte> colordetimg = huefilter.And(valfilter).And(channels[1]);//aqui habia un Not()
                                                                                          //pictureBox2.Image = colordetimg.Bitmap;
                                                                                          //colordetimg._Erode(1);
                                                                                          //2 y 4
                Mat SE2 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 2), new System.Drawing.Point(1, 1));
                CvInvoke.MorphologyEx(colordetimg, aux, MorphOp.Erode, SE2, new System.Drawing.Point(-1, -1), 2, BorderType.Default, new MCvScalar(255));
                Mat SE3 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new System.Drawing.Point(1, 1));
                CvInvoke.MorphologyEx(aux, aux2, MorphOp.Dilate, SE3, new System.Drawing.Point(-1, -1), 3, BorderType.Replicate, new MCvScalar(255));

                Mat SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new System.Drawing.Point(-1, -1));
                CvInvoke.MorphologyEx(aux2, aux3, Emgu.CV.CvEnum.MorphOp.Close, SE, new System.Drawing.Point(-1, -1), 5, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));
                CvInvoke.MorphologyEx(aux3, aux3, Emgu.CV.CvEnum.MorphOp.Open, SE, new System.Drawing.Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));

                

                _frame.CopyTo(finalFrame);

               // DetectarFiguras(aux3, finalFrame);

            // System.Drawing.Rectangle box = new System.Drawing.Rectangle();
            Image<Bgr, byte> temp = aux3.ToImage<Bgr, byte>();
            //Image<Bgr, Byte> buffer_im = displayFrame.ToImage<Bgr, Byte>();
            //float a = buffer_im.Width;
            //float b = buffer_im.Height;
            //MessageBox.Show("El tamano es   "+ a.ToString()+" y " +  b.ToString());


            rect.Clear();
            circleList.Clear();
            ellipseList.Clear();

            //transforma imagen
            //UMat uimage = new UMat();
            // CvInvoke.CvtColor(displayFrame, uimage, ColorConversion.Bgr2Gray);
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {

                Image<Bgr, Byte> resultadoFinal = finalFrame.ToImage<Bgr, byte>();


                double maxArea = 1000;
                //int chosen = 0;
                // VectorOfPoint contour = null;

                //Dibuja borde rojo
                var temp2 = temp.SmoothGaussian(5).Convert<Gray, byte>().ThresholdBinary(new Gray(20), new Gray(255));

                VectorOfVectorOfPoint contorno = new VectorOfVectorOfPoint();
                Mat mat = new Mat();
                CvInvoke.FindContours(temp2, contorno, mat, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);


                for (int i = 0; i < contorno.Size; i++)
                {

                    //VectorOfPoint approxContour = new VectorOfPoint();
                    double perimetro = CvInvoke.ArcLength(contorno[i], true);
                    VectorOfPoint approx = new VectorOfPoint();

                    // VectorOfPointF approxF = new VectorOfPointF();
                    double area = CvInvoke.ContourArea(contorno[i]);
                    if (area > maxArea)
                    {
                        CvInvoke.ApproxPolyDP(contorno[i], approx, 0.04 * perimetro, true);


                        //Obtiene los centros de las figuras
                        var moments = CvInvoke.Moments(contorno[i]);
                        int x = (int)(moments.M10 / moments.M00);
                        int y = (int)(moments.M01 / moments.M00);



                        resultados.Add(approx);

                        if (approx.Size == 3) //The contour has 3 vertices, it is a triangle
                        {

                            RotatedRect rectangle = CvInvoke.MinAreaRect(approx);
                            CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 1, LineType.AntiAlias);
                            resultadoFinal.Draw(rectangle, new Bgr(System.Drawing.Color.Cyan), 1);
                            rect.Add(CvInvoke.BoundingRectangle(approx));

                        }

                        if (approx.Size == 4) //The contour has 4 vertices.
                        {
                            //RotatedRect tt = new RotatedRect(CvInvoke.MinAreaRect(approx).Center, CvInvoke.MinAreaRect(approx).Size, 270) ;
                            //boxList.Add(tt);

                            //Calcular si es cuadrado
                            System.Drawing.Rectangle rectAux = CvInvoke.BoundingRectangle(contorno[i]);
                            double ar = (double)rectAux.Width / rectAux.Height;

                            //Calcular circularidad
                            double perimetro2 = CvInvoke.ArcLength(contorno[i], true);
                            double area2 = CvInvoke.ContourArea(contorno[i]);
                            double circularidad = 4 * Math.PI * area2 / Math.Pow(perimetro2, 2);

                            //MessageBox.Show("circularidad rect " + circularidad);
                            if (circularidad > 0.69)
                            {
                                //Si la circularidad>0.6 y cumple proporcion es cuadrado
                                if (ar >= 0.8 && ar <= 1.0)
                                {
                                    // MessageBox.Show("Cuadrado ");
                                    RotatedRect rectangle = CvInvoke.MinAreaRect(contorno[i]);
                                    CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 1, LineType.AntiAlias);
                                    resultadoFinal.Draw(rectangle, new Bgr(System.Drawing.Color.Cyan), 1);
                                    CvInvoke.PutText(resultadoFinal, "Rectangle", new System.Drawing.Point(x, y),
                                    Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 255), 2);
                                    rect.Add(CvInvoke.BoundingRectangle(approx));

                                }
                                //Es elipse 
                                else
                                {
                                    //MessageBox.Show("parecia rectangulo pero era elipse ");
                                    Ellipse final_ellipse = new Ellipse(CvInvoke.MinAreaRect(contorno[i]).Center, CvInvoke.MinAreaRect(contorno[i]).Size, 0);
                                    Ellipse final_ellipseDibujo = new Ellipse(CvInvoke.MinAreaRect(contorno[i]).Center, CvInvoke.MinAreaRect(contorno[i]).Size, 90);
                                    ellipseList.Add(final_ellipse);

                                    //IConvexPolygonF poligono = CvInvoke.MinAreaRect(approx);
                                    //resultadoFinal.Draw(poligono, new Bgr(Color.Cyan), 1);
                                    resultadoFinal.Draw(final_ellipseDibujo, new Bgr(System.Drawing.Color.Cyan), 1);
                                    CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 1, LineType.AntiAlias);
                                    CvInvoke.PutText(resultadoFinal, "Figura circular", new System.Drawing.Point(x, y),
                                            Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 255), 2);
                                }
                            }
                            //Es rectangulo
                            else
                            {
                                RotatedRect rectangle = CvInvoke.MinAreaRect(contorno[i]);
                                CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 1, LineType.AntiAlias);
                                resultadoFinal.Draw(rectangle, new Bgr(System.Drawing.Color.Cyan), 1);
                                CvInvoke.PutText(resultadoFinal, "Rectangle", new System.Drawing.Point(x, y),
                                Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 255), 2);
                                rect.Add(CvInvoke.BoundingRectangle(approx));


                            }
                        }

                        if (approx.Size >= 5)
                        {

                            //double perimetro2 = CvInvoke.ArcLength(contorno[i], true);
                            //double area2 = CvInvoke.ContourArea(contorno[i]);
                            // double circularidad = 4 * Math.PI * area2 / Math.Pow(perimetro2, 2);
                            //MessageBox.Show("circularidad elipse " + circularidad);

                            Ellipse final_ellipse = new Ellipse(CvInvoke.MinAreaRect(contorno[i]).Center, CvInvoke.MinAreaRect(contorno[i]).Size, 0);
                            Ellipse final_ellipseDibujo = new Ellipse(CvInvoke.MinAreaRect(contorno[i]).Center, CvInvoke.MinAreaRect(contorno[i]).Size, 90);
                            ellipseList.Add(final_ellipse);


                            resultadoFinal.Draw(final_ellipseDibujo, new Bgr(System.Drawing.Color.Cyan), 1);
                            CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 1, LineType.AntiAlias);
                            CvInvoke.PutText(resultadoFinal, "Figura circular", new System.Drawing.Point(x, y),
                                    Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 255), 2);




                        }



                    }

                }

                //pictureBox2.Image = resultadoFinal.Bitmap;
                //button2.Enabled = true;
            }

            if (rect.Count != 0 ||  ellipseList.Count !=0)
            {
                resultado = 1;
                Console.WriteLine("Rect: "+rect.Count+" y elipse: "+ellipseList.Count);
                Console.WriteLine("llamare a guardar figuras nombrepdf " + nombrePdf);
                GuardarFiguras(nombrePdf,page);
            }
            else
            {
                resultado = 0;
            }
            
            return  resultado;

            

        }




        public void GuardarFiguras(string nombrePDF, int page)
        {

            Console.WriteLine("Entro a funcion guardar figuras nombrepdf "+nombrePDF);
            string oldFile = nombrePDF;
            string newFile = "C:\\Users\\Denisse\\Desktop\\EDDIE-Augmented-Reading-master\\AugmentedReadingApp\\bin\\x86\\Debug\\temporal.pdf";


            PdfReader reader = new PdfReader(oldFile);
            var pageSize = reader.GetPageSize(1);
            Console.WriteLine("Tamano de pagina PDF " + pageSize);
            PdfStamper stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Append));


            PdfContentByte contentunder = stamper.GetUnderContent(1);

            //int rot;
            //rot = reader.GetPageRotation(1);
            //PdfDictionary pageDict;
            //pageDict = reader.GetPageN(1);
            //pageDict.Put(PdfName.ROTATE, new PdfNumber(rot + 90));


            float incremento = 130;
            foreach (var item in rect)
            {
                //float center1 = item.X+(item.X+item.Width)/2;
                //float center2 = item.Y + (item.Y + item.Height) / 2;
                //RotatedRect aux = new RotatedRect(new PointF(item.X, item.Y), item.Size, 180);
                //PointF[] puntos = aux.GetVertices();
                //float puntoX = puntos[0].X;
                //float puntoY = puntos[0].Y;
                //float tamanoH = item.Size.Height;
                //float tamanoW = item.Size.Width;


                float porcentaje = (item.Y * 100 / 640);
                double nuevoPtoY = (porcentaje * (841)) / 100;
                double proporcion = nuevoPtoY / item.Y;
                if (porcentaje < 50)
                {
                    incremento = 0;
                }
                else if (porcentaje > 50 && porcentaje < 80)
                {
                    incremento = 70;
                }
                else
                {
                    incremento = 100;
                }


                contentunder.SetColorStroke(BaseColor.YELLOW);
                ////Antigua coordenada contentunder.Rectangle(item.X - 50, (841 * 0.85) - (nuevoPtoY * proporcion)+incremento, item.Width * 1.3, item.Height * proporcion);
                ////nueva coordenada
                double propX = ((item.X * 100) / 640);
                double ptoX2 = ((propX * 792) / 100);
                double propY = ((item.Y * 100) / 480);
                double ptoY2 = ((propY * 612) / 100);
                double propArea = 1.58;
                double propW = ((item.Width) * 100) / 640;
                double ptoW2 = ((propW * 792) / 100);
                double propH = ((item.Height * 100) / 480);
                double ptoH2 = ((propH * 612) / 100);
                contentunder.Rectangle(ptoX2 + 30, (612 - ptoY2 - (ptoH2 - 30)), ptoW2 - 30, ptoH2 - 30);

                //contentunder.Rectangle(puntoX, puntoY, tamanoW, tamanoH);
                contentunder.Stroke();

            }

            //Sincronizar circulos
            foreach (var circle in circleList)
            {
                /*double x = circle.Center.X;
                double y = circle.Center.Y;
                double r = circle.Radius;
                double porcentaje = (y * 100 / 640);
                double nuevoPtoY = (porcentaje * (841*0.9)) / 100;
                double proporcion = nuevoPtoY / y;

              // Setting color to the circle
              contentunder.SetColorStroke(BaseColor.MAGENTA);
              // creating a circle
              contentunder.Circle(x, (841*0.9)-(nuevoPtoY), r*proporcion);*/
                double propX = ((circle.Center.X * 100) / 640);
                double ptoX2 = ((propX * 792) / 100);
                double propY = ((circle.Center.Y * 100) / 480);
                double ptoY2 = ((propY * 612) / 100);
                double r = circle.Radius;
                // Setting color to the circle
                contentunder.SetColorStroke(BaseColor.MAGENTA);
                // creating a circle
                contentunder.Circle(ptoX2, 612 - ptoY2 - r, r * 1.58);
                // Filling the circ
                //canvas.Fill();
                contentunder.Stroke();
            }


            //Sincronizar elipses y circulos
            foreach (var ellipse in ellipseList)
            {
                System.Drawing.Rectangle rr = ellipse.RotatedRect.MinAreaRect();
                double propLeft = ((rr.Left * 100) / 640);
                double ptoLeft2 = ((propLeft * 792) / 100);

                double propRight = ((rr.Right * 100) / 640);
                double ptoRight2 = ((propRight * 792) / 100);

                double propBottom = ((rr.Bottom * 100) / 480);
                double ptoBottom2 = ((propBottom * 612) / 100);

                double propTop = ((rr.Top * 100) / 480);
                double ptoTop2 = ((propTop * 612) / 100);

                contentunder.SetColorStroke(BaseColor.GREEN);
                contentunder.Ellipse(ptoLeft2, 612 - ptoBottom2, ptoRight2, 612 - ptoTop2);
                /*double porcentajeB = (rr.Bottom * 100 / 640);
                double nuevoPtoB = (porcentajeB * (841)) / 100;
                double proporcionB = nuevoPtoB / rr.Bottom;

                double porcentajeT = (rr.Top * 100 / 640);
                double nuevoPtoT = (porcentajeT * (841)) / 100;
                double proporcionT = nuevoPtoB / rr.Top;

               contentunder.SetColorStroke(BaseColor.GREEN);
               contentunder.Ellipse(rr.Left - 50, (841 * 0.9) - (nuevoPtoB), (rr.Right * 1.1) - 80, (841 * 0.9) - (nuevoPtoT));*/
                contentunder.Stroke();
            }

            stamper.Close();
            reader.Close();

            File.Replace(@newFile, oldFile, @"backup.pdf.bac");

           

            Console.WriteLine("Finalizado");
            //File.Replace(@"temporal.pdf", @"ejemploOK.pdf", @"backup.pdf.bac");
           // MessageBox.Show("Pdf modificado con exito, se ha guardado un backup de la versión anterior ");
            //axAcroPDF1.src = "C:\\Users\\Denisse\\Desktop\\prototipos\\Prototipo4-FigurasFisicoDigital\\Prototipo4-FigurasFisicoDigital\\bin\\Debug\\ejemploOK.pdf";

        }

        public string GetName()
        {
            string name = "FiguresPD";
            return name;
        }

        public string GetVersion()
        {
            string version = "1.0";
            return version;
        }
    }
}
