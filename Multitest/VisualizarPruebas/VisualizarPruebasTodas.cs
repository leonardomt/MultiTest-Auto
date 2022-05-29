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
using Multitest.VisualizarPruebasRealizadas;
using Multitest.AuxClass;
using System.IO;
using System.Diagnostics;
using Multitest.FormAux;

namespace Multitest
{
    public partial class VisualizarPruebasTodas : UserControl
    {

        String atleta = "";
        String fecha = "";
        String nombrePrueba = "";
        string etapa = "";
        string deporte = "";

        String idTestPrueba = "";
        String idTestTodaPrueba = "";

        private static VisualizarPruebasTodas _instance;
        List<String> valoresIdetem = new List<string>();
        public static VisualizarPruebasTodas Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VisualizarPruebasTodas();

                return _instance;
            }
        }
        public VisualizarPruebasTodas()
        {
            InitializeComponent();
            ActiveControlUser.Instance.addControl("VisualizarPruebasTodas");

        }

        public void cambiarNombre(String nombre)
        {
            atleta = "";
            fecha = "";

            nombrePrueba = nombre;

            comboBox1.DataSource = null;
            comboBox1.Text = "Seleccione";
            comboBox1.Enabled = false;

            comboBox3.DataSource = null;
            comboBox3.Text = "Seleccione";
            comboBox3.Enabled = false;


            comboBox4.DataSource = null;
            comboBox4.Text = "Seleccione";


            dataGridView1.Rows.Clear();

            checkBox1.CheckState = CheckState.Unchecked;
            dataGridView1.Columns.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (nombre == "Catell")
            {
                llenarColumnaTablaCatell();
                llenarTablaCatell();
            }


            if (nombre == "Raven")
            {
                llenarColumnaTablaRaven();
                llenarTablaRaven();
            }


            if (nombre == "IDARE (Situacional)")
            {
                llenarColumnaTablaIdareSituacional();
                llenarTablaIdareSituacional();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }

            if (nombre == "IDARE (Rasgo)")
            {

                llenarColumnaTablaIdareRasgo();
                llenarTablaIdareRasgo();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }

            if (nombre == "Test de Motivos Deportivos de Butt")
            {
                llenarColumnaTablaButt();
                llenarTablaButt();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Eysenck")
            {
                llenarColumnaTablaEysenck();
                llenarTablaEysenck();
                //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            if (nombre == "Dominó")
            {
                llenarColumnaTablaDomino();
                llenarTablaDomino();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Inventario Psicológico de Ejecución Deportiva(IPED)")
            {
                llenarColumnaTablaIped();
                llenarTablaIped();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            }

            if (nombre == "Weil")
            {
                llenarColumnaTablaWeil();
                llenarTablaWeil();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "IDETEM-1")
            {
                llenarColumnaTablaIdetem();
                llenarTablaIdetem();
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Cualidades Volitivas en el Deporte")
            {
                llenarColumnaTablaCaulidadesVolitivas();
                llenarTablaCualidadesVolitivas();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

            if (nombre == "Cualidades Motivacionales Deportivas")
            {
                llenarColumnaTablaCaulidadesMotivacionales();
                llenarTablaCualidadesMotivacionales();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

            if (nombre == "Actitud ante la Competencia")
            {
                llenarColumnaTablaActitudAnteCompe();
                llenarTablaActitudAnteCompe();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Tiempo de Reacción Simple")
            {
                llenarColumnaTablaTRS();
                llenarTablaTRS();
                //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Tiempo de Reacción Complejo")
            {
                llenarColumnaTablaTRC();
                llenarTablaTRC();
                //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Tiempo de Reacción Complejo con Sonido")
            {
                llenarColumnaTablaTRCS();
                llenarTablaTRCS();
                //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "Tiempo de Respuesta Anticipada")
            {
                llenarColumnaTablaRA();
                llenarTablaRA();
                //   dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (nombre == "16PF")
            {
                llenarColumnaTabla16Pf();
                llenarTabla16Pf();

            }

            if (nombre == "Tabla Rojo y Negra")
            {
                llenarColumnaTablaTRN();
                llenarTablaTRN();
                //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }


            if (nombre == "POMS")
            {
                llenarColumnaPOMS();
                llenarTablaPOMS();
                //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }


            if (nombre == "Ansiedad Precompetitiva CSAI-2R")
            {
                llenarColumnAnsiedadPreComp();
                llenarTablaAnsiedadPreComp();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }


        }

        private void llenarTablaAnsiedadPreComp()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();

                String query = "select * FROM AnsiedadCompetitiva inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  AnsiedadCompetitiva.idTest = SujetosEvaluados.PAnsiedadCompetitiva where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM AnsiedadCompetitiva inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  AnsiedadCompetitiva.idTest = SujetosEvaluados.PAnsiedadCompetitiva where  PAnsiedadCompetitiva != '' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM AnsiedadCompetitiva inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  AnsiedadCompetitiva.idTest = SujetosEvaluados.PAnsiedadCompetitiva where idSujeto='" + comboBox1.SelectedValue + "' and PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "' ";
                }


                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM AnsiedadCompetitiva inner join SujetosEvaluados inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  AnsiedadCompetitiva.idTest = SujetosEvaluados.PAnsiedadCompetitiva where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM AnsiedadCompetitiva inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  AnsiedadCompetitiva.idTest = SujetosEvaluados.PAnsiedadCompetitiva where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "' ";
                }



                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                         read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                  

                           read.GetValue(read.GetOrdinal("AS")),
                            read.GetValue(read.GetOrdinal("AC")),
                             read.GetValue(read.GetOrdinal("ACF")),


                         DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),



                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),


                            });

                            }


                        }
                    }

                }
            }
        }

        private void llenarColumnAnsiedadPreComp()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

         


            dataGridView1.Columns.Add("repetiiones", "Ansiedad Somática");
            dataGridView1.Columns.Add("saltos", "Ansiedad cognitiva");
            dataGridView1.Columns.Add("ccolor", "Autoconfianza");


            dataGridView1.Columns.Add("fecha", "Fecha");


            dataGridView1.Columns.Add("7", "id");
            dataGridView1.Columns[7].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[8].Visible = false;
        }

        private void llenarTablaPOMS()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();

                String query = "select * FROM PruPoms inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruPoms.idTest = SujetosEvaluados.PPoms where    Etapa='" + etapa + "'";



                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM PruPoms inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruPoms.idTest = SujetosEvaluados.PPoms where  PPoms != '' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruPoms inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruPoms.idTest = SujetosEvaluados.PPoms where idSujeto='" + comboBox1.SelectedValue + "' and PPoms != ''  and Etapa='" + etapa + "' ";
                }


                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruPoms inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruPoms.idTest = SujetosEvaluados.PPoms where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PPoms != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruPoms inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruPoms.idTest = SujetosEvaluados.PPoms where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PPoms != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                         read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                        read.GetValue(read.GetOrdinal("TensionAnsiedad")),
                        read.GetValue(read.GetOrdinal("DepresionMelancolia")),

                        read.GetValue(read.GetOrdinal("AngustiaHostilidad")),
                         read.GetValue(read.GetOrdinal("VigorActividad")),
                          read.GetValue(read.GetOrdinal("FatigaInercia")),
                           read.GetValue(read.GetOrdinal("ConfusionDesorient")),
                            read.GetValue(read.GetOrdinal("Amistosidad")),


                         DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),
                        dataGridView1.Columns[11].HeaderText="Detalles",


                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),


                            });

                            }


                        }
                    }

                }
            }
        }

        private void llenarTablaCatell()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruCatell inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruCatell.idTest = SujetosEvaluados.PCatell where    Etapa='" + etapa + "'";



                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM PruCatell inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruCatell.idTest = SujetosEvaluados.PCatell where  PCatell != '' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruCatell inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruCatell.idTest = SujetosEvaluados.PCatell where idSujeto='" + comboBox1.SelectedValue + "' and PCatell != ''  and Etapa='" + etapa + "' ";
                }


                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruCatell inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruCatell.idTest = SujetosEvaluados.PCatell where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PCatell != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruCatell inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruCatell.idTest = SujetosEvaluados.PCatell where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PCatell != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                         read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                       

                        read.GetValue(read.GetOrdinal("PuntTotalLE")),
                        read.GetValue(read.GetOrdinal("PStensTotal")),

                        read.GetValue(read.GetOrdinal("IntSico")),

                         DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),
                        dataGridView1.Columns[7].HeaderText="Ptos.Bruta y Stems",


                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),


                            });

                            }


                        }
                    }

                }
            }
        }

        private void llenarColumnaTablaCatell()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

          

            dataGridView1.Columns.Add("ansieda", "Ansiedad Total");
            dataGridView1.Columns.Add("stens", "Puntaje Ansiedad Stems");
            dataGridView1.Columns.Add("ccolor", "Imperpretación Psicológica");

            dataGridView1.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Ptos.Bruta y Stems";
            button.FlatStyle = FlatStyle.Flat;
            button.DefaultCellStyle.ForeColor = Color.White;
            button.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            dataGridView1.Columns.Add(button);

            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;





            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.Columns.Add("idTodo", "idTodo");
            dataGridView1.Columns[9].Visible = false;
        }

        private void llenarColumnaTablaTRN()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

         

            dataGridView1.Columns.Add("repetiiones", "Repeticiones");
            dataGridView1.Columns.Add("saltos", "Saltos");
            dataGridView1.Columns.Add("ccolor", "Cambios de Color");
            dataGridView1.Columns.Add("cdnr", "Cambio de Dirección no Rectificado");
            dataGridView1.Columns.Add("cdr", "Cambio de Dirección Rectificado");
            dataGridView1.Columns.Add("cal", "Calificación");
            dataGridView1.Columns.Add("duracion", "Duración");

            dataGridView1.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Detalles";
            button.FlatStyle = FlatStyle.Flat;
            button.DefaultCellStyle.ForeColor = Color.White;
            button.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            dataGridView1.Columns.Add(button);

            //   dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[12].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[13].Visible = false;


        }
        
        private void llenarColumnaPOMS()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

       

            dataGridView1.Columns.Add("repetiiones", "Tensión-Ansiedad");
            dataGridView1.Columns.Add("saltos", "Depresión-Melancolia");
            dataGridView1.Columns.Add("ccolor", "Angustia-Hostilidad-Cólera");
            dataGridView1.Columns.Add("cdnr", "Vigor-Actividad");
            dataGridView1.Columns.Add("cdr", "Fatiga-Inercia");
            dataGridView1.Columns.Add("cal", "Confusión-Desorientación");
            dataGridView1.Columns.Add("duracion", "Amistosidad");

            dataGridView1.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Detalles";
            button.FlatStyle = FlatStyle.Flat;
            button.DefaultCellStyle.ForeColor = Color.White;
            button.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            dataGridView1.Columns.Add(button);




            dataGridView1.Columns.Add("11", "id");
            dataGridView1.Columns[12].Visible = false;

            dataGridView1.Columns.Add("11", "idTodo");
            dataGridView1.Columns[13].Visible = false;


        }

        private void llenarTablaTRN()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruTRN inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTRN.idTest = SujetosEvaluados.PTRN where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTRN inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTRN.idTest = SujetosEvaluados.PTRN where  PTRN != '' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruTRN inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTRN.idTest = SujetosEvaluados.PTRN where idSujeto='" + comboBox1.SelectedValue + "' and PTRN != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTRN inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTRN.idTest = SujetosEvaluados.PTRN where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTRN != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruTRN inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTRN.idTest = SujetosEvaluados.PTRN where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTRN != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                                      read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                 

                        read.GetValue(read.GetOrdinal("NRepeticiones")),
                        read.GetValue(read.GetOrdinal("NSaltos")),
                        read.GetValue(read.GetOrdinal("Cambcolor")),
                        read.GetValue(read.GetOrdinal("CdNoRect")),
                        read.GetValue(read.GetOrdinal("CdRect")),
                        read.GetValue(read.GetOrdinal("Calificacion")),
                        read.GetValue(read.GetOrdinal("Duracion")),
                       DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                        dataGridView1.Columns[11].HeaderText="Detalles",


                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),


                            });

                            }


                        }
                    }

                }
            }
        }
        
        private void llenarColumnaTabla16Pf()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

           

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Hojas de Perfiles";
            button.FlatStyle = FlatStyle.Flat;
            button.DefaultCellStyle.ForeColor = Color.White;
            button.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            dataGridView1.Columns.Add(button);


            dataGridView1.Columns.Add("Perfil1", "Perfil 1");
            dataGridView1.Columns.Add("Perfil2", "Perfil 2");
            dataGridView1.Columns.Add("Perfil3", "Perfil 3");
            dataGridView1.Columns.Add("Perfil4", "Perfil 4");
            dataGridView1.Columns.Add("Neuroticismo", "Neuroticismo");

            dataGridView1.Columns.Add("AnotBrutaA", "Anotación BrutaA");
            dataGridView1.Columns.Add("AnotBrutaB", "Anotación BrutaB");
            dataGridView1.Columns.Add("AnotBrutaC", "Anotación BrutaC");
            dataGridView1.Columns.Add("AnotBrutaE", "Anotación BrutaE");
            dataGridView1.Columns.Add("AnotBrutaF", "Anotación BrutaF");
            dataGridView1.Columns.Add("AnotBrutaG", "Anotación BrutaG");
            dataGridView1.Columns.Add("AnotBrutaH", "Anotación BrutaH");
            dataGridView1.Columns.Add("AnotBrutaI", "Anotación BrutaI");
            dataGridView1.Columns.Add("AnotBrutaL", "Anotación BrutaL");
            dataGridView1.Columns.Add("AnotBrutaM", "Anotación BrutaM");
            dataGridView1.Columns.Add("AnotBrutaN", "Anotación BrutaN");
            dataGridView1.Columns.Add("AnotBrutaO", "Anotación BrutaO");
            dataGridView1.Columns.Add("AnotBrutaQ1", "Anotación BrutaQ1");
            dataGridView1.Columns.Add("AnotBrutaQ2", "Anotación BrutaQ2");
            dataGridView1.Columns.Add("AnotBrutaQ3", "Anotación BrutaQ3");
            dataGridView1.Columns.Add("AnotBrutaQ4", "Anotación BrutaQ4");


            dataGridView1.Columns.Add("AnotPesadaA", "Anotación PesadaA");
            dataGridView1.Columns.Add("AnotPesadaB", "Anotación PesadaB");
            dataGridView1.Columns.Add("AnotPesadaC", "Anotación PesadaC");
            dataGridView1.Columns.Add("AnotPesadaE", "Anotación PesadaE");
            dataGridView1.Columns.Add("AnotPesadaF", "Anotación PesadaF");
            dataGridView1.Columns.Add("AnotPesadaG", "Anotación PesadaG");
            dataGridView1.Columns.Add("AnotPesadaH", "Anotación PesadaH");
            dataGridView1.Columns.Add("AnotPesadaI", "Anotación PesadaI");
            dataGridView1.Columns.Add("AnotPesadaL", "Anotación PesadaL");
            dataGridView1.Columns.Add("AnotPesadaM", "Anotación PesadaM");
            dataGridView1.Columns.Add("AnotPesadaN", "Anotación PesadaN");
            dataGridView1.Columns.Add("AnotPesadaO", "Anotación PesadaO");
            dataGridView1.Columns.Add("AnotPesadaQ1", "Anotación PesadaQ1");
            dataGridView1.Columns.Add("AnotPesadaQ2", "Anotación PesadaQ2");
            dataGridView1.Columns.Add("AnotPesadaQ3", "Anotación PesadaQ3");
            dataGridView1.Columns.Add("AnotPesadaQ4", "Anotación PesadaQ4");

            dataGridView1.Columns.Add("fecha", "Fecha");



            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[42].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[43].Visible = false;

            dataGridView1.Columns.Add("9d", "idAtleta");
            dataGridView1.Columns[44].Visible = false;


        }

        private void llenarTabla16Pf()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM Pru16pf inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Pru16pf.idTest = SujetosEvaluados.P16pf where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM Pru16pf inner join SujetosEvaluados   inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Pru16pf.idTest = SujetosEvaluados.P16pf where  P16pf != '' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM Pru16pf inner join SujetosEvaluados   inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Pru16pf.idTest = SujetosEvaluados.P16pf where idSujeto='" + comboBox1.SelectedValue + "' and P16pf != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM Pru16pf inner join SujetosEvaluados      inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Pru16pf.idTest = SujetosEvaluados.P16pf where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and P16pf != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM Pru16pf inner join SujetosEvaluados     inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Pru16pf.idTest = SujetosEvaluados.P16pf where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and P16pf != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                    

                       dataGridView1.Columns[3].HeaderText="Hoja de Perfil",

                        read.GetValue(read.GetOrdinal("Perfil1")),
                        read.GetValue(read.GetOrdinal("Perfil2")),
                        read.GetValue(read.GetOrdinal("Perfil3")),
                        read.GetValue(read.GetOrdinal("Perfil4")),
                        read.GetValue(read.GetOrdinal("Neuroticismo")),

                        read.GetValue(read.GetOrdinal("AnotBrutaA")),
                        read.GetValue(read.GetOrdinal("AnotBrutaB")),
                        read.GetValue(read.GetOrdinal("AnotBrutaC")),
                        read.GetValue(read.GetOrdinal("AnotBrutaE")),
                        read.GetValue(read.GetOrdinal("AnotBrutaF")),
                        read.GetValue(read.GetOrdinal("AnotBrutaG")),
                        read.GetValue(read.GetOrdinal("AnotBrutaH")),
                        read.GetValue(read.GetOrdinal("AnotBrutaI")),
                        read.GetValue(read.GetOrdinal("AnotBrutaL")),
                        read.GetValue(read.GetOrdinal("AnotBrutaM")),
                        read.GetValue(read.GetOrdinal("AnotBrutaN")),
                        read.GetValue(read.GetOrdinal("AnotBrutaO")),
                        read.GetValue(read.GetOrdinal("AnotBrutaQ1")),
                        read.GetValue(read.GetOrdinal("AnotBrutaQ2")),
                        read.GetValue(read.GetOrdinal("AnotBrutaQ3")),
                        read.GetValue(read.GetOrdinal("AnotBrutaQ4")),




                        read.GetValue(read.GetOrdinal("AnotPesadaA")),
                        read.GetValue(read.GetOrdinal("AnotPesadaB")),
                        read.GetValue(read.GetOrdinal("AnotPesadaC")),
                        read.GetValue(read.GetOrdinal("AnotPesadaE")),
                        read.GetValue(read.GetOrdinal("AnotPesadaF")),
                        read.GetValue(read.GetOrdinal("AnotPesadaG")),
                        read.GetValue(read.GetOrdinal("AnotPesadaH")),
                        read.GetValue(read.GetOrdinal("AnotPesadaI")),
                        read.GetValue(read.GetOrdinal("AnotPesadaL")),
                        read.GetValue(read.GetOrdinal("AnotPesadaM")),
                        read.GetValue(read.GetOrdinal("AnotPesadaN")),
                        read.GetValue(read.GetOrdinal("AnotPesadaO")),
                        read.GetValue(read.GetOrdinal("AnotPesadaQ1")),
                        read.GetValue(read.GetOrdinal("AnotPesadaQ2")),
                        read.GetValue(read.GetOrdinal("AnotPesadaQ3")),
                        read.GetValue(read.GetOrdinal("AnotPesadaQ4")),


                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),



                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),
                         read.GetValue(read.GetOrdinal("idDatosSujetos")),
                     });

                            }


                        }
                    }

                }
            }
        }
        
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            if (nombrePrueba == "Raven")
                llenarComboAtletaRaven();
            if (nombrePrueba == "Catell")
                llenarComboAtletaCatell();
            if (nombrePrueba == "16PF")
                llenarComboAtleta16Pf();
            if (nombrePrueba == "Weil")
                llenarComboAtletaWeil();
            if (nombrePrueba == "Eysenck")
                llenarComboAtletaEysenck();
            if (nombrePrueba == "Test de Motivos Deportivos de Butt")
                llenarComboAtletaButt();
            if (nombrePrueba == "Dominó")
                llenarComboAtletaDomino();
            if (nombrePrueba == "IDARE (Situacional)")
                llenarComboAtletaIdareSituacional();
            if (nombrePrueba == "IDARE (Rasgo)")
                llenarComboAtletaIdareRasgo();
            if (nombrePrueba == "IDETEM-1")
                llenarComboAtletaIdetem();
            if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                llenarComboAtletaIped();
            if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                llenarComboAtletaCualiVolitivasDep();
            if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                llenarComboAtletaCualiMotivDep();
            if (nombrePrueba == "Actitud ante la Competencia")
                llenarComboAtletaActAnteCompet();
            if (nombrePrueba == "Tiempo de Reacción Simple")
                llenarComboAtletaTRS();
            if (nombrePrueba == "Tiempo de Reacción Complejo")
                llenarComboAtletaTRC();
            if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                llenarComboAtletaTRCS();
            if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                llenarComboAtletaTRA();
            if (nombrePrueba == "Tabla Rojo y Negra")
                llenarComboAtletaTTRN();
            if (nombrePrueba == "POMS")
                llenarComboAtletaPOMS();
            if (nombrePrueba == "Ansiedad Precompetitiva CSAI-2R")
                llenarComboAtletaAnsiedadProComp();



        }

        private void llenarComboAtletaAnsiedadProComp()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaPOMS()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PPoms != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            if (nombrePrueba == "Raven")
                llenarComboDeporteRaven();
            if (nombrePrueba == "Catell")
                llenarComboDeporteCatell();
            if (nombrePrueba == "16PF")
                llenarComboDeporte16Pf();
            if (nombrePrueba == "Weil")
                llenarComboDeporteWeil();
            if (nombrePrueba == "Eysenck")
                llenarComboDeporteEysenck();
            if (nombrePrueba == "Test de Motivos Deportivos de Butt")
                llenarComboDeporteButt();
            if (nombrePrueba == "Dominó")
                llenarComboDeporteDomino();
            if (nombrePrueba == "IDARE (Situacional)")
                llenarComboDeporteIdareSituacional();
            if (nombrePrueba == "IDARE (Rasgo)")
                llenarComboDeporteIdareRasgo();
            if (nombrePrueba == "IDETEM-1")
                llenarComboDeporteIdetem();
            if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                llenarComboDeporteIped();
            if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                llenarComboDeporteCualiVolitivasDep();
            if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                llenarComboDeporteCualiMotivDep();
            if (nombrePrueba == "Actitud ante la Competencia")
                llenarComboDeporteActAnteCompet();
            if (nombrePrueba == "Tiempo de Reacción Simple")
                llenarComboDeporteTRS();
            if (nombrePrueba == "Tiempo de Reacción Complejo")
                llenarComboDeporteTRC();
            if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                llenarComboDeporteTRCS();
            if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                llenarComboDeporteTRA();
            if (nombrePrueba == "Tabla Rojo y Negra")
                llenarComboDeporteTTRN();
            if (nombrePrueba == "Ansiedad Precompetitiva CSAI-2R") 
                llenarComboDeportePreCompe();
            if (nombrePrueba == "POMS")
                llenarComboDeportePOMS();

            comboBox1.Text = "Seleccione";
            comboBox3.Text = "Seleccione";
            comboBox3.Enabled = false;

        }

        private void llenarComboDeportePOMS()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PPoms != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeportePreCompe()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }
     
        private void llenarComboDeporteTTRN()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTRN != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteTRA()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PResanti != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteTRCS()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTrcomples != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteTRC()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTrcomple != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteTRS()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTrsimple != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteActAnteCompet()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PActiAnteComp != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteCualiMotivDep()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PCualidMotivDepor != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteCualiVolitivasDep()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PCualiVolitiv != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }

        }

        private void llenarComboDeporteIped()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIped != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteIdetem()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIdetem != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteIdareRasgo()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIdareRasgo != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteIdareSituacional()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIdareSitua != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteDomino()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PDomino != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteButt()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PMotivDepButt != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteEysenck()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PEysenk != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteWeil()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PWeil != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporte16Pf()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where P16pf != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteCatell()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PCatell != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }

        private void llenarComboDeporteRaven()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Entidad  FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PRaven != ''  and Etapa='" + etapa + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";
                }
            }
        }
        
        private void llenarComboAtletaCatell()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PCatell  != '' and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        internal void LimpiarCampos()
        {
            dataGridView1.Rows.Clear();
            comboBox1.Text = "Seleccione";
            comboBox3.Text = "Seleccione";
            buscarEtapa();
            checkBox1.CheckState = CheckState.Unchecked;
        }

        private void llenarComboAtletaIped()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIped  != '' and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaTRA()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PResanti  != ''  and Etapa='" + etapa + "'  and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaTTRN()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTRN != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaTRCS()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTrcomples  != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaTRC()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT  Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTrcomple  != ''   and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaTRS()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PTrsimple  != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaActAnteCompet()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PActiAnteComp  != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaCualiMotivDep()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PCualidMotivDepor  != ''   and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaCualiVolitivasDep()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PCualiVolitiv != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaIdetem()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIdetem != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaIdareSituacional()
        {

            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIdareSitua != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaIdareRasgo()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PIdareRasgo != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaDomino()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PDomino != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaButt()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PMotivDepButt != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaEysenck()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PEysenk != ''   and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaWeil()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PWeil != ''  and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtletaRaven()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where PRaven != ''   and Etapa='" + etapa + "' and Entidad='" + deporte + "' ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void llenarComboAtleta16Pf()
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.idDatosSujetos=SujetosEvaluados.idSujeto where P16pf != ''   and Etapa='" + etapa + "' and Entidad='" + deporte + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            atleta = comboBox1.SelectedValue.ToString();
            comboBox3.Text = "Seleccione";
            comboBox3.Enabled = true;

            llenarTabla();

        }

        private void llenarComboFechaTRA()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PResanti != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PResanti != ''  and Etapa='" + etapa + "' ";
                }

                if (fecha != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where Fecha='" + fecha + "' and PResanti != ''  and Etapa='" + etapa + "'";
                }


                if (fecha != "" && atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where Fecha='" + fecha + "' and idSujeto='" + atleta + "' and PResanti != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaTRCS()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PTrcomples != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PTrcomples != ''  and Etapa='" + etapa + "'";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaTRC()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PTrcomple != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PTrcomple != '' and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaTRS()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PTrsimple != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PTrsimple != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaActAnteCompet()
        {

            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PActiAnteComp != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PActiAnteComp != '' and Etapa='" + etapa + "' ";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaCualiMotivDep()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where  PCualidMotivDepor != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PCualidMotivDepor != ''  and Etapa='" + etapa + "' ";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaCualiVolitivasDep()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PCualiVolitiv != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PCualiVolitiv != ''  and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaIdetem()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where  PIdetem != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PIdetem != ''  and Etapa='" + etapa + "' ";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaIdareSituacional()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PIdareSitua != ''   and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PIdareSitua != ''  and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }
        
        private void llenarComboFechaIdareRasgo()
        {

            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PIdareRasgo != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PIdareRasgo != ''  and Etapa='" + etapa + "'";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }


        private void llenarComboFechaIdareSitucional()
        {

            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PIdareSitua != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PIdareSitua != ''  and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaDomino()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PDomino != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PDomino != ''   and Etapa='" + etapa + "'";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaButt()
        {

            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PMotivDepButt != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PMotivDepButt != ''  and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaEysenck()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PEysenk != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PEysenk != ''  and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaWeil()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where  PWeil != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PWeil != ''  and Etapa='" + etapa + "' ";
                }


                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaRaven()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where   PRaven != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PRaven != ''  and Etapa='" + etapa + "'";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFecha16Pf()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where  P16pf != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and P16pf != ''  and Etapa='" + etapa + "'";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }

        private void llenarComboFechaIped()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where  PIped != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PIped != ''  and Etapa='" + etapa + "'";
                }



                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }
        
        private void comboBox3_DropDown(object sender, EventArgs e)
        {

            if (nombrePrueba == "Raven")
                llenarComboFechaRaven();
            if (nombrePrueba == "Catell")
                llenarComboFechaCatell();
            if (nombrePrueba == "16PF")
                llenarComboFecha16Pf();
            if (nombrePrueba == "Weil")
                llenarComboFechaWeil();
            if (nombrePrueba == "Eysenck")
                llenarComboFechaEysenck();
            if (nombrePrueba == "Test de Motivos Deportivos de Butt")
                llenarComboFechaButt();
            if (nombrePrueba == "Dominó")
                llenarComboFechaDomino();
            if (nombrePrueba == "IDARE (Situacional)")
                llenarComboFechaIdareSituacional();
            if (nombrePrueba == "IDARE (Rasgo)")
                llenarComboFechaIdareRasgo();
            if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                llenarComboFechaIped();
            if (nombrePrueba == "IDETEM-1")
                llenarComboFechaIdetem();
            if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                llenarComboFechaCualiVolitivasDep();
            if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                llenarComboFechaCualiMotivDep();
            if (nombrePrueba == "Actitud ante la Competencia")
                llenarComboFechaActAnteCompet();
            if (nombrePrueba == "Tiempo de Reacción Simple")
                llenarComboFechaTRS();
            if (nombrePrueba == "Tiempo de Reacción Complejo")
                llenarComboFechaTRC();
            if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                llenarComboFechaTRCS();
            if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                llenarComboFechaTRA();
            if (nombrePrueba == "Tabla Rojo y Negra")
                llenarComboFechaTRN();
            if (nombrePrueba == "Ansiedad Precompetitiva CSAI-2R")
                llenarComboFechaAnsiedadPrecom();
            if (nombrePrueba == "POMS")
                llenarComboFechaPOMS();



        }

        private void llenarComboFechaPOMS()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where   PPoms != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PPoms != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);

                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";// DateTime.Parse("Fecha").ToString("dd/MM/yyyy");
                    comboBox3.ValueMember = "Fecha";

                }
            }
        }
        
        private void llenarComboFechaCatell()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where   PCatell != ''  and Etapa='" + etapa + "' ";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PCatell != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);

                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";// DateTime.Parse("Fecha").ToString("dd/MM/yyyy");
                    comboBox3.ValueMember = "Fecha";

                }
            }
        }

        private void llenarComboFechaTRN()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PTRN != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PTRN != ''  and Etapa='" + etapa + "' ";
                }

                if (fecha != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where Fecha='" + fecha + "' and PTRN != ''  and Etapa='" + etapa + "'";
                }


                if (fecha != "" && atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where Fecha='" + fecha + "' and idSujeto='" + atleta + "' and PTRN != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }
        
        private void llenarComboFechaAnsiedadPrecom()
        {
            using (mainEntities f = new mainEntities())
            {
                String query = "Select * from SujetosEvaluados where PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "'";

                if (atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where idSujeto='" + atleta + "' and PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "' ";
                }

                if (fecha != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where Fecha='" + fecha + "' and PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "'";
                }


                if (fecha != "" && atleta != "")
                {
                    query = "select * FROM DatosSujetos inner join SujetosEvaluados  on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where Fecha='" + fecha + "' and idSujeto='" + atleta + "' and PAnsiedadCompetitiva != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter(query, f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox3.DataSource = data;
                    comboBox3.DisplayMember = "Fecha";
                    comboBox3.ValueMember = "Fecha";
                }
            }
        }
        
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;

        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fecha = comboBox3.SelectedValue.ToString();
            llenarTabla();

        }
        
        public void llenarTabla()
        {

            if (nombrePrueba == "Raven")
                llenarTablaRaven();
            if (nombrePrueba == "Catell")
                llenarTablaCatell();
            if (nombrePrueba == "16PF")
                llenarTabla16Pf();
            if (nombrePrueba == "Weil")
                llenarTablaWeil();
            if (nombrePrueba == "Eysenck")
                llenarTablaEysenck();
            if (nombrePrueba == "Test de Motivos Deportivos de Butt")
                llenarTablaButt();
            if (nombrePrueba == "Dominó")
                llenarTablaDomino();
            if (nombrePrueba == "IDARE (Situacional)")
                llenarTablaIdareSituacional();
            if (nombrePrueba == "IDARE (Rasgo)")
                llenarTablaIdareRasgo();
            if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                llenarTablaIped();
            if (nombrePrueba == "IDETEM-1")
                llenarTablaIdetem();
            if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                llenarTablaCualidadesVolitivas();
            if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                llenarTablaCualidadesMotivacionales();
            if (nombrePrueba == "Actitud ante la Competencia")
                llenarTablaActitudAnteCompe();
            if (nombrePrueba == "Tiempo de Reacción Simple")
                llenarTablaTRS();
            if (nombrePrueba == "Tiempo de Reacción Complejo")
                llenarTablaTRC();
            if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                llenarTablaTRCS();
            if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                llenarTablaRA();
            if (nombrePrueba == "Tabla Rojo y Negra")
                llenarTablaTRN();
            if (nombrePrueba == "POMS")
                llenarTablaPOMS();
            if (nombrePrueba == "Ansiedad Precompetitiva CSAI-2R")
                llenarTablaAnsiedadPreComp();

        }
        
        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Enabled = false;
                comboBox3.Enabled = false;
                comboBox1.Text = "Seleccione";
                comboBox3.Text = "Seleccione";
                comboBox4.Text = "Seleccione";
                comboBox4.Enabled = false;
                llenarTabla();
            }
            else
            {

                comboBox4.Enabled = true;
                dataGridView1.Rows.Clear();
            }
        }
        
        //***********************LLenar Tabla y columnas*********************************

        private void VisualizarPruebasTodas_Load(object sender, EventArgs e)
        {

            buscarEtapa();
            llenarTabla();
        }

        public void llenarColumnaTablaRaven()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

        

            dataGridView1.Columns.Add("PuntajeTotal", "Puntaje total de la Prueba");
            dataGridView1.Columns.Add("PuntajeA", "Puntaje A");
            dataGridView1.Columns.Add("PuntajeB", "Puntaje B");
            dataGridView1.Columns.Add("PuntajeC", "Puntaje C");
            dataGridView1.Columns.Add("PuntajeD", "Puntaje D");
            dataGridView1.Columns.Add("PuntajeE", "Puntaje E");
            dataGridView1.Columns.Add("Porcentaje", "Porcentaje");
            dataGridView1.Columns.Add("Rango", "Rango");
            dataGridView1.Columns.Add("Diagnostico", "Diagnóstico");
            dataGridView1.Columns.Add("Consistencia", "Consistencia");

            dataGridView1.Columns.Add("fecha", "Fecha");


            dataGridView1.Columns.Add("11", "id");
            dataGridView1.Columns[14].Visible = false;

            dataGridView1.Columns.Add("11", "idTodo");
            dataGridView1.Columns[15].Visible = false;
        }

        public void llenarTablaRaven()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();

                String query = "select * FROM PruRaven inner join SujetosEvaluados    inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruRaven.idTest = SujetosEvaluados.PRaven where    Etapa='" + etapa + "'";

                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruRaven inner join SujetosEvaluados    inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruRaven.idTest = SujetosEvaluados.PRaven where   PRaven != ''  and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruRaven inner join SujetosEvaluados    inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruRaven.idTest = SujetosEvaluados.PRaven where idSujeto='" + comboBox1.SelectedValue + "' and PRaven != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruRaven inner join SujetosEvaluados    inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruRaven.idTest = SujetosEvaluados.PRaven where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PRaven != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruRaven inner join SujetosEvaluados   inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruRaven.idTest = SujetosEvaluados.PRaven where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PRaven != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {


                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                       

                        read.GetValue(read.GetOrdinal("PuntajeTotal")),
                        read.GetValue(read.GetOrdinal("PuntajeA")),
                        read.GetValue(read.GetOrdinal("PuntajeB")),
                        read.GetValue(read.GetOrdinal("PuntajeC")),
                        read.GetValue(read.GetOrdinal("PuntajeD")),
                        read.GetValue(read.GetOrdinal("PuntajeE")),
                        read.GetValue(read.GetOrdinal("Porcentaje")),
                        read.GetValue(read.GetOrdinal("Rango")),
                        read.GetValue(read.GetOrdinal("Diagnostico")),
                        read.GetValue(read.GetOrdinal("Consistencia")),
                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),



                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }

                }
            }

        }

        public void llenarColumnaTablaIdareSituacional()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

           

            dataGridView1.Columns.Add("ptoAnsiedadSituacional", "Puntaje Ansiedad Siuacional ");
            dataGridView1.Columns.Add("diagNosticoAnsiedadSituacional", "Diagnostico Ansiedad Situacional");
            dataGridView1.Columns.Add("fecha", "Fecha");
            
            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[6].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[7].Visible = false;

        }

        public void llenarColumnaTablaIdareRasgo()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

      
            
            dataGridView1.Columns.Add("ptoAnsiedadSituacional", "Puntaje Ansiedad Rango ");
            dataGridView1.Columns.Add("diagNosticoAnsiedadSituacional", "Diagnostico Ansiedad Rasgo");
            dataGridView1.Columns.Add("fecha", "Fecha");
            
            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[6].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[7].Visible = false;

        }

        public void llenarTablaIdareSituacional()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruIdareSituacional inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareSituacional.idTest = SujetosEvaluados.PIdareSitua where    Etapa='" + etapa + "'";



                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM PruIdareSituacional inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareSituacional.idTest = SujetosEvaluados.PIdareSitua where   PIdareSitua != '' and Entidad ='" + deporte + "'  and Etapa='" + etapa + "' ";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruIdareSituacional inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareSituacional.idTest = SujetosEvaluados.PIdareSitua where idSujeto='" + comboBox1.SelectedValue + "' and PIdareSitua != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruIdareSituacional inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareSituacional.idTest = SujetosEvaluados.PIdareSitua where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIdareSitua != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruIdareSituacional inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareSituacional.idTest = SujetosEvaluados.PIdareSitua where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIdareSitua != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {


                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                        

                        read.GetValue(read.GetOrdinal("PAnsiedadSituacional")),
                        read.GetValue(read.GetOrdinal("DiagAnsSituacional")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),



                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }

                }
            }

        }


        public void llenarTablaIdareRasgo()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruIdareRago inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareRago.idTest = SujetosEvaluados.PIdareRasgo where  Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruIdareRago inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareRago.idTest = SujetosEvaluados.PIdareRasgo where  PIdareRasgo != ''  and Entidad ='" + deporte + "' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruIdareRago inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareRago.idTest = SujetosEvaluados.PIdareRasgo where idSujeto='" + comboBox1.SelectedValue + "' and PIdareRasgo != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruIdareRago inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareRago.idTest = SujetosEvaluados.PIdareRasgo where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIdareRasgo != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruIdareRago inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruIdareRago.idTest = SujetosEvaluados.PIdareRasgo where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIdareRasgo != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                       

                        read.GetValue(read.GetOrdinal("PAnsiedadRasgo")),
                        read.GetValue(read.GetOrdinal("DiagAnsRasgo")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),
                        
                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }

                }

            }
        }
        
        public void llenarColumnaTablaButt()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

        

            dataGridView1.Columns.Add("conflicto", "Conflicto");
            dataGridView1.Columns.Add("rivalidad", "Rivalidad");
            dataGridView1.Columns.Add("suficiencia", "Suficiencia");
            dataGridView1.Columns.Add("cooperacion", "Cooperación");
            dataGridView1.Columns.Add("agresividad", "Agresividad");
            dataGridView1.Columns.Add("pregunta", "Pregunta");

            dataGridView1.Columns.Add("fecha", "Fecha");



            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[10].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[11].Visible = false;

        }

        public void llenarTablaButt()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM MotivDeporButt inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and MotivDeporButt.idTest = SujetosEvaluados.PMotivDepButt where Etapa='" + etapa + "'";

                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM MotivDeporButt inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and MotivDeporButt.idTest = SujetosEvaluados.PMotivDepButt where   PMotivDepButt != '' and Entidad ='" + deporte + "'  and Etapa='" + etapa + "' ";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM MotivDeporButt inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and MotivDeporButt.idTest = SujetosEvaluados.PMotivDepButt where idSujeto='" + comboBox1.SelectedValue + "' and PMotivDepButt != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM MotivDeporButt inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and MotivDeporButt.idTest = SujetosEvaluados.PMotivDepButt where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PMotivDepButt != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM MotivDeporButt inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and MotivDeporButt.idTest = SujetosEvaluados.PMotivDepButt where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PMotivDepButt != ''   and Etapa='" + etapa + "'";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                     

                        read.GetValue(read.GetOrdinal("Conflicto")),
                         read.GetValue(read.GetOrdinal("Rivalidad")),
                        read.GetValue(read.GetOrdinal("Suficiencia")),
                        read.GetValue(read.GetOrdinal("Cooperacion")),
                        read.GetValue(read.GetOrdinal("Agresividad")),
                         read.GetValue(read.GetOrdinal("Pregunta")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),
                     });

                            }


                        }
                    }

                }
            }

        }
        
        public void llenarColumnaTablaEysenck()
        {
            
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

          

            dataGridView1.Columns.Add("Neuroticismo", "Neuroticismo");
            dataGridView1.Columns.Add("Extroversión", "Extroversión");
            dataGridView1.Columns.Add("Sinceridad", "Sinceridad");
            
            dataGridView1.Columns.Add("neuroti", "Diagnóstico");
            dataGridView1.Columns.Add("diagnosNeuro", "Puntaje Semejantesa a los de");

            dataGridView1.Columns.Add("temperamento", "Temperamento");
            dataGridView1.Columns.Add("caulidades", "Cualidades");
            
            dataGridView1.Columns.Add("fecha", "Fecha");
            
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns[11].Visible = false;

            dataGridView1.Columns.Add("idTodo", "idTodo");
            dataGridView1.Columns[12].Visible = false;

        }

        public void llenarTablaEysenck()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruEysenk inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruEysenk.idTest = SujetosEvaluados.PEysenk where  Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruEysenk inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruEysenk.idTest = SujetosEvaluados.PEysenk where   PEysenk != '' and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruEysenk inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruEysenk.idTest = SujetosEvaluados.PEysenk where idSujeto='" + comboBox1.SelectedValue + "' and PEysenk != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruEysenk inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruEysenk.idTest = SujetosEvaluados.PEysenk where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PEysenk != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruEysenk inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruEysenk.idTest = SujetosEvaluados.PEysenk where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PEysenk != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                String diagnostico = read["DiagNeurotic"].ToString() + "-" + read["DiagExtrove"].ToString();
                                String puntajeSemejante = read["DiagnosticoLetra"].ToString();
                                String cualidades = "";
                                String temperamento = read["DiagCuadrante"].ToString();

                                if (read["DiagCuadrante"].ToString() == "Melancólico")
                                {
                                    cualidades = "Labil,Ansioso,Rígido,Severo,Pesimista,Reservado,Insaciable y Tranquilo";
                                }

                                if (read["DiagCuadrante"].ToString() == "Colérico")
                                {
                                    cualidades = "Susceptible, Agitado,Agresivo,Excitable,Variable,Impulsivo,Optimista y Activo";
                                }

                                if (read["DiagCuadrante"].ToString() == "Flemático")
                                {
                                    cualidades = "Pasivo,Cuidadoso,Pensativo,Apacible,Controlado,Leal,Ecuanime e Imperturbable";
                                }

                                if (read["DiagCuadrante"].ToString() == "Sanguineo")
                                {
                                    cualidades = "Sociable,Expresivo,Locuaz,Sensible,Vivaz,Adaptable,Animado,Despreocupado y Diligible";
                                }


                                dataGridView1.Rows.Add(new object[] {


                         read.GetValue(read.GetOrdinal("NombreS")),
                         read.GetValue(read.GetOrdinal("PrimerApellido")),
                         read.GetValue(read.GetOrdinal("SegundoApellido")),

                        
                         read.GetValue(read.GetOrdinal("Neuroticismo")),
                         read.GetValue(read.GetOrdinal("Extroversion")),
                         read.GetValue(read.GetOrdinal("Sinceridad")),
                              diagnostico,
                              puntajeSemejante,
                              temperamento,
                              cualidades,


                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),
                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),


                     });

                            }


                        }
                    }

                }
            }

        }

        public void llenarColumnaTablaDomino()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

         

            dataGridView1.Columns.Add("puntaje", "Puntaje");
            dataGridView1.Columns.Add("rango", "Rango");
            dataGridView1.Columns.Add("porcentaje", "Porcentaje");
            dataGridView1.Columns.Add("diagnostico", "Diagnóstico");

            dataGridView1.Columns.Add("fecha", "Fecha");



            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[9].Visible = false;

        }

        private void llenarTablaDomino()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruDomino inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruDomino.idTest = SujetosEvaluados.PDomino where Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruDomino inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruDomino.idTest = SujetosEvaluados.PDomino where   PDomino != ''  and Entidad ='" + deporte + "' and Etapa='" + etapa + "'";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruDomino inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruDomino.idTest = SujetosEvaluados.PDomino where idSujeto='" + comboBox1.SelectedValue + "' and PDomino != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruDomino inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruDomino.idTest = SujetosEvaluados.PDomino where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PDomino != ''  and Etapa='" + etapa + "'  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruDomino inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruDomino.idTest = SujetosEvaluados.PDomino where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PDomino != '' and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

            

                        read.GetValue(read.GetOrdinal("Puntaje")),
                        read.GetValue(read.GetOrdinal("Rango")),
                        read.GetValue(read.GetOrdinal("Porcentaje")),
                        read.GetValue(read.GetOrdinal("Diagnostico")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),


                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }
                }
            }
        }
  
        public void llenarColumnaTablaIped()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

            

            dataGridView1.Columns.Add("autoconfianza", "Autoconfianza");
            dataGridView1.Columns.Add("cntoAfrontamN", "Control Afrontamiento Negativo");
            dataGridView1.Columns.Add("cntolAten", "Control Atencional");
            dataGridView1.Columns.Add("cntolVisuo", "Control Visuoimaginativo");
            dataGridView1.Columns.Add("nivMotiv", "Nivel Motivacional");
            dataGridView1.Columns.Add("controlAfroPos", "Control de Afrontamiento Positivo");
            dataGridView1.Columns.Add("cntorAct", "Control Actitudinal");
            dataGridView1.Columns.Add("calfinal", "Calificacíon Final");

            dataGridView1.Columns.Add("fecha", "Fecha");
            
            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[12].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[13].Visible = false;
        }
        
        private void llenarTablaIped()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM Iped inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and Iped.idTest = SujetosEvaluados.PIped where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM Iped inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and Iped.idTest = SujetosEvaluados.PIped where   PIped != ''  and Entidad ='" + deporte + "' and Etapa='" + etapa + "'";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM Iped inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and Iped.idTest = SujetosEvaluados.PIped where idSujeto='" + comboBox1.SelectedValue + "' and PIped != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM Iped inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and Iped.idTest = SujetosEvaluados.PIped where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIped != '' and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM Iped inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and Iped.idTest = SujetosEvaluados.PIped where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIped != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                 

                        read.GetValue(read.GetOrdinal("Autoconfianza")),
                        read.GetValue(read.GetOrdinal("ContAfronNegativ")),
                        read.GetValue(read.GetOrdinal("ContAtencional")),
                        read.GetValue(read.GetOrdinal("ContVisuoimag")),
                        read.GetValue(read.GetOrdinal("NivelMotiv")),
                        read.GetValue(read.GetOrdinal("ContAfrontPositiv")),
                        read.GetValue(read.GetOrdinal("ContActitudinal")),
                        read.GetValue(read.GetOrdinal("calFinal")),


                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),


                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }
                }
            }
        }
        
        public void llenarColumnaTablaWeil()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

 

            dataGridView1.Columns.Add("puntajeTotal", "Puntaje Total");
            dataGridView1.Columns.Add("porcentaje", "Porcentaje");
            dataGridView1.Columns.Add("rango", "Rango");
            dataGridView1.Columns.Add("diagnostico", "Diagnóstico");


            dataGridView1.Columns.Add("fecha", "Fecha");


            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[9].Visible = false;

        }

        private void llenarTablaWeil()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruWeil inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruWeil.idTest = SujetosEvaluados.PWeil where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruWeil inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruWeil.idTest = SujetosEvaluados.PWeil where  PWeil != ''  and Entidad ='" + deporte + "'   and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruWeil inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruWeil.idTest = SujetosEvaluados.PWeil where idSujeto='" + comboBox1.SelectedValue + "' and PWeil != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruWeil inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruWeil.idTest = SujetosEvaluados.PWeil where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PWeil != '' and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruWeil inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruWeil.idTest = SujetosEvaluados.PWeil where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PWeil != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {

                        dataGridView1.Rows.Add(new object[] {
                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

        

                        read.GetValue(read.GetOrdinal("PuntajeTotal")),
                        read.GetValue(read.GetOrdinal("Porcentaje")),
                        read.GetValue(read.GetOrdinal("Rango")),
                        read.GetValue(read.GetOrdinal("Diagnostico")),



                         DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),


                         read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }

                        }
                    }
                }

            }
        }
        
        public void llenarColumnaTablaCaulidadesVolitivas()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

   

            dataGridView1.Columns.Add("puntajeAutoIde", "Autonomía y Dependencia");
            dataGridView1.Columns.Add("tenacidadReso", "Tenacidad y Resolución");
            dataGridView1.Columns.Add("persePersi", "Perseverancia y Persistencia");
            dataGridView1.Columns.Add("autodonAutoCon", "Autodominio y Autocontrol");

            dataGridView1.Columns.Add("ppuntajeAutoIde", "Pto.Autonomía y Dependencia");
            dataGridView1.Columns.Add("ptenacidadReso", "Pto.Tenacidad y Resolución");
            dataGridView1.Columns.Add("ppersePersi", "Pto.Perseverancia y Persistencia");
            dataGridView1.Columns.Add("pautodonAutoCon", "Pto.Autodominio y Autocontrol");
            dataGridView1.Columns.Add("pfalseamiento", "Falseamiento");


            dataGridView1.Columns.Add("fecha", "Fecha");


            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Gráfico";
            button2.FlatStyle = FlatStyle.Flat;
            button2.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            button2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(button2);

            // dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns.Add("9", "id");
            dataGridView1.Columns[14].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[15].Visible = false;

        }

        private void llenarTablaCualidadesVolitivas()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM CualiVolitivasDep inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualiVolitivasDep.idTest = SujetosEvaluados.PCualiVolitiv where  Etapa='" + etapa + "'";

                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM CualiVolitivasDep inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualiVolitivasDep.idTest = SujetosEvaluados.PCualiVolitiv where   PCualiVolitiv != '' and Entidad ='" + deporte + "'  and Etapa='" + etapa + "' ";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM CualiVolitivasDep inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualiVolitivasDep.idTest = SujetosEvaluados.PCualiVolitiv where idSujeto='" + comboBox1.SelectedValue + "' and PCualiVolitiv != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM CualiVolitivasDep inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualiVolitivasDep.idTest = SujetosEvaluados.PCualiVolitiv where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PCualiVolitiv != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM CualiVolitivasDep inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualiVolitivasDep.idTest = SujetosEvaluados.PCualiVolitiv where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PCualiVolitiv != ''  and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {

                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

     
                        read.GetValue(read.GetOrdinal("autoIndepen")),
                        read.GetValue(read.GetOrdinal("tenacidadResol")),
                        read.GetValue(read.GetOrdinal("persePersis")),
                        read.GetValue(read.GetOrdinal("autodAutocon")),


                        read.GetValue(read.GetOrdinal("PtoAutoIndepen")),
                        read.GetValue(read.GetOrdinal("PtoTenacidadResol")),
                        read.GetValue(read.GetOrdinal("PtoPersePersis")),
                        read.GetValue(read.GetOrdinal("PtoAutodAutocon")),

                        read.GetValue(read.GetOrdinal("Falseamiento")),
                        
                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                        dataGridView1.Columns[11].HeaderText="Gráfico",

                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }
                }
            }
        }
        
        public void llenarColumnaTablaCaulidadesMotivacionales()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

     

            dataGridView1.Columns.Add("MotivLogro", "Motivación de Logro");
            dataGridView1.Columns.Add("noMotivLogr", "No Motivación de Logro");
            dataGridView1.Columns.Add("motivIntr", "Motivación Intrinseca");
            dataGridView1.Columns.Add("autodonAutoCon", "Motivación Extrinseca");

            dataGridView1.Columns.Add("ppuntajeAutoIde", "Expectativa de Éxito");
            dataGridView1.Columns.Add("ptenacidadReso", "Expectativa de Eficacia");
            dataGridView1.Columns.Add("ppersePersi", "Motivo por aproximarse al éxito");
            dataGridView1.Columns.Add("motivFrac", "Motivo por evitar el fracaso");
            dataGridView1.Columns.Add("3", "Motivos Materiales");

            dataGridView1.Columns.Add("4", "Motivos de Reconocimiento");
            dataGridView1.Columns.Add("5", "Motivos de Autoafirmación Deportiva");

            dataGridView1.Columns.Add("6", "Motivos de Autoafirmación Personológica");
            dataGridView1.Columns.Add("7", "Motivos Supraindividuales");


            dataGridView1.Columns.Add("fecha", "Fecha");


            dataGridView1.Columns.Add("9f", "id");
            dataGridView1.Columns[17].Visible = false;

            dataGridView1.Columns.Add("9", "idTodo");
            dataGridView1.Columns[18].Visible = false;
        }

        private void llenarTablaCualidadesMotivacionales()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM CualidMotivDeportiv inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualidMotivDeportiv.idTest = SujetosEvaluados.PCualidMotivDepor where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM CualidMotivDeportiv inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualidMotivDeportiv.idTest = SujetosEvaluados.PCualidMotivDepor where   Entidad ='" + deporte + "'   and PCualidMotivDepor != ''  and Etapa='" + etapa + "' ";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM CualidMotivDeportiv inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualidMotivDeportiv.idTest = SujetosEvaluados.PCualidMotivDepor where idSujeto='" + comboBox1.SelectedValue + "' and PCualidMotivDepor != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM CualidMotivDeportiv inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualidMotivDeportiv.idTest = SujetosEvaluados.PCualidMotivDepor where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PCualidMotivDepor != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM CualidMotivDeportiv inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and CualidMotivDeportiv.idTest = SujetosEvaluados.PCualidMotivDepor where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PCualidMotivDepor != ''   and Etapa='" + etapa + "' ";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                

                        read.GetValue(read.GetOrdinal("motivLogro")),
                        read.GetValue(read.GetOrdinal("noMotivLogro")),
                        read.GetValue(read.GetOrdinal("motivIntrínseca")),
                        read.GetValue(read.GetOrdinal("motivExtrínseca")),


                        read.GetValue(read.GetOrdinal("expecExito")),
                        read.GetValue(read.GetOrdinal("expecEficacia")),
                        read.GetValue(read.GetOrdinal("motivAproExito")),
                        read.GetValue(read.GetOrdinal("movEvitarFracaso")),

                        read.GetValue(read.GetOrdinal("motivMater")),
                        read.GetValue(read.GetOrdinal("motivRecono")),
                        read.GetValue(read.GetOrdinal("motivAutoDeportiva")),

                        read.GetValue(read.GetOrdinal("motivAutoPersono")),
                        read.GetValue(read.GetOrdinal("motivSuprain")),


                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),



                     });

                            }


                        }
                    }
                }
            }
        }
        
        public void llenarColumnaTablaActitudAnteCompe()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");

          

            dataGridView1.Columns.Add("certeza", "Certeza");
            dataGridView1.Columns.Add("opinion", "Opinión");
            dataGridView1.Columns.Add("contrario", "Contrario");
            dataGridView1.Columns.Add("significacion", "Significación");


            dataGridView1.Columns.Add("fecha", "Fecha");


            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.Columns.Add("idtodo", "idTodo");
            dataGridView1.Columns[9].Visible = false;

        }

        private void llenarTablaActitudAnteCompe()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM ActitudAnteCompetencia inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and ActitudAnteCompetencia.idTest = SujetosEvaluados.PActiAnteComp where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM ActitudAnteCompetencia inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and ActitudAnteCompetencia.idTest = SujetosEvaluados.PActiAnteComp where   Entidad ='" + deporte + "'   and PActiAnteComp != ''  and Etapa='" + etapa + "' ";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM ActitudAnteCompetencia inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and ActitudAnteCompetencia.idTest = SujetosEvaluados.PActiAnteComp where idSujeto='" + comboBox1.SelectedValue + "' and PActiAnteComp != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM ActitudAnteCompetencia inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and ActitudAnteCompetencia.idTest = SujetosEvaluados.PActiAnteComp where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PActiAnteComp != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM ActitudAnteCompetencia inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and ActitudAnteCompetencia.idTest = SujetosEvaluados.PActiAnteComp where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PActiAnteComp != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),


                        read.GetValue(read.GetOrdinal("Certeza")),
                        read.GetValue(read.GetOrdinal("Opinion")),
                        read.GetValue(read.GetOrdinal("Contrario")),
                        read.GetValue(read.GetOrdinal("Significacion")),


                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),


                        read.GetValue(read.GetOrdinal("idTest")),
                        read.GetValue(read.GetOrdinal("idSujetoEvaluado")),
                     });

                            }

                        }
                    }
                }

            }
        }

       

        public void llenarColumnaTablaTRS()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");



            dataGridView1.Columns.Add("vMaximo", "Valor Máximo");
            dataGridView1.Columns.Add("vMinimo", "Valor Mínimo");
            dataGridView1.Columns.Add("catResCorr", "Cant. Resp. Correctas");
            dataGridView1.Columns.Add("media", "Media");
            dataGridView1.Columns.Add("devStandart", "Desv. Estándart");
            dataGridView1.Columns.Add("coefVar", "Coeficiente Variación");
            dataGridView1.Columns.Add("cantOmi", "Cant.Omisiones");
            dataGridView1.Columns.Add("cantEst", "Cant.Resp Adelantadas");
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns.Add("estimulo", "Color Estímulo");

            dataGridView1.Columns.Add("8", "Fecha");

            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Detalles";
            button2.FlatStyle = FlatStyle.Flat;
            button2.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            button2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(button2);
            //  dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns.Add("idTest", "IdTest");
            dataGridView1.Columns[14].Visible = false;

            dataGridView1.Columns.Add("idTestodo", "IdTestodo");
            dataGridView1.Columns[15].Visible = false;

            




        }
        
        private void llenarTablaTRS()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();
                String query = "select * FROM PruTrsimple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrsimple.idTest = SujetosEvaluados.PTrsimple where    Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTrsimple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrsimple.idTest = SujetosEvaluados.PTrsimple where Entidad ='" + deporte + "' and PTrsimple != ''   and Etapa='" + etapa + "'";
                }


                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruTrsimple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrsimple.idTest = SujetosEvaluados.PTrsimple where idSujeto='" + comboBox1.SelectedValue + "' and PTrsimple != ''   and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTrsimple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrsimple.idTest = SujetosEvaluados.PTrsimple where  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTrsimple != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruTrsimple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrsimple.idTest = SujetosEvaluados.PTrsimple where idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTrsimple != ''  and Etapa='" + etapa + "'";
                }

                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),


                        read.GetValue(read.GetOrdinal("TiempoMaximo")),
                        read.GetValue(read.GetOrdinal("TiempoMinimo")),
                        read.GetValue(read.GetOrdinal("RespCorrecta")),
                        read.GetValue(read.GetOrdinal("TiempoMedio")),
                        read.GetValue(read.GetOrdinal("DesvStandar")),
                        read.GetValue(read.GetOrdinal("CoefVariacion")),

                        read.GetValue(read.GetOrdinal("CantOmisiones")),
                        read.GetValue(read.GetOrdinal("CantAdel")),
                        read.GetValue(read.GetOrdinal("TipoEstimulo")),


                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                         dataGridView1.Columns[13].HeaderText="Detalles",


                         read.GetValue(read.GetOrdinal("idTest")),
                          read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                         
                     });

                            }


                        }
                    }
                }
            }
        }

       

        public void llenarColumnaTablaTRC()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");



            dataGridView1.Columns.Add("vMaximo", "Valor Máximo");
            dataGridView1.Columns.Add("vMinimo", "Valor Mínimo");
            dataGridView1.Columns.Add("catResCorr", "Cant. Resp. Correctas");
            dataGridView1.Columns.Add("media", "Sum. Tiempos");
            dataGridView1.Columns.Add("media", "Media");
            dataGridView1.Columns.Add("devStandart", "Desv. Estándart");
            dataGridView1.Columns.Add("coefVar", "Coeficiente Variación");
            dataGridView1.Columns.Add("cantOmi", "Cant.Omisiones");
            // dataGridView1.Columns.Add("cantEst", "Cant. Errores");

            dataGridView1.Columns.Add("fecha", "Fecha");

            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Detalles";
            button2.FlatStyle = FlatStyle.Flat;
            button2.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            button2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(button2);

            //  dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridView1.Columns.Add("idTest", "IdTest");
            dataGridView1.Columns[13].Visible = false;

            dataGridView1.Columns.Add("IdTestodo", "IdTestodo");
            dataGridView1.Columns[14].Visible = false;
            
           
            
         
        }



        private void llenarTablaTRC()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();

                String query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTrcomple.idTest = SujetosEvaluados.PTrcomple where  Sonido='No'  and Etapa='" + etapa + "'";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTrcomple.idTest = SujetosEvaluados.PTrcomple where Sonido='No' and  Entidad ='" + deporte + "' and PTrcomple != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTrcomple.idTest = SujetosEvaluados.PTrcomple where Sonido='No' and idSujeto='" + comboBox1.SelectedValue + "' and PTrcomple != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTrcomple.idTest = SujetosEvaluados.PTrcomple where  Sonido='No' and  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTrcomple != ''  and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruTrcomple.idTest = SujetosEvaluados.PTrcomple where   Sonido='No' and idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTrcomple != ''  and Etapa='" + etapa + "'";
                }



                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),


                        read.GetValue(read.GetOrdinal("TiempoMaximo")),
                        read.GetValue(read.GetOrdinal("TiempoMinimo")),
                        read.GetValue(read.GetOrdinal("RespCorrecta")),
                        read.GetValue(read.GetOrdinal("SumTiempo")),

                        read.GetValue(read.GetOrdinal("TiempoMedio")),
                        read.GetValue(read.GetOrdinal("DesvStandar")),
                        read.GetValue(read.GetOrdinal("CoefVariacion")),

                        read.GetValue(read.GetOrdinal("CantOmisiones")),
                      //  read.GetValue(read.GetOrdinal("Canterrores")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),
                        dataGridView1.Columns[12].HeaderText="Detalles",




                          read.GetValue(read.GetOrdinal("idTest")),
                          read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                        



                     });

                            }

                        }
                    }
                }

            }
        }
        
        public void llenarColumnaTablaTRCS()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");


            dataGridView1.Columns.Add("vMaximo", "Valor Máximo");
            dataGridView1.Columns.Add("vMinimo", "Valor Mínimo");
            dataGridView1.Columns.Add("catResCorr", "Cant. Resp. Correctas");
            dataGridView1.Columns.Add("media", "Sum. Tiempos");
            dataGridView1.Columns.Add("media", "Media");
            dataGridView1.Columns.Add("devStandart", "Desv. Estándart");
            dataGridView1.Columns.Add("coefVar", "Coeficiente Variación");
            dataGridView1.Columns.Add("cantOmi", "Cant.Omisiones");
            
            dataGridView1.Columns.Add("fecha", "Fecha");
            //   dataGridView1.Columns.Add("verEstimulos", "Más Detalles");

            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Detalles";
            button2.FlatStyle = FlatStyle.Flat;
            button2.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            button2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(button2);

            // dataGridView1.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridView1.Columns.Add("idTest", "IdTest");
            dataGridView1.Columns[13].Visible = false;

            dataGridView1.Columns.Add("IdTestodo", "IdTestodo");
            dataGridView1.Columns[14].Visible = false;


        }

        private void llenarTablaTRCS()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();


                String query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrcomple.idTest = SujetosEvaluados.PTrcomples where  Sonido='Si'  and Etapa='" + etapa + "' ";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrcomple.idTest = SujetosEvaluados.PTrcomples where Sonido='Si' and   Entidad ='" + deporte + "' and  PTrcomples != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrcomple.idTest = SujetosEvaluados.PTrcomples where Sonido='Si' and idSujeto='" + comboBox1.SelectedValue + "' and PTrcomples != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrcomple.idTest = SujetosEvaluados.PTrcomples where  Sonido='Si' and  SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTrcomples != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruTrcomple inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and PruTrcomple.idTest = SujetosEvaluados.PTrcomples where   Sonido='Si' and  idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PTrcomples != ''  and Etapa='" + etapa + "' ";
                }



                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {

                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                      

                        read.GetValue(read.GetOrdinal("TiempoMaximo")),
                        read.GetValue(read.GetOrdinal("TiempoMinimo")),
                        read.GetValue(read.GetOrdinal("RespCorrecta")),
                        read.GetValue(read.GetOrdinal("SumTiempo")),

                        read.GetValue(read.GetOrdinal("TiempoMedio")),
                        read.GetValue(read.GetOrdinal("DesvStandar")),
                        read.GetValue(read.GetOrdinal("CoefVariacion")),

                        read.GetValue(read.GetOrdinal("CantOmisiones")),
                       // read.GetValue(read.GetOrdinal("Canterrores")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                        dataGridView1.Columns[11].HeaderText="Detalles",


                        read.GetValue(read.GetOrdinal("idTest")),
                          read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                          

                     });

                            }


                        }
                    }
                }
            }
        }
        
        public void llenarColumnaTablaRA()
        {

            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");


            dataGridView1.Columns.Add("programa", "Programa");
            dataGridView1.Columns.Add("vMaximo", "Media");
            dataGridView1.Columns.Add("vMinimo", "Mediana");
            dataGridView1.Columns.Add("moda", "Moda");
            dataGridView1.Columns.Add("devStandart", "Desv. Estándart");
            dataGridView1.Columns.Add("coefVar", "Coeficiente Variación");
            dataGridView1.Columns.Add("coefVar", "Calificación");

            dataGridView1.Columns.Add("fecha", "Fecha");



            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Detalles";
            button2.FlatStyle = FlatStyle.Flat;
            button2.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            button2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(button2);

            //   dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            dataGridView1.Columns.Add("idTest", "IdTest");
            dataGridView1.Columns[12].Visible = false;

            dataGridView1.Columns.Add("idTestodo", "IdTodo");
            dataGridView1.Columns[13].Visible = false;

        }
        
        private void llenarTablaRA()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();


                String query = "select * FROM PruResanti inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruResanti.idTest = SujetosEvaluados.PResanti where   Etapa='" + etapa + "' ";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM PruResanti inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruResanti.idTest = SujetosEvaluados.PResanti where   Entidad ='" + deporte + "' and PResanti != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM PruResanti inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruResanti.idTest = SujetosEvaluados.PResanti where idSujeto='" + comboBox1.SelectedValue + "' and PResanti != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM PruResanti inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruResanti.idTest = SujetosEvaluados.PResanti where   SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PResanti != ''   and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM PruResanti inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  PruResanti.idTest = SujetosEvaluados.PResanti where     idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PResanti != ''  and Etapa='" + etapa + "' ";
                }



                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {


                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                 

                        read.GetValue(read.GetOrdinal("Programa")),
                        read.GetValue(read.GetOrdinal("Media")),
                        read.GetValue(read.GetOrdinal("Mediana")),
                        read.GetValue(read.GetOrdinal("Moda")),

                        read.GetValue(read.GetOrdinal("DesvStandar")),
                        read.GetValue(read.GetOrdinal("CoefVar")),
                        read.GetValue(read.GetOrdinal("Calificacion")),

                        DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),

                         dataGridView1.Columns[11].HeaderText="Detalles",


                        read.GetValue(read.GetOrdinal("idTest")),
                         read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }


                        }
                    }
                }
            }
        }
        
        public void llenarColumnaTablaIdetem()
        {
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("1apellido", "Primer Apellido");
            dataGridView1.Columns.Add("2apellido", "Segundo Apellido");


            dataGridView1.Columns.Add("vMaximo", "Sanguineo");
            dataGridView1.Columns.Add("vMinimo", "Colérico");
            dataGridView1.Columns.Add("catResCorr", "Flemático");
            dataGridView1.Columns.Add("media", "Equilibrio");

            dataGridView1.Columns.Add("vMaximoS", "% Sanguineo");
            dataGridView1.Columns.Add("vMinimoC", "% Colérico");
            dataGridView1.Columns.Add("catResCorrF", "% Flemático");
            dataGridView1.Columns.Add("mediaE", "% Equilibrio");


            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Sistema Nervioso";
            button.FlatStyle = FlatStyle.Flat;
            button.DefaultCellStyle.ForeColor = Color.White;
            button.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            dataGridView1.Columns.Add(button);


            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Prop.Psicodinámicas";
            button2.FlatStyle = FlatStyle.Flat;
            button2.DefaultCellStyle.BackColor = Color.FromArgb(55, 79, 105);
            button2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(button2);

            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dataGridView1.Columns.Add("fecha", "Fecha");

            dataGridView1.Columns.Add("idtest", "idTest");
            dataGridView1.Columns[13].Visible = false;

            dataGridView1.Columns.Add("id", "idTodo");
            dataGridView1.Columns[14].Visible = false;

        }

        public void cambiarEtapa()
        {
            buscarEtapa();
            comboBox1.Text = "Seleccione";
            comboBox3.Text = "Seleccione";
            comboBox3.Enabled = false;
            dataGridView1.Rows.Clear();


        }
        
        private void llenarTablaIdetem()
        {
            using (mainEntities f = new mainEntities())
            {
                dataGridView1.Rows.Clear();


                String query = "select * FROM Idetem inner join SujetosEvaluados inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Idetem.idTest = SujetosEvaluados.PIdetem   where   Etapa='" + etapa + "'  ";


                if (comboBox4.Text != "Seleccione" && comboBox3.Text == "Seleccione" && comboBox1.Text == "Seleccione")
                {

                    query = "select * FROM Idetem inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Idetem.idTest = SujetosEvaluados.PIdetem where   PIdetem != ''   and Entidad ='" + deporte + "' and Etapa='" + etapa + "' ";
                }

                if (comboBox1.Text != "Seleccione" && comboBox3.Text == "Seleccione")
                {
                    query = "select * FROM Idetem inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Idetem.idTest = SujetosEvaluados.PIdetem where  idSujeto='" + comboBox1.SelectedValue + "' and PIdetem != ''  and Etapa='" + etapa + "' ";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text == "Seleccione")
                {
                    query = "select * FROM Idetem inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Idetem.idTest = SujetosEvaluados.PIdetem where    SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIdetem != ''   and Etapa='" + etapa + "'";
                }

                if (comboBox3.Text != "Seleccione" && comboBox1.Text != "Seleccione")
                {
                    query = "select * FROM Idetem inner join SujetosEvaluados  inner join DatosSujetos  on DatosSujetos.IdDatosSujetos= SujetosEvaluados.idSujeto and  Idetem.idTest = SujetosEvaluados.PIdetem where    idSujeto='" + comboBox1.SelectedValue + "' and SujetosEvaluados.Fecha='" + comboBox3.SelectedValue + "' and PIdetem != ''  and Etapa='" + etapa + "' ";
                }



                using (SQLiteConnection c = new SQLiteConnection(f.Database.Connection.ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(query, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dataGridView1.Rows.Add(new object[] {


                        read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),

                     

                        read.GetValue(read.GetOrdinal("sanguineo")),
                        read.GetValue(read.GetOrdinal("colerico")),
                        read.GetValue(read.GetOrdinal("flematico")),
                        read.GetValue(read.GetOrdinal("melancolico")),


                         read.GetValue(read.GetOrdinal("porcientoSanguineo")),
                        read.GetValue(read.GetOrdinal("porcientoColerico")),
                        read.GetValue(read.GetOrdinal("porcientoFlematico")),
                        read.GetValue(read.GetOrdinal("porcientoMelancolico")),



                          dataGridView1.Columns[11].HeaderText="Sistema Nervioso",
                          dataGridView1.Columns[12].HeaderText="Prop.Psicodinámicas",
                         DateTime.Parse( read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),



                          read.GetValue(read.GetOrdinal("idTest")),
                          read.GetValue(read.GetOrdinal("idSujetoEvaluado")),

                     });

                            }

                            dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                            dataGridView1.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                        }
                    }

                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            String nombre = "";
            Application.DoEvents();
            Esperar rt = new Esperar();
            rt.Show();

            if (e.RowIndex >= 0)
            {
                nombre = dataGridView1.Rows[e.RowIndex].Cells[0].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[1].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            }

            if (nombrePrueba == "IDETEM-1" && e.RowIndex >= 0 && (e.ColumnIndex == 11 || e.ColumnIndex == 12))
            {

                string prop = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string idIdetemTest = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                IdetemProp ide = new IdetemProp(prop, idIdetemTest);
                rt.Close();
                ide.ShowDialog();

            }

            if (nombrePrueba == "16PF" && e.RowIndex >= 0 && e.ColumnIndex == 3)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[42].Value.ToString();
                int idAtl = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[44].Value.ToString());

                _16PfFormView ide = new _16PfFormView(id, nombre, true, idAtl);
                rt.Close();
                ide.ShowDialog();

            }


            if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido" && e.RowIndex >= 0 && e.ColumnIndex == 12)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                TRC_Form3 ide = new TRC_Form3(nombre, id, true, null, null);
                rt.Close();
                ide.ShowDialog();

            }

            if (nombrePrueba == "Tiempo de Reacción Complejo" && e.RowIndex >= 0 && e.ColumnIndex == 12)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                TRC_Form3 ide = new TRC_Form3(nombre, id, false, null, null);
                rt.Close();
                ide.ShowDialog();

            }


            if (nombrePrueba == "Tiempo de Reacción Simple" && e.RowIndex >= 0 && e.ColumnIndex == 13)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                TRS_Form3 ide = new TRS_Form3(nombre, id, false, null, null);
                rt.Close();
                ide.ShowDialog();

            }


            if (nombrePrueba == "Tiempo de Respuesta Anticipada" && e.RowIndex >= 0 && e.ColumnIndex == 11)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                RA_Form3 ide = new RA_Form3(nombre, id, null, null, null);
                rt.Close();
                ide.ShowDialog();

            }

            if (nombrePrueba == "Tabla Rojo y Negra" && e.RowIndex >= 0 && e.ColumnIndex == 11)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                TRN_Form2 ide = new TRN_Form2(nombre, id, false, null, null);
                rt.Close();
                ide.ShowDialog();

            }

            if (nombrePrueba == "Catell" && e.RowIndex >= 0 && e.ColumnIndex == 7)
            {

                string id = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                CatellDetails ide = new CatellDetails(id);
                rt.Close();
                ide.ShowDialog();

            }


            if (nombrePrueba == "Cualidades Volitivas en el Deporte" && e.RowIndex >= 0 && e.ColumnIndex == 13)
            {

                String PtoAutoIndepen = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                String PtoTenacidadResol = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                String PtoPersePersis = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                String PtoAutodAutocon = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();

                GraficoVolitiv dsd = new GraficoVolitiv(PtoAutoIndepen, PtoTenacidadResol, PtoPersePersis, PtoAutodAutocon, nombre);
                rt.Close();
                dsd.ShowDialog();

            }


            if (nombrePrueba == "POMS" && e.RowIndex >= 0 && e.ColumnIndex == 11)
            {

                String nombreAtleta = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                PruPoms po = new PruPoms();
                po.TensionAnsiedad = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                po.DepresionMelancolia = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                po.AngustiaHostilidad = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                po.VigorActividad = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                po.FatigaInercia = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                po.ConfusionDesorient = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                GraficoPoms graficoPoms = new GraficoPoms(po, nombreAtleta);
                graficoPoms.ShowDialog();

            }

            if (e.RowIndex >= 0)
            {
                if (nombrePrueba == "16PF")
                {
                    idTestPrueba = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 3].Value.ToString();
                    idTestTodaPrueba = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 2].Value.ToString();

                    rt.Close();
                }
                else
                {

                    idTestPrueba = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 2].Value.ToString();
                    idTestTodaPrueba = dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 1].Value.ToString();
                    dataGridView1.TabStop = false;
                    rt.Close();
                }
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
                                etapa = res["idEtapa"].ToString();
                                comboBox2.Text = res["Etapa"].ToString();

                            }
                        }
                    }
                }


            }

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            etapa = comboBox2.SelectedValue.ToString();
            if (checkBox1.Checked)
            {

                llenarTabla();

            }
            else
            {
                comboBox1.Text = "Seleccione";
                comboBox3.Text = "Seleccione";

                dataGridView1.Rows.Clear();
                llenarTabla();
            }

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {

            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * from Tipoetapa", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox2.DataSource = data;
                    comboBox2.DisplayMember = "Etapa";
                    comboBox2.ValueMember = "idEtapa";

                }

            }
        }

        public void LimpiarTodo()
        {
            dataGridView1.Rows.Clear();
            comboBox1.Text = "Seleccione";
            comboBox2.Text = "Seleccione";
            comboBox3.Text = "Seleccione";
            comboBox3.Text = etapa;
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (mainEntities db = new mainEntities())
                {
                    int id = Convert.ToInt32(idTestTodaPrueba);
                    SujetosEvaluados res = db.SujetosEvaluados.Where(s => s.idSujetoEvaluado == id).FirstOrDefault<SujetosEvaluados>();


                    if (nombrePrueba == "Raven")
                    {
                        res.PRaven = null;


                    }

                    if (nombrePrueba == "Eysenck")
                    {

                        res.PEysenk = null;

                    }

                    if (nombrePrueba == "Weil")
                    {
                        res.PWeil = null;


                    }

                    if (nombrePrueba == "Dominó")
                    {

                        res.PDomino = null;


                    }

                    if (nombrePrueba == "IDARE (Rasgo)")
                    {
                        res.PIdareRasgo = null;



                    }

                    if (nombrePrueba == "IDARE (Situacional)")
                    {

                        res.PIdareSitua = null;


                    }

                    if (nombrePrueba == "Catell")
                    {
                        res.PCatell = null;



                    }

                    if (nombrePrueba == "16PF")
                    {
                        res.P16pf = null;


                    }

                    if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                    {
                        res.PCualiVolitiv = null;


                    }

                    if (nombrePrueba == "Test de Motivos Deportivos de Butt")
                    {
                        res.PMotivDepButt = null;



                    }

                    if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                    {
                        res.PCualidMotivDepor = null;

                    }

                    if (nombrePrueba == "Actitud ante la Competencia")
                    {
                        res.PActiAnteComp = null;



                    }

                    if (nombrePrueba == "Tiempo de Reacción Simple")
                    {
                        res.PTrsimple = null;


                    }

                    if (nombrePrueba == "Tiempo de Reacción Complejo")
                    {
                        res.PTrcomple = null;


                    }

                    if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                    {
                        res.PTrcomples = null;


                    }

                    if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                    {
                        res.PResanti = null;


                    }

                    if (nombrePrueba == "IDETEM-1")
                    {
                        res.PIdetem = null;

                    }

                    if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                    {
                        res.PIped = null;
                    }

                    if (nombrePrueba == "Tabla Rojo y Negra")
                    {
                        res.PruTRN = null;
                    }

                    if (nombrePrueba == "POMS")
                    {
                        res.PruTRN = null;
                    }

                    if (nombrePrueba == "Ansiedad Precompetitiva CSAI-2R")
                    {
                        res.PAnsiedadCompetitiva = null;
                    }

                    if (res.PruTRN == null && res.PruTrsimple == null && res.PIped == null && res.PIdetem == null && res.PResanti == null && res.PTrcomples == null && res.PTrcomple == null && res.PRaven == null
                             && res.PEysenk == null && res.PWeil == null && res.PDomino == null && res.PIdareRasgo == null && res.PIdareSitua == null && res.PCatell == null
                             && res.P16pf == null && res.PCualiVolitiv == null && res.PMotivDepButt == null && res.PCualidMotivDepor == null && res.PActiAnteComp == null && res.PPoms == null && res.PAnsiedadCompetitiva == null
                             )
                    {
                        db.SujetosEvaluados.Remove(res);
                    }

                    db.SaveChangesAsync();



                    if (nombrePrueba == "Raven")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruRaven where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (nombrePrueba == "Eysenck")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruEysenk where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Weil")
                    {

                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruWeil where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (nombrePrueba == "Dominó")
                    {

                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruDomino where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "IDARE (Rasgo)")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruIdareRago where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (nombrePrueba == "IDARE (Situacional)")
                    {



                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruIdareSituacional where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Catell")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruCatell where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (nombrePrueba == "16PF")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from Pru16pf where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from CualiVolitivasDep where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Test de Motivos Deportivos de Butt")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from MotivDeporButt where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }

                    }

                    if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from CualidMotivDeportiv where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Actitud ante la Competencia")
                    {



                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from ActitudAnteCompetencia where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Tiempo de Reacción Simple")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrsimple where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Tiempo de Reacción Complejo")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrcomple where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrcomple where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTrcomple where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }



                    if (nombrePrueba == "Tabla Rojo y Negra")
                    {


                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from PruTRN where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }


                    if (nombrePrueba == "IDETEM-1")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from Idetem where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from Iped where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }

                    if (nombrePrueba == "Ansiedad Precompetitiva CSAI-2R")
                    {
                        using (SQLiteConnection c = new SQLiteConnection(db.Database.Connection.ConnectionString))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("Delete from AnsiedadCompetitiva where idTest='" + idTestPrueba + "'", c))
                            {
                                comm1.ExecuteNonQuery();

                            }

                        }
                    }



                    llenarTabla();
                    MessageBox.Show("Se ha eliminado la prueba", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            bool res = false;
            if (dataGridView1.Rows.Count > 0)
            {
                if (nombrePrueba == "IDETEM-1")
                {
                    ExportToExcelIdetem();
                    res = true;
                }

                if (nombrePrueba == "Catell")
                {
                    ExportToExcelCatell();
                    res = true;
                }

                if (nombrePrueba == "16PF")
                {
                    ExportToExcel16Pf();
                    res = true;
                }

                if (nombrePrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)")
                {
                    ExportToExcelIPED();
                    res = true;
                }

                if (nombrePrueba == "Cualidades Volitivas en el Deporte")
                {
                    ExportToExcelCualidVolitivDepor();
                    res = true;
                }

                if (nombrePrueba == "Cualidades Motivacionales Deportivas")
                {
                    ExportToExcelCualidMotivacioDep();
                    res = true;
                }


                if (nombrePrueba == "Tiempo de Reacción Simple")
                {
                    ExportToExcelTiempoReaccSimple();
                    res = true;
                }

                if (nombrePrueba == "Tiempo de Reacción Complejo")
                {
                    ExportToExcelTiempoReacCompleSinSonido();
                    res = true;
                }

                if (nombrePrueba == "Tiempo de Reacción Complejo con Sonido")
                {
                    ExportToExcelTiempoReacCompleConSonido();
                    res = true;
                }

                if (nombrePrueba == "Tiempo de Respuesta Anticipada")
                {
                    ExportToExcelTiempoRespAnti();
                    res = true;
                }

                if (nombrePrueba == "Tabla Rojo y Negra")
                {
                    ExportToExcelTRN();
                    res = true;
                }









                if (!res)
                    ExportToExcel();


            }
            else
            {
                MessageBox.Show("Deben existir datos en la tabla", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExportToExcelTRN()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  Tiempo de reacción complejo 
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    //worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Detalles" && headerText != "id" && headerText != "idTodo")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();

                            if (tempVal != "Detalles")
                            {
                                if (columna != 12 && columna != 13 && columna != 14)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;
                            }




                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);

            }
        }

        private void ExportToExcelTiempoRespAnti()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  Tiempo de reacción complejo 
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    //worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Detalles" && headerText != "IdTest" && headerText != "IdTestodo")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();

                            if (tempVal != "Detalles")
                            {
                                if (columna != 13 && columna != 14)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;
                            }




                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);

            }
        }

        private void ExportToExcelTiempoReacCompleConSonido()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    //worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Detalles" && headerText != "IdTest" && headerText != "IdTestodo")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();

                            if (tempVal != "Detalles")
                            {
                               

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                else if (dataGridView1.Columns[j].HeaderText == "Estimulo1" || dataGridView1.Columns[j].HeaderText == "Respuesta1" || dataGridView1.Columns[j].HeaderText == "Tiempo1"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo2" || dataGridView1.Columns[j].HeaderText == "Respuesta2" || dataGridView1.Columns[j].HeaderText == "Tiempo2"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo3" || dataGridView1.Columns[j].HeaderText == "Respuesta3" || dataGridView1.Columns[j].HeaderText == "Tiempo3"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo4" || dataGridView1.Columns[j].HeaderText == "Respuesta4" || dataGridView1.Columns[j].HeaderText == "Tiempo4"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo5" || dataGridView1.Columns[j].HeaderText == "Respuesta5" || dataGridView1.Columns[j].HeaderText == "Tiempo5"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo6" || dataGridView1.Columns[j].HeaderText == "Respuesta6" || dataGridView1.Columns[j].HeaderText == "Tiempo6"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo7" || dataGridView1.Columns[j].HeaderText == "Respuesta7" || dataGridView1.Columns[j].HeaderText == "Tiempo7"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo8" || dataGridView1.Columns[j].HeaderText == "Respuesta8" || dataGridView1.Columns[j].HeaderText == "Tiempo8"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo9" || dataGridView1.Columns[j].HeaderText == "Respuesta9" || dataGridView1.Columns[j].HeaderText == "Tiempo9"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo10" || dataGridView1.Columns[j].HeaderText == "Respuesta10" || dataGridView1.Columns[j].HeaderText == "Tiempo10"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo11" || dataGridView1.Columns[j].HeaderText == "Respuesta11" || dataGridView1.Columns[j].HeaderText == "Tiempo11"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo12" || dataGridView1.Columns[j].HeaderText == "Respuesta12" || dataGridView1.Columns[j].HeaderText == "Tiempo12"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo13" || dataGridView1.Columns[j].HeaderText == "Respuesta13" || dataGridView1.Columns[j].HeaderText == "Tiempo13"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo14" || dataGridView1.Columns[j].HeaderText == "Respuesta14" || dataGridView1.Columns[j].HeaderText == "Tiempo14"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo15" || dataGridView1.Columns[j].HeaderText == "Respuesta15" || dataGridView1.Columns[j].HeaderText == "Tiempo15"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo16" || dataGridView1.Columns[j].HeaderText == "Respuesta16" || dataGridView1.Columns[j].HeaderText == "Tiempo16"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo17" || dataGridView1.Columns[j].HeaderText == "Respuesta17" || dataGridView1.Columns[j].HeaderText == "Tiempo17"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo18" || dataGridView1.Columns[j].HeaderText == "Respuesta18" || dataGridView1.Columns[j].HeaderText == "Tiempo18"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo19" || dataGridView1.Columns[j].HeaderText == "Respuesta19" || dataGridView1.Columns[j].HeaderText == "Tiempo19"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo20" || dataGridView1.Columns[j].HeaderText == "Respuesta20" || dataGridView1.Columns[j].HeaderText == "Tiempo20"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo21" || dataGridView1.Columns[j].HeaderText == "Respuesta21" || dataGridView1.Columns[j].HeaderText == "Tiempo21"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo22" || dataGridView1.Columns[j].HeaderText == "Respuesta22" || dataGridView1.Columns[j].HeaderText == "Tiempo22"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo23" || dataGridView1.Columns[j].HeaderText == "Respuesta23" || dataGridView1.Columns[j].HeaderText == "Tiempo23"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo24" || dataGridView1.Columns[j].HeaderText == "Respuesta24" || dataGridView1.Columns[j].HeaderText == "Tiempo24"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo25" || dataGridView1.Columns[j].HeaderText == "Respuesta25" || dataGridView1.Columns[j].HeaderText == "Tiempo25"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo26" || dataGridView1.Columns[j].HeaderText == "Respuesta26" || dataGridView1.Columns[j].HeaderText == "Tiempo26"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo27" || dataGridView1.Columns[j].HeaderText == "Respuesta27" || dataGridView1.Columns[j].HeaderText == "Tiempo27"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo28" || dataGridView1.Columns[j].HeaderText == "Respuesta28" || dataGridView1.Columns[j].HeaderText == "Tiempo28"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo29" || dataGridView1.Columns[j].HeaderText == "Respuesta29" || dataGridView1.Columns[j].HeaderText == "Tiempo29"
                                    || dataGridView1.Columns[j].HeaderText == "Estimulo30" || dataGridView1.Columns[j].HeaderText == "Respuesta30" || dataGridView1.Columns[j].HeaderText == "Tiempo30")
                                {

                                    worksheet.Cells[i + 2, j - 2] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                              //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                              //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                              //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                }
                                else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                
                                columna++;
                            }




                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);

            }
        }
        
        private void ExportToExcelTiempoReacCompleSinSonido()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    //worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Detalles" && headerText != "IdTest" && headerText != "IdTestodo")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();

                            if (tempVal != "Detalles")
                            {
                                if (columna != 15 && columna != 16)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna ] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else if (dataGridView1.Columns[j].HeaderText == "Estimulo1" || dataGridView1.Columns[j].HeaderText == "Respuesta1" || dataGridView1.Columns[j].HeaderText == "Tiempo1"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo2" || dataGridView1.Columns[j].HeaderText == "Respuesta2" || dataGridView1.Columns[j].HeaderText == "Tiempo2"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo3" || dataGridView1.Columns[j].HeaderText == "Respuesta3" || dataGridView1.Columns[j].HeaderText == "Tiempo3"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo4" || dataGridView1.Columns[j].HeaderText == "Respuesta4" || dataGridView1.Columns[j].HeaderText == "Tiempo4"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo5" || dataGridView1.Columns[j].HeaderText == "Respuesta5" || dataGridView1.Columns[j].HeaderText == "Tiempo5"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo6" || dataGridView1.Columns[j].HeaderText == "Respuesta6" || dataGridView1.Columns[j].HeaderText == "Tiempo6"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo7" || dataGridView1.Columns[j].HeaderText == "Respuesta7" || dataGridView1.Columns[j].HeaderText == "Tiempo7"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo8" || dataGridView1.Columns[j].HeaderText == "Respuesta8" || dataGridView1.Columns[j].HeaderText == "Tiempo8"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo9" || dataGridView1.Columns[j].HeaderText == "Respuesta9" || dataGridView1.Columns[j].HeaderText == "Tiempo9"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo10" || dataGridView1.Columns[j].HeaderText == "Respuesta10" || dataGridView1.Columns[j].HeaderText == "Tiempo10"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo11" || dataGridView1.Columns[j].HeaderText == "Respuesta11" || dataGridView1.Columns[j].HeaderText == "Tiempo11"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo12" || dataGridView1.Columns[j].HeaderText == "Respuesta12" || dataGridView1.Columns[j].HeaderText == "Tiempo12"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo13" || dataGridView1.Columns[j].HeaderText == "Respuesta13" || dataGridView1.Columns[j].HeaderText == "Tiempo13"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo14" || dataGridView1.Columns[j].HeaderText == "Respuesta14" || dataGridView1.Columns[j].HeaderText == "Tiempo14"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo15" || dataGridView1.Columns[j].HeaderText == "Respuesta15" || dataGridView1.Columns[j].HeaderText == "Tiempo15"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo16" || dataGridView1.Columns[j].HeaderText == "Respuesta16" || dataGridView1.Columns[j].HeaderText == "Tiempo16"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo17" || dataGridView1.Columns[j].HeaderText == "Respuesta17" || dataGridView1.Columns[j].HeaderText == "Tiempo17"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo18" || dataGridView1.Columns[j].HeaderText == "Respuesta18" || dataGridView1.Columns[j].HeaderText == "Tiempo18"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo19" || dataGridView1.Columns[j].HeaderText == "Respuesta19" || dataGridView1.Columns[j].HeaderText == "Tiempo19"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo20" || dataGridView1.Columns[j].HeaderText == "Respuesta20" || dataGridView1.Columns[j].HeaderText == "Tiempo20"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo21" || dataGridView1.Columns[j].HeaderText == "Respuesta21" || dataGridView1.Columns[j].HeaderText == "Tiempo21"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo22" || dataGridView1.Columns[j].HeaderText == "Respuesta22" || dataGridView1.Columns[j].HeaderText == "Tiempo22"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo23" || dataGridView1.Columns[j].HeaderText == "Respuesta23" || dataGridView1.Columns[j].HeaderText == "Tiempo23"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo24" || dataGridView1.Columns[j].HeaderText == "Respuesta24" || dataGridView1.Columns[j].HeaderText == "Tiempo24"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo25" || dataGridView1.Columns[j].HeaderText == "Respuesta25" || dataGridView1.Columns[j].HeaderText == "Tiempo25"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo26" || dataGridView1.Columns[j].HeaderText == "Respuesta26" || dataGridView1.Columns[j].HeaderText == "Tiempo26"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo27" || dataGridView1.Columns[j].HeaderText == "Respuesta27" || dataGridView1.Columns[j].HeaderText == "Tiempo27"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo28" || dataGridView1.Columns[j].HeaderText == "Respuesta28" || dataGridView1.Columns[j].HeaderText == "Tiempo28"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo29" || dataGridView1.Columns[j].HeaderText == "Respuesta29" || dataGridView1.Columns[j].HeaderText == "Tiempo29"
                                        || dataGridView1.Columns[j].HeaderText == "Estimulo30" || dataGridView1.Columns[j].HeaderText == "Respuesta30" || dataGridView1.Columns[j].HeaderText == "Tiempo30")
                                    {

                                        worksheet.Cells[i + 2, j - 2] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else if ( dataGridView1.Columns[j].HeaderText == "Sonido1"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido2"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido3"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido4"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido5"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido6"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido7"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido8"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido9"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido10"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido11"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido12"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido13"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido14"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido15"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido16"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido17"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido18"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido19"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido20"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido21"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido22"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido23"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido24"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido25"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido26"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido27"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido28"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido29"
                                        || dataGridView1.Columns[j].HeaderText == "Sonido30")
                                    {

                                        worksheet.Cells[i + 2, j - 2] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                  //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                  //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                  //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;
                            }




                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);

                throw;
            }
        }

        private void ExportToExcelTiempoReaccSimple()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    //worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Detalles" && headerText != "IdTest" && headerText != "IdTestodo")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();

                            if (tempVal != "Detalles")
                            {
                               

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }
                                    

                                

                                
                                    columna++;
                            }






                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);

                throw;
            }
        }

        private void ExportToExcelCualidMotivacioDep()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    //worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "idTodo" && headerText != "id")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();


                            if (columna != 18 && columna != 19)
                            {

                                if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                {

                                    worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, columna] = tempVal;
                                }

                            }
                            columna++;





                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {

                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);

                throw;
            }
        }

        private void ExportToExcelCualidVolitivDepor()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Gráfico" && headerText != "idTodo" && headerText != "id")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (tempVal != "Gráfico")
                            {

                                if (columna != 14 && columna != 15 && columna != 16)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;



                            }

                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);
                throw;
            }
        }

        private void ExportToExcelIPED()
        {
            try
            {

     


                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = "IPED";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                  
                   

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Hoja de Perfil" && headerText != "Gráfico" && headerText != "Detalles" && headerText != "id")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (tempVal != "idTodo" && tempVal != "id")
                            {

                                if (columna != 12 && columna != 13)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;



                            }

                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);
                throw;
            }
        }

        private void ExportToExcel16Pf()
        {

            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Hoja de Perfil" && headerText != "Gráfico" && headerText != "Detalles" && headerText != "id")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (tempVal != "Hoja de Perfil" && tempVal != "Gráfico" && tempVal != "Detalles" && tempVal != "id")
                            {

                                if (columna != 42)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;



                            }

                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);
                throw;
            }

        }

        private void ExportToExcelIdetem()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  

                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = "IDETEM-1";
                    // storing header part in Excel  

                    //oRng = worksheet.get_Range("A1", "WWW1");
                    // oRng.EntireColumn.AutoFit();



                    worksheet.Range[worksheet.Cells[1, 8], worksheet.Cells[1, 17]].Merge();
                    worksheet.get_Range("H1", "Q1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    worksheet.Cells[1, 8] = "Propiedades del Sistema Nervioso";
                    worksheet.get_Range("H1", "Q1").Interior.Color = Color.FromArgb(33, 152, 211);
                    worksheet.get_Range("H1", "Q1").Font.Color = Color.White;


                    worksheet.Range[worksheet.Cells[1, 18], worksheet.Cells[1, 29]].Merge();
                    worksheet.get_Range("R1", "AC1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    worksheet.Cells[1, 18] = "Propiedades Psicodiámicas";
                    worksheet.get_Range("R1", "AC1").Interior.Color = Color.FromArgb(51, 196, 129);
                    worksheet.get_Range("R1", "AC1").Font.Color = Color.White;


                    worksheet.Cells[2, 1] = "Nombre";
                    worksheet.Cells[2, 2] = "Primer Apellido";
                    worksheet.Cells[2, 3] = "Segundo Apellido";

                    worksheet.Cells[2, 4] = "Sanguineo";
                    worksheet.Cells[2, 5] = "Colérico";
                    worksheet.Cells[2, 6] = "Flemático";
                    worksheet.Cells[2, 7] = "Melancólico";



                    worksheet.Cells[2, 8] = "Equilibrio";
                    worksheet.Cells[2, 9] = "Desequilibrio por exitación";
                    worksheet.Cells[2, 10] = "Desequilibrio por inhibición";
                    worksheet.Cells[2, 11] = "Fuerza";
                    worksheet.Cells[2, 12] = "Debilidad";
                    worksheet.Cells[2, 13] = "Movilidad";
                    worksheet.Cells[2, 14] = "Inercia";
                    worksheet.Cells[2, 15] = "Dinamísmo psíquico";
                    worksheet.Cells[2, 16] = "Poco dinamísmo psíquico";
                    worksheet.Cells[2, 17] = "Labilidad";



                    worksheet.Cells[2, 18] = "Actividad";
                    worksheet.Cells[2, 19] = "Reactividad moderada";
                    worksheet.Cells[2, 20] = "Reactividad alta";
                    worksheet.Cells[2, 21] = "Resistencia alta";
                    worksheet.Cells[2, 22] = "Resistencia baja";
                    worksheet.Cells[2, 23] = "Ritmo psíquico lento";
                    worksheet.Cells[2, 24] = "Sensibilidad";
                    worksheet.Cells[2, 25] = "Poca sensibilida";
                    worksheet.Cells[2, 26] = "Extroversión";
                    worksheet.Cells[2, 27] = "Introversión";
                    worksheet.Cells[2, 28] = "Plasticidad";
                    worksheet.Cells[2, 29] = "Rigidez";

                    worksheet.Cells[2, 30] = "Fecha";




                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A2", "AE2");
                    oRng.EntireColumn.AutoFit();


                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[14].Value.ToString();
                            String nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            String apellido1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            String apellido2 = dataGridView1.Rows[i].Cells[2].Value.ToString();

                            using (mainEntities entities = new mainEntities())
                            {
                                var idetem = entities.Idetem.Find(Convert.ToInt32(tempVal));


                                worksheet.Cells[i + 3, 1] = nombre;
                                worksheet.Cells[i + 3, 2] = apellido1;
                                worksheet.Cells[i + 3, 3] = apellido2;

                                worksheet.Cells[i + 3, 4] = idetem.sanguineo;
                                worksheet.Cells[i + 3, 5] = idetem.colerico;
                                worksheet.Cells[i + 3, 6] = idetem.flematico;
                                worksheet.Cells[i + 3, 7] = idetem.melancolico;

                                worksheet.Cells[i + 3, 8] = idetem.equilibrio;
                                worksheet.Cells[i + 3, 9] = idetem.deseqExita;
                                worksheet.Cells[i + 3, 10] = idetem.deseqInhibi;
                                worksheet.Cells[i + 3, 11] = idetem.fuerza;
                                worksheet.Cells[i + 3, 12] = idetem.debilidad;
                                worksheet.Cells[i + 3, 13] = idetem.movilidad;
                                worksheet.Cells[i + 3, 14] = idetem.inercia;
                                worksheet.Cells[i + 3, 15] = idetem.dinamPsiq;
                                worksheet.Cells[i + 3, 16] = idetem.pocoDinaPsiq;
                                worksheet.Cells[i + 3, 17] = idetem.labilidad;


                                worksheet.Cells[i + 3, 18] = idetem.actividad;
                                worksheet.Cells[i + 3, 19] = idetem.reactModer;
                                worksheet.Cells[i + 3, 20] = idetem.reactAlta;
                                worksheet.Cells[i + 3, 21] = idetem.resisAlta;
                                worksheet.Cells[i + 3, 22] = idetem.resisBaja;
                                worksheet.Cells[i + 3, 23] = idetem.ritmPsiLent;
                                worksheet.Cells[i + 3, 24] = idetem.sensibilidad;
                                worksheet.Cells[i + 3, 25] = idetem.pocaSensi;
                                worksheet.Cells[i + 3, 26] = idetem.extroversion;
                                worksheet.Cells[i + 3, 27] = idetem.introversion;
                                worksheet.Cells[i + 3, 28] = idetem.plasticidad;
                                worksheet.Cells[i + 3, 29] = idetem.rigidez;

                                String ini = "AD" + i + 3;
                                String end = "AD" + i + 3;
                                worksheet.get_Range(ini, end).EntireColumn.NumberFormat = "DD/MM/YYYY";
                                worksheet.Cells[i + 3, 30] = DateTime.Parse(idetem.Fecha).ToString("dd/MM/yyyy");


                            }

                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //  app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);

                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);
                throw;
            }
        }

        private void ExportToExcelCatell()
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "idTodo" && headerText != "id" && headerText != "Ptos.Bruta y Stems")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (tempVal != "Ptos.Bruta y Stems")
                            {

                                if (columna != 8 && columna != 9)
                                {

                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }

                                }
                                columna++;



                            }

                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  
                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);
                throw;
            }
        }

        public void ExportToExcel()
        {

            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Export to Excel";
                string tie = DateTime.Now.ToString("dd-MM-yyyy");
                fichero.FileName = nombrePrueba + "_" + tie;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fichero.FileName))
                    {
                        File.Delete(fichero.FileName);
                    }

                    Esperar er = new Esperar();
                    er.Show();
                    Application.DoEvents();


                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  
                    //  app.Visible = true;
                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    worksheet = workbook.Sheets["Hoja1"];
                    worksheet = workbook.ActiveSheet;
                    // changing the name of active sheet  
                    worksheet.Name = nombrePrueba.Replace(" ", "");
                    // storing header part in Excel  

                    int k = 1;
                    int r = 0;



                    while (r < dataGridView1.Columns.Count - 2)
                    {
                        String headerText = dataGridView1.Columns[r].HeaderText;
                        if (headerText != "Hoja de Perfil" && headerText != "Gráfico" && headerText != "Detalles" && headerText != "id")
                        {
                            worksheet.Cells[1, k] = headerText;
                            k++;

                        }
                        r++;
                    }

                    Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                    oRng.EntireColumn.AutoFit();

                    // storing Each row and column value to excel sheet  

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int columna = 1;
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            if (tempVal != "Hoja de Perfil" && tempVal != "Gráfico" && tempVal != "Detalles" && tempVal != "id")
                            {


                                if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                {

                                    worksheet.Cells[i + 2, columna] = tempVal;  //DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                                                                //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                                                                //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                                                //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, columna] = tempVal;
                                }


                                columna++;



                            }

                        }
                    }
                    // save the application  

                    workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //app.Visible = true;
                    // Exit from the application  


                    app.Quit();
                    er.Close();
                    MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(fichero.FileName);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + e.Message);
                throw;
            }
        }
        
        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            deporte = comboBox4.SelectedValue.ToString();
        }
        
        private void comboBox4_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;

            deporte = comboBox4.SelectedValue.ToString();
            llenarTabla();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        public void buscarID(string identidad)
        {

            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from DatosSujetos  where NCarnetIdent ='1'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                etapa = res["idEtapa"].ToString();
                                comboBox2.Text = res["Etapa"].ToString();

                            }
                        }
                    }
                }


            }

        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            etapa = comboBox2.SelectedValue.ToString();
            if (checkBox1.Checked)
            {

                llenarTabla();

            }
            else
            {
                comboBox1.Text = "Seleccione";
                comboBox3.Text = "Seleccione";

                dataGridView1.Rows.Clear();
                llenarTabla();
            }

        }

        private void comboBox5_DropDown(object sender, EventArgs e)
        {

            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * from Tipoetapa", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox2.DataSource = data;
                    comboBox2.DisplayMember = "Etapa";
                    comboBox2.ValueMember = "idEtapa";

                }

            }
        }
    }

}

