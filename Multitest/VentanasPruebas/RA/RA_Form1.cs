using FTD2XX_NET;
using Multitest.FormAux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;


namespace Multitest
{
    public partial class RA_Form1 : Form
    {
        double ftdiTimeCorrection = 0.8187;
        double arduinoTimeCorrection = 0.8831;
        int anchomm = 0;
        int alturamm = 0;
        int controlTimer3 = 0;
        String diferenciaTiempo = "";
        bool breakLopp = false;
        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice;
        string varianteSentido = "";

        String tiempoTarjetaFTDI = "";

        int[] P1 = new int[] { 6, 6, 2, 2, 6, 6, 2, 6, 2, 2, 6, 2, 6, 2, 2, 6, 2, 6, 6, 2 };
        int[] P2 = new int[] { 2, 1, 2, 6, 5, 6, 1, 6, 2, 6, 5, 2, 6, 2, 2, 6, 2, 6, 6, 2 };
        int[] P3 = new int[] { 5, 6, 2, 1, 6, 6, 1, 5, 1, 2, 5, 2, 6, 1, 1, 5, 2, 6, 5, 2 };
        int[] P4 = new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        int[] P5 = new int[] { 2, 1, 4, 3, 2, 2, 3, 1, 3, 4, 1, 4, 2, 3, 3, 1, 4, 2, 1, 4 };
        int[] P6 = new int[] { 5, 8, 7, 8, 6, 5, 7, 6, 8, 7, 5, 7, 7, 6, 6, 5, 8, 8, 6, 5 };
        int[] P7 = new int[] { 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4 };
        int[] P8 = new int[] { 5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6, 5, 6 };
        int[] P9 = new int[] { 8, 4, 5, 7, 2, 3, 3, 6, 5, 1, 1, 8, 6, 2, 5, 4, 7, 2, 3, 8 };

        List<Auxiliar> tiempos = new List<Auxiliar>();
        List<Auxiliar> tiemposCambioVelocidadSen = new List<Auxiliar>();
        int entrenamiento = 0;
        bool pintarCircunferencia = false;
        int[] variante = null;
        String varianteNombre = null;
        int countArduMessage = 0;

        bool salidaCirculo = false;
        Stopwatch tiempo = new Stopwatch();

        Screen screen;
        String velocidadAtual;
        bool finCliclo = false;
        int sentido = 0;

        int bordeIzquierdo;
        int bordeDerecho;

        int largoRect1;
        int anchoRect1;
        int posXRect1;
        int posYRect1;


        int largoRect2;
        int anchoRect2;
        int posXRect2;
        int posYRect2;


        int largoRect3;
        int anchoRect3;
        int posXRect3;
        int posYRect3;


        int largoRect4;
        int anchoRect4;
        int posXRect4;
        int posYRect4;


        int largoCir;
        int anchoCir;
        int posXCir;
        int posYCir;


        int width4 = 0;
        int HeightScreen;
        int WidthScreen;
        int tamañoTunel = 0;
        int xMove = 50;

        int count = 0;
        bool res = false;
        String inicio;
        String fin;

        double velocidad1 = 0;
        double velocidad4 = 0;
        double velocidad3 = 0;
        double velocidad2 = 0;

        int contador1 = 0;
        int contador2 = 0;
        int contador3 = 0;
        int contador4 = 0;
        Stopwatch time;



        double tiempoMedido = 0; // Tiempo de la bola visible + tiempo del contador 
        String idPerson;
        String idEtapa;
        String nombreAtleta;
        bool quitarCircunferencia = false;
        // double resultadoFinal = tiempomedido - tiempoNominal;
        // si el tiempoMedio es negativo esta adelantado si es positivo esta atrasado y si es 0 esta bien

      
        SolidBrush brushCircle = new SolidBrush(Color.DarkRed);
        Pen red = new Pen(Color.DarkRed);
        Rectangle circle;

     

        public RA_Form1(String idPerson, String idEtapa, String nombreAtleta)
        {


            InitializeComponent();

            myFtdiDevice = new FTDI();

            configurarPuertoTarjetas();



            time = new Stopwatch();
            screen = Screen.PrimaryScreen;
            HeightScreen = screen.Bounds.Height;
            WidthScreen = screen.WorkingArea.Width;

            calcularValoresPintar();
            this.idPerson = idPerson;
            this.idEtapa = idEtapa;
            this.nombreAtleta = nombreAtleta;

            screen = Screen.PrimaryScreen;

            HeightScreen = screen.Bounds.Height;
            WidthScreen = screen.WorkingArea.Width;



            anchomm = Convert.ToInt32(WidthScreen * 0.264);
            alturamm = Convert.ToInt32(HeightScreen * 0.264);


            Rectangle resolution = Screen.PrimaryScreen.Bounds;

            this.SetStyle(
               System.Windows.Forms.ControlStyles.UserPaint |
               System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
               System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
               true);



            backgroundWorker1.DoWork += recibirDatosFTDI;
            //backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += cancelRecibirDatos;  //Tell the user how the process went
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

        }

        private void cancelRecibirDatos(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled != null)
            {
                //MessageBox.Show("cancelo");
            }
        }

        private void recibirDatosFTDI(object sender, DoWorkEventArgs e)
        {
            String temp = readFromFTDI1Byte(Convert.ToUInt32(2));

            if (!breakLopp)
            {

                if (temp != "m")
                {
                    if (controlTimer3 == 1)
                    {

                        timer3.Stop();


                        String resultado = calcularAdelantado(Convert.ToInt32(tiempoTarjetaFTDI), velocidadAtual);

                        int x4 = Convert.ToInt32(Convert.ToDouble(diferenciaTiempo) * ftdiTimeCorrection);
                        int x5 = Convert.ToInt32(Convert.ToDouble(tiempoTarjetaFTDI) * ftdiTimeCorrection);

                        String resultadoCorreccion = Convert.ToString(x4);
                        String tiempoString = Convert.ToString(x5);

                        Auxiliar aux = new Auxiliar(tiempoString, velocidadAtual, resultado, resultadoCorreccion, varianteSentido);
                        tiempos.Add(aux);
                        timer1.Stop();
                        quitarCircunferencia = true;
                        salidaCirculo = true;


                        backgroundWorker1.CancelAsync();
                        Invalidate();


                    }

                }
                else
                {

                    backgroundWorker1.CancelAsync();
                    Invalidate();


                }
            }
        }

        private void configurarPuertoTarjetas()
        {

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {

                openPorFTDI();

            }
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
            {
                configurarPuerto();

            }

        }

        private void openPorFTDI()
        {
            if (!myFtdiDevice.IsOpen)
            {


                UInt32 ftdiDeviceCount = 0;

                ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

                ///lista de dispositivos ftdi
                FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

                //asigna la lista de dispositivos
                ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

                if (ftdiDeviceList.Length != 0)
                {

                    ftStatus = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);


                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        ftStatus = myFtdiDevice.SetBaudRate(9600);
                        ftStatus = myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8,
                            FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE);
                        // port = true;
                    }
                    //else
                    //    conection = 2;
                    // MessageBox.Show("Ocurrio un error al abrir el puerto de comunicacion.Reinicie la aplicacion");
                }
                //else
                //    // MessageBox.Show("Verifique si el dispositivo esta conectado");
                //    conection = 1;
            }

            if (myFtdiDevice.IsOpen)
            {
                //int y = 0;
            }

            //return port;
        }

        private void configurarPuerto()
        {

            try
            {
                arduinoPort = new SerialPort();
                string[] puertosDisponibles = SerialPort.GetPortNames();
                if (puertosDisponibles.Length > 0 && verificarArduino())
                {

                    string port = puertosDisponibles[puertosDisponibles.Length - 1];
                    arduinoPort.PortName = port;
                    arduinoPort.BaudRate = 9600;
                    arduinoPort.Parity = Parity.None;
                    arduinoPort.DataBits = 8;
                    arduinoPort.Handshake = Handshake.None;
                    arduinoPort.StopBits = StopBits.One;
                    arduinoPort.RtsEnable = true;
                    arduinoPort.DataReceived += new SerialDataReceivedEventHandler(recibirArduino);
                    arduinoPort.Open();

                }
                else
                {
                    MessageBox.Show("Conecte el equipo a la PC para poder realizar la prueba.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void recibirArduino(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string entradaDatos = sp.ReadExisting();

                if (entradaDatos != "TRAA" && entradaDatos != "FIN TRAA" && entradaDatos != "Liberado" && entradaDatos != "Oprimido")
                {
                    if (controlTimer3 == 1)
                    {
                        time.Stop();
                        timer3.Stop();
                        string s = time.ElapsedMilliseconds.ToString();
                        time.Reset();


                        String resultado = calcularAdelantado(Convert.ToInt32(s), velocidadAtual);

                        int x4 = Convert.ToInt32(Convert.ToDouble(diferenciaTiempo) * arduinoTimeCorrection);
                        int x5 = Convert.ToInt32(Convert.ToDouble(tiempoTarjetaFTDI) * arduinoTimeCorrection);

                        String resultadoCorreccion = Convert.ToString(x4);
                        String tiempoString = Convert.ToString(x5);

                        Auxiliar aux = new Auxiliar(tiempoString, velocidadAtual, resultado, resultadoCorreccion, varianteSentido);

                        tiempos.Add(aux);
                        timer1.Stop();
                        quitarCircunferencia = true;
                        salidaCirculo = true;

                        Invalidate();
                    }

                }
                else
                {
                    if (entradaDatos == "TRAA")
                    {
                        countArduMessage = 1;
                        comenzarPrueba();

                    }

                    if (entradaDatos == "Liberado")
                    {
                        time.Stop();
                        timer3.Stop();
                        timer1.Stop();
                        timer2.Stop();
                        timer4.Stop();
                        this.Invoke(new MethodInvoker(() => Cursor.Show()));
                        MessageBox.Show("Pulsador suelto, prueba cancelada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        arduinoPort.Write("9");

                        this.Invoke(new MethodInvoker(() => this.Close()));
                    }

                    if (entradaDatos == "FIN TRAA")
                    {
                        this.Invoke(new MethodInvoker(() => Cursor.Show()));

                        timer2.Stop();
                        timer1.Stop();
                        timer3.Stop();
                        timer4.Stop();
                        time.Stop();


                        this.BackColor = Color.Black;
                        pintarCircunferencia = false;
                        if (entrenamiento == 0)
                        {

                            Application.DoEvents();

                            Esperar d = new Esperar();
                            d.Show();

                            RA_Form3 form = new RA_Form3(tiempos, varianteNombre, idPerson, idEtapa, nombreAtleta, varianteNombre);
                            d.Close();
                            form.ShowDialog();

                            this.Invoke(new MethodInvoker(() => this.Close()));
                        }
                    }

                }

            }
            catch (Exception)
            {
                //String r = ec.Message;

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private string calcularAdelantado(int tiempo, String velocidadAtual)
        {
            String result = "";
            int temp = 0;


            if (velocidadAtual == "1")
            {
                temp = 1500 - tiempo;
            }
            if (velocidadAtual == "2")
            {
                temp = 2000 - tiempo;
            }
            if (velocidadAtual == "3")
            {
                temp = 2500 - tiempo;
            }
            if (velocidadAtual == "4")
            {
                temp = 3000 - tiempo;
            }



            if (temp > 0)
            {
                result = "Adelantado";

            }
            if (temp < 0)
            {

                result = "Atrasado";
            }

            if (temp == 0)
            {
                result = "Exacto";
            }

            if (temp < 0)
                temp = temp * -1;

            diferenciaTiempo = temp.ToString();

            return result;
        }

        private bool verificarArduino()
        {
            bool res = false;

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2",
            @"SELECT * FROM Win32_PnPEntity where DeviceID Like ""USB%"""))
                collection = searcher.Get();

            String arduinoID = "USB\\VID_1A86&PID_7523";
            String arduinoID2 = "USB\\VID_2341&PID_0043";



            foreach (var device in collection)
            {
                String temp = device.GetPropertyValue("DeviceID").ToString().Substring(0, 21);
                if (temp == arduinoID)
                    res = true;
                
                else if (temp == arduinoID2)
                {
                    res = true;

                }
            }
            return res;
        }

        private void calcularValoresPintar()
        {
            bordeIzquierdo = Convert.ToInt32((11.54 + 1.53) * WidthScreen / 100);
            bordeDerecho = bordeIzquierdo;

            largoRect1 = Convert.ToInt32((40 * WidthScreen) / 100);
            anchoRect1 = Convert.ToInt32((4.081 * HeightScreen) / 100);
            posXRect1 = bordeIzquierdo;
            posYRect1 = Convert.ToInt32(((34.7 + 3.26) * HeightScreen) / 100);



            largoRect2 = Convert.ToInt32((3.07 * WidthScreen) / 100);
            anchoRect2 = Convert.ToInt32((4.081 * HeightScreen) / 100);
            posXRect2 = Convert.ToInt32((11.54 + 1.53 + 40 + 30.7 - 3.07) * WidthScreen / 100);
            posYRect2 = posYRect1;



            largoRect3 = Convert.ToInt32((3.07 * WidthScreen) / 100);
            anchoRect3 = Convert.ToInt32((4.081 * HeightScreen) / 100);
            posXRect3 = Convert.ToInt32((13.07 * WidthScreen) / 100);
            posYRect3 = Convert.ToInt32(((61.229) * HeightScreen) / 100);



            largoRect4 = Convert.ToInt32((40 * WidthScreen) / 100);
            anchoRect4 = Convert.ToInt32((4.081 * HeightScreen) / 100);
            posXRect4 = Convert.ToInt32(((39.689 + 4.081) * WidthScreen) / 100);
            posYRect4 = Convert.ToInt32(((61.229) * HeightScreen) / 100);

            largoCir = Convert.ToInt32((3.07 * WidthScreen) / 100);
            anchoCir = Convert.ToInt32((4.081 * HeightScreen) / 100);
            posXCir = bordeIzquierdo;
            posYCir = posYRect1;

            velocidad1 = (0.4 * WidthScreen) / 1500;
            velocidad2 = (0.4 * WidthScreen) / 2000;
            velocidad3 = (0.4 * WidthScreen) / 2500;
            velocidad4 = (0.4 * WidthScreen) / 3000;

            contador1 = Convert.ToInt32(Math.Round(4 / velocidad1));
            contador2 = Convert.ToInt32(Math.Round(4 / velocidad2));
            contador3 = Convert.ToInt32(Math.Round(4 / velocidad3));
            contador4 = Convert.ToInt32(Math.Round(4 / velocidad4));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            ///+++++++++++++++++++++++++++++++++++++++++++++++++/////
            //Rectangulo 1

            SolidBrush blueBrush4 = new SolidBrush(Color.Black);
            Rectangle resolution4 = Screen.PrimaryScreen.Bounds;
            // Create location and size of rectangle.

            e.Graphics.FillRectangle(blueBrush4, posXRect1, posYRect1, largoRect1, anchoRect1);

            ///+++++++++++++++++++++++++++++++++++++++++++++++++/////
            //Rectangulo 2

            SolidBrush blueBrush5 = new SolidBrush(Color.Black);
            Rectangle resolution5 = Screen.PrimaryScreen.Bounds;
            // Create location and size of rectangle.

            e.Graphics.FillRectangle(blueBrush5, posXRect2, posYRect2, largoRect2, anchoRect2);

            ///////Pintar circulo/////////////////////////////////////////////////////
            ///

            SolidBrush blueBrush6 = new SolidBrush(Color.Black);
            Rectangle resolution6 = Screen.PrimaryScreen.Bounds;
            // Create location and size of rectangle.

            e.Graphics.FillRectangle(blueBrush6, posXRect3, posYRect3, largoRect3, anchoRect3);


            ///////Pintar circulo/////////////////////////////////////////////////////


            SolidBrush blueBrush7 = new SolidBrush(Color.Black);
            Rectangle resolution7 = Screen.PrimaryScreen.Bounds;
            // Create location and size of rectangle.

            e.Graphics.FillRectangle(blueBrush7, posXRect4, posYRect4, largoRect4, anchoRect4);

            ///////Pintar circulo/////////////////////////////////////////////////////

            if (pintarCircunferencia)
            {
                if (!salidaCirculo)
                {
                    if (!quitarCircunferencia)
                    {
                        circle = new Rectangle(posXCir, posYCir, largoCir, anchoCir);
                        Graphics graph = e.Graphics;
                        graph.FillEllipse(brushCircle, circle);
                    }
                    else
                    {

                        circle = new Rectangle(posXCir, posYCir, 0, 0);
                        Graphics graph = e.Graphics;
                        graph.FillEllipse(brushCircle, circle);


                        if (controlTimer3 == 0)
                        {
                            time.Start();
                            timer3.Start();
                            controlTimer3 = 1;
                        }

                        if (!finCliclo)
                        {
                            salidaCirculo = true;

                        }


                    }
                }
                else
                {
                    if (sentido == 1)
                    {
                        circle = new Rectangle(posXRect2, posYRect1, largoCir, anchoCir);
                        Graphics graph = e.Graphics;
                        graph.FillEllipse(brushCircle, circle);
                    }

                    if (sentido == 2)
                    {
                        circle = new Rectangle(bordeIzquierdo, posYRect3, largoCir, anchoCir);
                        Graphics graph = e.Graphics;
                        graph.FillEllipse(brushCircle, circle);

                    }

                    timer4.Start();
                }

            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            inicioTest();

        }

        private void inicioTest()
        {

            if (entrenamiento != 0)
            {
                pintarCircunferencia = false;

                salidaCirculo = false;
                finCliclo = false;


            }

            RA_Form2 f = new RA_Form2(entrenamiento);
            DialogResult res = f.ShowDialog();
           
            
            sendCommandFTDI("9");
            TimeSpan.FromMilliseconds(500);
            string fin = readFromFTDI1ByteInicial(2);

            if (fin == "f" || fin == "x")
            {
                myFtdiDevice.Purge(0);

            }
           

            




            if (f.resultado)
            {
                MessageBox.Show("Debe mantener el pulsador de color VERDE apretado y responder liberandolo para cada estímulo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(new MethodInvoker(() => Cursor.Hide()));

                //pinta el fondo de verde
                this.BackColor = Color.FromArgb(130, 115, 76);
                count = 0;
                entrenamiento = f.entrenamiento;

                if (f.variante == "P1")
                {
                    variante = P1;
                    varianteNombre = "1";
                }
                if (f.variante == "P2")
                {
                    variante = P2;
                    varianteNombre = "2";
                }
                if (f.variante == "P3")
                {
                    variante = P3;
                    varianteNombre = "3";
                }
                if (f.variante == "P4")
                {
                    variante = P4;
                    varianteNombre = "4";
                }
                if (f.variante == "P5")
                {
                    variante = P5;
                    varianteNombre = "5";
                }
                if (f.variante == "P6")
                {
                    variante = P6;
                    varianteNombre = "6";
                }
                if (f.variante == "P7")
                {
                    variante = P7;
                    varianteNombre = "7";
                }
                if (f.variante == "P8")
                {
                    variante = P8;
                    varianteNombre = "8";
                }
                if (f.variante == "P9")
                {
                    variante = P9;
                    varianteNombre = "9";
                }


                comenzarPrueba();
            }
            else
                this.Close();

        }

        private void comenzarPrueba()
        {

            if (countArduMessage == 0)
            {
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    if (fin == "x")
                        fin = readFromFTDI1ByteInicial(2);
                    sendCommandFTDI("5");

                    string prueba = readFirstDataFromFTDI1Byte();

                    if (prueba == "5")
                    {
                        countArduMessage = 1;
                    }
                    else
                        MessageBox.Show("El sofware no entro a la prueba");

                    
                }

                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                {
                    
                        arduinoPort.Write("5");

                }
            }


            if (countArduMessage == 1)
            {
                if (entrenamiento == 0)
                    this.Invoke(new MethodInvoker(() => timer2.Start()));
                else
                    this.Invoke(new MethodInvoker(() => timerEntrenamiento.Start()));
            }
             




        }

        private string readFirstDataFromFTDI1Byte()
        {
            string entradaDatos = "";
            string entradadoble = "";
            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = 2;
            myFtdiDevice.Purge(0);
            string temp = "";


            while (numBytesAvailable < numBytesExpected)
            {

                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);


                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    if (myFtdiDevice.IsOpen)
                    {

                    }
                    throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);
                }

            }


            if (numBytesAvailable > 2)
            {
                entradadoble = readFromFTDI1ByteInicial(2);
            }

           

            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


            if (rawData[0].ToString() != "5")
            {
                throw new Exception("Error: Invalid data in buffer. (1350)");
            }


            return rawData[0].ToString();
        }

        private string readFromFTDI1Byte(UInt32 cantByte)
        {
            string entradaDatos = "";


            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = cantByte;
            myFtdiDevice.Purge(0);


            while (numBytesAvailable < numBytesExpected && !breakLopp)
            {

                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                if (ftStatus != FTDI.FT_STATUS.FT_OK)

                    throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);

            }

            if (!breakLopp)
            {
                if (numBytesAvailable != numBytesExpected)
                    throw new Exception("Error: Invalid data in buffer. (1350)");


                tiempoTarjetaFTDI = "";
                time.Stop();
                tiempoTarjetaFTDI = time.ElapsedMilliseconds.ToString();
                time.Reset();

                UInt32 numBytesRead = 0;
                byte[] rawData = new byte[numBytesExpected];
                ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);



                entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();


            }

            return entradaDatos;
        }

        private string readFromFTDI1ByteInicial(UInt32 cantByte)
        {
            string entradaDatos = "x";

            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = cantByte;




            ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                bool res = myFtdiDevice.IsOpen;
                throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);
            }



            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];

            if (numBytesAvailable == 3)
            {
                numBytesExpected = 3;
                rawData = new byte[numBytesExpected];
            }


            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);

            if(numBytesAvailable != 0) 
            { 
            entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();
            }
            return entradaDatos;
        }

        private void sendCommandFTDI(String valor)
        {

            if (myFtdiDevice.IsOpen)
            {

                myFtdiDevice.Purge(0);
                byte[] r = null;
                if (valor == "9" || valor == "5")
                {
                    r = new byte[1];
                    r[0] = Convert.ToByte(Convert.ToInt32(valor));
                }
                else
                {
                    r = Encoding.ASCII.GetBytes(valor);
                }

                UInt32 numBytesWritten = 2;
                ftStatus = myFtdiDevice.Write(r, r.Length, ref numBytesWritten);


            }
        }


        private void timer1_Tick_1(object sender, EventArgs e)  
        {

            

            if (sentido == 1)
            {
                posXCir += 4;


                if (!res)
                {
                    tiempo.Start();
                    res = true;
                }

                if (posXCir >= largoRect1 + bordeIzquierdo - largoCir)
                {
                    timer1.Stop();
                    tiempo.Stop();


                    if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                    {
                        sendCommandFTDI("e");

                    }
                    if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                    {

                        arduinoPort.Write("e");
                    }

                    string res = tiempo.Elapsed.TotalSeconds.ToString();
                    quitarCircunferencia = true;

                }
            }
            else   //////////////////Variante 2
            {

                posXCir -= 4;


                if (!res)
                {
                    tiempo.Start();
                    res = true;
                }



                if (posXCir <= posXRect4)
                {
                    timer1.Stop();
                    tiempo.Stop();


                    if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                    {
                        sendCommandFTDI("e");
                       
                    }
                    if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                    {

                        arduinoPort.Write("e");
                    }

                   
                  


                    string res = tiempo.Elapsed.TotalSeconds.ToString();
                    quitarCircunferencia = true;

                }

            }

            Invalidate();


            //   Manda a recibir loas datos del FTDI
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            quitarCircunferencia = false;
            pintarCircunferencia = true;
            salidaCirculo = false;
            finCliclo = false;
            controlTimer3 = 0;


            if (variante.Length > count)
            {
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    sendCommandFTDI("i");
                    breakLopp = false;
                }
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                {

                    arduinoPort.Write("i");
                }


                
                if (variante[count] == 1)
                {
                    timer1.Interval = contador1;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 1266;
                    velocidadAtual = "1";


                }
                if (variante[count] == 2)
                {
                    timer1.Interval = contador1;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 1266;
                    velocidadAtual = "1";
                }
                if (variante[count] == 3)
                {
                    timer1.Interval = contador2;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 1688;
                    velocidadAtual = "2";
                }
                if (variante[count] == 4)
                {
                    timer1.Interval = contador2;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 1688;
                    velocidadAtual = "2";
                }
                if (variante[count] == 5)
                {
                    timer1.Interval = contador3;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 2110;
                    velocidadAtual = "3";

                }
                if (variante[count] == 6)
                {
                    timer1.Interval = contador3;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 2110;
                    velocidadAtual = "3";


                }
                if (variante[count] == 7)
                {
                    timer1.Interval = contador4;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 2532;
                    velocidadAtual = "4";


                }
                if (variante[count] == 8)
                {
                    timer1.Interval = contador4;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 2532;
                    velocidadAtual = "4";
                }

                varianteSentido = variante[count].ToString();

                Invalidate();

                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    String temp = readFromFTDI1Byte(Convert.ToUInt32(2));  //SI se queda 2 seg sin pulsar error


                    if (temp == "l")
                    {
                        breakLopp = true;
                        timer2.Stop();
                        timer1.Stop();
                        timer3.Stop();
                        timer4.Stop();
                        myFtdiDevice.Close();

                        this.Invoke(new MethodInvoker(() => Cursor.Show()));

                        MessageBox.Show("Pulsador suelto, prueba abortada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Invoke(new MethodInvoker(() => this.Close()));

                    }


                }
                

                timer1.Start();
                count++;

            }
            else
            {
                countArduMessage = 0;
                bool fin = false;
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    sendCommandFTDI("9");
                    breakLopp = false;
                    string res = readFromFTDI1Byte(Convert.ToUInt32(2));
                    if (res == "f")
                        fin = true;
                }
                else
                {
                    arduinoPort.Write("9");

                }

                timer2.Stop();
                timer1.Stop();
                timer3.Stop();
                timer4.Stop();
                time.Stop();


                if (fin)
                {
                    this.BackColor = Color.Black;
                    pintarCircunferencia = false;
                    this.Invoke(new MethodInvoker(() => Cursor.Show()));

                    Esperar d = new Esperar();
                    d.Show();

                    RA_Form3 form = new RA_Form3(tiempos, varianteNombre, idPerson, idEtapa, nombreAtleta, varianteNombre);
                    d.Close();
                    form.ShowDialog();
                    this.Invoke(new MethodInvoker(() => this.Close()));

                }

            }
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            time.Stop();
            time.Reset();
            Auxiliar aux = new Auxiliar("-", velocidadAtual, "Omisión", "Omisión", varianteSentido);
            tiempos.Add(aux);

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {

                backgroundWorker1.CancelAsync();
                breakLopp = true;
                sendCommandFTDI("c");

                if (entrenamiento != 0)
                {
                    if (count == entrenamiento)
                    {
                        breakLopp = false;
                        sendCommandFTDI("9");
                        Thread.Sleep(100);
                        String fin = readFromFTDI1Byte(Convert.ToUInt32(2));
                        breakLopp = true;
                        this.Invoke(new MethodInvoker(() => Cursor.Show()));
                        timerEntrenamiento.Stop();
                        timer1.Stop();
                        timer3.Stop();
                        timer4.Stop();
                        tiempos.Clear();
                        quitarCircunferencia = true;
                        salidaCirculo = true;

                        backgroundWorker1.CancelAsync();
                        this.BackColor = Color.Black;
                        Invalidate();

                        inicioTest();

                    }
                }
                else
                {
                    if (variante.Length == count)
                    {
                        breakLopp = false;
                        sendCommandFTDI("9");
                        Thread.Sleep(100);
                        String fin = readFromFTDI1Byte(Convert.ToUInt32(2));

                        if (fin == "f")
                        {

                            /*
                            timerEntrenamiento.Stop();
                            timer1.Stop();
                            timer3.Stop();
                            timer4.Stop();
                            tiempos.Clear();
                            quitarCircunferencia = true;
                            salidaCirculo = true;

                        */
                            breakLopp = true;
                            timer2.Stop();
                            timer1.Stop();
                            timer3.Stop();
                            timer4.Stop();
                            time.Stop();
                            backgroundWorker1.CancelAsync();
                            this.BackColor = Color.Black;
                            pintarCircunferencia = false;
                            this.Invoke(new MethodInvoker(() => Cursor.Show()));

                            Esperar d = new Esperar();
                            d.Show();

                            RA_Form3 form = new RA_Form3(tiempos, varianteNombre, idPerson, idEtapa, nombreAtleta, varianteNombre);
                            d.Close();
                            form.ShowDialog();
                            this.Invoke(new MethodInvoker(() => this.Close()));


                        }

                    }
                }


            }
            else
            {
                arduinoPort.Write("c");
            }



            timer3.Stop();
            Invalidate();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            salidaCirculo = false;
            finCliclo = true;

            Invalidate();
        }


        //Timer de entrenamiento
        private void timerEntrenamiento_Tick(object sender, EventArgs e)
        {

            quitarCircunferencia = false;
            pintarCircunferencia = true;
            salidaCirculo = false;
            finCliclo = false;
            controlTimer3 = 0;


            if (count < entrenamiento)
            {

                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    sendCommandFTDI("i");
                    breakLopp = false;
                }
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                {
                    arduinoPort.Write("i");
                }



                if (variante[count] == 1)
                {
                    timer1.Interval = contador1;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 1266;

            

                }
                if (variante[count] == 2)
                {
                    timer1.Interval = contador1;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 1266;
                }
                if (variante[count] == 3)
                {
                    timer1.Interval = contador2;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 1688;
                }
                if (variante[count] == 4)
                {
                    timer1.Interval = contador2;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 1688;
                }
                if (variante[count] == 5)
                {
                    timer1.Interval = contador3;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 2110;

                }
                if (variante[count] == 6)
                {
                    timer1.Interval = contador3;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 2110;

                }
                if (variante[count] == 7)
                {
                    timer1.Interval = contador4;
                    posXCir = bordeIzquierdo;
                    posYCir = posYRect1;
                    sentido = 1;
                    timer3.Interval = 3000 + 2532;

                }
                if (variante[count] == 8)
                {
                    timer1.Interval = contador4;
                    posXCir = posXRect2;
                    posYCir = posYRect3;
                    sentido = 2;
                    timer3.Interval = 3000 + 2532;

                }




                Invalidate();

                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    String temp = readFromFTDI1Byte(Convert.ToUInt32(2));

                    if (temp == "o")
                    {
                        sendCommandFTDI("e");

                    }
                    else
                    {
                        if (temp == "l")
                        {
                            breakLopp = true;
                            this.Invoke(new MethodInvoker(() => Cursor.Show()));
                            timerEntrenamiento.Stop();
                            timer1.Stop();
                            timer3.Stop();
                            timer4.Stop();
                            tiempos.Clear();
                            quitarCircunferencia = true;
                            salidaCirculo = true;

                            backgroundWorker1.CancelAsync();
                            this.BackColor = Color.Black;
                            Invalidate();

                            MessageBox.Show("Pulsador suelto, prueba abortada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            inicioTest();

                        }
                    }

                }
                //else
                //{
                //    arduinoPort.Write("e");
                //}



                timer1.Start();
                count++;

            }
            else
            {


                countArduMessage = 0;
                String finPruba = "";
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    sendCommandFTDI("9");

                    Thread.Sleep(100);

                    finPruba = readFromFTDI1Byte(Convert.ToUInt32(2));
                    if (finPruba == "f")
                    {
                        this.Invoke(new MethodInvoker(() => Cursor.Show()));
                        timerEntrenamiento.Stop();
                        timer1.Stop();
                        timer3.Stop();
                        timer4.Stop();
                        tiempos.Clear();
                        quitarCircunferencia = true;
                        salidaCirculo = true;
                        breakLopp = true;
                        backgroundWorker1.CancelAsync();
                        Invalidate();
                        this.BackColor = Color.Black;
                        inicioTest();
                    }
                }

                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                {
                    arduinoPort.Write("9");
                }



            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (myFtdiDevice.IsOpen)
            {
                breakLopp = true;
                myFtdiDevice.Purge(0);
                myFtdiDevice.Close();
            }




            if (arduinoPort.IsOpen)
            {
                arduinoPort.DiscardInBuffer();
                arduinoPort.Close();
            }


        }


    }
}
