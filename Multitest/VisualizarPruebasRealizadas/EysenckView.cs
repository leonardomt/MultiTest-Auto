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
    public partial class EysenckView : UserControl
    {

        private static EysenckView _instance;
        public PruEysenk Eysenk { get; set; }
        public static EysenckView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EysenckView();

                return _instance;
            }
        }


        public EysenckView()
        {
            InitializeComponent();
            Eysenk = new PruEysenk();
        }

        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join PruEysenk on SujetosEvaluados.PEysenk =  PruEysenk.idTest where PEysenk ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();



                                label19.Text = res["Neuroticismo"].ToString() != "" ? res["Neuroticismo"].ToString() : "";
                                label20.Text = res["Extroversion"].ToString() != "" ? res["Extroversion"].ToString() : "";
                                label18.Text = res["Sinceridad"].ToString() != "" ? res["Sinceridad"].ToString() : "";


                                String diagnostico = res["DiagNeurotic"].ToString() + "-" + res["DiagExtrove"].ToString();

                                label10.Text = diagnostico != "" ? diagnostico : "";

                                label8.Text = res["DiagCuadrante"].ToString() != "" ? res["DiagCuadrante"].ToString() : "";

                                label4.Text = res["DiagnosticoLetra"].ToString() != "" ? res["DiagnosticoLetra"].ToString() : "";



                                if (label8.Text == "Melancólico")
                                {
                                    label6.Text = "Hábil, Ansioso, Rígido, Severo, Pesimista, Reservado, Insaciable y Tranquilo";
                                    label2.Text = "DEBIL ( Equilibrio menor, Fuerza menor, Movilidad menor)";
                                }

                                if (label8.Text == "Colérico")
                                {
                                    label6.Text = "Susceptible, Agitado, Agresivo,Excitable, Variable,Impulsivo, Optimista y Activo";
                                    label2.Text = "FUERTE ( Equilibrio menor, Fuerza mayor, Movilidad mayor)";
                                }

                                if (label8.Text == "Flemático")
                                {
                                    label6.Text = "Pasivo, Cuidadoso, Pensativo, Apacible, Controlado, Leal, Ecuanime e Imperturbable";
                                    label2.Text = "FUERTE ( Equilibrio mayor, Fuerza mayor, Movilidad menor (pero normal) )";
                                }

                                if (label8.Text == "Sanguíneo")
                                {
                                    label6.Text = "Sociable, Expresivo, Locuaz, Sensible, Vivaz, Adaptable, Animado, Despreocupado y Diligente";
                                    label2.Text = "FUERTE ( Equilibrio mayor, Fuerza mayor, Movilidad mayor)";
                                }

                                if (label8.Text == "No determinado")
                                {
                                    label6.Text = "No se pudo determinar la personalidad, se aconseja repetir la prueba";
                                    label2.Text = "No se pudo determinar la personalidad, se aconseja repetir la prueba";
                                }



                                Eysenk.Neuroticismo= label19.Text;
                                
                                Eysenk.Extroversion = label20.Text;
                                Eysenk.Sinceridad = label18.Text;

                                Eysenk.DiagCuadrante = label8.Text;
                                Eysenk.DiagnosticoLetra = label4.Text;

                                Eysenk.DiagNeurotic = res["DiagNeurotic"].ToString();
                                Eysenk.DiagExtrove= res["DiagExtrove"].ToString();

                            }

                        }
                    }
                }

            }
        }

        public void cambiarNombreAtleta(String nombreAtleta, String fecha)
        {
            label24.Text = nombreAtleta;
            label27.Text = fecha;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
