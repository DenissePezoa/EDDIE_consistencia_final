using System;
using System.Collections.Generic;
using InterfacesModuloConsistencia;
using iTextSharp.text.pdf;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System.IO;
using System.Drawing;

namespace ConsistencyLibraryCommentsPD
{
    public class CommentsPD : ICommentsPD
    {

        List<Point> coorComentario = new List<Point>();
        Image<Bgr, Byte> final;
        int cantComentarios;
        string directorio;
        public List<List<string>> GetComments(string namePDF, Mat _frame, int page)
        {
            List<List<string>> comments = new List<List<string>>();
            List<String> aux_comments = new List<string>();
            
            Image<Bgr, byte> imagen_aux = _frame.ToImage<Bgr, byte>();
            imagen_aux = imagen_aux.Rotate(180, new Bgr(0, 0, 0));
            Mat m = new Mat();
            Mat n = new Mat();
            Mat o = new Mat();
            Mat aux = new Mat();
            Mat binaryDiffFrame = new Mat();
            Mat denoisedDiffFrame = new Mat();
            Mat finalFrame = new Mat();

           
            //Obtener directorio
            directorio = Directory.GetCurrentDirectory();
            Console.WriteLine(directorio);

            //Cantidad de comentarios
            cantComentarios = 0;

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

            //Filtro amarillo
            //140 en adelante //30
            Image<Gray, byte> huefilter = imghue.InRange(new Gray(0), new Gray(30));
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
            //Muestra imagen con color destacado

            Mat SE2 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 2), new Point(-1, -1));
            CvInvoke.MorphologyEx(colordetimg, colordetimg, MorphOp.Erode, SE2, new Point(-1, -1), 3, BorderType.Default, new MCvScalar(255));
            Mat SE3 = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 2), new Point(-1, -1));
            CvInvoke.MorphologyEx(colordetimg, colordetimg, MorphOp.Dilate, SE3, new Point(-1, -1), 4, BorderType.Default, new MCvScalar(255));


            Mat SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(colordetimg, aux, Emgu.CV.CvEnum.MorphOp.Close, SE, new Point(-1, -1), 5, Emgu.CV.CvEnum.BorderType.Reflect, new MCvScalar(255));


            Image<Bgr, byte> temp = aux.ToImage<Bgr, byte>();
            var temp2 = temp.SmoothGaussian(5).Convert<Gray, byte>().ThresholdBinary(new Gray(230), new Gray(255));
            VectorOfVectorOfPoint contorno = new VectorOfVectorOfPoint();
            Mat matAux = new Mat();

            CvInvoke.FindContours(temp2, contorno, matAux, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < contorno.Size; i++)
            {

                VectorOfPoint approxContour = new VectorOfPoint();
                double perimetro = CvInvoke.ArcLength(contorno[i], true);
                VectorOfPoint approx = new VectorOfPoint();
                double area = CvInvoke.ContourArea(contorno[i]);

                if (area > 15000)
                {
                    var moments = CvInvoke.Moments(contorno[i]);
                    int x = (int)(moments.M10 / moments.M00);
                    int y = (int)(moments.M01 / moments.M00);
                    CvInvoke.ApproxPolyDP(contorno[i], approx, 0.04 * perimetro, true);

                    if (approx.Size == 4) //The contour has 4 vertices.
                    {
                        cantComentarios = cantComentarios + 1;
                        CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(0, 255, 255), 2);
                        RotatedRect rectangle = CvInvoke.MinAreaRect(approx);
                        //CvInvoke.DrawContours(resultadoFinal, contorno, i, new MCvScalar(255, 255, 255), 2, LineType.AntiAlias);
                        resultadoFinal.Draw(rectangle, new Bgr(Color.Cyan), 1);
                        CvInvoke.PutText(resultadoFinal, "Comentario", new Point(x, y),
                        Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 255, 255), 2);
                        System.Drawing.Point[] pts = approx.ToArray();
                        System.Drawing.Rectangle box = new System.Drawing.Rectangle();
                        box = CvInvoke.BoundingRectangle(approx);


                        coorComentario.Add(new Point(box.X, box.Y));
                        auxImge3.ROI = box;
                        final = auxImge3.Copy();
                        //Bitmap src = bmp;
                        final = final.Rotate(180, new Bgr(0, 0, 0));
                        final.Save("comment" + cantComentarios + ".jpg");

                        Image<Gray, Byte> finalGray = final.Convert<Gray, Byte>();
                        CvInvoke.cvResetImageROI(auxImge3);
                        //El parámetro blockSize es el tamaño de la vecindad, y el valor de los píxeles procesados ​​debe ser mayor que el valor de vecindad promedio menos param1
                        CvInvoke.AdaptiveThreshold(finalGray, finalGray, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, 31, 20);
                    }

                }
            }
            Console.WriteLine("cantidad de comentarios " + cantComentarios);
            if (cantComentarios > 0)
            {
                aux_comments.Add("SI");
                comments.Add(aux_comments);
                SaveComments(namePDF, page);
            }

            else
            {
                aux_comments.Add("NO");
                comments.Add(aux_comments);
            }

            return comments;
        }

        public string GetName()
        {
            string name = "CommentsDP";
            return name;
        }

        public string GetVersion()
        {
            string version = "1.0";
            return version;
        }

        public void SaveComments(string namePDF, int page)
        {
            string oldFile = namePDF;
            string newFile = "temporal.pdf";

            PdfReader pdfReader = new PdfReader(oldFile);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            Point[] finalcoor = coorComentario.ToArray();
            for (int i = 1; i <= cantComentarios; i++)
            {
                string imageURL = directorio + "\\comment" + i + ".jpg";

                Stream inputImageStream = new FileStream(imageURL, FileMode.Open, FileAccess.Read, FileShare.Read);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                //int xpos = finalcoor[i - 1].X;
                //int ypos = finalcoor[i - 1].Y;
                Console.WriteLine("estas son las coordenadas" + finalcoor[i - 1].X + " y " + finalcoor[i - 1].Y);
                int xpos = 100;
                int ypos = 100;
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(35 + xpos, 650 - ypos, 35 + xpos + Convert.ToSingle(image.Width * 0.5), 650 - ypos + Convert.ToSingle(image.Height * 0.5));
                PdfAnnotation pdfStamp = PdfAnnotation.CreateStamp(pdfStamper.Writer, rect, null, Guid.NewGuid().ToString());
                image.SetAbsolutePosition(0, 0);
                PdfAppearance app = pdfStamper.GetOverContent(1).CreateAppearance(Convert.ToSingle(image.Width * 0.5), Convert.ToSingle(image.Height * 0.5));
                image.ScalePercent(50);
                app.AddImage(image);
                pdfStamp.SetAppearance(PdfName.N, app);
                pdfStamp.SetPage();
                pdfStamp.Flags = PdfAnnotation.FLAGS_PRINT;
                pdfStamper.AddAnnotation(pdfStamp, 1);
            }
            

            pdfStamper.Close();
            pdfReader.Close();
            File.Replace(newFile, oldFile, @"backup.pdf.bac");

            Console.WriteLine("Pdf modificado con exito, se ha guardado un backup de la versión anterior ");
            //axAcroPDF1.src = "C:\\Users\\Denisse\\Desktop\\prototipos\\prot2_ReconocerTexto\\prot2_ReconocerTexto\\bin\\Debug\\ejemploOK.pdf";

        }
    }
}
