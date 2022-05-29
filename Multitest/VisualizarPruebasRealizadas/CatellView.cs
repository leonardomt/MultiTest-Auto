using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Multitest.ADOmodel;
using System.Data.SQLite;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class CatellView : UserControl
    {

        private static CatellView _instance;
        public PruCatell catell { set; get; }
        public static CatellView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CatellView();

                return _instance;
            }
        }


        public CatellView()
        {
            InitializeComponent();
            catell = new PruCatell();
        }


        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join PruCatell on SujetosEvaluados.PCatell =  PruCatell.idTest where PCatell='" + id + "' ", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();


                                //Bruta latente
                                label10.Text = res["PBrutaLatQ3"].ToString() != "" ? res["PBrutaLatQ3"].ToString() : "";
                                label11.Text = res["PBrutaLatC"].ToString() != "" ? res["PBrutaLatC"].ToString() : "";
                                label12.Text = res["PBrutaLatL"].ToString() != "" ? res["PBrutaLatL"].ToString() : "";
                                label13.Text = res["PBrutaLatO"].ToString() != "" ? res["PBrutaLatO"].ToString() : "";
                                label14.Text = res["PBrutaLatQ4"].ToString() != "" ? res["PBrutaLatQ4"].ToString() : "";

                                //Bruta manifiesta
                                label17.Text = res["PBrutaManQ3"].ToString() != "" ? res["PBrutaManQ3"].ToString() : "";
                                label18.Text = res["PBrutaManC"].ToString() != "" ? res["PBrutaManC"].ToString() : "";
                                label19.Text = res["PBrutaManL"].ToString() != "" ? res["PBrutaManL"].ToString() : "";
                                label20.Text = res["PBrutaManO"].ToString() != "" ? res["PBrutaManO"].ToString() : "";
                                label21.Text = res["PBrutaManQ4"].ToString() != "" ? res["PBrutaManQ4"].ToString() : "";


                                //Bruta latente + manifieta
                                label24.Text = res["PBrutaLatManQ3"].ToString() != "" ? res["PBrutaLatManQ3"].ToString() : "";
                                label25.Text = res["PBrutaLaManC"].ToString() != "" ? res["PBrutaLaManC"].ToString() : "";
                                label26.Text = res["PBrutaLatManL"].ToString() != "" ? res["PBrutaLatManL"].ToString() : "";
                                label27.Text = res["PBrutaLatManO"].ToString() != "" ? res["PBrutaLatManO"].ToString() : "";
                                label28.Text = res["PBrutaLatManQ4"].ToString() != "" ? res["PBrutaLatManQ4"].ToString() : "";






                                //Stens latente
                                label33.Text = res["PStensLatQ3"].ToString() != "" ? res["PStensLatQ3"].ToString() : "";
                                label34.Text = res["PStensLatC"].ToString() != "" ? res["PStensLatC"].ToString() : "";
                                label35.Text = res["PStensLatL"].ToString() != "" ? res["PStensLatL"].ToString() : "";
                                label36.Text = res["PStensLatO"].ToString() != "" ? res["PStensLatO"].ToString() : "";
                                label37.Text = res["PStensLatQ4"].ToString() != "" ? res["PStensLatQ4"].ToString() : "";

                                //Stens manifiesta
                                label39.Text = res["PStensManQ3"].ToString() != "" ? res["PStensManQ3"].ToString() : "";
                                label40.Text = res["PStensManC"].ToString() != "" ? res["PStensManC"].ToString() : "";
                                label41.Text = res["PStensManL"].ToString() != "" ? res["PStensManL"].ToString() : "";
                                label42.Text = res["PStensManO"].ToString() != "" ? res["PStensManO"].ToString() : "";
                                label43.Text = res["PStensManQ4"].ToString() != "" ? res["PStensManQ4"].ToString() : "";


                                //stens latente + manifieta
                                label45.Text = res["PStensLatManQ3"].ToString() != "" ? res["PStensLatManQ3"].ToString() : "";
                                label46.Text = res["PStensLatManC"].ToString() != "" ? res["PStensLatManC"].ToString() : "";
                                label47.Text = res["PStensLatManL"].ToString() != "" ? res["PStensLatManL"].ToString() : "";
                                label48.Text = res["PStensLatManO"].ToString() != "" ? res["PStensLatManO"].ToString() : "";
                                label49.Text = res["PStensLatManQ4"].ToString() != "" ? res["PStensLatManQ4"].ToString() : "";



                                label54.Text = res["IntSico"].ToString() != "" ? res["IntSico"].ToString() : "No existe";
                                label30.Text = res["PuntTotalLE"].ToString() != "" ? res["PuntTotalLE"].ToString() : "";


                                label52.Text = res["PStensTotal"].ToString() != "" ? res["PStensTotal"].ToString() : "";


                                //--------------------------------------------------------------//


                                catell.PBrutaManQ3 = res["PBrutaManQ3"].ToString();
                                catell.PBrutaManC = res["PBrutaManC"].ToString();
                                catell.PBrutaManL = res["PBrutaManL"].ToString();
                                catell.PBrutaManO = res["PBrutaManO"].ToString();
                                catell.PBrutaManQ4 = res["PBrutaManQ4"].ToString();

                                catell.PBrutaLatQ3 = res["PBrutaLatQ3"].ToString();
                                catell.PBrutaLatC = res["PBrutaLatC"].ToString();
                                catell.PBrutaLatL = res["PBrutaLatL"].ToString();
                                catell.PBrutaLatO = res["PBrutaLatO"].ToString();
                                catell.PBrutaLatQ4 = res["PBrutaLatQ4"].ToString();


                                catell.PBrutaLatManQ3 = res["PBrutaLatManQ3"].ToString();
                                catell.PBrutaLaManC = res["PBrutaLaManC"].ToString();
                                catell.PBrutaLatManL = res["PBrutaLatManL"].ToString();
                                catell.PBrutaLatManO = res["PBrutaLatManO"].ToString();
                                catell.PBrutaLatManQ4 = res["PBrutaLatManQ4"].ToString();





                                catell.PStensManQ3 = res["PStensManQ3"].ToString();
                                catell.PStensManC = res["PStensManC"].ToString();
                                catell.PStensManL = res["PStensManL"].ToString();
                                catell.PStensManO = res["PStensManO"].ToString();
                                catell.PStensManQ4 = res["PStensManQ4"].ToString();

                                catell.PStensLatQ3 = res["PStensLatQ3"].ToString();
                                catell.PStensLatC = res["PStensLatC"].ToString();
                                catell.PStensLatL = res["PStensLatL"].ToString();
                                catell.PStensLatO = res["PStensLatO"].ToString();
                                catell.PStensLatQ4 = res["PStensLatQ4"].ToString();



                                catell.PStensLatManQ3 = res["PStensLatManQ3"].ToString();
                                catell.PStensLatManC = res["PStensLatManC"].ToString();
                                catell.PStensLatManL = res["PStensLatManL"].ToString();
                                catell.PStensLatManO = res["PStensLatManO"].ToString();
                                catell.PStensLatManQ4 = res["PStensLatManQ4"].ToString();



                                catell.PBrutaTotal= res["PBrutaTotal"].ToString();
                                catell.IntSico = res["IntSico"].ToString();
                                catell.PuntTotalLE = res["PuntTotalLE"].ToString();
                                catell.PStensTotal = res["PStensTotal"].ToString();


                            }
                        }
                    }
                }


            }
        }


        public void cambiarNombreAtleta(String nombreAtleta, String fecha)
        {

            label2.Text = nombreAtleta;
            label56.Text = fecha;
        }
    }
}
