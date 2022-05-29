using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Multitest.ADOmodel;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class IdetemView : UserControl
    {
        private static IdetemView _instance;
        string testId = "";
        public Idetem idetem { set; get; }

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

        public static IdetemView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IdetemView();

                return _instance;
            }
        }
        public IdetemView()
        {
            InitializeComponent();
            idetem = new Idetem();
        }

        public void cambiarNombreAtleta(String nombreAtleta, String fecha)
        {
            label2.Text = nombreAtleta;
            label24.Text = fecha;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IdetemProp d = new IdetemProp("Sistema Nervioso", testId);
            d.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IdetemProp d = new IdetemProp("Propiedades psicodinámicas", testId);
            d.ShowDialog();
        }

        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {
                testId = id;
                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join Idetem on SujetosEvaluados.PIdetem =  Idetem.idTest where PIdetem ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();



                                label19.Text = res["sanguineo"].ToString() != "" ? res["sanguineo"].ToString() : "";
                                label20.Text = res["colerico"].ToString() != "" ? res["colerico"].ToString() : "";
                                label18.Text = res["flematico"].ToString() != "" ? res["flematico"].ToString() : "";
                                label23.Text = res["melancolico"].ToString() != "" ? res["melancolico"].ToString() : "";


                                label7.Text = res["porcientoSanguineo"].ToString() != "" ? res["porcientoSanguineo"].ToString() : "";
                                label6.Text = res["porcientoColerico"].ToString() != "" ? res["porcientoColerico"].ToString() : "";
                                label5.Text = res["porcientoFlematico"].ToString() != "" ? res["porcientoFlematico"].ToString() : "";
                                label4.Text = res["porcientoMelancolico"].ToString() != "" ? res["porcientoMelancolico"].ToString() : "";


                                idetem.sanguineo = label19.Text;
                                idetem.colerico = label20.Text;
                                idetem.flematico = label18.Text;
                                idetem.melancolico = label23.Text;

                                idetem.porcientoSanguineo = label7.Text;
                                idetem.porcientoColerico = label6.Text;
                                idetem.porcientoFlematico = label5.Text;
                                idetem.porcientoMelancolico = label4.Text;





                                var cantequilibrioTemp = Convert.ToInt32(res["equilibrio"].ToString()) * 100 / cantequilibrio;
                                var cantdeseQuilExitaTemp = Convert.ToInt32(res["deseqExita"].ToString()) * 100 / cantdeseQuilExita;
                                var cantdeseQuilInibicionTemp = Convert.ToInt32(res["deseqInhibi"].ToString()) * 100 / cantdeseQuilInibicion;
                                var cantfuerzaTemp = Convert.ToInt32(res["fuerza"].ToString()) * 100 / cantfuerza;
                                var cantdebilidadTemp = Convert.ToInt32(res["debilidad"].ToString()) * 100 / cantdebilidad;
                                var cantmovilidadTemp = Convert.ToInt32(res["movilidad"].ToString()) * 100 / cantmovilidad;
                                var cantinerciaTemp = Convert.ToInt32(res["inercia"].ToString()) * 100 / cantinercia;
                                var cantdinamiPsiTemp = Convert.ToInt32(res["dinamPsiq"].ToString()) * 100 / cantdinamiPsi;
                                var cantpocoDinamiPsiTemp = Convert.ToInt32(res["pocoDinaPsiq"].ToString()) * 100 / cantpocoDinamiPsi;
                                var cantlabilidadTemp = Convert.ToInt32(res["labilidad"].ToString()) * 100 / cantlabilidad;



                                idetem.equilibrio = cantequilibrioTemp > 50 ? res["equilibrio"].ToString() + " (Significativo)" : res["equilibrio"].ToString();
                                idetem.deseqExita = cantdeseQuilExitaTemp > 50 ? res["deseqExita"].ToString() + " (Significativo)" : res["deseqExita"].ToString();
                                idetem.deseqInhibi = cantdeseQuilInibicionTemp > 50 ? res["deseqInhibi"].ToString() + " (Significativo)" : res["deseqInhibi"].ToString();
                                idetem.fuerza = cantfuerzaTemp > 50 ? res["fuerza"].ToString() + " (Significativo)" : res["fuerza"].ToString();
                                idetem.debilidad = cantdebilidadTemp > 50 ? res["debilidad"].ToString() + " (Significativo)" : res["debilidad"].ToString();
                                idetem.movilidad = cantmovilidadTemp > 50 ? res["movilidad"].ToString() + " (Significativo)" : res["movilidad"].ToString();
                                idetem.inercia = cantinerciaTemp > 50 ? res["inercia"].ToString() + " (Significativo)" : res["inercia"].ToString();
                                idetem.dinamPsiq = cantdinamiPsiTemp > 50 ? res["dinamPsiq"].ToString() + " (Significativo)" : res["dinamPsiq"].ToString();
                                idetem.pocoDinaPsiq = cantpocoDinamiPsiTemp > 50 ? res["pocoDinaPsiq"].ToString() + " (Significativo)" : res["pocoDinaPsiq"].ToString();
                                idetem.labilidad = cantlabilidadTemp > 50 ? res["labilidad"].ToString() + " (Significativo)" : res["labilidad"].ToString();










                                var cantactividadTemp = Convert.ToInt32(res["actividad"].ToString()) * 100 / cantactividad;
                                var cantreacModerTemp = Convert.ToInt32(res["reactModer"].ToString()) * 100 / cantreacModer;
                                var cantreacAltaTemp = Convert.ToInt32(res["reactAlta"].ToString()) * 100 / cantreacAlta;
                                var cantresisAltaTemp = Convert.ToInt32(res["resisAlta"].ToString()) * 100 / cantresisAlta;
                                var cantresisBajaTemp = Convert.ToInt32(res["resisBaja"].ToString()) * 100 / cantresisBaja;
                                var cantritmPsiRapidoTemp = Convert.ToInt32(res["ritmPsiRap"].ToString()) * 100 / cantritmPsiRapido;
                                var cantritmPsiLentoTemp = Convert.ToInt32(res["ritmPsiLent"].ToString()) * 100 / cantritmPsiLento;
                                var cantsensibilidadTemp = Convert.ToInt32(res["sensibilidad"].ToString()) * 100 / cantsensibilidad;
                                var cantpocaSensibiTemp = Convert.ToInt32(res["pocaSensi"].ToString()) * 100 / cantpocaSensibi;
                                var cantextroversionTemp = Convert.ToInt32(res["extroversion"].ToString()) * 100 / cantextroversion;
                                var cantintroversionTemp = Convert.ToInt32(res["introversion"].ToString()) * 100 / cantintroversion;
                                var cantplazticidadTemp = Convert.ToInt32(res["plasticidad"].ToString()) * 100 / cantplazticidad;
                                var cantrigidezTemp = Convert.ToInt32(res["rigidez"].ToString()) * 100 / cantrigidez;




                                idetem.actividad = cantactividadTemp > 50 ? res["actividad"].ToString() + " (Significativo)" : res["actividad"].ToString();
                                idetem.reactModer = cantreacModerTemp > 50 ? res["reactModer"].ToString() + " (Significativo)" : res["reactModer"].ToString();
                                idetem.reactAlta = cantreacAltaTemp > 50 ? res["reactAlta"].ToString() + " (Significativo)" : res["reactAlta"].ToString();
                                idetem.resisAlta = cantresisAltaTemp > 50 ? res["resisAlta"].ToString() + " (Significativo)" : res["resisAlta"].ToString();
                                idetem.resisBaja = cantresisBajaTemp > 50 ? res["resisBaja"].ToString() + " (Significativo)" : res["resisBaja"].ToString();
                                idetem.ritmPsiRap = cantritmPsiRapidoTemp > 50 ? res["ritmPsiRap"].ToString() + " (Significativo)" : res["ritmPsiRap"].ToString();
                                idetem.ritmPsiLent = cantritmPsiLentoTemp > 50 ? res["ritmPsiLent"].ToString() + " (Significativo)" : res["ritmPsiLent"].ToString();
                                idetem.sensibilidad = cantsensibilidadTemp > 50 ? res["sensibilidad"].ToString() + " (Significativo)" : res["sensibilidad"].ToString();
                                idetem.pocaSensi = cantpocaSensibiTemp > 50 ? res["pocaSensi"].ToString() + " (Significativo)" : res["pocaSensi"].ToString();
                                idetem.extroversion = cantextroversionTemp > 50 ? res["extroversion"].ToString() + " (Significativo)" : res["extroversion"].ToString();
                                idetem.introversion = cantintroversionTemp > 50 ? res["introversion"].ToString() + " (Significativo)" : res["introversion"].ToString();
                                idetem.plasticidad = cantplazticidadTemp > 50 ? res["plasticidad"].ToString() + " (Significativo)" : res["plasticidad"].ToString();
                                idetem.rigidez = cantrigidezTemp > 50 ? res["rigidez"].ToString() + " (Significativo)" : res["rigidez"].ToString();


                            }
                        }
                    }
                }
            }
        }
    }
}
