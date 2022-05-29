using Multitest.ADOmodel;
using Multitest.VisualizarPruebas;
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
    public partial class EstablecerEtapa : Form
    {
        string etapa = "";
        public EstablecerEtapa()
        {
            InitializeComponent();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * FROM Tipoetapa", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "Etapa";
                    comboBox1.ValueMember = "idEtapa";

                }
            }
        }

        private void EstablecerEtapa_Load(object sender, EventArgs e)
        {
            using (mainEntities f = new mainEntities())
            {
                etapa = f.Tipoetapa.Where(s => s.Actual == "1").FirstOrDefault<Tipoetapa>().Etapa.ToString();

                label3.Text = etapa;

                Screen d = Screen.PrimaryScreen;
                int with = d.Bounds.Width;
                int height = d.Bounds.Height;

                this.Height = height / 2;
                this.Width = with / 2;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Seleccione")
            {
                using (mainEntities entities = new mainEntities())
                {
                    int idEtap = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    var res = entities.Tipoetapa.Where(s => s.Actual == "1").FirstOrDefault<Tipoetapa>();
                    res.Actual = null;

                    var ultimo = entities.Set<Tipoetapa>().OrderByDescending(t => t.idEtapa == idEtap).FirstOrDefault();
                    ultimo.Actual = "1";
                    entities.SaveChanges();
                    label3.Text = ultimo.Etapa;
                    ActiveControlUser.Instance.upDateEtapa(ultimo.Etapa);
                  
                }
               // UserControlPruebas.Instance.RefrescarEtapa();
              //  UserControlVisualizarPruebaAtleta.Instance.buscarEtapa();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
