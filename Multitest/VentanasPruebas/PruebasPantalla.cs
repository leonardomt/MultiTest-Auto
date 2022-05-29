
using Multitest.ADOmodel;
using Multitest.AuxClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Multitest
{
    public partial class PruebasPantalla : Form
    {
        String prueba = null;
        String letraList = null;
        int countImage = 0;
        List<String> listImage;
        List<String> listClave;
        int checkMode = 0;
        String idPerson;
        String etapa;
        Stopwatch time = null;
        bool resCiclo = true;
        int valorUPPress = -1;
        int valorDownPress = -1;
        int flatUpDown = 0;
        List<String> list = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "W" });
        // List<String> list = new List<string>(new string[] { "90", "80", "75", "70", "60", "50", "40", "30", "25", "20", "10", "5" });

        List<String> percentilWeil = new List<string>(new string[] { "90", "80", "75", "70", "60", "50", "40", "30", "25", "20", "10", "5" });
        List<int> listWeilllave = new List<int>(new int[] { 2, 8, 7, 4, 5, 6, 8, 4, 4, 7, 3, 2, });
        List<String> listKeyRaven = new List<string>(new string[] { "A4", "A5", "A1", "A2", "A6", "A3", "A6", "A2", "A1", "A3", "A4", "A5", "B2", "B6", "B1", "B2", "B1", "B3", "B5", "B6", "B4", "B3", "B4", "B5", "C8", "C2", "C3", "C8", "C7", "C4", "C5", "C1", "C7", "C6", "C1", "C2", "D3", "D4", "D3", "D7", "D8", "D6", "D5", "D4", "D1", "D2", "D5", "D6", "E7", "E6", "E8", "E2", "E1", "E5", "E1", "E6", "E3", "E2", "E4", "E5" });
        List<AuxTest> listrResult = new List<AuxTest>();
        List<String> listConsistencia = new List<string>(new string[] { "15,8,4,2,1,0", "16,8,4,3,1,0", "17,8,5,3,1,0", "18,8,5,3,2,0", "19,8,6,3,2,0", "20,8,6,3,2,1", "21,8,6,4,2,1", "22,9,6,4,2,1", "23,9,7,4,2,1", "24,9,7,4,3,1", "25,10,7,4,3,1", "26,10,7,5,3,1", "27,10,7,5,4,1", "28,10,7,6,4,1", "29,10,7,6,5,1", "30,10,7,6,5,2", "31,10,7,7,5,2", "32,10,8,7,5,2", "33,11,8,7,5,2", "34,11,8,7,6,2", "35,11,8,7,7,2", "36,11,8,8,7,2", "37,11,9,8,7,2", "38,11,9,8,8,2", "39,11,9,8,8,3", "40,11,10,8,8,3", "41,11,10,9,8,3", "42,11,10,9,9,3", "43,12,10,9,9,3", "44,12,10,9,9,4", "45,12,10,9,9,5", "46,12,10,10,9,5", "47,12,10,10,9,6", "48,12,11,10,9,6", "49,12,11,10,10,6", "50,12,11,10,10,7", "51,12,11,11,10,7", "52,12,11,11,10,8", "53,12,11,11,11,8", "54,12,12,11,11,8", "55,12,12,11,11,9", "56,12,12,12,11,9", "57,12,12,12,11,10", "58,12,12,12,12,10", "59,12,12,12,12,11" });
        List<String> listKeyDomino = new List<string>(new string[] { "2,4", "6,1", "3,5", "0,2", "4,1", "3,6", "5,2", "0,4", "2,6", "3,3", "4,0", "5,1", "2,3", "5,6", "1,4", "2,5", "6,0", "1,4", "2,2", "1,5", "0,4", "6,3", "2,1", "3,5", "6,4", "3,0", "1,5", "2,4", "5,5", "3,6", "5,6", "4,0", "4,4", "1,0", "6,2", "3,5", "6,0", "4,6", "3,5", "2,1", "5,1", "0,6", "4,6", "3,0", "2,5", "5,6", "2,2", "1,3" });





        int Edad;
        //  int percentil = 0;
        string rango = "";
        String tiempoTotalTest = "";
        int edad = 0;
        public PruebasPantalla(String test, bool updateTest, String id, String etapa, int edad)
        {
            this.idPerson = id;
            this.Edad = edad;
            this.edad = edad;
            this.etapa = etapa;
            InitializeComponent();

            PruebasPantalla.ActiveForm.Text = "MultiTest-" + test;
            label5.Text = "Test de " + test;
            prueba = test;
            time = new Stopwatch();
            listImage = new List<String>();
            this.button5.TabStop = false;

            if (test == "Dominó")
            {
                tableLayoutPanel29.Visible = true;
                tableLayoutPanel5.Visible = false;
                label1.Text = "Observe el grupo de fichas y calcule los puntos que correspondan a la ficha e blanco,solo pueden ser números entre 0 y 6. Los valores introducidos serán mostrados en la ficha y para ser validados debe presionar la tecla Enter.";

            }


            iniciarTest();
        }

        private void iniciarTest()
        {
            if (prueba != null)
            {
                if (prueba == "Raven")
                    iniciarListImageRaven();
                if (prueba == "Weil")
                    iniciarListImageWeil();
                if (prueba == "Dominó")
                    iniciaListDomino();

                nextImage();
            }
        }

        private void iniciaListDomino()
        {
            label1.Text = "Observe el grupo de fichas y calcule los puntos que corresponden a la ficha en blanco, solo pueden ser números entre 0 y 6.  Se le da la solución de los dos primeros ejemplos (A y B) mientras que en los otros ejemplos (C y D) ud debe dar la respuesta.";
            for (int i = 1; i <= 48; i++)
            {
                String res = "F" + i + ".BMP";
                listImage.Add(res);
            }
        }

        private void iniciarListImageRaven()
        {
            label1.Text = "La presente prueba consta de una serie de dibujos, a cada uno de ellos le falta una parte, y debe elegir la parte que le falta entre 6 u 8 alternativas posibles que aparecen debajo. Dichos dibujos se agrupan en cinco series (A, B, C, D, E) y cada serie está integrada por 12 ítems. De esta forma se completa  una serie cada vez; al concluir el ejercicio 12 de la primera serie (A), pase a la serie B, comenzando por el ítems 1; cuando complete el ítems 12 de la serie B pase a trabajar en el ítems 1 de la serie C y así sucesivamente. A medida que avanza la prueba, las tareas aumentan en complejidad. Solo existe una respuesta correcta. " +
                           "Los tres primeros ejercicios se realizarán conjuntamente con ud, es decir le pediremos que diga cuál es el número del fragmento que falta en esas figuras.";
            for (int i = 0; i <= list.IndexOf("E"); i++)
            {
                for (int k = 1; k <= 12; k++)
                {
                    String res = list[i] + k + ".BMP";
                    listImage.Add(res);

                }

            }
        }

        private void iniciarClave()
        {
            //   throw new NotImplementedException();
        }

        private void iniciarListImageWeil()
        {
            label1.Text = "Como puede observar se presentan cuadros que tienen dibujos, en cada uno de estos cuadros se omite una parte y usted debe elegir entre las alternativas posibles que aparecen en la parte inferior del cuadro, la respuesta correcta en cada caso.";

            for (int i = 1; i <= 60; i++)
            {
                String res = "W" + i + ".BMP";
                listImage.Add(res);
            }
        }


        private void PruebasPantalla_Load(object sender, EventArgs e)
        {




        }





        private void nextImage()
        {

            if (countImage != listImage.Count)
            {
                if (checkMode == 0)
                {
                    String directory = System.IO.Directory.GetCurrentDirectory().ToString();
                    pictureBox1.Image = Image.FromFile(@"Images\" + listImage[countImage].ToString());

                    if (listImage[countImage].ToString().Length == 6)
                        label4.Text = listImage[countImage].ToString().Substring(0, 2);
                    if (listImage[countImage].ToString().Length == 7)
                        label4.Text = listImage[countImage].ToString().Substring(0, 3);


                    label3.Text = "";
                }
                else
                {
                    String directory = System.IO.Directory.GetCurrentDirectory().ToString();
                    pictureBox1.Image = Image.FromFile(@"Images\" + listrResult[countImage].figura);



                    if (prueba == "Dominó")
                    {
                        valorUPPress = Convert.ToInt32(listrResult[countImage].valorUP);
                        valorDownPress = Convert.ToInt32(listrResult[countImage].valorDown);
                        label4.Text = listrResult[countImage].figura.ToString();
                        pintarDominoUP(valorUPPress);
                        pintarDominoDown(valorDownPress);


                    }
                    else
                    {
                        if (listrResult[countImage].figura.Length == 6)
                            label4.Text = listrResult[countImage].figura.Substring(0, 2);
                        if (listrResult[countImage].ToString().Length == 7)
                            label4.Text = listrResult[countImage].figura.Substring(0, 3);
                        label3.Text = listrResult[countImage].valorUP;
                    }



                }

            }
            else
            {




                //  checkMode = 1;
                // countImage = 0;
                //  nextImage();
                //   flatUpDown = 0;
                time.Stop();
                // button1.Visible = true;

                tiempoTotalTest = time.ElapsedMilliseconds.ToString();

                if (prueba == "Raven")
                {
                    calificarTestRaven();
                }

                if (prueba == "Weil")
                {
                    calificarWeil();
                }

                if (prueba == "Dominó")
                {
                    calificarDomino();
                }


                MessageBox.Show("Prueba Terminada. Debe informar al especialista", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();

            }




        }



        private void PruebasPantalla_KeyPress(object sender, KeyPressEventArgs e)

        {


            if (prueba != null)
            {
                if (Char.IsNumber(e.KeyChar))
                {
                    int res = Convert.ToInt32(e.KeyChar.ToString());

                    if (!time.IsRunning)
                        time.Start();

                    if (prueba == "Raven")
                    {

                        if (countImage >= 24)
                        {
                            if (res > 0 && res < 9)
                            {
                                label3.Text = res.ToString();
                                e.Handled = true;
                            }
                        }
                        else

                        {
                            if (res > 0 && res < 7)
                            {
                                label3.Text = res.ToString();
                                e.Handled = true;
                            }

                        }
                    }

                    if (prueba == "Weil")
                    {
                        if (res > 0 && res < 9)
                        {
                            label3.Text = res.ToString();
                            e.Handled = true;
                        }
                    }

                    if (prueba == "Dominó")
                    {
                        if (res >= 0 && res <= 6)
                        {
                            if (flatUpDown == 0)
                            {
                                valorUPPress = res;
                                pintarDominoUP(res);
                                e.Handled = true;
                                flatUpDown = 1;



                            }
                            else
                            {
                                if (flatUpDown == 1)
                                {

                                    valorDownPress = res;
                                    pintarDominoDown(res);
                                    e.Handled = true;
                                    flatUpDown = 0;
                                }
                            }

                        }

                    }


                }


                if (label3.Text != "" || (valorUPPress != -1 && valorDownPress != -1))
                {

                    if (e.KeyChar.ToString() == "\r")
                    {
                        String valor = null;
                        String imagen = null;
                        AuxTest rev = null;

                        if (prueba == "Dominó")
                        {
                            imagen = listImage[countImage].ToString();
                            rev = new AuxTest(valorUPPress.ToString(), imagen, valorDownPress.ToString());

                            flatUpDown = 0;
                            valorUPPress = -1;
                            valorDownPress = -1;

                            valoresLimpiarDominno();
                        }
                        else
                        {

                            valor = label3.Text.ToString();
                            imagen = listImage[countImage].ToString();
                            rev = new AuxTest(valor, imagen);
                            label13.Text = "";
                        }


                        if (checkMode == 0)
                        {
                            listrResult.Add(rev);
                        }
                        else
                        {
                            listrResult[countImage].figura = rev.figura;
                            listrResult[countImage].valorUP = rev.valorUP;

                            if (prueba == "Dominó")
                            {
                                listrResult[countImage].valorDown = rev.valorDown;
                            }


                        }

                        countImage++;
                        nextImage();


                    }
                }

            }
        }

        private void pintarDominoUP(int valor)
        {
            label7.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;

            if (valor == 1)
            {
                label7.Visible = true;
            }

            if (valor == 2)
            {
                label10.Visible = true;
                label9.Visible = true;
            }
            if (valor == 3)
            {
                label10.Visible = true;
                label9.Visible = true;
                label7.Visible = true;
            }
            if (valor == 4)
            {
                label10.Visible = true;
                label9.Visible = true;
                label6.Visible = true;
                label11.Visible = true;
            }
            if (valor == 5)
            {
                label10.Visible = true;
                label9.Visible = true;
                label6.Visible = true;
                label11.Visible = true;
                label7.Visible = true;
            }
            if (valor == 6)
            {
                label10.Visible = true;
                label9.Visible = true;
                label6.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
            }




        }

        private void pintarDominoDown(int valor)
        {


            label15.Visible = false;
            label14.Visible = false;
            label17.Visible = false;
            label19.Visible = false;
            label16.Visible = false;
            label8.Visible = false;
            label18.Visible = false;

            if (valor == 1)
            {
                label19.Visible = true;
            }


            if (valor == 2)
            {
                label17.Visible = true;
                label16.Visible = true;

            }


            if (valor == 3)
            {
                label19.Visible = true;
                label17.Visible = true;
                label16.Visible = true;

            }


            if (valor == 4)
            {

                label17.Visible = true;
                label16.Visible = true;
                label18.Visible = true;
                label14.Visible = true;
            }

            if (valor == 5)
            {
                label17.Visible = true;
                label16.Visible = true;
                label18.Visible = true;
                label14.Visible = true;
                label19.Visible = true;
            }


            if (valor == 6)
            {
                label17.Visible = true;
                label16.Visible = true;
                label18.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label8.Visible = true;
            }


        }

        private void valoresLimpiarDominno()
        {
            label15.Visible = false;
            label14.Visible = false;
            label17.Visible = false;
            label19.Visible = false;
            label16.Visible = false;
            label8.Visible = false;
            label18.Visible = false;

            label7.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            label6.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (prueba == "Raven")
            {
                calificarTestRaven();
            }

            if (prueba == "Weil")
            {
                calificarWeil();
            }

            if (prueba == "Dominó")
            {
                calificarDomino();
            }

            MessageBox.Show("Datos guardados", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }







        private void calificarDomino()
        {
            int puntuacion = 0;
            int error = 0;
            String rangos = "";
            String diagnostico = "";
            int percentil = 0;

            for (int i = 0; i < listrResult.Count; i++)
            {
                int upRespuesta = 0;
                int downRespuesta = 0;
                if (listrResult[i].valorUP != null)
                    upRespuesta = Convert.ToInt32(listrResult[i].valorUP);
                if (listrResult[i].valorUP != null)
                    downRespuesta = Convert.ToInt32(listrResult[i].valorDown);

                int up = Convert.ToInt32(listKeyDomino[i].Substring(0, 1));
               
                int down = Convert.ToInt32(listKeyDomino[i].Substring(2, 1));

                if (upRespuesta == up && downRespuesta == down)
                {
                    puntuacion++;
                }

                if (upRespuesta == 0 || downRespuesta == 0)
                    error++;
            }




            if (puntuacion != 0)
            {
                DominoTest dominoTest = new DominoTest();


                int k = 0;


                while (k < dominoTest.edad.Count)
                {
                    int edadMenor = dominoTest.edad[k].edadMin;
                    int edadMayor = dominoTest.edad[k].edadMax;
                    int anterior = 1000;

                    if (Edad <= edadMayor && Edad >= edadMenor)
                    {
                        for (int j = 0; j < dominoTest.edad[k].puntos.Count; j++)
                        {
                            int valorTabla = dominoTest.edad[k].puntos[j];

                            if (puntuacion <= anterior && puntuacion >= valorTabla)
                            {
                                if (puntuacion == valorTabla)
                                {
                                    percentil = dominoTest.percentil[j];
                                }
                                else
                                {

                                    if (dominoTest.percentil[j] < 50)
                                    {
                                        
                                        percentil = dominoTest.percentil[j -1];
                                    }

                                    if (dominoTest.percentil[j] > 50)
                                    {
                                        percentil = dominoTest.percentil[j + 1];
                                    }

                                    if (dominoTest.percentil[j] == 50)
                                    {
                                        percentil = dominoTest.percentil[j];
                                    }
                                    
                                }

                                break;

                            }
                            anterior = valorTabla;
                        }

                    }

                    if (percentil != 0)
                        break;
                    k++;
                }


            }


            if (percentil == 95)
            {
                rangos = "I";
                diagnostico = "Muy Superior al Promedio";
            }
            if (percentil == 90)
            {
                rangos = "II";
                diagnostico = "Muy Superior al Promedio";
            }

            if (percentil == 75)
            {
                rangos = "III+";
                diagnostico = "Normal Alto";
            }
            if (percentil == 50)
            {
                rangos = "III";
                diagnostico = "Normal";
            }
            if (percentil == 25)
            {
                rangos = "III-";
                diagnostico = "Normal bajo";
            }
            if (percentil == 10)
            {
                rangos = "IV";
                diagnostico = "Inferior al Promedio";
            }
            if (percentil == 5)
            {
                rangos = "V";
                diagnostico = "Deficiente";
            }


            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");



            mainEntities entities = new mainEntities();


            int idUser = Convert.ToInt32(idPerson);
            int idEtapa = Convert.ToInt32(etapa);



            var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idUser).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

            if (sujetoEva == null)
            {

                PruDomino pruR = new PruDomino();
                pruR.Diagnostico = diagnostico;
                pruR.DuraPru = tiempoTotalTest;
                pruR.Puntaje = puntuacion.ToString();
                pruR.Porcentaje = percentil.ToString();
                pruR.Rango = rangos;
                pruR.Fecha = date;



                entities.PruDomino.Add(pruR);
                entities.SaveChanges();



                var ultimo = entities.Set<PruDomino>().OrderByDescending(t => t.idTest).FirstOrDefault();

                SujetosEvaluados pru = new SujetosEvaluados();

                pru.Fecha = date;
                pru.Etapa = Convert.ToInt32(etapa);
                pru.PDomino = ultimo.idTest;
                pru.Etapa = Convert.ToInt32(etapa);
                pru.idSujeto = Convert.ToInt32(idPerson);
                entities.SujetosEvaluados.Add(pru);
                entities.SaveChanges();


            }
            else
            {
                if (sujetoEva.PDomino == null)
                {
                    PruDomino pruR = new PruDomino();
                    pruR.Diagnostico = diagnostico;
                    pruR.DuraPru = tiempoTotalTest;
                    pruR.Puntaje = puntuacion.ToString();
                    pruR.Porcentaje = percentil.ToString();
                    pruR.Rango = rangos;
                    pruR.Fecha = date;

                    entities.PruDomino.Add(pruR);
                    entities.SaveChanges();

                    var ultimo = entities.Set<PruDomino>().OrderByDescending(t => t.idTest).FirstOrDefault();


                    sujetoEva.PDomino = ultimo.idTest;
                    entities.SaveChanges();

                }
                else
                {
                    var conect = entities.PruDomino.Where(f => f.idTest == sujetoEva.PDomino).FirstOrDefault<PruDomino>();

                    conect.Diagnostico = diagnostico;
                    conect.DuraPru = tiempoTotalTest;
                    conect.Puntaje = puntuacion.ToString();
                    conect.Porcentaje = percentil.ToString();
                    conect.Rango = rangos;
                    entities.SaveChanges();

                }
            }








        }

        private void calificarWeil()
        {

            String diagnostico = "";
            String rango = "";

            int puntuacion = calcularPuntuacion();
            int percentil = buscarPercentilWeil(puntuacion);



            if (percentil == 90)
            {
                diagnostico = "Superior Brillante";
                rango = "I";
            }

            if (percentil == 80)
            {
                diagnostico = "Superior Termino Medio";
                rango = "II";
            }

            if (percentil == 75)
            {
                diagnostico = "Normal Alto";
                rango = "III";
            }

            if (percentil == 70)
            {
                diagnostico = "Normal Promedio";
                rango = "IV";
            }

            if (percentil == 60)
            {
                diagnostico = "Normal Promedio";
                rango = "IV";
            }

            if (percentil == 50)
            {
                diagnostico = "Normal Promedio";
                rango = "IV";
            }

            if (percentil == 40)
            {
                diagnostico = "Normal Promedio";
                rango = "IV";
            }

            if (percentil == 30)
            {
                diagnostico = "Normal Torpe";
                rango = "V";
            }

            if (percentil == 25)
            {
                diagnostico = "Fronterizo";
                rango = "VI";
            }

            if (percentil == 20)
            {
                diagnostico = "Deficiente Ligero";
                rango = "VII";
            }

            if (percentil == 10)
            {
                diagnostico = "Deficiente Moderado";
                rango = "VIII";
            }

            if (percentil == 5)
            {
                diagnostico = "Débil mental";
                rango = "IX";
            }



            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");


            mainEntities entities = new mainEntities();
            int idUser = Convert.ToInt32(idPerson);
            int idEtapa = Convert.ToInt32(etapa);



            var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idUser).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

            if (sujetoEva == null)
            {

                PruWeil pruR = new PruWeil();
                pruR.Diagnostico = diagnostico;
                pruR.DuraPru = tiempoTotalTest;
                pruR.Fecha = date;
                pruR.Porcentaje = percentil.ToString();
                pruR.Rango = rango;
                pruR.Diagnostico = diagnostico;
                pruR.PuntajeTotal = puntuacion.ToString();

                entities.PruWeil.Add(pruR);
                entities.SaveChanges();



                var ultimo = entities.Set<PruWeil>().OrderByDescending(t => t.idTest).FirstOrDefault();

                SujetosEvaluados pru = new SujetosEvaluados();
                pru.Fecha = date;
                pru.Etapa = Convert.ToInt32(etapa);
                pru.PWeil = ultimo.idTest;
                pru.Etapa = Convert.ToInt32(etapa);
                pru.idSujeto = Convert.ToInt32(idPerson);
                entities.SujetosEvaluados.Add(pru);
                entities.SaveChanges();
            }
            else
            {
                if (sujetoEva.PWeil == null)
                {
                    PruWeil pruR = new PruWeil();
                    pruR.Diagnostico = diagnostico;
                    pruR.DuraPru = tiempoTotalTest;
                    pruR.Porcentaje = percentil.ToString();
                    pruR.Rango = rango;
                    pruR.Fecha = date;
                    pruR.Diagnostico = diagnostico;
                    pruR.DuraPru = tiempoTotalTest;
                    pruR.Porcentaje = percentil.ToString();
                    pruR.Rango = rango;
                    pruR.Diagnostico = diagnostico;
                    pruR.PuntajeTotal = puntuacion.ToString();

                    entities.PruWeil.Add(pruR);
                    entities.SaveChanges();

                    var ultimo = entities.Set<PruWeil>().OrderByDescending(t => t.idTest).FirstOrDefault();


                    sujetoEva.PWeil = ultimo.idTest;
                    entities.SaveChanges();

                }
                else
                {
                    var conect = entities.PruWeil.Where(f => f.idTest == sujetoEva.PWeil).FirstOrDefault<PruWeil>();
                    conect.Diagnostico = diagnostico;
                    conect.DuraPru = tiempoTotalTest;
                    conect.Porcentaje = percentil.ToString();
                    conect.Rango = rango;
                    conect.Diagnostico = diagnostico;
                    conect.PuntajeTotal = puntuacion.ToString();

                    entities.SaveChanges();

                }
            }



        }

        private int buscarPercentilWeil(int puntuacion)
        {
            int percentil = 0;

            List<int> column1 = new List<int>(new int[] { -1, -1, -1, -1, 23, 22, 20, 19, 18, 14, 7, 4 });
            TableWeil tab1 = new TableWeil("6", column1);

            List<int> column2 = new List<int>(new int[] { 35, 30, 29, 27, 25, 23, 21, 20, 17, 11, 8 });
            TableWeil tab2 = new TableWeil("7", column2);

            List<int> column3 = new List<int>(new int[] { 41, 36, 34, 33, 31, 28, 26, 23, 22, 20, 15, 12 });
            TableWeil tab3 = new TableWeil("8", column3);

            List<int> column4 = new List<int>(new int[] { 42, 39, 38, 37, 35, 32, 31, 30, 28, 26, 21, 16 });
            TableWeil tab4 = new TableWeil("9", column4);


            List<int> column5 = new List<int>(new int[] { 45, 42, 40, 39, 37, 33, 32, 31, 29, 28, 25, 19 });
            TableWeil tab5 = new TableWeil("10", column5);

            List<int> column6 = new List<int>(new int[] { 46, 43, 42, 41, 38, 33, 32, 31, 30, 29, 25, 21, });
            TableWeil tab6 = new TableWeil("11", column6);


            List<int> column7 = new List<int>(new int[] { 47, 43, 42, 41, 38, 34, 32, 32, 31, 30, 27, 22 });
            TableWeil tab7 = new TableWeil("12", column7);

            List<int> column8 = new List<int>(new int[] { 47, 43, 42, 41, 38, 35, 33, 33, 32, 31, 26, 22 });
            TableWeil tab8 = new TableWeil("13", column8);

            List<int> column9 = new List<int>(new int[] { 47, 43, 42, 41, 38, 35, 34, 33, 32, 31, 26, 22 });
            TableWeil tab9 = new TableWeil("14", column9);

            List<int> column10 = new List<int>(new int[] { 44, 43, 42, 41, 39, 36, 35, 34, 32, 31, 26, 23 });
            TableWeil tab10 = new TableWeil("15", column10);

            List<int> column11 = new List<int>(new int[] { 50, 46, 44, 42, 41, 36, 35, 34, 32, 26, 26, 23 });
            TableWeil tab11 = new TableWeil("16", column11);

            List<int> column12 = new List<int>(new int[] { 47, 43, 42, 40, 37, 35, 33, 30, 28, 26, 21, 16 });
            TableWeil tab12 = new TableWeil("A", column12);

            List<int> column13 = new List<int>(new int[] { 57, 56, 55, 54, 53, 51, 49, 46, 44, 42, 40, 35 });
            TableWeil tab13 = new TableWeil("B", column13);

            List<int> column14 = new List<int>(new int[] { 30, 25, 23, 21, 19, 17, 16, 13, 12, 8, 7, 5 });
            TableWeil tab14 = new TableWeil("C", column14);




            List<TableWeil> list = new List<TableWeil>();

            list.Add(tab1);
            list.Add(tab2);
            list.Add(tab3);
            list.Add(tab4);
            list.Add(tab5);
            list.Add(tab6);
            list.Add(tab7);
            list.Add(tab8);
            list.Add(tab9);
            list.Add(tab10);
            list.Add(tab11);
            list.Add(tab12);
            list.Add(tab13);
            list.Add(tab14);

            DatosSujetos sujetos;

            using (mainEntities entites = new mainEntities())
            {
                sujetos = entites.DatosSujetos.Find(Convert.ToInt32(idPerson));
            }


            bool primero = false;
             

            if (edad < 17)
            {

                for (int i = 0; i < list.Count - 3; i++)
                {

                    if (edad == Convert.ToInt32(list[i].Edad))
                    {
                        int anterior = 1000000;

                        for (int k = 0; k < list[i].column.Count; k++)
                        {
                            int valCol = list[i].column[k];



                            if (puntuacion <= anterior && puntuacion >= valCol && !primero)
                            {
                                if (puntuacion == anterior || puntuacion == valCol)
                                {
                                    if (puntuacion == anterior)
                                        percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                    else
                                        percentil = Convert.ToInt32(percentilWeil[k]);

                                }
                                else
                                {
                                    if (Convert.ToInt32(percentilWeil[k]) >= 50)
                                    {
                                        percentil = Convert.ToInt32(percentilWeil[k]);
                                    }
                                    else
                                    {
                                        percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                    }

                                }

                                primero = true;

                            }


                            anterior = valCol;
                        }


                    }

                }
            }
            else
            {
                if (sujetos.NivelEscolar == "Secundario" || sujetos.NivelEscolar == "Técnico Medio" || sujetos.NivelEscolar == "Medio Superior")
                {
                    int anterior = 1000000;

                    for (int k = 0; k < list[11].column.Count; k++)
                    {
                        int valCol = list[11].column[k];

                        if (puntuacion <= anterior && puntuacion >= valCol)
                        {
                            if (puntuacion == anterior || puntuacion == valCol)
                            {
                                if (puntuacion == anterior)
                                    percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                else
                                    percentil = Convert.ToInt32(percentilWeil[k]);

                            }
                            else
                            {
                                if (Convert.ToInt32(percentilWeil[k]) >= 50)
                                {
                                    percentil = Convert.ToInt32(percentilWeil[k]);
                                }
                                else
                                {
                                    percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                }

                            }


                        }


                        anterior = valCol;
                    }

                }

                if (sujetos.NivelEscolar == "Superior")
                {
                    int anterior = 1000000;

                    for (int k = 0; k < list[12].column.Count; k++)
                    {
                        int valCol = list[12].column[k];

                        if (puntuacion <= anterior && puntuacion >= valCol)
                        {
                            if (puntuacion == anterior || puntuacion == valCol)
                            {
                                if (puntuacion == anterior)
                                    percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                else
                                    percentil = Convert.ToInt32(percentilWeil[k]);

                            }
                            else
                            {
                                if (Convert.ToInt32(percentilWeil[k]) >= 50)
                                {
                                    percentil = Convert.ToInt32(percentilWeil[k]);
                                }
                                else
                                {
                                    percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                }

                            }


                        }


                        anterior = valCol;
                    }

                }
                if (sujetos.NivelEscolar == "Analfabeto")
                {
                    int anterior = 1000000;

                    for (int k = 0; k < list[13].column.Count; k++)
                    {
                        int valCol = list[13].column[k];

                        if (puntuacion <= anterior && puntuacion >= valCol)
                        {
                            if (puntuacion == anterior || puntuacion == valCol)
                            {
                                if (puntuacion == anterior)
                                    percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                else
                                    percentil = Convert.ToInt32(percentilWeil[k]);

                            }
                            else
                            {
                                if (Convert.ToInt32(percentilWeil[k]) >= 50)
                                {
                                    percentil = Convert.ToInt32(percentilWeil[k]);
                                }
                                else
                                {
                                    percentil = Convert.ToInt32(percentilWeil[k - 1]);
                                }

                            }


                        }


                        anterior = valCol;
                    }

                }
            }


            return percentil;

        }








        private int calcularPuntuacion()
        {
            int i = 0;
            int k = 0;
            int puntuacion = 0;

            while (i < listrResult.Count)
            {
                int temp = Convert.ToInt32(listrResult[i].valorUP);

                if (temp == listWeilllave[k])
                {
                    puntuacion++;
                }

                if (k == 11)
                    k = 0;
                else
                    k++;


                i++;

            }

            return puntuacion;
        }

        private void calificarTestRaven()
        {


            int puntajeA = 0;
            int puntajeB = 0;
            int puntajeC = 0;
            int puntajeD = 0;
            int puntajeE = 0;
            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string diagnostico = "";
            int percentil = 0;

            for (int i = 0; i < listKeyRaven.Count; i++)
            {
                String valorllave = listKeyRaven[i].Substring(1, 1);
                String valorLetra = listKeyRaven[i].Substring(0, 1);
                String valorResultado = listrResult[i].valorUP;

                if (valorllave == valorResultado)
                {
                    if (valorLetra == "A")
                        puntajeA++;

                    if (valorLetra == "B")
                        puntajeB++;

                    if (valorLetra == "C")
                        puntajeC++;

                    if (valorLetra == "D")
                        puntajeD++;

                    if (valorLetra == "E")
                        puntajeE++;
                }
            }

            int puntajeTotal = puntajeA + puntajeB + puntajeC + puntajeD + puntajeE;
            bool consistencia = medirConsistencia(puntajeTotal, puntajeA, puntajeB, puntajeC, puntajeD, puntajeE);



            RavenClass ravenTabla = new RavenClass();

            for (int i = 0; i < ravenTabla.edad.Count; i++)
            {
                int edadMini = ravenTabla.edad[i].edadMin;
                int edadMax = ravenTabla.edad[i].edadMax;

                if (Edad >= edadMini && Edad <= edadMax)
                {

                    if (puntajeTotal >= ravenTabla.edad[i].puntos[2])
                    {
                        if (puntajeTotal >= ravenTabla.edad[i].puntos[0])
                        {
                            percentil = 95;
                            rango = "I";
                            diagnostico = "Superior";
                        }

                        if (puntajeTotal >= ravenTabla.edad[i].puntos[1] && puntajeTotal < ravenTabla.edad[i].puntos[0])
                        {
                            percentil = 90;
                            rango = "II";
                            diagnostico = "Al Promedio";
                        }

                        if (puntajeTotal >= ravenTabla.edad[i].puntos[2] && puntajeTotal < ravenTabla.edad[i].puntos[1])
                        {
                            percentil = 75;
                            rango = "III+";
                            diagnostico = "Normal Alto";
                        }

                    }

                    if (puntajeTotal < ravenTabla.edad[i].puntos[2] && puntajeTotal > ravenTabla.edad[i].puntos[4])
                    {
                        percentil = 50;
                        rango = "III";
                        diagnostico = "Normal";
                    }

                    if (puntajeTotal <= ravenTabla.edad[i].puntos[4])
                    {
                        if (puntajeTotal == ravenTabla.edad[i].puntos[4])
                        {
                            percentil = 25;
                            rango = "III-";
                            diagnostico = "Normal";
                        }

                        if (puntajeTotal < ravenTabla.edad[i].puntos[4] && puntajeTotal >= ravenTabla.edad[i].puntos[5])
                        {
                            percentil = 10;
                            rango = "IV";
                            diagnostico = "Inferior";
                        }

                        if (puntajeTotal < ravenTabla.edad[i].puntos[5])
                        {
                            percentil = 5;
                            rango = "V";
                            diagnostico = "Al Promedio";
                        }

                    }

                }

            }


            //if (percentil == 95)
            //{
            //    rango = "I";
            //    diagnostico = "Inteligencia Superior";

            //}

            //if (percentil == 90)
            //{
            //    rango = "II";
            //    diagnostico = "Inteligencia Supeior al Promedio";
            //}

            //if (percentil == 75)
            //{
            //    rango = "III";
            //    diagnostico = "Inteligencia Normal Alta";

            //}

            //if (percentil == 50)
            //{
            //    rango = "IV";
            //    diagnostico = "Inteligencia Normal ";
            //}

            //if (percentil == 25)
            //{
            //    rango = "V";
            //    diagnostico = "Inteligencia Normal Baja";
            //}

            //if (percentil == 10)
            //{
            //    rango = "";
            //    diagnostico = "R.M.F";
            //}

            //if (percentil == 5)
            //{
            //    rango = "";
            //    diagnostico = "Deficiente";
            //}




            mainEntities entities = new mainEntities();
            int idUser = Convert.ToInt32(idPerson);
            int idEtapa = Convert.ToInt32(etapa);



            var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idUser).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

            if (sujetoEva == null)
            {
                PruRaven pruR = new PruRaven();
                pruR.Diagnostico = diagnostico;
                pruR.DuraPru = tiempoTotalTest;
                pruR.Porcentaje = percentil.ToString();
                pruR.Rango = rango;
                pruR.Fecha = date;
                pruR.Consistencia = consistencia == true ? "Si" : "No";
                pruR.PuntajeA = puntajeA.ToString();
                pruR.PuntajeB = puntajeB.ToString();
                pruR.PuntajeC = puntajeC.ToString();
                pruR.PuntajeD = puntajeD.ToString();
                pruR.PuntajeE = puntajeD.ToString();
                pruR.PuntajeTotal = puntajeTotal.ToString();
                entities.PruRaven.Add(pruR);
                entities.SaveChanges();
                var ultimo = entities.Set<PruRaven>().OrderByDescending(t => t.idTest).FirstOrDefault();

                SujetosEvaluados pru = new SujetosEvaluados();
                pru.Fecha = date;
                pru.Etapa = Convert.ToInt32(etapa);
                pru.PRaven = ultimo.idTest;
                pru.Etapa = Convert.ToInt32(etapa);
                pru.idSujeto = Convert.ToInt32(idPerson);
                entities.SujetosEvaluados.Add(pru);
                entities.SaveChanges();
            }
            else
            {
                if (sujetoEva.PruRaven == null)
                {
                    PruRaven pruR = new PruRaven();
                    pruR.Diagnostico = diagnostico;
                    pruR.DuraPru = tiempoTotalTest;
                    pruR.Porcentaje = percentil.ToString();
                    pruR.Rango = rango;
                    pruR.Fecha = date;
                    pruR.Consistencia = pruR.Consistencia = consistencia == true ? "Si" : "No";
                    pruR.PuntajeA = puntajeA.ToString();
                    pruR.PuntajeB = puntajeB.ToString();
                    pruR.PuntajeC = puntajeC.ToString();
                    pruR.PuntajeD = puntajeD.ToString();
                    pruR.PuntajeE = puntajeE.ToString();
                    pruR.PuntajeTotal = puntajeTotal.ToString();
                    entities.PruRaven.Add(pruR);
                    entities.SaveChanges();

                    var ultimo = entities.Set<PruRaven>().OrderByDescending(t => t.idTest).FirstOrDefault();


                    sujetoEva.PRaven = ultimo.idTest;
                    entities.SaveChanges();

                }
                else
                {
                    var conect = entities.PruRaven.Where(f => f.idTest == sujetoEva.PRaven).FirstOrDefault<PruRaven>();
                    conect.Diagnostico = diagnostico;
                    conect.DuraPru = tiempoTotalTest;
                    conect.Porcentaje = percentil.ToString();
                    conect.Rango = rango;
                    conect.Consistencia = consistencia == true ? "Si" : "No";
                    conect.PuntajeA = puntajeA.ToString();
                    conect.PuntajeB = puntajeB.ToString();
                    conect.PuntajeC = puntajeC.ToString();
                    conect.PuntajeD = puntajeD.ToString();
                    conect.PuntajeE = puntajeE.ToString();
                    conect.PuntajeTotal = puntajeTotal.ToString();
                    entities.SaveChanges();

                }
            }


        }

        private bool medirConsistencia(int puntajeTotal, int puntajeA, int puntajeB, int puntajeC, int puntajeD, int puntajeE)
        {
            bool consistencia = true;

            foreach (var item in listConsistencia)
            {
                String[] temp = item.Split(',');

                int total = Convert.ToInt32(temp[0]);
                int A = Convert.ToInt32(temp[1]);
                int B = Convert.ToInt32(temp[2]);
                int C = Convert.ToInt32(temp[3]);
                int D = Convert.ToInt32(temp[4]);
                int E = Convert.ToInt32(temp[5]);

                if (total == puntajeTotal)
                {

                    int susA = A - puntajeA;
                    int susB = B - puntajeB;
                    int susC = C - puntajeC;
                    int susD = E - puntajeD;
                    int susE = D - puntajeE;

                    if (susA >= -2 && susA <= 2 && susB >= -2 && susB <= 2 && susC >= -2 && susC <= 2 && susD >= -2 && susD <= 2 && susE >= -2 && susE <= 2)
                    {
                        consistencia = false;
                    }


                }


            }

            return consistencia;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}