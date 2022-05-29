using FTD2XX_NET;
using Multitest.ADOmodel;
using Multitest.AuxClass;
using Multitest.FormAux;
using Multitest.PanelesPrincipales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Multitest
{
    public partial class TRS_Form1 : Form
    {
   
        double ftdiTimeCorrection = 0.8187;
        double arduinoTimeCorrection = 0.8831;
        String color = null;
        int cantEnsayoTotal = 0;
        List<String> valores = new List<String>();
        List<String> litVal = new List<String>();
        int valorN = 0;
        int count = 0;
        int cantEnsayo = 0;
        bool flag = true;
        Stopwatch time = null;
        bool colgado = false;

        String etapa = "";
        String idUser = "";
        Stopwatch tiempo = null;
        ConectionFTDI ftdi;
        string nombreAtleta = "";
        bool cancelada = false;

        StopWatchUser userStopWatch = new StopWatchUser();
        DateTime start = new DateTime();
        DateTime end = new DateTime();
        int contador = 0;

        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice;
        bool isRunning = false;

        public TRS_Form1(String idUser, String etapa, String nombreAtleta)
        {


            InitializeComponent();
            this.nombreAtleta = nombreAtleta;

            tiempo = new Stopwatch();
            this.etapa = etapa;
            this.idUser = idUser;

            myFtdiDevice = new FTDI();
            time = new Stopwatch();

            //backgroundWorker1.DoWork += comenzarCicloFTDI2;
            ////backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            //backgroundWorker1.RunWorkerCompleted += cancelRecibirDatos;  //Tell the user how the process went
            //backgroundWorker1.WorkerReportsProgress = true;
            //backgroundWorker1.WorkerSupportsCancellation = true;

            // backgroundWorker1.DoWork += mostrarEstimulo;
            // backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            // backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //Tell the user how the process went
            // backgroundWorker1.WorkerReportsProgress = true;
            // backgroundWorker1.WorkerSupportsCancellation = true;

        }



        private void cancelRecibirDatos(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled != null)
            {
                //MessageBox.Show("cancelo");
            }
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

            

            comenzarPrueba();


        }


        public void comenzarPrueba()
        {
            if (flag)
            {
                TRS_Form2 f = new TRS_Form2();
                DialogResult res = f.ShowDialog();
                
                if (res == DialogResult.Cancel)
                {
                    color = f.color;
                    cantEnsayoTotal = f.cant;
                }

                if (color != null)
                {

                    sendCommandFTDI("9");
                   
                    string fin = readFromFTDI1ByteInicial(2);
                    TimeSpan.FromMilliseconds(500);
                    if (fin == "f" || fin == "x")
                    {
                        if (fin == "f")
                        {
                            colgado = true;
                        }
                        myFtdiDevice.Purge(0);

                    }

                    if (MessageBox.Show("Debe mantener el pulsador apretado y responder liberandolo para cada estímulo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {

                        
                        Invoke(new MethodInvoker(() => { Cursor.Hide(); }));


                        if (color == "Amarillo")
                        {
                            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                            {

                           if(fin == "x")
                                fin = readFromFTDI1ByteInicial(2);
                                sendCommandFTDI("2");

                            }
                            myFtdiDevice.Purge(0);

                            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                            {
                                arduinoPort.Write(Convert.ToString(2));

                            }

                            pictureBox1.Image = Properties.Resources.amarillo;
                        }

                        else
                        {

                            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                            {
                                sendCommandFTDI("3");
                            }

                            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                            {
                                arduinoPort.Write(Convert.ToString(3));

                            }

                            pictureBox1.Image = Properties.Resources.rojo;

                        }




                        if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                        {
                            string prueba = readFirstDataFromFTDI1Byte();
                            if (prueba == "2" || prueba == "3")
                                comenzarCicloFTDI();
                            else
                                MessageBox.Show("no se entro a la prueba");


                        }

                        if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                        {
                            comenzarCiclo();

                        }



                    }
                }
                else
                    this.Close();
            }
            else
            {
                //   Application.Exit();
                this.Close();
            }
        }

        private void sendCommandFTDI(String valor)
        {

            if (myFtdiDevice.IsOpen)
            {

                myFtdiDevice.Purge(0);
                byte[] r = null;
                if (valor == "2" || valor == "3" || valor == "9" || valor == "0")
                {
                    r = new byte[1];
                    r[0] = Convert.ToByte(Convert.ToInt32(valor));
                }
                else
                {
                    r = Encoding.ASCII.GetBytes("e");
                }

                UInt32 numBytesWritten = 1;
                ftStatus = myFtdiDevice.Write(r, r.Length, ref numBytesWritten);
         


            }
        }

        


        private void comenzarCiclo()
        {
            try
            {
                Random res = new Random();
                double valor = (res.NextDouble() * (4 - 2)) + 2;
                int rers = Convert.ToInt32(Math.Round(valor, 1) * 1000);
                Thread.Sleep(rers);


                arduinoPort.Write(Convert.ToString("e"));




            }
            catch (Exception)
            {

                MessageBox.Show("El dispositivo no está conectado a la PC. Para poder realizar la pruebas pruebas psofisiológicas  conecte el equipo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                this.Close();
                //   Application.Exit();
            }




        }

        public void mostrarEstimulo()
        {

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
                    MessageBox.Show("El dispositivo no está conectado a la PC. Para poder realizar la pruebas pruebas psofisiológicas  conecte el equipo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                    this.Close();
                    //  Application.Exit();
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
                this.Close();
                // Application.Exit();
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




                if (entradaDatos != "TRS" && entradaDatos != "FIN TRS Amarillo" && entradaDatos != "FIN TRS Rojo" && entradaDatos != "FIN TRS Amarillo Inicio" && entradaDatos != "FIN TRS Rojo Inicio")
                {


                    if (entradaDatos == "n")
                    {


                        Invoke(new MethodInvoker(() =>
                        {
                            mostrarEstimulo();

                        }));

                        //muestra el estimulo
                        valorN = 1;


                    }
                    else
                    {
                        if (entradaDatos != "Abort")
                        {
                            if (entradaDatos.Substring(0, 1) == "-")
                                valores.Add(entradaDatos);
                        }
                        else
                        {
                            if (entradaDatos == "Abort")
                            {
                                Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                                MessageBox.Show("Pulsador suelto prueba abortada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                flag = true;
                             
                                this.Invoke(new Action(() => this.Close()));
                            }
                        }


                    }



                    if (valorN == 1 && count == 1 && entradaDatos != "Prueba abortada")
                    {
                        if (entradaDatos == "Omision")
                            entradaDatos = "Omisión";

                        valores.Add(Convert.ToString(Convert.ToInt32(Convert.ToDouble(entradaDatos) * arduinoTimeCorrection)));
                        //  backgroundWorker1.CancelAsync();

                        pictureBox1.Invoke(new Action(() => pictureBox1.Visible = false));

                    }



                    count++;

                    if (entradaDatos == "Ready")
                    {


                        entradaDatos = null;
                        count = 0;
                        valorN = 0;
                        cantEnsayo++;

                        if (cantEnsayo < cantEnsayoTotal)
                            comenzarCiclo();





                    }





                    if (entradaDatos == "Prueba abortada" || cantEnsayo == cantEnsayoTotal)
                    {
                        if (entradaDatos == "Prueba abortada")
                        {
                            ///Cuando llega prueba abortada todos los valores en ell arduino se setean
                            count = 0;
                            valorN = 0;
                            cantEnsayo = 0;
                            valores.Clear();


                            MessageBox.Show("Prueba abortada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            //  Application.Exit();

                        }
                        else
                        {
                            //Aqui mando a finalizar la prueba en el arduino
                            arduinoPort.Write("9");
                        }



                    }
                }
                else
                {
                    if (entradaDatos == "FIN TRS Amarillo" || entradaDatos == "FIN TRS Rojo")
                    {

                        Invoke(new MethodInvoker(() => { Cursor.Show(); }));

                        salvaDatos();


                    }

                    if (entradaDatos == "FIN TRS Amarillo Inicio" || entradaDatos == "FIN TRS Rojo Inicio")
                    {
                        Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                        MessageBox.Show("Pulsador suelto prueba abortada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = true;
                        
                        this.Invoke(new Action(() => this.Close()));
                    }


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error. " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                this.Invoke(new Action(() => this.Close()));


            }

        }


        private void comenzarCicloFTDI()
        {
            try
            {

                
                if (cantEnsayo < cantEnsayoTotal && !cancelada)
                {
                    String ready = "";
                    Random res = new Random();
                    double valor = (res.NextDouble() * (4 - 2)) + 2;
                    int rers = Convert.ToInt32(Math.Round(valor, 1) * 1000);
                    Thread.Sleep(rers);

                    


                    sendCommandFTDI("e");

                 
                    string entradaDatos = readFromFTDI1Byte(Convert.ToUInt32(2));
                    litVal.Add(entradaDatos);

                    if (entradaDatos == "n")
                    {
                        Application.DoEvents();
                        pictureBox1.Invoke(new Action(() => pictureBox1.Visible = true));

                        if (!tiempo.IsRunning)
                            tiempo.Start();  // tiempo del contador



                        String temp = readFromFTDI1Byte(Convert.ToUInt32(2)) ;

                        pictureBox1.Invoke(new Action(() => pictureBox1.Visible = false)); // Muestra Estimulo

                        if (temp == "o")
                        {
                            temp = "Omisión";

                        }

                        valores.Add(temp.ToString());

                        ready = readFromFTDI1Byte(Convert.ToUInt32(2));

                        if (ready == "a")
                        {
                            Invoke(new MethodInvoker(() => { Cursor.Show(); }));
                            MessageBox.Show("Pulsador suelto. Prueba abortada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cancelada = true;
                            this.Invoke(new Action(() => this.Close()));
                            this.Close();
                            

                        }

                    }
                    else
                    {
                        if (entradaDatos != "f")
                        {
                            int adelantado = Convert.ToInt32(entradaDatos) * -1;
                            valores.Add(adelantado.ToString());

                            ready = readFromFTDI1Byte(Convert.ToUInt32(2));

                            if (ready == "a")
                            {
                                Invoke(new MethodInvoker(() => { Cursor.Show(); }));

                                MessageBox.Show("Pulsador suelto. Prueba abortada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cancelada = true;
                                this.Invoke(new Action(() => this.Close()));
                                this.Close();

                            }
                        }
                        else
                        {
                        
                            Invoke(new MethodInvoker(() => { Cursor.Show(); }));

                            MessageBox.Show("Pulsador suelto. Prueba abortada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cancelada = true;
                          
                            this.Invoke(new Action(() => this.Close()));
                            this.Close();
                        }

                    }

                    if (ready == "r")
                    {
                        cantEnsayo++;
                        comenzarCicloFTDI();
                    }

                }
                else
                {
                    if (!cancelada)
                    {

                        sendCommandFTDI("9");
                        Thread.Sleep(200);
                        string fin = readFromFTDI1Byte(2);
                        if (fin == "f")
                        {
                            myFtdiDevice.Purge(0);
                            myFtdiDevice.Close();
                            tiempo.Stop();
                            tiempo.Reset();

                            salvaDatos();
                        }
                        else
                        {
                            MessageBox.Show("Error al terminar la prueba. ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }


                    cantEnsayo = 0;
                    this.Invoke(new Action(() => this.Close()));


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error. " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
                this.Invoke(new Action(() => this.Close()));

            }

        }



        private string readFromFTDI1Byte(UInt32 cantByte)
        {
            string entradaDatos = "";

            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = cantByte;

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
            if (tiempo.IsRunning)
            {

                tiempo.Stop();
                temp = tiempo.ElapsedMilliseconds.ToString();
                tiempo.Reset();

            }


            if (numBytesAvailable != numBytesExpected)
            {
                UInt32 numBytesRead1 = 0;
                byte[] rawData1 = new byte[4];
                ftStatus = myFtdiDevice.Read(rawData1, numBytesAvailable, ref numBytesRead1);
                throw new Exception("Error: Invalid data in buffer. (1350)");
            }


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);

            entradaDatos = Encoding.ASCII.GetString(rawData, 0, rawData.Length - 1).ToString();

            if (entradaDatos != "n" && entradaDatos != "r" && entradaDatos != "a" && entradaDatos != "o" && entradaDatos != "f")
            {

                if (entradaDatos == "t")
                {
                    entradaDatos = temp;

                }
                else
                {
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(rawData);
                    entradaDatos = BitConverter.ToInt16(rawData, 0).ToString();
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
                if (myFtdiDevice.IsOpen)
                {

                }
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

            if (numBytesAvailable != 0)
            {
                entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();
            }
            

            

            return entradaDatos;
        }

        private string readFirstDataFromFTDI1Byte()
        {
            string entradaDatos = "";

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

          

            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesAvailable];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);
            if (rawData[0].ToString() == "f" && rawData[3].ToString() == "2") 
            {
                return rawData[3].ToString();
            }
            return rawData[0].ToString();
        }

        private void salvaDatos()
        {

            double tmaximo = 0;
            double tminimo = 100000000;
            double sumatoria = 0;
            int rcorrecta = 0;
            int cantAdel = 0;
            int cantOmisiones = 0;


            for (int i = 0; i < valores.Count; i++)
            {
                string val = valores[i];
                
                

                if (val != "Omisión")
                {
                    valores[i] = Convert.ToString(Convert.ToInt32(Convert.ToDouble(val) * ftdiTimeCorrection));
                    val = valores[i];
                    double temp = Convert.ToInt32(Convert.ToDouble(val));
                    if (temp > 0)
                    {
                        sumatoria += temp;

                        if (temp > tmaximo)
                            tmaximo = temp;
                        if (temp < tminimo)
                            tminimo = temp;


                        rcorrecta++;
                    }
                    else
                        cantAdel++;

                }
                else
                {
                    cantOmisiones++;
                }

            }

            double media = Math.Round(sumatoria / rcorrecta, 2);
           

            double desvEstandart = desviacionEstandart(media);



            string sum = Math.Round(sumatoria, 2).ToString();




            double desviacion = Math.Round(desvEstandart / media, 2);


            ///---------------------------------------------------------------------//


            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {


                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {

                    PruTrsimple pruR = new PruTrsimple();

                    pruR.Fecha = date;

                    pruR.Tiempo1 = valores.Count > 0 ? valores[0].ToString() : null;
                    pruR.Tiempo2 = valores.Count > 1 ? valores[1].ToString() : null;
                    pruR.Tiempo3 = valores.Count > 2 ? valores[2].ToString() : null;
                    pruR.Tiempo4 = valores.Count > 3 ? valores[3].ToString() : null;
                    pruR.Tiempo5 = valores.Count > 4 ? valores[4].ToString() : null;
                    pruR.Tiempo6 = valores.Count > 5 ? valores[5].ToString() : null;
                    pruR.Tiempo7 = valores.Count > 6 ? valores[6].ToString() : null;
                    pruR.Tiempo8 = valores.Count > 7 ? valores[7].ToString() : null;
                    pruR.Tiempo9 = valores.Count > 8 ? valores[8].ToString() : null;
                    pruR.Tiempo10 = valores.Count > 9 ? valores[9].ToString() : null;
                    pruR.Tiempo11 = valores.Count > 10 ? valores[10].ToString() : null;
                    pruR.Tiempo12 = valores.Count > 11 ? valores[11].ToString() : null;
                    pruR.Tiempo13 = valores.Count > 12 ? valores[12].ToString() : null;
                    pruR.Tiempo14 = valores.Count > 13 ? valores[13].ToString() : null;
                    pruR.Tiempo15 = valores.Count > 14 ? valores[14].ToString() : null;
                    pruR.Tiempo16 = valores.Count > 15 ? valores[15].ToString() : null;
                    pruR.Tiempo17 = valores.Count > 16 ? valores[16].ToString() : null;
                    pruR.Tiempo18 = valores.Count > 17 ? valores[17].ToString() : null;
                    pruR.Tiempo19 = valores.Count > 18 ? valores[18].ToString() : null;
                    pruR.Tiempo20 = valores.Count > 19 ? valores[19].ToString() : null;
                    pruR.Tiempo21 = valores.Count > 20 ? valores[20].ToString() : null;
                    pruR.Tiempo22 = valores.Count > 21 ? valores[21].ToString() : null;
                    pruR.Tiempo23 = valores.Count > 22 ? valores[22].ToString() : null;
                    pruR.Tiempo24 = valores.Count > 23 ? valores[23].ToString() : null;
                    pruR.Tiempo25 = valores.Count > 24 ? valores[24].ToString() : null;
                    pruR.Tiempo26 = valores.Count > 25 ? valores[25].ToString() : null;
                    pruR.Tiempo27 = valores.Count > 26 ? valores[26].ToString() : null;
                    pruR.Tiempo28 = valores.Count > 27 ? valores[27].ToString() : null;
                    pruR.Tiempo29 = valores.Count > 28 ? valores[28].ToString() : null;
                    pruR.Tiempo30 = valores.Count > 29 ? valores[29].ToString() : null;

                    pruR.TipoEstimulo = color;
                    pruR.CantAdel = cantAdel.ToString();
                    pruR.TiempoMaximo = tmaximo.ToString();
                    pruR.TiempoMedio = Math.Round(media, 2).ToString();
                    pruR.TiempoMinimo = tminimo.ToString();
                    pruR.SumTiempo = Math.Round(sumatoria, 2).ToString();
                    pruR.CantEstimulos = valores.Count().ToString();
                    pruR.CantOmisiones = cantOmisiones.ToString();
                    pruR.CoefVariacion = desviacion.ToString();
                    pruR.DesvStandar = desvEstandart.ToString();
                    pruR.RespCorrecta = rcorrecta.ToString();

                    entities.PruTrsimple.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruTrsimple>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PTrsimple = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PTrsimple == null)
                    {

                        PruTrsimple pruR = new PruTrsimple();
                        pruR.Fecha = date;


                        pruR.Tiempo1 = valores.Count > 0 ? valores[0].ToString() : null;
                        pruR.Tiempo2 = valores.Count > 1 ? valores[1].ToString() : null;
                        pruR.Tiempo3 = valores.Count > 2 ? valores[2].ToString() : null;
                        pruR.Tiempo4 = valores.Count > 3 ? valores[3].ToString() : null;
                        pruR.Tiempo5 = valores.Count > 4 ? valores[4].ToString() : null;
                        pruR.Tiempo6 = valores.Count > 5 ? valores[5].ToString() : null;
                        pruR.Tiempo7 = valores.Count > 6 ? valores[6].ToString() : null;
                        pruR.Tiempo8 = valores.Count > 7 ? valores[7].ToString() : null;
                        pruR.Tiempo9 = valores.Count > 8 ? valores[8].ToString() : null;
                        pruR.Tiempo10 = valores.Count > 9 ? valores[9].ToString() : null;
                        pruR.Tiempo11 = valores.Count > 10 ? valores[10].ToString() : null;
                        pruR.Tiempo12 = valores.Count > 11 ? valores[11].ToString() : null;
                        pruR.Tiempo13 = valores.Count > 12 ? valores[12].ToString() : null;
                        pruR.Tiempo14 = valores.Count > 13 ? valores[13].ToString() : null;
                        pruR.Tiempo15 = valores.Count > 14 ? valores[14].ToString() : null;
                        pruR.Tiempo16 = valores.Count > 15 ? valores[15].ToString() : null;
                        pruR.Tiempo17 = valores.Count > 16 ? valores[16].ToString() : null;
                        pruR.Tiempo18 = valores.Count > 17 ? valores[17].ToString() : null;
                        pruR.Tiempo19 = valores.Count > 18 ? valores[18].ToString() : null;
                        pruR.Tiempo20 = valores.Count > 19 ? valores[19].ToString() : null;
                        pruR.Tiempo21 = valores.Count > 20 ? valores[20].ToString() : null;
                        pruR.Tiempo22 = valores.Count > 21 ? valores[21].ToString() : null;
                        pruR.Tiempo23 = valores.Count > 22 ? valores[22].ToString() : null;
                        pruR.Tiempo24 = valores.Count > 23 ? valores[23].ToString() : null;
                        pruR.Tiempo25 = valores.Count > 24 ? valores[24].ToString() : null;
                        pruR.Tiempo26 = valores.Count > 25 ? valores[25].ToString() : null;
                        pruR.Tiempo27 = valores.Count > 26 ? valores[26].ToString() : null;
                        pruR.Tiempo28 = valores.Count > 27 ? valores[27].ToString() : null;
                        pruR.Tiempo29 = valores.Count > 28 ? valores[28].ToString() : null;
                        pruR.Tiempo30 = valores.Count > 29 ? valores[29].ToString() : null;

                        pruR.TipoEstimulo = color;
                        pruR.CantAdel = cantAdel.ToString();
                        pruR.TiempoMaximo = tmaximo.ToString();
                        pruR.TiempoMedio = Math.Round(media, 2).ToString();
                        pruR.TiempoMinimo = tminimo.ToString();
                        pruR.SumTiempo = Math.Round(sumatoria, 2).ToString();
                        pruR.CantEstimulos = valores.Count().ToString();
                        pruR.CantOmisiones = cantOmisiones.ToString();
                        pruR.CoefVariacion = desviacion.ToString();
                        pruR.DesvStandar = desvEstandart.ToString();
                        pruR.RespCorrecta = rcorrecta.ToString();

                        entities.PruTrsimple.Add(pruR);
                        entities.SaveChangesAsync();



                        var ultimo = entities.Set<PruTrsimple>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PTrsimple = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruTrsimple.Where(f => f.idTest == sujetoEva.PTrsimple).FirstOrDefault<PruTrsimple>();


                        conect.Fecha = date;

                        conect.TipoEstimulo = color;
                        conect.Tiempo1 = valores.Count > 0 ? valores[0].ToString() : null;
                        conect.Tiempo2 = valores.Count > 1 ? valores[1].ToString() : null;
                        conect.Tiempo3 = valores.Count > 2 ? valores[2].ToString() : null;
                        conect.Tiempo4 = valores.Count > 3 ? valores[3].ToString() : null;
                        conect.Tiempo5 = valores.Count > 4 ? valores[4].ToString() : null;
                        conect.Tiempo6 = valores.Count > 5 ? valores[5].ToString() : null;
                        conect.Tiempo7 = valores.Count > 6 ? valores[6].ToString() : null;
                        conect.Tiempo8 = valores.Count > 7 ? valores[7].ToString() : null;
                        conect.Tiempo9 = valores.Count > 8 ? valores[8].ToString() : null;
                        conect.Tiempo10 = valores.Count > 9 ? valores[9].ToString() : null;
                        conect.Tiempo11 = valores.Count > 10 ? valores[10].ToString() : null;
                        conect.Tiempo12 = valores.Count > 11 ? valores[11].ToString() : null;
                        conect.Tiempo13 = valores.Count > 12 ? valores[12].ToString() : null;
                        conect.Tiempo14 = valores.Count > 13 ? valores[13].ToString() : null;
                        conect.Tiempo15 = valores.Count > 14 ? valores[14].ToString() : null;
                        conect.Tiempo16 = valores.Count > 15 ? valores[15].ToString() : null;
                        conect.Tiempo17 = valores.Count > 16 ? valores[16].ToString() : null;
                        conect.Tiempo18 = valores.Count > 17 ? valores[17].ToString() : null;
                        conect.Tiempo19 = valores.Count > 18 ? valores[18].ToString() : null;
                        conect.Tiempo20 = valores.Count > 19 ? valores[19].ToString() : null;
                        conect.Tiempo21 = valores.Count > 20 ? valores[20].ToString() : null;
                        conect.Tiempo22 = valores.Count > 21 ? valores[21].ToString() : null;
                        conect.Tiempo23 = valores.Count > 22 ? valores[22].ToString() : null;
                        conect.Tiempo24 = valores.Count > 23 ? valores[23].ToString() : null;
                        conect.Tiempo25 = valores.Count > 24 ? valores[24].ToString() : null;
                        conect.Tiempo26 = valores.Count > 25 ? valores[25].ToString() : null;
                        conect.Tiempo27 = valores.Count > 26 ? valores[26].ToString() : null;
                        conect.Tiempo28 = valores.Count > 27 ? valores[27].ToString() : null;
                        conect.Tiempo29 = valores.Count > 28 ? valores[28].ToString() : null;
                        conect.Tiempo30 = valores.Count > 29 ? valores[29].ToString() : null;

                        conect.TiempoMaximo = tmaximo.ToString();
                        conect.TiempoMedio = Math.Round(media, 2).ToString();
                        conect.TiempoMinimo = tminimo.ToString();
                        conect.SumTiempo = Math.Round(sumatoria, 2).ToString();
                        conect.CantEstimulos = valores.Count().ToString();
                        conect.CantOmisiones = cantOmisiones.ToString();
                        conect.CoefVariacion = desviacion.ToString();
                        conect.DesvStandar = desvEstandart.ToString();
                        conect.RespCorrecta = rcorrecta.ToString();
                        conect.CantAdel = cantAdel.ToString();

                        entities.SaveChangesAsync();

                    }
                }
            }

            Application.DoEvents();
            Esperar te = new Esperar();
            te.Show();

            TRS_Form3 form = new TRS_Form3(valores, tmaximo, tminimo, sumatoria, rcorrecta, media, desvEstandart, desviacion, cantOmisiones, nombreAtleta, cantAdel, 0, color);
            te.Close();
            form.ShowDialog();


            this.Invoke(new Action(() => this.Close()));


        }

        private double desviacionEstandart(double media)
        {
            double cuadrado = 0;
            int count = 0;
            for (int i = 0; i < valores.Count; i++)
            {
                if (valores[i] != "Omisión")
                {
                    double temp = Convert.ToInt32(Convert.ToDouble(valores[i]));
                    if (temp > 0)
                    {
                        cuadrado += Math.Pow(temp - media, 2);
                        count++;
                    }
                }
            }

            ///* int count = //valo*/res.Count(x => x != "Omisión");
            double desvacion = cuadrado / count;
            desvacion = Math.Sqrt(desvacion);
            desvacion = Math.Round(desvacion, 2);
            return desvacion;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {


            if (myFtdiDevice.IsOpen)
            {

                myFtdiDevice.Purge(0);
                myFtdiDevice.Close();
            }


            if (flag)
            {
                //   arduinoPort.DiscardInBuffer();
                //   arduinoPort.Close();
                if (arduinoPort.IsOpen)
                {
                    arduinoPort.DiscardInBuffer();
                    arduinoPort.Close();
                }

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            contador++;
        }
    }
}
