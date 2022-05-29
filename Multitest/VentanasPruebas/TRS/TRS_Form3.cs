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
    public partial class TRS_Form3 : Form
    {
        string idTestTodo;
        string idTest;
        PruTrsimple pru = new PruTrsimple();
        string modalidad = "";
        string deporte = "";
        String edad = "";
        String etapa = "";

         

        public TRS_Form3(List<String> valores, double tmaximo, double tminimo, double sumatoria, double rcorrecta, double media, double desvEstand, double desviacion, double cantOmisiones, String nombreAtleta, int tAde, int mostrar, string color)

        {
            InitializeComponent();

            label2.Text = tmaximo.ToString();
            label4.Text = tminimo.ToString();
            label6.Text = sumatoria.ToString();
            label8.Text = rcorrecta.ToString();
            label10.Text = media.ToString();
            label12.Text = desvEstand.ToString();
            label14.Text = desviacion.ToString();
            label16.Text = cantOmisiones.ToString();
           
            Cursor.Show();
            llenarTabla(valores);
            label17.Text = nombreAtleta;
            label20.Text = "Estímulo " + color;

            if (mostrar == 0)
            {
                tableLayoutPanel11.Visible = false;
            }
        }

        public TRS_Form3(String atleta, String idTest, bool res, string idTestTodo, String idAtleta)
        {
            InitializeComponent();
            label17.Text = atleta;
            this.idTestTodo = idTestTodo;
            this.idTest = idTest;
            buscarPrueba(idTest);

            if (idAtleta != null)
            {
                buscarAtleta(idAtleta);
                buscarEtapa();
            }

            simpleButton1.Visible = res ? true : false;
            button1.Visible = res ? true : false;

        }



        public void mostrarCartel(object sender, DoWorkEventArgs e)
        {

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

                var test = entities.PruTrsimple.Find(Convert.ToInt32(idTest));
                label2.Text = test.TiempoMaximo;
                label4.Text = test.TiempoMinimo;
                label6.Text = test.SumTiempo;
                label8.Text = test.RespCorrecta;
                label10.Text = test.TiempoMedio;
                label12.Text = test.DesvStandar;
                label14.Text = test.CoefVariacion;
                label16.Text = test.CantOmisiones;
              
                label20.Text = "Estímulo: " + test.TipoEstimulo;

                pru.TiempoMaximo = test.TiempoMaximo;
                pru.TiempoMinimo = test.TiempoMinimo;
                pru.SumTiempo = test.SumTiempo;
                pru.RespCorrecta = test.RespCorrecta;
                pru.TiempoMedio = test.TiempoMedio;
                pru.DesvStandar = test.DesvStandar;
                pru.CoefVariacion = test.CoefVariacion;
                pru.CantOmisiones = test.CantOmisiones;
                pru.CantAdel = test.CantAdel;
                pru.Fecha = test.Fecha;
                pru.TipoEstimulo = test.TipoEstimulo;

                List<String> valores = new List<string>();


                if (test.Tiempo1 != null)
                {
                    valores.Add(test.Tiempo1);
                    pru.Tiempo1 = test.Tiempo1;
                }
                if (test.Tiempo2 != null)
                {
                    valores.Add(test.Tiempo2);
                    pru.Tiempo2 = test.Tiempo2;
                }
                if (test.Tiempo3 != null)
                {
                    valores.Add(test.Tiempo3);
                    pru.Tiempo3 = test.Tiempo3;
                }
                if (test.Tiempo4 != null)
                {
                    valores.Add(test.Tiempo4);
                    pru.Tiempo4 = test.Tiempo4;
                }
                if (test.Tiempo5 != null)
                {
                    valores.Add(test.Tiempo5);
                    pru.Tiempo5 = test.Tiempo5;
                }
                if (test.Tiempo6 != null)
                {
                    valores.Add(test.Tiempo6);
                    pru.Tiempo6 = test.Tiempo6;
                }
                if (test.Tiempo7 != null)
                {
                    valores.Add(test.Tiempo7);
                    pru.Tiempo7 = test.Tiempo7;
                }
                if (test.Tiempo8 != null)
                {
                    valores.Add(test.Tiempo8);
                    pru.Tiempo8 = test.Tiempo8;
                }
                if (test.Tiempo9 != null)
                {
                    valores.Add(test.Tiempo9);
                    pru.Tiempo9 = test.Tiempo9;
                }
                if (test.Tiempo10 != null)
                {
                    valores.Add(test.Tiempo10);
                    pru.Tiempo10 = test.Tiempo10;
                }
                if (test.Tiempo11 != null)
                {
                    valores.Add(test.Tiempo11);
                    pru.Tiempo11 = test.Tiempo11;
                }
                if (test.Tiempo12 != null)
                {
                    valores.Add(test.Tiempo12);
                    pru.Tiempo12 = test.Tiempo12;
                }
                if (test.Tiempo13 != null)
                {
                    valores.Add(test.Tiempo13);
                    pru.Tiempo13 = test.Tiempo13;
                }
                if (test.Tiempo14 != null)
                {
                    valores.Add(test.Tiempo14);
                    pru.Tiempo14 = test.Tiempo14;
                }
                if (test.Tiempo15 != null)
                {
                    valores.Add(test.Tiempo15);
                    pru.Tiempo15 = test.Tiempo15;
                }
                if (test.Tiempo16 != null)
                {
                    valores.Add(test.Tiempo16);
                    pru.Tiempo16 = test.Tiempo16;
                }
                if (test.Tiempo17 != null)
                {
                    valores.Add(test.Tiempo17);
                    pru.Tiempo17 = test.Tiempo17;
                }
                if (test.Tiempo18 != null)
                {
                    valores.Add(test.Tiempo18);
                    pru.Tiempo18 = test.Tiempo18;
                }
                if (test.Tiempo19 != null)
                {
                    valores.Add(test.Tiempo19);
                    pru.Tiempo19 = test.Tiempo19;
                }
                if (test.Tiempo20 != null)
                {
                    valores.Add(test.Tiempo20);
                    pru.Tiempo20 = test.Tiempo20;
                }
                if (test.Tiempo21 != null)
                {
                    valores.Add(test.Tiempo21);
                    pru.Tiempo21 = test.Tiempo21;
                }
                if (test.Tiempo22 != null)
                {
                    valores.Add(test.Tiempo22);
                    pru.Tiempo22 = test.Tiempo22;
                }
                if (test.Tiempo23 != null)
                {
                    valores.Add(test.Tiempo23);
                    pru.Tiempo23 = test.Tiempo23;
                }
                if (test.Tiempo24 != null)
                {
                    valores.Add(test.Tiempo24);
                    pru.Tiempo24 = test.Tiempo24;
                }
                if (test.Tiempo25 != null)
                {
                    valores.Add(test.Tiempo25);
                    pru.Tiempo25 = test.Tiempo25;
                }
                if (test.Tiempo26 != null)
                {
                    valores.Add(test.Tiempo26);
                    pru.Tiempo26 = test.Tiempo26;
                }
                if (test.Tiempo27 != null)
                {
                    valores.Add(test.Tiempo27);
                    pru.Tiempo27 = test.Tiempo27;
                }
                if (test.Tiempo28 != null)
                {
                    valores.Add(test.Tiempo28);
                    pru.Tiempo28 = test.Tiempo28;
                }
                if (test.Tiempo29 != null)
                {
                    valores.Add(test.Tiempo29);
                    pru.Tiempo29 = test.Tiempo29;
                }
                if (test.Tiempo30 != null)
                {
                    valores.Add(test.Tiempo30);
                    pru.Tiempo30 = test.Tiempo30;
                }

                llenarTabla(valores);


            }
        }

        private void llenarTabla(List<String> valores)
        {

            for (int i = 0; i < valores.Count; i++)
            {
                String valor = valores[i].ToString();

                dataGridView1.Rows.Add(new object[] {
                    i+1,
                   valor
                    });

                if (valor != "Omisión")
                {
                    if (Convert.ToDouble(valores[i]) < 0)
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
                }

            }





        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TRS_Form3_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (mainEntities db = new mainEntities())
                {
                    int id = Convert.ToInt32(idTestTodo);
                    SujetosEvaluados res = db.SujetosEvaluados.Where(s => s.idSujetoEvaluado == id).FirstOrDefault<SujetosEvaluados>();

                    res.PruTrsimple = null;

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
                        using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrsimple where idTest='" + idTest + "'", c))
                        {
                            comm1.ExecuteNonQuery();

                        }

                    }

                    MessageBox.Show("Se ha eliminado la prueba", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }



        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == label17.Text).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\TRS.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = label17.Text + " Tiempo de Reacción Simple";

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
                            rng1.Text = " " + pru.Fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + label17.Text;


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



                            Bookmark ptoA = wordDoc.Bookmarks["A"];
                            Range A = ptoA.Range;
                            A.Text = " " + pru.TiempoMaximo;


                            Bookmark ptoB = wordDoc.Bookmarks["B"];
                            Range B = ptoB.Range;
                            B.Text = " " + pru.TiempoMinimo;


                            Bookmark ptoC = wordDoc.Bookmarks["C"];
                            Range C = ptoC.Range;
                            C.Text = " " + pru.SumTiempo;


                            Bookmark ptoD = wordDoc.Bookmarks["D"];
                            Range D = ptoD.Range;
                            D.Text = " " + pru.RespCorrecta;


                            Bookmark ptoE = wordDoc.Bookmarks["E"];
                            Range E = ptoE.Range;
                            E.Text = " " + pru.TiempoMedio;


                            Bookmark ptoTotal = wordDoc.Bookmarks["F"];
                            Range pt = ptoTotal.Range;
                            pt.Text = " " + pru.DesvStandar;


                            Bookmark ptoPor = wordDoc.Bookmarks["G"];
                            Range porc = ptoPor.Range;
                            porc.Text = " " + pru.CoefVariacion;



                            Bookmark ptoRa = wordDoc.Bookmarks["H"];
                            Range porR = ptoRa.Range;
                            porR.Text = " " + pru.CantOmisiones;



                            Bookmark ptoDiw = wordDoc.Bookmarks["es"];
                            Range porDw = ptoDiw.Range;
                            porDw.Text = " " + pru.TipoEstimulo;


                            {
                                //Tiempos
                                if (pru.Tiempo1 != null)
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = " " + pru.Tiempo1;
                                }
                                else
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = "";
                                }

                                if (pru.Tiempo2 != null)
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " " + pru.Tiempo2;
                                }
                                else
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = "";
                                }


                                if (pru.Tiempo3 != null)
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " " + pru.Tiempo3;
                                }
                                else
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = "";
                                }

                                if (pru.Tiempo4 != null)
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " " + pru.Tiempo4;
                                }
                                else
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = "";
                                }

                                if (pru.Tiempo5 != null)
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " " + pru.Tiempo5;
                                }
                                else
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = "";
                                }


                                if (pru.Tiempo6 != null)
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " " + pru.Tiempo6;
                                }
                                else
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = "";
                                }

                                if (pru.Tiempo7 != null)
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " " + pru.Tiempo7;
                                }
                                else
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = "";
                                }

                                if (pru.Tiempo8 != null)
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " " + pru.Tiempo8;
                                }
                                else
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = "";
                                }

                                if (pru.Tiempo9 != null)
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " " + pru.Tiempo9;
                                }
                                else
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = "";
                                }


                                if (pru.Tiempo10 != null)
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " " + pru.Tiempo10;
                                }
                                else
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = "";
                                }



                                if (pru.Tiempo11 != null)
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " " + pru.Tiempo11;
                                }
                                else
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = "";
                                }


                                if (pru.Tiempo12 != null)
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = " " + pru.Tiempo12;
                                }
                                else
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = "";
                                }


                                if (pru.Tiempo13 != null)
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = " " + pru.Tiempo13;
                                }
                                else
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = "";
                                }


                                if (pru.Tiempo14 != null)
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = " " + pru.Tiempo14;
                                }
                                else
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = "";
                                }


                                if (pru.Tiempo15 != null)
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = " " + pru.Tiempo15;
                                }
                                else
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = "";
                                }


                                if (pru.Tiempo16 != null)
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = " " + pru.Tiempo16;
                                }
                                else
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = "";
                                }


                                if (pru.Tiempo17 != null)
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = " " + pru.Tiempo17;
                                }
                                else
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = "";
                                }

                                if (pru.Tiempo18 != null)
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = " " + pru.Tiempo18;
                                }
                                else
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = "";
                                }


                                if (pru.Tiempo19 != null)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + pru.Tiempo19;
                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = "";
                                }

                                if (pru.Tiempo20 != null)
                                {
                                    Bookmark t20 = wordDoc.Bookmarks["T20"];
                                    Range t20r = t20.Range;
                                    t20r.Text = " " + pru.Tiempo20;
                                }
                                else
                                {
                                    Bookmark t20 = wordDoc.Bookmarks["T20"];
                                    Range t20r = t20.Range;
                                    t20r.Text = "";
                                }


                                if (pru.Tiempo21 != null)
                                {
                                    Bookmark t21 = wordDoc.Bookmarks["T21"];
                                    Range t21r = t21.Range;
                                    t21r.Text = " " + pru.Tiempo21;
                                }
                                else
                                {
                                    Bookmark t21 = wordDoc.Bookmarks["T21"];
                                    Range t21r = t21.Range;
                                    t21r.Text = "";
                                }


                                if (pru.Tiempo22 != null)
                                {
                                    Bookmark t22 = wordDoc.Bookmarks["T22"];
                                    Range t22r = t22.Range;
                                    t22r.Text = " " + pru.Tiempo22;
                                }
                                else
                                {
                                    Bookmark t22 = wordDoc.Bookmarks["T22"];
                                    Range t22r = t22.Range;
                                    t22r.Text = "";
                                }

                                if (pru.Tiempo23 != null)
                                {
                                    Bookmark t23 = wordDoc.Bookmarks["T23"];
                                    Range t23r = t23.Range;
                                    t23r.Text = " " + pru.Tiempo23;
                                }
                                else
                                {
                                    Bookmark t23 = wordDoc.Bookmarks["T23"];
                                    Range t23r = t23.Range;
                                    t23r.Text = "";
                                }

                                if (pru.Tiempo24 != null)
                                {
                                    Bookmark t24 = wordDoc.Bookmarks["T24"];
                                    Range t24r = t24.Range;
                                    t24r.Text = " " + pru.Tiempo24;
                                }
                                else
                                {
                                    Bookmark t24 = wordDoc.Bookmarks["T24"];
                                    Range t24r = t24.Range;
                                    t24r.Text = "";
                                }


                                if (pru.Tiempo25 != null)
                                {
                                    Bookmark t25 = wordDoc.Bookmarks["T25"];
                                    Range t25r = t25.Range;
                                    t25r.Text = " " + pru.Tiempo25;
                                }
                                else
                                {
                                    Bookmark t25 = wordDoc.Bookmarks["T25"];
                                    Range t25r = t25.Range;
                                    t25r.Text = "";
                                }


                                if (pru.Tiempo26 != null)
                                {
                                    Bookmark t26 = wordDoc.Bookmarks["T26"];
                                    Range t26r = t26.Range;
                                    t26r.Text = " " + pru.Tiempo26;
                                }
                                else
                                {
                                    Bookmark t26 = wordDoc.Bookmarks["T26"];
                                    Range t26r = t26.Range;
                                    t26r.Text = "";
                                }

                                if (pru.Tiempo27 != null)
                                {
                                    Bookmark t27 = wordDoc.Bookmarks["T27"];
                                    Range t27r = t27.Range;
                                    t27r.Text = " " + pru.Tiempo27;
                                }
                                else
                                {
                                    Bookmark t27 = wordDoc.Bookmarks["T27"];
                                    Range t27r = t27.Range;
                                    t27r.Text = "";
                                }

                                if (pru.Tiempo28 != null)
                                {
                                    Bookmark t28 = wordDoc.Bookmarks["T28"];
                                    Range t28r = t28.Range;
                                    t28r.Text = " " + pru.Tiempo28;
                                }
                                else
                                {
                                    Bookmark t28 = wordDoc.Bookmarks["T28"];
                                    Range t28r = t28.Range;
                                    t28r.Text = "";
                                }

                                if (pru.Tiempo29 != null)
                                {
                                    Bookmark t29 = wordDoc.Bookmarks["T29"];
                                    Range t29r = t29.Range;
                                    t29r.Text = " " + pru.Tiempo29;
                                }
                                else
                                {
                                    Bookmark t29 = wordDoc.Bookmarks["T29"];
                                    Range t29r = t29.Range;
                                    t29r.Text = "";
                                }

                                if (pru.Tiempo30 != null)
                                {
                                    Bookmark t30 = wordDoc.Bookmarks["T30"];
                                    Range t30r = t30.Range;
                                    t30r.Text = " " + pru.Tiempo30;
                                }
                                else
                                {
                                    Bookmark t30 = wordDoc.Bookmarks["T30"];
                                    Range t30r = t30.Range;
                                    t30r.Text = "";
                                }

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

                    MessageBox.Show("Ha ocurrido un error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }


    }
}
