using Microsoft.Office.Interop.Word;
using Multitest.ADOmodel;
using Multitest.FormAux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest
{
    public partial class TRN_Form2 : Form
    {
        String idUser;
        String etapa;
        String idTest;
        String idTestTodo;

        String modalidad = "";
        String deporte = "";
        String edad = "";
        public TRN_Form2(List<String> list, String timerMinutes, String timerSecond, int repetidos, int saltos, int cambioColor, int cambioDirecc, int cambioDirecNo, String idPerson, String idEtapa, String nombreAtleta)
        {
            InitializeComponent();
            this.BringToFront();
            llenarResultados(list);
            label26.Text = nombreAtleta;
            this.idUser = idPerson;
            this.etapa = idEtapa;

            double minutos = Convert.ToDouble(timerMinutes.Substring(0, 4));
            minutos = minutos - 3;

            label4.Text = DateTime.Today.ToString("dd/MM/yyyy");
            label22.Text = timerMinutes.Substring(0, 4);
            label12.Text = repetidos.ToString();
            label14.Text = saltos.ToString();
            label20.Text = cambioColor.ToString();

            simpleButton1.Visible = false;
            button1.Visible = false;

            //   double temp = Convert.ToInt32(timerMinutes.Substring(0, 2));
            double pfinal = 0;

            if (minutos > 0)
            {

                pfinal = minutos + repetidos + saltos + cambioColor + (cambioDirecc * 3) + (cambioDirecNo * 5);

                //  pfinal = pfinal + (10 * second);
            }
            else
                pfinal = repetidos + saltos + cambioColor + (cambioDirecc * 3) + (cambioDirecNo * 5);

            pfinal = Math.Round(pfinal, 2);
            label24.Text = pfinal.ToString();
            label16.Text = cambioDirecc.ToString();
            label18.Text = cambioDirecNo.ToString();

            salvarDatos(list);
        }


        public TRN_Form2(String nombreAtleta, String idTest, bool mostrarButtons, String idTestTodo, string idAtleta)
        {
            InitializeComponent();
            this.idTestTodo = idTestTodo;
            label26.Text = nombreAtleta;
            simpleButton1.Visible = true;
            button1.Visible = true;
            buscarPrueba(idTest);
            this.idTest = idTest;
            simpleButton1.Visible = mostrarButtons ? true : false;
            button1.Visible = mostrarButtons ? true : false;
            if (idAtleta != null)
                buscarAtleta(idAtleta);
        }

        private void buscarAtleta(string idAtleta)
        {
            using (mainEntities entities = new mainEntities())
            {
                var atleta = entities.DatosSujetos.Find(Convert.ToInt32(idAtleta));
                modalidad = atleta.Ocupacion;
                deporte = atleta.Entidad;
                edad = atleta.Edad;
            }
        }

        private void buscarPrueba(String idTest)
        {
            using (mainEntities entities = new mainEntities())
            {
                var test = entities.PruTRN.Find(Convert.ToInt32(idTest));
                label12.Text = test.NRepeticiones;
                label14.Text = test.NSaltos;
                label16.Text = test.CdRect;
                label18.Text = test.CdNoRect;
                label20.Text = test.Cambcolor;
                label22.Text = test.Duracion;
                label24.Text = test.Calificacion;
                label4.Text = test.Fecha;

                List<String> list = new List<String>();
                list.Add(test.Resultado1);
                list.Add(test.Resultado2);
                list.Add(test.Resultado3);
                list.Add(test.Resultado4);
                list.Add(test.Resultado5);
                list.Add(test.Resultado6);
                list.Add(test.Resultado7);
                list.Add(test.Resultado8);
                list.Add(test.Resultado9);
                list.Add(test.Resultado10);
                list.Add(test.Resultado11);
                list.Add(test.Resultado12);
                list.Add(test.Resultado13);
                list.Add(test.Resultado14);
                list.Add(test.Resultado15);
                list.Add(test.Resultado16);
                list.Add(test.Resultado17);
                list.Add(test.Resultado18);
                list.Add(test.Resultado19);
                list.Add(test.Resultado20);
                list.Add(test.Resultado21);
                list.Add(test.Resultado22);
                list.Add(test.Resultado23);
                list.Add(test.Resultado24);
                list.Add(test.Resultado25);
                list.Add(test.Resultado26);
                list.Add(test.Resultado27);
                list.Add(test.Resultado28);
                list.Add(test.Resultado29);
                list.Add(test.Resultado30);
                list.Add(test.Resultado31);
                list.Add(test.Resultado32);
                list.Add(test.Resultado33);
                list.Add(test.Resultado34);
                list.Add(test.Resultado35);
                list.Add(test.Resultado36);
                list.Add(test.Resultado37);
                list.Add(test.Resultado38);
                list.Add(test.Resultado39);
                list.Add(test.Resultado40);
                list.Add(test.Resultado41);
                list.Add(test.Resultado42);
                list.Add(test.Resultado43);
                list.Add(test.Resultado44);
                list.Add(test.Resultado45);
                list.Add(test.Resultado46);
                list.Add(test.Resultado47);
                list.Add(test.Resultado48);
                list.Add(test.Resultado49);


                llenarResultados(list);


            }
        }




        private void llenarResultados(List<String> list)
        {
            int count = 1;
            foreach (var item in list)
            {
                string text = "n" + count;
                Label indicieA = this.Controls.Find(text, true).FirstOrDefault() as Label;
                indicieA.Text = item;
                indicieA.Visible = true;
                count++;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void salvarDatos(List<String> listTodo)
        {

            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {


                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {

                    PruTRN pruR = new PruTRN();


                    pruR.Fecha = date;

                    pruR.Resultado1 = listTodo[0];
                    pruR.Resultado2 = listTodo[1];
                    pruR.Resultado3 = listTodo[2];
                    pruR.Resultado4 = listTodo[3];
                    pruR.Resultado5 = listTodo[4];
                    pruR.Resultado6 = listTodo[5];
                    pruR.Resultado7 = listTodo[6];
                    pruR.Resultado8 = listTodo[7];
                    pruR.Resultado9 = listTodo[8];
                    pruR.Resultado10 = listTodo[9];


                    pruR.Resultado11 = listTodo[10];
                    pruR.Resultado12 = listTodo[11];
                    pruR.Resultado13 = listTodo[12];
                    pruR.Resultado14 = listTodo[13];
                    pruR.Resultado15 = listTodo[14];
                    pruR.Resultado16 = listTodo[15];
                    pruR.Resultado17 = listTodo[16];
                    pruR.Resultado18 = listTodo[17];
                    pruR.Resultado19 = listTodo[18];
                    pruR.Resultado20 = listTodo[19];


                    pruR.Resultado21 = listTodo[20];
                    pruR.Resultado22 = listTodo[21];
                    pruR.Resultado23 = listTodo[22];
                    pruR.Resultado24 = listTodo[23];
                    pruR.Resultado25 = listTodo[24];
                    pruR.Resultado26 = listTodo[25];
                    pruR.Resultado27 = listTodo[26];
                    pruR.Resultado28 = listTodo[27];
                    pruR.Resultado29 = listTodo[28];
                    pruR.Resultado30 = listTodo[29];

                    pruR.Resultado31 = listTodo[30];
                    pruR.Resultado32 = listTodo[31];
                    pruR.Resultado33 = listTodo[32];
                    pruR.Resultado34 = listTodo[33];
                    pruR.Resultado35 = listTodo[34];
                    pruR.Resultado36 = listTodo[35];
                    pruR.Resultado37 = listTodo[36];
                    pruR.Resultado38 = listTodo[37];
                    pruR.Resultado39 = listTodo[38];
                    pruR.Resultado40 = listTodo[39];

                    pruR.Resultado41 = listTodo[40];
                    pruR.Resultado42 = listTodo[41];
                    pruR.Resultado43 = listTodo[42];
                    pruR.Resultado44 = listTodo[43];
                    pruR.Resultado45 = listTodo[44];
                    pruR.Resultado46 = listTodo[45];
                    pruR.Resultado47 = listTodo[46];
                    pruR.Resultado48 = listTodo[47];
                    pruR.Resultado49 = listTodo[48];



                    pruR.Calificacion = label24.Text;
                    pruR.Cambcolor = label20.Text;
                    pruR.Duracion = label22.Text;
                    pruR.NRepeticiones = label12.Text;
                    pruR.NSaltos = label14.Text;
                    pruR.CdRect = label16.Text;
                    pruR.CdNoRect = label18.Text;
                    pruR.Variante = label5.Text;


                    entities.PruTRN.Add(pruR);

                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruTRN>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PTRN = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PTRN == null)
                    {

                        PruTRN pruR = new PruTRN();
                        pruR.Fecha = date;

                        pruR.Resultado1 = listTodo[0];
                        pruR.Resultado2 = listTodo[1];
                        pruR.Resultado3 = listTodo[2];
                        pruR.Resultado4 = listTodo[3];
                        pruR.Resultado5 = listTodo[4];
                        pruR.Resultado6 = listTodo[5];
                        pruR.Resultado7 = listTodo[6];
                        pruR.Resultado8 = listTodo[7];
                        pruR.Resultado9 = listTodo[8];
                        pruR.Resultado10 = listTodo[9];

                        pruR.Resultado11 = listTodo[10];
                        pruR.Resultado12 = listTodo[11];
                        pruR.Resultado13 = listTodo[12];
                        pruR.Resultado14 = listTodo[13];
                        pruR.Resultado15 = listTodo[14];
                        pruR.Resultado16 = listTodo[15];
                        pruR.Resultado17 = listTodo[16];
                        pruR.Resultado18 = listTodo[17];
                        pruR.Resultado19 = listTodo[18];
                        pruR.Resultado20 = listTodo[19];

                        pruR.Resultado21 = listTodo[20];
                        pruR.Resultado22 = listTodo[21];
                        pruR.Resultado23 = listTodo[22];
                        pruR.Resultado24 = listTodo[23];
                        pruR.Resultado25 = listTodo[24];
                        pruR.Resultado26 = listTodo[25];
                        pruR.Resultado27 = listTodo[26];
                        pruR.Resultado28 = listTodo[27];
                        pruR.Resultado29 = listTodo[28];
                        pruR.Resultado30 = listTodo[29];

                        pruR.Resultado31 = listTodo[30];
                        pruR.Resultado32 = listTodo[31];
                        pruR.Resultado33 = listTodo[32];
                        pruR.Resultado34 = listTodo[33];
                        pruR.Resultado35 = listTodo[34];
                        pruR.Resultado36 = listTodo[35];
                        pruR.Resultado37 = listTodo[36];
                        pruR.Resultado38 = listTodo[37];
                        pruR.Resultado39 = listTodo[38];
                        pruR.Resultado40 = listTodo[39];

                        pruR.Resultado41 = listTodo[40];
                        pruR.Resultado42 = listTodo[41];
                        pruR.Resultado43 = listTodo[42];
                        pruR.Resultado44 = listTodo[43];
                        pruR.Resultado45 = listTodo[44];
                        pruR.Resultado46 = listTodo[45];
                        pruR.Resultado47 = listTodo[46];
                        pruR.Resultado48 = listTodo[47];
                        pruR.Resultado49 = listTodo[48];

                        entities.PruTRN.Add(pruR);
                        entities.SaveChangesAsync();



                        var ultimo = entities.Set<PruTRN>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PTRN = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruTRN.Where(f => f.idTest == sujetoEva.PTRN).FirstOrDefault<PruTRN>();


                        conect.Fecha = date;

                        conect.Resultado1 = listTodo[0];
                        conect.Resultado2 = listTodo[1];
                        conect.Resultado3 = listTodo[2];
                        conect.Resultado4 = listTodo[3];
                        conect.Resultado5 = listTodo[4];
                        conect.Resultado6 = listTodo[5];
                        conect.Resultado7 = listTodo[6];
                        conect.Resultado8 = listTodo[7];
                        conect.Resultado9 = listTodo[8];
                        conect.Resultado10 = listTodo[9];

                        conect.Resultado11 = listTodo[10];
                        conect.Resultado12 = listTodo[11];
                        conect.Resultado13 = listTodo[12];
                        conect.Resultado14 = listTodo[13];
                        conect.Resultado15 = listTodo[14];
                        conect.Resultado16 = listTodo[15];
                        conect.Resultado17 = listTodo[16];
                        conect.Resultado18 = listTodo[17];
                        conect.Resultado19 = listTodo[18];
                        conect.Resultado20 = listTodo[19];

                        conect.Resultado21 = listTodo[20];
                        conect.Resultado22 = listTodo[21];
                        conect.Resultado23 = listTodo[22];
                        conect.Resultado24 = listTodo[23];
                        conect.Resultado25 = listTodo[24];
                        conect.Resultado26 = listTodo[25];
                        conect.Resultado27 = listTodo[26];
                        conect.Resultado28 = listTodo[27];
                        conect.Resultado29 = listTodo[28];
                        conect.Resultado30 = listTodo[29];

                        conect.Resultado31 = listTodo[30];
                        conect.Resultado32 = listTodo[31];
                        conect.Resultado33 = listTodo[32];
                        conect.Resultado34 = listTodo[33];
                        conect.Resultado35 = listTodo[34];
                        conect.Resultado36 = listTodo[35];
                        conect.Resultado37 = listTodo[36];
                        conect.Resultado38 = listTodo[37];
                        conect.Resultado39 = listTodo[38];
                        conect.Resultado40 = listTodo[39];

                        conect.Resultado41 = listTodo[40];
                        conect.Resultado42 = listTodo[41];
                        conect.Resultado43 = listTodo[42];
                        conect.Resultado44 = listTodo[43];
                        conect.Resultado45 = listTodo[44];
                        conect.Resultado46 = listTodo[45];
                        conect.Resultado47 = listTodo[46];
                        conect.Resultado48 = listTodo[47];
                        conect.Resultado49 = listTodo[48];


                        conect.Calificacion = label24.Text;
                        conect.Cambcolor = label24.Text;
                        conect.Duracion = label22.Text;
                        conect.NRepeticiones = label12.Text;
                        conect.NSaltos = label14.Text;
                        conect.CdRect = label16.Text;
                        conect.CdNoRect = label18.Text;
                        conect.Variante = label5.Text;


                        entities.SaveChangesAsync();

                    }
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (mainEntities db = new mainEntities())
                {
                    int id = Convert.ToInt32(idTestTodo);
                    SujetosEvaluados res = db.SujetosEvaluados.Where(s => s.idSujetoEvaluado == id).FirstOrDefault<SujetosEvaluados>();


                    res.PTRN = null;

                    if (res.PruTRN == null && res.PruTrsimple == null && res.PIped == null && res.PIdetem == null && res.PResanti == null && res.PTrcomples == null && res.PTrcomple == null && res.PRaven == null
                             && res.PEysenk == null && res.PWeil == null && res.PDomino == null && res.PIdareRasgo == null && res.PIdareSitua == null && res.PCatell == null
                             && res.P16pf == null && res.PCualiVolitiv == null && res.PMotivDepButt == null && res.PCualidMotivDepor == null && res.PActiAnteComp == null && res.PPoms == null && res.PAnsiedadCompetitiva == null
                             )
                    {
                        db.SujetosEvaluados.Remove(res);
                    }

                    db.SaveChangesAsync();

                    using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                    {
                        c.Open();
                        using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTRN where idTest='" + idTest + "'", c))
                        {
                            comm1.ExecuteNonQuery();

                        }

                    }





                    MessageBox.Show("Se ha eliminado la prueba", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == label26.Text).FirstOrDefault();
                String ci = res.NCarnetIdent;
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\TRN.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = label26.Text + " Tabla Rojo y Negra";

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
                            rng1.Text = " " + label4.Text;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + label26.Text;


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


                            if (label12.Text != null)
                            {
                            Bookmark ptoA = wordDoc.Bookmarks["A"];
                            Range A = ptoA.Range;
                            A.Text = " " + label12.Text;
                            }
                            else
                            {
                                Bookmark ptoA = wordDoc.Bookmarks["A"];
                                Range A = ptoA.Range;
                                A.Text = " ";
                            }



                            if (label14.Text != null)
                            {
                                Bookmark ptoB = wordDoc.Bookmarks["B"];
                                Range B = ptoB.Range;
                                B.Text = " " + label14.Text;
                            }
                            else
                            {
                                Bookmark ptoB = wordDoc.Bookmarks["B"];
                                Range B = ptoB.Range;
                                B.Text = " ";
                            }


                          

                            if (label16.Text != null)
                            {
                                Bookmark ptoC = wordDoc.Bookmarks["C"];
                                Range C = ptoC.Range;
                                C.Text = " " + label16.Text;
                            }
                            else
                            {
                                Bookmark ptoC = wordDoc.Bookmarks["C"];
                                Range C = ptoC.Range;
                                C.Text = " ";
                            }


                            if (label18.Text != null)
                            {
                                Bookmark ptoD = wordDoc.Bookmarks["D"];
                                Range D = ptoD.Range;
                                D.Text = " " + label18.Text;
                            }
                            else
                            {
                                Bookmark ptoD = wordDoc.Bookmarks["D"];
                                Range D = ptoD.Range;
                                D.Text = " ";
                            }




                            if (label20.Text != null)
                            {
                                Bookmark ptoE = wordDoc.Bookmarks["E1"];
                                Range E = ptoE.Range;
                                E.Text = " " + label20.Text;
                            }
                            else
                            {
                                Bookmark ptoE = wordDoc.Bookmarks["E1"];
                                Range E = ptoE.Range;
                                E.Text = " ";
                            }
                            



                            if (label22.Text != null)
                            {
                                Bookmark ptoTotal = wordDoc.Bookmarks["F1"];
                                Range pt = ptoTotal.Range;
                                pt.Text = " " + label22.Text;
                            }
                            else
                            {
                                Bookmark ptoTotal = wordDoc.Bookmarks["F1"];
                                Range pt = ptoTotal.Range;
                                pt.Text = " ";
                            }




                            if (label24.Text != null)
                            {
                                Bookmark ptoPor = wordDoc.Bookmarks["G1"];
                                Range porc = ptoPor.Range;
                                porc.Text = " " + label24.Text;
                            }
                            else
                            {
                                Bookmark ptoPor = wordDoc.Bookmarks["G1"];
                                Range porc = ptoPor.Range;
                                porc.Text = " ";
                            }
                            

                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();

                            esp.Close();
                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha ocurrido un error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }
    }
}
