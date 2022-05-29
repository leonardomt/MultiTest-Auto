using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;
using System.Data.SQLite;
using Multitest.ADOmodel;

namespace Multitest
{
    public partial class Etapa : Form
    {
        public Etapa()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {

                mainEntities entity = new mainEntities();
                Tipoetapa tipo = new Tipoetapa
                {
                    Etapa = textBox1.Text,

                };

                entity.Tipoetapa.Add(tipo);
                entity.SaveChanges();
                llenarTabla();
                textBox1.Text = "";
                MessageBox.Show("Los datos se han guardado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Los datos no deben ser vacío ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void llenarTabla()
        {
            using (mainEntities entities = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                using (SQLiteConnection c = new SQLiteConnection(entities.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("Select * from Tipoetapa", c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                                     read.GetValue(read.GetOrdinal("Etapa")),
                                      read.GetValue(read.GetOrdinal("idEtapa")),
                                        read.GetValue(read.GetOrdinal("Actual")),

                            });

                            }


                        }
                    }
                }

            }
        }

        private void Etapa_Load(object sender, EventArgs e)
        {
            llenarTabla();

            Screen d = Screen.PrimaryScreen;
            int with = d.Bounds.Width;
            int height = d.Bounds.Height;

            this.Height = height / 2;
            this.Width = with / 2;

        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {

                using (mainEntities entity = new mainEntities())
                {
                    Tipoetapa tipo = new Tipoetapa
                    {
                        Etapa = dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                        idEtapa = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString()),
                        Actual = dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                    };


                    if (tipo.Actual != "1")
                    {
                        entity.Tipoetapa.Attach(tipo);
                        entity.Entry(tipo).State = EntityState.Deleted;


                        entity.SaveChanges();
                        textBox1.Text = "Seleccione";
                        llenarTabla();
                        MessageBox.Show("Los datos se eliminaron satisfactoriamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No puede eliminar la etapa actual", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una etapa a eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}