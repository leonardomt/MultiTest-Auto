
using Multitest.ADOmodel;
using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace Multitest
{
    public partial class UserControlUser : UserControl
    {

        private static UserControlUser _instance;
        int idDatosSujeto = 0;
        AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        AutoCompleteStringCollection sourceOcupacion = new AutoCompleteStringCollection();
        
        public static UserControlUser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserControlUser();

                return _instance;
            }
        }

        public UserControlUser()
        {

            InitializeComponent();


            // SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            // tableLayoutPanel3.Visible = true;



        }



        private void User_Load(object sender, EventArgs e)
        {
            LLenarTabla();

        }


        public void LLenarTabla()
        {
            try
            {
                using (mainEntities entities = new mainEntities())
                {
                    dataGridView1.Rows.Clear();
                    using (SQLiteConnection c = new SQLiteConnection(entities.Database.Connection.ConnectionString))
                    {
                        c.Open();
                        using (SQLiteCommand comm1 = new SQLiteCommand("Select * from DatosSujetos ", c))
                        {
                            using (SQLiteDataReader read = comm1.ExecuteReader())
                            {

                                while (read.Read())
                                {
                                    dataGridView1.Rows.Add(new object[] {

                                     read.GetValue(read.GetOrdinal("NombreS")),
                        read.GetValue(read.GetOrdinal("PrimerApellido")),
                        read.GetValue(read.GetOrdinal("SegundoApellido")),
                        read.GetValue(read.GetOrdinal("NCarnetIdent")),
                        read.GetValue(read.GetOrdinal("Entidad")),
                        read.GetValue(read.GetOrdinal("Sexo")),
                        read.GetValue(read.GetOrdinal("Edad")),
                        read.GetValue(read.GetOrdinal("Ocupacion")),
                        read.GetValue(read.GetOrdinal("NivelEscolar")),
                          read.GetValue(read.GetOrdinal("IdDatosSujetos")),


                     });

                                }


                            }

                        }
                        c.Close();
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }

        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {

            if (textBoxCarnet.Text != "" && textBoxNombre.Text != "" && textBoxPrimerA.Text != "" && textBoxSegundoA.Text != "" && (comboBoxSexo.Text != "" && comboBoxSexo.Text != "Seleccione") && textBoxEdad.Text != ""
                && textBoxOcupacion.Text != "" && (comboBoxNivel.Text != "" && comboBoxNivel.Text != "Seleccione") && (textBox1.Text != "" && textBox1.Text != "Seleccione"))
            {
                DatosSujetos d = new DatosSujetos
                {

                    NCarnetIdent = textBoxCarnet.Text,
                    NombreS = textBoxNombre.Text,
                    PrimerApellido = textBoxPrimerA.Text,
                    SegundoApellido = textBoxSegundoA.Text,
                    Sexo = comboBoxSexo.Text,
                    Edad = textBoxEdad.Text,
                    Ocupacion = textBoxOcupacion.Text,
                    NivelEscolar = comboBoxNivel.Text,
                    Entidad = textBox1.Text,

                };

                string carnet = textBoxCarnet.Text;
                if (carnet.Length == 11)
                {
                    using (mainEntities db = new mainEntities())
                    {

                        DatosSujetos res = db.DatosSujetos.Where(s => s.NCarnetIdent == textBoxCarnet.Text).FirstOrDefault<DatosSujetos>();

                        if (res == null)
                        {
                            db.DatosSujetos.Add(d);
                            db.SaveChanges();
                            limpiarCampos();
                            LLenarTabla();
                            MessageBox.Show("Los datos se han guardado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {


                            if (res != null)
                            {
                                if (verificarSujeto(res, d))
                                {
                                    res.NombreS = d.NombreS;
                                    res.PrimerApellido = d.PrimerApellido;
                                    res.SegundoApellido = d.SegundoApellido;
                                    res.Sexo = d.Sexo;
                                    res.Entidad = d.Entidad;
                                    res.Ocupacion = d.Ocupacion;
                                    res.Edad = d.Edad;
                                    res.NivelEscolar = d.NivelEscolar;

                                    db.SaveChanges();

                                    limpiarCampos();
                                    LLenarTabla();

                                    MessageBox.Show("Los datos se han actualizados exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else

                                {

                                    MessageBox.Show("El sujeto ya existe en el sistema", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    limpiarCampos();

                                }
                            }


                        }
                    }
                }
                else
                {
                    MessageBox.Show("El Carnet de Identidad debe ser de 11 dígitos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }





        }

        private bool verificarSujeto(DatosSujetos res, DatosSujetos d)
        {
            bool result = true;

            if (res.NombreS == d.NombreS && res.PrimerApellido == d.PrimerApellido && res.SegundoApellido == d.SegundoApellido && res.Sexo == d.Sexo && res.Entidad == d.Entidad && res.Ocupacion == d.Ocupacion &&
            res.Edad == d.Edad &&
            res.NivelEscolar == d.NivelEscolar)

                result = false;


            return result;
        }

        private void limpiarCampos()
        {
            textBoxCarnet.Text = "";
            textBoxNombre.Text = "";
            textBoxPrimerA.Text = "";
            textBoxSegundoA.Text = "";
            comboBoxSexo.Text = "Seleccione";
            textBoxEdad.Text = "";
            textBoxOcupacion.Text = "";
            comboBoxNivel.Text = "Seleccione";
            textBox1.Text = "";
            simpleButton1.Text = "Insertar";
            textBoxCarnet.Enabled = true;

        }

        private void textBoxOcupacion_Click(object sender, EventArgs e)
        {
            if (sourceOcupacion.Count == 0)
            {
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
                        .Select<System.Data.DataRow, String>(x => x.Field<String>("Ocupacion"))
                        .ToArray();



                            sourceOcupacion.AddRange(postSource);
                            textBoxOcupacion.AutoCompleteCustomSource = sourceOcupacion;


                        }
                    }


                }
            }

        }
        

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (source.Count == 0)
            {


        
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
                        .Select<System.Data.DataRow, String>(x => x.Field<String>("Entidad"))
                        .ToArray();



                            source.AddRange(postSource);
                            textBox1.AutoCompleteCustomSource = source;


                        }
                    }


                }
            }

        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                textBoxCarnet.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBoxNombre.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBoxPrimerA.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBoxSegundoA.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                comboBoxSexo.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBoxEdad.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBoxOcupacion.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                comboBoxNivel.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                idDatosSujeto = Convert.ToInt32(dataGridView1.CurrentRow.Cells[9].Value.ToString());
                textBoxCarnet.Enabled = false;
                simpleButton1.Text = "Modificar";
            }
        }




        private void validarNumeros(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;


        }


        private void eliminar(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxNombre_Text_Changed(object sender, EventArgs e)
        {
            string str = textBoxNombre.Text;
            char[] ch = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }
            if (str != "")
                ch[0] = char.ToUpper(ch[0]);
            if(str.Length >= 2)
            {
                for (int i=0; i < str.Length; i++)
                {
                    if (i > 0)
                    {
                        if (Equals(ch[i - 1], ' '))
                        {
                            ch[i] = char.ToUpper(ch[i]);

                        }
                    }
                }
            }
            string s = new string(ch);
            textBoxNombre.Text = s;
            textBoxNombre.Select(textBoxNombre.Text.Length, 0);
        }

        private void textBox1_Text_Changed(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            char[] ch = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }
            if (str != "")
                ch[0] = char.ToUpper(ch[0]);
            if (str.Length >= 2)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 0)
                    {
                        if (Equals(ch[i - 1], ' '))
                        {
                            ch[i] = char.ToUpper(ch[i]);

                        }
                    }
                }
            }
            string s = new string(ch);
            textBox1.Text = s;
            textBox1.Select(textBox1.Text.Length, 0);
        }
        
        private void textBoxPrimerA_Text_Changed(object sender, EventArgs e)
        {
            string str = textBoxPrimerA.Text;
            char[] ch = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }
            if (str != "")
                ch[0] = char.ToUpper(ch[0]);
            if (str.Length >= 2)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 0)
                    {
                        if (Equals(ch[i - 1], ' '))
                        {
                            ch[i] = char.ToUpper(ch[i]);

                        }
                    }
                }
            }
            string s = new string(ch);
            textBoxPrimerA.Text = s;
            textBoxPrimerA.Select(textBoxPrimerA.Text.Length, 0);
        }
        
        private void textBoxSegundoA_Text_Changed(object sender, EventArgs e)
        {
            string str = textBoxSegundoA.Text;
            char[] ch = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }
            if (str != "")
                ch[0] = char.ToUpper(ch[0]);
            if (str.Length >= 2)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 0)
                    {
                        if (Equals(ch[i - 1], ' '))
                        {
                            ch[i] = char.ToUpper(ch[i]);

                        }
                    }
                }
            }
            string s = new string(ch);
            textBoxSegundoA.Text = s;
            textBoxSegundoA.Select(textBoxSegundoA.Text.Length, 0);
        }
        
        private void textBoxOcupacion_Text_Changed(object sender, EventArgs e)
        {
            string str = textBoxOcupacion.Text;
            char[] ch = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ch[i] = str[i];
            }
            if (str != "")
                ch[0] = char.ToUpper(ch[0]);
            if (str.Length >= 2)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (i > 0)
                    {
                        if (Equals(ch[i - 1], ' '))
                        {
                            ch[i] = char.ToUpper(ch[i]);

                        }
                    }
                }
            }
            string s = new string(ch);
            textBoxOcupacion.Text = s;
            textBoxOcupacion.Select(textBoxOcupacion.Text.Length, 0);
        }

        private void validarLetras(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                if (e.KeyChar != (char)Keys.Space)
                {
                    e.Handled = true;
                    return;
                }
            }
        }



        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea elimnar el sujeto?.", "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                eliminarSujeto();

        }

        private async void eliminarSujeto()
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos datos = await db.DatosSujetos.FindAsync(idDatosSujeto);

                db.DatosSujetos.Remove(datos);
                db.SaveChangesAsync();
                LLenarTabla();
                limpiarCampos();
                MessageBox.Show("Sujeto eliminado.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void comboBoxSexo_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = true;


        }

        private void comboBoxNivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LimpiarTodosCampos()
        {
            textBoxCarnet.Text = "";
            textBoxEdad.Text = "";
            textBox1.Text = "";
            textBoxNombre.Text = "";
            textBoxOcupacion.Text = "";
            textBoxPrimerA.Text = "";
            textBoxSegundoA.Text = "";
            textBoxCarnet.Enabled = true;

            comboBoxNivel.Text = "Seleccione";
            comboBoxSexo.Text = "Seleccione";

            button1.TabStop = false;
            simpleButton1.TabStop = false;
            simpleButton2.TabStop = false;

        }
    }
}
