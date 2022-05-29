using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Multitest
{
    public partial class GraficoVolitiv : Form
    {

        String PtoAutodAutocon;
        String PtoTenacidadResol;
        String PtoPersePersis;
        String PtoAutoIndepen;

        int AutodAutocon = 0;
        int TenacidadResol = 0;
        int PersePersis = 0;
        int AutoIndepen = 0;
        public GraficoVolitiv(String PtoAutodAutocon, String PtoTenacidadResol, String PtoPersePersis, String PtoAutoIndepen, string nombre)
        {

            this.PtoAutodAutocon = PtoAutodAutocon;
            this.PtoPersePersis = PtoPersePersis;
            this.PtoTenacidadResol = PtoTenacidadResol;
            this.PtoAutoIndepen = PtoAutoIndepen;
            InitializeComponent();
            label1.Text = "Sujeto: " + nombre;
            convertirPuntosAutoAutocon();
            convertirPuntosTenacidadResol();
            convertirPuntosPersePersis();
            convertirPuntosAutoIndepen();
        }


     
        private void convertirPuntosAutoIndepen()
        {
            if (PtoAutoIndepen == "6")
                AutoIndepen = 1;
            if (PtoAutoIndepen == "8")
                AutoIndepen = 3;
            if (PtoAutoIndepen == "9")
                AutoIndepen = 5;
            if (PtoAutoIndepen == "10")
                AutoIndepen = 12;
            if (PtoAutoIndepen == "11")
                AutoIndepen = 24;
            if (PtoAutoIndepen == "12")
                AutoIndepen = 31;
            if (PtoAutoIndepen == "13")
                AutoIndepen = 42;
            if (PtoAutoIndepen == "14")
                AutoIndepen = 61;
            if (PtoAutoIndepen == "15")
                AutoIndepen = 75;
            if (PtoAutoIndepen == "16")
                AutoIndepen = 83;
            if (PtoAutoIndepen == "17")
                AutoIndepen = 89;
            if (PtoAutoIndepen == "18")
                AutoIndepen = 94;
            if (PtoAutoIndepen == "19")
                AutoIndepen = 98;
            if (PtoAutoIndepen == "20")
                AutoIndepen = 99;

        }

        private void convertirPuntosPersePersis()
        {
            if (PtoPersePersis == "9")
                PersePersis = 1;
            if (PtoPersePersis == "10")
                PersePersis = 2;
            if (PtoPersePersis == "11")
                PersePersis = 5;
            if (PtoPersePersis == "12")
                PersePersis = 11;
            if (PtoPersePersis == "13")
                PersePersis = 15;
            if (PtoPersePersis == "14")
                PersePersis = 25;
            if (PtoPersePersis == "15")
                PersePersis = 34;
            if (PtoPersePersis == "16")
                PersePersis = 48;
            if (PtoPersePersis == "17")
                PersePersis = 58;
            if (PtoPersePersis == "18")
                PersePersis = 68;
            if (PtoPersePersis == "19")
                PersePersis = 81;
            if (PtoPersePersis == "20")
                PersePersis = 86;
            if (PtoPersePersis == "21")
                PersePersis = 89;
            if (PtoPersePersis == "22")
                PersePersis = 96;
            if (PtoPersePersis == "23")
                PersePersis = 98;
            if (PtoPersePersis == "24")
                PersePersis = 100;
        }

        private void convertirPuntosTenacidadResol()
        {
            if (PtoTenacidadResol == "2")
                TenacidadResol = 1;
            if (PtoTenacidadResol == "4")
                TenacidadResol = 4;
            if (PtoTenacidadResol == "5")
                TenacidadResol = 6;
            if (PtoTenacidadResol == "6")
                TenacidadResol = 9;
            if (PtoTenacidadResol == "7")
                TenacidadResol = 11;
            if (PtoTenacidadResol == "8")
                TenacidadResol = 22;
            if (PtoTenacidadResol == "9")
                TenacidadResol = 34;
            if (PtoTenacidadResol == "10")
                TenacidadResol = 43;
            if (PtoTenacidadResol == "11")
                TenacidadResol = 57;
            if (PtoTenacidadResol == "12")
                TenacidadResol = 70;
            if (PtoTenacidadResol == "13")
                TenacidadResol = 80;
            if (PtoTenacidadResol == "14")
                TenacidadResol = 91;
            if (PtoTenacidadResol == "15")
                TenacidadResol = 92;
            if (PtoTenacidadResol == "16")
                TenacidadResol = 94;
            if (PtoTenacidadResol == "17")
                TenacidadResol = 97;
            if (PtoTenacidadResol == "18")
                TenacidadResol = 99;
            if (PtoTenacidadResol == "20")
                TenacidadResol = 100;

        }

        private void convertirPuntosAutoAutocon()
        {



            if (PtoAutodAutocon == "4")
            {
                AutodAutocon = 1;


            }

            if (PtoAutodAutocon == "6")
            {
                AutodAutocon = 2;

            }
            if (PtoAutodAutocon == "7")
            {
                AutodAutocon = 5;

            }
            if (PtoAutodAutocon == "8")
            {
                AutodAutocon = 6;

            }
            if (PtoAutodAutocon == "9")
            {
                AutodAutocon = 12;

            }
            if (PtoAutodAutocon == "10")
            {
                AutodAutocon = 15;

            }
            if (PtoAutodAutocon == "11")
            {
                AutodAutocon = 25;

            }
            if (PtoAutodAutocon == "12")
            {
                AutodAutocon = 27;

            }
            if (PtoAutodAutocon == "13")
            {
                AutodAutocon = 51;

            }
            if (PtoAutodAutocon == "14")
            {
                AutodAutocon = 64;

            }
            if (PtoAutodAutocon == "15")
            {
                AutodAutocon = 79;

            }
            if (PtoAutodAutocon == "16")
            {
                AutodAutocon = 81;

            }
            if (PtoAutodAutocon == "17")
            {
                AutodAutocon = 88;

            }
            if (PtoAutodAutocon == "18")
            {
                AutodAutocon = 95;

            }
            if (PtoAutodAutocon == "19")
            {
                AutodAutocon = 98;

            }
            if (PtoAutodAutocon == "20")
            {
                AutodAutocon = 99;

            }
            if (PtoAutodAutocon == "21")
            {
                AutodAutocon = 100;

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {


            AddPoint();


            chart1.Series["Series2"].Points.AddXY("Autonomia e Indepencia", AutodAutocon);
            chart1.Series["Series2"].Points.AddXY("Tenacidad y Resolución", TenacidadResol);
            chart1.Series["Series2"].Points.AddXY("Persistencia y Perseverancia", PersePersis);
            chart1.Series["Series2"].Points.AddXY("Autocontrol y Autodominio", AutoIndepen);


            //chart1.Series["Series1"].Points[0].Label = PtoAutodAutocon.ToString() + " ptos";
            //chart1.Series["Series1"].Points[1].Label = PtoTenacidadResol.ToString() + " ptos";
            //chart1.Series["Series1"].Points[2].Label = PtoPersePersis.ToString() + " ptos";
            //chart1.Series["Series1"].Points[3].Label = PtoAutoIndepen.ToString() + " ptos";
        }

        private void AddPoint()
        {
            chart1.Series["Series1"].Points.AddXY("Autonomia e Indepencia", 1);
            chart1.Series["Series3"].Points.AddXY("Autonomia e Indepencia", 2);
            chart1.Series["Series4"].Points.AddXY("Autonomia e Indepencia", 5);
            chart1.Series["Series5"].Points.AddXY("Autonomia e Indepencia", 6);
            chart1.Series["Series6"].Points.AddXY("Autonomia e Indepencia", 12);
            chart1.Series["Series7"].Points.AddXY("Autonomia e Indepencia", 15);
            chart1.Series["Series8"].Points.AddXY("Autonomia e Indepencia", 25);
            chart1.Series["Series9"].Points.AddXY("Autonomia e Indepencia", 27);
            chart1.Series["Series10"].Points.AddXY("Autonomia e Indepencia", 51);
            chart1.Series["Series11"].Points.AddXY("Autonomia e Indepencia", 64);
            chart1.Series["Series12"].Points.AddXY("Autonomia e Indepencia", 79);
            chart1.Series["Series13"].Points.AddXY("Autonomia e Indepencia", 81);
            chart1.Series["Series14"].Points.AddXY("Autonomia e Indepencia", 88);
            chart1.Series["Series15"].Points.AddXY("Autonomia e Indepencia", 95);
            chart1.Series["Series16"].Points.AddXY("Autonomia e Indepencia", 98);
            chart1.Series["Series17"].Points.AddXY("Autonomia e Indepencia", 99);
            chart1.Series["Series18"].Points.AddXY("Autonomia e Indepencia", 100);


            chart1.Series["Series1"].Points.AddXY("Tenacidad y Resolución", 1);
            chart1.Series["Series3"].Points.AddXY("Tenacidad y Resolución", 4);
            chart1.Series["Series4"].Points.AddXY("Tenacidad y Resolución", 6);
            chart1.Series["Series5"].Points.AddXY("Tenacidad y Resolución", 9);
            chart1.Series["Series6"].Points.AddXY("Tenacidad y Resolución", 11);
            chart1.Series["Series7"].Points.AddXY("Tenacidad y Resolución", 22);
            chart1.Series["Series8"].Points.AddXY("Tenacidad y Resolución", 34);
            chart1.Series["Series9"].Points.AddXY("Tenacidad y Resolución", 43);
            chart1.Series["Series10"].Points.AddXY("Tenacidad y Resolución", 57);
            chart1.Series["Series11"].Points.AddXY("Tenacidad y Resolución", 70);
            chart1.Series["Series12"].Points.AddXY("Tenacidad y Resolución", 80);
            chart1.Series["Series13"].Points.AddXY("Tenacidad y Resolución", 91);
            chart1.Series["Series14"].Points.AddXY("Tenacidad y Resolución", 92);
            chart1.Series["Series15"].Points.AddXY("Tenacidad y Resolución", 94);
            chart1.Series["Series16"].Points.AddXY("Tenacidad y Resolución", 97);
            chart1.Series["Series17"].Points.AddXY("Tenacidad y Resolución", 99);
            chart1.Series["Series18"].Points.AddXY("Tenacidad y Resolución", 100);



            chart1.Series["Series1"].Points.AddXY("Persistencia y Perseverancia", 1);
            chart1.Series["Series3"].Points.AddXY("Persistencia y Perseverancia", 2);
            chart1.Series["Series4"].Points.AddXY("Persistencia y Perseverancia", 5);
            chart1.Series["Series5"].Points.AddXY("Persistencia y Perseverancia", 11);
            chart1.Series["Series6"].Points.AddXY("Persistencia y Perseverancia", 15);
            chart1.Series["Series7"].Points.AddXY("Persistencia y Perseverancia", 25);
            chart1.Series["Series8"].Points.AddXY("Persistencia y Perseverancia", 34);
            chart1.Series["Series9"].Points.AddXY("Persistencia y Perseverancia", 48);
            chart1.Series["Series10"].Points.AddXY("Persistencia y Perseverancia", 58);
            chart1.Series["Series11"].Points.AddXY("Persistencia y Perseverancia", 68);
            chart1.Series["Series12"].Points.AddXY("Persistencia y Perseverancia", 81);
            chart1.Series["Series13"].Points.AddXY("Persistencia y Perseverancia", 86);
            chart1.Series["Series14"].Points.AddXY("Persistencia y Perseverancia", 89);
            chart1.Series["Series15"].Points.AddXY("Persistencia y Perseverancia", 96);
            chart1.Series["Series16"].Points.AddXY("Persistencia y Perseverancia", 98);
            chart1.Series["Series17"].Points.AddXY("Persistencia y Perseverancia", 100);




            chart1.Series["Series1"].Points.AddXY("Autocontrol y Autodominio", 1);
            chart1.Series["Series3"].Points.AddXY("Autocontrol y Autodominio", 3);
            chart1.Series["Series4"].Points.AddXY("Autocontrol y Autodominio", 5);
            chart1.Series["Series5"].Points.AddXY("Autocontrol y Autodominio", 12);
            chart1.Series["Series6"].Points.AddXY("Autocontrol y Autodominio", 24);
            chart1.Series["Series7"].Points.AddXY("Autocontrol y Autodominio", 31);
            chart1.Series["Series8"].Points.AddXY("Autocontrol y Autodominio", 42);
            chart1.Series["Series9"].Points.AddXY("Autocontrol y Autodominio", 61);
            chart1.Series["Series10"].Points.AddXY("Autocontrol y Autodominio", 75);
            chart1.Series["Series11"].Points.AddXY("Autocontrol y Autodominio", 83);
            chart1.Series["Series12"].Points.AddXY("Autocontrol y Autodominio", 89);
            chart1.Series["Series13"].Points.AddXY("Autocontrol y Autodominio", 94);
            chart1.Series["Series14"].Points.AddXY("Autocontrol y Autodominio", 98);
            chart1.Series["Series15"].Points.AddXY("Autocontrol y Autodominio", 99);



            chart1.Series["Series1"].Points[0].Label = "4";
            chart1.Series["Series1"].Points[1].Label = "2";
            chart1.Series["Series1"].Points[2].Label = "9";
            chart1.Series["Series1"].Points[3].Label = "6";

            chart1.Series["Series3"].Points[0].Label = "6";
            chart1.Series["Series3"].Points[1].Label = "4";
            chart1.Series["Series3"].Points[2].Label = "10";
            chart1.Series["Series3"].Points[3].Label = "8";

            chart1.Series["Series4"].Points[0].Label = "7";
            chart1.Series["Series4"].Points[1].Label = "5";
            chart1.Series["Series4"].Points[2].Label = "11";
            chart1.Series["Series4"].Points[3].Label = "9";

            chart1.Series["Series5"].Points[0].Label = "8";
            chart1.Series["Series5"].Points[1].Label = "6";
            chart1.Series["Series5"].Points[2].Label = "12";
            chart1.Series["Series5"].Points[3].Label = "10";


            chart1.Series["Series6"].Points[0].Label = "9";
            chart1.Series["Series6"].Points[1].Label = "7";
            chart1.Series["Series6"].Points[2].Label = "13";
            chart1.Series["Series6"].Points[3].Label = "11";


            chart1.Series["Series7"].Points[0].Label = "10";
            chart1.Series["Series7"].Points[1].Label = "8";
            chart1.Series["Series7"].Points[2].Label = "14";
            chart1.Series["Series7"].Points[3].Label = "12";

            chart1.Series["Series8"].Points[0].Label = "11";
            chart1.Series["Series8"].Points[1].Label = "9";
            chart1.Series["Series8"].Points[2].Label = "15";
            chart1.Series["Series8"].Points[3].Label = "13";


            chart1.Series["Series9"].Points[0].Label = "12";
            chart1.Series["Series9"].Points[1].Label = "10";
            chart1.Series["Series9"].Points[2].Label = "16";
            chart1.Series["Series9"].Points[3].Label = "14";

            chart1.Series["Series10"].Points[0].Label = "13";
            chart1.Series["Series10"].Points[1].Label = "11";
            chart1.Series["Series10"].Points[2].Label = "17";
            chart1.Series["Series10"].Points[3].Label = "15";

            chart1.Series["Series11"].Points[0].Label = "14";
            chart1.Series["Series11"].Points[1].Label = "12";
            chart1.Series["Series11"].Points[2].Label = "18";
            chart1.Series["Series11"].Points[3].Label = "16";

            chart1.Series["Series12"].Points[0].Label = "15";
            chart1.Series["Series12"].Points[1].Label = "13";
            chart1.Series["Series12"].Points[2].Label = "19";
            chart1.Series["Series12"].Points[3].Label = "17";


            chart1.Series["Series13"].Points[0].Label = "16";
            chart1.Series["Series13"].Points[1].Label = "14";
            chart1.Series["Series13"].Points[2].Label = "20";
            chart1.Series["Series13"].Points[3].Label = "18";


            chart1.Series["Series14"].Points[0].Label = "17";
            chart1.Series["Series14"].Points[1].Label = "15";
            chart1.Series["Series14"].Points[2].Label = "21";
            chart1.Series["Series14"].Points[3].Label = "19";

            chart1.Series["Series15"].Points[0].Label = "18";
            chart1.Series["Series15"].Points[1].Label = "16";
            chart1.Series["Series15"].Points[2].Label = "22";
            chart1.Series["Series15"].Points[3].Label = "23";


            chart1.Series["Series16"].Points[0].Label = "19";
            chart1.Series["Series16"].Points[1].Label = "17";
            chart1.Series["Series16"].Points[2].Label = "23";



            chart1.Series["Series17"].Points[0].Label = "20";
            chart1.Series["Series17"].Points[1].Label = "18";
            chart1.Series["Series17"].Points[2].Label = "24";


            chart1.Series["Series18"].Points[0].Label = "21";
            chart1.Series["Series18"].Points[1].Label = "20";


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
