using FTD2XX_NET;
using Multitest.AuxClass;
using Multitest.FormAux;
using Multitest.PanelesPrincipales;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO.Ports;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SQLite;
using Multitest.ADOmodel;



namespace Multitest
{
    public partial class Form1 : Form
    {

        const int WM_DEVICECHANGE = 537;
        const int DBT_DEVICEARRIVAL = 0x8000;
        const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        bool controlPrueba = false;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private bool conectadoArduino = false;
        private bool conectadoFTDI = false;
        private bool iniciando = false;
        bool res = true;
        

        public Form1()
        {

            Thread t = new Thread(new ThreadStart(Loading));
            t.Start();
            Thread.Sleep(1000);

            InitializeComponent();
            arduinoPort = new SerialPort();

            configurarHardware();

            t.Abort();

            this.Focus();
            this.Activate();

        }



        public void Loading()
        {
            try
            {
                System.Windows.Forms.Application.Run(new Splash());
            }
            catch (Exception)
            {

                throw;
            }


        }



        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            controlPrueba = false;
            Esperar d = new Esperar();
            d.Show();
            Application.DoEvents();
            button1.BackColor = Color.FromArgb(66, 94, 126);
            button2.BackColor = Color.FromArgb(55, 79, 105);
            button3.BackColor = Color.FromArgb(55, 79, 105);

            Application.DoEvents();

            label1.Text = button1.Text;
            panel3.Controls.Add(UserControlUser.Instance);
            UserControlUser.Instance.Dock = DockStyle.Fill;
            UserControlUser.Instance.BringToFront();
            UserControlUser.Instance.LimpiarTodosCampos();
            UserControlUser.Instance.LLenarTabla();
            ActiveControlUser.Instance.enPanelPrueba = false;
            Application.DoEvents();

            d.Close();
        }

        private void verificarArduino()
        {
            bool res = false;

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2",
          @"SELECT * FROM Win32_PnPEntity where DeviceID Like ""USB%"""))
                collection = searcher.Get();

            String arduinoID = "USB\\VID_1A86&PID_7523";


            String aux = null;

            foreach (var device in collection)
            {
                String temp = device.GetPropertyValue("DeviceID").ToString().Substring(0, 21);

                if (temp == arduinoID)
                {
                    res = true;
                    aux = temp;
                    conectadoArduino = true;
                }



            }



            //configurarPuerto();
            //if (arduinoPort.IsOpen)
            //{

            //    arduinoPort.DiscardInBuffer();

            //    arduinoPort.Write("6");
            //    bool resut = true;

            //    while (resut)
            //    {
            //        try
            //        {
            //            Thread.Sleep(200);
            //            byte[] data = new byte[1024];
            //            int bytesRead = arduinoPort.Read(data, 0, data.Length);
            //            var _message = Encoding.ASCII.GetString(data, 0, bytesRead);

            //            if (_message == "Work Test")
            //            {
            //                conectadoArduino = true;
            //            }
            //            resut = false;

            //        }
            //        catch (TimeoutException) { }
            //    }

            //    arduinoPort.Close();

            //}


        }



        private void configurarPuerto()
        {

            try
            {
                if (!arduinoPort.IsOpen)
                {

                    string[] puertosDisponibles = SerialPort.GetPortNames();
                    if (puertosDisponibles.Length > 0)
                    {
                        string port = puertosDisponibles[puertosDisponibles.Length - 1];
                        arduinoPort.PortName = port;
                        arduinoPort.BaudRate = 9600;
                        arduinoPort.Parity = Parity.None;
                        arduinoPort.DataBits = 8;
                        arduinoPort.Handshake = Handshake.None;
                        arduinoPort.StopBits = StopBits.One;
                        arduinoPort.RtsEnable = true;
                        //     arduinoPort.DataReceived += new SerialDataReceivedEventHandler(recibirArduino);
                        arduinoPort.Open();
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor desconecte y vuelva a conectar el equipo" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }


        private void button2_Click(object sender, EventArgs e)
        {

            VerificarHardware verificar = new VerificarHardware();
            bool res = verificar.configurarHardware();


            if (res == true)
            {
                controlPrueba = true;
                añadirPanelPrueba();
            }
            else
            {
                MessageBox.Show("El dispositivo debe estar conectado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        public void añadirPanelPrueba()
        {
            Esperar d = new Esperar();
            d.Show();
            Application.DoEvents();
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI" )
            {

                label1.Text = button2.Text;
                panel3.Controls.Add(UserControlPruebas.Instance);
                UserControlPruebas.Instance.Dock = DockStyle.Fill;
                UserControlPruebas.Instance.BringToFront();
               // UserControlPruebas.Instance.LimpiarTodosCampos();

                ActiveControlUser.Instance.enPanelPrueba = true;
                Application.DoEvents();


            }
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
            {


                label1.Text = button2.Text;
                panel3.Controls.Add(UserControlPruebas.Instance);
                UserControlPruebas.Instance.Dock = DockStyle.Fill;
                UserControlPruebas.Instance.BringToFront();
                UserControlPruebas.Instance.LimpiarTodosCampos();

                ActiveControlUser.Instance.enPanelPrueba = true;
                Application.DoEvents();


            }

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "No"   )
            {
                panel3.Controls.Add(DeshailitadoControl1.Instance);
                DeshailitadoControl1.Instance.Dock = DockStyle.Fill;
                DeshailitadoControl1.Instance.BringToFront();
                DeshailitadoControl1.Instance.PonerNombreArduino();

            }



            button2.BackColor = Color.FromArgb(66, 94, 126);
            button1.BackColor = Color.FromArgb(55, 79, 105);
            button3.BackColor = Color.FromArgb(55, 79, 105);


            d.Close();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            controlPrueba = false;
            Esperar d = new Esperar();
            d.Show();

            button3.BackColor = Color.FromArgb(66, 94, 126);
            button2.BackColor = Color.FromArgb(55, 79, 105);
            button1.BackColor = Color.FromArgb(55, 79, 105);


            Application.DoEvents();
            label1.Text = button3.Text;
            panel3.Controls.Add(UserControlVerPruebas.Instance);
            UserControlVerPruebas.Instance.Dock = DockStyle.Fill;
            UserControlVerPruebas.Instance.BringToFront();

            ActiveControlUser.Instance.enPanelPrueba = false;



            Application.DoEvents();

            d.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = button4.Text;
            ActiveControlUser.Instance.enPanelPrueba = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Point screenPoint = button6.PointToScreen(new Point(button6.Left, button6.Bottom));
            if (screenPoint.Y + contextMenuStrip1.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                contextMenuStrip1.Show(button6, new Point(0, -contextMenuStrip1.Size.Height));
            }
            else
            {
                contextMenuStrip1.Show(button6, new Point(0, button6.Height));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel3.Controls.Add(UserControlInicio.Instance);
            UserControlInicio.Instance.Dock = DockStyle.Fill;
            UserControlInicio.Instance.BringToFront();

           



        }




        private void fsdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoMultitest tipoMultitest = new TipoMultitest();
            tipoMultitest.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            controlPrueba = false;
            label1.Text = "Inicio";
            button3.BackColor = Color.FromArgb(55, 79, 105);
            button2.BackColor = Color.FromArgb(55, 79, 105);
            button1.BackColor = Color.FromArgb(55, 79, 105);
            Application.DoEvents();
            panel3.Controls.Add(UserControlInicio.Instance);
            UserControlInicio.Instance.Dock = DockStyle.Fill;
            UserControlInicio.Instance.BringToFront();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(152, 204, 211);


        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = SystemColors.ControlLight;
        }



        private void cambiarEtapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Esperar r = new Esperar();
            r.Show();
            Application.DoEvents();
            EstablecerEtapa eta = new EstablecerEtapa();
            eta.ShowDialog();
            r.Close();
        }

        private void crearNuevaEtapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Esperar r = new Esperar();
            r.Show();
            Application.DoEvents();
            Etapa f = new Etapa();
            f.ShowDialog();
            r.Close();
        }



        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDecs f = new AcercaDecs();
            f.ShowDialog();
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string user_system = Environment.UserName;
            string path = Application.StartupPath + @"\MultiTest.db";
            string c_user1 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string c_user = "C:/User/";
            string source = "/AppData/Local/Apps/2.0/R7C447C3.14P/KO532MKN.G4Z/mult...exe_0000000000000000_0001.0000_none_52040398fbeb07b7/MultiTest.db";
            string backup = "/Downloads/MultiTest.db";

            string sourceDir = c_user1 + source;
            string backupDir = c_user1 + backup;

                // Will overwrite if the destination file already exists.
                File.Copy(path, backupDir, true);
                MessageBox.Show("Los datos se han exportado correctamente en su carpeta de descargas", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(*.db, *.mdb)| *.db; *.mdb";
            
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();




            if (result == DialogResult.OK) // Test result.
            {

                string ext = Path.GetExtension(openFileDialog1.FileName);
                if (ext == ".db")
                {

                    string c_user1 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string c_user = "C:/User/";
                    string source = "/AppData/Local/Apps/2.0/R7C447C3.14P/KO532MKN.G4Z/mult...exe_0000000000000000_0001.0000_none_52040398fbeb07b7/MultiTest.db";
                    string backup = "/Downloads/MultiTest.db";
                    string path = Application.StartupPath + @"\MultiTest.db";
                    string sourceDir = c_user1 + source;
                    string backupDir = c_user1 + backup;

                    string newPath = c_user1 + source;
                    File.Copy(openFileDialog1.FileName, path, true);
                    MessageBox.Show("Los datos se han importado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                if (ext == ".mdb")
                {
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        string pathMdb = openFileDialog1.FileName;
                        Esperar d = new Esperar();
                        ImportarDatos(pathMdb);
                        // ImportarDatos();
                        MessageBox.Show("Los datos se han importado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al importar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           
        }

      

        public bool verificarFTDI()
        {
            FTDI myFtdiDevice = new FTDI();

            bool conectado = false;
            UInt32 ftdiDeviceCount = 0;

            FTDI.FT_STATUS ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

            ///lista de dispositivos ftdi
            FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

            //asigna la lista de dispositivos
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);
            if (ftdiDeviceList.Length != 0)
            {
                conectado = true;
                conectadoFTDI = true;
            }

            return conectado;

        }

        //protected override void WndProc(ref Message m)
        //{
        //    try
        //    {
        //        String d = m.WParam.ToInt32().ToString();
        //        if (m.WParam.ToInt32() == 7)
        //        {

        //            configurarHardware();
        //            if (controlPrueba && !ActiveControlUser.Instance.inTest)
        //            {
        //                añadirPanelPrueba();

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    base.WndProc(ref m);
        //}

        private void configurarHardware()
        {

            conectadoArduino = false;
            conectadoFTDI = false;


            verificarArduino();

            verificarFTDI();





            if (conectadoArduino)
            {
                String tipoMultitest = "Arduino";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = tipoMultitest;
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = "No";
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }

            if (conectadoFTDI)
            {
                String tipoMultitest = "FTDI";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = tipoMultitest;
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = "No";
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }

            if (!conectadoArduino && !conectadoFTDI)
            {
                String tipoMultitest = "FTDI";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = "No";
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }

        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {

        }

        //*--------------------------------------------------Importar Datos----------------------------------
        private /*async Task*/void ImportarDatos(string pathMdb)
        {
            bool x1 = DatosSujetoInsert(pathMdb);
            bool x2 = PruebasSujetoInsert(pathMdb);
            bool x3 = Pru16pfInsert(pathMdb);

            bool x4 = PruCatellInsert(pathMdb);
            bool x5 = PruCleaverInsert(pathMdb);
            bool x6 = PruDominoInsert(pathMdb);
            bool x7 = PruEysenkInsert(pathMdb);
            bool x8 = PruFHumanoInsert(pathMdb);
            bool x9 = PruIdareInsert(pathMdb);
            bool x10 = PruLoehrInsert(pathMdb);
            bool x11 = PruPomsInsert(pathMdb);
            bool x12 = PruRavenInsert(pathMdb);
            bool x13 = PruResantiInsert(pathMdb);
            bool x14 = PruTrcompleInsert(pathMdb);
            bool x15 = PruTrsimpleInsert(pathMdb);
            bool x16 = PruTRNInsert(pathMdb);
            bool x17 = PruWeilInsert(pathMdb);
            //bool x18 = SwitchboardItemsInsert(pathMdb);
            //bool x19 = TipoetapaInsert(pathMdb);
     

        }
        //*--------------------------------------------------Datos Sujeto-------------------------------------
        private bool DatosSujetoInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from DatosSujetos", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String carnet = reader["NCarnetIdent"].ToString();
                    String nombre = reader["NombreS"].ToString();
                    String apellido = reader["PrimerApellido"].ToString();
                    String apellido2 = reader["SegundoApellido"].ToString();
                    String sexo = reader["Sexo"].ToString();
                    String edad = reader["Edad"].ToString();
                    String deporte = reader["Ocupacion"].ToString();
                    String nivel = reader["NivelEscolar"].ToString();
                    String Entidad = reader["Entidad"].ToString();

                    DatosSujetos d = new DatosSujetos
                    {

                        NCarnetIdent = carnet,
                        NombreS = nombre,
                        PrimerApellido = apellido,
                        SegundoApellido = apellido2,
                        Sexo = sexo,
                        Edad = edad,
                        Ocupacion = deporte,
                        NivelEscolar = nivel,
                        Entidad = Entidad,

                    };



                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    DbDataReader redear2 = con2.ExecuteReader();

                    if (!redear2.HasRows)
                    {
                        db.DatosSujetos.Add(d);
                        db.SaveChanges();
                    }

                    c.Close();
                }

                reader.Close();

            }

                return insertado;
        }


        //---------------------Pruebas Sujetos-------------------------------------------------

        private bool PruebasSujetoInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from DatosSujetos", connectionAccess);
            DbDataReader redear = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                OleDbCommand conAcceSujetosEvaluados = new OleDbCommand("select * from SujetosEvaluados", connectionAccess);
                DbDataReader readarEvaluados = conAcceSujetosEvaluados.ExecuteReader();

                while (/*await redear.ReadAsync()*/readarEvaluados.Read())
                {

                    int idSujeto = Int32.Parse(readarEvaluados["idsujeto"].ToString());
                    String NCarnetIdentidad = readarEvaluados["NCarnetIdentidad"].ToString();
                    String Fecha = readarEvaluados["Fecha"].ToString();
                    String Etapa = readarEvaluados["Etapa"].ToString();
                    bool PIdare = Boolean.Parse(readarEvaluados["PIdare"].ToString());
                    bool P16pf = Boolean.Parse(readarEvaluados["P16pf"].ToString());
                    bool PEpi = Boolean.Parse(readarEvaluados["PEpi"].ToString());
                    bool PRaven = Boolean.Parse(readarEvaluados["PRaven"].ToString());
                    bool PDomino = Boolean.Parse(readarEvaluados["PDomino"].ToString());

                    bool PCleaver = Boolean.Parse(readarEvaluados["PCleaver"].ToString());
                    bool PFactorHum = Boolean.Parse(readarEvaluados["PFactorHum"].ToString());
                    bool PCatell = Boolean.Parse(readarEvaluados["PCatell"].ToString());
                    bool PWeil = Boolean.Parse(readarEvaluados["PWeil"].ToString());

                    bool PTrsimple = Boolean.Parse(readarEvaluados["PTrsimple"].ToString());
                    bool PTrcomple = Boolean.Parse(readarEvaluados["PTrcomple"].ToString());
                    bool PResanti = Boolean.Parse(readarEvaluados["PResanti"].ToString());
                    bool PTRN = Boolean.Parse(readarEvaluados["PTRN"].ToString());

                    bool PPoms = Boolean.Parse(readarEvaluados["PPoms"].ToString());
                    bool PLoehr = Boolean.Parse(readarEvaluados["PLoehr"].ToString());
                    bool PTrcomples = Boolean.Parse(readarEvaluados["PTrcomples"].ToString());

                    SujetosEvaluados evaluados = new SujetosEvaluados
                    {
                        idSujeto = idSujeto,
                        Fecha = Fecha,
                        Etapa = 1,
                        PIdareSitua = Convert.ToInt64(PIdare),
                        P16pf = Convert.ToInt64(P16pf),
                        PEpi = Convert.ToInt64(PEpi),
                        PRaven = Convert.ToInt64(PRaven),
                        PDomino = Convert.ToInt64(PDomino),
                        PCleaver = Convert.ToInt64(PCleaver),
                        PFactorHum = Convert.ToInt64(PFactorHum),
                        PCatell = Convert.ToInt64(PCatell),
                        PWeil = Convert.ToInt64(PWeil),
                        PTrsimple = Convert.ToInt64(PTrsimple),
                        PTrcomple = Convert.ToInt64(PTrcomple),
                        PTrcomples = Convert.ToInt64(PTrcomples),
                        PResanti = Convert.ToInt64(PResanti),
                        PTRN = Convert.ToInt64(PTRN),
                        PPoms = Convert.ToInt64(PPoms),
                        PLoehr = Convert.ToInt64(PLoehr),
                        PEysenk = 0,
                        PActiAnteComp = 0,
                        PCualidMotivDepor = 0,
                        PMotivDepButt = 0,
                        PIped = 0,
                        PIdetem = 0,
                        PCualiVolitiv = 0,
                        PIdareRasgo = 0,
                        PAnsiedadCompetitiva = 0,

                    };



                    SQLiteConnection Evaluados = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    Evaluados.Open();


                    db.SujetosEvaluados.Add(evaluados);
                    db.SaveChanges();

                    Evaluados.Close();
                }

                redear.Close();

            }
            return insertado;
        }

        //--------------------------------------Pru16pf------------------------------------
        private bool Pru16pfInsert(string pathMdb)
        {
            bool insert = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();
            OleDbCommand conAccePru16f = new OleDbCommand("select * from Pru16pf", connectionAccess);
            DbDataReader redearPru16f = conAccePru16f.ExecuteReader();
            using (mainEntities db1 = new mainEntities())
            {
                while (redearPru16f.Read())
                {

                    String idsujeto = redearPru16f["idsujeto"].ToString();
                    String DuraPru = redearPru16f["DuraPru"].ToString();
                    String AnotBrutaMD = redearPru16f["AnotBrutaMD"].ToString();
                    String AnotBrutaA = redearPru16f["AnotBrutaA"].ToString();
                    String AnotBrutaB = redearPru16f["AnotBrutaB"].ToString();
                    String AnotBrutaC = redearPru16f["AnotBrutaC"].ToString();
                    String AnotBrutaE = redearPru16f["AnotBrutaE"].ToString();
                    String AnotBrutaF = redearPru16f["AnotBrutaF"].ToString();
                    String AnotBrutaG = redearPru16f["AnotBrutaG"].ToString();
                    String AnotBrutaH = redearPru16f["AnotBrutaH"].ToString();
                    String AnotBrutaI = redearPru16f["AnotBrutaI"].ToString();
                    String AnotBrutaL = redearPru16f["AnotBrutaL"].ToString();
                    String AnotBrutaM = redearPru16f["AnotBrutaM"].ToString();
                    String AnotBrutaN = redearPru16f["AnotBrutaN"].ToString();
                    String AnotBrutaO = redearPru16f["AnotBrutaO"].ToString();
                    String AnotBrutaQ1 = redearPru16f["AnotBrutaQ1"].ToString();
                    String AnotBrutaQ2 = redearPru16f["AnotBrutaQ2"].ToString();
                    String AnotBrutaQ3 = redearPru16f["AnotBrutaQ3"].ToString();
                    String AnotBrutaQ4 = redearPru16f["AnotBrutaQ4"].ToString();
                    String AnotPesadaA = redearPru16f["AnotPesadaA"].ToString();
                    String AnotPesadaB = redearPru16f["AnotPesadaB"].ToString();
                    String AnotPesadaC = redearPru16f["AnotPesadaC"].ToString();
                    String AnotPesadaE = redearPru16f["AnotPesadaE"].ToString();
                    String AnotPesadaF = redearPru16f["AnotPesadaF"].ToString();
                    String AnotPesadaG = redearPru16f["AnotPesadaG"].ToString();
                    String AnotPesadaH = redearPru16f["AnotPesadaH"].ToString();
                    String AnotPesadaI = redearPru16f["AnotPesadaI"].ToString();
                    String AnotPesadaL = redearPru16f["AnotPesadaL"].ToString();
                    String AnotPesadaM = redearPru16f["AnotPesadaM"].ToString();
                    String AnotPesadaN = redearPru16f["AnotPesadaN"].ToString();
                    String AnotPesadaO = redearPru16f["AnotPesadaO"].ToString();
                    String AnotPesadaQ1 = redearPru16f["AnotPesadaQ1"].ToString();
                    String AnotPesadaQ2 = redearPru16f["AnotPesadaQ2"].ToString();
                    String AnotPesadaQ3 = redearPru16f["AnotPesadaQ3"].ToString();
                    String AnotPesadaQ4 = redearPru16f["AnotPesadaQ4"].ToString();
                    String Perfil1 = redearPru16f["Perfil1"].ToString();
                    String Perfil2 = redearPru16f["Perfil2"].ToString();
                    String Perfil3 = redearPru16f["Perfil3"].ToString();
                    String Perfil4 = redearPru16f["Perfil4"].ToString();

                    Pru16pf p = new Pru16pf
                    {

                        DuraPru = DuraPru,
                        AnotBrutaMD = AnotBrutaMD,
                        AnotBrutaA = AnotBrutaA,
                        AnotBrutaB = AnotBrutaB,
                        AnotBrutaC = AnotBrutaC,
                        AnotBrutaE = AnotBrutaE,
                        AnotBrutaF = AnotBrutaF,
                        AnotBrutaG = AnotBrutaG,
                        AnotBrutaH = AnotBrutaH,
                        AnotBrutaI = AnotBrutaI,
                        AnotBrutaL = AnotBrutaL,
                        AnotBrutaM = AnotBrutaM,
                        AnotBrutaN = AnotBrutaN,
                        AnotBrutaO = AnotBrutaO,
                        AnotBrutaQ1 = AnotBrutaQ1,
                        AnotBrutaQ2 = AnotBrutaQ2,
                        AnotBrutaQ3 = AnotBrutaQ3,
                        AnotBrutaQ4 = AnotBrutaQ4,
                        AnotPesadaA = AnotPesadaA,
                        AnotPesadaB = AnotPesadaB,
                        AnotPesadaC = AnotPesadaC,
                        AnotPesadaE = AnotPesadaE,
                        AnotPesadaF = AnotPesadaF,
                        AnotPesadaG = AnotPesadaG,
                        AnotPesadaH = AnotPesadaH,
                        AnotPesadaI = AnotPesadaI,
                        AnotPesadaL = AnotPesadaL,
                        AnotPesadaM = AnotPesadaM,
                        AnotPesadaN = AnotPesadaN,
                        AnotPesadaO = AnotPesadaO,
                        AnotPesadaQ1 = AnotPesadaQ1,
                        AnotPesadaQ2 = AnotPesadaQ2,
                        AnotPesadaQ3 = AnotPesadaQ3,
                        AnotPesadaQ4 = AnotPesadaQ4,
                        Perfil1 = Perfil1,
                        Perfil2 = Perfil2,
                        Perfil3 = Perfil3,
                        Perfil4 = Perfil4,
                        Fecha = "Na",
                        Neuroticismo = "Na",
                        Distorsion = "Na",
                    };




                    db1.Pru16pf.Add(p);

                    db1.SaveChanges();
                }
                redearPru16f.Close();
                connectionAccess.Close();

                
            }
            return insert;
        }

        //------------------------------------------------------------------------------


        //*--------------------------------------------------PruCatell------------------------
        private bool PruCatellInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruCatell", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String PBrutaLatQ3 = reader["PBrutaLatQ3"].ToString();
                    String PBrutaLatQ4 = reader["PBrutaLatQ4"].ToString();
                    String PBrutaLatO = reader["PBrutaLatO"].ToString();
                    String PBrutaLatL = reader["PBrutaLatL"].ToString();
                    String PBrutaLatC = reader["PBrutaLatC"].ToString();
                    String PBrutaEvidQ3 = reader["PBrutaEvidQ3"].ToString();
                    String PBrutaEvidQ4 = reader["PBrutaEvidQ4"].ToString();
                    String PBrutaEvidC = reader["PBrutaEvidC"].ToString();
                    String PBrutaEvidL = reader["PBrutaEvidL"].ToString();
                    String PBrutaEvidO = reader["PBrutaEvidO"].ToString();
                    String PBrutaTotal = reader["PBrutaTotal"].ToString();
                    String PBrutaLatEvidQ3 = reader["PBrutaLatEvidQ3"].ToString();
                    String PBrutaLatEvidC = reader["PBrutaLatEvidC"].ToString();
                    String PBrutaLatEvidL = reader["PBrutaLatEvidL"].ToString();
                    String PBrutaLatEvidO = reader["PBrutaLatEvidO"].ToString();
                    String PBrutaLatEvidQ4 = reader["PBrutaLatEvidQ4"].ToString();
                    String PStensLatQ3 = reader["PStensLatQ3"].ToString();
                    String PStensLatC = reader["PStensLatC"].ToString();
                    String PStensLatL = reader["PStensLatL"].ToString();
                    String PStensLatO = reader["PStensLatO"].ToString();
                    String PStensLatQ4 = reader["PStensLatQ4"].ToString();
                    String PStensEvidQ3 = reader["PStensEvidQ3"].ToString();
                    String PStensEvidC = reader["PStensEvidC"].ToString();
                    String PStensEvidL = reader["PStensEvidL"].ToString();
                    String PStensEvidO = reader["PStensEvidO"].ToString();
                    String PStensEvidQ4 = reader["PStensEvidQ4"].ToString();
                    String PStensLatEvidQ3 = reader["PStensLatEvidQ3"].ToString();
                    String PStensLatEvidC = reader["PStensLatEvidC"].ToString();
                    String PStensLatEvidL = reader["PStensLatEvidL"].ToString();
                    String PStensLatEvidO = reader["PStensLatEvidO"].ToString();
                    String PStensLatEvidQ4 = reader["PStensLatEvidQ4"].ToString();
                    String PStensTotal = reader["PStensTotal"].ToString();
                    String IntSico = reader["IntSico"].ToString();
                    String PuntTotalLE = reader["PuntTotalLE"].ToString();


                    PruCatell pc = new PruCatell
                    {
                        DuraPru = "-",
                        PBrutaLatQ3 = PBrutaLatQ3,
                        PBrutaLatQ4 = PBrutaLatQ4,
                        PBrutaLatO = PBrutaLatO,
                        PBrutaLatL = PBrutaLatL,
                        PBrutaLatC = PBrutaLatC,
                        PBrutaManQ3 = PBrutaEvidQ3,
                        PBrutaManQ4 = PBrutaEvidQ4,
                        PBrutaManC = PBrutaEvidC,
                        PBrutaManL = PBrutaEvidL,
                        PBrutaManO = PBrutaEvidO,
                        PBrutaLatManQ3 = PBrutaLatEvidQ3,
                        PBrutaLatManQ4 = PBrutaLatEvidQ4,
                        PBrutaLaManC = PBrutaLatEvidC,
                        PBrutaLatManL = PBrutaLatEvidL,
                        PBrutaLatManO = PBrutaLatEvidO,
                        PBrutaTotal = PBrutaTotal,
                        PStensLatQ3 = PStensLatQ3,
                        PStensLatC = PStensLatC,
                        PStensLatL = PStensLatL,
                        PStensLatO = PStensLatO,
                        PStensLatQ4 = PStensLatQ4,
                        PStensManQ3 = PStensEvidQ3,
                        PStensManC = PStensEvidC,
                        PStensManL = PStensEvidL,
                        PStensManO = PStensEvidO,
                        PStensManQ4 = PStensEvidQ4,
                        PStensLatManQ3 = PStensLatEvidQ3,
                        PStensLatManC = PStensLatEvidC,
                        PStensLatManL = PStensLatEvidL,
                        PStensLatManO = PStensLatEvidO,
                        PStensLatManQ4 = PStensLatEvidQ4,
                        PStensTotal = PStensTotal,
                        IntSico = IntSico,
                        PuntTotalLE = PuntTotalLE,
                        Fecha = "Na",


                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruCatell.Add(pc);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruCleaver------------------------
        private bool PruCleaverInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruCleaver", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeFDM = reader["PuntajeFDM"].ToString();
                    String PuntajeFIM = reader["PuntajeFIM"].ToString();
                    String PuntajeFSM = reader["PuntajeFSM"].ToString();
                    String PuntajeFCM = reader["PuntajeFCM"].ToString();
                    String PuntajeFDL = reader["PuntajeFDL"].ToString();
                    String PuntajeFIL = reader["PuntajeFIL"].ToString();
                    String PuntajeFSL = reader["PuntajeFSL"].ToString();
                    String PuntajeFCL = reader["PuntajeFCL"].ToString();
                    String PuntajeTD = reader["PuntajeTD"].ToString();
                    String PuntajeTI = reader["PuntajeTI"].ToString();
                    String PuntajeTS = reader["PuntajeTS"].ToString();
                    String PuntajeTC = reader["PuntajeTC"].ToString();
                    String Validez = reader["Validez"].ToString();


                    PruCleaver pc = new PruCleaver
                    {
                        DuraPru = "-",
                        PuntajeFDM = PuntajeFDM,
                        PuntajeFIM = PuntajeFIM,
                        PuntajeFSM = PuntajeFSM,
                        PuntajeFCM = PuntajeFCM,
                        PuntajeFDL = PuntajeFDL,
                        PuntajeFIL = PuntajeFIL,
                        PuntajeFSL = PuntajeFSL,
                        PuntajeFCL = PuntajeFCL,
                        PuntajeTD = PuntajeTD,
                        PuntajeTI = PuntajeTI,
                        PuntajeTS = PuntajeTS,
                        PuntajeTC = PuntajeTC,
                        Validez = Validez,
                        Fecha = "Na",


                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruCleaver.Add(pc);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruDomino------------------------
        private bool PruDominoInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruDomino", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String Puntaje = reader["Puntaje"].ToString();
                    String Porcentaje = reader["Porcentaje"].ToString();
                    String Rango = reader["Rango"].ToString();
                    String Diagnostico = reader["Diagnostico"].ToString();


                    PruDomino pd = new PruDomino
                    {
                        DuraPru = "-",
                        Puntaje = Puntaje,
                        Porcentaje = Porcentaje,
                        Rango = Rango,
                        Diagnostico = Diagnostico,
                        Fecha = "Na",


                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruDomino.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruEysenk------------------------
        private bool PruEysenkInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruEysenk", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeN = reader["PuntajeN"].ToString();
                    String PuntajeE = reader["PuntajeE"].ToString();
                    String PuntajeL = reader["PuntajeL"].ToString();
                    String Temperamento = reader["Temperamento"].ToString();
                    String Cualidad1 = reader["Cualidad1"].ToString();
                    String Cualidad2 = reader["Cualidad2"].ToString();
                    String Cualidad3 = reader["Cualidad3"].ToString();
                    String Cualidad4 = reader["Cualidad4"].ToString();
                    String Cualidad5 = reader["Cualidad5"].ToString();
                    String Cualidad6 = reader["Cualidad6"].ToString();
                    String Cualidad7 = reader["Cualidad7"].ToString();
                    String Cualidad8 = reader["Cualidad8"].ToString();
                    String Cualidad9 = reader["Cualidad9"].ToString();
                    String Diagnostico = reader["Diagnostico"].ToString();
                    String Diagnostico1 = reader["Diagnostico1"].ToString();


                    PruEysenk pd = new PruEysenk
                    {
                        DuraPru = "-",
                        DiagNeurotic = "Na",
                        DiagExtrove = "Na",
                        Neuroticismo = "Na",
                        Sinceridad = "Na",
                        DiagnosticoLetra = "Na",
                        DiagCuadrante = "Na",
                        Fecha = "Na",


                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruEysenk.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }



        //*--------------------------------------------------PruFHumano------------------------
        private bool PruFHumanoInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruFHumano", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeFDR = reader["PuntajeFDR"].ToString();
                    String PuntajeFIR = reader["PuntajeFIR"].ToString();
                    String PuntajeFSR = reader["PuntajeFSR"].ToString();
                    String PuntajeFCR = reader["PuntajeFCR"].ToString();
                    String A = reader["A"].ToString();
                    String PuntDifD = reader["PuntDifD"].ToString();
                    String PuntDifI = reader["PuntDifI"].ToString();
                    String PuntDifS = reader["PuntDifS"].ToString();
                    String PuntDifC = reader["PuntDifC"].ToString();
                    String PuntDifDPC = reader["PuntDifDPC"].ToString();
                    String PuntDifIPC = reader["PuntDifIPC"].ToString();
                    String PuntDifSPC = reader["PuntDifSPC"].ToString();
                    String PuntDifCPC = reader["PuntDifCPC"].ToString();
                    String PFinalD = reader["PFinalD"].ToString();
                    String PFinalI = reader["PFinalI"].ToString();
                    String PFinalS = reader["PFinalS"].ToString();
                    String PFinalC = reader["PFinalC"].ToString();
                    String PuntajeTR = reader["PuntajeTR"].ToString();


                    PruFHumano pd = new PruFHumano
                    {
                        DuraPru = "-",
                        PuntajeFDR = PuntajeFDR,
                        PuntajeFIR = PuntajeFIR,
                        PuntajeFSR = PuntajeFSR,
                        PuntajeFCR = PuntajeFCR,
                        A = A,
                        PuntDifD = PuntDifD,
                        PuntDifI = PuntDifI,
                        PuntDifS = PuntDifS,
                        PuntDifC = PuntDifC,
                        PuntDifDPC = PuntDifDPC,
                        PuntDifIPC = PuntDifIPC,
                        PuntDifSPC = PuntDifSPC,
                        PuntDifCPC = PuntDifCPC,
                        PFinalD = PFinalD,
                        PFinalI = PFinalI,
                        PFinalS = PFinalS,
                        PFinalC = PFinalC,
                        Fecha = "Na",


                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruFHumano.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }

        //*--------------------------------------------------PruIdare------------------------
        private bool PruIdareInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruIdare", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PAnsiedadReactiva = reader["PAnsiedadReactiva"].ToString();
                    String PAnsiedadRasgo = reader["PAnsiedadRasgo"].ToString();
                    String DiagAnsReactiva = reader["DiagAnsReactiva"].ToString();
                    String DiagAnsRasgo = reader["DiagAnsRasgo"].ToString();

                    PruIdareRago pd = new PruIdareRago
                    {
                        DuraPru = DuraPru,
                        PAnsiedadRasgo = PAnsiedadRasgo,
                        DiagAnsRasgo = DiagAnsRasgo,
                        Fecha = "Na",

                    };

                    PruIdareSituacional pi = new PruIdareSituacional
                    {
                        DuraPru = DuraPru,
                        PAnsiedadSituacional = PAnsiedadReactiva,
                        DiagAnsSituacional = DiagAnsReactiva,
                        Fecha = "Na",
                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruIdareRago.Add(pd);
                    db.PruIdareSituacional.Add(pi);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }

        //*--------------------------------------------------PruLoehr------------------------
        private bool PruLoehrInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruLoehr", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeFA = reader["PuntajeFA"].ToString();
                    String PuntajeFEN = reader["PuntajeFEN"].ToString();
                    String PuntajeFCA = reader["PuntajeFCA"].ToString();
                    String PuntajeFSV = reader["PuntajeFCV"].ToString();
                    String PuntajeFNM = reader["PuntajeFNM"].ToString();
                    String PuntajeFEP = reader["PuntajeFEP"].ToString();
                    String PuntajeFCACT = reader["PuntajeFCACT"].ToString();

                    PruLoehr pd = new PruLoehr
                    {
                        DuraPru = DuraPru,
                        PuntajeFA = PuntajeFA,
                        PuntajeFEN = PuntajeFEN,
                        PuntajeFCA = PuntajeFCA,
                        PuntajeFCV = PuntajeFSV,
                        PuntajeFNM = PuntajeFNM,
                        PuntajeFEP = PuntajeFEP,
                        PuntajeFCACT = PuntajeFCACT,
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruLoehr.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruPoms------------------------
        private bool PruPomsInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruPoms", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeFT = reader["PuntajeFT"].ToString();
                    String PuntajeFD = reader["PuntajeFD"].ToString();
                    String PuntajeFA = reader["PuntajeFA"].ToString();
                    String PuntajeFV = reader["PuntajeFV"].ToString();
                    String PuntajeFF = reader["PuntajeFF"].ToString();
                    String PuntajeFC = reader["PuntajeFC"].ToString();

                    PruPoms pd = new PruPoms
                    {
                        TensionAnsiedad = PuntajeFT,
                        DepresionMelancolia = PuntajeFD,
                        AngustiaHostilidad = PuntajeFA,
                        VigorActividad = PuntajeFV,
                        FatigaInercia = PuntajeFF,
                        ConfusionDesorient = PuntajeFC,
                        Amistosidad = "Na",
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruPoms.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }



        //*--------------------------------------------------PruRaven------------------------
        private bool PruRavenInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruRaven", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeTotal = reader["PuntajeC"].ToString();
                    String PuntajeA = reader["PuntajeA"].ToString();
                    String PuntajeB = reader["PuntajeB"].ToString();
                    String PuntajeC = reader["PuntajeC"].ToString();
                    String PuntajeD = reader["PuntajeD"].ToString();
                    String PuntajeE = reader["PuntajeE"].ToString();
                    String Porcentaje = reader["Porcentaje"].ToString();
                    String Rango = reader["PuntajeD"].ToString();
                    String Diagnostico = reader["PuntajeE"].ToString();

                    PruRaven pd = new PruRaven
                    {
                        DuraPru = DuraPru,
                        PuntajeTotal = PuntajeTotal,
                        PuntajeA = PuntajeA,
                        PuntajeB = PuntajeB,
                        PuntajeC = PuntajeC,
                        PuntajeD = PuntajeD,
                        PuntajeE = PuntajeE,
                        Porcentaje = Porcentaje,
                        Rango = Rango,
                        Diagnostico = Diagnostico,
                        Consistencia = "Na",
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruRaven.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruResanti------------------------
        private bool PruResantiInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruResanti", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String Programa = reader["Programa"].ToString();
                    String Manual = reader["Manual"].ToString();
                    String Aviso = reader["Aviso"].ToString();
                    String Media = reader["Media"].ToString();
                    String Mediana = reader["Mediana"].ToString();
                    String Moda = reader["Moda"].ToString();
                    String DesvStandar = reader["DesvStandar"].ToString();
                    String CoefVar = reader["CoefVar"].ToString();
                    String Calificacion = reader["Calificacion"].ToString();

                    String Media15 = reader["Media15"].ToString();
                    String Mediana15 = reader["Mediana15"].ToString();
                    String DesvStandar15 = reader["DesvStandar15"].ToString();
                    String CoefVar15 = reader["CoefVar15"].ToString();
                    String Media2 = reader["Media2"].ToString();
                    String Mediana2 = reader["Mediana2"].ToString();
                    String DesvStandar2 = reader["DesvStandar2"].ToString();
                    String CoefVar2 = reader["CoefVar2"].ToString();
                    String Media25 = reader["Media25"].ToString();
                    String Mediana25 = reader["Mediana25"].ToString();
                    String DesvStandar25 = reader["DesvStandar25"].ToString();
                    String CoefVar25 = reader["CoefVar25"].ToString();
                    String Media3 = reader["Media3"].ToString();
                    String Mediana3 = reader["Mediana3"].ToString();
                    String DesvStandar3 = reader["DesvStandar3"].ToString();
                    String CoefVar3 = reader["CoefVar3"].ToString();

                    String Num1 = reader["Num1"].ToString();
                    String Resultado1 = reader["Resultado1"].ToString();
                    String Demora1 = reader["Demora1"].ToString();
                    String Num2 = reader["Num2"].ToString();
                    String Resultado2 = reader["Resultado2"].ToString();
                    String Demora2 = reader["Demora2"].ToString();
                    String Num3 = reader["Num3"].ToString();
                    String Resultado3 = reader["Resultado3"].ToString();
                    String Demora3 = reader["Demora3"].ToString();
                    String Num4 = reader["Num4"].ToString();
                    String Resultado4 = reader["Resultado4"].ToString();
                    String Demora4 = reader["Demora4"].ToString();
                    String Num5 = reader["Num5"].ToString();
                    String Resultado5 = reader["Resultado5"].ToString();
                    String Demora5 = reader["Demora5"].ToString();
                    String Num6 = reader["Num6"].ToString();
                    String Resultado6 = reader["Resultado6"].ToString();
                    String Demora6 = reader["Demora6"].ToString();
                    String Num7 = reader["Num7"].ToString();
                    String Resultado7 = reader["Resultado7"].ToString();
                    String Demora7 = reader["Demora7"].ToString();
                    String Num8 = reader["Num8"].ToString();
                    String Resultado8 = reader["Resultado8"].ToString();
                    String Demora8 = reader["Demora8"].ToString();
                    String Num9 = reader["Num9"].ToString();
                    String Resultado9 = reader["Resultado9"].ToString();
                    String Demora9 = reader["Demora9"].ToString();
                    String Num10 = reader["Num10"].ToString();
                    String Resultado10 = reader["Resultado10"].ToString();
                    String Demora10 = reader["Demora10"].ToString();
                    String Num11 = reader["Num11"].ToString();
                    String Resultado11 = reader["Resultado11"].ToString();
                    String Demora11 = reader["Demora11"].ToString();
                    String Num12 = reader["Num12"].ToString();
                    String Resultado12 = reader["Resultado12"].ToString();
                    String Demora12 = reader["Demora12"].ToString();
                    String Num13 = reader["Num13"].ToString();
                    String Resultado13 = reader["Resultado13"].ToString();
                    String Demora13 = reader["Demora13"].ToString();
                    String Num14 = reader["Num14"].ToString();
                    String Resultado14 = reader["Resultado14"].ToString();
                    String Demora14 = reader["Demora14"].ToString();
                    String Num15 = reader["Num15"].ToString();
                    String Resultado15 = reader["Resultado15"].ToString();
                    String Demora15 = reader["Demora15"].ToString();
                    String Num16 = reader["Num16"].ToString();
                    String Resultado16 = reader["Resultado16"].ToString();
                    String Demora16 = reader["Demora16"].ToString();
                    String Num17 = reader["Num17"].ToString();
                    String Resultado17 = reader["Resultado17"].ToString();
                    String Demora17 = reader["Demora17"].ToString();
                    String Num18 = reader["Num18"].ToString();
                    String Resultado18 = reader["Resultado18"].ToString();
                    String Demora18 = reader["Demora18"].ToString();
                    String Num19 = reader["Num19"].ToString();
                    String Resultado19 = reader["Resultado19"].ToString();
                    String Demora19 = reader["Demora19"].ToString();
                    String Num20 = reader["Num20"].ToString();
                    String Resultado20 = reader["Resultado20"].ToString();
                    String Demora20 = reader["Demora20"].ToString();

                    PruResanti pd = new PruResanti
                    {
                        Programa = Programa,
                        Manual = Manual,
                        Moda = Moda,
                        Media = Media,
                        Mediana = Mediana,
                        DesvStandar = DesvStandar,
                        CoefVar = CoefVar,
                        Calificacion = Calificacion,

                        Media15 = Media15,
                        Moda15 = "Na",
                        Mediana15 = Mediana15,
                        DesvStandar15 = DesvStandar15,
                        CoefVar15 = CoefVar15,
                        Media2 = Media2,
                        Moda2 = "Na",
                        Mediana2 = Mediana2,
                        DesvStandar2 = DesvStandar2,
                        CoefVar2 = CoefVar2,
                        Media25 = Media25,
                        Mediana25 = Mediana25,
                        Moda25 = "Na",
                        DesvStandar25 = DesvStandar25,
                        CoefVar25 = CoefVar25,
                        Media3 = Media3,
                        Moda3 = "Na",
                        Mediana3 = Mediana3,
                        DesvStandar3 = DesvStandar3,
                        CoefVar3 = CoefVar3,


                        Tiempo1 = Num1,
                        Resultado1 = Resultado1,
                        VariantVelo1 = Demora1,
                        DiferenciaTiempo1 = "Na",
                        Tiempo2 = Num2,
                        Resultado2 = Resultado2,
                        VariantVelo2 = Demora2,
                        DiferenciaTiempo2 = "Na",
                        Tiempo3 = Num3,
                        Resultado3 = Resultado3,
                        VariantVelo3 = Demora3,
                        DiferenciaTiempo3 = "Na",
                        Tiempo4 = Num4,
                        Resultado4 = Resultado4,
                        VariantVelo4 = Demora4,
                        DiferenciaTiempo4 = "Na",
                        Tiempo5 = Num5,
                        Resultado5 = Resultado5,
                        VariantVelo5 = Demora5,
                        DiferenciaTiempo5 = "Na",
                        Tiempo6 = Num6,
                        Resultado6 = Resultado6,
                        VariantVelo6 = Demora6,
                        DiferenciaTiempo6 = "Na",
                        Tiempo7 = Num7,
                        Resultado7 = Resultado7,
                        VariantVelo7 = Demora7,
                        DiferenciaTiempo7 = "Na",
                        Tiempo8 = Num8,
                        Resultado8 = Resultado8,
                        VariantVelo8 = Demora8,
                        DiferenciaTiempo8 = "Na",
                        Tiempo9 = Num9,
                        Resultado9 = Resultado9,
                        VariantVelo9 = Demora9,
                        DiferenciaTiempo9 = "Na",
                        Tiempo10 = Num10,
                        Resultado10 = Resultado10,
                        VariantVelo10 = Demora10,
                        DiferenciaTiempo10 = "Na",
                        Tiempo11 = Num11,
                        Resultado11 = Resultado11,
                        VariantVelo11 = Demora11,
                        DiferenciaTiempo11 = "Na",
                        Tiempo12 = Num12,
                        Resultado12 = Resultado12,
                        VariantVelo12 = Demora12,
                        DiferenciaTiempo12 = "Na",
                        Tiempo13 = Num13,
                        Resultado13 = Resultado13,
                        VariantVelo13 = Demora13,
                        DiferenciaTiempo13 = "Na",
                        Tiempo14 = Num14,
                        Resultado14 = Resultado14,
                        VariantVelo14 = Demora14,
                        DiferenciaTiempo14 = "Na",
                        Tiempo15 = Num15,
                        Resultado15 = Resultado15,
                        VariantVelo15 = Demora15,
                        DiferenciaTiempo15 = "Na",
                        Tiempo16 = Num16,
                        Resultado16 = Resultado16,
                        VariantVelo16 = Demora16,
                        DiferenciaTiempo16 = "Na",
                        Tiempo17 = Num17,
                        Resultado17 = Resultado17,
                        VariantVelo17 = Demora17,
                        DiferenciaTiempo17 = "Na",
                        Tiempo18 = Num18,
                        Resultado18 = Resultado18,
                        VariantVelo18 = Demora18,
                        DiferenciaTiempo18 = "Na",
                        Tiempo19 = Num19,
                        Resultado19 = Resultado19,
                        VariantVelo19 = Demora19,
                        DiferenciaTiempo19 = "Na",
                        Tiempo20 = Num20,
                        Resultado20 = Resultado20,
                        VariantVelo20 = Demora20,
                        DiferenciaTiempo20 = "Na",
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruResanti.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruTrcomple------------------------
        private bool PruTrcompleInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruTrcomple", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String Sonido = reader["Sonido"].ToString();
                    String CantEstimulos = reader["CantEstimulos"].ToString();
                    String Variante = reader["Variante"].ToString();
                    String TiempoMaximo = reader["TiempoMaximo"].ToString();
                    String TiempoMinimo = reader["TiempoMinimo"].ToString();
                    String SumTiempo = reader["SumTiempo"].ToString();
                    String RespCorrecta = reader["RespCorrecta"].ToString();
                    String TiempoMedio = reader["TiempoMedio"].ToString();
                    String DesvStandar = reader["DesvStandar"].ToString();
                    String CoefVariacion = reader["CoefVariacion"].ToString();
                    String CantOmisiones = reader["CantOmisiones"].ToString();
                    String Canterrores = reader["Canterrores"].ToString();
                    String Canterroresson = reader["Canterroresson"].ToString();

                    String NumEst1 = reader["NumEst1"].ToString();
                    String Estimulo1 = reader["Estimulo1"].ToString();
                    String Respuesta1 = reader["Respuesta1"].ToString();
                    String Resultado1 = reader["Resultado1"].ToString();
                    String Esnumero1 = reader["Esnumero1"].ToString();
                    String Tiempo1 = reader["Tiempo1"].ToString();

                    String NumEst2 = reader["NumEst2"].ToString();
                    String Estimulo2 = reader["Estimulo2"].ToString();
                    String Respuesta2 = reader["Respuesta2"].ToString();
                    String Resultado2 = reader["Resultado2"].ToString();
                    String Esnumero2 = reader["Esnumero2"].ToString();
                    String Tiempo2 = reader["Tiempo2"].ToString();

                    String NumEst3 = reader["NumEst3"].ToString();
                    String Estimulo3 = reader["Estimulo3"].ToString();
                    String Respuesta3 = reader["Respuesta3"].ToString();
                    String Resultado3 = reader["Resultado3"].ToString();
                    String Esnumero3 = reader["Esnumero3"].ToString();
                    String Tiempo3 = reader["Tiempo3"].ToString();

                    String NumEst4 = reader["NumEst4"].ToString();
                    String Estimulo4 = reader["Estimulo4"].ToString();
                    String Respuesta4 = reader["Respuesta4"].ToString();
                    String Resultado4 = reader["Resultado4"].ToString();
                    String Esnumero4 = reader["Esnumero4"].ToString();
                    String Tiempo4 = reader["Tiempo4"].ToString();

                    String NumEst5 = reader["NumEst5"].ToString();
                    String Estimulo5 = reader["Estimulo5"].ToString();
                    String Respuesta5 = reader["Respuesta5"].ToString();
                    String Resultado5 = reader["Resultado5"].ToString();
                    String Esnumero5 = reader["Esnumero5"].ToString();
                    String Tiempo5 = reader["Tiempo5"].ToString();

                    String NumEst6 = reader["NumEst6"].ToString();
                    String Estimulo6 = reader["Estimulo6"].ToString();
                    String Respuesta6 = reader["Respuesta6"].ToString();
                    String Resultado6 = reader["Resultado6"].ToString();
                    String Esnumero6 = reader["Esnumero6"].ToString();
                    String Tiempo6 = reader["Tiempo6"].ToString();

                    String NumEst7 = reader["NumEst7"].ToString();
                    String Estimulo7 = reader["Estimulo7"].ToString();
                    String Respuesta7 = reader["Respuesta7"].ToString();
                    String Resultado7 = reader["Resultado7"].ToString();
                    String Esnumero7 = reader["Esnumero7"].ToString();
                    String Tiempo7 = reader["Tiempo7"].ToString();

                    String NumEst8 = reader["NumEst8"].ToString();
                    String Estimulo8 = reader["Estimulo8"].ToString();
                    String Respuesta8 = reader["Respuesta8"].ToString();
                    String Resultado8 = reader["Resultado8"].ToString();
                    String Esnumero8 = reader["Esnumero8"].ToString();
                    String Tiempo8 = reader["Tiempo8"].ToString();

                    String NumEst9 = reader["NumEst9"].ToString();
                    String Estimulo9 = reader["Estimulo9"].ToString();
                    String Respuesta9 = reader["Respuesta9"].ToString();
                    String Resultado9 = reader["Resultado9"].ToString();
                    String Esnumero9 = reader["Esnumero9"].ToString();
                    String Tiempo9 = reader["Tiempo9"].ToString();

                    String NumEst10 = reader["NumEst10"].ToString();
                    String Estimulo10 = reader["Estimulo10"].ToString();
                    String Respuesta10 = reader["Respuesta10"].ToString();
                    String Resultado10 = reader["Resultado10"].ToString();
                    String Esnumero10 = reader["Esnumero10"].ToString();
                    String Tiempo10 = reader["Tiempo10"].ToString();

                    String NumEst11 = reader["NumEst11"].ToString();
                    String Estimulo11 = reader["Estimulo11"].ToString();
                    String Respuesta11 = reader["Respuesta11"].ToString();
                    String Resultado11 = reader["Resultado11"].ToString();
                    String Esnumero11 = reader["Esnumero11"].ToString();
                    String Tiempo11 = reader["Tiempo11"].ToString();

                    String NumEst12 = reader["NumEst12"].ToString();
                    String Estimulo12 = reader["Estimulo12"].ToString();
                    String Respuesta12 = reader["Respuesta12"].ToString();
                    String Resultado12 = reader["Resultado12"].ToString();
                    String Esnumero12 = reader["Esnumero12"].ToString();
                    String Tiempo12 = reader["Tiempo12"].ToString();

                    String NumEst13 = reader["NumEst13"].ToString();
                    String Estimulo13 = reader["Estimulo13"].ToString();
                    String Respuesta13 = reader["Respuesta13"].ToString();
                    String Resultado13 = reader["Resultado13"].ToString();
                    String Esnumero13 = reader["Esnumero13"].ToString();
                    String Tiempo13 = reader["Tiempo13"].ToString();

                    String NumEst14 = reader["NumEst14"].ToString();
                    String Estimulo14 = reader["Estimulo14"].ToString();
                    String Respuesta14 = reader["Respuesta14"].ToString();
                    String Resultado14 = reader["Resultado14"].ToString();
                    String Esnumero14 = reader["Esnumero14"].ToString();
                    String Tiempo14 = reader["Tiempo14"].ToString();

                    String NumEst15 = reader["NumEst15"].ToString();
                    String Estimulo15 = reader["Estimulo15"].ToString();
                    String Respuesta15 = reader["Respuesta15"].ToString();
                    String Resultado15 = reader["Resultado15"].ToString();
                    String Esnumero15 = reader["Esnumero15"].ToString();
                    String Tiempo15 = reader["Tiempo15"].ToString();

                    String NumEst16 = reader["NumEst16"].ToString();
                    String Estimulo16 = reader["Estimulo16"].ToString();
                    String Respuesta16 = reader["Respuesta16"].ToString();
                    String Resultado16 = reader["Resultado16"].ToString();
                    String Esnumero16 = reader["Esnumero16"].ToString();
                    String Tiempo16 = reader["Tiempo16"].ToString();

                    String NumEst17 = reader["NumEst17"].ToString();
                    String Estimulo17 = reader["Estimulo17"].ToString();
                    String Respuesta17 = reader["Respuesta17"].ToString();
                    String Resultado17 = reader["Resultado17"].ToString();
                    String Esnumero17 = reader["Esnumero17"].ToString();
                    String Tiempo17 = reader["Tiempo17"].ToString();

                    String NumEst18 = reader["NumEst18"].ToString();
                    String Estimulo18 = reader["Estimulo18"].ToString();
                    String Respuesta18 = reader["Respuesta18"].ToString();
                    String Resultado18 = reader["Resultado18"].ToString();
                    String Esnumero18 = reader["Esnumero18"].ToString();
                    String Tiempo18 = reader["Tiempo18"].ToString();

                    String NumEst19 = reader["NumEst19"].ToString();
                    String Estimulo19 = reader["Estimulo19"].ToString();
                    String Respuesta19 = reader["Respuesta19"].ToString();
                    String Resultado19 = reader["Resultado19"].ToString();
                    String Esnumero19 = reader["Esnumero19"].ToString();
                    String Tiempo19 = reader["Tiempo19"].ToString();

                    String NumEst20 = reader["NumEst20"].ToString();
                    String Estimulo20 = reader["Estimulo20"].ToString();
                    String Respuesta20 = reader["Respuesta20"].ToString();
                    String Resultado20 = reader["Resultado20"].ToString();
                    String Esnumero20 = reader["Esnumero20"].ToString();
                    String Tiempo20 = reader["Tiempo20"].ToString();

                    String NumEst21 = reader["NumEst21"].ToString();
                    String Estimulo21 = reader["Estimulo21"].ToString();
                    String Respuesta21 = reader["Respuesta21"].ToString();
                    String Resultado21 = reader["Resultado21"].ToString();
                    String Esnumero21 = reader["Esnumero21"].ToString();
                    String Tiempo21 = reader["Tiempo21"].ToString();

                    String NumEst22 = reader["NumEst22"].ToString();
                    String Estimulo22 = reader["Estimulo22"].ToString();
                    String Respuesta22 = reader["Respuesta22"].ToString();
                    String Resultado22 = reader["Resultado22"].ToString();
                    String Esnumero22 = reader["Esnumero22"].ToString();
                    String Tiempo22 = reader["Tiempo22"].ToString();

                    String NumEst23 = reader["NumEst23"].ToString();
                    String Estimulo23 = reader["Estimulo23"].ToString();
                    String Respuesta23 = reader["Respuesta23"].ToString();
                    String Resultado23 = reader["Resultado23"].ToString();
                    String Esnumero23 = reader["Esnumero23"].ToString();
                    String Tiempo23 = reader["Tiempo23"].ToString();

                    String NumEst24 = reader["NumEst24"].ToString();
                    String Estimulo24 = reader["Estimulo24"].ToString();
                    String Respuesta24 = reader["Respuesta24"].ToString();
                    String Resultado24 = reader["Resultado24"].ToString();
                    String Esnumero24 = reader["Esnumero24"].ToString();
                    String Tiempo24 = reader["Tiempo24"].ToString();

                    String NumEst25 = reader["NumEst25"].ToString();
                    String Estimulo25 = reader["Estimulo25"].ToString();
                    String Respuesta25 = reader["Respuesta25"].ToString();
                    String Resultado25 = reader["Resultado25"].ToString();
                    String Esnumero25 = reader["Esnumero25"].ToString();
                    String Tiempo25 = reader["Tiempo25"].ToString();

                    String NumEst26 = reader["NumEst26"].ToString();
                    String Estimulo26 = reader["Estimulo26"].ToString();
                    String Respuesta26 = reader["Respuesta26"].ToString();
                    String Resultado26 = reader["Resultado26"].ToString();
                    String Esnumero26 = reader["Esnumero26"].ToString();
                    String Tiempo26 = reader["Tiempo26"].ToString();

                    String NumEst27 = reader["NumEst27"].ToString();
                    String Estimulo27 = reader["Estimulo27"].ToString();
                    String Respuesta27 = reader["Respuesta27"].ToString();
                    String Resultado27 = reader["Resultado27"].ToString();
                    String Esnumero27 = reader["Esnumero27"].ToString();
                    String Tiempo27 = reader["Tiempo27"].ToString();

                    String NumEst28 = reader["NumEst28"].ToString();
                    String Estimulo28 = reader["Estimulo28"].ToString();
                    String Respuesta28 = reader["Respuesta28"].ToString();
                    String Resultado28 = reader["Resultado28"].ToString();
                    String Esnumero28 = reader["Esnumero28"].ToString();
                    String Tiempo28 = reader["Tiempo28"].ToString();

                    String NumEst29 = reader["NumEst29"].ToString();
                    String Estimulo29 = reader["Estimulo29"].ToString();
                    String Respuesta29 = reader["Respuesta29"].ToString();
                    String Resultado29 = reader["Resultado29"].ToString();
                    String Esnumero29 = reader["Esnumero29"].ToString();
                    String Tiempo29 = reader["Tiempo29"].ToString();

                    String NumEst30 = reader["NumEst30"].ToString();
                    String Estimulo30 = reader["Estimulo30"].ToString();
                    String Respuesta30 = reader["Respuesta30"].ToString();
                    String Resultado30 = reader["Resultado30"].ToString();
                    String Esnumero30 = reader["Esnumero30"].ToString();
                    String Tiempo30 = reader["Tiempo30"].ToString();

                    PruTrcomple pd = new PruTrcomple
                    {
                        Sonido = Sonido,
                        CantEstimulos = CantEstimulos,
                        Variante = Variante,
                        TiempoMaximo = TiempoMaximo,
                        TiempoMinimo = TiempoMinimo,
                        SumTiempo = SumTiempo,
                        RespCorrecta = RespCorrecta,
                        TiempoMedio = TiempoMedio,
                        DesvStandar = DesvStandar,
                        CoefVariacion = CoefVariacion,
                        CantOmisiones = CantOmisiones,
                        Canterrores = Canterrores,
                        Canterroresson = Canterroresson,

                        Estimulo1 = Estimulo1,
                        Respuesta1 = Respuesta1,
                        Tiempo1 = Tiempo1,
                        Sonido1 = "Na",
                        Estimulo2 = Estimulo2,
                        Respuesta2 = Respuesta2,
                        Tiempo2 = Tiempo2,
                        Sonido2 = "Na",
                        Estimulo3 = Estimulo3,
                        Respuesta3 = Respuesta3,
                        Tiempo3 = Tiempo3,
                        Sonido3 = "Na",
                        Estimulo4 = Estimulo4,
                        Respuesta4 = Respuesta4,
                        Tiempo4 = Tiempo4,
                        Sonido4 = "Na",
                        Estimulo5 = Estimulo5,
                        Respuesta5 = Respuesta5,
                        Tiempo5 = Tiempo5,
                        Sonido5 = "Na",
                        Respuesta6 = Respuesta6,
                        Tiempo6 = Tiempo6,
                        Sonido6 = "Na",
                        Estimulo7 = Estimulo7,
                        Respuesta7 = Respuesta7,
                        Tiempo7 = Tiempo7,
                        Sonido7 = "Na",
                        Estimulo8 = Estimulo8,
                        Respuesta8 = Respuesta8,
                        Tiempo8 = Tiempo8,
                        Sonido8 = "Na",
                        Estimulo9 = Estimulo9,
                        Respuesta9 = Respuesta9,
                        Tiempo9 = Tiempo9,
                        Sonido9 = "Na",
                        Estimulo10 = Estimulo10,
                        Respuesta10 = Respuesta10,
                        Tiempo10 = Tiempo10,
                        Sonido10 = "Na",
                        Estimulo11 = Estimulo11,
                        Respuesta11 = Respuesta11,
                        Tiempo11 = Tiempo11,
                        Sonido11 = "Na",
                        Estimulo12 = Estimulo12,
                        Respuesta12 = Respuesta12,
                        Tiempo12 = Tiempo12,
                        Sonido12 = "Na",
                        Estimulo13 = Estimulo13,
                        Respuesta13 = Respuesta13,
                        Tiempo13 = Tiempo13,
                        Sonido13 = "Na",
                        Estimulo14 = Estimulo14,
                        Respuesta14 = Respuesta14,
                        Tiempo14 = Tiempo14,
                        Sonido14 = "Na",
                        Estimulo15 = Estimulo15,
                        Respuesta15 = Respuesta15,
                        Tiempo15 = Tiempo15,
                        Sonido15 = "Na",
                        Estimulo16 = Estimulo16,
                        Respuesta16 = Respuesta16,
                        Tiempo16 = Tiempo16,
                        Sonido16 = "Na",
                        Estimulo17 = Estimulo17,
                        Respuesta17 = Respuesta17,
                        Tiempo17 = Tiempo17,
                        Sonido17 = "Na",
                        Estimulo18 = Estimulo18,
                        Respuesta18 = Respuesta18,
                        Tiempo18 = Tiempo18,
                        Sonido18 = "Na",
                        Estimulo19 = Estimulo19,
                        Respuesta19 = Respuesta19,
                        Tiempo19 = Tiempo19,
                        Sonido19 = "Na",
                        Estimulo20 = Estimulo20,
                        Respuesta20 = Respuesta20,
                        Tiempo20 = Tiempo20,
                        Sonido20 = "Na",
                        Estimulo21 = Estimulo21,
                        Respuesta21 = Respuesta21,
                        Tiempo21 = Tiempo21,
                        Sonido21 = "Na",
                        Estimulo22 = Estimulo22,
                        Respuesta22 = Respuesta22,
                        Tiempo22 = Tiempo22,
                        Sonido22 = "Na",
                        Estimulo23 = Estimulo23,
                        Respuesta23 = Respuesta23,
                        Tiempo23 = Tiempo23,
                        Sonido23 = "Na",
                        Estimulo24 = Estimulo24,
                        Respuesta24 = Respuesta24,
                        Tiempo24 = Tiempo24,
                        Sonido24 = "Na",
                        Estimulo25 = Estimulo25,
                        Respuesta25 = Respuesta25,
                        Tiempo25 = Tiempo25,
                        Sonido25 = "Na",
                        Estimulo26 = Estimulo26,
                        Respuesta26 = Respuesta26,
                        Tiempo26 = Tiempo26,
                        Sonido26 = "Na",
                        Estimulo27 = Estimulo27,
                        Respuesta27 = Respuesta27,
                        Tiempo27 = Tiempo27,
                        Sonido27 = "Na",
                        Estimulo28 = Estimulo28,
                        Respuesta28 = Respuesta28,
                        Tiempo28 = Tiempo28,
                        Sonido28 = "Na",
                        Estimulo29 = Estimulo29,
                        Respuesta29 = Respuesta29,
                        Tiempo29 = Tiempo29,
                        Sonido29 = "Na",
                        Estimulo30 = Estimulo30,
                        Respuesta30 = Respuesta30,
                        Tiempo30 = Tiempo30,
                        Sonido30 = "Na",
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruTrcomple.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruTrsimple------------------------
        private bool PruTrsimpleInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruTrsimple", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String CantEstimulos = reader["CantEstimulos"].ToString();
                    
                    String TiempoMaximo = reader["TiempoMaximo"].ToString();
                    String TiempoMinimo = reader["TiempoMinimo"].ToString();
                    String SumTiempo = reader["SumTiempo"].ToString();
                    String RespCorrecta = reader["RespCorrecta"].ToString();
                    String TiempoMedio = reader["TiempoMedio"].ToString();
                    String DesvStandar = reader["DesvStandar"].ToString();
                    String CoefVariacion = reader["CoefVariacion"].ToString();
                    String CantOmisiones = reader["CantOmisiones"].ToString();

                    String NumEst1 = reader["NumEst1"].ToString();
                    String Resultado1 = reader["Resultado1"].ToString();
                    String Esnumero1 = reader["Esnumero1"].ToString();
                    String Tiempo1 = reader["Tiempo1"].ToString();

                    String NumEst2 = reader["NumEst2"].ToString();
                    String Resultado2 = reader["Resultado2"].ToString();
                    String Esnumero2 = reader["Esnumero2"].ToString();
                    String Tiempo2 = reader["Tiempo2"].ToString();

                    String NumEst3 = reader["NumEst3"].ToString();
                    String Resultado3 = reader["Resultado3"].ToString();
                    String Esnumero3 = reader["Esnumero3"].ToString();
                    String Tiempo3 = reader["Tiempo3"].ToString();

                    String NumEst4 = reader["NumEst4"].ToString();
                    String Resultado4 = reader["Resultado4"].ToString();
                    String Esnumero4 = reader["Esnumero4"].ToString();
                    String Tiempo4 = reader["Tiempo4"].ToString();

                    String NumEst5 = reader["NumEst5"].ToString();
                    String Resultado5 = reader["Resultado5"].ToString();
                    String Esnumero5 = reader["Esnumero5"].ToString();
                    String Tiempo5 = reader["Tiempo5"].ToString();

                    String NumEst6 = reader["NumEst6"].ToString();
                    String Resultado6 = reader["Resultado6"].ToString();
                    String Esnumero6 = reader["Esnumero6"].ToString();
                    String Tiempo6 = reader["Tiempo6"].ToString();

                    String NumEst7 = reader["NumEst7"].ToString();
                    String Resultado7 = reader["Resultado7"].ToString();
                    String Esnumero7 = reader["Esnumero7"].ToString();
                    String Tiempo7 = reader["Tiempo7"].ToString();

                    String NumEst8 = reader["NumEst8"].ToString();
                    String Resultado8 = reader["Resultado8"].ToString();
                    String Esnumero8 = reader["Esnumero8"].ToString();
                    String Tiempo8 = reader["Tiempo8"].ToString();

                    String NumEst9 = reader["NumEst9"].ToString();
                    String Resultado9 = reader["Resultado9"].ToString();
                    String Esnumero9 = reader["Esnumero9"].ToString();
                    String Tiempo9 = reader["Tiempo9"].ToString();

                    String NumEst10 = reader["NumEst10"].ToString();
                    String Resultado10 = reader["Resultado10"].ToString();
                    String Esnumero10 = reader["Esnumero10"].ToString();
                    String Tiempo10 = reader["Tiempo10"].ToString();

                    String NumEst11 = reader["NumEst11"].ToString();
                    String Resultado11 = reader["Resultado11"].ToString();
                    String Esnumero11 = reader["Esnumero11"].ToString();
                    String Tiempo11 = reader["Tiempo11"].ToString();

                    String NumEst12 = reader["NumEst12"].ToString();
                    String Resultado12 = reader["Resultado12"].ToString();
                    String Esnumero12 = reader["Esnumero12"].ToString();
                    String Tiempo12 = reader["Tiempo12"].ToString();

                    String NumEst13 = reader["NumEst13"].ToString();
                    String Resultado13 = reader["Resultado13"].ToString();
                    String Esnumero13 = reader["Esnumero13"].ToString();
                    String Tiempo13 = reader["Tiempo13"].ToString();

                    String NumEst14 = reader["NumEst14"].ToString();
                    String Resultado14 = reader["Resultado14"].ToString();
                    String Esnumero14 = reader["Esnumero14"].ToString();
                    String Tiempo14 = reader["Tiempo14"].ToString();

                    String NumEst15 = reader["NumEst15"].ToString();
                    String Resultado15 = reader["Resultado15"].ToString();
                    String Esnumero15 = reader["Esnumero15"].ToString();
                    String Tiempo15 = reader["Tiempo15"].ToString();

                    String NumEst16 = reader["NumEst16"].ToString();
                    String Resultado16 = reader["Resultado16"].ToString();
                    String Esnumero16 = reader["Esnumero16"].ToString();
                    String Tiempo16 = reader["Tiempo16"].ToString();

                    String NumEst17 = reader["NumEst17"].ToString();
                    String Resultado17 = reader["Resultado17"].ToString();
                    String Esnumero17 = reader["Esnumero17"].ToString();
                    String Tiempo17 = reader["Tiempo17"].ToString();

                    String NumEst18 = reader["NumEst18"].ToString();
                    String Resultado18 = reader["Resultado18"].ToString();
                    String Esnumero18 = reader["Esnumero18"].ToString();
                    String Tiempo18 = reader["Tiempo18"].ToString();

                    String NumEst19 = reader["NumEst19"].ToString();
                    String Resultado19 = reader["Resultado19"].ToString();
                    String Esnumero19 = reader["Esnumero19"].ToString();
                    String Tiempo19 = reader["Tiempo19"].ToString();

                    String NumEst20 = reader["NumEst20"].ToString();
                    String Resultado20 = reader["Resultado20"].ToString();
                    String Esnumero20 = reader["Esnumero20"].ToString();
                    String Tiempo20 = reader["Tiempo20"].ToString();

                    String NumEst21 = reader["NumEst21"].ToString();
                    String Resultado21 = reader["Resultado21"].ToString();
                    String Esnumero21 = reader["Esnumero21"].ToString();
                    String Tiempo21 = reader["Tiempo21"].ToString();

                    String NumEst22 = reader["NumEst22"].ToString();
                    String Resultado22 = reader["Resultado22"].ToString();
                    String Esnumero22 = reader["Esnumero22"].ToString();
                    String Tiempo22 = reader["Tiempo22"].ToString();

                    String NumEst23 = reader["NumEst23"].ToString();
                    String Resultado23 = reader["Resultado23"].ToString();
                    String Esnumero23 = reader["Esnumero23"].ToString();
                    String Tiempo23 = reader["Tiempo23"].ToString();

                    String NumEst24 = reader["NumEst24"].ToString();
                    String Resultado24 = reader["Resultado24"].ToString();
                    String Esnumero24 = reader["Esnumero24"].ToString();
                    String Tiempo24 = reader["Tiempo24"].ToString();

                    String NumEst25 = reader["NumEst25"].ToString();
                    String Resultado25 = reader["Resultado25"].ToString();
                    String Esnumero25 = reader["Esnumero25"].ToString();
                    String Tiempo25 = reader["Tiempo25"].ToString();

                    String NumEst26 = reader["NumEst26"].ToString();
                    String Resultado26 = reader["Resultado26"].ToString();
                    String Esnumero26 = reader["Esnumero26"].ToString();
                    String Tiempo26 = reader["Tiempo26"].ToString();

                    String NumEst27 = reader["NumEst27"].ToString();
                    String Resultado27 = reader["Resultado27"].ToString();
                    String Esnumero27 = reader["Esnumero27"].ToString();
                    String Tiempo27 = reader["Tiempo27"].ToString();

                    String NumEst28 = reader["NumEst28"].ToString();
                    String Resultado28 = reader["Resultado28"].ToString();
                    String Esnumero28 = reader["Esnumero28"].ToString();
                    String Tiempo28 = reader["Tiempo28"].ToString();

                    String NumEst29 = reader["NumEst29"].ToString();
                    String Resultado29 = reader["Resultado29"].ToString();
                    String Esnumero29 = reader["Esnumero29"].ToString();
                    String Tiempo29 = reader["Tiempo29"].ToString();

                    String NumEst30 = reader["NumEst30"].ToString();
                    String Resultado30 = reader["Resultado30"].ToString();
                    String Esnumero30 = reader["Esnumero30"].ToString();
                    String Tiempo30 = reader["Tiempo30"].ToString();

                    PruTrsimple pd = new PruTrsimple
                    {
                        CantEstimulos = CantEstimulos,
                        TiempoMaximo = TiempoMaximo,
                        TiempoMinimo = TiempoMinimo,
                        SumTiempo = SumTiempo,
                        RespCorrecta = RespCorrecta,
                        TiempoMedio = TiempoMedio,
                        DesvStandar = DesvStandar,
                        CoefVariacion = CoefVariacion,
                        CantOmisiones = CantOmisiones,

                        Tiempo1 = Tiempo1,
                        Tiempo2 = Tiempo2,
                        Tiempo3 = Tiempo3,
                        Tiempo4 = Tiempo4,
                        Tiempo5 = Tiempo5,
                        Tiempo6 = Tiempo6,
                        Tiempo7 = Tiempo7,
                        Tiempo8 = Tiempo8,
                        Tiempo9 = Tiempo9,
                        Tiempo10 = Tiempo10,
                        Tiempo11 = Tiempo11,
                        Tiempo12 = Tiempo12,
                        Tiempo13 = Tiempo13,
                        Tiempo14 = Tiempo14,
                        Tiempo15 = Tiempo15,
                        Tiempo16 = Tiempo16,
                        Tiempo17 = Tiempo17,
                        Tiempo18 = Tiempo18,
                        Tiempo19 = Tiempo19,
                        Tiempo20 = Tiempo20,
                        Tiempo21 = Tiempo21,
                        Tiempo22 = Tiempo22,
                        Tiempo23 = Tiempo23,
                        Tiempo24 = Tiempo24,
                        Tiempo25 = Tiempo25,
                        Tiempo26 = Tiempo26,
                        Tiempo27 = Tiempo27,
                        Tiempo28 = Tiempo28,
                        Tiempo29 = Tiempo29,
                        Tiempo30 = Tiempo30,
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruTrsimple.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruTRN------------------------
        private bool PruTRNInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruTRN", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String Variante = reader["Variante"].ToString();
                    String Calificacion = reader["Calificacion"].ToString();
                    String CantResp = reader["CantResp"].ToString();
                    String NRepeticiones = reader["NRepeticiones"].ToString();
                    String NSaltos = reader["NSaltos"].ToString();
                    String CdRect = reader["CdRect"].ToString();
                    String CdsRect = reader["CdsRect"].ToString();
                    String Cambcolor = reader["Cambcolor"].ToString();
                    String Duracion = reader["Duracion"].ToString();

                    String Resultado1 = reader["Resultado1"].ToString();
                    String Resultado2 = reader["Resultado2"].ToString();
                    String Resultado3 = reader["Resultado3"].ToString();
                    String Resultado4 = reader["Resultado4"].ToString();
                    String Resultado5 = reader["Resultado5"].ToString();
                    String Resultado6 = reader["Resultado6"].ToString();
                    String Resultado7 = reader["Resultado7"].ToString();
                    String Resultado8 = reader["Resultado8"].ToString();
                    String Resultado9 = reader["Resultado9"].ToString();
                    String Resultado10 = reader["Resultado10"].ToString();
                    String Resultado11 = reader["Resultado11"].ToString();
                    String Resultado12 = reader["Resultado12"].ToString();
                    String Resultado13 = reader["Resultado13"].ToString();
                    String Resultado14 = reader["Resultado14"].ToString();
                    String Resultado15 = reader["Resultado15"].ToString();
                    String Resultado16 = reader["Resultado16"].ToString();
                    String Resultado17 = reader["Resultado17"].ToString();
                    String Resultado18 = reader["Resultado18"].ToString();
                    String Resultado19 = reader["Resultado19"].ToString();
                    String Resultado20 = reader["Resultado20"].ToString();
                    String Resultado21 = reader["Resultado21"].ToString();
                    String Resultado22 = reader["Resultado22"].ToString();
                    String Resultado23 = reader["Resultado23"].ToString();
                    String Resultado24 = reader["Resultado24"].ToString();
                    String Resultado25 = reader["Resultado25"].ToString();
                    String Resultado26 = reader["Resultado26"].ToString();
                    String Resultado27 = reader["Resultado27"].ToString();
                    String Resultado28 = reader["Resultado28"].ToString();
                    String Resultado29 = reader["Resultado29"].ToString();
                    String Resultado30 = reader["Resultado30"].ToString();
                    String Resultado31 = reader["Resultado31"].ToString();
                    String Resultado32 = reader["Resultado32"].ToString();
                    String Resultado33 = reader["Resultado33"].ToString();
                    String Resultado34 = reader["Resultado34"].ToString();
                    String Resultado35 = reader["Resultado35"].ToString();
                    String Resultado36 = reader["Resultado36"].ToString();
                    String Resultado37 = reader["Resultado37"].ToString();
                    String Resultado38 = reader["Resultado38"].ToString();
                    String Resultado39 = reader["Resultado39"].ToString();
                    String Resultado40 = reader["Resultado40"].ToString();
                    String Resultado41 = reader["Resultado41"].ToString();
                    String Resultado42 = reader["Resultado42"].ToString();
                    String Resultado43 = reader["Resultado43"].ToString();
                    String Resultado44 = reader["Resultado44"].ToString();
                    String Resultado45 = reader["Resultado45"].ToString();
                    String Resultado46 = reader["Resultado46"].ToString();
                    String Resultado47 = reader["Resultado47"].ToString();
                    String Resultado48 = reader["Resultado48"].ToString();
                    String Resultado49 = reader["Resultado49"].ToString();

                    PruTRN pd = new PruTRN
                    {
                        Variante = Variante,
                        Calificacion = Calificacion,
                        CantResp = CantResp,
                        NRepeticiones = NRepeticiones,
                        NSaltos = NSaltos,
                        CdRect = CdRect,
                        CdNoRect = CdsRect,
                        Cambcolor = Cambcolor,
                        Duracion = Duracion,

                        Resultado1 = Resultado1,
                        Resultado2 = Resultado2,
                        Resultado3 = Resultado3,
                        Resultado4 = Resultado4,
                        Resultado5 = Resultado5,
                        Resultado6 = Resultado6,
                        Resultado7 = Resultado7,
                        Resultado8 = Resultado8,
                        Resultado9 = Resultado9,
                        Resultado10 = Resultado10,
                        Resultado11 = Resultado11,
                        Resultado12 = Resultado12,
                        Resultado13 = Resultado13,
                        Resultado14 = Resultado14,
                        Resultado15 = Resultado15,
                        Resultado16 = Resultado16,
                        Resultado17 = Resultado17,
                        Resultado18 = Resultado18,
                        Resultado19 = Resultado19,
                        Resultado20 = Resultado20,
                        Resultado21 = Resultado21,
                        Resultado22 = Resultado22,
                        Resultado23 = Resultado23,
                        Resultado24 = Resultado24,
                        Resultado25 = Resultado25,
                        Resultado26 = Resultado26,
                        Resultado27 = Resultado27,
                        Resultado28 = Resultado28,
                        Resultado29 = Resultado29,
                        Resultado30 = Resultado30,
                        Resultado31 = Resultado31,
                        Resultado32 = Resultado32,
                        Resultado33 = Resultado33,
                        Resultado34 = Resultado34,
                        Resultado35 = Resultado35,
                        Resultado36 = Resultado36,
                        Resultado37 = Resultado37,
                        Resultado38 = Resultado38,
                        Resultado39 = Resultado39,
                        Resultado40 = Resultado40,
                        Resultado41 = Resultado41,
                        Resultado42 = Resultado42,
                        Resultado43 = Resultado43,
                        Resultado44 = Resultado44,
                        Resultado45 = Resultado45,
                        Resultado46 = Resultado46,
                        Resultado47 = Resultado47,
                        Resultado48 = Resultado48,
                        Resultado49 = Resultado49,
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruTRN.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------PruWeil------------------------
        private bool PruWeilInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from PruWeil", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String idSujeto = reader["idSujeto"].ToString();
                    String DuraPru = reader["DuraPru"].ToString();
                    String PuntajeTotal = reader["PuntajeTotal"].ToString();
                    String Porcentaje = reader["Porcentaje"].ToString();
                    String Rango = reader["Rango"].ToString();
                    String Diagnostico = reader["Diagnostico"].ToString();

                    PruWeil pd = new PruWeil
                    {
                        DuraPru = DuraPru,
                        PuntajeTotal = PuntajeTotal,
                        Porcentaje = Porcentaje,
                        Rango = Rango,
                        Diagnostico = Diagnostico,
                        Fecha = "Na",

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.PruWeil.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }




        //*--------------------------------------------------Switchboard Items------------------------
        private bool SwitchboardItemsInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from Switchboard Items", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    int ItemNumber = Int32.Parse(reader["ItemNumber"].ToString());
                    String ItemText = reader["ItemText"].ToString();
                    int Command = Int32.Parse(reader["Command"].ToString());
                    String Argument = reader["Argument"].ToString();

                    Switchboard_Items pd = new Switchboard_Items
                    {
                        ItemNumber = ItemNumber,
                        ItemText = ItemText,
                        Command = Command,
                        Argument = Argument,

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.Switchboard_Items.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }


        //*--------------------------------------------------Tipoetapa------------------------
        private bool TipoetapaInsert(string pathMdb)
        {
            bool insertado = false;
            OleDbConnection connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            connectionAccess.Open();

            OleDbCommand conAcce = new OleDbCommand("select * from Tipoetapa", connectionAccess);
            DbDataReader reader = conAcce.ExecuteReader();
            using (mainEntities db = new mainEntities())
            {

                while (/*await redear.ReadAsync()*/reader.Read())
                {

                    String Etapa = reader["Etapa"].ToString();

                    Tipoetapa pd = new Tipoetapa
                    {
                        Etapa = Etapa,

                    };

                    SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString);
                    c.Open();
                    // SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where NCarnetIdent  =" + carnet, c);
                    // DbDataReader redear2 = con2.ExecuteReader();

                    db.Tipoetapa.Add(pd);
                    db.SaveChanges();

                    c.Close();
                }

                reader.Close();

            }

            return insertado;
        }



        //------------------------------------------------------------------------------
        private bool verificarSujeto(DatosSujetos res, DatosSujetos d)
        {
            bool result = true;

            if (res.NombreS == d.NombreS && res.PrimerApellido == d.PrimerApellido && res.SegundoApellido == d.SegundoApellido && res.Sexo == d.Sexo && res.Entidad == d.Entidad && res.Ocupacion == d.Ocupacion &&
            res.Edad == d.Edad &&
            res.NivelEscolar == d.NivelEscolar)

                result = false;


            return result;
        }

    }

    
}
