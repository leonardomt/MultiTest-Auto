
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Multitest.VisualizarPruebasRealizadas;

using Multitest.ADOmodel;
using Multitest.VentanasPruebas;
using System.Configuration;
using Multitest.AuxClass;
using Multitest.VisualizarPruebas;

using System.Linq;

namespace Multitest
{
    public partial class UserControlPruebas : UserControl
    {

        private static UserControlPruebas _instance;
        String tipoPrueba = null;

        String idEtapa = "";
        int edad = 0;
        String sexo = "";
        String fecha = "";
        String nombreAtleta = "";
        String carnet = "";
        String entidad = "";
        String value = "";

        AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        string[] idTest = new string[22];

        public static UserControlPruebas Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserControlPruebas();

                return _instance;
            }
        }

        public UserControlPruebas()
        {
            InitializeComponent();
            idEtapa = buscarEtapa();
            ActiveControlUser.Instance.addControl("UserControlPruebas");

        }

        private void ComboBox1_DropDown(object sender, EventArgs e)
        {

            using (mainEntities f = new mainEntities())
            {

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select Sexo,IdDatosSujetos,(NombreS||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos where Entidad='" + comboBox2.SelectedValue + "'", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox1.DataSource = data;
                    comboBox1.DisplayMember = "fila";
                    comboBox1.ValueMember = "IdDatosSujetos";
                
                    
                }

                limpiarPruebasDia();
                comboBox3.Text = "Seleccione";
                
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {



            VerificarHardware verificar = new VerificarHardware();
            bool result = verificar.configurarHardware();

            if (/*result*/true)
            {
                tipoPrueba = comboBox3.Text;

                if (tipoPrueba != "Seleccione")
                {
                    if (comboBox1.Text != "Seleccione")
                    {
                        bool resul = verificarPrueba();
                        bool updateTest = false;
                        int entro = 0;

                        if (resul == true)
                        {
                            updateTest = MessageBox.Show("Ya existe una test realizado en el día. Desea volver a realizarlo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
                            if (updateTest == false)
                            {
                                entro = 1;

                            }

                        }



                        if (entro != 1)
                        {
                            
                            ActiveControlUser.Instance.inTest = true;

                            String idPerson = "";
                            if (comboBox1.SelectedValue == null)
                                idPerson = value;
                            else
                                idPerson = comboBox1.SelectedValue.ToString();
                            
                            findSexoEdad(idPerson);

                            if (tipoPrueba == "Raven" || tipoPrueba == "Dominó" || tipoPrueba == "Weil")
                            {
                                PruebasPantalla p = new PruebasPantalla(tipoPrueba, updateTest, idPerson, idEtapa, edad);
                                p.ShowDialog();
                            }

                            if (tipoPrueba == "Eysenck" || tipoPrueba == "Catell"
                                || tipoPrueba == "Factor Humano" || tipoPrueba == "POMS" || tipoPrueba == "I.R de Loehr"
                                || tipoPrueba == "Actitud ante la Competencia" || tipoPrueba == "Test de Motivos Deportivos de Butt"
                                || tipoPrueba == "Cualidades Motivacionales Deportivas" || tipoPrueba == "Ansiedad Precompetitiva de Martens"
                                || tipoPrueba == "IDETEM-1" || tipoPrueba == "Inventario Psicológico de Ejecución Deportiva (IPED)" ||
                                  tipoPrueba == "Cualidades Volitivas en el Deporte" || tipoPrueba == "IDARE (Situacional)"
                                || tipoPrueba == "IDARE (Rasgo)" || tipoPrueba == "Ansiedad Precompetitiva CSAI-2R")
                            {
                                Preguntas p = new Preguntas(null, tipoPrueba, updateTest, idPerson, idEtapa, sexo, edad);
                                p.ShowDialog();
                            }

                            if (tipoPrueba == "16PF")
                            {
                                Form16Pf cre = new Form16Pf(idPerson, idEtapa, sexo);
                                cre.ShowDialog();
                            }



                            if (tipoPrueba == "Tiempo de Reacción Simple")
                            {
                                if (ConfigurationManager.AppSettings["Tipomultitest"] != "")
                                {
                                    TRS_Form1 r = new TRS_Form1(idPerson, idEtapa, nombreAtleta);
                                    r.ShowDialog();

                                }
                                else
                                    MessageBox.Show("Debe seleccionar el tipo de hardware en la pestaña ARCHIVO ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                            if (tipoPrueba == "Tiempo de Reacción Complejo")
                            {
                                if (ConfigurationManager.AppSettings["Tipomultitest"] != "")
                                {
                                    TRC_Form1 r = new TRC_Form1(false, idPerson, idEtapa, nombreAtleta);
                                    r.ShowDialog();

                                }

                                else
                                    MessageBox.Show("Debe seleccionar el tipo de hardware en la pestaña ARCHIVO ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            if (tipoPrueba == "Tiempo de Reacción Complejo con Sonido")
                            {
                                if (ConfigurationManager.AppSettings["Tipomultitest"] != "")
                                {
                                    TRC_Form1 r = new TRC_Form1(true, idPerson, idEtapa, nombreAtleta);
                                    r.ShowDialog();

                                }
                                else
                                    MessageBox.Show("Debe seleccionar el tipo de hardware en la pestaña ARCHIVO ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }

                            if (tipoPrueba == "Tiempo de Respuesta Anticipada")
                            {
                                if (ConfigurationManager.AppSettings["Tipomultitest"] != "")
                                {
                                    RA_Form1 r = new RA_Form1(idPerson, idEtapa, nombreAtleta);
                                    r.ShowDialog();

                                }
                                else
                                    MessageBox.Show("Debe seleccionar el tipo de hardware en la pestaña ARCHIVO ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            if (tipoPrueba == "Tabla Rojo y Negra")
                            {
                                if (ConfigurationManager.AppSettings["Tipomultitest"] != "")
                                {
                                    TRN_Form1 tRN = new TRN_Form1(idPerson, idEtapa, nombreAtleta);
                                    tRN.ShowDialog();

                                }
                                else
                                    MessageBox.Show("Debe seleccionar el tipo de hardware en la pestaña ARCHIVO ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                            //Busca las pruebas realizadas en el dia
                            ActiveControlUser.Instance.inTest = false;
                            buscarPrueba();


                            if (ActiveControlUser.Instance.verResultados == 1)
                            {
                                VisualizarPruebasTodas.Instance.cambiarNombre(tipoPrueba);

                            }
                            if (ActiveControlUser.Instance.verResultados == 2)
                            {


                                UserControlVisualizarPruebaAtleta.Instance.buscarPruebaAtlteta();


                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un sujeto ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una prueba ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            else
            {
                MessageBox.Show("El dispositivo debe estar conectado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }




        private void comboBox3_Text_Changed(object sender, EventArgs e)
        {

            

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

        private void textBox1_Text_Changed(object sender, EventArgs e)
        {
          
            carnet = textBox1.Text;
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
                                            carnet = res["NCarnetIdent"].ToString();
                                            //idAtleta = res["IdDatosSujetos"].ToString();
                                            entidad = res["Entidad"].ToString();
                                            nombreAtleta = nombreAtleta = res["NombreS"].ToString() + " " + res["PrimerApellido"].ToString() + " " + res["SegundoApellido"].ToString();
                                            value = res["IdDatosSujetos"].ToString();


                                        }
                                    }
                                }
                            }

                            comboBox2.Text = entidad;
                            comboBox1.Text = nombreAtleta;
                            comboBox2.Enabled = true;
                            comboBox3.Enabled = true;
                            comboBox1.Enabled = true;

                            limpiarPruebasDia();
                            buscarPrueba();

                        }




                    }
                    else
                    {
                        
                        
                        
                        comboBox1.Enabled = true;
               
                        limpiarPruebasDia();
                        buscarPrueba();
                    }
                }
                else
                {
                    
               

                    comboBox2.Enabled = true;
                    comboBox1.Enabled = true;
                    limpiarPruebasDia();
                    buscarPrueba();
                }
            }
            else
            {
              
                comboBox2.Enabled = true;
                comboBox1.Enabled = true;
                limpiarPruebasDia();
                buscarPrueba();
            }




        }

        private void textBox1_Click(object sender, EventArgs e)
        {
          


                List<string> list = new List<string>();
                //if (source == null)
                {

                    using (mainEntities f = new mainEntities())
                    {
                        using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * from DatosSujetos", f.Database.Connection.ConnectionString.ToString()))
                        {
                            DataTable data = new DataTable();
                            categoria.Fill(data);
                            string[] postSource = data
                        .AsEnumerable()
                        .Select<System.Data.DataRow, String>(x => x.Field<String>("NCarnetIdent"))
                        .ToArray();



                            source.AddRange(postSource);
                            textBox1.AutoCompleteCustomSource = source;


                        }
                    }


                }
            

        }
        private string findSexoEdad(string idPerson)
        {


            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from DatosSujetos  where IdDatosSujetos ='" + idPerson + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                sexo = res["Sexo"].ToString();
                                edad = Convert.ToInt32(res["Edad"].ToString());
                            }
                        }
                    }
                }
            }
            return sexo;


        }

        private bool verificarPrueba()
        {
            bool result = false;


            if (tipoPrueba == "Raven" && r.Visible == true)
            {
                result = true;
            }

            if (tipoPrueba == "Weil" && button5.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Dominó" && button10.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Eysenck" && button1.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Cattell" && button6.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "16PF" && button11.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Poms-VIC" && button7.Visible == true)
            {
                result = true;
            }



            if (tipoPrueba == "IDARE (Rasgo)" && button15.Visible == true)
            {
                result = true;
            }


            if (tipoPrueba == "IDARE (Situacional)" && button20.Visible == true)
            {
                result = true;
            }


            if (tipoPrueba == "Test Motivo de Butt" && button2.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Inventario Psicológico de Ejecución Deportiva(IPED)" && button13.Visible == true)
            {
                result = true;
            }

            if (tipoPrueba == "Actitud ante la Competencia" && button19.Visible == true)
            {
                result = true;
            }





            if (tipoPrueba == "Tiempo de Reacción Simple" && button3.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Tiempo de Reacción Complejo" && button8.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Tiempo de Reacción Complejo con Sonido" && button16.Visible == true)
            {
                result = true;
            }
            if (tipoPrueba == "Tiempo de Respuesta Anticipada" && button18.Visible == true)
            {
                result = true;
            }


            if (tipoPrueba == "PruTRN" && button21.Visible == true)
            {
                result = true;
            }




            return result;

        }



        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataRowView d = (DataRowView)comboBox1.SelectedItem;
            
            
            nombreAtleta = d.Row.ItemArray[2].ToString();
            
            comboBox3.Enabled = true;
            using (mainEntities db = new mainEntities())
            {
                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("Select * from DatosSujetos  where IdDatosSujetos = " + d.Row.ItemArray[1].ToString(), ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();
                                carnet = res["NCarnetIdent"].ToString();
                                
                                value = res["IdDatosSujetos"].ToString();
                                textBox1.Text = carnet;


                            }
                        }
                    }
                }

                

            }

            limpiarPruebasDia();
            buscarPrueba();


        }

        private void limpiarPruebasDia()
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
        }

        private void buscarPrueba()
        {

            if (comboBox1.SelectedValue != null)
            {
                String idSujeto = comboBox1.SelectedValue.ToString();
                
                if (comboBox1.SelectedValue.ToString() == null)
                    idSujeto = value;
                //String idEtapa = buscarEtapa();
                fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");

                // if (comboBox3.Text != "Seleccione")
                //     fecha = comboBox3.SelectedItem.ToString();


                using (mainEntities db = new mainEntities())
                {

                    using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                    {
                        using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados  where idSujeto =='" + idSujeto + "' and Etapa=='" + idEtapa + "' and Fecha=='" + fecha + "'", ne))
                        {
                            ne.Open();
                            using (SQLiteDataReader res = command.ExecuteReader())
                            {
                                if (res.HasRows)
                                {
                                    res.Read();


                                    if (res["PIdareSitua"].ToString() != "")
                                    {
                                        button20.Visible = true;
                                        label23.ForeColor = Color.Red;
                                        idTest[0] = res["PIdareSitua"].ToString();


                                    }

                                    if (res["PIdareRasgo"].ToString() != "")
                                    {
                                        button15.Visible = true;
                                        label18.ForeColor = Color.Red;
                                        idTest[21] = res["PIdareRasgo"].ToString();


                                    }

                                    if (res["PRaven"].ToString() != "")
                                    {
                                        r.Visible = true;
                                        label3.ForeColor = Color.Red;
                                        idTest[1] = res["PRaven"].ToString();
                                    }



                                    if (res["PWeil"].ToString() != "")
                                    {
                                        button5.Visible = true;
                                        label8.ForeColor = Color.Red;
                                        idTest[3] = res["PWeil"].ToString();
                                    }

                                    if (res["PDomino"].ToString() != "")
                                    {
                                        button10.Visible = true;
                                        label13.ForeColor = Color.Red;
                                        idTest[4] = res["PDomino"].ToString();
                                    }

                                    if (res["PEysenk"].ToString() != "")
                                    {
                                        button1.Visible = true;
                                        label5.ForeColor = Color.Red;
                                        idTest[5] = res["PEysenk"].ToString();
                                    }

                                    if (res["PCatell"].ToString() != "")
                                    {
                                        button6.Visible = true;
                                        label9.ForeColor = Color.Red;
                                        idTest[6] = res["PCatell"].ToString();
                                    }

                                    if (res["P16pf"].ToString() != "")
                                    {
                                        button11.Visible = true;
                                        label14.ForeColor = Color.Red;
                                        idTest[7] = res["P16pf"].ToString();
                                    }

                                    //if (res["PCleaver"].ToString() != "")
                                    //{
                                    //    button14.Visible = true;
                                    //    label17.ForeColor = Color.Red;
                                    //    idTest[8] = res["PCleaver"].ToString();
                                    //}



                                    if (res["PPoms"].ToString() != "")
                                    {
                                        button7.Visible = true;
                                        label10.ForeColor = Color.Red;
                                        idTest[9] = res["PPoms"].ToString();
                                    }



                                    if (res["PActiAnteComp"].ToString() != "")
                                    {
                                        button19.Visible = true;
                                        label22.ForeColor = Color.Red;
                                        idTest[11] = res["PActiAnteComp"].ToString();
                                    }

                                    if (res["PMotivDepButt"].ToString() != "")
                                    {
                                        button2.Visible = true;
                                        label4.ForeColor = Color.Red;
                                        idTest[12] = res["PMotivDepButt"].ToString();
                                    }

                                    if (res["PTrsimple"].ToString() != "")
                                    {
                                        button3.Visible = true;
                                        label6.ForeColor = Color.Red;
                                        idTest[13] = res["PTrsimple"].ToString();
                                    }

                                    if (res["PTrcomple"].ToString() != "")
                                    {
                                        button8.Visible = true;
                                        label11.ForeColor = Color.Red;
                                        idTest[14] = res["PTrcomple"].ToString();
                                    }


                                    if (res["PTrcomples"].ToString() != "")
                                    {
                                        button16.Visible = true;
                                        label19.ForeColor = Color.Red;
                                        idTest[17] = res["PTrcomples"].ToString();
                                    }

                                    if (res["PResanti"].ToString() != "")
                                    {
                                        button18.Visible = true;
                                        label21.ForeColor = Color.Red;
                                        idTest[15] = res["PResanti"].ToString();
                                    }




                                    if (res["PIped"].ToString() != "")
                                    {
                                        button13.Visible = true;
                                        label16.ForeColor = Color.Red;
                                        idTest[18] = res["PIped"].ToString();
                                    }


                                    if (res["PTRN"].ToString() != "")
                                    {
                                        button21.Visible = true;
                                        label24.ForeColor = Color.Red;
                                        idTest[19] = res["PTRN"].ToString();
                                    }


                                    if (res["PAnsiedadCompetitiva"].ToString() != "")
                                    {
                                        button4.Visible = true;
                                        label7.ForeColor = Color.Red;
                                        idTest[8] = res["PAnsiedadCompetitiva"].ToString();
                                    }
                                }
                            }
                        }
                    }

                }
            }

        }

        private string buscarEtapa()
        {
            String etapa1 = "";
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
                                etapa1 = res["idEtapa"].ToString();

                            }
                        }
                    }
                }

            }
            return etapa1;
        }




        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void r_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Raven", nombreAtleta, idTest[1], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Eysenck", nombreAtleta, idTest[5], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Dominó", nombreAtleta, idTest[4], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Weil", nombreAtleta, idTest[3], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Idare (Rasgo)", nombreAtleta, idTest[21], fecha, null, null);
            prueba.ShowDialog();
        }
        private void button20_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Idare (Situacional)", nombreAtleta, idTest[0], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Catell", nombreAtleta, idTest[6], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("16Pf", nombreAtleta, idTest[7], fecha);
            //prueba.ShowDialog();
            _16PfFormView ds = new _16PfFormView(idTest[7], nombreAtleta, false, 0);
            ds.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Cualidades Volitivas", nombreAtleta, idTest[16], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Butt", nombreAtleta, idTest[12], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Cualidades Motivacionales", nombreAtleta, idTest[10], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Actitud Ante la Competencia", nombreAtleta, idTest[11], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /* VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("TRS", nombreAtleta, idTest[13], fecha,null,null);
             prueba.ShowDialog();
             Cursor.Show();*/
            TRS_Form3 res = new TRS_Form3(nombreAtleta, idTest[13], false, null, null);
            res.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("TRC", nombreAtleta, idTest[14], fecha, null, null);
            //prueba.ShowDialog();

            TRC_Form3 tRC_Form3 = new TRC_Form3(nombreAtleta, idTest[14], false, null, null);
            tRC_Form3.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {


            TRC_Form3 tRC_Form3 = new TRC_Form3(nombreAtleta, idTest[17], true, null, null);
            tRC_Form3.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Martens");
            //  prueba.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Idetem", nombreAtleta, idTest[2], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Iped", nombreAtleta, idTest[18], fecha, null, null);
            prueba.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            TRN_Form2 tRN_Form3 = new TRN_Form2(nombreAtleta, idTest[19], false, null, null);
            tRN_Form3.ShowDialog();
        }

        private void Pruebas_Load(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            RA_Form3 prueba = new RA_Form3(nombreAtleta, idTest[15], null, null, null);
            prueba.ShowDialog();
        }

        public void RefrescarEtapa()
        {
            String id = buscarEtapa();
            if (id != idEtapa)
            {
                idEtapa = id;

                limpiarPruebasDia();
                buscarPrueba();
            }
        }




        public void LimpiarTodosCampos()
        {
            comboBox1.Text = "Seleccione";
            comboBox3.Text = "Seleccione";
            comboBox1.Enabled = false;
            comboBox2.Text = "Seleccione";
            comboBox3.Enabled = false;
            simpleButton1.TabStop = false;
            limpiarPruebasDia();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            using (mainEntities f = new mainEntities())
            {

                using (SQLiteDataAdapter categoria = new SQLiteDataAdapter("select  distinct Entidad from DatosSujetos ", f.Database.Connection.ConnectionString.ToString()))
                {
                    DataTable data = new DataTable();
                    categoria.Fill(data);
                    comboBox2.DataSource = data;
                    comboBox2.DisplayMember = "Entidad";
                    comboBox2.ValueMember = "Entidad";

                    textBox1.Text = "";
                    comboBox1.Text = "Seleccione";
                }

                limpiarPruebasDia();
                comboBox3.Text = "Seleccione";

            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
     
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("POMS", nombreAtleta, idTest[9], fecha, null, null);
            prueba.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            VisualizarPruebasRealizada prueba = new VisualizarPruebasRealizada("Ansiedad Precompetitiva CSAI-2R", nombreAtleta, idTest[8], fecha, null, null);
            prueba.ShowDialog();
        }
    }
}
