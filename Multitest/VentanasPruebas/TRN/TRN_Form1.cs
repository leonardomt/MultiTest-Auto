using FTD2XX_NET;
using Multitest.FormAux;
using Multitest.PanelesPrincipales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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
    public partial class TRN_Form1 : Form
    {

        bool conectado = false;
        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice;
        List<String> listTodo = new List<String>();
        string[] valoresTodos = new string[] { "1N", "24R", "2N", "23R", "3N", "22R", "4N", "21R", "5N", "20R", "6N",
                                          "19R", "7N", "18R", "8N", "17R", "9N", "16R", "10N", "15R", "11N", "14R",
                                          "12N", "13R", "13N", "12R", "14N", "11R", "15N", "10R", "16N", "9R", "17N",
                                          "8R", "18N", "7R", "19N", "6R", "20N", "5R", "21N", "4R", "22N", "3R", "23N",
                                          "2R", "24N", "1R", "25N" };


        bool finTRN = false;

        List<int> valoresN = new List<int>();
        List<int> valoresR = new List<int>();

        int ultimoValorR = 25;
        int ultimoValorN = 0;
        bool primeroNegro = false;
        bool primeroRojo = false;
        int count = 1;
        int countX = 1;
        bool timerFlag = false;
        string tiempoTotalTest = null;
        int saltos = 0;
        int repetidos = 0;
        int cambioColor = 0;
        bool borrarflag = false;
        Stopwatch time = null;
        String direccionNegra = "Ascendente";
        String direccionRojo = "Descendente";

        int countR = 0;
        int countN = 0;

        int saltoN = 0;

        int color = 0;
        int tecla = 0;

        int cambioDireccion = 0;
        int cambioDireccionRec = 0;
        String idPerson;
        String idEtapa;
        String nombreAtleta;

        ConectionFTDI ftdi;



        public TRN_Form1(String idPerson, String idEtapa, String nombreAtleta)
        {

            InitializeComponent();
            time = new Stopwatch();
            this.idPerson = idPerson;
            this.nombreAtleta = nombreAtleta;
            this.idEtapa = idEtapa;
            ftdi = new ConectionFTDI();
            myFtdiDevice = new FTDI();
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
                    MessageBox.Show("Conecte el equipo a la PC para poder realizar la prueba.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conectado = false;
                    this.Close();
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conectado = false;
                this.Close();
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




            foreach (var device in collection)
            {
                String temp = device.GetPropertyValue("DeviceID").ToString().Substring(0, 21);
                if (temp == arduinoID)
                    res = true;

            }
            return res;
        }

        private void recibirArduino(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string entradaDatos = sp.ReadExisting();

                if (entradaDatos != "Tabla-RN" && entradaDatos != "FIN_TRN")
                {
                    if (entradaDatos != "Delete")
                    {
                        if (!timerFlag)
                        {
                            time.Start();
                            timerFlag = true;
                        }

                        if (count == 6)
                        {
                            borrarflag = true;
                        }



                        listTodo.Add(entradaDatos);
                        string text = "n" + count;

                        Label lab = this.Controls.Find(text, true).FirstOrDefault() as Label;

                        lab.Invoke(new Action(() => lab.Visible = true));
                        lab.Invoke(new Action(() => lab.Text = entradaDatos));


                        string temp = entradaDatos.Substring(entradaDatos.Length - 1, 1);

                        //  string temp2 = valoresN[count - 1].Substring(valoresN[count - 1].Length - 1, 1);


                        int num1 = Convert.ToInt32(entradaDatos.Substring(0, entradaDatos.Length - 1));




                        if (count % 2 != 0)
                        {
                            if (temp == "N")
                            {
                                valoresN.Add(num1);

                                if (direccionNegra == "Ascendente")
                                {

                                    if (ultimoValorN != num1)
                                    {

                                        if ((ultimoValorN + 1) != num1)
                                        {
                                            if ((ultimoValorN + 1) < num1 && primeroNegro)
                                            {
                                                saltos++;

                                                if (countN != 0)
                                                    countN = 0;
                                            }
                                            else
                                            {

                                                countN++;
                                                if (countN == 2)
                                                {
                                                    direccionNegra = "Descendente";

                                                    countN = 0;
                                                }


                                            }

                                        }
                                        else
                                          if (countN != 0)
                                            countN = 0;
                                    }


                                }
                                else
                                {
                                    if (ultimoValorN != num1)
                                    {
                                        if (num1 > (ultimoValorN + 1) && primeroNegro)
                                        {
                                            saltos++;
                                        }

                                        if (num1 >= (ultimoValorN + 1))
                                        {
                                            countN++;
                                            if (countN == 2)
                                            {
                                                direccionNegra = "Ascendente";
                                                cambioDireccionRec++;
                                                countN = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (countN != 0)
                                                countN = 0;
                                        }

                                    }
                                }
                                ultimoValorN = num1;
                                if (!primeroNegro)
                                {
                                    primeroNegro = true;
                                }
                            }
                            else
                            {
                                cambioColor++;
                            }
                        }
                        else
                        {
                            if (temp == "R")
                            {
                                valoresR.Add(num1);

                                if (direccionRojo == "Descendente")
                                {

                                    if (ultimoValorR != num1)
                                    {

                                        if ((ultimoValorR - 1) != num1)
                                        {
                                            if ((ultimoValorR - 1) > num1 && primeroRojo)
                                            {
                                                saltos++;

                                                if (countR != 0)
                                                    countR = 0;
                                            }
                                            else
                                            {

                                                countR++;
                                                if (countR == 2)
                                                {
                                                    direccionRojo = "Ascendente";

                                                    countR = 0;
                                                }


                                            }

                                        }
                                        else
                                          if (countR != 0)
                                            countR = 0;
                                    }


                                }
                                else
                                {
                                    if (ultimoValorR != num1)
                                    {
                                        if ((ultimoValorR - 1) > num1 && primeroRojo)
                                        {
                                            saltos++;
                                        }

                                        if ((ultimoValorR - 1) >= num1)
                                        {
                                            countR++;
                                            if (countR == 2)
                                            {
                                                direccionRojo = "Descendente";
                                                cambioDireccionRec++;
                                                countR = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (countR != 0)
                                                countR = 0;
                                        }

                                    }
                                }
                                ultimoValorR = num1;

                                if (!primeroRojo)
                                {
                                    primeroRojo = true;
                                }
                            }
                            else
                            {
                                cambioColor++;
                            }

                        }








                        count++;

                        if (count % 2 != 0)
                        {
                            string textX = "X" + countX;
                            Label indicieA = this.Controls.Find(textX, true).FirstOrDefault() as Label;

                            indicieA.Invoke(new Action(() => indicieA.Visible = false));


                            countX++;


                            if (countX <= 25)
                            {
                                textX = "X" + countX;
                                Label indicie = this.Controls.Find(textX, true).FirstOrDefault() as Label;
                                indicie.Invoke(new Action(() => indicie.Visible = true));
                            }



                        }

                    }
                    else
                    {
                        if (!borrarflag)
                        {
                            count--;
                            listTodo.RemoveAt(count - 1);
                            string text = "n" + count;

                            Label lab = this.Controls.Find(text, true).FirstOrDefault() as Label;
                            lab.Invoke(new Action(() => lab.Visible = false));
                            lab.Invoke(new Action(() => lab.Text = ""));




                            if (count % 2 == 0)
                            {
                                string textX = "X" + countX;
                                Label indicieA = this.Controls.Find(textX, true).FirstOrDefault() as Label;

                                indicieA.Invoke(new Action(() => indicieA.Visible = false));


                                countX--;

                                textX = "X" + countX;
                                Label indicie = this.Controls.Find(textX, true).FirstOrDefault() as Label;
                                indicie.Invoke(new Action(() => indicie.Visible = true));
                            }

                        }

                    }


                    if (count == 50)
                    {
                        if (direccionRojo == "Ascendente")
                        {
                            cambioDireccion++;
                        }

                        if (direccionNegra == "Descendente")
                        {
                            cambioDireccion++;
                        }

                        time.Stop();


                        arduinoPort.Write("9");



                    }


                }
                else
                {
                    if (entradaDatos == "FIN_TRN")
                    {
                        tiempoTotalTest = time.Elapsed.TotalMinutes.ToString();
                        String tiempoTotalTestSecond = time.Elapsed.TotalSeconds.ToString();
                        calcularRepetidos();

                        Esperar r = new Esperar();
                        r.Show();
                        TRN_Form2 form = new TRN_Form2(listTodo, tiempoTotalTest, tiempoTotalTestSecond, repetidos, saltos, cambioColor, cambioDireccionRec, cambioDireccion, idPerson, idEtapa, nombreAtleta);
                        r.Close();
                        form.ShowDialog();
                        this.Invoke(new Action(() => this.Close()));


                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conectado = false;
                this.Close();
            }


        }


        public void calcularRepetidos()
        {

            foreach (var item in valoresN.GroupBy(x => x))
            {
                int tem = item.Count();
                if (tem > 1)
                    repetidos += (tem - 1);
            }

            foreach (var item in valoresR.GroupBy(x => x))
            {
                int tem = item.Count();
                if (tem > 1)
                    repetidos += (tem - 1);
            }


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
                arduinoPort.Write(Convert.ToString(9));
                arduinoPort.DiscardInBuffer();

                arduinoPort.Close();

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {



            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {

                openPorFTDI();
                conectado = true;

            }
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
            {
                configurarPuerto();
                conectado = true;
            }

            comenzarPrueba();



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

        public void comenzarPrueba()
        {

            if (conectado)
            {
                TRN_Form3 form = new TRN_Form3();

                DialogResult res = form.ShowDialog();
                if (form.comenzar)
                {
                    if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                    {

                        sendCommandFTDI("9");
                        Thread.Sleep(100);
                        string fin = readFirstDataFromFTDI1ByteInicial();
                        if (fin == "f" || fin == "x")
                        {
                            myFtdiDevice.Purge(0);

                        }


                        sendCommandFTDI();
                       
                        Thread.Sleep(200);
                      

                        string prueba = readFirstDataFromFTDI1Byte();
                        

                        

                        if (prueba == "1")
                        {
                            recibirFTDI();
                        }
                        else
                            MessageBox.Show("El sistema no entro a la prueba");
                    }
                    if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                    {
                        arduinoPort.Write("1");
                    }

                }
                else
                    this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void sendCommandFTDI()
        {

            if (myFtdiDevice.IsOpen)
            {
                // byte[] r = Encoding.ASCII.GetBytes("e");

                byte[] r = new byte[1];
                r[0] = 1;

                UInt32 numBytesWritten = 1;
                ftStatus = myFtdiDevice.Write(r, r.Length, ref numBytesWritten);


            }
        }

        public void recibirFTDI()
        {
            try
            {

                //++++++++++++++++++++++Recibo del FTDI+++++++++++++++++++++++
                if (!finTRN)
                {

                    string entradaDatos = readFromFTDI1Byte(2);

                    if (entradaDatos != "Tabla-RN" && entradaDatos != "FIN_TRN")
                    {
                        if (entradaDatos != "Delete")
                        {
                            if (!timerFlag)
                            {
                                time.Start();
                                timerFlag = true;
                            }

                            if (count == 6)
                            {
                                borrarflag = true;
                            }


                            listTodo.Add(entradaDatos);
                            string text = "n" + count;

                            Label lab = this.Controls.Find(text, true).FirstOrDefault() as Label;

                            lab.Invoke(new Action(() => lab.Visible = true));
                            lab.Invoke(new Action(() => lab.Text = entradaDatos));


                            string temp = entradaDatos.Substring(entradaDatos.Length - 1, 1);

                            //  string temp2 = valoresN[count - 1].Substring(valoresN[count - 1].Length - 1, 1);


                            int num1 = Convert.ToInt32(entradaDatos.Substring(0, entradaDatos.Length - 1));




                            if (count % 2 != 0)
                            {
                                if (temp == "N")
                                {
                                    valoresN.Add(num1);

                                    if (direccionNegra == "Ascendente")
                                    {

                                        if (ultimoValorN != num1)
                                        {

                                            if ((ultimoValorN + 1) != num1)
                                            {
                                                if ((ultimoValorN + 1) < num1 && primeroNegro)
                                                {
                                                    saltos++;

                                                    if (countN != 0)
                                                        countN = 0;
                                                }
                                                else
                                                {

                                                    countN++;
                                                    if (countN == 2)
                                                    {
                                                        direccionNegra = "Descendente";

                                                        countN = 0;
                                                    }


                                                }

                                            }
                                            else
                                              if (countN != 0)
                                                countN = 0;
                                        }


                                    }
                                    else
                                    {
                                        if (ultimoValorN != num1)
                                        {
                                            if (num1 > (ultimoValorN + 1) && primeroNegro)
                                            {
                                                saltos++;
                                            }

                                            if (num1 >= (ultimoValorN + 1))
                                            {
                                                countN++;
                                                if (countN == 2)
                                                {
                                                    direccionNegra = "Ascendente";
                                                    cambioDireccionRec++;
                                                    countN = 0;
                                                }
                                            }
                                            else
                                            {
                                                if (countN != 0)
                                                    countN = 0;
                                            }

                                        }
                                    }
                                    ultimoValorN = num1;
                                    if (!primeroNegro)
                                    {
                                        primeroNegro = true;
                                    }
                                }
                                else
                                {
                                    cambioColor++;
                                }
                            }
                            else
                            {
                                if (temp == "R")
                                {
                                    valoresR.Add(num1);

                                    if (direccionRojo == "Descendente")
                                    {

                                        if (ultimoValorR != num1)
                                        {

                                            if ((ultimoValorR - 1) != num1)
                                            {
                                                if ((ultimoValorR - 1) > num1 && primeroRojo)
                                                {
                                                    saltos++;

                                                    if (countR != 0)
                                                        countR = 0;
                                                }
                                                else
                                                {

                                                    countR++;
                                                    if (countR == 2)
                                                    {
                                                        direccionRojo = "Ascendente";

                                                        countR = 0;
                                                    }


                                                }

                                            }
                                            else
                                              if (countR != 0)
                                                countR = 0;
                                        }


                                    }
                                    else
                                    {
                                        if (ultimoValorR != num1)
                                        {
                                            if ((ultimoValorR - 1) > num1 && primeroRojo)
                                            {
                                                saltos++;
                                            }

                                            if ((ultimoValorR - 1) >= num1)
                                            {
                                                countR++;
                                                if (countR == 2)
                                                {
                                                    direccionRojo = "Descendente";
                                                    cambioDireccionRec++;
                                                    countR = 0;
                                                }
                                            }
                                            else
                                            {
                                                if (countR != 0)
                                                    countR = 0;
                                            }

                                        }
                                    }
                                    ultimoValorR = num1;

                                    if (!primeroRojo)
                                    {
                                        primeroRojo = true;
                                    }
                                }
                                else
                                {
                                    cambioColor++;
                                }

                            }



                            count++;

                            if (count % 2 != 0)
                            {
                                string textX = "X" + countX;
                                Label indicieA = this.Controls.Find(textX, true).FirstOrDefault() as Label;

                                indicieA.Invoke(new Action(() => indicieA.Visible = false));


                                countX++;


                                if (countX <= 25)
                                {
                                    textX = "X" + countX;
                                    Label indicie = this.Controls.Find(textX, true).FirstOrDefault() as Label;
                                    indicie.Invoke(new Action(() => indicie.Visible = true));
                                }



                            }

                        }
                        else
                        {
                            if (!borrarflag)
                            {
                                count--;
                                listTodo.RemoveAt(count - 1);
                                string text = "n" + count;

                                Label lab = this.Controls.Find(text, true).FirstOrDefault() as Label;
                                lab.Invoke(new Action(() => lab.Visible = false));
                                lab.Invoke(new Action(() => lab.Text = ""));




                                if (count % 2 == 0)
                                {
                                    string textX = "X" + countX;
                                    Label indicieA = this.Controls.Find(textX, true).FirstOrDefault() as Label;

                                    indicieA.Invoke(new Action(() => indicieA.Visible = false));


                                    countX--;

                                    textX = "X" + countX;
                                    Label indicie = this.Controls.Find(textX, true).FirstOrDefault() as Label;
                                    indicie.Invoke(new Action(() => indicie.Visible = true));
                                }

                            }

                        }


                        if (count == 50)
                        {
                            if (direccionRojo == "Ascendente")
                            {
                                cambioDireccion++;
                            }

                            if (direccionNegra == "Descendente")
                            {
                                cambioDireccion++;
                            }

                            time.Stop();


                            //++++++++++++++Manda el valor 9 para fin de prueba+++++++++++++++++++
                            sendCommandFTDI("9");
                           
                            Thread.Sleep(100);
                            

                            String fin = readFromFTDI1Byte(2);
                            if (fin == "FIN TRN")
                                finTRN = true;

                            //++++++++++++++Manda el valor 9 para fin de prueba+++++++++++++++++++


                            //myFtdiDevice.Close();

                        }


                    }


                    recibirFTDI();
                }
                else
                {
                    if (finTRN == true)
                    {

                        tiempoTotalTest = time.Elapsed.TotalMinutes.ToString();
                        String tiempoTotalTestSecond = time.Elapsed.TotalSeconds.ToString();
                        calcularRepetidos();
                        Esperar t = new Esperar();
                        t.Show();
                        TRN_Form2 form = new TRN_Form2(listTodo, tiempoTotalTest, tiempoTotalTestSecond, repetidos, saltos, cambioColor, cambioDireccionRec, cambioDireccion, idPerson, idEtapa, nombreAtleta);
                        t.Close();
                        form.ShowDialog();
                        this.Invoke(new Action(() => this.Close()));


                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conectado = false;
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


            while (numBytesAvailable < numBytesExpected)
            {

                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                if (ftStatus != FTDI.FT_STATUS.FT_OK)

                    throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);

            }

            if (numBytesAvailable != numBytesExpected)
                throw new Exception("Error: Invalid data in buffer. (1350)");


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


            entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();

            tecla = Convert.ToInt32(rawData[0]);
            color = Convert.ToInt32(rawData[1]);

            entradaDatos = convertirValor(tecla, color);



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


            if (numBytesAvailable != numBytesExpected)
            {
                throw new Exception("Error: Invalid data in buffer. (1350)");
            }


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);

            return rawData[0].ToString();
        }

        private string readFirstDataFromFTDI1ByteInicial()
        {
            string entradaDatos = "x";

            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = 2;
            myFtdiDevice.Purge(0);
            string temp = "";


         

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

            if (rawData[0].ToString() == "0")
            {
                entradaDatos = "x";
            }
            else
                entradaDatos = rawData[0].ToString();

            return entradaDatos;
        }

        private string convertirValor(int tecla, int color)
        {
            string temp = "";
            if (color == 114)
            {
                temp = tecla.ToString() + "R";
            }
            if (color == 98)
            {
                temp = tecla.ToString() + "N";
            }
            if (color == 100)
            {
                temp = "Delete";
            }
            if (color == 102)
            {
                temp = "FIN TRN";
            }





            return temp;
        }


        private void sendCommandFTDI(String valor)
        {

            if (myFtdiDevice.IsOpen)
            {

                myFtdiDevice.Purge(0);
                byte[] r = null;
                if (valor == "4" || valor == "3" || valor == "9")
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

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
