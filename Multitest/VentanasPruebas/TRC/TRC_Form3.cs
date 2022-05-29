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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest
{
    public partial class TRC_Form3 : Form
    {
        private List<Result> valores;
        public String carnet;
        public String nombreAtleta;
        public String idTest;
        string idUser;
        string etapa;
        String idTestTodo;
        bool sonido;
        String deporte;
        String edad;
        string modalidad;
        PruTrcomple pru = new PruTrcomple();

        public TRC_Form3(String variate, List<Result> valores, bool sonido, String id, string etapa, String variante, String nombreAtleta)
        {

            InitializeComponent();


            this.idUser = id;
            this.etapa = etapa;
            label32.Text = nombreAtleta;
            this.sonido = sonido;
            if (!sonido)
            {
                label22.Visible = false;
                label25.Visible = false;
            }

            calificar(valores, variate, sonido);


        }


        public TRC_Form3(String nombreAtleta, String idTest, bool sonido, String idTestTodo, string idAtleta)
        {
            InitializeComponent();
            this.idTest = idTest;
            label32.Text = nombreAtleta;
            this.idTestTodo = idTestTodo;
            this.idTest = idTest;
            if (sonido == true)
            {
                label22.Visible = true;
                label25.Visible = true;
                label27.Text = "Con sonido";
            }

            else
            {
                label27.Text = "Sin sonido";
                label22.Visible = false;
                label25.Visible = false;
            }





            if (idTestTodo != null)
            {
                button1.Visible = true;
                button2.Visible = true;
            }

            if (idAtleta != null)
            {
                buscarAtleta(idAtleta);
                buscarEtapa();
            }

            llenarCampos();
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

        private void llenarCampos()
        {
            using (mainEntities entities = new mainEntities())
            {

                var test = entities.PruTrcomple.Find(Convert.ToInt32(idTest));
                label2.Text = test.CantEstimulos.ToString();
                addValueToArray(test);

                int i = 0;
                foreach (var res in valores)
                {
                    dataGridView1.Rows.Add(new object[] {
                              i+1,
                              res.colorPantalla,
                              res.colorArduino,
                              res.tiempo
                               });


                    if (res.sonido)
                    {
                        if (res.colorPantalla != res.colorArduino && res.colorPantalla == "-")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;

                        if (res.colorPantalla != res.colorArduino && res.colorArduino == "Omisión")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Tan;

                        if (res.colorPantalla != res.colorArduino && res.colorArduino != "Omisión" && res.colorArduino != "-")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;

                        if (res.colorArduino != "-" && res.tiempo != "-")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                    }
                    else
                    {

                        if (res.colorPantalla != res.colorArduino && res.colorPantalla == "-")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;

                        if (res.colorPantalla != res.colorArduino && res.colorArduino == "Omisión")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Tan;

                        if (res.colorPantalla != res.colorArduino && res.colorArduino != "Omisión" && res.colorPantalla != "-")
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;


                    }



                    //if (res.colorPantalla != res.colorArduino)
                    //{
                    //    if (res.colorPantalla == "-")
                    //    {
                    //        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                    //    }
                    //    else
                    //    {
                    //        if (res.colorArduino == "Omisión")
                    //        {
                    //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Tan;
                    //        }

                    //        if (res.tiempo != "-" && res.colorArduino != "-")
                    //            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
                    //    }
                    //}
                    i++;
                }

                label6.Text = test.TiempoMaximo == "NaN" || test.TiempoMaximo == "" ? "0" : test.TiempoMaximo;
                label8.Text = test.TiempoMinimo == "NaN" || test.TiempoMinimo == "" ? "0" : test.TiempoMinimo;
                label10.Text = test.SumTiempo == "NaN" || test.SumTiempo == "" ? "0" : test.SumTiempo;
                label12.Text = test.RespCorrecta == "NaN" || test.RespCorrecta == "" ? "0" : test.RespCorrecta;
                label17.Text = test.TiempoMedio == "NaN" || test.TiempoMedio == "" ? "0" : test.TiempoMedio;
                label18.Text = test.DesvStandar == "NaN" || test.DesvStandar == "" ? "0" : test.DesvStandar;
                label19.Text = test.CoefVariacion == "NaN" || test.CoefVariacion == "" ? "0" : test.CoefVariacion;
                label20.Text = test.CantOmisiones == "NaN" || test.CantOmisiones == "" ? "0" : test.CantOmisiones;
                label24.Text = test.CantErrorColor == "NaN" || test.CantErrorColor == "" ? "0" : test.CantErrorColor;
                label25.Text = test.Canterroresson == "NaN" || test.Canterroresson == "" ? "0" : test.Canterroresson;
                label4.Text = test.Variante == "NaN" || test.Variante == "" ? "0" : test.Variante;
                //  label34.Text = test.CantRespAdelant == "NaN" || test.CantRespAdelant == null ? "0" : test.CantRespAdelant;

                pru.TiempoMaximo = label6.Text;
                pru.TiempoMinimo = label8.Text;
                pru.SumTiempo = label10.Text;
                pru.RespCorrecta = label12.Text;
                pru.TiempoMedio = label17.Text;
                pru.DesvStandar = label18.Text;
                pru.CoefVariacion = label19.Text;
                pru.CantOmisiones = label20.Text;
                pru.CantErrorColor = label24.Text;
                pru.Canterroresson = label25.Text;
                pru.Variante = label4.Text;
                //  pru.CantRespAdelant = label34.Text;
                pru.Fecha = test.Fecha;

            }
        }

        private void addValueToArray(PruTrcomple test)
        {


            valores = new List<Result>();

            Result res1 = new Result(test.Sonido1 != null ? bool.Parse(test.Sonido1) : false, test.Estimulo1, test.Respuesta1, test.Tiempo1);
            Result res2 = new Result(test.Sonido2 != null ? bool.Parse(test.Sonido2) : false, test.Estimulo2, test.Respuesta2, test.Tiempo2);
            Result res3 = new Result(test.Sonido3 != null ? bool.Parse(test.Sonido3) : false, test.Estimulo3, test.Respuesta3, test.Tiempo3);
            Result res4 = new Result(test.Sonido4 != null ? bool.Parse(test.Sonido4) : false, test.Estimulo4, test.Respuesta4, test.Tiempo4);
            Result res5 = new Result(test.Sonido5 != null ? bool.Parse(test.Sonido5) : false, test.Estimulo5, test.Respuesta5, test.Tiempo5);
            Result res6 = new Result(test.Sonido6 != null ? bool.Parse(test.Sonido6) : false, test.Estimulo6, test.Respuesta6, test.Tiempo6);
            Result res7 = new Result(test.Sonido7 != null ? bool.Parse(test.Sonido7) : false, test.Estimulo7, test.Respuesta7, test.Tiempo7);
            Result res8 = new Result(test.Sonido8 != null ? bool.Parse(test.Sonido8) : false, test.Estimulo8, test.Respuesta8, test.Tiempo8);
            Result res9 = new Result(test.Sonido9 != null ? bool.Parse(test.Sonido9) : false, test.Estimulo9, test.Respuesta9, test.Tiempo9);
            Result res10 = new Result(test.Sonido10 != null ? bool.Parse(test.Sonido10) : false, test.Estimulo10, test.Respuesta10, test.Tiempo10);


            Result res11 = new Result(test.Sonido11 != null ? bool.Parse(test.Sonido11) : false, test.Estimulo11, test.Respuesta11, test.Tiempo11);
            Result res12 = new Result(test.Sonido12 != null ? bool.Parse(test.Sonido12) : false, test.Estimulo12, test.Respuesta12, test.Tiempo12);
            Result res13 = new Result(test.Sonido13 != null ? bool.Parse(test.Sonido13) : false, test.Estimulo13, test.Respuesta13, test.Tiempo13);
            Result res14 = new Result(test.Sonido14 != null ? bool.Parse(test.Sonido14) : false, test.Estimulo14, test.Respuesta14, test.Tiempo14);
            Result res15 = new Result(test.Sonido15 != null ? bool.Parse(test.Sonido15) : false, test.Estimulo15, test.Respuesta15, test.Tiempo15);
            Result res16 = new Result(test.Sonido16 != null ? bool.Parse(test.Sonido16) : false, test.Estimulo16, test.Respuesta16, test.Tiempo16);
            Result res17 = new Result(test.Sonido17 != null ? bool.Parse(test.Sonido17) : false, test.Estimulo17, test.Respuesta17, test.Tiempo17);
            Result res18 = new Result(test.Sonido18 != null ? bool.Parse(test.Sonido18) : false, test.Estimulo18, test.Respuesta18, test.Tiempo18);
            Result res19 = new Result(test.Sonido19 != null ? bool.Parse(test.Sonido19) : false, test.Estimulo19, test.Respuesta19, test.Tiempo19);
            Result res20 = new Result(test.Sonido20 != null ? bool.Parse(test.Sonido20) : false, test.Estimulo20, test.Respuesta20, test.Tiempo20);



            Result res21 = new Result(test.Sonido21 != null ? bool.Parse(test.Sonido21) : false, test.Estimulo21, test.Respuesta21, test.Tiempo21);
            Result res22 = new Result(test.Sonido22 != null ? bool.Parse(test.Sonido22) : false, test.Estimulo22, test.Respuesta22, test.Tiempo22);
            Result res23 = new Result(test.Sonido23 != null ? bool.Parse(test.Sonido23) : false, test.Estimulo23, test.Respuesta23, test.Tiempo23);
            Result res24 = new Result(test.Sonido24 != null ? bool.Parse(test.Sonido24) : false, test.Estimulo24, test.Respuesta24, test.Tiempo24);
            Result res25 = new Result(test.Sonido25 != null ? bool.Parse(test.Sonido25) : false, test.Estimulo25, test.Respuesta25, test.Tiempo25);
            Result res26 = new Result(test.Sonido26 != null ? bool.Parse(test.Sonido26) : false, test.Estimulo26, test.Respuesta26, test.Tiempo26);
            Result res27 = new Result(test.Sonido27 != null ? bool.Parse(test.Sonido27) : false, test.Estimulo27, test.Respuesta27, test.Tiempo27);
            Result res28 = new Result(test.Sonido28 != null ? bool.Parse(test.Sonido28) : false, test.Estimulo28, test.Respuesta28, test.Tiempo28);
            Result res29 = new Result(test.Sonido29 != null ? bool.Parse(test.Sonido29) : false, test.Estimulo29, test.Respuesta29, test.Tiempo29);
            Result res30 = new Result(test.Sonido30 != null ? bool.Parse(test.Sonido30) : false, test.Estimulo30, test.Respuesta30, test.Tiempo30);

            if (res1.tiempo != null && res1.colorArduino != null && res1.colorPantalla != null)
                valores.Add(res1);
            if (res2.tiempo != null && res2.colorArduino != null && res2.colorPantalla != null)
                valores.Add(res2);
            if (res3.tiempo != null && res3.colorArduino != null && res3.colorPantalla != null)
                valores.Add(res3);
            if (res4.tiempo != null && res4.colorArduino != null && res4.colorPantalla != null)
                valores.Add(res4);
            if (res5.tiempo != null && res5.colorArduino != null && res5.colorPantalla != null)
                valores.Add(res5);
            if (res6.tiempo != null && res6.colorArduino != null && res6.colorPantalla != null)
                valores.Add(res6);
            if (res7.tiempo != null && res7.colorArduino != null && res7.colorPantalla != null)
                valores.Add(res7);
            if (res8.tiempo != null && res8.colorArduino != null && res8.colorPantalla != null)
                valores.Add(res8);
            if (res9.tiempo != null && res9.colorArduino != null && res9.colorPantalla != null)
                valores.Add(res9);
            if (res10.tiempo != null && res10.colorArduino != null && res10.colorPantalla != null)
                valores.Add(res10);
            if (res11.tiempo != null && res11.colorArduino != null && res11.colorPantalla != null)
                valores.Add(res11);
            if (res12.tiempo != null && res12.colorArduino != null && res12.colorPantalla != null)
                valores.Add(res12);
            if (res13.tiempo != null && res13.colorArduino != null && res13.colorPantalla != null)
                valores.Add(res13);
            if (res14.tiempo != null && res14.colorArduino != null && res14.colorPantalla != null)
                valores.Add(res14);
            if (res15.tiempo != null && res15.colorArduino != null && res15.colorPantalla != null)
                valores.Add(res15);
            if (res16.tiempo != null && res16.colorArduino != null && res16.colorPantalla != null)
                valores.Add(res16);
            if (res17.tiempo != null && res17.colorArduino != null && res17.colorPantalla != null)
                valores.Add(res17);
            if (res18.tiempo != null && res18.colorArduino != null && res18.colorPantalla != null)
                valores.Add(res18);
            if (res19.tiempo != null && res19.colorArduino != null && res19.colorPantalla != null)
                valores.Add(res19);
            if (res20.tiempo != null && res20.colorArduino != null && res20.colorPantalla != null)
                valores.Add(res20);
            if (res21.tiempo != null && res21.colorArduino != null && res21.colorPantalla != null)
                valores.Add(res21);
            if (res22.tiempo != null && res22.colorArduino != null && res22.colorPantalla != null)
                valores.Add(res22);
            if (res23.tiempo != null && res23.colorArduino != null && res23.colorPantalla != null)
                valores.Add(res23);
            if (res24.tiempo != null && res24.colorArduino != null && res24.colorPantalla != null)
                valores.Add(res24);
            if (res25.tiempo != null && res25.colorArduino != null && res25.colorPantalla != null)
                valores.Add(res25);
            if (res26.tiempo != null && res26.colorArduino != null && res26.colorPantalla != null)
                valores.Add(res26);
            if (res27.tiempo != null && res27.colorArduino != null && res27.colorPantalla != null)
                valores.Add(res27);
            if (res28.tiempo != null && res28.colorArduino != null && res28.colorPantalla != null)
                valores.Add(res28);
            if (res29.tiempo != null && res29.colorArduino != null && res29.colorPantalla != null)
                valores.Add(res29);
            if (res30.tiempo != null && res30.colorArduino != null && res30.colorPantalla != null)
                valores.Add(res30);



        }

        private void calificar(List<Result> valores, String variate, bool sonido)
        {
            double maxT = 0;
            double minT = 100000;
            double sumatoria = 0;
            int omision = 0;
            int cantidadValores = 0;
            int rptaCorrecta = 0;
            int errorColor = 0;
            int rptaCorrectaMedia = 0;
            int cantErroresSon = 0;
            int respAnticipada = 0;


            for (int i = 0; i < valores.Count; i++)
            {
                Result res = valores[i];
                string t = res.tiempo;
                if (t != "-")
                {
                    if (res.sonido != true)
                    {
                        double temp = Convert.ToDouble(res.tiempo);


                        dataGridView1.Rows.Add(new object[] {
                              i+1,
                              res.colorPantalla,
                              res.colorArduino,
                              res.tiempo
                               });



                        if (res.colorPantalla != res.colorArduino)
                        {
                            if (res.colorPantalla == "-")
                            {
                                respAnticipada++;
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                            }
                            else
                            {
                                errorColor++;
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
                            }
                        }
                        else
                        {
                            if (temp > maxT)
                                maxT = temp;

                            if (temp < minT)
                                minT = temp;

                            rptaCorrectaMedia++;
                            rptaCorrecta++;
                            sumatoria += temp;
                        }


                        cantidadValores++;
                    }
                    else
                    {



                        if (res.tiempo != "-")
                        {
                            dataGridView1.Rows.Add(new object[] {
                              i+1,
                              res.colorPantalla,
                              res.colorArduino,
                              res.tiempo
                               });
                            cantErroresSon++;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                        }
                        else
                        {
                            dataGridView1.Rows.Add(new object[] {
                              i+1,
                              res.colorPantalla,
                              "-",
                              "-"
                               });
                            rptaCorrecta++;

                        }


                    }
                }
                else
                {
                    if (res.sonido == true)
                    {
                        if (res.colorArduino == "-" && res.tiempo == "-")
                        {
                            dataGridView1.Rows.Add(new object[] {
                              i+1,
                              res.colorPantalla,
                              res.colorArduino,
                              res.tiempo
                               });
                            rptaCorrecta++;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Add(new object[] {
                              i+1,
                              res.colorPantalla,
                              "Omisión",
                              "Omisión"
                               });
                        res.colorArduino = "Omisión";
                        res.tiempo = "Omisión";
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Tan;
                        omision++;
                    }
                }


            }



            if (sonido == true)
                label27.Text = "Con sonido";
            else
                label27.Text = "Sin sonido";

            double media = Math.Round(sumatoria / rptaCorrectaMedia, 2);
            label2.Text = valores.Count.ToString();
            label4.Text = variate;
            label6.Text = maxT.ToString();

            if (minT != 100000)
                label8.Text = minT.ToString();
            else
                label8.Text = "0";

            //   label34.Text = respAnticipada.ToString();

            label10.Text = Math.Round(sumatoria, 2).ToString();
            label12.Text = rptaCorrecta.ToString();


            label20.Text = omision.ToString();
            label25.Text = cantErroresSon.ToString();
            label24.Text = errorColor.ToString();

            double desviacionEstandt = desviacionEstandart(media, valores);

            if (desviacionEstandt.ToString() != "NaN")
            {
                label18.Text = Math.Round(desviacionEstandt, 2).ToString();
                label19.Text = Math.Round(desviacionEstandt / media, 2).ToString();
                label17.Text = media.ToString();
            }
            else
            {
                label18.Text = "0";
                label19.Text = "0";
                label17.Text = "0";
            }



            //---------------------------------------------------------//

            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            using (mainEntities entities = new mainEntities())
            {


                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {

                    PruTrcomple pruR = new PruTrcomple();

                    pruR.Fecha = date.ToString();

                    pruR.Tiempo1 = valores.Count > 0 ? valores[0].tiempo : null;
                    pruR.Respuesta1 = valores.Count > 0 ? valores[0].colorArduino : null;
                    pruR.Estimulo1 = valores.Count > 0 ? valores[0].colorPantalla : null;
                    pruR.Sonido1 = valores.Count > 0 ? valores[0].sonido.ToString() : null;

                    pruR.Tiempo2 = valores.Count > 1 ? valores[1].tiempo : null;
                    pruR.Respuesta2 = valores.Count > 1 ? valores[1].colorArduino : null;
                    pruR.Estimulo2 = valores.Count > 1 ? valores[1].colorPantalla : null;
                    pruR.Sonido2 = valores.Count > 1 ? valores[1].sonido.ToString() : null;


                    pruR.Tiempo3 = valores.Count > 2 ? valores[2].tiempo : null;
                    pruR.Respuesta3 = valores.Count > 2 ? valores[2].colorArduino : null;
                    pruR.Estimulo3 = valores.Count > 2 ? valores[2].colorPantalla : null;
                    pruR.Sonido3 = valores.Count > 2 ? valores[2].sonido.ToString() : null;

                    pruR.Tiempo4 = valores.Count > 3 ? valores[3].tiempo : null;
                    pruR.Respuesta4 = valores.Count > 3 ? valores[3].colorArduino : null;
                    pruR.Estimulo4 = valores.Count > 3 ? valores[3].colorPantalla : null;
                    pruR.Sonido4 = valores.Count > 3 ? valores[3].sonido.ToString() : null;

                    pruR.Tiempo5 = valores.Count > 4 ? valores[4].tiempo : null;
                    pruR.Respuesta5 = valores.Count > 4 ? valores[4].colorArduino : null;
                    pruR.Estimulo5 = valores.Count > 4 ? valores[4].colorPantalla : null;
                    pruR.Sonido5 = valores.Count > 4 ? valores[4].sonido.ToString() : null;

                    pruR.Tiempo6 = valores.Count > 5 ? valores[5].tiempo : null;
                    pruR.Respuesta6 = valores.Count > 5 ? valores[5].colorArduino : null;
                    pruR.Estimulo6 = valores.Count > 5 ? valores[5].colorPantalla : null;
                    pruR.Sonido6 = valores.Count > 5 ? valores[5].sonido.ToString() : null;

                    pruR.Tiempo7 = valores.Count > 6 ? valores[6].tiempo : null;
                    pruR.Respuesta7 = valores.Count > 6 ? valores[6].colorArduino : null;
                    pruR.Estimulo7 = valores.Count > 6 ? valores[6].colorPantalla : null;
                    pruR.Sonido7 = valores.Count > 6 ? valores[6].sonido.ToString() : null;

                    pruR.Tiempo8 = valores.Count > 7 ? valores[7].tiempo : null;
                    pruR.Respuesta8 = valores.Count > 7 ? valores[7].colorArduino : null;
                    pruR.Estimulo8 = valores.Count > 7 ? valores[7].colorPantalla : null;
                    pruR.Sonido8 = valores.Count > 7 ? valores[7].sonido.ToString() : null;

                    pruR.Tiempo9 = valores.Count > 8 ? valores[8].tiempo : null;
                    pruR.Respuesta9 = valores.Count > 8 ? valores[8].colorArduino : null;
                    pruR.Estimulo9 = valores.Count > 8 ? valores[8].colorPantalla : null;
                    pruR.Sonido9 = valores.Count > 8 ? valores[8].sonido.ToString() : null;

                    pruR.Tiempo10 = valores.Count > 9 ? valores[9].tiempo : null;
                    pruR.Respuesta10 = valores.Count > 9 ? valores[9].colorArduino : null;
                    pruR.Estimulo10 = valores.Count > 9 ? valores[9].colorPantalla : null;
                    pruR.Sonido10 = valores.Count > 9 ? valores[9].sonido.ToString() : null;


                    pruR.Tiempo11 = valores.Count > 10 ? valores[10].tiempo : null;
                    pruR.Respuesta11 = valores.Count > 10 ? valores[10].colorArduino : null;
                    pruR.Estimulo11 = valores.Count > 10 ? valores[10].colorPantalla : null;
                    pruR.Sonido11 = valores.Count > 10 ? valores[10].sonido.ToString() : null;

                    pruR.Tiempo12 = valores.Count > 11 ? valores[11].tiempo : null;
                    pruR.Respuesta12 = valores.Count > 11 ? valores[11].colorArduino : null;
                    pruR.Estimulo12 = valores.Count > 11 ? valores[11].colorPantalla : null;
                    pruR.Sonido12 = valores.Count > 11 ? valores[11].sonido.ToString() : null;


                    pruR.Tiempo13 = valores.Count > 12 ? valores[12].tiempo : null;
                    pruR.Respuesta13 = valores.Count > 12 ? valores[12].colorArduino : null;
                    pruR.Estimulo13 = valores.Count > 12 ? valores[12].colorPantalla : null;
                    pruR.Sonido13 = valores.Count > 12 ? valores[12].sonido.ToString() : null;

                    pruR.Tiempo14 = valores.Count > 13 ? valores[13].tiempo : null;
                    pruR.Respuesta14 = valores.Count > 13 ? valores[13].colorArduino : null;
                    pruR.Estimulo14 = valores.Count > 13 ? valores[13].colorPantalla : null;
                    pruR.Sonido14 = valores.Count > 13 ? valores[13].sonido.ToString() : null;


                    pruR.Tiempo15 = valores.Count > 14 ? valores[14].tiempo : null;
                    pruR.Respuesta15 = valores.Count > 14 ? valores[14].colorArduino : null;
                    pruR.Estimulo15 = valores.Count > 14 ? valores[14].colorPantalla : null;
                    pruR.Sonido15 = valores.Count > 14 ? valores[14].sonido.ToString() : null;


                    pruR.Tiempo16 = valores.Count > 15 ? valores[15].tiempo : null;
                    pruR.Respuesta16 = valores.Count > 15 ? valores[15].colorArduino : null;
                    pruR.Estimulo16 = valores.Count > 15 ? valores[15].colorPantalla : null;
                    pruR.Sonido16 = valores.Count > 15 ? valores[15].sonido.ToString() : null;


                    pruR.Tiempo17 = valores.Count > 16 ? valores[16].tiempo : null;
                    pruR.Respuesta17 = valores.Count > 16 ? valores[16].colorArduino : null;
                    pruR.Estimulo17 = valores.Count > 16 ? valores[16].colorPantalla : null;
                    pruR.Sonido17 = valores.Count > 16 ? valores[16].sonido.ToString() : null;


                    pruR.Tiempo18 = valores.Count > 17 ? valores[17].tiempo : null;
                    pruR.Respuesta18 = valores.Count > 17 ? valores[17].colorArduino : null;
                    pruR.Estimulo18 = valores.Count > 17 ? valores[17].colorPantalla : null;
                    pruR.Sonido18 = valores.Count > 17 ? valores[17].sonido.ToString() : null;


                    pruR.Tiempo19 = valores.Count > 18 ? valores[18].tiempo : null;
                    pruR.Respuesta19 = valores.Count > 18 ? valores[18].colorArduino : null;
                    pruR.Estimulo19 = valores.Count > 18 ? valores[18].colorPantalla : null;
                    pruR.Sonido19 = valores.Count > 18 ? valores[18].sonido.ToString() : null;

                    pruR.Tiempo20 = valores.Count > 19 ? valores[19].tiempo : null;
                    pruR.Respuesta20 = valores.Count > 19 ? valores[19].colorArduino : null;
                    pruR.Estimulo20 = valores.Count > 19 ? valores[19].colorPantalla : null;
                    pruR.Sonido20 = valores.Count > 19 ? valores[19].sonido.ToString() : null;

                    pruR.Tiempo21 = valores.Count > 20 ? valores[20].tiempo : null;
                    pruR.Respuesta21 = valores.Count > 20 ? valores[20].colorArduino : null;
                    pruR.Estimulo21 = valores.Count > 20 ? valores[20].colorPantalla : null;
                    pruR.Sonido21 = valores.Count > 20 ? valores[20].sonido.ToString() : null;

                    pruR.Tiempo22 = valores.Count > 21 ? valores[21].tiempo : null;
                    pruR.Respuesta22 = valores.Count > 21 ? valores[21].colorArduino : null;
                    pruR.Estimulo22 = valores.Count > 21 ? valores[21].colorPantalla : null;
                    pruR.Sonido22 = valores.Count > 21 ? valores[21].sonido.ToString() : null;


                    pruR.Tiempo23 = valores.Count > 22 ? valores[22].tiempo : null;
                    pruR.Respuesta23 = valores.Count > 22 ? valores[22].colorArduino : null;
                    pruR.Estimulo23 = valores.Count > 22 ? valores[22].colorPantalla : null;
                    pruR.Sonido23 = valores.Count > 22 ? valores[22].sonido.ToString() : null;


                    pruR.Tiempo24 = valores.Count > 23 ? valores[23].tiempo : null;
                    pruR.Respuesta24 = valores.Count > 23 ? valores[23].colorArduino : null;
                    pruR.Estimulo24 = valores.Count > 23 ? valores[23].colorPantalla : null;
                    pruR.Sonido24 = valores.Count > 23 ? valores[23].sonido.ToString() : null;


                    pruR.Tiempo25 = valores.Count > 24 ? valores[24].tiempo : null;
                    pruR.Respuesta25 = valores.Count > 24 ? valores[24].colorArduino : null;
                    pruR.Estimulo25 = valores.Count > 24 ? valores[24].colorPantalla : null;
                    pruR.Sonido25 = valores.Count > 24 ? valores[24].sonido.ToString() : null;

                    pruR.Tiempo26 = valores.Count > 25 ? valores[25].tiempo : null;
                    pruR.Respuesta26 = valores.Count > 25 ? valores[25].colorArduino : null;
                    pruR.Estimulo26 = valores.Count > 25 ? valores[25].colorPantalla : null;
                    pruR.Sonido26 = valores.Count > 25 ? valores[25].sonido.ToString() : null;


                    pruR.Tiempo27 = valores.Count > 26 ? valores[26].tiempo : null;
                    pruR.Respuesta27 = valores.Count > 26 ? valores[26].colorArduino : null;
                    pruR.Estimulo27 = valores.Count > 26 ? valores[26].colorPantalla : null;
                    pruR.Sonido27 = valores.Count > 26 ? valores[26].sonido.ToString() : null;


                    pruR.Tiempo28 = valores.Count > 27 ? valores[27].tiempo : null;
                    pruR.Respuesta28 = valores.Count > 27 ? valores[27].colorArduino : null;
                    pruR.Estimulo28 = valores.Count > 27 ? valores[27].colorPantalla : null;
                    pruR.Sonido28 = valores.Count > 27 ? valores[27].sonido.ToString() : null;


                    pruR.Tiempo29 = valores.Count > 28 ? valores[28].tiempo : null;
                    pruR.Respuesta29 = valores.Count > 28 ? valores[28].colorArduino : null;
                    pruR.Estimulo29 = valores.Count > 28 ? valores[28].colorPantalla : null;
                    pruR.Sonido29 = valores.Count > 28 ? valores[28].sonido.ToString() : null;


                    pruR.Tiempo30 = valores.Count > 29 ? valores[29].tiempo : null;
                    pruR.Respuesta30 = valores.Count > 29 ? valores[29].colorArduino : null;
                    pruR.Estimulo30 = valores.Count > 29 ? valores[29].colorPantalla : null;
                    pruR.Sonido30 = valores.Count > 29 ? valores[29].sonido.ToString() : null;




                    pruR.TiempoMaximo = maxT.ToString();
                    pruR.TiempoMedio = Math.Round(media, 2).ToString();
                    pruR.TiempoMinimo = minT != 100000 ? minT.ToString() : null;
                    pruR.SumTiempo = Math.Round(sumatoria, 2).ToString();
                    pruR.CantEstimulos = valores.Count().ToString();
                    pruR.CantOmisiones = omision.ToString();
                    pruR.CoefVariacion = Math.Round(desviacionEstandt / media, 2).ToString();
                    pruR.DesvStandar = desviacionEstandt.ToString();
                    pruR.RespCorrecta = rptaCorrecta.ToString();
                    pruR.Variante = variate;
                    pruR.Canterroresson = cantErroresSon.ToString();
                    pruR.Sonido = sonido == true ? "Si" : "No";
                    pruR.CantErrorColor = errorColor.ToString();
                    pruR.CantRespAdelant = respAnticipada.ToString();

                    pruR.TiempoMedio = media.ToString();

                    entities.PruTrcomple.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruTrcomple>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);

                    if (sonido == true)
                        pru.PTrcomples = ultimo.idTest;
                    else
                        pru.PTrcomple = ultimo.idTest;

                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sonido == false)
                    {
                        if (sujetoEva.PTrcomple == null)
                        {

                            PruTrcomple pruR = new PruTrcomple();
                            pruR.Fecha = date;


                            pruR.Tiempo1 = valores.Count > 0 ? valores[0].tiempo : null;
                            pruR.Respuesta1 = valores.Count > 0 ? valores[0].colorArduino : null;
                            pruR.Estimulo1 = valores.Count > 0 ? valores[0].colorPantalla : null;
                            pruR.Sonido1 = valores.Count > 0 ? valores[0].sonido.ToString() : null;

                            pruR.Tiempo2 = valores.Count > 1 ? valores[1].tiempo : null;
                            pruR.Respuesta2 = valores.Count > 1 ? valores[1].colorArduino : null;
                            pruR.Estimulo2 = valores.Count > 1 ? valores[1].colorPantalla : null;
                            pruR.Sonido2 = valores.Count > 1 ? valores[1].sonido.ToString() : null;


                            pruR.Tiempo3 = valores.Count > 2 ? valores[2].tiempo : null;
                            pruR.Respuesta3 = valores.Count > 2 ? valores[2].colorArduino : null;
                            pruR.Estimulo3 = valores.Count > 2 ? valores[2].colorPantalla : null;
                            pruR.Sonido3 = valores.Count > 2 ? valores[2].sonido.ToString() : null;

                            pruR.Tiempo4 = valores.Count > 3 ? valores[3].tiempo : null;
                            pruR.Respuesta4 = valores.Count > 3 ? valores[3].colorArduino : null;
                            pruR.Estimulo4 = valores.Count > 3 ? valores[3].colorPantalla : null;
                            pruR.Sonido4 = valores.Count > 3 ? valores[3].sonido.ToString() : null;

                            pruR.Tiempo5 = valores.Count > 4 ? valores[4].tiempo : null;
                            pruR.Respuesta5 = valores.Count > 4 ? valores[4].colorArduino : null;
                            pruR.Estimulo5 = valores.Count > 4 ? valores[4].colorPantalla : null;
                            pruR.Sonido5 = valores.Count > 4 ? valores[4].sonido.ToString() : null;

                            pruR.Tiempo6 = valores.Count > 5 ? valores[5].tiempo : null;
                            pruR.Respuesta6 = valores.Count > 5 ? valores[5].colorArduino : null;
                            pruR.Estimulo6 = valores.Count > 5 ? valores[5].colorPantalla : null;
                            pruR.Sonido6 = valores.Count > 5 ? valores[5].sonido.ToString() : null;

                            pruR.Tiempo7 = valores.Count > 6 ? valores[6].tiempo : null;
                            pruR.Respuesta7 = valores.Count > 6 ? valores[6].colorArduino : null;
                            pruR.Estimulo7 = valores.Count > 6 ? valores[6].colorPantalla : null;
                            pruR.Sonido7 = valores.Count > 6 ? valores[6].sonido.ToString() : null;

                            pruR.Tiempo8 = valores.Count > 7 ? valores[7].tiempo : null;
                            pruR.Respuesta8 = valores.Count > 7 ? valores[7].colorArduino : null;
                            pruR.Estimulo8 = valores.Count > 7 ? valores[7].colorPantalla : null;
                            pruR.Sonido8 = valores.Count > 7 ? valores[7].sonido.ToString() : null;

                            pruR.Tiempo9 = valores.Count > 8 ? valores[8].tiempo : null;
                            pruR.Respuesta9 = valores.Count > 8 ? valores[8].colorArduino : null;
                            pruR.Estimulo9 = valores.Count > 8 ? valores[8].colorPantalla : null;
                            pruR.Sonido9 = valores.Count > 8 ? valores[8].sonido.ToString() : null;

                            pruR.Tiempo10 = valores.Count > 9 ? valores[9].tiempo : null;
                            pruR.Respuesta10 = valores.Count > 9 ? valores[9].colorArduino : null;
                            pruR.Estimulo10 = valores.Count > 9 ? valores[9].colorPantalla : null;
                            pruR.Sonido10 = valores.Count > 9 ? valores[9].sonido.ToString() : null;


                            pruR.Tiempo11 = valores.Count > 10 ? valores[10].tiempo : null;
                            pruR.Respuesta11 = valores.Count > 10 ? valores[10].colorArduino : null;
                            pruR.Estimulo11 = valores.Count > 10 ? valores[10].colorPantalla : null;
                            pruR.Sonido11 = valores.Count > 10 ? valores[10].sonido.ToString() : null;

                            pruR.Tiempo12 = valores.Count > 11 ? valores[11].tiempo : null;
                            pruR.Respuesta12 = valores.Count > 11 ? valores[11].colorArduino : null;
                            pruR.Estimulo12 = valores.Count > 11 ? valores[11].colorPantalla : null;
                            pruR.Sonido12 = valores.Count > 11 ? valores[11].sonido.ToString() : null;


                            pruR.Tiempo13 = valores.Count > 12 ? valores[12].tiempo : null;
                            pruR.Respuesta13 = valores.Count > 12 ? valores[12].colorArduino : null;
                            pruR.Estimulo13 = valores.Count > 12 ? valores[12].colorPantalla : null;
                            pruR.Sonido13 = valores.Count > 12 ? valores[12].sonido.ToString() : null;

                            pruR.Tiempo14 = valores.Count > 13 ? valores[13].tiempo : null;
                            pruR.Respuesta14 = valores.Count > 13 ? valores[13].colorArduino : null;
                            pruR.Estimulo14 = valores.Count > 13 ? valores[13].colorPantalla : null;
                            pruR.Sonido14 = valores.Count > 13 ? valores[13].sonido.ToString() : null;


                            pruR.Tiempo15 = valores.Count > 14 ? valores[14].tiempo : null;
                            pruR.Respuesta15 = valores.Count > 14 ? valores[14].colorArduino : null;
                            pruR.Estimulo15 = valores.Count > 14 ? valores[14].colorPantalla : null;
                            pruR.Sonido15 = valores.Count > 14 ? valores[14].sonido.ToString() : null;


                            pruR.Tiempo16 = valores.Count > 15 ? valores[15].tiempo : null;
                            pruR.Respuesta16 = valores.Count > 15 ? valores[15].colorArduino : null;
                            pruR.Estimulo16 = valores.Count > 15 ? valores[15].colorPantalla : null;
                            pruR.Sonido16 = valores.Count > 15 ? valores[15].sonido.ToString() : null;


                            pruR.Tiempo17 = valores.Count > 16 ? valores[16].tiempo : null;
                            pruR.Respuesta17 = valores.Count > 16 ? valores[16].colorArduino : null;
                            pruR.Estimulo17 = valores.Count > 16 ? valores[16].colorPantalla : null;
                            pruR.Sonido17 = valores.Count > 16 ? valores[16].sonido.ToString() : null;


                            pruR.Tiempo18 = valores.Count > 17 ? valores[17].tiempo : null;
                            pruR.Respuesta18 = valores.Count > 17 ? valores[17].colorArduino : null;
                            pruR.Estimulo18 = valores.Count > 17 ? valores[17].colorPantalla : null;
                            pruR.Sonido18 = valores.Count > 17 ? valores[17].sonido.ToString() : null;


                            pruR.Tiempo19 = valores.Count > 18 ? valores[18].tiempo : null;
                            pruR.Respuesta19 = valores.Count > 18 ? valores[18].colorArduino : null;
                            pruR.Estimulo19 = valores.Count > 18 ? valores[18].colorPantalla : null;
                            pruR.Sonido19 = valores.Count > 18 ? valores[18].sonido.ToString() : null;

                            pruR.Tiempo20 = valores.Count > 19 ? valores[19].tiempo : null;
                            pruR.Respuesta20 = valores.Count > 19 ? valores[19].colorArduino : null;
                            pruR.Estimulo20 = valores.Count > 19 ? valores[19].colorPantalla : null;
                            pruR.Sonido20 = valores.Count > 19 ? valores[19].sonido.ToString() : null;

                            pruR.Tiempo21 = valores.Count > 20 ? valores[20].tiempo : null;
                            pruR.Respuesta21 = valores.Count > 20 ? valores[20].colorArduino : null;
                            pruR.Estimulo21 = valores.Count > 20 ? valores[20].colorPantalla : null;
                            pruR.Sonido21 = valores.Count > 20 ? valores[20].sonido.ToString() : null;

                            pruR.Tiempo22 = valores.Count > 21 ? valores[21].tiempo : null;
                            pruR.Respuesta22 = valores.Count > 21 ? valores[21].colorArduino : null;
                            pruR.Estimulo22 = valores.Count > 21 ? valores[21].colorPantalla : null;
                            pruR.Sonido22 = valores.Count > 21 ? valores[21].sonido.ToString() : null;


                            pruR.Tiempo23 = valores.Count > 22 ? valores[22].tiempo : null;
                            pruR.Respuesta23 = valores.Count > 22 ? valores[22].colorArduino : null;
                            pruR.Estimulo23 = valores.Count > 22 ? valores[22].colorPantalla : null;
                            pruR.Sonido23 = valores.Count > 22 ? valores[22].sonido.ToString() : null;


                            pruR.Tiempo24 = valores.Count > 23 ? valores[23].tiempo : null;
                            pruR.Respuesta24 = valores.Count > 23 ? valores[23].colorArduino : null;
                            pruR.Estimulo24 = valores.Count > 23 ? valores[23].colorPantalla : null;
                            pruR.Sonido24 = valores.Count > 23 ? valores[23].sonido.ToString() : null;


                            pruR.Tiempo25 = valores.Count > 24 ? valores[24].tiempo : null;
                            pruR.Respuesta25 = valores.Count > 24 ? valores[24].colorArduino : null;
                            pruR.Estimulo25 = valores.Count > 24 ? valores[24].colorPantalla : null;
                            pruR.Sonido25 = valores.Count > 24 ? valores[24].sonido.ToString() : null;

                            pruR.Tiempo26 = valores.Count > 25 ? valores[25].tiempo : null;
                            pruR.Respuesta26 = valores.Count > 25 ? valores[25].colorArduino : null;
                            pruR.Estimulo26 = valores.Count > 25 ? valores[25].colorPantalla : null;
                            pruR.Sonido26 = valores.Count > 25 ? valores[25].sonido.ToString() : null;


                            pruR.Tiempo27 = valores.Count > 26 ? valores[26].tiempo : null;
                            pruR.Respuesta27 = valores.Count > 26 ? valores[26].colorArduino : null;
                            pruR.Estimulo27 = valores.Count > 26 ? valores[26].colorPantalla : null;
                            pruR.Sonido27 = valores.Count > 26 ? valores[26].sonido.ToString() : null;


                            pruR.Tiempo28 = valores.Count > 27 ? valores[27].tiempo : null;
                            pruR.Respuesta28 = valores.Count > 27 ? valores[27].colorArduino : null;
                            pruR.Estimulo28 = valores.Count > 27 ? valores[27].colorPantalla : null;
                            pruR.Sonido28 = valores.Count > 27 ? valores[27].sonido.ToString() : null;


                            pruR.Tiempo29 = valores.Count > 28 ? valores[28].tiempo : null;
                            pruR.Respuesta29 = valores.Count > 28 ? valores[28].colorArduino : null;
                            pruR.Estimulo29 = valores.Count > 28 ? valores[28].colorPantalla : null;
                            pruR.Sonido29 = valores.Count > 28 ? valores[28].sonido.ToString() : null;


                            pruR.Tiempo30 = valores.Count > 29 ? valores[29].tiempo : null;
                            pruR.Respuesta30 = valores.Count > 29 ? valores[29].colorArduino : null;
                            pruR.Estimulo30 = valores.Count > 29 ? valores[29].colorPantalla : null;
                            pruR.Sonido30 = valores.Count > 29 ? valores[29].sonido.ToString() : null;




                            pruR.TiempoMaximo = maxT.ToString();
                            pruR.TiempoMedio = Math.Round(media, 2).ToString();
                            pruR.TiempoMinimo = minT != 100000 ? minT.ToString() : null;
                            pruR.SumTiempo = Math.Round(sumatoria, 2).ToString();
                            pruR.CantEstimulos = valores.Count().ToString();
                            pruR.CantOmisiones = omision.ToString();
                            pruR.CoefVariacion = Math.Round(desviacionEstandt / media, 2).ToString();
                            pruR.DesvStandar = desviacionEstandt.ToString();
                            pruR.RespCorrecta = rptaCorrecta.ToString();
                            pruR.Variante = variate;
                            pruR.Canterroresson = cantErroresSon.ToString();
                            pruR.Sonido = "No";
                            pruR.CantErrorColor = errorColor.ToString();
                            pruR.CantRespAdelant = respAnticipada.ToString();

                            pruR.TiempoMedio = media.ToString();

                            entities.PruTrcomple.Add(pruR);
                            entities.SaveChangesAsync();



                            var ultimo = entities.Set<PruTrcomple>().OrderByDescending(t => t.idTest).FirstOrDefault();


                            sujetoEva.PTrcomple = ultimo.idTest;

                            entities.SaveChangesAsync();

                        }
                        else
                        {
                            PruTrcomple conect = new PruTrcomple();


                            conect = entities.PruTrcomple.Where(f => f.idTest == sujetoEva.PTrcomple).FirstOrDefault<PruTrcomple>();

                            conect.Fecha = date;
                            conect.Tiempo1 = valores.Count > 0 ? valores[0].tiempo : null;
                            conect.Respuesta1 = valores.Count > 0 ? valores[0].colorArduino : null;
                            conect.Estimulo1 = valores.Count > 0 ? valores[0].colorPantalla : null;
                            conect.Sonido1 = valores.Count > 0 ? valores[0].sonido.ToString() : null;

                            conect.Tiempo2 = valores.Count > 1 ? valores[1].tiempo : null;
                            conect.Respuesta2 = valores.Count > 1 ? valores[1].colorArduino : null;
                            conect.Estimulo2 = valores.Count > 1 ? valores[1].colorPantalla : null;
                            conect.Sonido2 = valores.Count > 1 ? valores[1].sonido.ToString() : null;


                            conect.Tiempo3 = valores.Count > 2 ? valores[2].tiempo : null;
                            conect.Respuesta3 = valores.Count > 2 ? valores[2].colorArduino : null;
                            conect.Estimulo3 = valores.Count > 2 ? valores[2].colorPantalla : null;
                            conect.Sonido3 = valores.Count > 2 ? valores[2].sonido.ToString() : null;

                            conect.Tiempo4 = valores.Count > 3 ? valores[3].tiempo : null;
                            conect.Respuesta4 = valores.Count > 3 ? valores[3].colorArduino : null;
                            conect.Estimulo4 = valores.Count > 3 ? valores[3].colorPantalla : null;
                            conect.Sonido4 = valores.Count > 3 ? valores[3].sonido.ToString() : null;

                            conect.Tiempo5 = valores.Count > 4 ? valores[4].tiempo : null;
                            conect.Respuesta5 = valores.Count > 4 ? valores[4].colorArduino : null;
                            conect.Estimulo5 = valores.Count > 4 ? valores[4].colorPantalla : null;
                            conect.Sonido5 = valores.Count > 4 ? valores[4].sonido.ToString() : null;

                            conect.Tiempo6 = valores.Count > 5 ? valores[5].tiempo : null;
                            conect.Respuesta6 = valores.Count > 5 ? valores[5].colorArduino : null;
                            conect.Estimulo6 = valores.Count > 5 ? valores[5].colorPantalla : null;
                            conect.Sonido6 = valores.Count > 5 ? valores[5].sonido.ToString() : null;

                            conect.Tiempo7 = valores.Count > 6 ? valores[6].tiempo : null;
                            conect.Respuesta7 = valores.Count > 6 ? valores[6].colorArduino : null;
                            conect.Estimulo7 = valores.Count > 6 ? valores[6].colorPantalla : null;
                            conect.Sonido7 = valores.Count > 6 ? valores[6].sonido.ToString() : null;

                            conect.Tiempo8 = valores.Count > 7 ? valores[7].tiempo : null;
                            conect.Respuesta8 = valores.Count > 7 ? valores[7].colorArduino : null;
                            conect.Estimulo8 = valores.Count > 7 ? valores[7].colorPantalla : null;
                            conect.Sonido8 = valores.Count > 7 ? valores[7].sonido.ToString() : null;

                            conect.Tiempo9 = valores.Count > 8 ? valores[8].tiempo : null;
                            conect.Respuesta9 = valores.Count > 8 ? valores[8].colorArduino : null;
                            conect.Estimulo9 = valores.Count > 8 ? valores[8].colorPantalla : null;
                            conect.Sonido9 = valores.Count > 8 ? valores[8].sonido.ToString() : null;

                            conect.Tiempo10 = valores.Count > 9 ? valores[9].tiempo : null;
                            conect.Respuesta10 = valores.Count > 9 ? valores[9].colorArduino : null;
                            conect.Estimulo10 = valores.Count > 9 ? valores[9].colorPantalla : null;
                            conect.Sonido10 = valores.Count > 9 ? valores[9].sonido.ToString() : null;


                            conect.Tiempo11 = valores.Count > 10 ? valores[10].tiempo : null;
                            conect.Respuesta11 = valores.Count > 10 ? valores[10].colorArduino : null;
                            conect.Estimulo11 = valores.Count > 10 ? valores[10].colorPantalla : null;
                            conect.Sonido11 = valores.Count > 10 ? valores[10].sonido.ToString() : null;

                            conect.Tiempo12 = valores.Count > 11 ? valores[11].tiempo : null;
                            conect.Respuesta12 = valores.Count > 11 ? valores[11].colorArduino : null;
                            conect.Estimulo12 = valores.Count > 11 ? valores[11].colorPantalla : null;
                            conect.Sonido12 = valores.Count > 11 ? valores[11].sonido.ToString() : null;


                            conect.Tiempo13 = valores.Count > 12 ? valores[12].tiempo : null;
                            conect.Respuesta13 = valores.Count > 12 ? valores[12].colorArduino : null;
                            conect.Estimulo13 = valores.Count > 12 ? valores[12].colorPantalla : null;
                            conect.Sonido13 = valores.Count > 12 ? valores[12].sonido.ToString() : null;

                            conect.Tiempo14 = valores.Count > 13 ? valores[13].tiempo : null;
                            conect.Respuesta14 = valores.Count > 13 ? valores[13].colorArduino : null;
                            conect.Estimulo14 = valores.Count > 13 ? valores[13].colorPantalla : null;
                            conect.Sonido14 = valores.Count > 13 ? valores[13].sonido.ToString() : null;


                            conect.Tiempo15 = valores.Count > 14 ? valores[14].tiempo : null;
                            conect.Respuesta15 = valores.Count > 14 ? valores[14].colorArduino : null;
                            conect.Estimulo15 = valores.Count > 14 ? valores[14].colorPantalla : null;
                            conect.Sonido15 = valores.Count > 14 ? valores[14].sonido.ToString() : null;


                            conect.Tiempo16 = valores.Count > 15 ? valores[15].tiempo : null;
                            conect.Respuesta16 = valores.Count > 15 ? valores[15].colorArduino : null;
                            conect.Estimulo16 = valores.Count > 15 ? valores[15].colorPantalla : null;
                            conect.Sonido16 = valores.Count > 15 ? valores[15].sonido.ToString() : null;


                            conect.Tiempo17 = valores.Count > 16 ? valores[16].tiempo : null;
                            conect.Respuesta17 = valores.Count > 16 ? valores[16].colorArduino : null;
                            conect.Estimulo17 = valores.Count > 16 ? valores[16].colorPantalla : null;
                            conect.Sonido17 = valores.Count > 16 ? valores[16].sonido.ToString() : null;


                            conect.Tiempo18 = valores.Count > 17 ? valores[17].tiempo : null;
                            conect.Respuesta18 = valores.Count > 17 ? valores[17].colorArduino : null;
                            conect.Estimulo18 = valores.Count > 17 ? valores[17].colorPantalla : null;
                            conect.Sonido18 = valores.Count > 17 ? valores[17].sonido.ToString() : null;


                            conect.Tiempo19 = valores.Count > 18 ? valores[18].tiempo : null;
                            conect.Respuesta19 = valores.Count > 18 ? valores[18].colorArduino : null;
                            conect.Estimulo19 = valores.Count > 18 ? valores[18].colorPantalla : null;
                            conect.Sonido19 = valores.Count > 18 ? valores[18].sonido.ToString() : null;

                            conect.Tiempo20 = valores.Count > 19 ? valores[19].tiempo : null;
                            conect.Respuesta20 = valores.Count > 19 ? valores[19].colorArduino : null;
                            conect.Estimulo20 = valores.Count > 19 ? valores[19].colorPantalla : null;
                            conect.Sonido20 = valores.Count > 19 ? valores[19].sonido.ToString() : null;

                            conect.Tiempo21 = valores.Count > 20 ? valores[20].tiempo : null;
                            conect.Respuesta21 = valores.Count > 20 ? valores[20].colorArduino : null;
                            conect.Estimulo21 = valores.Count > 20 ? valores[20].colorPantalla : null;
                            conect.Sonido21 = valores.Count > 20 ? valores[20].sonido.ToString() : null;

                            conect.Tiempo22 = valores.Count > 21 ? valores[21].tiempo : null;
                            conect.Respuesta22 = valores.Count > 21 ? valores[21].colorArduino : null;
                            conect.Estimulo22 = valores.Count > 21 ? valores[21].colorPantalla : null;
                            conect.Sonido22 = valores.Count > 21 ? valores[21].sonido.ToString() : null;


                            conect.Tiempo23 = valores.Count > 22 ? valores[22].tiempo : null;
                            conect.Respuesta23 = valores.Count > 22 ? valores[22].colorArduino : null;
                            conect.Estimulo23 = valores.Count > 22 ? valores[22].colorPantalla : null;
                            conect.Sonido23 = valores.Count > 22 ? valores[22].sonido.ToString() : null;


                            conect.Tiempo24 = valores.Count > 23 ? valores[23].tiempo : null;
                            conect.Respuesta24 = valores.Count > 23 ? valores[23].colorArduino : null;
                            conect.Estimulo24 = valores.Count > 23 ? valores[23].colorPantalla : null;
                            conect.Sonido24 = valores.Count > 23 ? valores[23].sonido.ToString() : null;


                            conect.Tiempo25 = valores.Count > 24 ? valores[24].tiempo : null;
                            conect.Respuesta25 = valores.Count > 24 ? valores[24].colorArduino : null;
                            conect.Estimulo25 = valores.Count > 24 ? valores[24].colorPantalla : null;
                            conect.Sonido25 = valores.Count > 24 ? valores[24].sonido.ToString() : null;

                            conect.Tiempo26 = valores.Count > 25 ? valores[25].tiempo : null;
                            conect.Respuesta26 = valores.Count > 25 ? valores[25].colorArduino : null;
                            conect.Estimulo26 = valores.Count > 25 ? valores[25].colorPantalla : null;
                            conect.Sonido26 = valores.Count > 25 ? valores[25].sonido.ToString() : null;


                            conect.Tiempo27 = valores.Count > 26 ? valores[26].tiempo : null;
                            conect.Respuesta27 = valores.Count > 26 ? valores[26].colorArduino : null;
                            conect.Estimulo27 = valores.Count > 26 ? valores[26].colorPantalla : null;
                            conect.Sonido27 = valores.Count > 26 ? valores[26].sonido.ToString() : null;


                            conect.Tiempo28 = valores.Count > 27 ? valores[27].tiempo : null;
                            conect.Respuesta28 = valores.Count > 27 ? valores[27].colorArduino : null;
                            conect.Estimulo28 = valores.Count > 27 ? valores[27].colorPantalla : null;
                            conect.Sonido28 = valores.Count > 27 ? valores[27].sonido.ToString() : null;


                            conect.Tiempo29 = valores.Count > 28 ? valores[28].tiempo : null;
                            conect.Respuesta29 = valores.Count > 28 ? valores[28].colorArduino : null;
                            conect.Estimulo29 = valores.Count > 28 ? valores[28].colorPantalla : null;
                            conect.Sonido29 = valores.Count > 28 ? valores[28].sonido.ToString() : null;


                            conect.Tiempo30 = valores.Count > 29 ? valores[29].tiempo : null;
                            conect.Respuesta30 = valores.Count > 29 ? valores[29].colorArduino : null;
                            conect.Estimulo30 = valores.Count > 29 ? valores[29].colorPantalla : null;
                            conect.Sonido30 = valores.Count > 29 ? valores[29].sonido.ToString() : null;




                            conect.TiempoMaximo = maxT.ToString();
                            conect.TiempoMedio = Math.Round(media, 2).ToString();
                            conect.TiempoMinimo = minT != 100000 ? minT.ToString() : null;
                            conect.SumTiempo = Math.Round(sumatoria, 2).ToString();
                            conect.CantEstimulos = valores.Count().ToString();
                            conect.CantOmisiones = omision.ToString();
                            conect.CoefVariacion = Math.Round(desviacionEstandt / media, 2).ToString();
                            conect.DesvStandar = desviacionEstandt.ToString();
                            conect.RespCorrecta = rptaCorrecta.ToString();
                            conect.Variante = variate;
                            conect.Canterroresson = cantErroresSon.ToString();
                            conect.Sonido = "No";
                            conect.CantErrorColor = errorColor.ToString();
                            conect.CantRespAdelant = respAnticipada.ToString();
                            conect.TiempoMedio = media.ToString();

                            entities.SaveChangesAsync();

                        }
                    }


                    if (sonido == true)
                    {
                        if (sujetoEva.PTrcomples == null)
                        {

                            PruTrcomple pruR = new PruTrcomple();
                            pruR.Fecha = date;


                            pruR.Tiempo1 = valores.Count > 0 ? valores[0].tiempo : null;
                            pruR.Respuesta1 = valores.Count > 0 ? valores[0].colorArduino : null;
                            pruR.Estimulo1 = valores.Count > 0 ? valores[0].colorPantalla : null;
                            pruR.Sonido1 = valores.Count > 0 ? valores[0].sonido.ToString() : null;

                            pruR.Tiempo2 = valores.Count > 1 ? valores[1].tiempo : null;
                            pruR.Respuesta2 = valores.Count > 1 ? valores[1].colorArduino : null;
                            pruR.Estimulo2 = valores.Count > 1 ? valores[1].colorPantalla : null;
                            pruR.Sonido2 = valores.Count > 1 ? valores[1].sonido.ToString() : null;


                            pruR.Tiempo3 = valores.Count > 2 ? valores[2].tiempo : null;
                            pruR.Respuesta3 = valores.Count > 2 ? valores[2].colorArduino : null;
                            pruR.Estimulo3 = valores.Count > 2 ? valores[2].colorPantalla : null;
                            pruR.Sonido3 = valores.Count > 2 ? valores[2].sonido.ToString() : null;

                            pruR.Tiempo4 = valores.Count > 3 ? valores[3].tiempo : null;
                            pruR.Respuesta4 = valores.Count > 3 ? valores[3].colorArduino : null;
                            pruR.Estimulo4 = valores.Count > 3 ? valores[3].colorPantalla : null;
                            pruR.Sonido4 = valores.Count > 3 ? valores[3].sonido.ToString() : null;

                            pruR.Tiempo5 = valores.Count > 4 ? valores[4].tiempo : null;
                            pruR.Respuesta5 = valores.Count > 4 ? valores[4].colorArduino : null;
                            pruR.Estimulo5 = valores.Count > 4 ? valores[4].colorPantalla : null;
                            pruR.Sonido5 = valores.Count > 4 ? valores[4].sonido.ToString() : null;

                            pruR.Tiempo6 = valores.Count > 5 ? valores[5].tiempo : null;
                            pruR.Respuesta6 = valores.Count > 5 ? valores[5].colorArduino : null;
                            pruR.Estimulo6 = valores.Count > 5 ? valores[5].colorPantalla : null;
                            pruR.Sonido6 = valores.Count > 5 ? valores[5].sonido.ToString() : null;

                            pruR.Tiempo7 = valores.Count > 6 ? valores[6].tiempo : null;
                            pruR.Respuesta7 = valores.Count > 6 ? valores[6].colorArduino : null;
                            pruR.Estimulo7 = valores.Count > 6 ? valores[6].colorPantalla : null;
                            pruR.Sonido7 = valores.Count > 6 ? valores[6].sonido.ToString() : null;

                            pruR.Tiempo8 = valores.Count > 7 ? valores[7].tiempo : null;
                            pruR.Respuesta8 = valores.Count > 7 ? valores[7].colorArduino : null;
                            pruR.Estimulo8 = valores.Count > 7 ? valores[7].colorPantalla : null;
                            pruR.Sonido8 = valores.Count > 7 ? valores[7].sonido.ToString() : null;

                            pruR.Tiempo9 = valores.Count > 8 ? valores[8].tiempo : null;
                            pruR.Respuesta9 = valores.Count > 8 ? valores[8].colorArduino : null;
                            pruR.Estimulo9 = valores.Count > 8 ? valores[8].colorPantalla : null;
                            pruR.Sonido9 = valores.Count > 8 ? valores[8].sonido.ToString() : null;

                            pruR.Tiempo10 = valores.Count > 9 ? valores[9].tiempo : null;
                            pruR.Respuesta10 = valores.Count > 9 ? valores[9].colorArduino : null;
                            pruR.Estimulo10 = valores.Count > 9 ? valores[9].colorPantalla : null;
                            pruR.Sonido10 = valores.Count > 9 ? valores[9].sonido.ToString() : null;


                            pruR.Tiempo11 = valores.Count > 10 ? valores[10].tiempo : null;
                            pruR.Respuesta11 = valores.Count > 10 ? valores[10].colorArduino : null;
                            pruR.Estimulo11 = valores.Count > 10 ? valores[10].colorPantalla : null;
                            pruR.Sonido11 = valores.Count > 10 ? valores[10].sonido.ToString() : null;

                            pruR.Tiempo12 = valores.Count > 11 ? valores[11].tiempo : null;
                            pruR.Respuesta12 = valores.Count > 11 ? valores[11].colorArduino : null;
                            pruR.Estimulo12 = valores.Count > 11 ? valores[11].colorPantalla : null;
                            pruR.Sonido12 = valores.Count > 11 ? valores[11].sonido.ToString() : null;


                            pruR.Tiempo13 = valores.Count > 12 ? valores[12].tiempo : null;
                            pruR.Respuesta13 = valores.Count > 12 ? valores[12].colorArduino : null;
                            pruR.Estimulo13 = valores.Count > 12 ? valores[12].colorPantalla : null;
                            pruR.Sonido13 = valores.Count > 12 ? valores[12].sonido.ToString() : null;

                            pruR.Tiempo14 = valores.Count > 13 ? valores[13].tiempo : null;
                            pruR.Respuesta14 = valores.Count > 13 ? valores[13].colorArduino : null;
                            pruR.Estimulo14 = valores.Count > 13 ? valores[13].colorPantalla : null;
                            pruR.Sonido14 = valores.Count > 13 ? valores[13].sonido.ToString() : null;


                            pruR.Tiempo15 = valores.Count > 14 ? valores[14].tiempo : null;
                            pruR.Respuesta15 = valores.Count > 14 ? valores[14].colorArduino : null;
                            pruR.Estimulo15 = valores.Count > 14 ? valores[14].colorPantalla : null;
                            pruR.Sonido15 = valores.Count > 14 ? valores[14].sonido.ToString() : null;


                            pruR.Tiempo16 = valores.Count > 15 ? valores[15].tiempo : null;
                            pruR.Respuesta16 = valores.Count > 15 ? valores[15].colorArduino : null;
                            pruR.Estimulo16 = valores.Count > 15 ? valores[15].colorPantalla : null;
                            pruR.Sonido16 = valores.Count > 15 ? valores[15].sonido.ToString() : null;


                            pruR.Tiempo17 = valores.Count > 16 ? valores[16].tiempo : null;
                            pruR.Respuesta17 = valores.Count > 16 ? valores[16].colorArduino : null;
                            pruR.Estimulo17 = valores.Count > 16 ? valores[16].colorPantalla : null;
                            pruR.Sonido17 = valores.Count > 16 ? valores[16].sonido.ToString() : null;


                            pruR.Tiempo18 = valores.Count > 17 ? valores[17].tiempo : null;
                            pruR.Respuesta18 = valores.Count > 17 ? valores[17].colorArduino : null;
                            pruR.Estimulo18 = valores.Count > 17 ? valores[17].colorPantalla : null;
                            pruR.Sonido18 = valores.Count > 17 ? valores[17].sonido.ToString() : null;


                            pruR.Tiempo19 = valores.Count > 18 ? valores[18].tiempo : null;
                            pruR.Respuesta19 = valores.Count > 18 ? valores[18].colorArduino : null;
                            pruR.Estimulo19 = valores.Count > 18 ? valores[18].colorPantalla : null;
                            pruR.Sonido19 = valores.Count > 18 ? valores[18].sonido.ToString() : null;

                            pruR.Tiempo20 = valores.Count > 19 ? valores[19].tiempo : null;
                            pruR.Respuesta20 = valores.Count > 19 ? valores[19].colorArduino : null;
                            pruR.Estimulo20 = valores.Count > 19 ? valores[19].colorPantalla : null;
                            pruR.Sonido20 = valores.Count > 19 ? valores[19].sonido.ToString() : null;

                            pruR.Tiempo21 = valores.Count > 20 ? valores[20].tiempo : null;
                            pruR.Respuesta21 = valores.Count > 20 ? valores[20].colorArduino : null;
                            pruR.Estimulo21 = valores.Count > 20 ? valores[20].colorPantalla : null;
                            pruR.Sonido21 = valores.Count > 20 ? valores[20].sonido.ToString() : null;

                            pruR.Tiempo22 = valores.Count > 21 ? valores[21].tiempo : null;
                            pruR.Respuesta22 = valores.Count > 21 ? valores[21].colorArduino : null;
                            pruR.Estimulo22 = valores.Count > 21 ? valores[21].colorPantalla : null;
                            pruR.Sonido22 = valores.Count > 21 ? valores[21].sonido.ToString() : null;


                            pruR.Tiempo23 = valores.Count > 22 ? valores[22].tiempo : null;
                            pruR.Respuesta23 = valores.Count > 22 ? valores[22].colorArduino : null;
                            pruR.Estimulo23 = valores.Count > 22 ? valores[22].colorPantalla : null;
                            pruR.Sonido23 = valores.Count > 22 ? valores[22].sonido.ToString() : null;


                            pruR.Tiempo24 = valores.Count > 23 ? valores[23].tiempo : null;
                            pruR.Respuesta24 = valores.Count > 23 ? valores[23].colorArduino : null;
                            pruR.Estimulo24 = valores.Count > 23 ? valores[23].colorPantalla : null;
                            pruR.Sonido24 = valores.Count > 23 ? valores[23].sonido.ToString() : null;


                            pruR.Tiempo25 = valores.Count > 24 ? valores[24].tiempo : null;
                            pruR.Respuesta25 = valores.Count > 24 ? valores[24].colorArduino : null;
                            pruR.Estimulo25 = valores.Count > 24 ? valores[24].colorPantalla : null;
                            pruR.Sonido25 = valores.Count > 24 ? valores[24].sonido.ToString() : null;

                            pruR.Tiempo26 = valores.Count > 25 ? valores[25].tiempo : null;
                            pruR.Respuesta26 = valores.Count > 25 ? valores[25].colorArduino : null;
                            pruR.Estimulo26 = valores.Count > 25 ? valores[25].colorPantalla : null;
                            pruR.Sonido26 = valores.Count > 25 ? valores[25].sonido.ToString() : null;


                            pruR.Tiempo27 = valores.Count > 26 ? valores[26].tiempo : null;
                            pruR.Respuesta27 = valores.Count > 26 ? valores[26].colorArduino : null;
                            pruR.Estimulo27 = valores.Count > 26 ? valores[26].colorPantalla : null;
                            pruR.Sonido27 = valores.Count > 26 ? valores[26].sonido.ToString() : null;


                            pruR.Tiempo28 = valores.Count > 27 ? valores[27].tiempo : null;
                            pruR.Respuesta28 = valores.Count > 27 ? valores[27].colorArduino : null;
                            pruR.Estimulo28 = valores.Count > 27 ? valores[27].colorPantalla : null;
                            pruR.Sonido28 = valores.Count > 27 ? valores[27].sonido.ToString() : null;


                            pruR.Tiempo29 = valores.Count > 28 ? valores[28].tiempo : null;
                            pruR.Respuesta29 = valores.Count > 28 ? valores[28].colorArduino : null;
                            pruR.Estimulo29 = valores.Count > 28 ? valores[28].colorPantalla : null;
                            pruR.Sonido29 = valores.Count > 28 ? valores[28].sonido.ToString() : null;


                            pruR.Tiempo30 = valores.Count > 29 ? valores[29].tiempo : null;
                            pruR.Respuesta30 = valores.Count > 29 ? valores[29].colorArduino : null;
                            pruR.Estimulo30 = valores.Count > 29 ? valores[29].colorPantalla : null;
                            pruR.Sonido30 = valores.Count > 29 ? valores[29].sonido.ToString() : null;




                            pruR.TiempoMaximo = maxT.ToString();
                            pruR.TiempoMedio = Math.Round(media, 2).ToString();
                            pruR.TiempoMinimo = minT != 100000 ? minT.ToString() : null;
                            pruR.SumTiempo = Math.Round(sumatoria, 2).ToString();
                            pruR.CantEstimulos = valores.Count().ToString();
                            pruR.CantOmisiones = omision.ToString();
                            pruR.CoefVariacion = Math.Round(desviacionEstandt / media, 2).ToString();
                            pruR.DesvStandar = desviacionEstandt.ToString();
                            pruR.RespCorrecta = rptaCorrecta.ToString();
                            pruR.Variante = variate;
                            pruR.Canterroresson = cantErroresSon.ToString();
                            pruR.Sonido = "Si";
                            pruR.CantErrorColor = errorColor.ToString();
                            pruR.CantRespAdelant = respAnticipada.ToString();

                            pruR.TiempoMedio = media.ToString();

                            entities.PruTrcomple.Add(pruR);
                            entities.SaveChangesAsync();



                            var ultimo = entities.Set<PruTrcomple>().OrderByDescending(t => t.idTest).FirstOrDefault();


                            sujetoEva.PTrcomples = ultimo.idTest;


                            entities.SaveChangesAsync();

                        }
                        else
                        {
                            PruTrcomple conect = new PruTrcomple();


                            conect = entities.PruTrcomple.Where(f => f.idTest == sujetoEva.PTrcomples).FirstOrDefault<PruTrcomple>();

                            conect.Fecha = date;
                            conect.Tiempo1 = valores.Count > 0 ? valores[0].tiempo : null;
                            conect.Respuesta1 = valores.Count > 0 ? valores[0].colorArduino : null;
                            conect.Estimulo1 = valores.Count > 0 ? valores[0].colorPantalla : null;
                            conect.Sonido1 = valores.Count > 0 ? valores[0].sonido.ToString() : null;

                            conect.Tiempo2 = valores.Count > 1 ? valores[1].tiempo : null;
                            conect.Respuesta2 = valores.Count > 1 ? valores[1].colorArduino : null;
                            conect.Estimulo2 = valores.Count > 1 ? valores[1].colorPantalla : null;
                            conect.Sonido2 = valores.Count > 1 ? valores[1].sonido.ToString() : null;


                            conect.Tiempo3 = valores.Count > 2 ? valores[2].tiempo : null;
                            conect.Respuesta3 = valores.Count > 2 ? valores[2].colorArduino : null;
                            conect.Estimulo3 = valores.Count > 2 ? valores[2].colorPantalla : null;
                            conect.Sonido3 = valores.Count > 2 ? valores[2].sonido.ToString() : null;

                            conect.Tiempo4 = valores.Count > 3 ? valores[3].tiempo : null;
                            conect.Respuesta4 = valores.Count > 3 ? valores[3].colorArduino : null;
                            conect.Estimulo4 = valores.Count > 3 ? valores[3].colorPantalla : null;
                            conect.Sonido4 = valores.Count > 3 ? valores[3].sonido.ToString() : null;

                            conect.Tiempo5 = valores.Count > 4 ? valores[4].tiempo : null;
                            conect.Respuesta5 = valores.Count > 4 ? valores[4].colorArduino : null;
                            conect.Estimulo5 = valores.Count > 4 ? valores[4].colorPantalla : null;
                            conect.Sonido5 = valores.Count > 4 ? valores[4].sonido.ToString() : null;

                            conect.Tiempo6 = valores.Count > 5 ? valores[5].tiempo : null;
                            conect.Respuesta6 = valores.Count > 5 ? valores[5].colorArduino : null;
                            conect.Estimulo6 = valores.Count > 5 ? valores[5].colorPantalla : null;
                            conect.Sonido6 = valores.Count > 5 ? valores[5].sonido.ToString() : null;

                            conect.Tiempo7 = valores.Count > 6 ? valores[6].tiempo : null;
                            conect.Respuesta7 = valores.Count > 6 ? valores[6].colorArduino : null;
                            conect.Estimulo7 = valores.Count > 6 ? valores[6].colorPantalla : null;
                            conect.Sonido7 = valores.Count > 6 ? valores[6].sonido.ToString() : null;

                            conect.Tiempo8 = valores.Count > 7 ? valores[7].tiempo : null;
                            conect.Respuesta8 = valores.Count > 7 ? valores[7].colorArduino : null;
                            conect.Estimulo8 = valores.Count > 7 ? valores[7].colorPantalla : null;
                            conect.Sonido8 = valores.Count > 7 ? valores[7].sonido.ToString() : null;

                            conect.Tiempo9 = valores.Count > 8 ? valores[8].tiempo : null;
                            conect.Respuesta9 = valores.Count > 8 ? valores[8].colorArduino : null;
                            conect.Estimulo9 = valores.Count > 8 ? valores[8].colorPantalla : null;
                            conect.Sonido9 = valores.Count > 8 ? valores[8].sonido.ToString() : null;

                            conect.Tiempo10 = valores.Count > 9 ? valores[9].tiempo : null;
                            conect.Respuesta10 = valores.Count > 9 ? valores[9].colorArduino : null;
                            conect.Estimulo10 = valores.Count > 9 ? valores[9].colorPantalla : null;
                            conect.Sonido10 = valores.Count > 9 ? valores[9].sonido.ToString() : null;


                            conect.Tiempo11 = valores.Count > 10 ? valores[10].tiempo : null;
                            conect.Respuesta11 = valores.Count > 10 ? valores[10].colorArduino : null;
                            conect.Estimulo11 = valores.Count > 10 ? valores[10].colorPantalla : null;
                            conect.Sonido11 = valores.Count > 10 ? valores[10].sonido.ToString() : null;

                            conect.Tiempo12 = valores.Count > 11 ? valores[11].tiempo : null;
                            conect.Respuesta12 = valores.Count > 11 ? valores[11].colorArduino : null;
                            conect.Estimulo12 = valores.Count > 11 ? valores[11].colorPantalla : null;
                            conect.Sonido12 = valores.Count > 11 ? valores[11].sonido.ToString() : null;


                            conect.Tiempo13 = valores.Count > 12 ? valores[12].tiempo : null;
                            conect.Respuesta13 = valores.Count > 12 ? valores[12].colorArduino : null;
                            conect.Estimulo13 = valores.Count > 12 ? valores[12].colorPantalla : null;
                            conect.Sonido13 = valores.Count > 12 ? valores[12].sonido.ToString() : null;

                            conect.Tiempo14 = valores.Count > 13 ? valores[13].tiempo : null;
                            conect.Respuesta14 = valores.Count > 13 ? valores[13].colorArduino : null;
                            conect.Estimulo14 = valores.Count > 13 ? valores[13].colorPantalla : null;
                            conect.Sonido14 = valores.Count > 13 ? valores[13].sonido.ToString() : null;


                            conect.Tiempo15 = valores.Count > 14 ? valores[14].tiempo : null;
                            conect.Respuesta15 = valores.Count > 14 ? valores[14].colorArduino : null;
                            conect.Estimulo15 = valores.Count > 14 ? valores[14].colorPantalla : null;
                            conect.Sonido15 = valores.Count > 14 ? valores[14].sonido.ToString() : null;


                            conect.Tiempo16 = valores.Count > 15 ? valores[15].tiempo : null;
                            conect.Respuesta16 = valores.Count > 15 ? valores[15].colorArduino : null;
                            conect.Estimulo16 = valores.Count > 15 ? valores[15].colorPantalla : null;
                            conect.Sonido16 = valores.Count > 15 ? valores[15].sonido.ToString() : null;


                            conect.Tiempo17 = valores.Count > 16 ? valores[16].tiempo : null;
                            conect.Respuesta17 = valores.Count > 16 ? valores[16].colorArduino : null;
                            conect.Estimulo17 = valores.Count > 16 ? valores[16].colorPantalla : null;
                            conect.Sonido17 = valores.Count > 16 ? valores[16].sonido.ToString() : null;


                            conect.Tiempo18 = valores.Count > 17 ? valores[17].tiempo : null;
                            conect.Respuesta18 = valores.Count > 17 ? valores[17].colorArduino : null;
                            conect.Estimulo18 = valores.Count > 17 ? valores[17].colorPantalla : null;
                            conect.Sonido18 = valores.Count > 17 ? valores[17].sonido.ToString() : null;


                            conect.Tiempo19 = valores.Count > 18 ? valores[18].tiempo : null;
                            conect.Respuesta19 = valores.Count > 18 ? valores[18].colorArduino : null;
                            conect.Estimulo19 = valores.Count > 18 ? valores[18].colorPantalla : null;
                            conect.Sonido19 = valores.Count > 18 ? valores[18].sonido.ToString() : null;

                            conect.Tiempo20 = valores.Count > 19 ? valores[19].tiempo : null;
                            conect.Respuesta20 = valores.Count > 19 ? valores[19].colorArduino : null;
                            conect.Estimulo20 = valores.Count > 19 ? valores[19].colorPantalla : null;
                            conect.Sonido20 = valores.Count > 19 ? valores[19].sonido.ToString() : null;

                            conect.Tiempo21 = valores.Count > 20 ? valores[20].tiempo : null;
                            conect.Respuesta21 = valores.Count > 20 ? valores[20].colorArduino : null;
                            conect.Estimulo21 = valores.Count > 20 ? valores[20].colorPantalla : null;
                            conect.Sonido21 = valores.Count > 20 ? valores[20].sonido.ToString() : null;

                            conect.Tiempo22 = valores.Count > 21 ? valores[21].tiempo : null;
                            conect.Respuesta22 = valores.Count > 21 ? valores[21].colorArduino : null;
                            conect.Estimulo22 = valores.Count > 21 ? valores[21].colorPantalla : null;
                            conect.Sonido22 = valores.Count > 21 ? valores[21].sonido.ToString() : null;


                            conect.Tiempo23 = valores.Count > 22 ? valores[22].tiempo : null;
                            conect.Respuesta23 = valores.Count > 22 ? valores[22].colorArduino : null;
                            conect.Estimulo23 = valores.Count > 22 ? valores[22].colorPantalla : null;
                            conect.Sonido23 = valores.Count > 22 ? valores[22].sonido.ToString() : null;


                            conect.Tiempo24 = valores.Count > 23 ? valores[23].tiempo : null;
                            conect.Respuesta24 = valores.Count > 23 ? valores[23].colorArduino : null;
                            conect.Estimulo24 = valores.Count > 23 ? valores[23].colorPantalla : null;
                            conect.Sonido24 = valores.Count > 23 ? valores[23].sonido.ToString() : null;


                            conect.Tiempo25 = valores.Count > 24 ? valores[24].tiempo : null;
                            conect.Respuesta25 = valores.Count > 24 ? valores[24].colorArduino : null;
                            conect.Estimulo25 = valores.Count > 24 ? valores[24].colorPantalla : null;
                            conect.Sonido25 = valores.Count > 24 ? valores[24].sonido.ToString() : null;

                            conect.Tiempo26 = valores.Count > 25 ? valores[25].tiempo : null;
                            conect.Respuesta26 = valores.Count > 25 ? valores[25].colorArduino : null;
                            conect.Estimulo26 = valores.Count > 25 ? valores[25].colorPantalla : null;
                            conect.Sonido26 = valores.Count > 25 ? valores[25].sonido.ToString() : null;


                            conect.Tiempo27 = valores.Count > 26 ? valores[26].tiempo : null;
                            conect.Respuesta27 = valores.Count > 26 ? valores[26].colorArduino : null;
                            conect.Estimulo27 = valores.Count > 26 ? valores[26].colorPantalla : null;
                            conect.Sonido27 = valores.Count > 26 ? valores[26].sonido.ToString() : null;


                            conect.Tiempo28 = valores.Count > 27 ? valores[27].tiempo : null;
                            conect.Respuesta28 = valores.Count > 27 ? valores[27].colorArduino : null;
                            conect.Estimulo28 = valores.Count > 27 ? valores[27].colorPantalla : null;
                            conect.Sonido28 = valores.Count > 27 ? valores[27].sonido.ToString() : null;


                            conect.Tiempo29 = valores.Count > 28 ? valores[28].tiempo : null;
                            conect.Respuesta29 = valores.Count > 28 ? valores[28].colorArduino : null;
                            conect.Estimulo29 = valores.Count > 28 ? valores[28].colorPantalla : null;
                            conect.Sonido29 = valores.Count > 28 ? valores[28].sonido.ToString() : null;


                            conect.Tiempo30 = valores.Count > 29 ? valores[29].tiempo : null;
                            conect.Respuesta30 = valores.Count > 29 ? valores[29].colorArduino : null;
                            conect.Estimulo30 = valores.Count > 29 ? valores[29].colorPantalla : null;
                            conect.Sonido30 = valores.Count > 29 ? valores[29].sonido.ToString() : null;




                            conect.TiempoMaximo = maxT.ToString();
                            conect.TiempoMedio = Math.Round(media, 2).ToString();
                            conect.TiempoMinimo = minT != 100000 ? minT.ToString() : null;
                            conect.SumTiempo = Math.Round(sumatoria, 2).ToString();
                            conect.CantEstimulos = valores.Count().ToString();
                            conect.CantOmisiones = omision.ToString();
                            conect.CoefVariacion = Math.Round(desviacionEstandt / media, 2).ToString();
                            conect.DesvStandar = desviacionEstandt.ToString();
                            conect.RespCorrecta = rptaCorrecta.ToString();
                            conect.Variante = variate;
                            conect.Canterroresson = cantErroresSon.ToString();
                            conect.Sonido = "Si";
                            conect.CantErrorColor = errorColor.ToString();
                            conect.CantRespAdelant = respAnticipada.ToString();
                            conect.TiempoMedio = media.ToString();

                            entities.SaveChangesAsync();

                        }
                    }
                }
            }
        }


        private double desviacionEstandart(double media, List<Result> valores)
        {
            double cuadrado = 0;
            int contador = 0;
            for (int i = 0; i < valores.Count; i++)
            {
                if (valores[i].tiempo != "-" && !valores[i].sonido &&
                    valores[i].colorArduino == valores[i].colorPantalla)
                {
                    int res = Convert.ToInt32(valores[i].tiempo);
                    if (res > 0)
                    {
                        double temp = Convert.ToDouble(valores[i].tiempo);
                        cuadrado += Math.Pow(temp - media, 2);
                        contador++;
                    }
                }
            }

            double desvacion = cuadrado / contador;
            desvacion = Math.Sqrt(desvacion);
            desvacion = Math.Round(desvacion, 2);
            return desvacion;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (mainEntities db = new mainEntities())
                {
                    int id = Convert.ToInt32(idTestTodo);
                    SujetosEvaluados res = db.SujetosEvaluados.Where(s => s.idSujetoEvaluado == id).FirstOrDefault<SujetosEvaluados>();

                    if (label27.Text == "Con sonido")
                        res.PTrcomples = null;
                    else
                        res.PTrcomple = null;


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

                    this.Close();

                    MessageBox.Show("Se ha eliminado la prueba", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (label27.Text == "Con sonido")
            {
                exportarConSonido();
            }
            else
            {
                exportarSinSonido();
            }
        }

        private void exportarConSonido()
        {
            using (mainEntities db = new mainEntities())
            {

                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == label32.Text).FirstOrDefault();
                String ci = res.NCarnetIdent;

                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\TRCS.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = label32.Text + " Tiempo de Reacción Complejo (Sonido)";

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

                            Bookmark bkmfechae = wordDoc.Bookmarks["K"];
                            Range rng1e = bkmfechae.Range;
                            rng1e.Text = " " + label25.Text;


                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + label32.Text;


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
                            rng6.Text = " " + res.NCarnetIdent;

                            

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
                           

                            if (pru.DesvStandar != null)
                            {
                                Bookmark ptoTotal = wordDoc.Bookmarks["F"];
                                Range pt = ptoTotal.Range;
                                pt.Text = " " + pru.DesvStandar;
                            }
                            else
                            {
                                Bookmark ptoTotal = wordDoc.Bookmarks["F"];
                                Range pt = ptoTotal.Range;
                                pt.Text = " 0";
                            }
                           


                            if (pru.CoefVariacion != null)
                            {
                                Bookmark ptoPor = wordDoc.Bookmarks["G"];
                                Range porc = ptoPor.Range;
                                porc.Text = " " + pru.CoefVariacion;
                            }
                            else
                            {
                                Bookmark ptoPor = wordDoc.Bookmarks["G"];
                                Range porc = ptoPor.Range;
                                porc.Text = " 0" ;
                            }
                          

                            if (pru.CantOmisiones != null)
                            {
                                Bookmark ptoRa = wordDoc.Bookmarks["H"];
                                Range porR = ptoRa.Range;
                                porR.Text = " " + pru.CantOmisiones;
                            }
                            else
                            {
                                Bookmark ptoRa = wordDoc.Bookmarks["H"];
                                Range porR = ptoRa.Range;
                                porR.Text = " 0";
                            }
                           

                            Bookmark ptoRa2 = wordDoc.Bookmarks["I"];
                            Range porR2 = ptoRa2.Range;
                            porR2.Text = " " + pru.CantErrorColor;


                           


                            
                            //-----------------------------------------///



                            {
                                //valores
                                if (0 < valores.Count)
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = " " + valores[0].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V1"];
                                    Range t1rv = v.Range;
                                    t1rv.Text = " " + valores[0].colorArduino;



                                    Bookmark v1 = wordDoc.Bookmarks["R1"];
                                    Range t1rv1 = v1.Range;
                                    t1rv1.Text = " " + valores[0].tiempo;

                                   


                                }
                                else
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = "";


                                    Bookmark v = wordDoc.Bookmarks["V1"];
                                    Range t1rv = v.Range;
                                    t1rv.Text = "";



                                    Bookmark v1 = wordDoc.Bookmarks["R1"];
                                    Range t1rv1 = v1.Range;
                                    t1rv1.Text = "";

                                }



                                if (1 < valores.Count)
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " " + valores[1].colorPantalla;


                                    Bookmark vr = wordDoc.Bookmarks["V2"];
                                    Range t2rv = vr.Range;
                                    t2rv.Text = " " + valores[1].colorArduino;



                                    Bookmark v2 = wordDoc.Bookmarks["R2"];
                                    Range t2rv2 = v2.Range;
                                    t2rv2.Text = " " + valores[1].tiempo;



                                }
                                else
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " ";


                                    Bookmark v24 = wordDoc.Bookmarks["V2"];
                                    Range t2rv = v24.Range;
                                    t2rv.Text = " ";



                                    Bookmark v2 = wordDoc.Bookmarks["R2"];
                                    Range t2rv2 = v2.Range;
                                    t2rv2.Text = " ";


                                }


                                if (2 < valores.Count)
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " " + valores[2].colorPantalla;


                                    Bookmark vt = wordDoc.Bookmarks["V3"];
                                    Range t3rv = vt.Range;
                                    t3rv.Text = " " + valores[2].colorArduino;



                                    Bookmark v3 = wordDoc.Bookmarks["R3"];
                                    Range t3rv3 = v3.Range;
                                    t3rv3.Text = " " + valores[2].tiempo;


                                }
                                else
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " ";


                                    Bookmark v2 = wordDoc.Bookmarks["V3"];
                                    Range t3rv = v2.Range;
                                    t3rv.Text = " ";



                                    Bookmark v3 = wordDoc.Bookmarks["R3"];
                                    Range t3rv3 = v3.Range;
                                    t3rv3.Text = " ";




                                }


                                if (3 < valores.Count)
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " " + valores[3].colorPantalla;


                                    Bookmark vy = wordDoc.Bookmarks["V4"];
                                    Range t4rv = vy.Range;
                                    t4rv.Text = " " + valores[3].colorArduino;



                                    Bookmark v4 = wordDoc.Bookmarks["R4"];
                                    Range t4rv4 = v4.Range;
                                    t4rv4.Text = " " + valores[3].tiempo;




                                }
                                else
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V4"];
                                    Range t4rv = vr.Range;
                                    t4rv.Text = " ";



                                    Bookmark v4 = wordDoc.Bookmarks["R4"];
                                    Range t4rv4 = v4.Range;
                                    t4rv4.Text = " ";



                                }



                                if (4 < valores.Count)
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " " + valores[4].colorPantalla;


                                    Bookmark vq = wordDoc.Bookmarks["V5"];
                                    Range t5rv = vq.Range;
                                    t5rv.Text = " " + valores[4].colorArduino;



                                    Bookmark v5 = wordDoc.Bookmarks["R5"];
                                    Range t5rv5 = v5.Range;
                                    t5rv5.Text = " " + valores[4].tiempo;


                                }
                                else
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V5"];
                                    Range t5rv = vr.Range;
                                    t5rv.Text = " ";



                                    Bookmark v5 = wordDoc.Bookmarks["R5"];
                                    Range t5rv5 = v5.Range;
                                    t5rv5.Text = " ";


                                }



                                if (5 < valores.Count)
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " " + valores[5].colorPantalla;


                                    Bookmark vq = wordDoc.Bookmarks["V6"];
                                    Range t6rv = vq.Range;
                                    t6rv.Text = " " + valores[5].colorArduino;



                                    Bookmark v6 = wordDoc.Bookmarks["R6"];
                                    Range t6rv6 = v6.Range;
                                    t6rv6.Text = " " + valores[5].tiempo;


                                }
                                else
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V6"];
                                    Range t6rv = vr.Range;
                                    t6rv.Text = " ";



                                    Bookmark v6 = wordDoc.Bookmarks["R6"];
                                    Range t6rv6 = v6.Range;
                                    t6rv6.Text = " ";




                                }


                                if (6 < valores.Count)
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " " + valores[6].colorPantalla;


                                    Bookmark v3 = wordDoc.Bookmarks["V7"];
                                    Range t7rv = v3.Range;
                                    t7rv.Text = " " + valores[6].colorArduino;



                                    Bookmark v7 = wordDoc.Bookmarks["R7"];
                                    Range t7rv7 = v7.Range;
                                    t7rv7.Text = " " + valores[6].tiempo;




                                }
                                else
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " ";


                                    Bookmark ve = wordDoc.Bookmarks["V7"];
                                    Range t7rv = ve.Range;
                                    t7rv.Text = " ";



                                    Bookmark v7 = wordDoc.Bookmarks["R7"];
                                    Range t7rv7 = v7.Range;
                                    t7rv7.Text = " ";




                                }



                                if (7 < valores.Count)
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " " + valores[7].colorPantalla;


                                    Bookmark v2 = wordDoc.Bookmarks["V8"];
                                    Range t8rv = v2.Range;
                                    t8rv.Text = " " + valores[7].colorArduino;



                                    Bookmark v8 = wordDoc.Bookmarks["R8"];
                                    Range t8rv8 = v8.Range;
                                    t8rv8.Text = " " + valores[7].tiempo;




                                }
                                else
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " ";


                                    Bookmark vq = wordDoc.Bookmarks["V8"];
                                    Range t8rv = vq.Range;
                                    t8rv.Text = " ";



                                    Bookmark v8 = wordDoc.Bookmarks["R8"];
                                    Range t8rv8 = v8.Range;
                                    t8rv8.Text = " ";




                                }





                                if (8 < valores.Count)
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " " + valores[8].colorPantalla;


                                    Bookmark ve = wordDoc.Bookmarks["V9"];
                                    Range t9rv = ve.Range;
                                    t9rv.Text = " " + valores[8].colorArduino;



                                    Bookmark v9 = wordDoc.Bookmarks["R9"];
                                    Range t9rv9 = v9.Range;
                                    t9rv9.Text = " " + valores[8].tiempo;




                                }
                                else
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V9"];
                                    Range t9rv = vr.Range;
                                    t9rv.Text = " ";



                                    Bookmark v9 = wordDoc.Bookmarks["R9"];
                                    Range t9rv9 = v9.Range;
                                    t9rv9.Text = " ";



                                }


                                if (9 < valores.Count)
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " " + valores[9].colorPantalla;


                                    Bookmark vt = wordDoc.Bookmarks["V10"];
                                    Range t10rv = vt.Range;
                                    t10rv.Text = " " + valores[9].colorArduino;



                                    Bookmark v10 = wordDoc.Bookmarks["R10"];
                                    Range t10rv10 = v10.Range;
                                    t10rv10.Text = " " + valores[9].tiempo;




                                }
                                else
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " ";


                                    Bookmark vw = wordDoc.Bookmarks["V10"];
                                    Range t10rv = vw.Range;
                                    t10rv.Text = " ";



                                    Bookmark v10 = wordDoc.Bookmarks["R10"];
                                    Range t10rv10 = v10.Range;
                                    t10rv10.Text = " ";




                                }



                                if (10 < valores.Count)
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " " + valores[10].colorPantalla;


                                    Bookmark vt = wordDoc.Bookmarks["V11"];
                                    Range t11rv = vt.Range;
                                    t11rv.Text = " " + valores[10].colorArduino;



                                    Bookmark v11 = wordDoc.Bookmarks["R11"];
                                    Range t11rv11 = v11.Range;
                                    t11rv11.Text = " " + valores[10].tiempo;




                                }
                                else
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " ";


                                    Bookmark v3 = wordDoc.Bookmarks["V11"];
                                    Range t11rv = v3.Range;
                                    t11rv.Text = " ";



                                    Bookmark v11 = wordDoc.Bookmarks["R11"];
                                    Range t11rv11 = v11.Range;
                                    t11rv11.Text = " ";



                                }


                                if (11 < valores.Count)
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = " " + valores[11].colorPantalla;


                                    Bookmark v3 = wordDoc.Bookmarks["V12"];
                                    Range t12rv = v3.Range;
                                    t12rv.Text = " " + valores[11].colorArduino;



                                    Bookmark v12 = wordDoc.Bookmarks["R12"];
                                    Range t12rv12 = v12.Range;
                                    t12rv12.Text = " " + valores[11].tiempo;



                                }
                                else
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V12"];
                                    Range t12rv = v.Range;
                                    t12rv.Text = " ";



                                    Bookmark v12 = wordDoc.Bookmarks["R12"];
                                    Range t12rv12 = v12.Range;
                                    t12rv12.Text = " ";




                                }


                                if (12 < valores.Count)
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = " " + valores[12].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V13"];
                                    Range t13rv = v.Range;
                                    t13rv.Text = " " + valores[12].colorArduino;



                                    Bookmark v13 = wordDoc.Bookmarks["R13"];
                                    Range t13rv13 = v13.Range;
                                    t13rv13.Text = " " + valores[12].tiempo;



                                }
                                else
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V13"];
                                    Range t13rv = v.Range;
                                    t13rv.Text = " ";



                                    Bookmark v13 = wordDoc.Bookmarks["R13"];
                                    Range t13rv13 = v13.Range;
                                    t13rv13.Text = " ";


                                }


                                if (13 < valores.Count)
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = " " + valores[14].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V14"];
                                    Range t14rv = v.Range;
                                    t14rv.Text = " " + valores[14].colorArduino;



                                    Bookmark v14 = wordDoc.Bookmarks["R14"];
                                    Range t14rv14 = v14.Range;
                                    t14rv14.Text = " " + valores[14].tiempo;




                                }
                                else
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V14"];
                                    Range t14rv = v.Range;
                                    t14rv.Text = " ";



                                    Bookmark v14 = wordDoc.Bookmarks["R14"];
                                    Range t14rv14 = v14.Range;
                                    t14rv14.Text = " ";




                                }


                                if (14 < valores.Count)
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = " " + valores[14].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V15"];
                                    Range t15rv = v.Range;
                                    t15rv.Text = " " + valores[14].colorArduino;



                                    Bookmark v15 = wordDoc.Bookmarks["R15"];
                                    Range t15rv15 = v15.Range;
                                    t15rv15.Text = " " + valores[14].tiempo;




                                }
                                else
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V15"];
                                    Range t15rv = v.Range;
                                    t15rv.Text = " ";



                                    Bookmark v15 = wordDoc.Bookmarks["R15"];
                                    Range t15rv15 = v15.Range;
                                    t15rv15.Text = " ";



                                }


                                if (15 < valores.Count)
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = " " + valores[15].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V16"];
                                    Range t16rv = v.Range;
                                    t16rv.Text = " " + valores[15].colorArduino;



                                    Bookmark v16 = wordDoc.Bookmarks["R16"];
                                    Range t16rv16 = v16.Range;
                                    t16rv16.Text = " " + valores[15].tiempo;



                                }
                                else
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V16"];
                                    Range t16rv = v.Range;
                                    t16rv.Text = " ";



                                    Bookmark v16 = wordDoc.Bookmarks["R16"];
                                    Range t16rv16 = v16.Range;
                                    t16rv16.Text = " ";



                                }


                                if (16 < valores.Count)
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = " " + valores[16].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V17"];
                                    Range t17rv = v.Range;
                                    t17rv.Text = " " + valores[16].colorArduino;



                                    Bookmark v17 = wordDoc.Bookmarks["R17"];
                                    Range t17rv17 = v17.Range;
                                    t17rv17.Text = " " + valores[16].tiempo;




                                }
                                else
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V17"];
                                    Range t17rv = v.Range;
                                    t17rv.Text = " ";



                                    Bookmark v17 = wordDoc.Bookmarks["R17"];
                                    Range t17rv17 = v17.Range;
                                    t17rv17.Text = " ";




                                }

                                if (17 < valores.Count)
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = " " + valores[17].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V18"];
                                    Range t18rv = v.Range;
                                    t18rv.Text = " " + valores[17].colorArduino;



                                    Bookmark v18 = wordDoc.Bookmarks["R18"];
                                    Range t18rv18 = v18.Range;
                                    t18rv18.Text = " " + valores[17].tiempo;



                                }
                                else
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V18"];
                                    Range t18rv = v.Range;
                                    t18rv.Text = " ";



                                    Bookmark v18 = wordDoc.Bookmarks["R18"];
                                    Range t18rv18 = v18.Range;
                                    t18rv18.Text = " ";



                                }


                                if (18 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[18].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V19"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[18].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R19"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[18].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V19"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R19"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }


                                if (19 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T20"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[19].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V20"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[19].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R20"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[19].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T20"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V20"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R20"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }










                                if (20 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T21"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[20].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V21"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[20].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R21"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[20].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T21"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V21"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R21"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }




                                if (21 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T22"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[21].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V22"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[21].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R22"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[21].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T22"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V22"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R22"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }



                                if (22 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T23"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[22].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V23"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[22].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R23"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[22].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T23"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V23"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";


                                    Bookmark v19 = wordDoc.Bookmarks["R23"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }


                                if (23 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T24"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[23].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V24"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[23].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R24"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[23].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T24"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V24"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R24"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }

                                if (24 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T25"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[24].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V25"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[24].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R25"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[24].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T25"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V25"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R25"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }

                                if (25 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T26"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[25].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V26"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[25].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R26"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[25].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T26"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V26"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R26"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (26 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T27"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[26].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V27"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[26].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R27"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[26].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T27"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V27"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R27"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (27 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T28"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[27].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V28"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[27].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R28"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[27].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T28"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V28"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R28"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (28 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T29"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[28].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V29"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[28].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R29"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[28].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T29"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V29"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R29"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (29 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T30"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[29].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V30"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[29].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R30"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[29].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T30"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V30"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R30"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



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

        private void exportarSinSonido()
        {

            using (mainEntities db = new mainEntities())
            {

                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == label32.Text).FirstOrDefault();
                
                String ci = res.NCarnetIdent;
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\TRC.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = label32.Text + " Tiempo de Reacción Complejo";

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
                            rng2.Text = " " + label32.Text;


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
                            rng6.Text = " " + res.NCarnetIdent;

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

                            Bookmark ptoRa2 = wordDoc.Bookmarks["I"];
                            Range porR2 = ptoRa2.Range;
                            porR2.Text = " " + pru.CantErrorColor;

                     
                            //-----------------------------------------///



                            {
                                //valores
                                if (0 < valores.Count)
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = " " + valores[0].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V1"];
                                    Range t1rv = v.Range;
                                    t1rv.Text = " " + valores[0].colorArduino;



                                    Bookmark v1 = wordDoc.Bookmarks["R1"];
                                    Range t1rv1 = v1.Range;
                                    t1rv1.Text = " " + valores[0].tiempo;




                                }
                                else
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = "";


                                    Bookmark v = wordDoc.Bookmarks["V1"];
                                    Range t1rv = v.Range;
                                    t1rv.Text = "";



                                    Bookmark v1 = wordDoc.Bookmarks["R1"];
                                    Range t1rv1 = v1.Range;
                                    t1rv1.Text = "";

                                }



                                if (1 < valores.Count)
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " " + valores[1].colorPantalla;


                                    Bookmark vr = wordDoc.Bookmarks["V2"];
                                    Range t2rv = vr.Range;
                                    t2rv.Text = " " + valores[1].colorArduino;



                                    Bookmark v2 = wordDoc.Bookmarks["R2"];
                                    Range t2rv2 = v2.Range;
                                    t2rv2.Text = " " + valores[1].tiempo;



                                }
                                else
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " ";


                                    Bookmark v24 = wordDoc.Bookmarks["V2"];
                                    Range t2rv = v24.Range;
                                    t2rv.Text = " ";



                                    Bookmark v2 = wordDoc.Bookmarks["R2"];
                                    Range t2rv2 = v2.Range;
                                    t2rv2.Text = " ";


                                }


                                if (2 < valores.Count)
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " " + valores[2].colorPantalla;


                                    Bookmark vt = wordDoc.Bookmarks["V3"];
                                    Range t3rv = vt.Range;
                                    t3rv.Text = " " + valores[2].colorArduino;



                                    Bookmark v3 = wordDoc.Bookmarks["R3"];
                                    Range t3rv3 = v3.Range;
                                    t3rv3.Text = " " + valores[2].tiempo;


                                }
                                else
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " ";


                                    Bookmark v2 = wordDoc.Bookmarks["V3"];
                                    Range t3rv = v2.Range;
                                    t3rv.Text = " ";



                                    Bookmark v3 = wordDoc.Bookmarks["R3"];
                                    Range t3rv3 = v3.Range;
                                    t3rv3.Text = " ";




                                }


                                if (3 < valores.Count)
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " " + valores[3].colorPantalla;


                                    Bookmark vy = wordDoc.Bookmarks["V4"];
                                    Range t4rv = vy.Range;
                                    t4rv.Text = " " + valores[3].colorArduino;



                                    Bookmark v4 = wordDoc.Bookmarks["R4"];
                                    Range t4rv4 = v4.Range;
                                    t4rv4.Text = " " + valores[3].tiempo;




                                }
                                else
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V4"];
                                    Range t4rv = vr.Range;
                                    t4rv.Text = " ";



                                    Bookmark v4 = wordDoc.Bookmarks["R4"];
                                    Range t4rv4 = v4.Range;
                                    t4rv4.Text = " ";



                                }



                                if (4 < valores.Count)
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " " + valores[4].colorPantalla;


                                    Bookmark vq = wordDoc.Bookmarks["V5"];
                                    Range t5rv = vq.Range;
                                    t5rv.Text = " " + valores[4].colorArduino;



                                    Bookmark v5 = wordDoc.Bookmarks["R5"];
                                    Range t5rv5 = v5.Range;
                                    t5rv5.Text = " " + valores[4].tiempo;


                                }
                                else
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V5"];
                                    Range t5rv = vr.Range;
                                    t5rv.Text = " ";



                                    Bookmark v5 = wordDoc.Bookmarks["R5"];
                                    Range t5rv5 = v5.Range;
                                    t5rv5.Text = " ";


                                }



                                if (5 < valores.Count)
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " " + valores[5].colorPantalla;


                                    Bookmark vq = wordDoc.Bookmarks["V6"];
                                    Range t6rv = vq.Range;
                                    t6rv.Text = " " + valores[5].colorArduino;



                                    Bookmark v6 = wordDoc.Bookmarks["R6"];
                                    Range t6rv6 = v6.Range;
                                    t6rv6.Text = " " + valores[5].tiempo;


                                }
                                else
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V6"];
                                    Range t6rv = vr.Range;
                                    t6rv.Text = " ";



                                    Bookmark v6 = wordDoc.Bookmarks["R6"];
                                    Range t6rv6 = v6.Range;
                                    t6rv6.Text = " ";




                                }


                                if (6 < valores.Count)
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " " + valores[6].colorPantalla;


                                    Bookmark v3 = wordDoc.Bookmarks["V7"];
                                    Range t7rv = v3.Range;
                                    t7rv.Text = " " + valores[6].colorArduino;



                                    Bookmark v7 = wordDoc.Bookmarks["R7"];
                                    Range t7rv7 = v7.Range;
                                    t7rv7.Text = " " + valores[6].tiempo;




                                }
                                else
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " ";


                                    Bookmark ve = wordDoc.Bookmarks["V7"];
                                    Range t7rv = ve.Range;
                                    t7rv.Text = " ";



                                    Bookmark v7 = wordDoc.Bookmarks["R7"];
                                    Range t7rv7 = v7.Range;
                                    t7rv7.Text = " ";




                                }



                                if (7 < valores.Count)
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " " + valores[7].colorPantalla;


                                    Bookmark v2 = wordDoc.Bookmarks["V8"];
                                    Range t8rv = v2.Range;
                                    t8rv.Text = " " + valores[7].colorArduino;



                                    Bookmark v8 = wordDoc.Bookmarks["R8"];
                                    Range t8rv8 = v8.Range;
                                    t8rv8.Text = " " + valores[7].tiempo;




                                }
                                else
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " ";


                                    Bookmark vq = wordDoc.Bookmarks["V8"];
                                    Range t8rv = vq.Range;
                                    t8rv.Text = " ";



                                    Bookmark v8 = wordDoc.Bookmarks["R8"];
                                    Range t8rv8 = v8.Range;
                                    t8rv8.Text = " ";




                                }





                                if (8 < valores.Count)
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " " + valores[8].colorPantalla;


                                    Bookmark ve = wordDoc.Bookmarks["V9"];
                                    Range t9rv = ve.Range;
                                    t9rv.Text = " " + valores[8].colorArduino;



                                    Bookmark v9 = wordDoc.Bookmarks["R9"];
                                    Range t9rv9 = v9.Range;
                                    t9rv9.Text = " " + valores[8].tiempo;




                                }
                                else
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " ";


                                    Bookmark vr = wordDoc.Bookmarks["V9"];
                                    Range t9rv = vr.Range;
                                    t9rv.Text = " ";



                                    Bookmark v9 = wordDoc.Bookmarks["R9"];
                                    Range t9rv9 = v9.Range;
                                    t9rv9.Text = " ";



                                }


                                if (9 < valores.Count)
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " " + valores[9].colorPantalla;


                                    Bookmark vt = wordDoc.Bookmarks["V10"];
                                    Range t10rv = vt.Range;
                                    t10rv.Text = " " + valores[9].colorArduino;



                                    Bookmark v10 = wordDoc.Bookmarks["R10"];
                                    Range t10rv10 = v10.Range;
                                    t10rv10.Text = " " + valores[9].tiempo;




                                }
                                else
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " ";


                                    Bookmark vw = wordDoc.Bookmarks["V10"];
                                    Range t10rv = vw.Range;
                                    t10rv.Text = " ";



                                    Bookmark v10 = wordDoc.Bookmarks["R10"];
                                    Range t10rv10 = v10.Range;
                                    t10rv10.Text = " ";




                                }



                                if (10 < valores.Count)
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " " + valores[10].colorPantalla;


                                    Bookmark vt = wordDoc.Bookmarks["V11"];
                                    Range t11rv = vt.Range;
                                    t11rv.Text = " " + valores[10].colorArduino;



                                    Bookmark v11 = wordDoc.Bookmarks["R11"];
                                    Range t11rv11 = v11.Range;
                                    t11rv11.Text = " " + valores[10].tiempo;




                                }
                                else
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " ";


                                    Bookmark v3 = wordDoc.Bookmarks["V11"];
                                    Range t11rv = v3.Range;
                                    t11rv.Text = " ";



                                    Bookmark v11 = wordDoc.Bookmarks["R11"];
                                    Range t11rv11 = v11.Range;
                                    t11rv11.Text = " ";



                                }


                                if (11 < valores.Count)
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = " " + valores[11].colorPantalla;


                                    Bookmark v3 = wordDoc.Bookmarks["V12"];
                                    Range t12rv = v3.Range;
                                    t12rv.Text = " " + valores[11].colorArduino;



                                    Bookmark v12 = wordDoc.Bookmarks["R12"];
                                    Range t12rv12 = v12.Range;
                                    t12rv12.Text = " " + valores[11].tiempo;



                                }
                                else
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V12"];
                                    Range t12rv = v.Range;
                                    t12rv.Text = " ";



                                    Bookmark v12 = wordDoc.Bookmarks["R12"];
                                    Range t12rv12 = v12.Range;
                                    t12rv12.Text = " ";




                                }


                                if (12 < valores.Count)
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = " " + valores[12].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V13"];
                                    Range t13rv = v.Range;
                                    t13rv.Text = " " + valores[12].colorArduino;



                                    Bookmark v13 = wordDoc.Bookmarks["R13"];
                                    Range t13rv13 = v13.Range;
                                    t13rv13.Text = " " + valores[12].tiempo;



                                }
                                else
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V13"];
                                    Range t13rv = v.Range;
                                    t13rv.Text = " ";



                                    Bookmark v13 = wordDoc.Bookmarks["R13"];
                                    Range t13rv13 = v13.Range;
                                    t13rv13.Text = " ";


                                }


                                if (13 < valores.Count)
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = " " + valores[14].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V14"];
                                    Range t14rv = v.Range;
                                    t14rv.Text = " " + valores[14].colorArduino;



                                    Bookmark v14 = wordDoc.Bookmarks["R14"];
                                    Range t14rv14 = v14.Range;
                                    t14rv14.Text = " " + valores[14].tiempo;




                                }
                                else
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V14"];
                                    Range t14rv = v.Range;
                                    t14rv.Text = " ";



                                    Bookmark v14 = wordDoc.Bookmarks["R14"];
                                    Range t14rv14 = v14.Range;
                                    t14rv14.Text = " ";




                                }


                                if (14 < valores.Count)
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = " " + valores[14].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V15"];
                                    Range t15rv = v.Range;
                                    t15rv.Text = " " + valores[14].colorArduino;



                                    Bookmark v15 = wordDoc.Bookmarks["R15"];
                                    Range t15rv15 = v15.Range;
                                    t15rv15.Text = " " + valores[14].tiempo;




                                }
                                else
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V15"];
                                    Range t15rv = v.Range;
                                    t15rv.Text = " ";



                                    Bookmark v15 = wordDoc.Bookmarks["R15"];
                                    Range t15rv15 = v15.Range;
                                    t15rv15.Text = " ";



                                }


                                if (15 < valores.Count)
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = " " + valores[15].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V16"];
                                    Range t16rv = v.Range;
                                    t16rv.Text = " " + valores[15].colorArduino;



                                    Bookmark v16 = wordDoc.Bookmarks["R16"];
                                    Range t16rv16 = v16.Range;
                                    t16rv16.Text = " " + valores[15].tiempo;



                                }
                                else
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V16"];
                                    Range t16rv = v.Range;
                                    t16rv.Text = " ";



                                    Bookmark v16 = wordDoc.Bookmarks["R16"];
                                    Range t16rv16 = v16.Range;
                                    t16rv16.Text = " ";



                                }


                                if (16 < valores.Count)
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = " " + valores[16].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V17"];
                                    Range t17rv = v.Range;
                                    t17rv.Text = " " + valores[16].colorArduino;



                                    Bookmark v17 = wordDoc.Bookmarks["R17"];
                                    Range t17rv17 = v17.Range;
                                    t17rv17.Text = " " + valores[16].tiempo;




                                }
                                else
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V17"];
                                    Range t17rv = v.Range;
                                    t17rv.Text = " ";



                                    Bookmark v17 = wordDoc.Bookmarks["R17"];
                                    Range t17rv17 = v17.Range;
                                    t17rv17.Text = " ";




                                }

                                if (17 < valores.Count)
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = " " + valores[17].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V18"];
                                    Range t18rv = v.Range;
                                    t18rv.Text = " " + valores[17].colorArduino;



                                    Bookmark v18 = wordDoc.Bookmarks["R18"];
                                    Range t18rv18 = v18.Range;
                                    t18rv18.Text = " " + valores[17].tiempo;



                                }
                                else
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V18"];
                                    Range t18rv = v.Range;
                                    t18rv.Text = " ";



                                    Bookmark v18 = wordDoc.Bookmarks["R18"];
                                    Range t18rv18 = v18.Range;
                                    t18rv18.Text = " ";



                                }


                                if (18 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[18].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V19"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[18].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R19"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[18].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V19"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R19"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }


                                if (19 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T20"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[19].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V20"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[19].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R20"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[19].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T20"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V20"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R20"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }










                                if (20 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T21"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[20].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V21"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[20].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R21"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[20].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T21"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V21"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R21"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }




                                if (21 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T22"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[21].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V22"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[21].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R22"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[21].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T22"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V22"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R22"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }



                                if (22 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T23"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[22].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V23"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[22].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R23"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[22].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T23"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V23"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";


                                    Bookmark v19 = wordDoc.Bookmarks["R23"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }


                                if (23 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T24"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[23].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V24"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[23].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R24"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[23].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T24"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V24"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R24"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }

                                if (24 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T25"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[24].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V25"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[24].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R25"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[24].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T25"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V25"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R25"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }

                                if (25 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T26"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[25].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V26"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[25].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R26"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[25].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T26"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V26"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R26"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (26 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T27"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[26].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V27"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[26].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R27"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[26].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T27"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V27"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R27"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (27 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T28"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[27].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V28"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[27].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R28"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[27].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T28"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V28"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R28"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (28 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T29"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[28].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V29"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[28].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R29"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[28].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T29"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V29"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R29"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



                                }
                                if (29 < valores.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T30"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + valores[29].colorPantalla;


                                    Bookmark v = wordDoc.Bookmarks["V30"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + valores[29].colorArduino;



                                    Bookmark v19 = wordDoc.Bookmarks["R30"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + valores[29].tiempo;


                                }
                                else
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T30"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V30"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " ";



                                    Bookmark v19 = wordDoc.Bookmarks["R30"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " ";



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

                    MessageBox.Show("Ha ocurrido un error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }


        }
    }
}
