using Microsoft.Office.Interop.Word;
using Multitest.ADOmodel;
using Multitest.FormAux;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Multitest
{
    public partial class RA_Form3 : Form
    {





        List<Auxiliar> tiemposTemp = new List<Auxiliar>();
        List<Auxiliar> tiempos = new List<Auxiliar>();
        List<Double> tiempoModa = new List<double>();
        int media = 0;
        String varianteNombre = null;
        String idTodo;
        String idTestPrueba;
        PruResanti resp = new PruResanti();


        double mediaGeneral = 0;
        double media1 = 0;
        double media2 = 0;
        double media3 = 0;
        double media4 = 0;
        int omisiones = 0;

        String idPerson;
        String idEtapa;
        String nombreAtleta;

        String modalidad = "";
        String deporte = "";
        String edad = "";
        String etapa = "";

        public RA_Form3(List<Auxiliar> tiempos, String varianteNombre, String idPerson, String idEtapa, String nombreAtleta, string vaiantePrueba)
        {

            this.varianteNombre = varianteNombre;

            InitializeComponent();
            this.tiempos = tiempos;
            //this.tiemposTemp = tiempos;
            //positivosTiempo();
            label34.Text = varianteNombre;
            label25.Text = nombreAtleta;
            this.idPerson = idPerson;
            this.idEtapa = idEtapa;
            this.nombreAtleta = nombreAtleta;


            llenarTabla();
            calcularMedias();
            desviacionEstandart();
            CalcularModaGeneral();
            calcularMedianaGeneral();
            CalcularMedianaModaPorVelocidad();
            salvarDatos();
        }



        public RA_Form3(String nombreAtleta, String idTest, String idTodo, String idAtleta, String etapa)
        {

            InitializeComponent();
            label25.Text = nombreAtleta;
            this.nombreAtleta = nombreAtleta;
            llenarCampos(idTest);
            this.idTodo = idTodo;
            this.idTestPrueba = idTest;

            if (idAtleta != null)
            {
                buscarAtleta(idAtleta);
                buscarEtapa();
            }


            if (idTodo != null)
            {
                button1.Visible = true;
                button2.Visible = true;
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

        private void llenarCampos(string idTest)
        {

            using (mainEntities entities = new mainEntities())
            {

                var test = entities.PruResanti.Find(Convert.ToInt32(idTest));

                llenarValores(test);
                llenarTabla();


                label17.Text = test.Media15 == "NaN" || test.Media15 == "0" ? "-" : test.Media15;
                label30.Text = test.Media2 == "NaN" || test.Media2 == "0" ? "-" : test.Media2; ;
                label35.Text = test.Media25 == "NaN" || test.Media25 == "0" ? "-" : test.Media25; ;
                label40.Text = test.Media3 == "NaN" || test.Media3 == "0" ? "-" : test.Media3; ;

                label20.Text = test.Mediana15 == "NaN" || test.Mediana15 == "0" ? "-" : test.Mediana15;
                label31.Text = test.Mediana2 == "NaN" || test.Mediana2 == "0" ? "-" : test.Mediana2;
                label36.Text = test.Mediana25 == "NaN" || test.Mediana25 == "0" ? "-" : test.Mediana25;
                label41.Text = test.Mediana3 == "NaN" || test.Mediana3 == "0" ? "-" : test.Mediana3;



                label13.Text = test.DesvStandar15 == "NaN" || test.DesvStandar15 == "0" ? "-" : test.DesvStandar15;
                label28.Text = test.DesvStandar2 == "NaN" || test.DesvStandar2 == "0" ? "-" : test.DesvStandar2;
                label33.Text = test.DesvStandar25 == "NaN" || test.DesvStandar25 == "0" ? "-" : test.DesvStandar25;
                label38.Text = test.DesvStandar3 == "NaN" || test.DesvStandar3 == "0" ? "-" : test.DesvStandar3;

                label11.Text = test.CoefVar15 == "NaN" || test.CoefVar15 == "0" ? "-" : test.CoefVar15;
                label27.Text = test.CoefVar2 == "NaN" || test.CoefVar2 == "0" ? "-" : test.CoefVar2; ;
                label32.Text = test.CoefVar25 == "NaN" || test.CoefVar25 == "0" ? "-" : test.CoefVar25;
                label37.Text = test.CoefVar3 == "NaN" || test.CoefVar3 == "0" ? "-" : test.CoefVar3;



                label2.Text = test.Media == "NaN" || test.Media == "0" ? "-" : test.Media;
                label4.Text = test.Mediana == "NaN" || test.Mediana == "0" ? "-" : test.Mediana;
                label6.Text = test.Moda == "NaN" || test.Moda == "0" ? "-" : test.Moda;
                label8.Text = test.DesvStandar == "NaN" || test.DesvStandar == "0" ? "-" : test.DesvStandar;
                label10.Text = test.CoefVar == "NaN" || test.CoefVar == "0" ? "-" : test.CoefVar;

                label22.Text = test.Calificacion;
                label34.Text = test.Programa;
                label42.Text = omisiones.ToString();




                resp.Media = test.Media == "NaN" || test.Media == "0" ? "-" : test.Media;
                resp.Mediana = test.Mediana == "NaN" || test.Mediana == "0" ? "-" : test.Mediana;
                resp.Moda = test.Moda == "NaN" || test.Moda == "0" ? "-" : test.Moda;
                resp.DesvStandar = test.DesvStandar == "NaN" || test.DesvStandar == "0" ? "-" : test.DesvStandar;
                resp.CoefVar = test.CoefVar == "NaN" || test.CoefVar == "0" ? "-" : test.CoefVar;

                resp.Calificacion = test.Calificacion;
                resp.Programa = test.Programa;



                resp.DesvStandar15 = test.DesvStandar15;
                resp.DesvStandar2 = test.DesvStandar2;
                resp.DesvStandar25 = test.DesvStandar25;
                resp.DesvStandar3 = test.DesvStandar3;


                resp.CoefVar15 = test.CoefVar15;
                resp.CoefVar2 = test.CoefVar2;
                resp.CoefVar25 = test.CoefVar25;
                resp.CoefVar3 = test.CoefVar3;


                resp.Mediana15 = test.Mediana15;
                resp.Mediana2 = test.Mediana2;
                resp.Mediana25 = test.Mediana25;
                resp.Mediana3 = test.Mediana3;


                resp.Media15 = test.Media15;
                resp.Media2 = test.Media2;
                resp.Media25 = test.Media25;
                resp.Media3 = test.Media3;

                resp.Fecha = test.Fecha;



            }
        }

        private void llenarValores(PruResanti test)
        {
            tiempos = new List<Auxiliar>();

            Auxiliar a1 = new Auxiliar(test.Tiempo1, null, test.Resultado1, test.DiferenciaTiempo1, test.VariantVelo1);
            tiempos.Add(a1);

            Auxiliar a2 = new Auxiliar(test.Tiempo2, null, test.Resultado2, test.DiferenciaTiempo2, test.VariantVelo2);
            tiempos.Add(a2);


            Auxiliar a3 = new Auxiliar(test.Tiempo3, null, test.Resultado3, test.DiferenciaTiempo3, test.VariantVelo3);
            tiempos.Add(a3);

            Auxiliar a4 = new Auxiliar(test.Tiempo4, null, test.Resultado4, test.DiferenciaTiempo4, test.VariantVelo4);
            tiempos.Add(a4);

            Auxiliar a5 = new Auxiliar(test.Tiempo5, null, test.Resultado5, test.DiferenciaTiempo5, test.VariantVelo5);
            tiempos.Add(a5);

            Auxiliar a6 = new Auxiliar(test.Tiempo6, null, test.Resultado6, test.DiferenciaTiempo6, test.VariantVelo6);
            tiempos.Add(a6);

            Auxiliar a7 = new Auxiliar(test.Tiempo7, null, test.Resultado7, test.DiferenciaTiempo7, test.VariantVelo7);
            tiempos.Add(a7);

            Auxiliar a8 = new Auxiliar(test.Tiempo8, null, test.Resultado8, test.DiferenciaTiempo8, test.VariantVelo8);
            tiempos.Add(a8);

            Auxiliar a9 = new Auxiliar(test.Tiempo9, null, test.Resultado9, test.DiferenciaTiempo9, test.VariantVelo9);
            tiempos.Add(a9);

            Auxiliar a10 = new Auxiliar(test.Tiempo10, null, test.Resultado10, test.DiferenciaTiempo10, test.VariantVelo10);
            tiempos.Add(a10);

            Auxiliar a11 = new Auxiliar(test.Tiempo11, null, test.Resultado11, test.DiferenciaTiempo11, test.VariantVelo11);
            tiempos.Add(a11);

            Auxiliar a12 = new Auxiliar(test.Tiempo12, null, test.Resultado12, test.DiferenciaTiempo12, test.VariantVelo12);
            tiempos.Add(a12);

            Auxiliar a13 = new Auxiliar(test.Tiempo13, null, test.Resultado13, test.DiferenciaTiempo13, test.VariantVelo13);
            tiempos.Add(a13);

            Auxiliar a14 = new Auxiliar(test.Tiempo14, null, test.Resultado14, test.DiferenciaTiempo14, test.VariantVelo14);
            tiempos.Add(a14);

            Auxiliar a15 = new Auxiliar(test.Tiempo15, null, test.Resultado15, test.DiferenciaTiempo15, test.VariantVelo15);
            tiempos.Add(a15);

            Auxiliar a16 = new Auxiliar(test.Tiempo16, null, test.Resultado16, test.DiferenciaTiempo16, test.VariantVelo16);
            tiempos.Add(a16);

            Auxiliar a17 = new Auxiliar(test.Tiempo17, null, test.Resultado17, test.DiferenciaTiempo17, test.VariantVelo17);
            tiempos.Add(a17);

            Auxiliar a18 = new Auxiliar(test.Tiempo18, null, test.Resultado18, test.DiferenciaTiempo18, test.VariantVelo18);
            tiempos.Add(a18);

            Auxiliar a19 = new Auxiliar(test.Tiempo19, null, test.Resultado19, test.DiferenciaTiempo19, test.VariantVelo19);
            tiempos.Add(a19);

            Auxiliar a20 = new Auxiliar(test.Tiempo20, null, test.Resultado20, test.DiferenciaTiempo20, test.VariantVelo20);
            tiempos.Add(a20);



        }

        private void CalcularMedianaModaPorVelocidad()
        {
            List<double> list1 = new List<double>();
            List<double> list2 = new List<double>();
            List<double> list3 = new List<double>();
            List<double> list4 = new List<double>();
            omisiones = 0;
            foreach (var item in tiempos)
            {
                if (item.diferencia != "Omisión")
                {
                    double tiempo = Convert.ToDouble(item.diferencia);
                    if (item.sentidoVelocidad == "1" || item.sentidoVelocidad == "2")
                    {
                        list1.Add(tiempo);
                    }
                    if (item.sentidoVelocidad == "3" || item.sentidoVelocidad == "4")
                    {
                        list2.Add(tiempo);
                    }
                    if (item.sentidoVelocidad == "5" || item.sentidoVelocidad == "6")
                    {
                        list3.Add(tiempo);
                    }
                    if (item.sentidoVelocidad == "7" || item.sentidoVelocidad == "8")
                    {
                        list4.Add(tiempo);
                    }
                }
                if (item.diferencia == "Omisión")
                {
                  
                        omisiones++;

                }
            }
            label42.Text = omisiones.ToString();


            var temp1 = list1.OrderBy(x => x).ToList();
            var temp2 = list2.OrderBy(x => x).ToList();
            var temp3 = list3.OrderBy(x => x).ToList();
            var temp4 = list4.OrderBy(x => x).ToList();


            String mediana1 = calcularMediana(temp1).ToString();
            String mediana2 = calcularMediana(temp2).ToString();
            String mediana3 = calcularMediana(temp3).ToString();
            String mediana4 = calcularMediana(temp4).ToString();

            if (list1.Count > 0)
                label20.Text = mediana1 == "NaN" || mediana1 == "0" ? "-" : mediana1;
            if (list2.Count > 0)
                label31.Text = mediana2 == "NaN" || mediana2 == "0" ? "-" : mediana2;
            if (list3.Count > 0)
                label36.Text = mediana3 == "NaN" || mediana3 == "0" ? "-" : mediana3;
            if (list4.Count > 0)
                label41.Text = mediana4 == "NaN" || mediana4 == "0" ? "-" : mediana4;



        }



        private double calcularMediana(List<double> list)
        {
            double med = 0;
            if (list.Count > 0)
            {
                if (list.Count % 2 == 0)
                {
                    double val1 = list[(list.Count / 2) - 1];
                    double val2 = list[(list.Count / 2) - 1 + 1];
                    med = (val1 + val2) / 2;
                }
                else
                {
                    int pos = list.Count / 2;
                    med = list[pos];
                }
            }

            return med;


        }


        private void calcularMedianaGeneral()
        {

            List<Double> listTiempoTemp = tiempoModa.OrderBy(x => x).ToList();

            if (listTiempoTemp.Count > 0)
            {
                double med = 0;


                if (listTiempoTemp.Count % 2 == 0)
                {
                    double val1 = listTiempoTemp[(listTiempoTemp.Count / 2) - 1];
                    double val2 = listTiempoTemp[(listTiempoTemp.Count / 2) - 1 + 1];
                    med = (val1 + val2) / 2;
                }
                else
                {
                    int pos = listTiempoTemp.Count / 2;
                    med = listTiempoTemp[pos];
                }



                label4.Text = med.ToString();
            }


        }

        private void CalcularModaGeneral()
        {


            if (tiempoModa.Count > 0)
            {
                var group = tiempoModa.GroupBy(x => x);
                var res = group.OrderByDescending(x => x.Count()).First();
                label6.Text = res.Count() > 1 ? "" + res.Key : "-";
            }




        }

        private double CalcularModaTiempos(List<double> list)
        {
            double result = 0;
            if (list.Count > 0)
            {
                var group = list.GroupBy(x => x);
                var res = group.OrderByDescending(x => x.Count()).First();
                result = res.Count() > 1 ? res.Key : 0;
            }
            return result;

        }

        private void llenarTabla()
        {
            int atrasado = 0;
            int adelantado = 0;
            omisiones = 0;
            foreach (var item in tiempos)
            {
                if (item.diferencia != "Omisión")
                {
                    double tiempo = Convert.ToDouble(item.diferencia);
                    tiempoModa.Add(tiempo);
                    
                }
                if (item.diferencia == "Omisión")
                {
                    omisiones++;

                }



                if (item.Resultado == "Adelantado")
                {
                    adelantado++;
                }

                if (item.Resultado == "Atrasado")
                {

                    atrasado++;
                }




                if (item.sentidoVelocidad != null)
                {

                    dataGridView1.Rows.Add(new object[] {

                  item.tiempo,
                  item.sentidoVelocidad,
                  item.Resultado,
                  item.diferencia,
                  });
                }

            }

            label22.Text = adelantado > atrasado ? "Adelantado" : "Atrasado";

        }

        private void calcularMedias()
        {

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;


            foreach (var item in tiempos)
            {
                int temp = 0;
                if (item.diferencia != "Omisión")
                {
                    temp = Convert.ToInt32(item.diferencia);
                    mediaGeneral += temp;
                }

                if ((item.sentidoVelocidad == "1" || item.sentidoVelocidad == "2") && item.diferencia != "Omisión")
                {
                    media1 += temp;
                    count1++;
                }
                if ((item.sentidoVelocidad == "3" || item.sentidoVelocidad == "4") && item.diferencia != "Omisión")
                {
                    media2 += temp;
                    count2++;
                }
                if ((item.sentidoVelocidad == "5" || item.sentidoVelocidad == "6") && item.diferencia != "Omisión")
                {
                    media3 += temp;
                    count3++;
                }
                if ((item.sentidoVelocidad == "7" || item.sentidoVelocidad == "8") && item.diferencia != "Omisión")
                {
                    media4 += temp;
                    count4++;
                }


            }


            int cantReal = count1 + count2 + count3 + count4;

            mediaGeneral = Math.Round(mediaGeneral / cantReal, 2);

            label2.Text = mediaGeneral.ToString() == "NaN" || mediaGeneral.ToString() == "0" ? "-" : mediaGeneral.ToString();

            if (count1 != 0)
            {
                media1 = Math.Round(media1 / count1, 2);
                label17.Text = media1.ToString() == "NaN" || media1.ToString() == "0" ? "-" : media1.ToString();
            }
            if (count2 != 0)
            {
                media2 = Math.Round(media2 / count2, 2);
                label30.Text = media2.ToString() == "NaN" || media2.ToString() == "0" ? "-" : media2.ToString();
            }
            if (count3 != 0)
            {
                media3 = Math.Round(media3 / count3, 2);
                label35.Text = media3.ToString() == "NaN" || media3.ToString() == "0" ? "-" : media3.ToString();
            }
            if (count4 != 0)
            {
                media4 = Math.Round(media4 / count4, 2);
                label40.Text = media4.ToString() == "NaN" || media4.ToString() == "0" ? "-" : media4.ToString();
            }



        }

        private void desviacionEstandart()
        {
            double cuadrado = 0;
            int contador = 0;

            double cuadrado1 = 0;
            int contador1 = 0;

            double cuadrado2 = 0;
            int contador2 = 0;

            double cuadrado3 = 0;
            int contador3 = 0;

            double cuadrado4 = 0;
            int contador4 = 0;

            foreach (var item in tiempos)
            {


                if (item.diferencia != "Omisión")
                {
                    double temp = Convert.ToDouble(item.diferencia);
                    cuadrado += Math.Pow(temp - mediaGeneral, 2);
                    contador++;



                    if (item.sentidoVelocidad == "1" || item.sentidoVelocidad == "2")
                    {
                        //velocidad1
                        double temp1 = Convert.ToDouble(item.diferencia);
                        cuadrado1 += Math.Pow(temp1 - media1, 2);
                        contador1++;
                    }
                    if (item.sentidoVelocidad == "3" || item.sentidoVelocidad == "4")
                    {
                        //velocidad2
                        double temp2 = Convert.ToDouble(item.diferencia);
                        cuadrado2 += Math.Pow(temp2 - media2, 2);
                        contador2++;
                    }
                    if (item.sentidoVelocidad == "5" || item.sentidoVelocidad == "6")
                    {
                        double temp3 = Convert.ToDouble(item.diferencia);
                        cuadrado3 += Math.Pow(temp3 - media3, 2);
                        contador3++;
                    }
                    if (item.sentidoVelocidad == "7" || item.sentidoVelocidad == "8")
                    {
                        double temp4 = Convert.ToDouble(item.diferencia);
                        cuadrado4 += Math.Pow(temp4 - media4, 2);
                        contador4++;
                    }
                }

            }

            double desvacion = 0;
            double desvacion1 = 0;
            double desvacion2 = 0;
            double desvacion3 = 0;
            double desvacion4 = 0;

            if (contador != 0)
            {
                desvacion = cuadrado / contador;
                desvacion = Math.Sqrt(desvacion);
                desvacion = Math.Round(desvacion, 2);
                label8.Text = desvacion.ToString();


                //Coeficiente de variacion

                String val = Math.Round(desvacion / mediaGeneral, 2).ToString();
                label10.Text = val == "NaN" ? "0.0" : val;
            }

            if (contador1 != 0)
            {
                //desviacion standart
                desvacion1 = cuadrado1 / contador1;
                desvacion1 = Math.Sqrt(desvacion1);
                desvacion1 = Math.Round(desvacion1, 2);
                label13.Text = desvacion1.ToString() == "NaN" ? "0.0" : desvacion1.ToString();

                //Coeficiente de variacion
                String val1 = Math.Round(desvacion1 / media1, 2).ToString();
                label11.Text = val1 == "NaN" ? "0.0" : val1;

            }

            if (contador2 != 0)
            {
                //desviacion 
                desvacion2 = cuadrado2 / contador2;
                desvacion2 = Math.Sqrt(desvacion2);
                desvacion2 = Math.Round(desvacion2, 2);
                label28.Text = desvacion2.ToString() == "NaN" ? "0.0" : desvacion2.ToString();

                //Coeficiente de variacion
                String val2 = Math.Round(desvacion2 / media2, 2).ToString();
                label27.Text = val2 == "NaN" ? "0.0" : val2;
            }


            if (contador3 != 0)
            {
                //desviacion 
                desvacion3 = cuadrado3 / contador3;
                desvacion3 = Math.Sqrt(desvacion3);
                desvacion3 = Math.Round(desvacion3, 2);
                label33.Text = desvacion3.ToString() == "NaN" ? "0.0" : desvacion3.ToString();

                //Coeficiente de variacion
                String val3 = Math.Round(desvacion3 / media3, 2).ToString();
                label32.Text = val3 == "NaN" ? "0.0" : val3;
            }


            if (contador4 != 0)
            {
                //desviacion 
                desvacion4 = cuadrado4 / contador4;
                desvacion4 = Math.Sqrt(desvacion4);
                desvacion4 = Math.Round(desvacion4, 2);
                label38.Text = desvacion4.ToString() == "NaN" ? "0.0" : desvacion4.ToString();

                // coeficiente de variacion
                String val4 = Math.Round(desvacion4 / media4, 2).ToString();
                label37.Text = val4 == "NaN" ? "0.0" : val4;
            }










        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }



        private void salvarDatos()
        {

            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {



                int idAtleta = Convert.ToInt32(idPerson);
                int idEta = Convert.ToInt32(idEtapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idAtleta).Where(g => g.Etapa == idEta).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    PruResanti pruR = new PruResanti();

                    pruR.Fecha = date;

                    pruR.Tiempo1 = tiempos.Count > 0 ? tiempos[0].tiempo.ToString() : null;
                    pruR.Tiempo2 = tiempos.Count > 1 ? tiempos[1].tiempo.ToString() : null;
                    pruR.Tiempo3 = tiempos.Count > 2 ? tiempos[2].tiempo.ToString() : null;
                    pruR.Tiempo4 = tiempos.Count > 3 ? tiempos[3].tiempo.ToString() : null;
                    pruR.Tiempo5 = tiempos.Count > 4 ? tiempos[4].tiempo.ToString() : null;
                    pruR.Tiempo6 = tiempos.Count > 5 ? tiempos[5].tiempo.ToString() : null;
                    pruR.Tiempo7 = tiempos.Count > 6 ? tiempos[6].tiempo.ToString() : null;
                    pruR.Tiempo8 = tiempos.Count > 7 ? tiempos[7].tiempo.ToString() : null;
                    pruR.Tiempo9 = tiempos.Count > 8 ? tiempos[8].tiempo.ToString() : null;
                    pruR.Tiempo10 = tiempos.Count > 9 ? tiempos[9].tiempo.ToString() : null;
                    pruR.Tiempo11 = tiempos.Count > 10 ? tiempos[10].tiempo.ToString() : null;
                    pruR.Tiempo12 = tiempos.Count > 11 ? tiempos[11].tiempo.ToString() : null;
                    pruR.Tiempo13 = tiempos.Count > 12 ? tiempos[12].tiempo.ToString() : null;
                    pruR.Tiempo14 = tiempos.Count > 13 ? tiempos[13].tiempo.ToString() : null;
                    pruR.Tiempo15 = tiempos.Count > 14 ? tiempos[14].tiempo.ToString() : null;
                    pruR.Tiempo16 = tiempos.Count > 15 ? tiempos[15].tiempo.ToString() : null;
                    pruR.Tiempo17 = tiempos.Count > 16 ? tiempos[16].tiempo.ToString() : null;
                    pruR.Tiempo18 = tiempos.Count > 17 ? tiempos[17].tiempo.ToString() : null;
                    pruR.Tiempo19 = tiempos.Count > 18 ? tiempos[18].tiempo.ToString() : null;
                    pruR.Tiempo20 = tiempos.Count > 19 ? tiempos[19].tiempo.ToString() : null;


                    pruR.Resultado1 = tiempos.Count > 0 ? tiempos[0].Resultado.ToString() : null;
                    pruR.Resultado2 = tiempos.Count > 1 ? tiempos[1].Resultado.ToString() : null;
                    pruR.Resultado3 = tiempos.Count > 2 ? tiempos[2].Resultado.ToString() : null;
                    pruR.Resultado4 = tiempos.Count > 3 ? tiempos[3].Resultado.ToString() : null;
                    pruR.Resultado5 = tiempos.Count > 4 ? tiempos[4].Resultado.ToString() : null;
                    pruR.Resultado6 = tiempos.Count > 5 ? tiempos[5].Resultado.ToString() : null;
                    pruR.Resultado7 = tiempos.Count > 6 ? tiempos[6].Resultado.ToString() : null;
                    pruR.Resultado8 = tiempos.Count > 7 ? tiempos[7].Resultado.ToString() : null;
                    pruR.Resultado9 = tiempos.Count > 8 ? tiempos[8].Resultado.ToString() : null;
                    pruR.Resultado10 = tiempos.Count > 9 ? tiempos[9].Resultado.ToString() : null;
                    pruR.Resultado11 = tiempos.Count > 10 ? tiempos[10].Resultado.ToString() : null;
                    pruR.Resultado12 = tiempos.Count > 11 ? tiempos[11].Resultado.ToString() : null;
                    pruR.Resultado13 = tiempos.Count > 12 ? tiempos[12].Resultado.ToString() : null;
                    pruR.Resultado14 = tiempos.Count > 13 ? tiempos[13].Resultado.ToString() : null;
                    pruR.Resultado15 = tiempos.Count > 14 ? tiempos[14].Resultado.ToString() : null;
                    pruR.Resultado16 = tiempos.Count > 15 ? tiempos[15].Resultado.ToString() : null;
                    pruR.Resultado17 = tiempos.Count > 16 ? tiempos[16].Resultado.ToString() : null;
                    pruR.Resultado18 = tiempos.Count > 17 ? tiempos[17].Resultado.ToString() : null;
                    pruR.Resultado19 = tiempos.Count > 18 ? tiempos[18].Resultado.ToString() : null;
                    pruR.Resultado20 = tiempos.Count > 19 ? tiempos[19].Resultado.ToString() : null;




                    pruR.VariantVelo1 = tiempos.Count > 0 ? tiempos[0].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo2 = tiempos.Count > 1 ? tiempos[1].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo3 = tiempos.Count > 2 ? tiempos[2].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo4 = tiempos.Count > 3 ? tiempos[3].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo5 = tiempos.Count > 4 ? tiempos[4].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo6 = tiempos.Count > 5 ? tiempos[5].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo7 = tiempos.Count > 6 ? tiempos[6].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo8 = tiempos.Count > 7 ? tiempos[7].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo9 = tiempos.Count > 8 ? tiempos[8].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo10 = tiempos.Count > 9 ? tiempos[9].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo11 = tiempos.Count > 10 ? tiempos[10].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo12 = tiempos.Count > 11 ? tiempos[11].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo13 = tiempos.Count > 12 ? tiempos[12].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo14 = tiempos.Count > 13 ? tiempos[13].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo15 = tiempos.Count > 14 ? tiempos[14].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo16 = tiempos.Count > 15 ? tiempos[15].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo17 = tiempos.Count > 16 ? tiempos[16].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo18 = tiempos.Count > 17 ? tiempos[17].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo19 = tiempos.Count > 18 ? tiempos[18].sentidoVelocidad.ToString() : null;
                    pruR.VariantVelo20 = tiempos.Count > 19 ? tiempos[19].sentidoVelocidad.ToString() : null;


                    pruR.DiferenciaTiempo1 = tiempos.Count > 0 ? tiempos[0].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo2 = tiempos.Count > 1 ? tiempos[1].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo3 = tiempos.Count > 2 ? tiempos[2].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo4 = tiempos.Count > 3 ? tiempos[3].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo5 = tiempos.Count > 4 ? tiempos[4].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo6 = tiempos.Count > 5 ? tiempos[5].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo7 = tiempos.Count > 6 ? tiempos[6].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo8 = tiempos.Count > 7 ? tiempos[7].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo9 = tiempos.Count > 8 ? tiempos[8].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo10 = tiempos.Count > 9 ? tiempos[9].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo11 = tiempos.Count > 10 ? tiempos[10].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo12 = tiempos.Count > 11 ? tiempos[11].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo13 = tiempos.Count > 12 ? tiempos[12].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo14 = tiempos.Count > 13 ? tiempos[13].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo15 = tiempos.Count > 14 ? tiempos[14].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo16 = tiempos.Count > 15 ? tiempos[15].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo17 = tiempos.Count > 16 ? tiempos[16].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo18 = tiempos.Count > 17 ? tiempos[17].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo19 = tiempos.Count > 18 ? tiempos[18].diferencia.ToString() : null;
                    pruR.DiferenciaTiempo20 = tiempos.Count > 19 ? tiempos[19].diferencia.ToString() : null;




                    pruR.Programa = label34.Text;
                    pruR.Media15 = label17.Text;
                    pruR.Media2 = label30.Text;
                    pruR.Media25 = label35.Text;
                    pruR.Media3 = label40.Text;

                    pruR.Mediana15 = label20.Text;
                    pruR.Mediana2 = label31.Text;
                    pruR.Mediana25 = label36.Text;
                    pruR.Mediana3 = label41.Text;




                    pruR.DesvStandar15 = label13.Text;
                    pruR.DesvStandar2 = label28.Text;
                    pruR.DesvStandar25 = label33.Text;
                    pruR.DesvStandar3 = label38.Text;

                    pruR.CoefVar15 = label11.Text;
                    pruR.CoefVar2 = label27.Text;
                    pruR.CoefVar25 = label32.Text;
                    pruR.CoefVar3 = label37.Text;



                    pruR.Media = label2.Text;
                    pruR.Mediana = label4.Text;
                    pruR.Moda = label6.Text;
                    pruR.DesvStandar = label8.Text;
                    pruR.CoefVar = label10.Text;
                    pruR.Calificacion = label22.Text;



                    entities.PruResanti.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruResanti>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(idEta);
                    pru.PResanti = ultimo.idTest;
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PResanti == null)
                    {

                        PruResanti pruR = new PruResanti();

                        pruR.Fecha = date;

                        pruR.Tiempo1 = tiempos.Count > 0 ? tiempos[0].tiempo.ToString() : null;
                        pruR.Tiempo2 = tiempos.Count > 1 ? tiempos[1].tiempo.ToString() : null;
                        pruR.Tiempo3 = tiempos.Count > 2 ? tiempos[2].tiempo.ToString() : null;
                        pruR.Tiempo4 = tiempos.Count > 3 ? tiempos[3].tiempo.ToString() : null;
                        pruR.Tiempo5 = tiempos.Count > 4 ? tiempos[4].tiempo.ToString() : null;
                        pruR.Tiempo6 = tiempos.Count > 5 ? tiempos[5].tiempo.ToString() : null;
                        pruR.Tiempo7 = tiempos.Count > 6 ? tiempos[6].tiempo.ToString() : null;
                        pruR.Tiempo8 = tiempos.Count > 7 ? tiempos[7].tiempo.ToString() : null;
                        pruR.Tiempo9 = tiempos.Count > 8 ? tiempos[8].tiempo.ToString() : null;
                        pruR.Tiempo10 = tiempos.Count > 9 ? tiempos[9].tiempo.ToString() : null;
                        pruR.Tiempo11 = tiempos.Count > 10 ? tiempos[10].tiempo.ToString() : null;
                        pruR.Tiempo12 = tiempos.Count > 11 ? tiempos[11].tiempo.ToString() : null;
                        pruR.Tiempo13 = tiempos.Count > 12 ? tiempos[12].tiempo.ToString() : null;
                        pruR.Tiempo14 = tiempos.Count > 13 ? tiempos[13].tiempo.ToString() : null;
                        pruR.Tiempo15 = tiempos.Count > 14 ? tiempos[14].tiempo.ToString() : null;
                        pruR.Tiempo16 = tiempos.Count > 15 ? tiempos[15].tiempo.ToString() : null;
                        pruR.Tiempo17 = tiempos.Count > 16 ? tiempos[16].tiempo.ToString() : null;
                        pruR.Tiempo18 = tiempos.Count > 17 ? tiempos[17].tiempo.ToString() : null;
                        pruR.Tiempo19 = tiempos.Count > 18 ? tiempos[18].tiempo.ToString() : null;
                        pruR.Tiempo20 = tiempos.Count > 19 ? tiempos[19].tiempo.ToString() : null;



                        pruR.Resultado1 = tiempos.Count > 0 ? tiempos[0].Resultado.ToString() : null;
                        pruR.Resultado2 = tiempos.Count > 1 ? tiempos[1].Resultado.ToString() : null;
                        pruR.Resultado3 = tiempos.Count > 2 ? tiempos[2].Resultado.ToString() : null;
                        pruR.Resultado4 = tiempos.Count > 3 ? tiempos[3].Resultado.ToString() : null;
                        pruR.Resultado5 = tiempos.Count > 4 ? tiempos[4].Resultado.ToString() : null;
                        pruR.Resultado6 = tiempos.Count > 5 ? tiempos[5].Resultado.ToString() : null;
                        pruR.Resultado7 = tiempos.Count > 6 ? tiempos[6].Resultado.ToString() : null;
                        pruR.Resultado8 = tiempos.Count > 7 ? tiempos[7].Resultado.ToString() : null;
                        pruR.Resultado9 = tiempos.Count > 8 ? tiempos[8].Resultado.ToString() : null;
                        pruR.Resultado10 = tiempos.Count > 9 ? tiempos[9].Resultado.ToString() : null;
                        pruR.Resultado11 = tiempos.Count > 10 ? tiempos[10].Resultado.ToString() : null;
                        pruR.Resultado12 = tiempos.Count > 11 ? tiempos[11].Resultado.ToString() : null;
                        pruR.Resultado13 = tiempos.Count > 12 ? tiempos[12].Resultado.ToString() : null;
                        pruR.Resultado14 = tiempos.Count > 13 ? tiempos[13].Resultado.ToString() : null;
                        pruR.Resultado15 = tiempos.Count > 14 ? tiempos[14].Resultado.ToString() : null;
                        pruR.Resultado16 = tiempos.Count > 15 ? tiempos[15].Resultado.ToString() : null;
                        pruR.Resultado17 = tiempos.Count > 16 ? tiempos[16].Resultado.ToString() : null;
                        pruR.Resultado18 = tiempos.Count > 17 ? tiempos[17].Resultado.ToString() : null;
                        pruR.Resultado19 = tiempos.Count > 18 ? tiempos[18].Resultado.ToString() : null;
                        pruR.Resultado20 = tiempos.Count > 19 ? tiempos[19].Resultado.ToString() : null;




                        pruR.VariantVelo1 = tiempos.Count > 0 ? tiempos[0].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo2 = tiempos.Count > 1 ? tiempos[1].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo3 = tiempos.Count > 2 ? tiempos[2].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo4 = tiempos.Count > 3 ? tiempos[3].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo5 = tiempos.Count > 4 ? tiempos[4].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo6 = tiempos.Count > 5 ? tiempos[5].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo7 = tiempos.Count > 6 ? tiempos[6].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo8 = tiempos.Count > 7 ? tiempos[7].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo9 = tiempos.Count > 8 ? tiempos[8].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo10 = tiempos.Count > 9 ? tiempos[9].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo11 = tiempos.Count > 10 ? tiempos[10].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo12 = tiempos.Count > 11 ? tiempos[11].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo13 = tiempos.Count > 12 ? tiempos[12].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo14 = tiempos.Count > 13 ? tiempos[13].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo15 = tiempos.Count > 14 ? tiempos[14].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo16 = tiempos.Count > 15 ? tiempos[15].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo17 = tiempos.Count > 16 ? tiempos[16].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo18 = tiempos.Count > 17 ? tiempos[17].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo19 = tiempos.Count > 18 ? tiempos[18].sentidoVelocidad.ToString() : null;
                        pruR.VariantVelo20 = tiempos.Count > 19 ? tiempos[19].sentidoVelocidad.ToString() : null;




                        pruR.DiferenciaTiempo1 = tiempos.Count > 0 ? tiempos[0].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo2 = tiempos.Count > 1 ? tiempos[1].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo3 = tiempos.Count > 2 ? tiempos[2].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo4 = tiempos.Count > 3 ? tiempos[3].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo5 = tiempos.Count > 4 ? tiempos[4].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo6 = tiempos.Count > 5 ? tiempos[5].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo7 = tiempos.Count > 6 ? tiempos[6].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo8 = tiempos.Count > 7 ? tiempos[7].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo9 = tiempos.Count > 8 ? tiempos[8].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo10 = tiempos.Count > 9 ? tiempos[9].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo11 = tiempos.Count > 10 ? tiempos[10].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo12 = tiempos.Count > 11 ? tiempos[11].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo13 = tiempos.Count > 12 ? tiempos[12].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo14 = tiempos.Count > 13 ? tiempos[13].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo15 = tiempos.Count > 14 ? tiempos[14].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo16 = tiempos.Count > 15 ? tiempos[15].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo17 = tiempos.Count > 16 ? tiempos[16].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo18 = tiempos.Count > 17 ? tiempos[17].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo19 = tiempos.Count > 18 ? tiempos[18].diferencia.ToString() : null;
                        pruR.DiferenciaTiempo20 = tiempos.Count > 19 ? tiempos[19].diferencia.ToString() : null;


                        pruR.Programa = label34.Text;

                        pruR.Media15 = label17.Text;
                        pruR.Media2 = label30.Text;
                        pruR.Media25 = label35.Text;
                        pruR.Media3 = label40.Text;

                        pruR.Mediana15 = label20.Text;
                        pruR.Mediana2 = label31.Text;
                        pruR.Mediana25 = label36.Text;
                        pruR.Mediana3 = label41.Text;




                        pruR.DesvStandar15 = label13.Text;
                        pruR.DesvStandar2 = label28.Text;
                        pruR.DesvStandar25 = label33.Text;
                        pruR.DesvStandar3 = label38.Text;

                        pruR.CoefVar15 = label11.Text;
                        pruR.CoefVar2 = label27.Text;
                        pruR.CoefVar25 = label32.Text;
                        pruR.CoefVar3 = label37.Text;



                        pruR.Media = label2.Text;
                        pruR.Mediana = label4.Text;
                        pruR.Moda = label6.Text;
                        pruR.DesvStandar = label8.Text;
                        pruR.CoefVar = label10.Text;
                        pruR.Calificacion = label22.Text;


                        entities.PruResanti.Add(pruR);
                        entities.SaveChangesAsync();

                        var ultimo = entities.Set<PruResanti>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PResanti = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruResanti.Where(f => f.idTest == sujetoEva.PResanti).FirstOrDefault<PruResanti>();

                        conect.Tiempo1 = tiempos.Count > 0 ? tiempos[0].tiempo.ToString() : null;
                        conect.Tiempo2 = tiempos.Count > 1 ? tiempos[1].tiempo.ToString() : null;
                        conect.Tiempo3 = tiempos.Count > 2 ? tiempos[2].tiempo.ToString() : null;
                        conect.Tiempo4 = tiempos.Count > 3 ? tiempos[3].tiempo.ToString() : null;
                        conect.Tiempo5 = tiempos.Count > 4 ? tiempos[4].tiempo.ToString() : null;
                        conect.Tiempo6 = tiempos.Count > 5 ? tiempos[5].tiempo.ToString() : null;
                        conect.Tiempo7 = tiempos.Count > 6 ? tiempos[6].tiempo.ToString() : null;
                        conect.Tiempo8 = tiempos.Count > 7 ? tiempos[7].tiempo.ToString() : null;
                        conect.Tiempo9 = tiempos.Count > 8 ? tiempos[8].tiempo.ToString() : null;
                        conect.Tiempo10 = tiempos.Count > 9 ? tiempos[9].tiempo.ToString() : null;
                        conect.Tiempo11 = tiempos.Count > 10 ? tiempos[10].tiempo.ToString() : null;
                        conect.Tiempo12 = tiempos.Count > 11 ? tiempos[11].tiempo.ToString() : null;
                        conect.Tiempo13 = tiempos.Count > 12 ? tiempos[12].tiempo.ToString() : null;
                        conect.Tiempo14 = tiempos.Count > 13 ? tiempos[13].tiempo.ToString() : null;
                        conect.Tiempo15 = tiempos.Count > 14 ? tiempos[14].tiempo.ToString() : null;
                        conect.Tiempo16 = tiempos.Count > 15 ? tiempos[15].tiempo.ToString() : null;
                        conect.Tiempo17 = tiempos.Count > 16 ? tiempos[16].tiempo.ToString() : null;
                        conect.Tiempo18 = tiempos.Count > 17 ? tiempos[17].tiempo.ToString() : null;
                        conect.Tiempo19 = tiempos.Count > 18 ? tiempos[18].tiempo.ToString() : null;
                        conect.Tiempo20 = tiempos.Count > 19 ? tiempos[19].tiempo.ToString() : null;



                        conect.Resultado1 = tiempos.Count > 0 ? tiempos[0].Resultado.ToString() : null;
                        conect.Resultado2 = tiempos.Count > 1 ? tiempos[1].Resultado.ToString() : null;
                        conect.Resultado3 = tiempos.Count > 2 ? tiempos[2].Resultado.ToString() : null;
                        conect.Resultado4 = tiempos.Count > 3 ? tiempos[3].Resultado.ToString() : null;
                        conect.Resultado5 = tiempos.Count > 4 ? tiempos[4].Resultado.ToString() : null;
                        conect.Resultado6 = tiempos.Count > 5 ? tiempos[5].Resultado.ToString() : null;
                        conect.Resultado7 = tiempos.Count > 6 ? tiempos[6].Resultado.ToString() : null;
                        conect.Resultado8 = tiempos.Count > 7 ? tiempos[7].Resultado.ToString() : null;
                        conect.Resultado9 = tiempos.Count > 8 ? tiempos[8].Resultado.ToString() : null;
                        conect.Resultado10 = tiempos.Count > 9 ? tiempos[9].Resultado.ToString() : null;
                        conect.Resultado11 = tiempos.Count > 10 ? tiempos[10].Resultado.ToString() : null;
                        conect.Resultado12 = tiempos.Count > 11 ? tiempos[11].Resultado.ToString() : null;
                        conect.Resultado13 = tiempos.Count > 12 ? tiempos[12].Resultado.ToString() : null;
                        conect.Resultado14 = tiempos.Count > 13 ? tiempos[13].Resultado.ToString() : null;
                        conect.Resultado15 = tiempos.Count > 14 ? tiempos[14].Resultado.ToString() : null;
                        conect.Resultado16 = tiempos.Count > 15 ? tiempos[15].Resultado.ToString() : null;
                        conect.Resultado17 = tiempos.Count > 16 ? tiempos[16].Resultado.ToString() : null;
                        conect.Resultado18 = tiempos.Count > 17 ? tiempos[17].Resultado.ToString() : null;
                        conect.Resultado19 = tiempos.Count > 18 ? tiempos[18].Resultado.ToString() : null;
                        conect.Resultado20 = tiempos.Count > 19 ? tiempos[19].Resultado.ToString() : null;





                        conect.VariantVelo1 = tiempos.Count > 0 ? tiempos[0].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo2 = tiempos.Count > 1 ? tiempos[1].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo3 = tiempos.Count > 2 ? tiempos[2].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo4 = tiempos.Count > 3 ? tiempos[3].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo5 = tiempos.Count > 4 ? tiempos[4].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo6 = tiempos.Count > 5 ? tiempos[5].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo7 = tiempos.Count > 6 ? tiempos[6].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo8 = tiempos.Count > 7 ? tiempos[7].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo9 = tiempos.Count > 8 ? tiempos[8].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo10 = tiempos.Count > 9 ? tiempos[9].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo11 = tiempos.Count > 10 ? tiempos[10].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo12 = tiempos.Count > 11 ? tiempos[11].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo13 = tiempos.Count > 12 ? tiempos[12].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo14 = tiempos.Count > 13 ? tiempos[13].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo15 = tiempos.Count > 14 ? tiempos[14].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo16 = tiempos.Count > 15 ? tiempos[15].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo17 = tiempos.Count > 16 ? tiempos[16].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo18 = tiempos.Count > 17 ? tiempos[17].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo19 = tiempos.Count > 18 ? tiempos[18].sentidoVelocidad.ToString() : null;
                        conect.VariantVelo20 = tiempos.Count > 19 ? tiempos[19].sentidoVelocidad.ToString() : null;




                        conect.DiferenciaTiempo1 = tiempos.Count > 0 ? tiempos[0].diferencia.ToString() : null;
                        conect.DiferenciaTiempo2 = tiempos.Count > 1 ? tiempos[1].diferencia.ToString() : null;
                        conect.DiferenciaTiempo3 = tiempos.Count > 2 ? tiempos[2].diferencia.ToString() : null;
                        conect.DiferenciaTiempo4 = tiempos.Count > 3 ? tiempos[3].diferencia.ToString() : null;
                        conect.DiferenciaTiempo5 = tiempos.Count > 4 ? tiempos[4].diferencia.ToString() : null;
                        conect.DiferenciaTiempo6 = tiempos.Count > 5 ? tiempos[5].diferencia.ToString() : null;
                        conect.DiferenciaTiempo7 = tiempos.Count > 6 ? tiempos[6].diferencia.ToString() : null;
                        conect.DiferenciaTiempo8 = tiempos.Count > 7 ? tiempos[7].diferencia.ToString() : null;
                        conect.DiferenciaTiempo9 = tiempos.Count > 8 ? tiempos[8].diferencia.ToString() : null;
                        conect.DiferenciaTiempo10 = tiempos.Count > 9 ? tiempos[9].diferencia.ToString() : null;
                        conect.DiferenciaTiempo11 = tiempos.Count > 10 ? tiempos[10].diferencia.ToString() : null;
                        conect.DiferenciaTiempo12 = tiempos.Count > 11 ? tiempos[11].diferencia.ToString() : null;
                        conect.DiferenciaTiempo13 = tiempos.Count > 12 ? tiempos[12].diferencia.ToString() : null;
                        conect.DiferenciaTiempo14 = tiempos.Count > 13 ? tiempos[13].diferencia.ToString() : null;
                        conect.DiferenciaTiempo15 = tiempos.Count > 14 ? tiempos[14].diferencia.ToString() : null;
                        conect.DiferenciaTiempo16 = tiempos.Count > 15 ? tiempos[15].diferencia.ToString() : null;
                        conect.DiferenciaTiempo17 = tiempos.Count > 16 ? tiempos[16].diferencia.ToString() : null;
                        conect.DiferenciaTiempo18 = tiempos.Count > 17 ? tiempos[17].diferencia.ToString() : null;
                        conect.DiferenciaTiempo19 = tiempos.Count > 18 ? tiempos[18].diferencia.ToString() : null;
                        conect.DiferenciaTiempo20 = tiempos.Count > 19 ? tiempos[19].diferencia.ToString() : null;




                        conect.Programa = label34.Text;
                        conect.Media15 = label17.Text;
                        conect.Media2 = label30.Text;
                        conect.Media25 = label35.Text;
                        conect.Media3 = label40.Text;

                        conect.Mediana15 = label20.Text;
                        conect.Mediana2 = label31.Text;
                        conect.Mediana25 = label36.Text;
                        conect.Mediana3 = label41.Text;


                        conect.DesvStandar15 = label13.Text;
                        conect.DesvStandar2 = label28.Text;
                        conect.DesvStandar25 = label33.Text;
                        conect.DesvStandar3 = label38.Text;

                        conect.CoefVar15 = label11.Text;
                        conect.CoefVar2 = label27.Text;
                        conect.CoefVar25 = label32.Text;
                        conect.CoefVar3 = label37.Text;



                        conect.Media = label2.Text;
                        conect.Mediana = label4.Text;
                        conect.Moda = label6.Text;
                        conect.DesvStandar = label8.Text;
                        conect.CoefVar = label10.Text;
                        conect.Calificacion = label22.Text;






                        entities.SaveChangesAsync();

                    }
                }
            }



        }



        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Desea eliminar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (mainEntities db = new mainEntities())
                {
                    int id = Convert.ToInt32(idTodo);
                    SujetosEvaluados res = db.SujetosEvaluados.Where(s => s.idSujetoEvaluado == id).FirstOrDefault<SujetosEvaluados>();


                    res.PResanti = null;



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

                        using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruResanti where idTest='" + idTestPrueba + "'", c))
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

                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();

                String ci = res.NCarnetIdent;
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\RA.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = label25.Text + " Tiempo de Respuesta Anticipada";

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
                            rng1.Text = " " + resp.Fecha;

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



                            Bookmark ptoA = wordDoc.Bookmarks["A"];
                            Range A = ptoA.Range;
                            A.Text = " " + resp.Media;


                            Bookmark ptoB = wordDoc.Bookmarks["B"];
                            Range B = ptoB.Range;
                            B.Text = " " + resp.Mediana;


                            Bookmark ptoC = wordDoc.Bookmarks["C"];
                            Range C = ptoC.Range;
                            C.Text = " " + resp.Moda;


                            Bookmark ptoD = wordDoc.Bookmarks["D"];
                            Range D = ptoD.Range;
                            D.Text = " " + resp.DesvStandar;


                            Bookmark ptoE = wordDoc.Bookmarks["E"];
                            Range E = ptoE.Range;
                            E.Text = " " + resp.CoefVar;


                            Bookmark ptoTotal = wordDoc.Bookmarks["F"];
                            Range pt = ptoTotal.Range;
                            pt.Text = " " + resp.Calificacion;


                            Bookmark ptoPor = wordDoc.Bookmarks["G"];
                            Range porc = ptoPor.Range;
                            porc.Text = " " + resp.Programa;


                            Bookmark ptoOM = wordDoc.Bookmarks["OO"];
                            Range porOm = ptoOM.Range;
                            porOm.Text = " " + omisiones;



                            //-----------------------------------------///


                            Bookmark ptoRa = wordDoc.Bookmarks["M1"];
                            Range porR = ptoRa.Range;
                            porR.Text = " " + resp.Media15;



                            Bookmark ptoDi = wordDoc.Bookmarks["M2"];
                            Range porD = ptoDi.Range;
                            porD.Text = " " + resp.Media2;



                            Bookmark ptoDiw = wordDoc.Bookmarks["M3"];
                            Range porDw = ptoDiw.Range;
                            porDw.Text = " " + resp.Media25;


                            Bookmark ptoDiw2 = wordDoc.Bookmarks["M4"];
                            Range porDw2 = ptoDiw2.Range;
                            porDw2.Text = " " + resp.Media3;





                            Bookmark ptoRa1 = wordDoc.Bookmarks["MA1"];
                            Range porR1 = ptoRa1.Range;
                            porR1.Text = " " + resp.Mediana15;



                            Bookmark ptoDi1 = wordDoc.Bookmarks["MA2"];
                            Range porD1 = ptoDi1.Range;
                            porD1.Text = " " + resp.Mediana2;



                            Bookmark ptoDiw1 = wordDoc.Bookmarks["MA3"];
                            Range porDw1 = ptoDiw1.Range;
                            porDw1.Text = " " + resp.Mediana25;


                            Bookmark ptoDiw21 = wordDoc.Bookmarks["MA4"];
                            Range porDw21 = ptoDiw21.Range;
                            porDw21.Text = " " + resp.Mediana3;





                            Bookmark ptoRa11 = wordDoc.Bookmarks["DE1"];
                            Range porR11 = ptoRa11.Range;
                            porR11.Text = " " + resp.DesvStandar15;



                            Bookmark ptoDi11 = wordDoc.Bookmarks["DE2"];
                            Range porD11 = ptoDi11.Range;
                            porD11.Text = " " + resp.DesvStandar2;



                            Bookmark ptoDiw11 = wordDoc.Bookmarks["DE3"];
                            Range porDw11 = ptoDiw11.Range;
                            porDw11.Text = " " + resp.DesvStandar25;


                            Bookmark ptoDiw211 = wordDoc.Bookmarks["DE4"];
                            Range porDw211 = ptoDiw211.Range;
                            porDw211.Text = " " + resp.DesvStandar3;







                            Bookmark ptoRa111 = wordDoc.Bookmarks["CV1"];
                            Range porR111 = ptoRa111.Range;
                            porR111.Text = " " + resp.CoefVar15;



                            Bookmark ptoDi111 = wordDoc.Bookmarks["CV2"];
                            Range porD111 = ptoDi111.Range;
                            porD111.Text = " " + resp.CoefVar2;



                            Bookmark ptoDiw111 = wordDoc.Bookmarks["CV3"];
                            Range porDw111 = ptoDiw111.Range;
                            porDw111.Text = " " + resp.CoefVar25;


                            Bookmark ptoDiw2111 = wordDoc.Bookmarks["CV4"];
                            Range porDw2111 = ptoDiw2111.Range;
                            porDw2111.Text = " " + resp.CoefVar3;



                            {
                                //Tiempos
                                if (0 < tiempos.Count)
                                {
                                    Bookmark t1 = wordDoc.Bookmarks["T1"];
                                    Range t1r = t1.Range;
                                    t1r.Text = " " + tiempos[0].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V1"];
                                    Range t1rv = v.Range;
                                    t1rv.Text = " " + tiempos[0].sentidoVelocidad;



                                    Bookmark v1 = wordDoc.Bookmarks["R1"];
                                    Range t1rv1 = v1.Range;
                                    t1rv1.Text = " " + tiempos[0].Resultado;



                                    Bookmark v11 = wordDoc.Bookmarks["D1"];
                                    Range t1rv11 = v11.Range;
                                    t1rv11.Text = " " + tiempos[0].diferencia;
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



                                    Bookmark v11 = wordDoc.Bookmarks["D1"];
                                    Range t1rv11 = v11.Range;
                                    t1rv11.Text = "";
                                }


                                if (1 < tiempos.Count)
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " " + tiempos[1].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V2"];
                                    Range t2rv = v.Range;
                                    t2rv.Text = " " + tiempos[1].sentidoVelocidad;



                                    Bookmark v2 = wordDoc.Bookmarks["R2"];
                                    Range t2rv2 = v2.Range;
                                    t2rv2.Text = " " + tiempos[1].Resultado;


                                    Bookmark v22 = wordDoc.Bookmarks["D2"];
                                    Range t2rv22 = v22.Range;
                                    t2rv22.Text = " " + tiempos[1].diferencia;
                                }
                                else
                                {
                                    Bookmark t2 = wordDoc.Bookmarks["T2"];
                                    Range t2r = t2.Range;
                                    t2r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V2"];
                                    Range t2rv = v.Range;
                                    t2rv.Text = " ";



                                    Bookmark v2 = wordDoc.Bookmarks["R2"];
                                    Range t2rv2 = v2.Range;
                                    t2rv2.Text = " ";



                                    Bookmark v22 = wordDoc.Bookmarks["D2"];
                                    Range t2rv22 = v22.Range;
                                    t2rv22.Text = " ";
                                }


                                if (2 < tiempos.Count)
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " " + tiempos[2].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V3"];
                                    Range t3rv = v.Range;
                                    t3rv.Text = " " + tiempos[2].sentidoVelocidad;



                                    Bookmark v3 = wordDoc.Bookmarks["R3"];
                                    Range t3rv3 = v3.Range;
                                    t3rv3.Text = " " + tiempos[2].Resultado;



                                    Bookmark v33 = wordDoc.Bookmarks["D3"];
                                    Range t3rv33 = v33.Range;
                                    t3rv33.Text = " " + tiempos[2].diferencia;
                                }
                                else
                                {
                                    Bookmark t3 = wordDoc.Bookmarks["T3"];
                                    Range t3r = t3.Range;
                                    t3r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V3"];
                                    Range t3rv = v.Range;
                                    t3rv.Text = " ";



                                    Bookmark v3 = wordDoc.Bookmarks["R3"];
                                    Range t3rv3 = v3.Range;
                                    t3rv3.Text = " ";



                                    Bookmark v33 = wordDoc.Bookmarks["D3"];
                                    Range t3rv33 = v33.Range;
                                    t3rv33.Text = " ";
                                }


                                if (3 < tiempos.Count)
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " " + tiempos[3].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V4"];
                                    Range t4rv = v.Range;
                                    t4rv.Text = " " + tiempos[3].sentidoVelocidad;



                                    Bookmark v4 = wordDoc.Bookmarks["R4"];
                                    Range t4rv4 = v4.Range;
                                    t4rv4.Text = " " + tiempos[3].Resultado;



                                    Bookmark v44 = wordDoc.Bookmarks["D4"];
                                    Range t4rv44 = v44.Range;
                                    t4rv44.Text = " " + tiempos[3].diferencia;
                                }
                                else
                                {
                                    Bookmark t4 = wordDoc.Bookmarks["T4"];
                                    Range t4r = t4.Range;
                                    t4r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V4"];
                                    Range t4rv = v.Range;
                                    t4rv.Text = " ";



                                    Bookmark v4 = wordDoc.Bookmarks["R4"];
                                    Range t4rv4 = v4.Range;
                                    t4rv4.Text = " ";



                                    Bookmark v44 = wordDoc.Bookmarks["D4"];
                                    Range t4rv44 = v44.Range;
                                    t4rv44.Text = " ";
                                }



                                if (4 < tiempos.Count)
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " " + tiempos[4].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V5"];
                                    Range t5rv = v.Range;
                                    t5rv.Text = " " + tiempos[4].sentidoVelocidad;



                                    Bookmark v5 = wordDoc.Bookmarks["R5"];
                                    Range t5rv5 = v5.Range;
                                    t5rv5.Text = " " + tiempos[4].Resultado;



                                    Bookmark v55 = wordDoc.Bookmarks["D5"];
                                    Range t5rv55 = v55.Range;
                                    t5rv55.Text = " " + tiempos[4].diferencia;
                                }
                                else
                                {
                                    Bookmark t5 = wordDoc.Bookmarks["T5"];
                                    Range t5r = t5.Range;
                                    t5r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V5"];
                                    Range t5rv = v.Range;
                                    t5rv.Text = " ";



                                    Bookmark v5 = wordDoc.Bookmarks["R5"];
                                    Range t5rv5 = v5.Range;
                                    t5rv5.Text = " ";



                                    Bookmark v55 = wordDoc.Bookmarks["D5"];
                                    Range t5rv55 = v55.Range;
                                    t5rv55.Text = " ";
                                }



                                if (5 < tiempos.Count)
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " " + tiempos[5].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V6"];
                                    Range t6rv = v.Range;
                                    t6rv.Text = " " + tiempos[5].sentidoVelocidad;



                                    Bookmark v6 = wordDoc.Bookmarks["R6"];
                                    Range t6rv6 = v6.Range;
                                    t6rv6.Text = " " + tiempos[5].Resultado;



                                    Bookmark v66 = wordDoc.Bookmarks["D6"];
                                    Range t6rv66 = v66.Range;
                                    t6rv66.Text = " " + tiempos[5].diferencia;
                                }
                                else
                                {
                                    Bookmark t6 = wordDoc.Bookmarks["T6"];
                                    Range t6r = t6.Range;
                                    t6r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V6"];
                                    Range t6rv = v.Range;
                                    t6rv.Text = " ";



                                    Bookmark v6 = wordDoc.Bookmarks["R6"];
                                    Range t6rv6 = v6.Range;
                                    t6rv6.Text = " ";



                                    Bookmark v66 = wordDoc.Bookmarks["D6"];
                                    Range t6rv66 = v66.Range;
                                    t6rv66.Text = " ";
                                }


                                if (6 < tiempos.Count)
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " " + tiempos[6].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V7"];
                                    Range t7rv = v.Range;
                                    t7rv.Text = " " + tiempos[6].sentidoVelocidad;



                                    Bookmark v7 = wordDoc.Bookmarks["R7"];
                                    Range t7rv7 = v7.Range;
                                    t7rv7.Text = " " + tiempos[6].Resultado;



                                    Bookmark v77 = wordDoc.Bookmarks["D7"];
                                    Range t7rv77 = v77.Range;
                                    t7rv77.Text = " " + tiempos[6].diferencia;
                                }
                                else
                                {
                                    Bookmark t7 = wordDoc.Bookmarks["T7"];
                                    Range t7r = t7.Range;
                                    t7r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V7"];
                                    Range t7rv = v.Range;
                                    t7rv.Text = " ";



                                    Bookmark v7 = wordDoc.Bookmarks["R7"];
                                    Range t7rv7 = v7.Range;
                                    t7rv7.Text = " ";



                                    Bookmark v77 = wordDoc.Bookmarks["D7"];
                                    Range t7rv77 = v77.Range;
                                    t7rv77.Text = " ";
                                }



                                if (7 < tiempos.Count)
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " " + tiempos[7].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V8"];
                                    Range t8rv = v.Range;
                                    t8rv.Text = " " + tiempos[7].sentidoVelocidad;



                                    Bookmark v8 = wordDoc.Bookmarks["R8"];
                                    Range t8rv8 = v8.Range;
                                    t8rv8.Text = " " + tiempos[7].Resultado;



                                    Bookmark v88 = wordDoc.Bookmarks["D8"];
                                    Range t8rv88 = v88.Range;
                                    t8rv88.Text = " " + tiempos[7].diferencia;
                                }
                                else
                                {
                                    Bookmark t8 = wordDoc.Bookmarks["T8"];
                                    Range t8r = t8.Range;
                                    t8r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V8"];
                                    Range t8rv = v.Range;
                                    t8rv.Text = " ";



                                    Bookmark v8 = wordDoc.Bookmarks["R8"];
                                    Range t8rv8 = v8.Range;
                                    t8rv8.Text = " ";



                                    Bookmark v88 = wordDoc.Bookmarks["D8"];
                                    Range t8rv88 = v88.Range;
                                    t8rv88.Text = " ";
                                }





                                if (8 < tiempos.Count)
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " " + tiempos[8].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V9"];
                                    Range t9rv = v.Range;
                                    t9rv.Text = " " + tiempos[8].sentidoVelocidad;



                                    Bookmark v9 = wordDoc.Bookmarks["R9"];
                                    Range t9rv9 = v9.Range;
                                    t9rv9.Text = " " + tiempos[8].Resultado;



                                    Bookmark v99 = wordDoc.Bookmarks["D9"];
                                    Range t9rv99 = v99.Range;
                                    t9rv99.Text = " " + tiempos[8].diferencia;
                                }
                                else
                                {
                                    Bookmark t9 = wordDoc.Bookmarks["T9"];
                                    Range t9r = t9.Range;
                                    t9r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V9"];
                                    Range t9rv = v.Range;
                                    t9rv.Text = " ";



                                    Bookmark v9 = wordDoc.Bookmarks["R9"];
                                    Range t9rv9 = v9.Range;
                                    t9rv9.Text = " ";



                                    Bookmark v99 = wordDoc.Bookmarks["D9"];
                                    Range t9rv99 = v99.Range;
                                    t9rv99.Text = " ";
                                }


                                if (9 < tiempos.Count)
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " " + tiempos[9].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V10"];
                                    Range t10rv = v.Range;
                                    t10rv.Text = " " + tiempos[9].sentidoVelocidad;



                                    Bookmark v10 = wordDoc.Bookmarks["R10"];
                                    Range t10rv10 = v10.Range;
                                    t10rv10.Text = " " + tiempos[9].Resultado;



                                    Bookmark v1010 = wordDoc.Bookmarks["D10"];
                                    Range t10rv1010 = v1010.Range;
                                    t10rv1010.Text = " " + tiempos[9].diferencia;
                                }
                                else
                                {
                                    Bookmark t10 = wordDoc.Bookmarks["T10"];
                                    Range t10r = t10.Range;
                                    t10r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V10"];
                                    Range t10rv = v.Range;
                                    t10rv.Text = " ";



                                    Bookmark v10 = wordDoc.Bookmarks["R10"];
                                    Range t10rv10 = v10.Range;
                                    t10rv10.Text = " ";



                                    Bookmark v1010 = wordDoc.Bookmarks["D10"];
                                    Range t10rv1010 = v1010.Range;
                                    t10rv1010.Text = " ";
                                }



                                if (10 < tiempos.Count)
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " " + tiempos[10].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V11"];
                                    Range t11rv = v.Range;
                                    t11rv.Text = " " + tiempos[10].sentidoVelocidad;



                                    Bookmark v11 = wordDoc.Bookmarks["R11"];
                                    Range t11rv11 = v11.Range;
                                    t11rv11.Text = " " + tiempos[10].Resultado;



                                    Bookmark v1111 = wordDoc.Bookmarks["D11"];
                                    Range t11rv1111 = v1111.Range;
                                    t11rv1111.Text = " " + tiempos[10].diferencia;
                                }
                                else
                                {
                                    Bookmark t11 = wordDoc.Bookmarks["T11"];
                                    Range t11r = t11.Range;
                                    t11r.Text = " ";


                                    Bookmark v = wordDoc.Bookmarks["V11"];
                                    Range t11rv = v.Range;
                                    t11rv.Text = " ";



                                    Bookmark v11 = wordDoc.Bookmarks["R11"];
                                    Range t11rv11 = v11.Range;
                                    t11rv11.Text = " ";



                                    Bookmark v1111 = wordDoc.Bookmarks["D11"];
                                    Range t11rv1111 = v1111.Range;
                                    t11rv1111.Text = " ";
                                }


                                if (11 < tiempos.Count)
                                {
                                    Bookmark t12 = wordDoc.Bookmarks["T12"];
                                    Range t12r = t12.Range;
                                    t12r.Text = " " + tiempos[11].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V12"];
                                    Range t12rv = v.Range;
                                    t12rv.Text = " " + tiempos[11].sentidoVelocidad;



                                    Bookmark v12 = wordDoc.Bookmarks["R12"];
                                    Range t12rv12 = v12.Range;
                                    t12rv12.Text = " " + tiempos[11].Resultado;



                                    Bookmark v1212 = wordDoc.Bookmarks["D12"];
                                    Range t12rv1212 = v1212.Range;
                                    t12rv1212.Text = " " + tiempos[11].diferencia;
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



                                    Bookmark v1212 = wordDoc.Bookmarks["D12"];
                                    Range t12rv1212 = v1212.Range;
                                    t12rv1212.Text = " ";
                                }


                                if (12 < tiempos.Count)
                                {
                                    Bookmark t13 = wordDoc.Bookmarks["T13"];
                                    Range t13r = t13.Range;
                                    t13r.Text = " " + tiempos[12].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V13"];
                                    Range t13rv = v.Range;
                                    t13rv.Text = " " + tiempos[12].sentidoVelocidad;



                                    Bookmark v13 = wordDoc.Bookmarks["R13"];
                                    Range t13rv13 = v13.Range;
                                    t13rv13.Text = " " + tiempos[12].Resultado;



                                    Bookmark v1313 = wordDoc.Bookmarks["D13"];
                                    Range t13rv1313 = v1313.Range;
                                    t13rv1313.Text = " " + tiempos[12].diferencia;
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



                                    Bookmark v1313 = wordDoc.Bookmarks["D13"];
                                    Range t13rv1313 = v1313.Range;
                                    t13rv1313.Text = " ";
                                }


                                if (13 < tiempos.Count)
                                {
                                    Bookmark t14 = wordDoc.Bookmarks["T14"];
                                    Range t14r = t14.Range;
                                    t14r.Text = " " + tiempos[13].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V14"];
                                    Range t14rv = v.Range;
                                    t14rv.Text = " " + tiempos[13].sentidoVelocidad;



                                    Bookmark v14 = wordDoc.Bookmarks["R14"];
                                    Range t14rv14 = v14.Range;
                                    t14rv14.Text = " " + tiempos[13].Resultado;



                                    Bookmark v1414 = wordDoc.Bookmarks["D14"];
                                    Range t14rv1414 = v1414.Range;
                                    t14rv1414.Text = " " + tiempos[13].diferencia;
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



                                    Bookmark v1414 = wordDoc.Bookmarks["D14"];
                                    Range t14rv1414 = v1414.Range;
                                    t14rv1414.Text = " ";
                                }


                                if (14 < tiempos.Count)
                                {
                                    Bookmark t15 = wordDoc.Bookmarks["T15"];
                                    Range t15r = t15.Range;
                                    t15r.Text = " " + tiempos[14].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V15"];
                                    Range t15rv = v.Range;
                                    t15rv.Text = " " + tiempos[14].sentidoVelocidad;



                                    Bookmark v15 = wordDoc.Bookmarks["R15"];
                                    Range t15rv15 = v15.Range;
                                    t15rv15.Text = " " + tiempos[14].Resultado;



                                    Bookmark v1515 = wordDoc.Bookmarks["D15"];
                                    Range t15rv1515 = v1515.Range;
                                    t15rv1515.Text = " " + tiempos[14].diferencia;
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



                                    Bookmark v1515 = wordDoc.Bookmarks["D15"];
                                    Range t15rv1515 = v1515.Range;
                                    t15rv1515.Text = " ";
                                }


                                if (15 < tiempos.Count)
                                {
                                    Bookmark t16 = wordDoc.Bookmarks["T16"];
                                    Range t16r = t16.Range;
                                    t16r.Text = " " + tiempos[15].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V16"];
                                    Range t16rv = v.Range;
                                    t16rv.Text = " " + tiempos[15].sentidoVelocidad;



                                    Bookmark v16 = wordDoc.Bookmarks["R16"];
                                    Range t16rv16 = v16.Range;
                                    t16rv16.Text = " " + tiempos[15].Resultado;



                                    Bookmark v1616 = wordDoc.Bookmarks["D16"];
                                    Range t16rv1616 = v1616.Range;
                                    t16rv1616.Text = " " + tiempos[15].diferencia;
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



                                    Bookmark v1616 = wordDoc.Bookmarks["D16"];
                                    Range t16rv1616 = v1616.Range;
                                    t16rv1616.Text = " ";
                                }


                                if (16 < tiempos.Count)
                                {
                                    Bookmark t17 = wordDoc.Bookmarks["T17"];
                                    Range t17r = t17.Range;
                                    t17r.Text = " " + tiempos[16].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V17"];
                                    Range t17rv = v.Range;
                                    t17rv.Text = " " + tiempos[16].sentidoVelocidad;



                                    Bookmark v17 = wordDoc.Bookmarks["R17"];
                                    Range t17rv17 = v17.Range;
                                    t17rv17.Text = " " + tiempos[16].Resultado;



                                    Bookmark v1717 = wordDoc.Bookmarks["D17"];
                                    Range t17rv1717 = v1717.Range;
                                    t17rv1717.Text = " " + tiempos[16].diferencia;
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



                                    Bookmark v1717 = wordDoc.Bookmarks["D17"];
                                    Range t17rv1717 = v1717.Range;
                                    t17rv1717.Text = " ";
                                }

                                if (17 < tiempos.Count)
                                {
                                    Bookmark t18 = wordDoc.Bookmarks["T18"];
                                    Range t18r = t18.Range;
                                    t18r.Text = " " + tiempos[17].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V18"];
                                    Range t18rv = v.Range;
                                    t18rv.Text = " " + tiempos[17].sentidoVelocidad;



                                    Bookmark v18 = wordDoc.Bookmarks["R18"];
                                    Range t18rv18 = v18.Range;
                                    t18rv18.Text = " " + tiempos[17].Resultado;



                                    Bookmark v1818 = wordDoc.Bookmarks["D18"];
                                    Range t18rv1818 = v1818.Range;
                                    t18rv1818.Text = " " + tiempos[17].diferencia;
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



                                    Bookmark v1818 = wordDoc.Bookmarks["D18"];
                                    Range t18rv1818 = v1818.Range;
                                    t18rv1818.Text = " ";
                                }


                                if (18 < tiempos.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T19"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + tiempos[18].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V19"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + tiempos[18].sentidoVelocidad;



                                    Bookmark v19 = wordDoc.Bookmarks["R19"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + tiempos[18].Resultado;



                                    Bookmark v1919 = wordDoc.Bookmarks["D19"];
                                    Range t19rv1919 = v1919.Range;
                                    t19rv1919.Text = " " + tiempos[18].diferencia;
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



                                    Bookmark v1919 = wordDoc.Bookmarks["D19"];
                                    Range t19rv1919 = v1919.Range;
                                    t19rv1919.Text = " ";
                                }


                                if (19 < tiempos.Count)
                                {
                                    Bookmark t19 = wordDoc.Bookmarks["T20"];
                                    Range t19r = t19.Range;
                                    t19r.Text = " " + tiempos[19].tiempo;


                                    Bookmark v = wordDoc.Bookmarks["V20"];
                                    Range t19rv = v.Range;
                                    t19rv.Text = " " + tiempos[19].sentidoVelocidad;



                                    Bookmark v19 = wordDoc.Bookmarks["R20"];
                                    Range t19rv19 = v19.Range;
                                    t19rv19.Text = " " + tiempos[19].Resultado;



                                    Bookmark v1919 = wordDoc.Bookmarks["D20"];
                                    Range t19rv1919 = v1919.Range;
                                    t19rv1919.Text = " " + tiempos[19].diferencia;
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



                                    Bookmark v1919 = wordDoc.Bookmarks["D20"];
                                    Range t19rv1919 = v1919.Range;
                                    t19rv1919.Text = " ";
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

        private void label39_Click(object sender, EventArgs e)
        {

        }
    }
}
