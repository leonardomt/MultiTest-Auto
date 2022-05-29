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
    public partial class AnsiedadPrecomView : UserControl
    {
        private static AnsiedadPrecomView _instance;

        public AnsiedadCompetitiva ansiedad { set; get; }
        public static AnsiedadPrecomView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AnsiedadPrecomView();

                return _instance;
            }
        }

        public AnsiedadPrecomView()
        {
            InitializeComponent();
            ansiedad = new AnsiedadCompetitiva();
        }

        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join AnsiedadCompetitiva on SujetosEvaluados.PAnsiedadCompetitiva =  AnsiedadCompetitiva.idTest where PAnsiedadCompetitiva ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();

                                label19.Text = res["AS"].ToString() != "" ? res["AS"].ToString() : "";
                                label18.Text = res["AC"].ToString() != "" ? res["AC"].ToString() : "";
                                label4.Text = res["ACF"].ToString() != "" ? res["ACF"].ToString() : "";



                                //_-------------------//

                                ansiedad.AC = label18.Text;
                                ansiedad.ACF = label4.Text;
                                ansiedad.AS = label19.Text;
                            }
                        }
                    }
                }

            }
        }

        public void cambiarNombreAtleta(String nombreAtleta, String fecha)
        {
            label2.Text = nombreAtleta;
            label24.Text = fecha;
        }
    }
}
