using FTD2XX_NET;
using Multitest.FormAux;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Multitest
{
    public partial class TRC_Form1 : Form
    {
        double ftdiTimeCorrection = 0.8187;
        double arduinoTimeCorrection = 0.8831;
        bool sonido = false;
        bool sonidoPrueba = false;
        int cantEnsayoTotal = 0;
        bool backGroungFlag = false;
        private List<Result> valores = new List<Result>();
        private Stopwatch time;
        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice;
        bool sonidoTarjeta = false;
        bool cancelada = false;

        string[] v1 = new string[] { "2", "1", "3", "4", "2", "3", "5", "3", "2", "1", "5", "2", "1", "3", "3", "2", "1", "4", "2", "3", "2", "1", "6", "2", "3", "3", "1", "1", "2", "6" };
        string[] v2 = new string[] { "1", "3", "2", "2", "6", "3", "4", "2", "3", "1", "2", "1", "5", "5", "2", "3", "1", "1", "2", "6", "4", "2", "1", "6", "3", "2", "1", "4", "2", "3" };
        string[] v3 = new string[] { "1", "2", "3", "4", "2", "1", "6", "4", "3", "1", "2", "1", "3", "5", "2", "3", "1", "1", "2", "2", "4", "3", "1", "6", "4", "2", "1", "5", "3", "2" };
        string[] v4 = new string[] { "3", "3", "1", "2", "4", "2", "3", "6", "3", "1", "2", "1", "5", "4", "2", "3", "1", "1", "3", "2", "6", "3", "1", "3", "5", "2", "1", "4", "3", "2" };
        string[] v5 = new string[] { "6", "3", "2", "2", "5", "3", "4", "2", "3", "1", "2", "1", "5", "5", "2", "3", "1", "1", "2", "4", "3", "2", "1", "6", "2", "3", "1", "4", "3", "3" };
        string[] v6 = new string[] { "1", "3", "2", "2", "1", "3", "1", "2", "3", "1", "2", "1", "3", "3", "2", "1", "3", "1", "2", "2", "3", "2", "1", "3", "2", "3", "1", "2", "3", "3" };

        string[] varianteEscojida = new string[] { };

        int indexVariante = 0;
        string variante = "";


        List<String> litVal = new List<String>();
        //  string[] colores = new string[] { "amarillo", "rojo", "Verde" };
        int valorN = 0;
        int count = 0;
        int cantEnsayo = 0;
        bool flag = true;
        bool entrenamiento = false;

        String tiempo = "";
        String estimuloPantalla = "";
        String estimuloArduino = "";
        int u = 0;
        String idPerson = "";
        String idEtapa = "";
        String nombreAtleta;
        public TRC_Form1(bool sonido, String idPerson, String idEtapa, String nombreAtleta)
        {
            InitializeComponent();
            this.idPerson = idPerson;
            this.sonido = sonido;
            this.idEtapa = idEtapa;
            this.nombreAtleta = nombreAtleta;
            myFtdiDevice = new FTDI();
            time = new Stopwatch();


        }



        private void Form1_Load(object sender, EventArgs e)
        {

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {
                openPorFTDI();

            }

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
            {
                configurarPuertoArduino();

            }


            empezar();


        }

        public void empezar()
        {
            if (flag)
            {

                TRC_Form2 f = new TRC_Form2(entrenamiento, sonido);
                DialogResult res = f.ShowDialog();
                if (f.resultado)
                {


                    cantEnsayoTotal = f.cant;
                    entrenamiento = f.entrenamiento;
                    variante = f.variante.ToString();
                    aignarVariante(variante);

                    
                    sendCommandFTDI("9");
                    Thread.Sleep(100);
                    string fin = readFromFTDI1ByteInicial(3);
                    if (fin == "f" || fin == "x")
                    {
                        myFtdiDevice.Purge(0);

                    }
                    
                    if (MessageBox.Show("Debe mantener el pulsador apretado y responder liberandolo para cada estímulo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {

                        Invoke(new MethodInvoker(() => { Cursor.Hide(); }));


                        if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                        {

                            Thread.Sleep(100);
                            sendCommandFTDI("4");
                            Thread.Sleep(100);
                            string prueba = readFirstDataFromFTDI1Byte();

                            if (prueba == "4")
                            {
                                comenzarCicloFTDI();
                            }

                        }

                        if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                        {
                            arduinoPort.Write("4");
                            comenzarCiclo();

                        }




                    }
                }
                else
                {

                    Invoke(new Action(() => { this.Close(); }));
                }



            }

        }




        private string readFirstDataFromFTDI1Byte()
        {
            string entradaDatos = "";
            string entradadoble = "";
            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = 3;
            // myFtdiDevice.Purge(0);
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


            if (numBytesAvailable > 3)
            {
                entradadoble = readFromFTDI1ByteInicial(3);
            }


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);

            return rawData[0].ToString();
        }

        private void sendCommandFTDI(String valor)
        {

            if (myFtdiDevice.IsOpen)
            {

                myFtdiDevice.Purge(0);
                byte[] r = null;
                if (valor == "4" || valor == "3" || valor == "9" || valor == "9")
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


        private void comenzarCicloFTDI()
        {
            try
            {

                if (cantEnsayo < cantEnsayoTotal && !cancelada)
                {
                    string ready = "";
                    Random res = new Random();
                    double valor = (res.NextDouble() * (4 - 2)) + 2;
                    int rers = Convert.ToInt32(Math.Round(valor, 1) * 1000);
                    Thread.Sleep(rers);

                    sendCommandFTDI("e");
                    bool sonidoSinReacc = false;
                    string entradaDatos = readFromFTDI1Byte(Convert.ToUInt32(3));

                    litVal.Add(entradaDatos);

                    if (entradaDatos == "n")
                    {

                        Invoke(new MethodInvoker(() =>
                        {
                            mostrarEstimuloFTDI();

                        }));

                        if (!time.IsRunning)
                            time.Start();


                        ready = readFromFTDI1Byte(Convert.ToUInt32(3));

                        String temp = null;
                        if (ready != "o")
                        {

                            if (ready == "r")
                            {
                                sonidoSinReacc = true;
                                temp = "-";
                                estimuloArduino = "-";
                            }
                            else
                                temp = ready;

                            int timen;
                            bool isNumeric = int.TryParse(temp, out timen);
                            if (isNumeric)
                            {
                                temp = Convert.ToString( Convert.ToInt32(timen * ftdiTimeCorrection)); 
                            }

                            Result temp1 = new Result(sonidoPrueba, estimuloPantalla, estimuloArduino, temp);
                            valores.Add(temp1);

                        }
                        else
                        {
                            Result temp1 = new Result(sonidoPrueba, estimuloPantalla, estimuloArduino, "-");
                            valores.Add(temp1);
                            temp = null;
                        }

                        pictureBox1.Invoke(new Action(() => pictureBox1.Visible = false));


                        if (!sonidoSinReacc)
                        {
                            ready = readFromFTDI1Byte(Convert.ToUInt32(3));
                            if (ready == "a")
                            {
                                cancelada = true;
                                Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                                MessageBox.Show("Pulsadores sueltos. Prueba abortada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                            }

                        }

                    }
                    else
                    {

                        if (entradaDatos != "f")
                        {
                            Result temp = new Result(sonidoPrueba, "-", estimuloArduino, entradaDatos);
                            valores.Add(temp);
                            temp = null;

                            ready = readFromFTDI1Byte(Convert.ToUInt32(3));

                            if (ready == "a")
                            {
                                Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                                MessageBox.Show("Prueba abortada.Pulsadores sueltos. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cancelada = true;
                            }
                        }
                        else
                        {
                            
                            Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                            MessageBox.Show("Prueba abortada.Pulsadores sueltos. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cancelada = true;
                            
                            Invoke(new MethodInvoker(() => { this.Close(); }));

                        }


                    }

                    if (ready == "r")
                    {
                        cantEnsayo++;
                        indexVariante++;
                        comenzarCicloFTDI();
                    }
                }
                else
                {
                    if (!cancelada)
                    {
                        sendCommandFTDI("9");
                       

                        if (entrenamiento)
                        {

                           
                            String fin = readFromFTDI1Byte(Convert.ToUInt32("3"));
                            if (fin == "f")
                            {
                                cantEnsayo = 0;
                                entrenamiento = false;
                                cantEnsayoTotal = 0;
                                cancelada = false;
                                varianteEscojida = null;
                                indexVariante = 0;
                                 
                                Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                                empezar();
                            }

                        }
                        else
                        {

                           
                            string fin = readFromFTDI1Byte(3);

                            cantEnsayo = 0;
                            indexVariante = 0;
                            if (fin == "f" && !entrenamiento)
                            {
                                Application.DoEvents();
                                Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                                Esperar t = new Esperar();
                                t.Show();
                                TRC_Form3 form = new TRC_Form3(variante, valores, sonido, idPerson, idEtapa, variante, nombreAtleta);
                                t.Close();
                                form.ShowDialog();
                            }
                        }



                    }

                    Invoke(new MethodInvoker(() =>
                    {
                        this.Close();

                    }));


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error. " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                this.Close();

            }

        }

        private string readFromFTDI1Byte(UInt32 cantByte)
        {
            string entradaDatos = "";

            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = cantByte;
            myFtdiDevice.Purge(0);
            string temp = "";

            while (numBytesAvailable < numBytesExpected)
            {

                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    bool res = myFtdiDevice.IsOpen;
                    throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);
                }



            }

            if (numBytesAvailable != numBytesExpected)
            {
                //throw new Exception("Error: Invalid data in buffer. (1350)");

                UInt32 numBytesRead1 = 5;
                byte[] rawData1 = new byte[numBytesExpected];
                ftStatus = myFtdiDevice.Read(rawData1, 5, ref numBytesRead1);
            }

            if (time.IsRunning)
            {
                time.Stop();
                temp = time.Elapsed.Milliseconds.ToString();
                time.Reset();
            }


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


            entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();

            litVal.Add(entradaDatos);
            if (entradaDatos != "n" && entradaDatos != "r" && entradaDatos != "a" && entradaDatos != "f")
            {

                if (entradaDatos != "o")
                {
                    if (entradaDatos == "t")
                    {
                        estimuloArduino = ftdiColor(rawData[2].ToString());
                        entradaDatos = temp;
                    }
                    else
                    {
                        estimuloArduino = ftdiColor(rawData[2].ToString());
                        entradaDatos = temp;

                        byte[] tiempoArr = new byte[2];
                        tiempoArr[0] = rawData[0];
                        tiempoArr[1] = rawData[1];

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(tiempoArr);

                        entradaDatos = BitConverter.ToInt16(tiempoArr, 0).ToString();
                    }

                }

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

            if (numBytesAvailable == 2 || numBytesAvailable == 4)
            {
                numBytesExpected = numBytesAvailable;
                rawData = new byte[numBytesExpected];
            }

          
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesExpected);
            entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();
            if (entradaDatos == "\0")
            {
                entradaDatos = "x";
            }
            
            
            return entradaDatos;
        }



        private void aignarVariante(string var)
        {
            if (var == "v1")
                varianteEscojida = v1;
            if (var == "v2")
                varianteEscojida = v2;
            if (var == "v3")
                varianteEscojida = v3;
            if (var == "v4")
                varianteEscojida = v4;
            if (var == "v5")
                varianteEscojida = v5;
            if (var == "v6")
                varianteEscojida = v6;
        }

        private void comenzarCiclo()
        {
            try
            {
                arduinoPort.DiscardInBuffer();
                Random res = new Random();
                double valor = (res.NextDouble() * (4 - 2)) + 2;
                int rers = Convert.ToInt32(Math.Round(valor, 1) * 1000);
                Thread.Sleep(rers);

                arduinoPort.Write(Convert.ToString("e"));

            }
            catch (Exception)
            {

                MessageBox.Show("Ha ocurrido un error reconecte el equipo a la PC.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                this.Invoke(new Action(() => this.Close()));
            }




        }

        public void mostrarEstimuloArduino()
        {
            String estimulo = varianteEscojida[indexVariante];

            if (estimulo == "1")
            {
                arduinoPort.Write("n");
                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.rojo));
                estimuloPantalla = "rojo";
            }

            if (estimulo == "2")
            {
                arduinoPort.Write("n");
                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.Verde));
                estimuloPantalla = "verde";
            }

            if (estimulo == "3")
            {
                arduinoPort.Write("n");
                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.amarillo));
                estimuloPantalla = "amarillo";
            }


            if (estimulo == "4")
            {
                if (sonido == false)
                {
                    arduinoPort.Write("n");
                    sonidoPrueba = false;

                }
                else
                {
                    arduinoPort.Write("m");
                    sonidoPrueba = true;
                }

                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.rojo));
                estimuloPantalla = "rojo";

            }
            if (estimulo == "5")
            {
                if (sonido == false)
                {
                    arduinoPort.Write("n");
                    sonidoPrueba = false;
                }
                else
                {
                    arduinoPort.Write("m");
                    sonidoPrueba = true;
                }

                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.Verde));
                estimuloPantalla = "verde";

            }
            if (estimulo == "6")
            {
                if (sonido == false)
                {
                    arduinoPort.Write("n");
                    sonidoPrueba = false;

                }
                else
                {
                    arduinoPort.Write("m");
                    sonidoPrueba = true;
                }

                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.amarillo));
                estimuloPantalla = "amarillo";

            }

            pictureBox1.Invoke(new Action(() => pictureBox1.Visible = true));




        }


        public void mostrarEstimuloFTDI()
        {
            String estimulo = varianteEscojida[indexVariante];
            sonidoPrueba = false;
            if (estimulo == "1")
            {
                sendCommandFTDI("n");
                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.rojo));
                estimuloPantalla = "rojo";
            }

            if (estimulo == "2")
            {
                sendCommandFTDI("n");
                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.Verde));
                estimuloPantalla = "verde";
            }

            if (estimulo == "3")
            {
                sendCommandFTDI("n");
                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.amarillo));
                estimuloPantalla = "amarillo";
            }


            if (estimulo == "4")
            {
                if (sonido == false)
                {
                    sendCommandFTDI("n");
                    sonidoPrueba = false;

                }
                else
                {
                    sendCommandFTDI("s");
                    sonidoPrueba = true;
                }

                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.rojo));
                estimuloPantalla = "rojo";

            }
            if (estimulo == "5")
            {
                if (sonido == false)
                {
                    sendCommandFTDI("n");
                    sonidoPrueba = false;
                }
                else
                {
                    sendCommandFTDI("s");
                    sonidoPrueba = true;
                }

                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.Verde));
                estimuloPantalla = "verde";

            }
            if (estimulo == "6")
            {
                if (sonido == false)
                {
                    sendCommandFTDI("n");
                    sonidoPrueba = false;

                }
                else
                {
                    sendCommandFTDI("s");
                    sonidoPrueba = true;
                }

                pictureBox1.Invoke(new Action(() => pictureBox1.Image = Properties.Resources.amarillo));
                estimuloPantalla = "amarillo";

            }

            pictureBox1.Invoke(new Action(() => pictureBox1.Visible = true));




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

        private void configurarPuertoArduino()
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
                    MessageBox.Show("El equipo no esta conectado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                    this.Invoke(new Action(() => this.Close()));
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                this.Invoke(new Action(() => this.Close()));
            }


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

        private void recibirArduino(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                SerialPort sp = (SerialPort)sender;
                string entradaDatos = sp.ReadExisting();
                entradaDatos = entradaDatos.Trim();
                litVal.Add(entradaDatos);


                if (entradaDatos == "Abort")
                    entradaDatos = "Prueba abortada";


                if (entradaDatos != "TRC" && entradaDatos != "FIN TRC" && entradaDatos != "Prueba abortada" && entradaDatos != "FIN TRC Inicio")
                {

                    //  Application.Exit();
                    if (entradaDatos == "n")
                    {


                        // backgroundWorker1.RunWorkerAsync(); //muestra el estimulo


                        Invoke(new MethodInvoker(() =>
                        {
                            mostrarEstimuloArduino();

                        }));

                        // arduinoPort.Write("n");
                        valorN = 1;



                    }
                    else
                    {
                        // entradaDatos = entradaDatos.Substring(1, entradaDatos.Length - 1);
                        if (entradaDatos.Substring(0, 1) == "-")
                        {
                            tiempo = entradaDatos.Substring(0, entradaDatos.Length);
                            estimuloArduino = "-";


                        }


                    }


                    if (valorN == 1 && count == 1 && entradaDatos != "Prueba abortada")
                    {
                        if (entradaDatos == "Omision" || entradaDatos == "Ready")
                        {
                            tiempo = "-";

                            //if (entradaDatos == "Omision")
                            //    tiempo = "-";
                            //else
                            //    tiempo = "-";
                        }

                        else
                        {

                            String temp = entradaDatos.Trim();
                            int index = temp.IndexOf(";");
                            tiempo = temp.Substring(0, index);
                            string result = temp.Substring(index + 1, temp.Length - (index + 1));
                            estimuloArduino = arduinoColor(result);
                            u++;
                        }

                        /* if (backgroundWorker1.IsBusy)
                         {
                             backgroundWorker1.CancelAsync();
                         }
                        */
                        pictureBox1.Invoke(new Action(() => pictureBox1.Visible = false));

                    }


                    count++;

                    if (entradaDatos == "Ready")
                    {
                        if (!entrenamiento)
                        {

                            if (sonidoPrueba == true && estimuloPantalla != "" && estimuloArduino == "")
                                tiempo = "-";


                            int timen;
                            bool isNumeric = int.TryParse(tiempo, out timen);
                            if (isNumeric)
                            {
                                tiempo = Convert.ToString(Convert.ToInt32(timen * ftdiTimeCorrection));
                            }

                            Result temp = new Result(sonidoPrueba, estimuloPantalla, estimuloArduino, tiempo);
                            valores.Add(temp);
                            temp = null;
                        }


                        entradaDatos = null;
                        count = 0;
                        valorN = 0;
                        sonidoPrueba = false;

                        indexVariante++;
                        cantEnsayo++;

                        if (cantEnsayo < cantEnsayoTotal)
                            comenzarCiclo();

                    }



                    if (entradaDatos == "Prueba abortada" || cantEnsayo == cantEnsayoTotal)
                    {
                        if (entradaDatos == "Prueba abortada")
                        {
                            count = 0;

                            valorN = 0;
                            cantEnsayo = 0;
                            valores.Clear();

                        }

                        arduinoPort.Write("9");


                    }


                }
                else
                {

                    if (entradaDatos == "FIN TRC")
                    {
                        if (entrenamiento)
                        {
                            indexVariante = 0;
                            cantEnsayo = 0;
                            Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                            empezar();
                        }
                        else
                        {
                            Application.DoEvents();

                            Invoke(new MethodInvoker(() => { Cursor.Show(); }));

                            Esperar t = new Esperar();
                            t.Show();
                            TRC_Form3 form = new TRC_Form3(variante, valores, sonido, idPerson, idEtapa, variante, nombreAtleta);
                            t.Close();

                            form.ShowDialog();
                            this.Invoke(new Action(() => this.Close()));
                        }

                    }
                    else
                    {
                        if (entradaDatos != "FIN TRC Inicio")
                        {
                            if (entradaDatos == "Prueba abortada")
                            {

                                MessageBox.Show("Prueba Abortada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (entrenamiento == true)
                                {
                                    flag = true;
                                    indexVariante = 0;
                                    cantEnsayo = 0;
                                    empezar();
                                }
                                else
                                {


                                    Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                                    flag = false;
                                    this.Invoke(new MethodInvoker(() => this.Close()));
                                }


                            }
                        }
                        else
                        {
                            Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                            MessageBox.Show("Pulsador suelto prueba Abortada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         
                            this.Invoke(new MethodInvoker(() => this.Close()));

                        }
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Ha ocurrido un error reconecte el equipo a la PC", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                flag = false;
                this.Invoke(new MethodInvoker(() => this.Close()));
            }

        }


        private String arduinoColor(String num)
        {
            string color = null;
            if (num == "8")
                color = "amarillo";
            if (num == "9")
                color = "verde";
            if (num == "10")
                color = "rojo";

            return color;
        }


        private String ftdiColor(String num)
        {
            string color = null;
            if (num == "1")
                color = "amarillo";
            if (num == "2")
                color = "verde";
            if (num == "3")
                color = "rojo";

            return color;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {


            if (myFtdiDevice.IsOpen)
            {
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
