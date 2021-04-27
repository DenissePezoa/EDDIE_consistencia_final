using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Emgu.CV;
using Emgu.CV.Structure;
using ModuloBusquedaWeb;
using ModuloConsistenciaDatos;
using ModuloProcesamientoImagenes;
using ModuloVisualizacionDatos;
using Newtonsoft.Json.Linq;
using ModuloRastreoOcular;

namespace AugmentedReadingApp
{
    public partial class ProjectionScreenActivity : Form
    {
        public int CameraNumber;
        InteractionCoordinator originalForm;


        bool markPoint = true;
        public ColorRecognition recTxt;

        public HighlightTool Highlight;

        public ChromiumWebBrowser navegador;

        int panelWidth;
        bool Hidden;

        string conceptoBuscar;
        //string conceptoBuscar = "LQ!ve";

        StringBuilder csvFile = new StringBuilder();
        string csvpath = Directory.GetCurrentDirectory() + "/CSV_registro_actividades/log_actividades.csv";

        int nPage = 1;
        int nPages = 1;


        //Agrega Modulo Consistencia DP
        public int numeroCamara;
        FiguresDP FiguresDP;


        public ProjectionScreenActivity(InteractionCoordinator incomingForm)
        {
            
            originalForm = incomingForm;

            InitializeComponent();
            
            pictureBox3.Image = Image.FromFile("x_mark_red_circle.png");
            pictureBox4.Image = Image.FromFile("x_mark_red_circle.png");
            pictureBox5.Image = Image.FromFile("x_mark_red_circle.png");

            markPoint = false;


            Highlight = new HighlightTool(30, 517, 400)
            {
                Name = "HighlightBox",
                BackColor = Color.FromArgb(255, 255, 255),
                
                //Location = new Point(285, 100),
                Location = new Point(285,65),


            };
            this.Controls.Add(Highlight);
            Highlight.BringToFront();

            Highlight.NumClicks += ButtonAction;//Evento gatillado al terminar un resaltado o una captura de imagen
            Highlight.FirstClicks += FirstClick;

            SeleccionApis formSeleccionApis = new SeleccionApis(this);
            formSeleccionApis.TopMost = true;
            formSeleccionApis.Show();

            panelWidth = panel_log.Width;
            Hidden = true;
            panel_log.Visible = false;
            csvFile.AppendLine("FECHA Y HORA ; PALABRA BUSCADA ; API UTILIZADA");

        }


        private void Form2_Load(object sender, EventArgs e)
        {
            // En esta parte se comprueba si el usuario ha seleccionado//
            // La opción de interacción a través de la voz o no//
            var opcionInteraccionSeleccionada = SeleccionInteraccionPorVoz.activarBusquedaVoz;
            var opcionMostrarBotonesSeleccionada = SeleccionInteraccionPorVoz.mostrarBotonesconVoz;

            if (opcionInteraccionSeleccionada == "Si" && opcionMostrarBotonesSeleccionada == "Si")
            {
                iniciarVR();
                btn_buscarWeb.Visible = true;
            }
            if (opcionInteraccionSeleccionada == "Si" && opcionMostrarBotonesSeleccionada == "No")
            {
                iniciarVR();
                btn_buscarWeb.Visible = false;
            }
            if (opcionInteraccionSeleccionada == "No")
            {
                btn_buscarWeb.Visible = true;
            }
            //Fin de la comprobación para interacción por voz//
            
            CefSettings cfsettings = new CefSettings();

            cfsettings.UserAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G930V Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.125 Mobile Safari/537.36";
            Cef.Initialize(cfsettings);

            navegador = new CefSharp.WinForms.ChromiumWebBrowser("")
            {
                Dock = DockStyle.Fill,
            };
            panel_navegador.Controls.Add(navegador);
            navegador.Visible = false;
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            if (markPoint)
            {


                pictureBox3.Image = Image.FromFile("x_mark_red_circle.png");
                pictureBox4.Image = Image.FromFile("x_mark_red_circle.png");
                pictureBox5.Image = Image.FromFile("x_mark_red_circle.png");

                markPoint = false;
            }
            else
            {
                pictureBox3.Image = null;
                pictureBox4.Image = null;
                pictureBox5.Image = null;
                markPoint = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Highlight.HighLightOn)
            {

                originalForm.CaptureTxt();

                try
                {
                    textBox1.Text = OCRProcess.TransformImage(originalForm.recTxt.TextImage.Bitmap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OCRProcess Resaltado: " + ex.Message);
                }

            }
            else
            {
                originalForm.CaptureImage();

                try
                {
                    textBox1.Text = OCRProcess.TransformImage(originalForm.RectangleImage.Bitmap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OCRProcess rectangulo: " + ex.Message);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (Highlight.HighLightOn)
            {

                textImageButton.Text = "TEXT/IMAGE";
                textImageButton.BackColor = default(Color);
                Highlight.HighLightOn = false;

            }
            else
            {
                //button3.BackColor = Color.Gray;
                textImageButton.BackColor = default(Color);
                textImageButton.Text = "HIGHLIGHT";
                Highlight.HighLightOn = true;
            }


        }



        private void FirstClick()
        {
            originalForm.recGestual.FinalToRectXY();

        }
        private void ButtonAction()
        {

            if (Highlight.HighLightOn)//condicion de que el highlight esta presionado, 
            {                        //si no ocurre se va automaticamente al modo de captura de imagen.

                originalForm.CaptureTxt();

                try
                {
                    textBox1.Text = OCRProcess.TransformImage(originalForm.recTxt.TextImage.Bitmap); //Transforma un fragmento de texto
                    conceptoBuscar = OCRProcess.TransformImage(originalForm.recTxt.TextImage.Bitmap);
                    //string palabraBuscar = OCRProcess.TransformImage(originalForm.recTxt.TextImage.Bitmap);
                    //conceptoBuscar = Regex.Replace(conceptoBuscar, @"\n", "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OCRProcess Resaltado: " + ex.Message);
                }
                conceptoBuscar = Regex.Replace(conceptoBuscar, @"\n", "");
            }
            else
            {
                if (originalForm.captureGesture != null)
                {
                    if (originalForm.plugin.AutoCamCapture)//codicion de que el plugin permita autocapture (funcione con una camara). ej: reconocimiento gestual por lapiz
                    {
                        if (!originalForm.checkBoxMouse.Checked)// capture no esta chequeado
                        {
                            originalForm.recGestual.FinalToRectWH();
                        }

                        originalForm.CaptureImage();

                        //activar si se selecciona un fragmento de texto
                        //try
                        //{
                        //    // textBox1.Text = OCRProcess.TransformImage();
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("OCRProcess rectangulo: " + ex.Message);
                        //}
                    }
                }
            }
        }

        private void syncPdfButton_Click(object sender, EventArgs e)
        {

            float llx = (Highlight.normRect.Left) * originalForm.documentoSyn.rectPage.Right;
            float lly = (1 - Highlight.normRect.Bottom ) * originalForm.documentoSyn.rectPage.Top - (Highlight.normRect.Top) * originalForm.documentoSyn.rectPage.Top;
            float urx = (Highlight.normRect.Left) * originalForm.documentoSyn.rectPage.Right + (Highlight.normRect.Right) * originalForm.documentoSyn.rectPage.Right;
            float ury = (1 - Highlight.normRect.Bottom) * originalForm.documentoSyn.rectPage.Top;
            originalForm.documentoSyn.SaveAnno(llx, lly, urx, ury,nPage);
            Highlight.GetRectangles(originalForm.documentoSyn.listaRectangulos);
            nPages = originalForm.documentoSyn.NPages;
            syncButton.Text = " Sync\nPage " + nPage + "/" +nPages;

        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            nPage++;
            if (nPage > nPages) nPage = nPages; 
            syncButton.Text = " Sync\nPage " + nPage + "/" + nPages;
        }

        private void backPagebutton_Click(object sender, EventArgs e)
        {
            nPage--;
            if (nPage < 1) nPage = 1;
            syncButton.Text = " Sync\nPage " + nPage + "/" + nPages;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            PagesLabel.Text = String.Join(",", originalForm.documentoSyn.SearchTxtPdf(searchTextBox.Text).ToArray()); ;
        }

        private void btn_buscarWeb_Click(object sender, EventArgs e)
        {
            if (btn_diccionario.Visible == false || btn_enciclopedia.Visible == false || btn_traductor.Visible == false || btn_video.Visible == false || btn_imagen.Visible == false)
            {
                btn_diccionario.Visible = true;
                btn_enciclopedia.Visible = true;
                btn_traductor.Visible = true;
                btn_video.Visible = true;
                btn_imagen.Visible = true;
                lbl_diccionario.Visible = true;
                lbl_enciclopedia.Visible = true;
                lbl_traductor.Visible = true;
                lbl_video.Visible = true;
                lbl_imagenes.Visible = true;
            }
            else
            {
                btn_diccionario.Visible = false;
                btn_enciclopedia.Visible = false;
                btn_traductor.Visible = false;
                btn_video.Visible = false;
                btn_imagen.Visible = false;
                lbl_diccionario.Visible = false;
                lbl_enciclopedia.Visible = false;
                lbl_traductor.Visible = false;
                lbl_video.Visible = false;
                lbl_imagenes.Visible = false;
            }
        }

        private void btn_enciclopedia_Click(object sender, EventArgs e)
        {
            buscar_Enciclopedia();
        }

        private void buscar_Enciclopedia()
        {
            btn_cerrarVentanaDerecha.Visible = true;
            rtb_ResultadosWikipedia.Clear();
            lbl_PalabraBuscada.Visible = true;
            rtb_ResultadosWikipedia.Visible = true;
            btn_leerEnciclopedia.Visible = true;

            var textoBuscar = conceptoBuscar;

            busquedasRecientes busquedaReciente = new busquedasRecientes();
            var time = DateTime.Now;
            string formatTime = time.ToString("yyyy/MM/dd-HH:mm:ss");
            busquedaReciente.terminoBuscado = textoBuscar;
            string apiUtilizada = "Enciclopedia";
            busquedaReciente.servicioApi = apiUtilizada;
            busquedaReciente.fechaYhora = formatTime;

            if (string.IsNullOrWhiteSpace(textoBuscar))
            {
                rtb_ResultadosWikipedia.Text = "Seleccione un término para buscar";
            }
            else
            {
                List<string> datosObtenidos = new List<string>();
                BuscarEnciclopedia buscarEnciclopedia = new BuscarEnciclopedia();
                try {
                    fl_busquedasRecientes.Controls.Add(busquedaReciente);
                    csvFile.AppendLine(formatTime + " ; " + textoBuscar + " ; " + apiUtilizada);

                    var apiSeleccionada = SeleccionApis.apiSeleccionadaEnciclopedia;
                    datosObtenidos = buscarEnciclopedia.buscarEnciclopedia(textoBuscar, apiSeleccionada);
                    lbl_PalabraBuscada.Text = textoBuscar;

                } catch (Exception ex) {MessageBox.Show("Seleccione una api para búsqueda en enciclopedia."); }

                foreach (var elemento in datosObtenidos)
                {
                    rtb_ResultadosWikipedia.AppendText(Environment.NewLine + elemento);
                }
            }
        }

        private void btn_video_Click(object sender, EventArgs e)
        {
            buscar_Video();
        }

        private void buscar_Video()
        {
            btn_leerEnciclopedia.Visible = false;
            var videoBuscar = conceptoBuscar;
            lbl_PalabraBuscada.Visible = true;
            lbl_PalabraBuscada.Text = videoBuscar;

            if (rtb_ResultadosWikipedia.Visible == true)
            {
                rtb_ResultadosWikipedia.Visible = false;
            }
            busquedasRecientes busquedaReciente = new busquedasRecientes();
            var time = DateTime.Now;
            string formatTime = time.ToString("yyyy/MM/dd-HH:mm:ss");
            busquedaReciente.terminoBuscado = videoBuscar;
            string apiUtilizada = "Video";
            busquedaReciente.servicioApi = apiUtilizada;
            busquedaReciente.fechaYhora = formatTime;

            if (string.IsNullOrWhiteSpace(videoBuscar))
            {
                MessageBox.Show("Seleccione un término para buscar en video");
                navegador.Load("www.youtube.com");
            }
            else
            {
                navegador.Visible = true;
                btn_nav_adelante.Visible = true;
                btn_nav_atras.Visible = true;
                btn_cerrarVentanaDerecha.Visible = true;
                BuscarVideo buscarVideoYoutube = new BuscarVideo();  
                try
                {
                    fl_busquedasRecientes.Controls.Add(busquedaReciente);
                    csvFile.AppendLine(formatTime + " ; " + videoBuscar + " ; " + apiUtilizada);

                    var apiSeleccionada = SeleccionApis.apiSeleccionadaVideo;
                    var urlObtenida = buscarVideoYoutube.buscarVideo(videoBuscar, apiSeleccionada);
                    navegador.Load(urlObtenida);
                }
                catch (Exception ex) { MessageBox.Show("Seleccione una api para búsqueda en video."); }
            }

        }

        private void btn_traductor_Click(object sender, EventArgs e)
        {
            traductor();
        }

        private void traductor()
        {
            rtb_result_definicion_traduccion.Visible = true;
            rtb_result_definicion_traduccion.Clear();
            btn_cerrarVentanaIzquierda.Visible = true;
            btn_leerDefinicionTraduccion.Visible = true;

            var textoTraducir = conceptoBuscar;
            var idiomaSeleccionado = SeleccionApis.idiomaSeleccionadoTraduccion;

            busquedasRecientes busquedaReciente = new busquedasRecientes();
            var time = DateTime.Now;
            string formatTime = time.ToString("yyyy/MM/dd-HH:mm:ss");
            busquedaReciente.terminoBuscado = textoTraducir;
            string apiUtilizada = "Traductor";
            busquedaReciente.servicioApi = apiUtilizada;
            busquedaReciente.fechaYhora = formatTime;
           
            if (string.IsNullOrWhiteSpace(textoTraducir))
            {
                rtb_result_definicion_traduccion.Text = "Seleccione un término para traducir";
            }
            else
            {
                btn_cerrarVentanaIzquierda.Visible = true;
                lbl_datoBuscado_trad_def.Visible = true;
                lbl_datoBuscado_trad_def.Text = textoTraducir;
                TraducirTexto traducirTexto = new TraducirTexto();
                try{
                    fl_busquedasRecientes.Controls.Add(busquedaReciente);
                    csvFile.AppendLine(formatTime + " ; " + textoTraducir + " ; " + apiUtilizada);

                    var apiSeleccionada = SeleccionApis.apiSeleccionadaTraduccion;
                    string resultado = traducirTexto.traducirTexto(textoTraducir, idiomaSeleccionado, apiSeleccionada);
                    rtb_result_definicion_traduccion.AppendText(resultado);
                }
                catch (Exception ex) { MessageBox.Show("Seleccione una api para búsqueda en traductor."); }
                
            }
        }

        private void btn_diccionario_Click(object sender, EventArgs e)
        {
            diccionario();
        }

        private void diccionario()
        {
            btn_cerrarVentanaIzquierda.Visible = true;
            lbl_datoBuscado_trad_def.Visible = true;
            rtb_result_definicion_traduccion.Visible = true;
            btn_leerDefinicionTraduccion.Visible = true;

            //rtb_result_definicion_traduccion.SelectionBullet = true;
            rtb_result_definicion_traduccion.Clear();
            var definicionBuscada = conceptoBuscar;

            lbl_datoBuscado_trad_def.Text = definicionBuscada;
     

            busquedasRecientes busquedaReciente = new busquedasRecientes();
            var time = DateTime.Now;
            string formatTime = time.ToString("yyyy/MM/dd-HH:mm:ss");
            busquedaReciente.terminoBuscado = definicionBuscada;
            string apiUtilizada = "Diccionario";
            busquedaReciente.servicioApi = apiUtilizada;
            busquedaReciente.fechaYhora = formatTime;

            if (string.IsNullOrEmpty(definicionBuscada))
            {
                rtb_result_definicion_traduccion.AppendText("Seleccione un término para buscar");
            }
            else {
                try
                {
                    fl_busquedasRecientes.Controls.Add(busquedaReciente);
                    csvFile.AppendLine(formatTime + " ; " + definicionBuscada + " ; " + apiUtilizada);

                    BuscarDefinicion buscarDefinicionG = new BuscarDefinicion();
                    var apiSeleccionada = SeleccionApis.apiSeleccionadaDefinicion;

                    if (apiSeleccionada == null)
                    {
                        MessageBox.Show("Seleccionar Api para busqueda de definición");
                    }
                    else
                    {
                        List<string> definicionesEncontradas = new List<string>();
                        definicionesEncontradas = buscarDefinicionG.buscarDefinicion(definicionBuscada, apiSeleccionada);
                        foreach (var elemento in definicionesEncontradas)
                        {
                            rtb_result_definicion_traduccion.SelectionBullet = true;
                            rtb_result_definicion_traduccion.AppendText(elemento + Environment.NewLine);
                            rtb_result_definicion_traduccion.SelectionBullet = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    rtb_result_definicion_traduccion.AppendText("No se ha podido encontrar la palabra buscada");
                }
            }            
        }

        private void btn_imagen_Click(object sender, EventArgs e)
        {
            buscarPorImagen();
        }

        private void buscarPorImagen() {
            btn_leerEnciclopedia.Visible = false;
            busquedasRecientes busquedaReciente = new busquedasRecientes();
            var time = DateTime.Now;
            string formatTime = time.ToString("yyyy/MM/dd-HH:mm:ss");
            string apiUtilizada = "Imagen";
            busquedaReciente.servicioApi = apiUtilizada;
            busquedaReciente.fechaYhora = formatTime;

            byte[] bytesImagenBuscada = originalForm.byteImagenBuscada;
            //string base64 = @"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";
            //byte[] bytesImagenBuscada = Convert.FromBase64String(base64);

            if (bytesImagenBuscada == null)
            {
                MessageBox.Show("Seleccione una imagen para buscar");
            }
            else {
                navegador.Visible = true;
                btn_cerrarVentanaDerecha.Visible = true;
                btn_nav_adelante.Visible = true;
                btn_nav_atras.Visible = true;
                try
                {
                    fl_busquedasRecientes.Controls.Add(busquedaReciente);
                    List<String> resultadosObtenidosCloudVision = new List<string>();
                    BuscarImagen buscarImagen = new BuscarImagen();
                    var apiSeleccionada = SeleccionApis.apiSeleccionadaImagen;
                    if (apiSeleccionada == null)
                    {
                        MessageBox.Show("Seleccione una api para búsqueda por imagen.");
                    }
                    else
                    {
                        resultadosObtenidosCloudVision = buscarImagen.buscarImagen(bytesImagenBuscada, apiSeleccionada);
                        var primerResultado = resultadosObtenidosCloudVision[0];
                        lbl_PalabraBuscada.Visible = true;
                        lbl_PalabraBuscada.Text = primerResultado;
                        busquedaReciente.terminoBuscado = primerResultado;
                        navegador.Load("https://www.ecosia.org/search?q=" + primerResultado);

                        csvFile.AppendLine(formatTime + " ; " + primerResultado + " ; " + apiUtilizada);
                    }

                }
                catch (Exception e) { System.Diagnostics.Debug.WriteLine("No se seleccionó ninguna imagen"); }
            }
        }

        private void btn_cerrarVentanaDerecha_Click(object sender, EventArgs e)
        {
            cerrar_enciclopedia();
            cerrar_video();
            cerrar_imagen();
        }

        private void btn_cerrarVentanaIzquierda_Click(object sender, EventArgs e)
        {
            cerrar_definicion();
            cerrar_traduccion();
        }

        private void btn_leerDefinicionTraduccion_Click(object sender, EventArgs e)
        {
            leerVozAlta(rtb_result_definicion_traduccion.Text);
        }

        private void btn_leerEnciclopedia_Click(object sender, EventArgs e)
        {
            leerVozAlta(rtb_ResultadosWikipedia.Text);
        }

        private void cerrar_enciclopedia()
        {
            rtb_ResultadosWikipedia.Visible = false;
            rtb_ResultadosWikipedia.Clear();
            lbl_PalabraBuscada.Visible = false;
            btn_cerrarVentanaDerecha.Visible = false;
            btn_leerEnciclopedia.Visible = false;
            ss.SpeakAsyncCancelAll();
        }

        private void cerrar_definicion()
        {
            rtb_result_definicion_traduccion.Visible = false;
            rtb_result_definicion_traduccion.Clear();
            rtb_result_definicion_traduccion.SelectionBullet = false;
            lbl_datoBuscado_trad_def.Visible = false;
            btn_cerrarVentanaIzquierda.Visible = false;
            btn_leerDefinicionTraduccion.Visible = false;
            ss.SpeakAsyncCancelAll();
        }

        private void cerrar_traduccion()
        {
            rtb_result_definicion_traduccion.Visible = false;
            rtb_result_definicion_traduccion.Clear();
            rtb_result_definicion_traduccion.SelectionBullet = false;
            lbl_datoBuscado_trad_def.Visible = false;
            btn_cerrarVentanaIzquierda.Visible = false;
            btn_leerDefinicionTraduccion.Visible = false;
            ss.SpeakAsyncCancelAll();
        }

        private void cerrar_video()
        {
            navegador.Visible = false;
            lbl_PalabraBuscada.Visible = false;
            btn_nav_adelante.Visible = false;
            btn_nav_atras.Visible = false;
            btn_cerrarVentanaDerecha.Visible = false;
            navegador.Load("www.youtube.com");
        }

        private void cerrar_imagen()
        {
            navegador.Visible = false;
            btn_nav_adelante.Visible = false;
            btn_nav_atras.Visible = false;
            btn_cerrarVentanaDerecha.Visible = false;
            lbl_PalabraBuscada.Visible = false;
        }

        private void btn_nav_atras_Click(object sender, EventArgs e)
        {
            if (navegador.CanGoBack)
                navegador.Back();
        }

        private void btn_nav_adelante_Click(object sender, EventArgs e)
        {
            if (navegador.CanGoForward)
                navegador.Forward();
        }

        private void ProjectionScreenActivity_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.AppendAllText(csvpath, csvFile.ToString());
        }

        private void btn_drawMenu_Click(object sender, EventArgs e)
        {
            timer1.Start();
            panel_log.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {

                panel_log.Width = panel_log.Width + 10;
                if (panel_log.Width >= panelWidth)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panel_log.Width = panel_log.Width - 10;
                if (panel_log.Width <= 0)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void leerVozAlta(string textoLeerVozAlta) {
            ss.SpeakAsync(textoLeerVozAlta);
        }

        // Código para la interacción a través de comandos de voz //

        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        GrammarBuilder testBuilder = new GrammarBuilder();

        private void iniciarVR()
        {
            Choices commandChoices = new Choices();

            commandChoices.Add(new string[] {
                "Video",
                "Enciclopedia",
                "Traducción",
                "Definición",
                "Imagen" });

            GrammarBuilder builderBusqueda = new GrammarBuilder("Buscar");
            GrammarBuilder builderLectura = new GrammarBuilder("Lectura");
            GrammarBuilder builderCerrar = new GrammarBuilder("Cerrar");
            builderBusqueda.Append(commandChoices);
            builderLectura.Append(commandChoices);
            builderCerrar.Append(commandChoices);

            Grammar commandGrammarBusqueda = new Grammar(builderBusqueda);
            Grammar commandGrammarLectura = new Grammar(builderLectura);
            Grammar commandGrammarCerrar = new Grammar(builderCerrar);

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammarAsync(commandGrammarBusqueda);
                sre.LoadGrammarAsync(commandGrammarLectura);
                sre.LoadGrammarAsync(commandGrammarCerrar);
                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string resultado = e.Result.Text;
            if (resultado.Equals("Cerrar Enciclopedia"))
            {
                cerrar_enciclopedia();
            }
            else if (resultado.Equals("Cerrar Video"))
            {
                cerrar_video();
            }
            else if (resultado.Equals("Cerrar Definición"))
            {
                cerrar_definicion();
            }
            else if (resultado.Equals("Cerrar Traducción"))
            {
                cerrar_traduccion();
            }
            else if (resultado.Equals("Cerrar Imagen"))
            {
                cerrar_imagen();
            }
            else if (resultado.Equals("Lectura Definición"))
            {
                leerVozAlta(rtb_result_definicion_traduccion.Text);
            }
            else if (resultado.Equals("Lectura Traducción"))
            {
                leerVozAlta(rtb_result_definicion_traduccion.Text);
            }
            else if (resultado.Equals("Lectura Enciclopedia"))
            {
                leerVozAlta(rtb_ResultadosWikipedia.Text);
            }
            else if (resultado.Equals("Buscar Traducción"))
            {
                traductor();
            }
            else if (resultado.Equals("Buscar Definición"))
            {
                diccionario();
            }
            else if (resultado.Equals("Buscar Video"))
            {
                buscar_Video();
            }
            else if (resultado.Equals("Buscar Enciclopedia"))
            {
                buscar_Enciclopedia();
            }
            else if (resultado.Equals("Buscar Imagen"))
            {
                buscarPorImagen();
            }
        }
        // Fin del código para la interacción a través de comandos de voz //


        //Sincronizar figuras desde físico a digital
        private void buttonFiguresPD_Click(object sender, EventArgs e)
        {
            string pdfName = originalForm.pdfName;
            numeroCamara = originalForm.numeroCamara;
            int page = Int32.Parse(searchTextBox.Text);
            VideoCapture auxCapture = originalForm._capture;
            Mat captureImage = new Mat();

            auxCapture.Read(captureImage);
            Image<Bgr, byte> imagen_aux = captureImage.ToImage<Bgr, byte>();
            //pictureBox1.Image = imagen_aux.Bitmap;
            //imageBox1.Image = imagen_aux;

            Console.WriteLine("Entro en figuras desde fisico " + numeroCamara);
           
            // Inicia el contador de tiempo figuras DP:
            DateTime timeFiguresPD1 = DateTime.Now;
            ConsistenciaFigurasFD buscarFiguras = new ConsistenciaFigurasFD();
            List<string> figurasEncontradas = new List<string>();
            //JObject resultado;

            if (pdfName != null)
            {

                int resultado = buscarFiguras.buscarFiguras(captureImage, originalForm.pdfName, page);
                // Para el contador e imprime el resultado:
                DateTime timeFiguresPD2 = DateTime.Now;
                Console.WriteLine("este es el resultado de Sinc Figuras: " + resultado);
                if (resultado == 1)
                {

                    TimeSpan timeFiguresPD = new TimeSpan(timeFiguresPD2.Ticks - timeFiguresPD1.Ticks);
                    Console.WriteLine("TIEMPO Figures PD: " + timeFiguresPD.ToString());                  
                    MessageBox.Show("Figuras sincronizadas");
                }
                else
                {
                    MessageBox.Show("No se encontraron figuras");
                }
                
            }
            else
            {
                MessageBox.Show("No se ha ingresado archivo PDF");
            }
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            checkBox1.Checked = true;
            int page = Int32.Parse(searchTextBox.Text);
            Console.WriteLine("Entro en sincronizar PDF  ");
            string pdfName = originalForm.pdfName;
            Mat imagen = new Mat();
            FiguresConsistencyDP buscarDP = new FiguresConsistencyDP();
            PageMarkerDP getMarkerDP = new PageMarkerDP();
            ConsistencyCommentsDP getCommentsDP = new ConsistencyCommentsDP();
            JObject resultado;
            if (pdfName != null)
            {


                // Inicia el contador:
                Stopwatch tiempo = Stopwatch.StartNew();
                // Inicia el contador de tiempo figuras DP:
                DateTime timeFiguresDP1 = DateTime.Now;
                resultado = buscarDP.SincronizarFiguras(originalForm.pdfName, page);
                // Para el contador e imprime el resultado:
                Console.WriteLine("TIEMPO 2: " + tiempo.ElapsedMilliseconds.ToString());
                // Para el contador e imprime el resultado:
                DateTime timeFiguresDP2 = DateTime.Now;
                TimeSpan timeFiguresDP = new TimeSpan(timeFiguresDP2.Ticks - timeFiguresDP1.Ticks);
                Console.WriteLine("TIEMPO Figures DP: " + timeFiguresDP.ToString());
                //TimesFromFiguresDP.Add(timeFiguresDP.ToString());

            
                int k = 0;


                FiguresDP = new FiguresDP(resultado)
                {
                    Name = "FiguresDP",
                    BackColor = Color.FromArgb(255, 255, 255),
                    Location = new Point(285, 65),
                };


                this.Controls.Add(FiguresDP);
                FiguresDP.BringToFront();

                // Inicia el contador de tiempo Marker DP:
                DateTime timeMarkerDP1 = DateTime.Now;
                List<string> marker = getMarkerDP.GetPageMarker(originalForm.pdfName);
                // Para el contador e imprime el resultado:
                DateTime timeMarkerDP2 = DateTime.Now;
                TimeSpan timeMarkerDP = new TimeSpan(timeMarkerDP2.Ticks - timeMarkerDP1.Ticks);
                Console.WriteLine("TIEMPO MARKER DP: " + timeMarkerDP.ToString());
                //TimesFromMarkerDP.Add(timeMarkerDP.ToString());
                

                // Inicia el contador de tiempo Marker DP:
                DateTime timeCommentsDP1 = DateTime.Now;
                List<List<string>> comments = getCommentsDP.GetComments(originalForm.pdfName);
                // Para el contador e imprime el resultado:
                DateTime timeCommentsDP2 = DateTime.Now;
                TimeSpan timeCommentsDP = new TimeSpan(timeCommentsDP2.Ticks - timeCommentsDP1.Ticks);
                Console.WriteLine("TIEMPO Comments DP: " + timeCommentsDP.ToString());
                //TimesFromCommentsDP.Add(timeCommentsDP.ToString());
               

                foreach (var item in marker)
                {

                    Console.WriteLine("Marcadores  " + item.ToString());
                    listBox1.Items.Add(item);
                }
                foreach (var item in comments)
                {

                    Console.WriteLine("Comentarios  " + item.ToString());
                    listBox2.Items.Add(item[0] + " Pag." + item[1]);
                }
            }
            else
            {
                MessageBox.Show("No se ha ingresado archivo PDF");
            }

            if (listBox1.Items.Count > 0)
            {

                buttonEliminarMarcador.Enabled = true;

            }
            if (listBox2.Items.Count > 0)
            {
                buttonEliminarComentario.Enabled = true;

            }
        }

        private void buttonEliminarMarcador_Click(object sender, EventArgs e)
        {
            string pdfName = originalForm.pdfName;
            //string curItem = listBox1.SelectedItem.ToString();
            if (listBox1.SelectedIndex > -1)
            {
                string curItem = listBox1.SelectedItem.ToString();
                MessageBox.Show("Item " + curItem);
                PageMarkerDP deleteMarkerDP = new PageMarkerDP();
                int res = deleteMarkerDP.DeleteMarker(curItem, pdfName);
                if (res == 1)
                {
                    MessageBox.Show("Marcador eliminado");
                    listBox1.Items.Remove(curItem);
                }
                else
                {
                    MessageBox.Show("No se pudo completar la operación");
                }
            }
            else
            {
                MessageBox.Show("No esta seleccionado");
            }

        }

        private void buttonEliminarComentario_Click(object sender, EventArgs e)
        {
            //string curItem = listBox2.SelectedItem.ToString();
            if (listBox2.SelectedIndex > -1)
            {
                string curItem = listBox2.SelectedItem.ToString();

                //string text = listBox2.GetItemText(listBox2.SelectedItem);
                MessageBox.Show("Comentario " + curItem + " eliminado");
                listBox2.Items.Remove(curItem);
            }
            else
            {
                MessageBox.Show("No esta seleccionado");
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Cambio");
            if (FiguresDP == null)
            {
                MessageBox.Show("FiguresDP is null.\n" +
                                "Check ProjectionScreenActivity.cs line 1014 for debugging");
            }
            else
            {
                if (FiguresDP.Visible == true)
                {
                    FiguresDP.Visible = false;
                    Color redColor = Color.FromArgb(255, 0, 0);
                    checkBox1.ForeColor = redColor;
                }
                else
                {
                    FiguresDP.Visible = true;
                    Color greenColor = Color.FromArgb(0, 192, 0);
                    checkBox1.ForeColor = greenColor;

                }
            }
        }


        //Sincronizar comentarios desde fisico a digital
        private void buttonComments_Click(object sender, EventArgs e)
        {
            string pdfName = originalForm.pdfName;
            numeroCamara = originalForm.numeroCamara;
            try
            {
                int page = Int32.Parse(searchTextBox.Text);

                VideoCapture auxCapture = originalForm._capture;
                Mat captureImage = new Mat();
                auxCapture.Read(captureImage);
                Image<Bgr, byte> imagen_aux = captureImage.ToImage<Bgr, byte>();
                ///pictureBox1.Image = imagen_aux.Bitmap;
                //imageBox1.Image = imagen_aux;

                ConsistencyCommentsPD getCommentsPD = new ConsistencyCommentsPD();
                Console.WriteLine("Entro en comentarios PD " + numeroCamara);


                if (pdfName != null)
                {
                    // Inicia el contador de tiempo figuras DP:
                    DateTime timeCommentsPD1 = DateTime.Now;
                    List<List<string>> comments = getCommentsPD.GetComments(originalForm.pdfName, captureImage, page);
                    // Para el contador e imprime el resultado:
                    DateTime timeCommentsPD2 = DateTime.Now;
                    TimeSpan timeCommentsPD = new TimeSpan(timeCommentsPD2.Ticks - timeCommentsPD1.Ticks);
                    Console.WriteLine("TIEMPO Comments PD: " + timeCommentsPD.ToString());



                    foreach (var item in comments)
                    {
                        foreach (var item2 in item)
                        {
                            Console.WriteLine("Respondio de biblio " + item2);
                            if (item2 == "SI")
                            {
                                MessageBox.Show("Se sincronizó el comentario");
                            }
                            else
                            {
                                MessageBox.Show("No se encontró comentario para sincronizar");
                            }
                        }

                    }

                }
                else
                {
                    MessageBox.Show("No se ha ingresado archivo PDF");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Consistencia marcadores fisico a digital
        private void buttonMarker_Click(object sender, EventArgs e)
        {
            string pdfName = originalForm.pdfName;
            numeroCamara = originalForm.numeroCamara;
            int page = Int32.Parse(searchTextBox.Text);
            VideoCapture auxCapture = originalForm._capture;
            Mat captureImage = new Mat();
            auxCapture.Read(captureImage);
            Image<Bgr, byte> imagen_aux = captureImage.ToImage<Bgr, byte>();
            //pictureBox1.Image = imagen_aux.Bitmap;
            //imageBox1.Image = imagen_aux;

            PageMarkerPD getMarkerPD = new PageMarkerPD();
           
            if (pdfName != null)
            {
                // Inicia el contador de tiempo Marker PD:
                DateTime timeMarkerPD1 = DateTime.Now;
                int markerRes = getMarkerPD.GetPageMarker(captureImage, originalForm.pdfName, page);
                // Para el contador e imprime el resultado:
                DateTime timeMarkerPD2 = DateTime.Now;


                if (markerRes == 0)
                {

                    MessageBox.Show("No se encontró marcador");

                }
                else if (markerRes == 10)
                {
                    MessageBox.Show("Se sincronizó correctamente");
                    TimeSpan timeMarkerPD = new TimeSpan(timeMarkerPD2.Ticks - timeMarkerPD1.Ticks);
                    Console.WriteLine("TIEMPO MARKER PD: " + timeMarkerPD.ToString());
                    //TimesFromMarkerDP.Add(timeMarkerDP.ToString());
                    /*ESCRIBIR EN ARCHIVO*/
                    StreamWriter sw = new StreamWriter("C:\\Users\\Denisse\\Desktop\\EDDIE-Augmented-Reading-master\\AugmentedReadingApp\\bin\\x86\\Debug\\TiempoMarcadorFisico.txt", true);
                    //Write a line of text
                    sw.WriteLine("MarkerPD;" + timeMarkerPD.ToString());
                    sw.Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione PDF");
            }
        }
    }
}

