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

namespace Multitest.AuxClass
{
    public partial class CatellDetails : Form
    {
        public CatellDetails(string id)
        {
            InitializeComponent();
            buscarPrueba(id);
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
    }
}
