using Multitest.ADOmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest
{
    public partial class IdetemProp : Form
    {

        int cantequilibrio = 16;
        int cantdeseQuilExita = 13;
        int cantdeseQuilInibicion = 9;
        int cantfuerza = 9;
        int cantdebilidad = 9;
        int cantmovilidad = 14;
        int cantinercia = 10;
        int cantdinamiPsi = 10;
        int cantpocoDinamiPsi = 3;
        int cantlabilidad = 5;


        double cantequilibrioTemp = 0;
        double cantdeseQuilExitaTemp = 0;
        double cantdeseQuilInibicionTemp = 0;
        double cantfuerzaTemp = 0;
        double cantdebilidadTemp = 0;
        double cantmovilidadTemp = 4;
        double cantinerciaTemp = 0;
        double cantdinamiPsiTemp = 0;
        double cantpocoDinamiPsiTemp = 0;
        double cantlabilidadTemp = 0;


        int cantactividad = 8;
        int cantreacModer = 9;
        int cantreacAlta = 10;
        int cantresisAlta = 10;
        int cantresisBaja = 6;
        int cantritmPsiRapido = 5;
        int cantritmPsiLento = 4;
        int cantsensibilidad = 7;
        int cantpocaSensibi = 1;
        int cantextroversion = 6;
        int cantintroversion = 4;
        int cantplazticidad = 10;
        int cantrigidez = 5;

        public IdetemProp(String propiedad, String idTest)
        {
            InitializeComponent();

            if (propiedad == "Sistema Nervioso")

                llenarNervioso(idTest);

            else
                llenarPsicoDinamica(idTest);


        }

        private void llenarPsicoDinamica(String idTest)
        {
            label1.Text = "Propiedades Psicodinámicas";


            label2.Text = "Actividad:";
            label4.Text = "Reactividad moderada:";
            label6.Text = "Reactividad alta:";
            label8.Text = "Resistencia alta:";
            label10.Text = "Resistencia baja:";
            label12.Text = "Ritmo psíquico rápido:";
            label14.Text = "Ritmo psíquico lento:";
            label16.Text = "Sensibilidad:";
            label18.Text = "Poca sensibilidad:";
            label20.Text = "Extroversión:";

            label22.Text = "Introversión:";
            label24.Text = "Plasticidad:";
            label26.Text = "Rigidez:";


            label24.Visible = true;
            label26.Visible = true;
            label25.Visible = true;
            label27.Visible = true;
            label22.Visible = true;
            label23.Visible = true;

            using (mainEntities f = new mainEntities())
            {
                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("Select * from Idetem where idTest='" + idTest + "'", c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            read.Read();

                            label3.Text = read["actividad"].ToString() + " ptos";
                            label5.Text = read["reactModer"].ToString() + " ptos";
                            label7.Text = read["reactAlta"].ToString() + " ptos";
                            label9.Text = read["resisAlta"].ToString() + " ptos";
                            label11.Text = read["resisBaja"].ToString() + " ptos";
                            label13.Text = read["ritmPsiRap"].ToString() + " ptos";
                            label15.Text = read["ritmPsiLent"].ToString() + " ptos";
                            label17.Text = read["sensibilidad"].ToString() + " ptos";
                            label19.Text = read["pocaSensi"].ToString() + " ptos";
                            label21.Text = read["extroversion"].ToString() + " ptos";

                            label23.Text = read["introversion"].ToString() + " ptos";
                            label25.Text = read["plasticidad"].ToString() + " ptos";
                            label27.Text = read["rigidez"].ToString() + " ptos";

 

                            cantactividad = Convert.ToInt32(read["actividad"].ToString()) * 100 / cantactividad;
                            cantreacModer = Convert.ToInt32(read["reactModer"].ToString()) * 100 / cantreacModer;
                            cantreacAlta = Convert.ToInt32(read["reactAlta"].ToString()) * 100 / cantreacAlta;
                            cantresisAlta = Convert.ToInt32(read["resisAlta"].ToString()) * 100 / cantresisAlta;
                            cantresisBaja = Convert.ToInt32(read["resisBaja"].ToString()) * 100 / cantresisBaja;
                            cantritmPsiRapido = Convert.ToInt32(read["ritmPsiRap"].ToString()) * 100 / cantritmPsiRapido;
                            cantritmPsiLento = Convert.ToInt32(read["ritmPsiLent"].ToString()) * 100 / cantritmPsiLento;
                            cantsensibilidad = Convert.ToInt32(read["sensibilidad"].ToString()) * 100 / cantsensibilidad;
                            cantpocaSensibi = Convert.ToInt32(read["pocaSensi"].ToString()) * 100 / cantpocaSensibi;
                            cantextroversion = Convert.ToInt32(read["extroversion"].ToString()) * 100 / cantextroversion;

                            cantintroversion = Convert.ToInt32(read["introversion"].ToString()) * 100 / cantintroversion;
                            cantplazticidad = Convert.ToInt32(read["plasticidad"].ToString()) * 100 / cantplazticidad;
                            cantrigidez = Convert.ToInt32(read["rigidez"].ToString()) * 100 / cantrigidez;



                            label3.Text = cantactividad > 50 ? label3.Text + " (Significativo)" : label3.Text;
                            label5.Text = cantreacModer > 50 ? label5.Text + " (Significativo)" : label5.Text;
                            label7.Text = cantreacAlta > 50 ? label7.Text + " (Significativo)" : label7.Text;
                            label9.Text = cantresisAlta > 50 ? label9.Text + " (Significativo)" : label9.Text;
                            label11.Text = cantresisBaja > 50 ? label11.Text + " (Significativo)" : label11.Text;
                            label13.Text = cantritmPsiRapido > 50 ? label13.Text + " (Significativo)" : label13.Text;
                            label15.Text = cantritmPsiLento > 50 ? label15.Text + " (Significativo)" : label15.Text;
                            label17.Text = cantsensibilidad > 50 ? label17.Text + " (Significativo)" : label17.Text;
                            label19.Text = cantpocaSensibi > 50 ? label19.Text + " (Significativo)" : label19.Text;
                            label21.Text = cantextroversion > 50 ? label21.Text + " (Significativo)" : label21.Text;

                            label23.Text = cantintroversion > 50 ? label23.Text + " (Significativo)" : label23.Text;
                            label25.Text = cantplazticidad > 50 ? label25.Text + " (Significativo)" : label25.Text;
                            label27.Text = cantrigidez > 50 ? label27.Text + " (Significativo)" : label27.Text;

                        }
                    }

                }
            }

        }

        private void llenarNervioso(String idTest)
        {
            label1.Text = "Propiedades del sistema nervioso";


            label2.Text = "Equilibrio:";
            label4.Text = "Desequilibrio por excitación:";
            label6.Text = "Desequilibrio por inhibición:";
            label8.Text = "Fuerza:";
            label10.Text = "Debilidad:";
            label12.Text = "Movilidad:";
            label14.Text = "Inercia:";
            label16.Text = "Dinamismo psíquico:";
            label18.Text = "Poco dinamismo psíquico:";
            label20.Text = "Labilidad:";


            label24.Visible = false;
            label26.Visible = false;
            label25.Visible = false;
            label27.Visible = false;
            label22.Visible = false;
            label23.Visible = false;






            using (mainEntities f = new mainEntities())
            {
                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("Select * from Idetem where idTest='" + idTest + "'", c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            read.Read();

                            label3.Text = read["equilibrio"].ToString() + " ptos";
                            label5.Text = read["deseqExita"].ToString() + " ptos";
                            label7.Text = read["deseqInhibi"].ToString() + " ptos";
                            label9.Text = read["fuerza"].ToString() + " ptos";
                            label11.Text = read["debilidad"].ToString() + " ptos";
                            label13.Text = read["movilidad"].ToString() + " ptos";
                            label15.Text = read["inercia"].ToString() + " ptos";
                            label17.Text = read["dinamPsiq"].ToString() + " ptos";
                            label19.Text = read["pocoDinaPsiq"].ToString() + " ptos";
                            label21.Text = read["labilidad"].ToString() + " ptos";



                            cantequilibrioTemp = Convert.ToInt32(read["equilibrio"].ToString()) * 100 / cantequilibrio;
                            cantdeseQuilExitaTemp = Convert.ToInt32(read["deseqExita"].ToString()) * 100 / cantdeseQuilExita;
                            cantdeseQuilInibicionTemp = Convert.ToInt32(read["deseqInhibi"].ToString()) * 100 / cantdeseQuilInibicion;
                            cantfuerzaTemp = Convert.ToInt32(read["fuerza"].ToString()) * 100 / cantfuerza;
                            cantdebilidadTemp = Convert.ToInt32(read["debilidad"].ToString()) * 100 / cantdebilidad;
                            cantmovilidadTemp = Convert.ToInt32(read["movilidad"].ToString()) * 100 / cantmovilidad;
                            cantinerciaTemp = Convert.ToInt32(read["inercia"].ToString()) * 100 / cantinercia;
                            cantdinamiPsiTemp = Convert.ToInt32(read["dinamPsiq"].ToString()) * 100 / cantdinamiPsi;
                            cantpocoDinamiPsiTemp = Convert.ToInt32(read["pocoDinaPsiq"].ToString()) * 100 / cantpocoDinamiPsi;
                            cantlabilidadTemp = Convert.ToInt32(read["labilidad"].ToString()) * 100 / cantlabilidad;



                            label3.Text = cantequilibrioTemp > 50 ? label3.Text + " (Significativo)" : label3.Text;
                            label5.Text = cantdeseQuilExitaTemp > 50 ? label5.Text + " (Significativo)" : label5.Text;
                            label7.Text = cantdeseQuilInibicionTemp > 50 ? label7.Text + " (Significativo)" : label7.Text;
                            label9.Text = cantfuerzaTemp > 50 ? label9.Text + " (Significativo)" : label9.Text;
                            label11.Text = cantdebilidadTemp > 50 ? label11.Text + " (Significativo)" : label11.Text;
                            label13.Text = cantmovilidadTemp > 50 ? label13.Text + " (Significativo)" : label13.Text;
                            label15.Text = cantinerciaTemp > 50 ? label15.Text + " (Significativo)" : label15.Text;
                            label17.Text = cantdinamiPsiTemp > 50 ? label17.Text + " (Significativo)" : label17.Text;
                            label19.Text = cantpocoDinamiPsiTemp > 50 ? label19.Text + " (Significativo)" : label19.Text;
                            label21.Text = cantlabilidadTemp > 50 ? label21.Text + " (Significativo)" : label21.Text;


                        }
                    }

                }
            }

           


        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
