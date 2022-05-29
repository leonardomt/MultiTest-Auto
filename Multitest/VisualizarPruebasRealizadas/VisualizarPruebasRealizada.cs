using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using Microsoft.Office.Interop.Word;
using Multitest.ADOmodel;
using System.Diagnostics;
using Multitest.FormAux;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class VisualizarPruebasRealizada : Form
    {
        string prueba;
        string fecha;
        String nombreAtleta;
        String edad;
        String deporte;
        String modalidad;
        int idAtleta;
        string etapa;
        String idATestTodo;
        String idTest;
        public VisualizarPruebasRealizada(String prueba, String nombreAtleta, String idPrueba, string fecha, string idAtleta, String idATestTodo)
        {
            InitializeComponent();
            fecha = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy");
            this.fecha = fecha;
            this.nombreAtleta = nombreAtleta;
            this.idAtleta = Convert.ToInt32(idAtleta);
            this.prueba = prueba;
            this.idATestTodo = idATestTodo;
            this.idTest = idPrueba;


            if (prueba == "Raven")
            {
                this.Text = "Test de Raven";
                panel3.Controls.Add(RavenView.Instance);
                RavenView.Instance.Dock = DockStyle.Fill;
                RavenView.Instance.BringToFront();
                RavenView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                RavenView.Instance.buscarPrueba(idPrueba);


            }


            if (prueba == "Eysenck")
            {
                this.Text = "Test de Personalidad Eysenck (EPI)";
                panel3.Controls.Add(EysenckView.Instance);
                EysenckView.Instance.Dock = DockStyle.Fill;
                EysenckView.Instance.BringToFront();
                EysenckView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                EysenckView.Instance.buscarPrueba(idPrueba);

            }



            if (prueba == "Weil")
            {
                this.Text = "Test de Weil";
                panel3.Controls.Add(WeilView.Instance);
                WeilView.Instance.Dock = DockStyle.Fill;
                WeilView.Instance.BringToFront();
                WeilView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                WeilView.Instance.buscarPrueba(idPrueba);
            }

            if (prueba == "Dominó")
            {
                this.Text = "Test de Dominó";
                panel3.Controls.Add(DominoView.Instance);
                DominoView.Instance.Dock = DockStyle.Fill;
                DominoView.Instance.BringToFront();
                DominoView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                DominoView.Instance.buscarPrueba(idPrueba);
            }


            if (prueba == "Idare (Rasgo)")
            {
                this.Text = "Test Idare (Rasgo)";
                panel3.Controls.Add(IdareView.Instance);
                IdareView.Instance.Dock = DockStyle.Fill;
                IdareView.Instance.BringToFront();
                IdareView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                IdareView.Instance.buscarPrueba(idPrueba, "Rasgo");

            }


            if (prueba == "Idare (Situacional)")
            {
                this.Text = "Test Idare (Situacional)";
                panel3.Controls.Add(IdareView.Instance);
                IdareView.Instance.Dock = DockStyle.Fill;
                IdareView.Instance.BringToFront();
                IdareView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                IdareView.Instance.buscarPrueba(idPrueba, "Situacional");

            }



            if (prueba == "Catell")
            {
                this.Text = "Test de Catell";
                panel3.Controls.Add(CatellView.Instance);
                CatellView.Instance.Dock = DockStyle.Fill;
                CatellView.Instance.BringToFront();
                CatellView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                CatellView.Instance.buscarPrueba(idPrueba);

            }
            if (prueba == "Ansiedad Precompetitiva CSAI-2R")
            {
                this.Text = "Test de Ansiedad Precompetitiva CSAI-2R";
                panel3.Controls.Add(AnsiedadPrecomView.Instance);
                AnsiedadPrecomView.Instance.Dock = DockStyle.Fill;
                AnsiedadPrecomView.Instance.BringToFront();
                AnsiedadPrecomView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                AnsiedadPrecomView.Instance.buscarPrueba(idPrueba);
            }






            if (prueba == "Cualidades Volitivas")
            {
                this.Text = "Cualidades Volitivas en el Deporte";
                panel3.Controls.Add(CualidadesVolitivasView.Instance);
                CualidadesVolitivasView.Instance.Dock = DockStyle.Fill;
                CualidadesVolitivasView.Instance.BringToFront();
                CualidadesVolitivasView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                CualidadesVolitivasView.Instance.buscarPrueba(idPrueba);
            }

            if (prueba == "Butt")
            {
                this.Text = "Test de Motivos de Butt";
                panel3.Controls.Add(ButtView.Instance);
                ButtView.Instance.Dock = DockStyle.Fill;
                ButtView.Instance.BringToFront();
                ButtView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                ButtView.Instance.buscarPrueba(idPrueba);
            }


            if (prueba == "Cualidades Motivacionales")
            {
                this.Text = "Test de Cualidades Motivacionales en el Deporte";
                panel3.Controls.Add(CualidadesMotivacionalesView.Instance);
                CualidadesMotivacionalesView.Instance.Dock = DockStyle.Fill;
                CualidadesMotivacionalesView.Instance.BringToFront();
                CualidadesMotivacionalesView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                CualidadesMotivacionalesView.Instance.buscarPrueba(idPrueba);
            }



            if (prueba == "Actitud Ante la Competencia")
            {
                this.Text = "Test de Actitud Ante la Competencia en el Deporte";
                panel3.Controls.Add(ActitudCompetenciaView.Instance);
                ActitudCompetenciaView.Instance.Dock = DockStyle.Fill;
                ActitudCompetenciaView.Instance.BringToFront();
                ActitudCompetenciaView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                ActitudCompetenciaView.Instance.buscarPrueba(idPrueba);
            }


            if (prueba == "TRS")
            {
                this.Text = "Tiempo de Reacción Simple";
                panel3.Controls.Add(TRSView.Instance);
                TRSView.Instance.Dock = DockStyle.Fill;
                TRSView.Instance.BringToFront();
                TRSView.Instance.cambiarNombreAtleta(nombreAtleta);
            }



            if (prueba == "TRC")
            {
                this.Text = "Tiempo de Reacción Complejo";
                panel3.Controls.Add(TRCView.Instance);
                TRCView.Instance.Dock = DockStyle.Fill;
                TRCView.Instance.BringToFront();
                TRCView.Instance.cambiarNombreAtleta(nombreAtleta);
            }


            if (prueba == "TRCS")
            {
                this.Text = "Tiempo de Reacción Complejo con Sonido";
                panel3.Controls.Add(TRCSView.Instance);
                TRCSView.Instance.Dock = DockStyle.Fill;
                TRCSView.Instance.BringToFront();
                TRCSView.Instance.cambiarNombreAtleta(nombreAtleta);
            }


            if (prueba == "Tiempo de Respuesta Anticipada")
            {
                this.Text = "Tiempo de Respuesta Anticipada";
                panel3.Controls.Add(ResptAnticipadaView.Instance);
                ResptAnticipadaView.Instance.Dock = DockStyle.Fill;
                ResptAnticipadaView.Instance.BringToFront();
                ResptAnticipadaView.Instance.cambiarNombreAtleta(nombreAtleta);
            }


            if (prueba == "Martens")
            {
                this.Text = "Test de Ansiedad Precompetitiva de Martens";
                panel3.Controls.Add(AnsiedadPrecomView.Instance);
                AnsiedadPrecomView.Instance.Dock = DockStyle.Fill;
                AnsiedadPrecomView.Instance.BringToFront();
                AnsiedadPrecomView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                AnsiedadPrecomView.Instance.buscarPrueba(idPrueba);
            }

            if (prueba == "Idetem")
            {
                this.Text = "Inventario para la determinación del temperamento (IDETEM-1)";
                panel3.Controls.Add(IdetemView.Instance);
                IdetemView.Instance.Dock = DockStyle.Fill;
                IdetemView.Instance.BringToFront();
                IdetemView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                IdetemView.Instance.buscarPrueba(idPrueba);
            }

            if (prueba == "Iped")
            {
                this.Text = "Inventario Psicológico de Ejecución Deportiva IPED";
                panel3.Controls.Add(IpedView.Instance);
                IpedView.Instance.Dock = DockStyle.Fill;
                IpedView.Instance.BringToFront();
                IpedView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                IpedView.Instance.buscarPrueba(idPrueba);
            }


            if (prueba == "POMS")
            {
                this.Text = "POMS";
                panel3.Controls.Add(PomsView.Instance);
                PomsView.Instance.Dock = DockStyle.Fill;
                PomsView.Instance.BringToFront();
                PomsView.Instance.cambiarNombreAtleta(nombreAtleta, fecha);
                PomsView.Instance.buscarPrueba(idPrueba);
            }



        }

        private void buscarAtleta()
        {
            using (mainEntities entities = new mainEntities())
            {
                var atleta = entities.DatosSujetos.Find(idAtleta);
                modalidad = atleta.Ocupacion;
                deporte = atleta.Entidad;
                edad = atleta.Edad;
            }
        }


        public void buscarEtapa()
        {

            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from Tipoetapa  where Actual ='1'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                etapa = res["Etapa"].ToString();

                            }
                        }
                    }
                }

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VisualizarPruebasRealizada_Load(object sender, EventArgs e)
        {
            Screen d = Screen.PrimaryScreen;
            int with = d.Bounds.Width;
            int height = d.Bounds.Height;
            label1.Text = "Test de " + prueba;

            simpleButton1.Visible = ActiveControlUser.Instance.enPanelPrueba ? false : true;
            button1.Visible = ActiveControlUser.Instance.enPanelPrueba ? false : true;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            buscarAtleta();
            buscarEtapa();

            if (prueba == "Raven")
            {
                exportarRaven();
            }

            if (prueba == "Weil")
            {
                exportarWeil();
            }
            if (prueba == "Dominó")
            {
                exportarDomino();
            }
            if (prueba == "Butt")
            {
                exportarButt();
            }
            if (prueba == "Idare (Situacional)")
            {
                exportarIdareSit();
            }
            if (prueba == "Idare (Rasgo)")
            {
                exportarIdareRasg();
            }
            if (prueba == "Catell")
            {
                exportarCatell();
            }

            if (prueba == "Eysenck")
            {

                exportarEysenck();
            }

            if (prueba == "Cualidades Volitivas")
            {
                //REVISAR AUN
                exportarCualidadesVolitiv();
            }

            if (prueba == "Actitud Ante la Competencia")
            {

                exportarActitudAnteCompe();
            }
            if (prueba == "Cualidades Motivacionales")
            {

                exportarCualidadesMotiv();
            }

            if (prueba == "Idetem")
            {

                exportarIdetem();
            }

            if (prueba == "Iped")
            {
                exportarIped();
            }


            if (prueba == "Ansiedad Precompetitiva CSAI-2R")
            {
                exportarAnsiedadPreComp();
            }

            if (prueba == "POMS")
            {
                exportarPOMS();
            }


        }

        private void exportarPOMS()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
            {

                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Poms.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " POMS";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {

                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();

                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;


                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;


                        //---------------------------------------------------------//

                        PruPoms pru = PomsView.Instance.poms;


                        Bookmark Q3L = wordDoc.Bookmarks["TS"];
                        Range A = Q3L.Range;
                        A.Text = " " + pru.TensionAnsiedad;

                        Bookmark CL = wordDoc.Bookmarks["DM"];
                        Range B = CL.Range;
                        B.Text = " " + pru.DepresionMelancolia;

                        Bookmark LL = wordDoc.Bookmarks["AH"];
                        Range C = LL.Range;
                        C.Text = " " + pru.AngustiaHostilidad;


                        Bookmark LL1 = wordDoc.Bookmarks["VA"];
                        Range C1 = LL1.Range;
                        C1.Text = " " + pru.VigorActividad;



                        Bookmark LL2 = wordDoc.Bookmarks["FI"];
                        Range C2 = LL2.Range;
                        C2.Text = " " + pru.FatigaInercia;



                        Bookmark LL3 = wordDoc.Bookmarks["CD"];
                        Range C3 = LL3.Range;
                        C3.Text = " " + pru.ConfusionDesorient;


                        Bookmark LL4 = wordDoc.Bookmarks["A"];
                        Range C4 = LL4.Range;
                        C4.Text = " " + pru.Amistosidad;



                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();
                        esp.Close();

                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarAnsiedadPreComp()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
            {

                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Ansiedad Pre Competitiva.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " Ansiedad Pre Competitiva";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {

                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();

                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;


                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;


                        //---------------------------------------------------------//

                        AnsiedadCompetitiva pru = AnsiedadPrecomView.Instance.ansiedad;


                        Bookmark Q3L = wordDoc.Bookmarks["AS"];
                        Range A = Q3L.Range;
                        A.Text = " " + pru.AS;

                        Bookmark CL = wordDoc.Bookmarks["AC"];
                        Range B = CL.Range;
                        B.Text = " " + pru.AC;

                        Bookmark LL = wordDoc.Bookmarks["ACF"];
                        Range C = LL.Range;
                        C.Text = " " + pru.ACF;


                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();
                        esp.Close();

                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarIped()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
            {

                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Iped.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " IPED";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {

                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();

                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;


                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;


                        //---------------------------------------------------------//

                        Iped pru = IpedView.Instance.iped;


                        Bookmark Q3L = wordDoc.Bookmarks["A"];
                        Range A = Q3L.Range;
                        A.Text = " " + pru.Autoconfianza;

                        Bookmark CL = wordDoc.Bookmarks["B"];
                        Range B = CL.Range;
                        B.Text = " " + pru.ContAfronNegativ;

                        Bookmark LL = wordDoc.Bookmarks["C"];
                        Range C = LL.Range;
                        C.Text = " " + pru.ContAtencional;

                        Bookmark OL = wordDoc.Bookmarks["D"];
                        Range D = OL.Range;
                        D.Text = " " + pru.ContVisuoimag;

                        Bookmark Q3Md = wordDoc.Bookmarks["E"];
                        Range Fd = Q3Md.Range;
                        Fd.Text = " " + pru.ContAfrontPositiv;

                        Bookmark Q3M = wordDoc.Bookmarks["F"];
                        Range F = Q3M.Range;
                        F.Text = " " + pru.ContActitudinal;

                            Bookmark Q4M = wordDoc.Bookmarks["G"];
                            Range G = Q4M.Range;
                            G.Text = " " + pru.NivelMotiv;



                            wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();
                        esp.Close();

                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarIdetem()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
            {

                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\IDETEM.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " IDETEM-1";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {
                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();

                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;


                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;


                        //---------------------------------------------------------//

                        Idetem pru = IdetemView.Instance.idetem;


                        Bookmark Q3L = wordDoc.Bookmarks["A"];
                        Range A = Q3L.Range;
                        A.Text = " " + pru.sanguineo;

                        Bookmark CL = wordDoc.Bookmarks["B"];
                        Range B = CL.Range;
                        B.Text = " " + pru.colerico;

                        Bookmark LL = wordDoc.Bookmarks["C"];
                        Range C = LL.Range;
                        C.Text = " " + pru.flematico;

                        Bookmark OL = wordDoc.Bookmarks["D"];
                        Range D = OL.Range;
                        D.Text = " " + pru.melancolico;

                        Bookmark Q3Md = wordDoc.Bookmarks["E"];
                        Range Fd = Q3Md.Range;
                        Fd.Text = " " + pru.sanguineo;

                        Bookmark Q3M = wordDoc.Bookmarks["F"];
                        Range F = Q3M.Range;
                        F.Text = " " + pru.colerico;

                        Bookmark CM = wordDoc.Bookmarks["G"];
                        Range G = CM.Range;
                        G.Text = " " + pru.flematico;

                        Bookmark LM = wordDoc.Bookmarks["H"];
                        Range h = LM.Range;
                        h.Text = " " + pru.melancolico;


                        Bookmark OM = wordDoc.Bookmarks["I"];
                        Range I = OM.Range;
                        I.Text = " " + pru.equilibrio;

                        Bookmark OMq = wordDoc.Bookmarks["J"];
                        Range Iq = OMq.Range;
                        Iq.Text = " " + pru.deseqExita;

                        Bookmark OMq1 = wordDoc.Bookmarks["K"];
                        Range Iq1 = OMq1.Range;
                        Iq1.Text = " " + pru.deseqInhibi;

                        Bookmark OMq1r = wordDoc.Bookmarks["L"];
                        Range Iq1r = OMq1r.Range;
                        Iq1r.Text = " " + pru.fuerza;

                        Bookmark OMq1rt = wordDoc.Bookmarks["M"];
                        Range Iq1rt = OMq1rt.Range;
                        Iq1rt.Text = " " + pru.debilidad;

                        Bookmark OMq1rt1 = wordDoc.Bookmarks["N"];
                        Range Iq1rt1 = OMq1rt1.Range;
                        Iq1rt1.Text = " " + pru.movilidad;


                        Bookmark OMq1rt12 = wordDoc.Bookmarks["O"];
                        Range Iq1rt12 = OMq1rt12.Range;
                        Iq1rt12.Text = " " + pru.inercia;


                        Bookmark OMq1rt121 = wordDoc.Bookmarks["P"];
                        Range Iq1rt121 = OMq1rt121.Range;
                        Iq1rt121.Text = " " + pru.dinamPsiq;


                        Bookmark OMq1rt1213 = wordDoc.Bookmarks["Q"];
                        Range Iq1rt1213 = OMq1rt1213.Range;
                        Iq1rt1213.Text = " " + pru.pocoDinaPsiq;

                        Bookmark OMq1rt1213q = wordDoc.Bookmarks["R"];
                        Range Iq1rt1213q = OMq1rt1213q.Range;
                        Iq1rt1213q.Text = " " + pru.labilidad;

                        Bookmark OMq1rts = wordDoc.Bookmarks["S"];
                        Range Iq1rts = OMq1rts.Range;
                        Iq1rts.Text = " " + pru.actividad;

                        Bookmark OMq1rt1t = wordDoc.Bookmarks["T"];
                        Range Iq1rt1t = OMq1rt1t.Range;
                        Iq1rt1t.Text = " " + pru.reactModer;


                        Bookmark OMq1rt1211 = wordDoc.Bookmarks["U"];
                        Range Iq1rt1211 = OMq1rt1211.Range;
                        Iq1rt1211.Text = " " + pru.reactAlta;


                        Bookmark OMq1rt121v = wordDoc.Bookmarks["V"];
                        Range Iq1rt121v = OMq1rt121v.Range;
                        Iq1rt121v.Text = " " + pru.resisAlta;


                        Bookmark OMq1rt12132 = wordDoc.Bookmarks["W"];
                        Range Iq1rt12132 = OMq1rt12132.Range;
                        Iq1rt12132.Text = " " + pru.resisBaja;

                        Bookmark OMq1rt1213qq = wordDoc.Bookmarks["X"];
                        Range Iq1rt1213qq = OMq1rt1213qq.Range;
                        Iq1rt1213qq.Text = " " + pru.ritmPsiRap;



                        Bookmark OMq1rt1213q1 = wordDoc.Bookmarks["Y"];
                        Range Iq1rt1213q1 = OMq1rt1213q1.Range;
                        Iq1rt1213q1.Text = " " + pru.ritmPsiLent;



                        Bookmark OMq1rt1213qz = wordDoc.Bookmarks["Z"];
                        Range Iq1rt1213qz = OMq1rt1213qz.Range;
                        Iq1rt1213qz.Text = " " + pru.sensibilidad;





                        Bookmark OMq1rt121vq = wordDoc.Bookmarks["A1"];
                        Range Iq1rt121vq = OMq1rt121vq.Range;
                        Iq1rt121vq.Text = " " + pru.pocaSensi;


                        Bookmark OMq1rt121321 = wordDoc.Bookmarks["A2"];
                        Range Iq1rt121321 = OMq1rt121321.Range;
                        Iq1rt121321.Text = " " + pru.extroversion;

                        Bookmark OMq1rt1213qq2 = wordDoc.Bookmarks["A3"];
                        Range Iq1rt1213qq2 = OMq1rt1213qq2.Range;
                        Iq1rt1213qq2.Text = " " + pru.introversion;



                        Bookmark OMq1rt1213q12 = wordDoc.Bookmarks["A4"];
                        Range Iq1rt1213q12 = OMq1rt1213q12.Range;
                        Iq1rt1213q12.Text = " " + pru.plasticidad;



                        Bookmark OMq1rt1213qzw = wordDoc.Bookmarks["A5"];
                        Range Iq1rt1213qzw = OMq1rt1213qzw.Range;
                        Iq1rt1213qzw.Text = " " + pru.rigidez;








                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();

                        esp.Close();

                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarCualidadesMotiv()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
            {

                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\CualidadesMotivacionales.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " Cualidades Motivacionales del Deporte";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {
                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();
                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;


                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;


                        //---------------------------------------------------------//

                        CualidMotivDeportiv pru = CualidadesMotivacionalesView.Instance.cualidades;


                        Bookmark Q3L = wordDoc.Bookmarks["A"];
                        Range A = Q3L.Range;
                        A.Text = " " + pru.motivLogro;

                        Bookmark CL = wordDoc.Bookmarks["B"];
                        Range B = CL.Range;
                        B.Text = " " + pru.motivIntrínseca;

                        Bookmark LL = wordDoc.Bookmarks["C"];
                        Range C = LL.Range;
                        C.Text = " " + pru.expecExito;

                        Bookmark OL = wordDoc.Bookmarks["D"];
                        Range D = OL.Range;
                        D.Text = " " + pru.motivAproExito;

                        Bookmark Q3Md = wordDoc.Bookmarks["E"];
                        Range Fd = Q3Md.Range;
                        Fd.Text = " " + pru.motivMater;

                        Bookmark Q3M = wordDoc.Bookmarks["F"];
                        Range F = Q3M.Range;
                        F.Text = " " + pru.motivAutoDeportiva;

                        Bookmark CM = wordDoc.Bookmarks["G"];
                        Range G = CM.Range;
                        G.Text = " " + pru.motivSuprain;

                        Bookmark LM = wordDoc.Bookmarks["H"];
                        Range h = LM.Range;
                        h.Text = " " + pru.noMotivLogro;

                        Bookmark OM = wordDoc.Bookmarks["I"];
                        Range I = OM.Range;
                        I.Text = " " + pru.motivExtrínseca;

                        Bookmark OMq = wordDoc.Bookmarks["J"];
                        Range Iq = OMq.Range;
                        Iq.Text = " " + pru.expecEficacia;

                        Bookmark OMq1 = wordDoc.Bookmarks["K"];
                        Range Iq1 = OMq1.Range;
                        Iq1.Text = " " + pru.movEvitarFracaso;

                        Bookmark OMq1r = wordDoc.Bookmarks["L"];
                        Range Iq1r = OMq1r.Range;
                        Iq1r.Text = " " + pru.motivRecono;

                        Bookmark OMq1rt = wordDoc.Bookmarks["M"];
                        Range Iq1rt = OMq1rt.Range;
                        Iq1rt.Text = " " + pru.motivAutoPersono;



                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();

                        esp.Close();
                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarActitudAnteCompe()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\ActitudAnteCompetencia.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = nombreAtleta + " Actitud Ante la Competencia";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {

                            System.Windows.Forms.Application.DoEvents();
                            Esperar esp = new Esperar();
                            esp.Show();

                            File.Copy(rutaProject, fichero.FileName, true);


                            String path = fichero.FileName.ToString();


                            Microsoft.Office.Interop.Word.Application wordApp = null;
                            wordApp = new Microsoft.Office.Interop.Word.Application();
                            String username = Environment.UserName;
                            Document wordDoc = wordApp.Documents.Open(path);



                            Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                            Range rng1 = bkmfecha.Range;
                            rng1.Text = " " + fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + nombreAtleta;


                            Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                            Range rng3 = bkmnEdad.Range;
                            rng3.Text = " " + edad;


                            Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                            Range rng4 = bkmnDeport.Range;
                            rng4.Text = " " + deporte;


                            Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                            Range rng5 = bkmnModal.Range;
                            rng5.Text = " " + modalidad;


                            Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                            Range rng6 = bkmnModal1.Range;
                            rng6.Text = " " + ci;


                            //---------------------------------------------------------//

                            ActitudAnteCompetencia pru = ActitudCompetenciaView.Instance.actitud;


                            Bookmark Q3L = wordDoc.Bookmarks["A"];
                            Range A = Q3L.Range;
                            A.Text = " " + pru.Certeza;

                            Bookmark CL = wordDoc.Bookmarks["B"];
                            Range B = CL.Range;
                            B.Text = " " + pru.Opinion;

                            Bookmark LL = wordDoc.Bookmarks["C"];
                            Range C = LL.Range;
                            C.Text = " " + pru.Contrario;

                            Bookmark OL = wordDoc.Bookmarks["D"];
                            Range D = OL.Range;
                            D.Text = " " + pru.Significacion;






                            Bookmark Q3L1 = wordDoc.Bookmarks["pcer"];
                            Range A1 = Q3L1.Range;
                            A1.Text = " " + pru.ptoCerteza;

                            Bookmark CL1 = wordDoc.Bookmarks["pcon"];
                            Range B1 = CL1.Range;
                            B1.Text = " " + pru.ptoContrio;

                            Bookmark LL1 = wordDoc.Bookmarks["psig"];
                            Range C1 = LL1.Range;
                            C1.Text = " " + pru.ptoSignificacion;

                            Bookmark OL1 = wordDoc.Bookmarks["popi"];
                            Range D1 = OL1.Range;
                            D1.Text = " " + pru.ptoOpinion;




                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();

                            esp.Close();
                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception e)
                {

                    MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void exportarCualidadesVolitiv()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\CualidadesVolitivas.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = nombreAtleta + " Cualidades Volitivas";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {

                            System.Windows.Forms.Application.DoEvents();
                            Esperar esp = new Esperar();
                            esp.Show();
                            File.Copy(rutaProject, fichero.FileName, true);


                            String path = fichero.FileName.ToString();


                            Microsoft.Office.Interop.Word.Application wordApp = null;
                            wordApp = new Microsoft.Office.Interop.Word.Application();
                            String username = Environment.UserName;
                            Document wordDoc = wordApp.Documents.Open(path);



                            Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                            Range rng1 = bkmfecha.Range;
                            rng1.Text = " " + fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + nombreAtleta;


                            Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                            Range rng3 = bkmnEdad.Range;
                            rng3.Text = " " + edad;


                            Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                            Range rng4 = bkmnDeport.Range;
                            rng4.Text = " " + deporte;


                            Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                            Range rng5 = bkmnModal.Range;
                            rng5.Text = " " + modalidad;

                            //---------------------------------------------------------//

                            CualiVolitivasDep pru = CualidadesVolitivasView.Instance.cualidades;


                            Bookmark Q3L = wordDoc.Bookmarks["A"];
                            Range A = Q3L.Range;
                            A.Text = " " + pru.autoIndepen;

                            Bookmark CL = wordDoc.Bookmarks["B"];
                            Range B = CL.Range;
                            B.Text = " " + pru.tenacidadResol;

                            Bookmark LL = wordDoc.Bookmarks["C"];
                            Range C = LL.Range;
                            C.Text = " " + pru.persePersis;

                            Bookmark OL = wordDoc.Bookmarks["D"];
                            Range D = OL.Range;
                            D.Text = " " + pru.autodAutocon;


                            Bookmark Q4L = wordDoc.Bookmarks["E"];
                            Range E = Q4L.Range;
                            E.Text = " " + pru.PtoAutoIndepen;



                            Bookmark Q3M = wordDoc.Bookmarks["F"];
                            Range F = Q3M.Range;
                            F.Text = " " + pru.PtoTenacidadResol;

                            Bookmark CM = wordDoc.Bookmarks["G"];
                            Range G = CM.Range;
                            G.Text = " " + pru.PtoPersePersis;

                            Bookmark LM = wordDoc.Bookmarks["H"];
                            Range h = LM.Range;
                            h.Text = " " + pru.PtoAutodAutocon;

                            Bookmark OM = wordDoc.Bookmarks["fal"];
                            Range I = OM.Range;
                            I.Text = " " + pru.Falseamiento;





                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();
                            esp.Close();

                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception e)
                {

                    MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void exportarEysenck()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
            {


                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Eysenck.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " EPI";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {

                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();
                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;

                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;

                        //---------------------------------------------------------//

                        PruEysenk pru = EysenckView.Instance.Eysenk;

                        Bookmark Q3L = wordDoc.Bookmarks["Conflicto"];
                        Range A = Q3L.Range;
                        A.Text = " " + pru.Neuroticismo;

                        Bookmark CL = wordDoc.Bookmarks["Rivalidad"];
                        Range B = CL.Range;
                        B.Text = " " + pru.Extroversion;

                        Bookmark LL = wordDoc.Bookmarks["Suficiencia"];
                        Range C = LL.Range;
                        C.Text = " " + pru.Sinceridad;




                        Bookmark LL1 = wordDoc.Bookmarks["diag"];
                        Range C1 = LL1.Range;
                        C1.Text = " " + pru.DiagNeurotic + "-" + pru.DiagExtrove;


                        Bookmark LL12 = wordDoc.Bookmarks["temp"];
                        Range C12 = LL12.Range;
                        C12.Text = " " + pru.DiagCuadrante;


                        Bookmark LL124 = wordDoc.Bookmarks["trastorno"];
                        Range C124 = LL124.Range;
                        C124.Text = " " + pru.DiagnosticoLetra;




                        if (pru.DiagCuadrante == "Melancólico")
                        {
                            Bookmark L = wordDoc.Bookmarks["cualidades"];
                            Range C9 = L.Range;
                            C9.Text = " Labil,Ansioso,Rígido,Severo,Pesimista,Reservado,Insaciable y Tranquilo";


                            Bookmark L1 = wordDoc.Bookmarks["sisner"];
                            Range C91 = L1.Range;
                            C91.Text = " DEBIL (equilibrio menor,fuerza menor,movilidad menor)";

                        }

                        if (pru.DiagCuadrante == "Colérico")
                        {

                            Bookmark L = wordDoc.Bookmarks["cualidades"];
                            Range C9 = L.Range;
                            C9.Text = " Susceptible, Agitado,Agresivo,Excitable,Variable,Impulsivo,Optimista y Activo";


                            Bookmark L1 = wordDoc.Bookmarks["sisner"];
                            Range C91 = L1.Range;
                            C91.Text = " FUERTE (sistema nervioso fuerte,equilibrio menor,fuerza mayor,movilidad mayor)";

                        }

                        if (pru.DiagCuadrante == "Flemático")
                        {
                            Bookmark L = wordDoc.Bookmarks["cualidades"];
                            Range C9 = L.Range;
                            C9.Text = " Pasivo,Cuidadoso,Pensativo,Apacible,Controlado,Leal,Ecuanime e Imperturbable";


                            Bookmark L1 = wordDoc.Bookmarks["sisner"];
                            Range C91 = L1.Range;
                            C91.Text = " FUERTE (sistema nervioso fuerte,equilibrio mayor,fuerza mayor,movilidad menor (pero normal) )";

                        }

                        if (pru.DiagCuadrante == "Sanguíneo")
                        {
                            Bookmark L = wordDoc.Bookmarks["cualidades"];
                            Range C9 = L.Range;
                            C9.Text = " Sociable,Expresivo,Locuaz,Sensible,Vivaz,Adaptable,Animado,Despreocupado y Diligente";


                            Bookmark L1 = wordDoc.Bookmarks["sisner"];
                            Range C91 = L1.Range;
                            C91.Text = " FUERTE (sistema nervioso fuerte,equilibrio mayor,fuerza mayor,movilidad mayor)";

                        }



                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();

                        esp.Close();

                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarCatell()
        {

            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {


                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Catell.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = nombreAtleta + " Test_Catell";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {

                            System.Windows.Forms.Application.DoEvents();
                            Esperar esp = new Esperar();
                            esp.Show();
                            File.Copy(rutaProject, fichero.FileName, true);


                            String path = fichero.FileName.ToString();


                            Microsoft.Office.Interop.Word.Application wordApp = null;
                            wordApp = new Microsoft.Office.Interop.Word.Application();
                            String username = Environment.UserName;
                            Document wordDoc = wordApp.Documents.Open(path);



                            Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                            Range rng1 = bkmfecha.Range;
                            rng1.Text = " " + fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + nombreAtleta;


                            Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                            Range rng3 = bkmnEdad.Range;
                            rng3.Text = " " + edad;


                            Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                            Range rng4 = bkmnDeport.Range;
                            rng4.Text = " " + deporte;


                            Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                            Range rng5 = bkmnModal.Range;
                            rng5.Text = " " + modalidad;

                            Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                            Range rng6 = bkmnModal1.Range;
                            rng6.Text = " " + ci;
                            //---------------------------------------------------------//

                            PruCatell pru = CatellView.Instance.catell;

                            Bookmark Q3L = wordDoc.Bookmarks["Q3L"];
                            Range A = Q3L.Range;
                            A.Text = " " + pru.PBrutaLatQ3;

                            Bookmark CL = wordDoc.Bookmarks["CL"];
                            Range B = CL.Range;
                            B.Text = " " + pru.PBrutaLatC;

                            Bookmark LL = wordDoc.Bookmarks["LL"];
                            Range C = LL.Range;
                            C.Text = " " + pru.PBrutaLatL;

                            Bookmark OL = wordDoc.Bookmarks["OL"];
                            Range D = OL.Range;
                            D.Text = " " + pru.PBrutaLatO;


                            Bookmark Q4L = wordDoc.Bookmarks["Q4L"];
                            Range E = Q4L.Range;
                            E.Text = " " + pru.PBrutaLatQ4;



                            Bookmark Q3M = wordDoc.Bookmarks["Q3M"];
                            Range F = Q3M.Range;
                            F.Text = " " + pru.PBrutaManQ3;

                            Bookmark CM = wordDoc.Bookmarks["CM"];
                            Range G = CM.Range;
                            G.Text = " " + pru.PBrutaManC;

                            Bookmark LM = wordDoc.Bookmarks["LM"];
                            Range h = LM.Range;
                            h.Text = " " + pru.PBrutaManL;

                            Bookmark OM = wordDoc.Bookmarks["OM"];
                            Range I = OM.Range;
                            I.Text = " " + pru.PBrutaManO;


                            Bookmark Q4M = wordDoc.Bookmarks["Q4M"];
                            Range J = Q4M.Range;
                            J.Text = " " + pru.PBrutaManQ4;







                            Bookmark LMQ3 = wordDoc.Bookmarks["LMQ3"];
                            Range K = LMQ3.Range;
                            K.Text = " " + pru.PBrutaLatManQ3;

                            Bookmark LMC = wordDoc.Bookmarks["LMC"];
                            Range L = LMC.Range;
                            L.Text = " " + pru.PBrutaLaManC;

                            Bookmark LML = wordDoc.Bookmarks["LML"];
                            Range M = LML.Range;
                            M.Text = " " + pru.PBrutaLatManL;

                            Bookmark LMO = wordDoc.Bookmarks["LMO"];
                            Range N = LMO.Range;
                            N.Text = " " + pru.PBrutaLatManO;

                            Bookmark LMQ4 = wordDoc.Bookmarks["LMQ4"];
                            Range O = LMQ4.Range;
                            O.Text = " " + pru.PBrutaLatManQ4;






                            //---------------------------------Stens-------------------------------//
                            Bookmark Q3SL = wordDoc.Bookmarks["Q3SL"];
                            Range A1 = Q3SL.Range;
                            A1.Text = " " + pru.PStensLatQ3;

                            Bookmark CSL = wordDoc.Bookmarks["CSL"];
                            Range B1 = CSL.Range;
                            B1.Text = " " + pru.PStensLatC;

                            Bookmark LSL = wordDoc.Bookmarks["LSL"];
                            Range C1 = LSL.Range;
                            C1.Text = " " + pru.PStensLatL;

                            Bookmark OSL = wordDoc.Bookmarks["OSL"];
                            Range D1 = OSL.Range;
                            D1.Text = " " + pru.PStensLatO;


                            Bookmark Q4SL = wordDoc.Bookmarks["Q4SL"];
                            Range E1 = Q4SL.Range;
                            E1.Text = " " + pru.PStensLatQ4;



                            Bookmark Q3SM = wordDoc.Bookmarks["Q3SM"];
                            Range F1 = Q3SM.Range;
                            F1.Text = " " + pru.PStensManQ3;

                            Bookmark CSM = wordDoc.Bookmarks["CSM"];
                            Range G1 = CSM.Range;
                            G1.Text = " " + pru.PStensManC;

                            Bookmark LSM = wordDoc.Bookmarks["LSM"];
                            Range h1 = LSM.Range;
                            h1.Text = " " + pru.PStensManL;

                            Bookmark OSM = wordDoc.Bookmarks["OSM"];
                            Range I1 = OSM.Range;
                            I1.Text = " " + pru.PStensManO;


                            Bookmark Q4SM = wordDoc.Bookmarks["Q4SM"];
                            Range J1 = Q4SM.Range;
                            J1.Text = " " + pru.PStensManQ4;







                            Bookmark LMSQ3 = wordDoc.Bookmarks["LMSQ3"];
                            Range K1 = LMSQ3.Range;
                            K1.Text = " " + pru.PStensLatManQ3;

                            Bookmark LMSC = wordDoc.Bookmarks["LMSC"];
                            Range L1 = LMSC.Range;
                            L1.Text = " " + pru.PStensLatManC;

                            Bookmark LMSL = wordDoc.Bookmarks["LMSL"];
                            Range M1 = LMSL.Range;
                            M1.Text = " " + pru.PStensLatManL;

                            Bookmark LMSO = wordDoc.Bookmarks["LMSO"];
                            Range N1 = LMSO.Range;
                            N1.Text = " " + pru.PStensLatManO;

                            Bookmark LMSQ4 = wordDoc.Bookmarks["LMSQ4"];
                            Range O1 = LMSQ4.Range;
                            O1.Text = " " + pru.PStensLatManQ4;



                            //-------------------------------------------------------//

                            Bookmark ans = wordDoc.Bookmarks["ANS"];
                            Range an = ans.Range;
                            an.Text = " " + pru.PuntTotalLE;


                            Bookmark Stens = wordDoc.Bookmarks["STENS"];
                            Range stens = Stens.Range;
                            stens.Text = " " + pru.PStensTotal;


                            Bookmark Ip = wordDoc.Bookmarks["ps"];
                            Range r = Ip.Range;
                            r.Text = " " + pru.IntSico;



                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();

                            esp.Close();

                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception e)
                {

                    MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (mainEntities db = new mainEntities())
                {
                    int id = Convert.ToInt32(idATestTodo);
                    SujetosEvaluados res = db.SujetosEvaluados.Where(s => s.idSujetoEvaluado == id).FirstOrDefault<SujetosEvaluados>();


                    if (prueba == "Raven")
                    {
                        res.PRaven = null;


                    }

                    if (prueba == "Ansiedad Precompetitiva CSAI-2R")
                    {
                        res.PAnsiedadCompetitiva = null;


                    }



                    if (prueba == "Eysenck")
                    {

                        res.PEysenk = null;

                    }

                    if (prueba == "Weil")
                    {
                        res.PWeil = null;


                    }

                    if (prueba == "Dominó")
                    {

                        res.PDomino = null;


                    }

                    if (prueba == "Idare (Rasgo)")
                    {
                        res.PIdareRasgo = null;



                    }

                    if (prueba == "Idare (Situacional)")
                    {

                        res.PIdareSitua = null;


                    }

                    if (prueba == "Catell")
                    {
                        res.PCatell = null;



                    }

                    if (prueba == "16Pf")
                    {
                        res.P16pf = null;


                    }

                    if (prueba == "Cualidades Volitivas")
                    {
                        res.PCualiVolitiv = null;


                    }

                    if (prueba == "Butt")
                    {
                        res.PMotivDepButt = null;



                    }

                    if (prueba == "Cualidades Motivacionales")
                    {
                        res.PCualidMotivDepor = null;


                    }

                    if (prueba == "Actitud Ante la Competencia")
                    {
                        res.PActiAnteComp = null;



                    }

                    if (prueba == "TRS")
                    {
                        res.PTrsimple = null;


                    }

                    if (prueba == "TRC")
                    {
                        res.PTrcomple = null;


                    }

                    if (prueba == "TRCS")
                    {
                        res.PTrcomples = null;


                    }

                    if (prueba == "Tiempo de Respuesta Anticipada")
                    {
                        res.PResanti = null;


                    }

                    if (prueba == "Martens")
                    {

                    }

                    if (prueba == "Idetem")
                    {
                        res.PIdetem = null;

                    }

                    if (prueba == "Iped")
                    {
                        res.PIped = null;
                    }

                    if (prueba == "POMS")
                    {
                        res.PPoms = null;
                    }

                    if (prueba == "Ansiedad Precompetitiva CSAI-2R")
                    {
                        res.PAnsiedadCompetitiva = null;
                    }

                    if (res.PruTRN == null && res.PruTrsimple == null && res.PIped == null && res.PIdetem == null && res.PResanti == null && res.PTrcomples == null && res.PTrcomple == null && res.PRaven == null
                             && res.PEysenk == null && res.PWeil == null && res.PDomino == null && res.PIdareRasgo == null && res.PIdareSitua == null && res.PCatell == null
                             && res.P16pf == null && res.PCualiVolitiv == null && res.PMotivDepButt == null && res.PCualidMotivDepor == null && res.PActiAnteComp == null && res.PPoms == null && res.PAnsiedadCompetitiva == null
                             )
                    {
                        db.SujetosEvaluados.Remove(res);
                    }

                    db.SaveChangesAsync();



                    if (prueba == "Raven")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruRaven where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (prueba == "Ansiedad Precompetitiva CSAI-2R")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from AnsiedadCompetitiva where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }


                    if (prueba == "Eysenck")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruEysenk where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Weil")
                    {

                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruWeil where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (prueba == "Dominó")
                    {



                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruDomino where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Idare (Rasgo)")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruIdareRago where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (prueba == "Idare (Situacional)")
                    {



                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruIdareSituacional where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Catell")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruCatell where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (prueba == "16Pf")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from Pru16pf where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Cualidades Volitivas")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from CualiVolitivasDep where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Butt")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from MotivDeporButt where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (prueba == "Cualidades Motivacionales")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from CualidMotivDeportiv where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Actitud Ante la Competencia")
                    {



                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from ActitudAnteCompetencia where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "TRS")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrsimple where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "TRC")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrcomple where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "TRCS")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrcomple where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Tiempo de Respuesta Anticipada")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrcomple where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Ansiedad Precompetitiva CSAI-2R")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from AnsiedadCompetitiva where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Idetem")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from Idetem where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "Iped")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from Iped where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (prueba == "POMS")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruPoms where idTest='" + idTest + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }


         


                    MessageBox.Show("Se ha eliminado la prueba", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
            }
        }
        private void exportarIdareRasg()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
               
                try
            {


                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\IDARE(Rasgo).doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " Test_IDARE(Rasgo)";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {
                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();
                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;

                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;
                        //---------------------------------------------------------//

                        PruIdareRago pru = IdareView.Instance.idareRasgo;

                        Bookmark ptoA = wordDoc.Bookmarks["ptos"];
                        Range A = ptoA.Range;
                        A.Text = " " + pru.PAnsiedadRasgo + " ptos";


                        Bookmark ptoB = wordDoc.Bookmarks["diag"];
                        Range B = ptoB.Range;
                        B.Text = " " + pru.DiagAnsRasgo;




                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();


                        esp.Close();

                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarIdareSit()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;

                try
            {


                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\IDARE(Situacional).doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " Test_IDARE(Situacional)";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {

                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();

                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;

                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;

                        //---------------------------------------------------------//

                        PruIdareSituacional pru = IdareView.Instance.idareSit;

                        Bookmark ptoA = wordDoc.Bookmarks["ptos"];
                        Range A = ptoA.Range;
                        A.Text = " " + pru.PAnsiedadSituacional + " ptos";


                        Bookmark ptoB = wordDoc.Bookmarks["diag"];
                        Range B = ptoB.Range;
                        B.Text = " " + pru.DiagAnsSituacional;




                        wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();

                        esp.Close();
                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        }

        private void exportarButt()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;

                try
            {


                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Butt.doc");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Word (*.doc)|*.doc";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";
                fichero.FileName = nombreAtleta + " Test_Butt";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {
                        System.Windows.Forms.Application.DoEvents();
                        Esperar esp = new Esperar();
                        esp.Show();

                        File.Copy(rutaProject, fichero.FileName, true);


                        String path = fichero.FileName.ToString();


                        Microsoft.Office.Interop.Word.Application wordApp = null;
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        String username = Environment.UserName;
                        Document wordDoc = wordApp.Documents.Open(path);



                        Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                        Range rng1 = bkmfecha.Range;
                        rng1.Text = " " + fecha;

                        Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                        Range rng2 = bkmnNombre.Range;
                        rng2.Text = " " + nombreAtleta;


                        Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                        Range rng3 = bkmnEdad.Range;
                        rng3.Text = " " + edad;


                        Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                        Range rng4 = bkmnDeport.Range;
                        rng4.Text = " " + deporte;


                        Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                        Range rng5 = bkmnModal.Range;
                        rng5.Text = " " + modalidad;


                        Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                        Range rng6 = bkmnModal1.Range;
                        rng6.Text = " " + ci;
                        //---------------------------------------------------------//

                        MotivDeporButt pru = ButtView.Instance.prueba;

                        Bookmark ptoA = wordDoc.Bookmarks["pregunta"];
                        Range A = ptoA.Range;
                        A.Text = " " + pru.Pregunta;


                        Bookmark ptoB = wordDoc.Bookmarks["PT"];
                        Range B = ptoB.Range;
                        B.Text = " " + pru.PuntuacionTotal + " ptos";


                        Bookmark ptoC = wordDoc.Bookmarks["Rivalidad"];
                        Range C = ptoC.Range;
                        C.Text = " " + pru.Rivalidad;


                        Bookmark ptoD = wordDoc.Bookmarks["Suficiencia"];
                        Range D = ptoD.Range;
                        D.Text = " " + pru.Suficiencia;


                        Bookmark ptoE = wordDoc.Bookmarks["Agresividad"];
                        Range E = ptoE.Range;
                        E.Text = " " + pru.Agresividad;


                        Bookmark ptoTotal = wordDoc.Bookmarks["Conflicto"];
                        Range pt = ptoTotal.Range;
                        pt.Text = " " + pru.Conflicto;


                        Bookmark ptoPor = wordDoc.Bookmarks["Cooperacion"];
                        Range porc = ptoPor.Range;
                        porc.Text = " " + pru.Cooperacion;

                        Bookmark ptoEva = wordDoc.Bookmarks["EVA"];
                        Range eva = ptoEva.Range;
                        eva.Text = " " + pru.calFilna;


                            wordApp.ActiveDocument.Save();
                        wordApp.ActiveDocument.Close();

                        esp.Close();
                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e)
            {

                MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        }

        private void exportarRaven()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {


                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Raven.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = nombreAtleta + " Test_Raven";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {
                            System.Windows.Forms.Application.DoEvents();
                            Esperar esp = new Esperar();
                            esp.Show();
                            File.Copy(rutaProject, fichero.FileName, true);


                            String path = fichero.FileName.ToString();


                            Microsoft.Office.Interop.Word.Application wordApp = null;
                            wordApp = new Microsoft.Office.Interop.Word.Application();
                            String username = Environment.UserName;
                            Document wordDoc = wordApp.Documents.Open(path);



                            Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                            Range rng1 = bkmfecha.Range;
                            rng1.Text = " " + fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + nombreAtleta;


                            Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                            Range rng3 = bkmnEdad.Range;
                            rng3.Text = " " + edad;


                            Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                            Range rng4 = bkmnDeport.Range;
                            rng4.Text = " " + deporte;


                            Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                            Range rng5 = bkmnModal.Range;
                            rng5.Text = " " + modalidad;

                            Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                            Range rng6 = bkmnModal1.Range;
                            rng6.Text = " " + ci;

                            //---------------------------------------------------------//

                            PruRaven pru = RavenView.Instance.prueba;

                            Bookmark ptoA = wordDoc.Bookmarks["A"];
                            Range A = ptoA.Range;
                            A.Text = " " + pru.PuntajeA + " ptos";


                            Bookmark ptoB = wordDoc.Bookmarks["B"];
                            Range B = ptoB.Range;
                            B.Text = " " + pru.PuntajeB + " ptos";


                            Bookmark ptoC = wordDoc.Bookmarks["C"];
                            Range C = ptoC.Range;
                            C.Text = " " + pru.PuntajeC + " ptos";


                            Bookmark ptoD = wordDoc.Bookmarks["D"];
                            Range D = ptoD.Range;
                            D.Text = " " + pru.PuntajeD + " ptos";


                            Bookmark ptoE = wordDoc.Bookmarks["E"];
                            Range E = ptoE.Range;
                            E.Text = " " + pru.PuntajeE + " ptos";


                            Bookmark ptoTotal = wordDoc.Bookmarks["PT"];
                            Range pt = ptoTotal.Range;
                            pt.Text = " " + pru.PuntajeTotal + " ptos";


                            Bookmark ptoPor = wordDoc.Bookmarks["P"];
                            Range porc = ptoPor.Range;
                            porc.Text = " " + pru.Porcentaje + "%";



                            Bookmark ptoRa = wordDoc.Bookmarks["R"];
                            Range porR = ptoRa.Range;
                            porR.Text = " " + pru.Rango;



                            Bookmark ptoDi = wordDoc.Bookmarks["Di"];
                            Range porD = ptoDi.Range;
                            porD.Text = " " + pru.Diagnostico;




                            Bookmark con = wordDoc.Bookmarks["Con"];
                            Range cs = con.Range;
                            cs.Text = " " + pru.Consistencia;

                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();

                            esp.Close();
                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception e)
                {

                    MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void exportarWeil()
        {
            using (mainEntities db = new mainEntities())
            {

                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {


                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Weil.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = nombreAtleta + " Test_Weil";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {

                            System.Windows.Forms.Application.DoEvents();
                            Esperar esp = new Esperar();
                            esp.Show();

                            File.Copy(rutaProject, fichero.FileName, true);


                            String path = fichero.FileName.ToString();


                            Microsoft.Office.Interop.Word.Application wordApp = null;
                            wordApp = new Microsoft.Office.Interop.Word.Application();
                            String username = Environment.UserName;
                            Document wordDoc = wordApp.Documents.Open(path);



                            Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                            Range rng1 = bkmfecha.Range;
                            rng1.Text = " " + fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + nombreAtleta;


                            Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                            Range rng3 = bkmnEdad.Range;
                            rng3.Text = " " + edad;


                            Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                            Range rng4 = bkmnDeport.Range;
                            rng4.Text = " " + deporte;


                            Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                            Range rng5 = bkmnModal.Range;
                            rng5.Text = " " + modalidad;


                            Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                            Range rng6 = bkmnModal1.Range;
                            rng6.Text = " " + ci;
                            //---------------------------------------------------------//

                            PruWeil pru = WeilView.Instance.prueba;



                            Bookmark ptoTotal = wordDoc.Bookmarks["PT"];
                            Range pt = ptoTotal.Range;
                            pt.Text = " " + pru.PuntajeTotal + " ptos";


                            Bookmark ptoPor = wordDoc.Bookmarks["P"];
                            Range porc = ptoPor.Range;
                            porc.Text = " " + pru.Porcentaje + "%";



                            Bookmark ptoRa = wordDoc.Bookmarks["R"];
                            Range porR = ptoRa.Range;
                            porR.Text = " " + pru.Rango;



                            Bookmark ptoDi = wordDoc.Bookmarks["Di"];
                            Range porD = ptoDi.Range;
                            porD.Text = " " + pru.Diagnostico;

                           

                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();

                            esp.Close();
                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception e)
                {

                    MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }


        private void exportarDomino()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
             
                    try
                    {


                        string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\Dominó.doc");

                        SaveFileDialog fichero = new SaveFileDialog();
                        fichero.Filter = "Word (*.doc)|*.doc";
                        fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                        fichero.Title = "Exportar Pruebas";
                        fichero.FileName = nombreAtleta + " Test_Dominó";

                        if (fichero.ShowDialog() == DialogResult.OK)
                        {
                            if (File.Exists(rutaProject))
                            {
                                System.Windows.Forms.Application.DoEvents();
                                Esperar esp = new Esperar();
                                esp.Show();

                                File.Copy(rutaProject, fichero.FileName, true);


                                String path = fichero.FileName.ToString();


                                Microsoft.Office.Interop.Word.Application wordApp = null;
                                wordApp = new Microsoft.Office.Interop.Word.Application();
                                String username = Environment.UserName;
                                Document wordDoc = wordApp.Documents.Open(path);



                                Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                                Range rng1 = bkmfecha.Range;
                                rng1.Text = " " + fecha;

                                Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                                Range rng2 = bkmnNombre.Range;
                                rng2.Text = " " + nombreAtleta;


                                Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                                Range rng3 = bkmnEdad.Range;
                                rng3.Text = " " + edad;


                                Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                                Range rng4 = bkmnDeport.Range;
                                rng4.Text = " " + deporte;


                                Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                                Range rng5 = bkmnModal.Range;
                                rng5.Text = " " + modalidad;


                                Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                                Range rng6 = bkmnModal1.Range;
                                rng6.Text = " " + ci;
                                //---------------------------------------------------------//

                                PruDomino pru = DominoView.Instance.prueba;



                                Bookmark ptoTotal = wordDoc.Bookmarks["PT"];
                                Range pt = ptoTotal.Range;
                                pt.Text = " " + pru.Puntaje + " ptos";


                                Bookmark ptoPor = wordDoc.Bookmarks["P"];
                                Range porc = ptoPor.Range;
                                porc.Text = " " + pru.Porcentaje + "%";



                                Bookmark ptoRa = wordDoc.Bookmarks["R"];
                                Range porR = ptoRa.Range;
                                porR.Text = " " + pru.Rango;



                                Bookmark ptoDi = wordDoc.Bookmarks["DI"];
                                Range porD = ptoDi.Range;
                                porD.Text = " " + pru.Diagnostico;

                                





                                wordApp.ActiveDocument.Save();
                                wordApp.ActiveDocument.Close();
                                esp.Close();

                                MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start(fichero.FileName);
                            }

                        }




                    }
                    catch (Exception e)
                    {

                        MessageBox.Show("Ha ocurrido un error " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                
            }
        }
    }
}
