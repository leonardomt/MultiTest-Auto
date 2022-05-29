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
using Multitest.VisualizarPruebasRealizadas;
using Multitest.FormAux;

namespace Multitest.VisualizarPruebas
{
    public partial class UserControlVisualizarPruebaAtleta : UserControl
    {

        string[] idTest;
        string nombreAtleta = "";
        string fecha = "";
        string etapa = "";
        string entidad = "";
        string carnet = "";
        String idAtleta = "";
        String idATestTodo = "";
        AutoCompleteStringCollection source = new AutoCompleteStringCollection();

        private static UserControlVisualizarPruebaAtleta _instance;

        public static UserControlVisualizarPruebaAtleta Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserControlVisualizarPruebaAtleta();

                return _instance;
            }
        }

        public UserControlVisualizarPruebaAtleta()
        {
            InitializeComponent();
            ActiveControlUser.Instance.addControl("UserControlVisualizarPruebaAtleta");

        }

       

        
        public void buscarPrueba()
        {
            String idSujeto = idAtleta.ToString();
            //String idEtapa = buscarEtapa();
            fecha = comboBox2.SelectedValue.ToString();

            // if (comboBox3.Text != "Seleccione")
            //     fecha = comboBox3.SelectedItem.ToString();


            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados  where idSujeto =='" + idSujeto + "'  and Fecha=='" + fecha + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                idATestTodo = res["idSujetoEvaluado"].ToString();
                                idTest = new string[22];
                                bool existePrueba = false;

                                if (res["PIdareSitua"].ToString() != "")
                                {
                                    button20.Visible = true;
                                    label23.ForeColor = Color.Red;
                                    idTest[0] = res["PIdareSitua"].ToString();
                                    existePrueba = true;

                                }

                                if (res["PIdareRasgo"].ToString() != "")
                                {
                                    button15.Visible = true;
                                    label18.ForeColor = Color.Red;
                                    idTest[21] = res["PIdareRasgo"].ToString();

                                    existePrueba = true;
                                }

                                if (res["PRaven"].ToString() != "")
                                {
                                    r.Visible = true;
                                    label3.ForeColor = Color.Red;
                                    idTest[1] = res["PRaven"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PIdetem"].ToString() != "")
                                {
                                
                                    idTest[2] = res["PIdetem"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PWeil"].ToString() != "")
                                {
                                    button5.Visible = true;
                                    label8.ForeColor = Color.Red;
                                    idTest[3] = res["PWeil"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PDomino"].ToString() != "")
                                {
                                    button10.Visible = true;
                                    label13.ForeColor = Color.Red;
                                    idTest[4] = res["PDomino"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PEysenk"].ToString() != "")
                                {
                                    button1.Visible = true;
                                    label5.ForeColor = Color.Red;
                                    idTest[5] = res["PEysenk"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PCatell"].ToString() != "")
                                {
                                    button6.Visible = true;
                                    label9.ForeColor = Color.Red;
                                    idTest[6] = res["PCatell"].ToString();
                                    existePrueba = true;
                                }

                                if (res["P16pf"].ToString() != "")
                                {
                                    button11.Visible = true;
                                    label14.ForeColor = Color.Red;
                                    idTest[7] = res["P16pf"].ToString();
                                    existePrueba = true;
                                }

 



                                if (res["PPoms"].ToString() != "")
                                {
                                    button7.Visible = true;
                                    label10.ForeColor = Color.Red;
                                    idTest[9] = res["PPoms"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PCualidMotivDepor"].ToString() != "")
                                {
                                  
                                    idTest[10] = res["PCualidMotivDepor"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PActiAnteComp"].ToString() != "")
                                {
                                    button19.Visible = true;
                                    label22.ForeColor = Color.Red;
                                    idTest[11] = res["PActiAnteComp"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PMotivDepButt"].ToString() != "")
                                {
                                    button2.Visible = true;
                                    label4.ForeColor = Color.Red;
                                    idTest[12] = res["PMotivDepButt"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PTrsimple"].ToString() != "")
                                {
                                    button3.Visible = true;
                                    label6.ForeColor = Color.Red;
                                    idTest[13] = res["PTrsimple"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PTrcomple"].ToString() != "")
                                {
                                    button8.Visible = true;
                                    label11.ForeColor = Color.Red;
                                    idTest[14] = res["PTrcomple"].ToString();
                                    existePrueba = true;
                                }


                                if (res["PTrcomples"].ToString() != "")
                                {
                                    button16.Visible = true;
                                    label19.ForeColor = Color.Red;
                                    idTest[17] = res["PTrcomples"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PResanti"].ToString() != "")
                                {
                                    button18.Visible = true;
                                    label21.ForeColor = Color.Red;
                                    idTest[15] = res["PResanti"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PCualiVolitiv"].ToString() != "")
                                {
                                   
                                    idTest[16] = res["PCualiVolitiv"].ToString();
                                    existePrueba = true;
                                }


                                if (res["PIped"].ToString() != "")
                                {
                                    button13.Visible = true;
                                    label16.ForeColor = Color.Red;
                                    idTest[18] = res["PIped"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PTRN"].ToString() != "")
                                {
                                    button21.Visible = true;
                                    label24.ForeColor = Color.Red;
                                    idTest[19] = res["PTRN"].ToString();
                                    existePrueba = true;
                                }

                                if (res["PAnsiedadCompetitiva"].ToString() != "")
                                {
                                    button9.Visible = true;
                                    label12.ForeColor = Color.Red;
                                    idTest[20] = res["PAnsiedadCompetitiva"].ToString();
                                    existePrueba = true;
                                }

                            }
                            else
                            {

                                comboBox1.Text = "Seleccione";
                                comboBox2.Text = "Seleccione";
                                comboBox2.Enabled = false;
                                buscarEtapa();

                            }
                        }
                    }
                }
            }


        }

        public void limpiarPruebasDia()
        {


            button15.Visible = false;
            label18.ForeColor = Color.Black;

            button20.Visible = false;
            label23.ForeColor = Color.Black;

            button15.Visible = false;
            label18.ForeColor = Color.Black;

            r.Visible = false;
            label3.ForeColor = Color.Black;
 

            button5.Visible = false;
            label8.ForeColor = Color.Black;

            button10.Visible = false;
            label13.ForeColor = Color.Black;

            button1.Visible = false;
            label5.ForeColor = Color.Black;


            button6.Visible = false;
            label9.ForeColor = Color.Black;


            button11.Visible = false;
            label14.ForeColor = Color.Black;


           

            button7.Visible = false;
            label10.ForeColor = Color.Black;

 


            button19.Visible = false;
            label22.ForeColor = Color.Black;


            button2.Visible = false;
            label4.ForeColor = Color.Black;


            button3.Visible = false;
            label6.ForeColor = Color.Black;

            button8.Visible = false;
            label11.ForeColor = Color.Black;


            button16.Visible = false;
            label19.ForeColor = Color.Black;

            button18.Visible = false;
            label21.ForeColor = Color.Black;

          

            button13.Visible = false;
            label16.ForeColor = Color.Black;


            button20.Visible = false;
            label23.ForeColor = Color.Black;

            button21.Visible = false;
            label24.ForeColor = Color.Black;

            button9.Visible = false;
            label12.ForeColor = Color.Black;
            

        }

        private void UserControlVisualizarPruebaAtleta_Load(object sender, EventArgs e)
        {
           // buscarEtapa();
        }

        public void buscarEtapa()
        {

            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from DatosSujetos  where Actual ='1'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                etapa = res["NCarnetIdent"].ToString();
                                comboBox3.Text = res["ID"].ToString();

                            }
                        }
                    }
                }

            }

        }



        public void cambiarEtapa()
        {
            
            limpiarPruebasDia();
            comboBox1.Text = "Seleccione";
            comboBox2.Text = "Seleccione";
            comboBox2.Text = "Seleccione";



        }
        private bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }


        private void comboBox3_Text_Changed(object sender, EventArgs e)
        {

            carnet = comboBox3.Text;
            if (carnet.Length == 11)
            {
                if (IsAllDigits(carnet))
                {
                    if (source.Contains(carnet))
                    {
                        using (mainEntities db = new mainEntities())
                        {
                            using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                            {
                                using (SQLiteCommand command = new SQLiteCommand("Select * from DatosSujetos  where NCarnetIdent = " + carnet, ne))
                                {
                                    ne.Open();
                                    using (SQLiteDataReader res = command.ExecuteReader())
                                    {
                                        if (res.HasRows)
                                        {
                                            res.Read();
                                            etapa = res["NCarnetIdent"].ToString();
                                            idAtleta = res["IdDatosSujetos"].ToString();
                                            entidad = res["Entidad"].ToString();
                                            nombreAtleta = nombreAtleta = res["NombreS"].ToString() + " " + res["PrimerApellido"].ToString() + " " + res["SegundoApellido"].ToString();
                                            


                                        }
                                    }
                                }
                            }

                            comboBox1.Text = nombreAtleta;
                            comboBox2.Text = "Seleccione";
                            comboBox4.Text = entidad;
                            comboBox2.Enabled = true;
                            comboBox1.Enabled = true;
                            comboBox2.DroppedDown = true;
                            limpiarPruebasDia();
                        }
                        

                      
             
                    }
                    else
                    {
                        comboBox1.Text = "Seleccione";
                        comboBox2.Text = "Seleccione";
                        //comboBox4.Text = "Seleccione";
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        limpiarPruebasDia();
                    }
                }
                else
                {
                    comboBox1.Text = "Seleccione";
                    comboBox2.Text = "Seleccione";
                    //comboBox4.Text = "Seleccione";
                    comboBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    limpiarPruebasDia();
                }
            }
            else
            {
                comboBox1.Text = "Seleccione";
                comboBox2.Text = "Seleccione";
                //comboBox4.Text = "Seleccione";
                comboBox2.Enabled = false;
                comboBox1.Enabled = false;
                limpiarPruebasDia();
            }




        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);



            List<string> list = new List<string>();
            //if (source == null)
            {

                using (mainEntities f = new mainEntities())
                {
                    using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * from DatosSujetos inner join SujetosEvaluados on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto", f.Database.Connection.ConnectionString.ToString()))
                    {
                        DataTable data = new DataTable();
                        categoria.Fill(data);
                        string[] postSource = data
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("NCarnetIdent"))
                    .ToArray();



                        source.AddRange(postSource);
                        comboBox3.AutoCompleteCustomSource = source;


                    }
                }


            }
            

        }


        private void comboBox3_AutoComplete_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            List<string> list = new List<string>();
            //if (source == null)
            {

                using (mainEntities f = new mainEntities())
                {
                    using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * from DatosSujetos inner join SujetosEvaluados on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto", f.Database.Connection.ConnectionString.ToString()))
                    {
                        DataTable data = new DataTable();
                        categoria.Fill(data);
                        string[] postSource = data
                    .AsEnumerable()
                    .Select<System.Data.DataRow, String>(x => x.Field<String>("NCarnetIdent"))
                    .ToArray();


                        
                        source.AddRange(postSource);
                        comboBox3.AutoCompleteCustomSource = source;
          

                    }
                }


            }
            

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select DISTINCT Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join SujetosEvaluados on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto where  Entidad='" + comboBox4.SelectedValue + "'", f.Database.Connection.ConnectionString.ToString()))
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
            nombreAtleta = comboBox1.SelectedText;
            idAtleta = comboBox1.SelectedValue.ToString();

            using (mainEntities db = new mainEntities())
            {
                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("Select * from DatosSujetos inner join SujetosEvaluados on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto  where IdDatosSujetos = " + idAtleta, ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                etapa = res["NCarnetIdent"].ToString();
                                carnet = etapa;


                            }
                        }
                    }
                }

                
                
                comboBox3.Text = carnet;
                comboBox2.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.DroppedDown = true;

                limpiarPruebasDia();
            }

            
           
          
        }




        private void r_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Raven", nombreAtleta, idTest[1], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Dominó", nombreAtleta, idTest[4], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Weil", nombreAtleta, idTest[3], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Idare (Rasgo)", nombreAtleta, idTest[21], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }
        private void button20_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Idare (Situacional)", nombreAtleta, idTest[0], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Catell", nombreAtleta, idTest[6], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            _16PfFormView d = new _16PfFormView(idTest[7], nombreAtleta, true, Convert.ToInt32(idAtleta));
            d.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Cualidades Volitivas", nombreAtleta, idTest[16], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Butt", nombreAtleta, idTest[12], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Cualidades Motivacionales", nombreAtleta, idTest[10], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Actitud Ante la Competencia", nombreAtleta, idTest[11], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }


    

        private void Button7_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("POMS", nombreAtleta, idTest[9], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Esperar es = new Esperar();
            es.Show();
            TRS_Form3 prueba = new TRS_Form3(nombreAtleta, idTest[13], true, idATestTodo, idAtleta);
            es.Close();
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Esperar esp = new Esperar();
            esp.Show();
            TRC_Form3 tRC_Form3 = new TRC_Form3(nombreAtleta, idTest[14], false, idATestTodo, idAtleta);
            esp.Close();
            tRC_Form3.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            TRC_Form3 tRC_Form3 = new TRC_Form3(nombreAtleta, idTest[17], true, idATestTodo, idAtleta);
            tRC_Form3.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Esperar es = new Esperar();
            es.Show();
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Ansiedad Precompetitiva CSAI-2R", nombreAtleta, idTest[20], fecha, idAtleta, idATestTodo);
            es.Close();
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Idetem", nombreAtleta, idTest[2], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Iped", nombreAtleta, idTest[18], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }


        private void button18_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Esperar es = new Esperar();
            es.Show();
            RA_Form3 res = new RA_Form3(nombreAtleta, idTest[15], idATestTodo, idAtleta, etapa);
            es.Close();
            res.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Eysenck", nombreAtleta, idTest[5], fecha, idAtleta, idATestTodo);
            prueba.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            TRN_Form2 tRN_Form = new TRN_Form2(nombreAtleta, idTest[19], true, idATestTodo, idAtleta);
            tRN_Form.ShowDialog();
            limpiarPruebasDia();
            buscarPrueba();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox3_TextUpdate(Object sender, EventArgs e)
        {

            
        }


        public void LimpiarCampos()
        {
            comboBox1.Text = "Seleccione";
            comboBox2.Text = "Selecione";
            comboBox3.Text = "Selecione";

            buscarEtapa();
            limpiarPruebasDia();
        }


        

        private void comboBox2_DropDown(object sender, EventArgs e)  // Fecha
        {
            using (mainEntities f = new mainEntities())
            {
                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select *  FROM SujetosEvaluados where idSujeto='" + idAtleta + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox2.DataSource = data;
                    comboBox2.DisplayMember = "Fecha";
                    comboBox2.ValueMember = "Fecha";

                }
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)  //Fecha
        {
            
            nombreAtleta = nombreAtleta;


            limpiarPruebasDia();
            buscarPrueba();
        }


        private void comboBox4_DropDown(object sender, EventArgs e)  // Entidad
        {
            entidad = comboBox4.SelectedText;
            etapa = "";
            nombreAtleta = "";
            fecha = "";

            using (mainEntities f = new mainEntities())
            {

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  distinct Entidad from DatosSujetos inner join SujetosEvaluados on DatosSujetos.IdDatosSujetos = SujetosEvaluados.idSujeto", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox4.DataSource = data;
                    comboBox4.DisplayMember = "Entidad";
                    comboBox4.ValueMember = "Entidad";


                    comboBox1.Text = "Seleccione";
                    comboBox2.Text = "Seleccione";
                    
                }
            }

        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            
        }
//CAMBIARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
        public void buscarPruebaAtlteta()
        {
            if (comboBox4.Text != "Seleccione" && comboBox1.Text != "Seleccione" && comboBox2.Text != "Seleccione" ) // CAMBIARRRRRRRRRRRRRRRRRRRRRRRRRR
            {
                DataRowView d = (DataRowView)comboBox1.SelectedItem;
                nombreAtleta = d.Row.ItemArray[2].ToString();

                limpiarPruebasDia();
                buscarPrueba();
            }

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

       
    }
}
