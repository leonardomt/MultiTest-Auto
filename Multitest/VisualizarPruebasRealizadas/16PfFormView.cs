using Microsoft.Office.Interop.Word;
using Multitest.ADOmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class _16PfFormView : Form
    {
        string idPrueba;
        string nombreAtleta;
        String etapa;
        private String fecha;
        string modalidad;
        String edad;
        String deporte;
       

        Pru16pf test;
        List<Label> listLabel;
        public _16PfFormView(String idPrueba, String nombreAtleta, bool visible,int idAtleta)
        {
            InitializeComponent();

            this.idPrueba = idPrueba;
            this.nombreAtleta = nombreAtleta;
            listLabel = new List<Label>();

            label307.Text = nombreAtleta;
            button1.Visible = visible;
            if (visible)
            {
                buscarEtapa();
                buscarAtleta(idAtleta);
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _16PfFormView_Load(object sender, EventArgs e)
        {
            buscarPrueba();
            pintarGrafico();
            pintarX();
        }

        private void pintarX()
        {
            foreach (var item in tableLayoutPanel9.Controls)
            {
                Label lab = item as Label;

                if (lab.Text == "X")
                {
                    lab.ForeColor = Color.DarkOrange;

                }
            }
        }

        private void pintarGrafico()
        {
            label274.Text = test.AnotBrutaA;
            label275.Text = test.AnotBrutaB;
            label276.Text = test.AnotBrutaC;
            label277.Text = test.AnotBrutaE;
            label278.Text = test.AnotBrutaF;
            label279.Text = test.AnotBrutaG;
            label280.Text = test.AnotBrutaH;
            label281.Text = test.AnotBrutaI;
            label282.Text = test.AnotBrutaL;
            label283.Text = test.AnotBrutaM;
            label284.Text = test.AnotBrutaN;
            label285.Text = test.AnotBrutaO;
            label286.Text = test.AnotBrutaQ1;
            label287.Text = test.AnotBrutaQ2;
            label288.Text = test.AnotBrutaQ3;
            label289.Text = test.AnotBrutaQ4;

            label290.Text = test.AnotPesadaA;
            label291.Text = test.AnotPesadaB;
            label292.Text = test.AnotPesadaC;
            label293.Text = test.AnotPesadaE;
            label294.Text = test.AnotPesadaF;
            label295.Text = test.AnotPesadaG;
            label296.Text = test.AnotPesadaH;
            label297.Text = test.AnotPesadaI;
            label298.Text = test.AnotPesadaL;
            label299.Text = test.AnotPesadaM;
            label300.Text = test.AnotPesadaN;
            label301.Text = test.AnotPesadaO;
            label302.Text = test.AnotPesadaQ1;
            label303.Text = test.AnotPesadaQ2;
            label304.Text = test.AnotPesadaQ3;
            label305.Text = test.AnotPesadaQ4;




            label53.Text = test.AnotPesadaA == "1" ? "X" : "•";
            label109.Text = test.AnotPesadaA == "2" ? "X" : "•";
            label55.Text = test.AnotPesadaA == "3" ? "X" : "•";
            label110.Text = test.AnotPesadaA == "4" ? "X" : "•";
            label111.Text = test.AnotPesadaA == "5" ? "X" : "•";
            label112.Text = test.AnotPesadaA == "6" ? "X" : "•";
            label113.Text = test.AnotPesadaA == "7" ? "X" : "•";
            label114.Text = test.AnotPesadaA == "8" ? "X" : "•";
            label115.Text = test.AnotPesadaA == "9" ? "X" : "•";
            label116.Text = test.AnotPesadaA == "10" ? "X" : "•";

            if (label53.Text == "X")
                listLabel.Add(label53);
            if (label55.Text == "X")
                listLabel.Add(label55);
            if (label109.Text == "X")
                listLabel.Add(label109);
            if (label110.Text == "X")
                listLabel.Add(label110);
            if (label111.Text == "X")
                listLabel.Add(label111);
            if (label112.Text == "X")
                listLabel.Add(label112);
            if (label113.Text == "X")
                listLabel.Add(label113);
            if (label114.Text == "X")
                listLabel.Add(label114);
            if (label115.Text == "X")
                listLabel.Add(label115);
            if (label116.Text == "X")
                listLabel.Add(label116);


            label56.Text = test.AnotPesadaB == "1" ? "X" : "•";
            label117.Text = test.AnotPesadaB == "2" ? "X" : "•";
            label57.Text = test.AnotPesadaB == "3" ? "X" : "•";
            label118.Text = test.AnotPesadaB == "4" ? "X" : "•";
            label119.Text = test.AnotPesadaB == "5" ? "X" : "•";
            label120.Text = test.AnotPesadaB == "6" ? "X" : "•";
            label121.Text = test.AnotPesadaB == "7" ? "X" : "•";
            label122.Text = test.AnotPesadaB == "8" ? "X" : "•";
            label123.Text = test.AnotPesadaB == "9" ? "X" : "•";
            label124.Text = test.AnotPesadaB == "10" ? "X" : "•";


            if (label56.Text == "X")
                listLabel.Add(label56);
            if (label117.Text == "X")
                listLabel.Add(label117);
            if (label57.Text == "X")
                listLabel.Add(label57);
            if (label118.Text == "X")
                listLabel.Add(label118);
            if (label119.Text == "X")
                listLabel.Add(label119);
            if (label120.Text == "X")
                listLabel.Add(label120);
            if (label121.Text == "X")
                listLabel.Add(label121);
            if (label122.Text == "X")
                listLabel.Add(label122);
            if (label123.Text == "X")
                listLabel.Add(label123);
            if (label124.Text == "X")
                listLabel.Add(label124);









            label58.Text = test.AnotPesadaC == "1" ? "X" : "•";
            label125.Text = test.AnotPesadaC == "2" ? "X" : "•";
            label59.Text = test.AnotPesadaC == "3" ? "X" : "•";
            label126.Text = test.AnotPesadaC == "4" ? "X" : "•";
            label127.Text = test.AnotPesadaC == "5" ? "X" : "•";
            label128.Text = test.AnotPesadaC == "6" ? "X" : "•";
            label129.Text = test.AnotPesadaC == "7" ? "X" : "•";
            label130.Text = test.AnotPesadaC == "8" ? "X" : "•";
            label131.Text = test.AnotPesadaC == "9" ? "X" : "•";
            label132.Text = test.AnotPesadaC == "10" ? "X" : "•";



            if (label58.Text == "X")
                listLabel.Add(label58);
            if (label125.Text == "X")
                listLabel.Add(label125);
            if (label59.Text == "X")
                listLabel.Add(label59);
            if (label126.Text == "X")
                listLabel.Add(label126);
            if (label127.Text == "X")
                listLabel.Add(label127);
            if (label128.Text == "X")
                listLabel.Add(label128);
            if (label129.Text == "X")
                listLabel.Add(label129);
            if (label130.Text == "X")
                listLabel.Add(label130);
            if (label131.Text == "X")
                listLabel.Add(label131);
            if (label132.Text == "X")
                listLabel.Add(label132);









            label60.Text = test.AnotPesadaE == "1" ? "X" : "•";
            label133.Text = test.AnotPesadaE == "2" ? "X" : "•";
            label61.Text = test.AnotPesadaE == "3" ? "X" : "•";
            label134.Text = test.AnotPesadaE == "4" ? "X" : "•";
            label135.Text = test.AnotPesadaE == "5" ? "X" : "•";
            label136.Text = test.AnotPesadaE == "6" ? "X" : "•";
            label137.Text = test.AnotPesadaE == "7" ? "X" : "•";
            label138.Text = test.AnotPesadaE == "8" ? "X" : "•";
            label139.Text = test.AnotPesadaE == "9" ? "X" : "•";
            label140.Text = test.AnotPesadaE == "10" ? "X" : "•";


            if (label60.Text == "X")
                listLabel.Add(label60);
            if (label133.Text == "X")
                listLabel.Add(label133);
            if (label61.Text == "X")
                listLabel.Add(label61);
            if (label134.Text == "X")
                listLabel.Add(label134);
            if (label135.Text == "X")
                listLabel.Add(label135);
            if (label136.Text == "X")
                listLabel.Add(label136);
            if (label137.Text == "X")
                listLabel.Add(label137);
            if (label138.Text == "X")
                listLabel.Add(label138);
            if (label139.Text == "X")
                listLabel.Add(label139);
            if (label140.Text == "X")
                listLabel.Add(label140);







            label62.Text = test.AnotPesadaF == "1" ? "X" : "•";
            label141.Text = test.AnotPesadaF == "2" ? "X" : "•";
            label63.Text = test.AnotPesadaF == "3" ? "X" : "•";
            label142.Text = test.AnotPesadaF == "4" ? "X" : "•";
            label143.Text = test.AnotPesadaF == "5" ? "X" : "•";
            label144.Text = test.AnotPesadaF == "6" ? "X" : "•";
            label145.Text = test.AnotPesadaF == "7" ? "X" : "•";
            label146.Text = test.AnotPesadaF == "8" ? "X" : "•";
            label147.Text = test.AnotPesadaF == "9" ? "X" : "•";
            label148.Text = test.AnotPesadaF == "10" ? "X" : "•";


            if (label62.Text == "X")
                listLabel.Add(label62);
            if (label141.Text == "X")
                listLabel.Add(label141);
            if (label63.Text == "X")
                listLabel.Add(label63);
            if (label142.Text == "X")
                listLabel.Add(label142);
            if (label143.Text == "X")
                listLabel.Add(label143);
            if (label144.Text == "X")
                listLabel.Add(label144);
            if (label145.Text == "X")
                listLabel.Add(label145);
            if (label146.Text == "X")
                listLabel.Add(label146);
            if (label147.Text == "X")
                listLabel.Add(label147);
            if (label148.Text == "X")
                listLabel.Add(label148);



            label164.Text = test.AnotPesadaG == "1" ? "X" : "•";
            label149.Text = test.AnotPesadaG == "2" ? "X" : "•";
            label64.Text = test.AnotPesadaG == "3" ? "X" : "•";
            label150.Text = test.AnotPesadaG == "4" ? "X" : "•";
            label151.Text = test.AnotPesadaG == "5" ? "X" : "•";
            label152.Text = test.AnotPesadaG == "6" ? "X" : "•";
            label153.Text = test.AnotPesadaG == "7" ? "X" : "•";
            label154.Text = test.AnotPesadaG == "8" ? "X" : "•";
            label155.Text = test.AnotPesadaG == "9" ? "X" : "•";
            label156.Text = test.AnotPesadaG == "10" ? "X" : "•";


            if (label164.Text == "X")
                listLabel.Add(label164);
            if (label149.Text == "X")
                listLabel.Add(label149);
            if (label64.Text == "X")
                listLabel.Add(label64);
            if (label150.Text == "X")
                listLabel.Add(label150);
            if (label151.Text == "X")
                listLabel.Add(label151);
            if (label152.Text == "X")
                listLabel.Add(label152);
            if (label153.Text == "X")
                listLabel.Add(label153);
            if (label154.Text == "X")
                listLabel.Add(label154);
            if (label155.Text == "X")
                listLabel.Add(label155);
            if (label156.Text == "X")
                listLabel.Add(label156);



            label66.Text = test.AnotPesadaH == "1" ? "X" : "•";
            label157.Text = test.AnotPesadaH == "2" ? "X" : "•";
            label67.Text = test.AnotPesadaH == "3" ? "X" : "•";
            label158.Text = test.AnotPesadaH == "4" ? "X" : "•";
            label159.Text = test.AnotPesadaH == "5" ? "X" : "•";
            label160.Text = test.AnotPesadaH == "6" ? "X" : "•";
            label161.Text = test.AnotPesadaH == "7" ? "X" : "•";
            label162.Text = test.AnotPesadaH == "8" ? "X" : "•";
            label163.Text = test.AnotPesadaH == "9" ? "X" : "•";
            label164.Text = test.AnotPesadaH == "10" ? "X" : "•";


            if (label66.Text == "X")
                listLabel.Add(label66);
            if (label157.Text == "X")
                listLabel.Add(label157);
            if (label67.Text == "X")
                listLabel.Add(label67);
            if (label158.Text == "X")
                listLabel.Add(label158);
            if (label159.Text == "X")
                listLabel.Add(label159);
            if (label160.Text == "X")
                listLabel.Add(label160);
            if (label161.Text == "X")
                listLabel.Add(label161);
            if (label162.Text == "X")
                listLabel.Add(label162);
            if (label163.Text == "X")
                listLabel.Add(label163);
            if (label164.Text == "X")
                listLabel.Add(label164);



            label68.Text = test.AnotPesadaI == "1" ? "X" : "•";
            label165.Text = test.AnotPesadaI == "2" ? "X" : "•";
            label69.Text = test.AnotPesadaI == "3" ? "X" : "•";
            label166.Text = test.AnotPesadaI == "4" ? "X" : "•";
            label167.Text = test.AnotPesadaI == "5" ? "X" : "•";
            label168.Text = test.AnotPesadaI == "6" ? "X" : "•";
            label169.Text = test.AnotPesadaI == "7" ? "X" : "•";
            label170.Text = test.AnotPesadaI == "8" ? "X" : "•";
            label171.Text = test.AnotPesadaI == "9" ? "X" : "•";
            label172.Text = test.AnotPesadaI == "10" ? "X" : "•";


            if (label68.Text == "X")
                listLabel.Add(label68);
            if (label165.Text == "X")
                listLabel.Add(label165);
            if (label69.Text == "X")
                listLabel.Add(label69);
            if (label166.Text == "X")
                listLabel.Add(label166);
            if (label167.Text == "X")
                listLabel.Add(label167);
            if (label168.Text == "X")
                listLabel.Add(label168);
            if (label169.Text == "X")
                listLabel.Add(label169);
            if (label170.Text == "X")
                listLabel.Add(label170);
            if (label171.Text == "X")
                listLabel.Add(label171);
            if (label172.Text == "X")
                listLabel.Add(label172);


            label70.Text = test.AnotPesadaL == "1" ? "X" : "•";
            label173.Text = test.AnotPesadaL == "2" ? "X" : "•";
            label71.Text = test.AnotPesadaL == "3" ? "X" : "•";
            label174.Text = test.AnotPesadaL == "4" ? "X" : "•";
            label175.Text = test.AnotPesadaL == "5" ? "X" : "•";
            label176.Text = test.AnotPesadaL == "6" ? "X" : "•";
            label177.Text = test.AnotPesadaL == "7" ? "X" : "•";
            label178.Text = test.AnotPesadaL == "8" ? "X" : "•";
            label179.Text = test.AnotPesadaL == "9" ? "X" : "•";
            label180.Text = test.AnotPesadaL == "10" ? "X" : "•";


            if (label70.Text == "X")
                listLabel.Add(label70);
            if (label173.Text == "X")
                listLabel.Add(label173);
            if (label71.Text == "X")
                listLabel.Add(label71);
            if (label174.Text == "X")
                listLabel.Add(label174);
            if (label175.Text == "X")
                listLabel.Add(label175);
            if (label176.Text == "X")
                listLabel.Add(label176);
            if (label177.Text == "X")
                listLabel.Add(label177);
            if (label178.Text == "X")
                listLabel.Add(label178);
            if (label179.Text == "X")
                listLabel.Add(label179);
            if (label180.Text == "X")
                listLabel.Add(label180);


            label75.Text = test.AnotPesadaM == "1" ? "X" : "•";
            label181.Text = test.AnotPesadaM == "2" ? "X" : "•";
            label76.Text = test.AnotPesadaM == "3" ? "X" : "•";
            label182.Text = test.AnotPesadaM == "4" ? "X" : "•";
            label183.Text = test.AnotPesadaM == "5" ? "X" : "•";
            label184.Text = test.AnotPesadaM == "6" ? "X" : "•";
            label185.Text = test.AnotPesadaM == "7" ? "X" : "•";
            label186.Text = test.AnotPesadaM == "8" ? "X" : "•";
            label187.Text = test.AnotPesadaM == "9" ? "X" : "•";
            label188.Text = test.AnotPesadaM == "10" ? "X" : "•";


            if (label75.Text == "X")
                listLabel.Add(label75);
            if (label181.Text == "X")
                listLabel.Add(label181);
            if (label76.Text == "X")
                listLabel.Add(label76);
            if (label182.Text == "X")
                listLabel.Add(label182);
            if (label183.Text == "X")
                listLabel.Add(label183);
            if (label184.Text == "X")
                listLabel.Add(label184);
            if (label185.Text == "X")
                listLabel.Add(label185);
            if (label186.Text == "X")
                listLabel.Add(label186);
            if (label187.Text == "X")
                listLabel.Add(label187);
            if (label188.Text == "X")
                listLabel.Add(label188);



            label77.Text = test.AnotPesadaN == "1" ? "X" : "•";
            label189.Text = test.AnotPesadaN == "2" ? "X" : "•";
            label78.Text = test.AnotPesadaN == "3" ? "X" : "•";
            label190.Text = test.AnotPesadaN == "4" ? "X" : "•";
            label191.Text = test.AnotPesadaN == "5" ? "X" : "•";
            label192.Text = test.AnotPesadaN == "6" ? "X" : "•";
            label193.Text = test.AnotPesadaN == "7" ? "X" : "•";
            label194.Text = test.AnotPesadaN == "8" ? "X" : "•";
            label195.Text = test.AnotPesadaN == "9" ? "X" : "•";
            label196.Text = test.AnotPesadaN == "10" ? "X" : "•";


            if (label77.Text == "X")
                listLabel.Add(label77);
            if (label189.Text == "X")
                listLabel.Add(label189);
            if (label78.Text == "X")
                listLabel.Add(label78);
            if (label190.Text == "X")
                listLabel.Add(label190);
            if (label191.Text == "X")
                listLabel.Add(label191);
            if (label192.Text == "X")
                listLabel.Add(label192);
            if (label193.Text == "X")
                listLabel.Add(label193);
            if (label194.Text == "X")
                listLabel.Add(label194);
            if (label195.Text == "X")
                listLabel.Add(label195);
            if (label196.Text == "X")
                listLabel.Add(label196);




            label79.Text = test.AnotPesadaO == "1" ? "X" : "•";
            label197.Text = test.AnotPesadaO == "2" ? "X" : "•";
            label80.Text = test.AnotPesadaO == "3" ? "X" : "•";
            label198.Text = test.AnotPesadaO == "4" ? "X" : "•";
            label199.Text = test.AnotPesadaO == "5" ? "X" : "•";
            label200.Text = test.AnotPesadaO == "6" ? "X" : "•";
            label201.Text = test.AnotPesadaO == "7" ? "X" : "•";
            label202.Text = test.AnotPesadaO == "8" ? "X" : "•";
            label203.Text = test.AnotPesadaO == "9" ? "X" : "•";
            label204.Text = test.AnotPesadaO == "10" ? "X" : "•";



            if (label79.Text == "X")
                listLabel.Add(label79);
            if (label197.Text == "X")
                listLabel.Add(label197);
            if (label80.Text == "X")
                listLabel.Add(label80);
            if (label198.Text == "X")
                listLabel.Add(label198);
            if (label199.Text == "X")
                listLabel.Add(label199);
            if (label200.Text == "X")
                listLabel.Add(label200);
            if (label201.Text == "X")
                listLabel.Add(label201);
            if (label202.Text == "X")
                listLabel.Add(label202);
            if (label203.Text == "X")
                listLabel.Add(label203);
            if (label204.Text == "X")
                listLabel.Add(label204);




            label81.Text = test.AnotPesadaQ1 == "1" ? "X" : "•";
            label205.Text = test.AnotPesadaQ1 == "2" ? "X" : "•";
            label82.Text = test.AnotPesadaQ1 == "3" ? "X" : "•";
            label206.Text = test.AnotPesadaQ1 == "4" ? "X" : "•";
            label207.Text = test.AnotPesadaQ1 == "5" ? "X" : "•";
            label208.Text = test.AnotPesadaQ1 == "6" ? "X" : "•";
            label209.Text = test.AnotPesadaQ1 == "7" ? "X" : "•";
            label210.Text = test.AnotPesadaQ1 == "8" ? "X" : "•";
            label211.Text = test.AnotPesadaQ1 == "9" ? "X" : "•";
            label212.Text = test.AnotPesadaQ1 == "10" ? "X" : "•";



            if (label81.Text == "X")
                listLabel.Add(label81);
            if (label205.Text == "X")
                listLabel.Add(label205);
            if (label82.Text == "X")
                listLabel.Add(label82);
            if (label206.Text == "X")
                listLabel.Add(label206);
            if (label207.Text == "X")
                listLabel.Add(label207);
            if (label208.Text == "X")
                listLabel.Add(label208);
            if (label209.Text == "X")
                listLabel.Add(label209);
            if (label210.Text == "X")
                listLabel.Add(label210);
            if (label211.Text == "X")
                listLabel.Add(label211);
            if (label212.Text == "X")
                listLabel.Add(label212);







            label83.Text = test.AnotPesadaQ2 == "1" ? "X" : "•";
            label213.Text = test.AnotPesadaQ2 == "2" ? "X" : "•";
            label84.Text = test.AnotPesadaQ2 == "3" ? "X" : "•";
            label214.Text = test.AnotPesadaQ2 == "4" ? "X" : "•";
            label215.Text = test.AnotPesadaQ2 == "5" ? "X" : "•";
            label216.Text = test.AnotPesadaQ2 == "6" ? "X" : "•";
            label217.Text = test.AnotPesadaQ2 == "7" ? "X" : "•";
            label218.Text = test.AnotPesadaQ2 == "8" ? "X" : "•";
            label219.Text = test.AnotPesadaQ2 == "9" ? "X" : "•";
            label220.Text = test.AnotPesadaQ2 == "10" ? "X" : "•";

            if (label83.Text == "X")
                listLabel.Add(label83);
            if (label213.Text == "X")
                listLabel.Add(label213);
            if (label84.Text == "X")
                listLabel.Add(label84);
            if (label214.Text == "X")
                listLabel.Add(label214);
            if (label215.Text == "X")
                listLabel.Add(label215);
            if (label216.Text == "X")
                listLabel.Add(label216);
            if (label217.Text == "X")
                listLabel.Add(label217);
            if (label218.Text == "X")
                listLabel.Add(label218);
            if (label219.Text == "X")
                listLabel.Add(label219);
            if (label220.Text == "X")
                listLabel.Add(label220);




            label85.Text = test.AnotPesadaQ3 == "1" ? "X" : "•";
            label221.Text = test.AnotPesadaQ3 == "2" ? "X" : "•";
            label86.Text = test.AnotPesadaQ3 == "3" ? "X" : "•";
            label222.Text = test.AnotPesadaQ3 == "4" ? "X" : "•";
            label223.Text = test.AnotPesadaQ3 == "5" ? "X" : "•";
            label224.Text = test.AnotPesadaQ3 == "6" ? "X" : "•";
            label225.Text = test.AnotPesadaQ3 == "7" ? "X" : "•";
            label226.Text = test.AnotPesadaQ3 == "8" ? "X" : "•";
            label227.Text = test.AnotPesadaQ3 == "9" ? "X" : "•";
            label228.Text = test.AnotPesadaQ3 == "10" ? "X" : "•";


            if (label85.Text == "X")
                listLabel.Add(label85);
            if (label221.Text == "X")
                listLabel.Add(label221);
            if (label86.Text == "X")
                listLabel.Add(label86);
            if (label222.Text == "X")
                listLabel.Add(label222);
            if (label223.Text == "X")
                listLabel.Add(label223);
            if (label224.Text == "X")
                listLabel.Add(label224);
            if (label225.Text == "X")
                listLabel.Add(label225);
            if (label226.Text == "X")
                listLabel.Add(label226);
            if (label227.Text == "X")
                listLabel.Add(label227);
            if (label228.Text == "X")
                listLabel.Add(label228);



            label107.Text = test.AnotPesadaQ4 == "1" ? "X" : "•";
            label108.Text = test.AnotPesadaQ4 == "2" ? "X" : "•";
            label106.Text = test.AnotPesadaQ4 == "3" ? "X" : "•";
            label87.Text = test.AnotPesadaQ4 == "4" ? "X" : "•";
            label88.Text = test.AnotPesadaQ4 == "5" ? "X" : "•";
            label229.Text = test.AnotPesadaQ4 == "6" ? "X" : "•";
            label230.Text = test.AnotPesadaQ4 == "7" ? "X" : "•";
            label231.Text = test.AnotPesadaQ4 == "8" ? "X" : "•";
            label232.Text = test.AnotPesadaQ4 == "9" ? "X" : "•";
            label233.Text = test.AnotPesadaQ4 == "10" ? "X" : "•";


            if (label107.Text == "X")
                listLabel.Add(label107);
            if (label108.Text == "X")
                listLabel.Add(label108);
            if (label106.Text == "X")
                listLabel.Add(label106);
            if (label87.Text == "X")
                listLabel.Add(label87);
            if (label88.Text == "X")
                listLabel.Add(label88);
            if (label229.Text == "X")
                listLabel.Add(label229);
            if (label230.Text == "X")
                listLabel.Add(label230);
            if (label231.Text == "X")
                listLabel.Add(label231);
            if (label232.Text == "X")
                listLabel.Add(label232);
            if (label233.Text == "X")
                listLabel.Add(label233);





        }

        public void buscarPrueba()
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join Pru16pf on SujetosEvaluados.P16pf =  Pru16pf.idTest where P16pf ='" + idPrueba + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();

                                test = new Pru16pf();

                                test.AnotBrutaA = res["AnotBrutaA"].ToString() != "" ? res["AnotBrutaA"].ToString() : "";
                                test.AnotBrutaB = res["AnotBrutaB"].ToString() != "" ? res["AnotBrutaB"].ToString() : "";
                                test.AnotBrutaC = res["AnotBrutaC"].ToString() != "" ? res["AnotBrutaC"].ToString() : "";
                                test.AnotBrutaE = res["AnotBrutaE"].ToString() != "" ? res["AnotBrutaE"].ToString() : "";
                                test.AnotBrutaF = res["AnotBrutaF"].ToString() != "" ? res["AnotBrutaF"].ToString() : "";
                                test.AnotBrutaG = res["AnotBrutaG"].ToString() != "" ? res["AnotBrutaG"].ToString() : "";
                                test.AnotBrutaH = res["AnotBrutaH"].ToString() != "" ? res["AnotBrutaH"].ToString() : "";
                                test.AnotBrutaI = res["AnotBrutaI"].ToString() != "" ? res["AnotBrutaI"].ToString() : "";
                                test.AnotBrutaL = res["AnotBrutaL"].ToString() != "" ? res["AnotBrutaL"].ToString() : "";
                                test.AnotBrutaM = res["AnotBrutaM"].ToString() != "" ? res["AnotBrutaM"].ToString() : "";
                                test.AnotBrutaN = res["AnotBrutaN"].ToString() != "" ? res["AnotBrutaN"].ToString() : "";
                                test.AnotBrutaO = res["AnotBrutaO"].ToString() != "" ? res["AnotBrutaO"].ToString() : "";
                                test.AnotBrutaQ1 = res["AnotBrutaQ1"].ToString() != "" ? res["AnotBrutaQ1"].ToString() : "";
                                test.AnotBrutaQ2 = res["AnotBrutaQ2"].ToString() != "" ? res["AnotBrutaQ2"].ToString() : "";
                                test.AnotBrutaQ3 = res["AnotBrutaQ3"].ToString() != "" ? res["AnotBrutaQ3"].ToString() : "";
                                test.AnotBrutaQ4 = res["AnotBrutaQ4"].ToString() != "" ? res["AnotBrutaQ4"].ToString() : "";

                                test.AnotPesadaA = res["AnotPesadaA"].ToString() != "" ? res["AnotPesadaA"].ToString() : "";
                                test.AnotPesadaB = res["AnotPesadaB"].ToString() != "" ? res["AnotPesadaB"].ToString() : "";
                                test.AnotPesadaC = res["AnotPesadaC"].ToString() != "" ? res["AnotPesadaC"].ToString() : "";
                                test.AnotPesadaE = res["AnotPesadaE"].ToString() != "" ? res["AnotPesadaE"].ToString() : "";
                                test.AnotPesadaF = res["AnotPesadaF"].ToString() != "" ? res["AnotPesadaF"].ToString() : "";
                                test.AnotPesadaG = res["AnotPesadaG"].ToString() != "" ? res["AnotPesadaG"].ToString() : "";
                                test.AnotPesadaH = res["AnotPesadaH"].ToString() != "" ? res["AnotPesadaH"].ToString() : "";
                                test.AnotPesadaI = res["AnotPesadaI"].ToString() != "" ? res["AnotPesadaI"].ToString() : "";
                                test.AnotPesadaL = res["AnotPesadaL"].ToString() != "" ? res["AnotPesadaL"].ToString() : "";
                                test.AnotPesadaM = res["AnotPesadaM"].ToString() != "" ? res["AnotPesadaM"].ToString() : "";
                                test.AnotPesadaN = res["AnotPesadaN"].ToString() != "" ? res["AnotPesadaN"].ToString() : "";
                                test.AnotPesadaO = res["AnotPesadaO"].ToString() != "" ? res["AnotPesadaO"].ToString() : "";
                                test.AnotPesadaQ1 = res["AnotPesadaQ1"].ToString() != "" ? res["AnotPesadaQ1"].ToString() : "";
                                test.AnotPesadaQ2 = res["AnotPesadaQ2"].ToString() != "" ? res["AnotPesadaQ2"].ToString() : "";
                                test.AnotPesadaQ3 = res["AnotPesadaQ3"].ToString() != "" ? res["AnotPesadaQ3"].ToString() : "";
                                test.AnotPesadaQ4 = res["AnotPesadaQ4"].ToString() != "" ? res["AnotPesadaQ4"].ToString() : "";

                                test.Perfil1 = res["Perfil1"].ToString() != "" ? res["Perfil1"].ToString() : "";
                                test.Perfil2 = res["Perfil2"].ToString() != "" ? res["Perfil2"].ToString() : "";
                                test.Perfil3 = res["Perfil3"].ToString() != "" ? res["Perfil3"].ToString() : "";
                                test.Perfil4 = res["Perfil4"].ToString() != "" ? res["Perfil4"].ToString() : "";

                                 
                                test.Distorsion = res["Distorsion"].ToString() != "" ? res["Distorsion"].ToString() : "";

                                test.AnotBrutaMD = res["AnotBrutaMD"].ToString();
                                test.Neuroticismo = res["Neuroticismo"].ToString() != "" ? res["Neuroticismo"].ToString() : "";

                                this.fecha = Convert.ToDateTime(res["Fecha"]).ToString("dd/MM/yyyy");

                                label250.Text = test.Perfil1;
                                label252.Text = test.Perfil2;
                                label254.Text = test.Perfil3;
                                label256.Text = test.Perfil4;
                                label312.Text = test.Distorsion;
                                label314.Text = test.Neuroticismo;

                                label306.Text ="MD:"+ res["AnotBrutaMD"].ToString();

                            }
                        }
                    }
                }
            }
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < listLabel.Count; i++)

            {
                Pen pen = new Pen(Color.Red);

                if (i + 1 != listLabel.Count)
                    e.Graphics.DrawLine(pen, listLabel[i].Location.X, listLabel[i].Location.Y, listLabel[i + 1].Location.X, listLabel[i + 1].Location.Y);
            }



        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _16PFAvanzado ds = new _16PFAvanzado(test);
            ds.ShowDialog();
        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                DatosSujetos res = db.DatosSujetos.Where(s => s.NombreS + " " + s.PrimerApellido + " " + s.SegundoApellido == nombreAtleta).FirstOrDefault();
                String ci = res.NCarnetIdent;

                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillasWord\16pf.doc");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Word (*.doc)|*.doc";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";
                    fichero.FileName = nombreAtleta + " Test_16Pf";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {
                            File.Copy(rutaProject, fichero.FileName, true);

                            String path = fichero.FileName.ToString();

                            Microsoft.Office.Interop.Word.Application wordApp = null;
                            wordApp = new Microsoft.Office.Interop.Word.Application();
                            String username = Environment.UserName;
                            Document wordDoc = wordApp.Documents.Open(path);



                            Bookmark bkmfecha = wordDoc.Bookmarks["fecha"];
                            Range rng1 = bkmfecha.Range;
                            rng1.Text = " " + fecha;

                            Bookmark bkmnNombre = wordDoc.Bookmarks["nombre"];
                            Range rng2 = bkmnNombre.Range;
                            rng2.Text = " " + nombreAtleta;


                            Bookmark bkmnEdad = wordDoc.Bookmarks["edad"];
                            Range rng3 = bkmnEdad.Range;
                            rng3.Text = " " + edad;


                            Bookmark bkmnDeport = wordDoc.Bookmarks["deporte"];
                            Range rng4 = bkmnDeport.Range;
                            rng4.Text = " " + deporte;


                            Bookmark bkmnModal = wordDoc.Bookmarks["modalidad"];
                            Range rng5 = bkmnModal.Range;
                            rng5.Text = " " + modalidad;

                            Bookmark bkmnModal1 = wordDoc.Bookmarks["etapa"];
                            Range rng6 = bkmnModal1.Range;
                            rng6.Text = " " + ci;
                            //---------------------------------------------------------//



                            Bookmark Q3L = wordDoc.Bookmarks["AA"];
                            Range A = Q3L.Range;
                            A.Text = " " + test.AnotBrutaA;

                            Bookmark CL = wordDoc.Bookmarks["AB"];
                            Range B = CL.Range;
                            B.Text = " " + test.AnotBrutaB;

                            Bookmark LL = wordDoc.Bookmarks["AC"];
                            Range C = LL.Range;
                            C.Text = " " + test.AnotBrutaC;

                            Bookmark OL = wordDoc.Bookmarks["AE"];
                            Range D = OL.Range;
                            D.Text = " " + test.AnotBrutaE;


                            Bookmark Q4L = wordDoc.Bookmarks["AF"];
                            Range E = Q4L.Range;
                            E.Text = " " + test.AnotBrutaF;



                            Bookmark Q3M = wordDoc.Bookmarks["AG"];
                            Range F = Q3M.Range;
                            F.Text = " " + test.AnotBrutaG;

                            Bookmark CM = wordDoc.Bookmarks["AH"];
                            Range G = CM.Range;
                            G.Text = " " + test.AnotBrutaH;

                            Bookmark LM = wordDoc.Bookmarks["AI"];
                            Range h = LM.Range;
                            h.Text = " " + test.AnotBrutaI;

                            Bookmark OM = wordDoc.Bookmarks["AL"];
                            Range I = OM.Range;
                            I.Text = " " + test.AnotBrutaL;


                            Bookmark Q4M = wordDoc.Bookmarks["AM"];
                            Range J = Q4M.Range;
                            J.Text = " " + test.AnotBrutaM;



                            Bookmark LMQ3 = wordDoc.Bookmarks["AN"];
                            Range K = LMQ3.Range;
                            K.Text = " " + test.AnotBrutaN;

                            Bookmark LMC = wordDoc.Bookmarks["AO"];
                            Range L = LMC.Range;
                            L.Text = " " + test.AnotBrutaO;

                            Bookmark LML = wordDoc.Bookmarks["AQ1"];
                            Range M = LML.Range;
                            M.Text = " " + test.AnotBrutaQ1;

                            Bookmark LMO = wordDoc.Bookmarks["AQ2"];
                            Range N = LMO.Range;
                            N.Text = " " + test.AnotBrutaQ2;

                            Bookmark LMQ4 = wordDoc.Bookmarks["AQ3"];
                            Range O = LMQ4.Range;
                            O.Text = " " + test.AnotBrutaQ3;

                            Bookmark LMQ42 = wordDoc.Bookmarks["AQ4"];
                            Range O2 = LMQ42.Range;
                            O2.Text = " " + test.AnotBrutaQ4;






                            //---------------------------------Stens-------------------------------//
                            Bookmark Q3SL = wordDoc.Bookmarks["PA"];
                            Range A1 = Q3SL.Range;
                            A1.Text = " " + test.AnotPesadaA;

                            Bookmark CSL = wordDoc.Bookmarks["PB"];
                            Range B1 = CSL.Range;
                            B1.Text = " " + test.AnotPesadaB;

                            Bookmark LSL = wordDoc.Bookmarks["PC"];
                            Range C1 = LSL.Range;
                            C1.Text = " " + test.AnotPesadaC;

                            Bookmark OSL = wordDoc.Bookmarks["PE"];
                            Range D1 = OSL.Range;
                            D1.Text = " " + test.AnotPesadaE;


                            Bookmark Q4SL = wordDoc.Bookmarks["PF"];
                            Range E1 = Q4SL.Range;
                            E1.Text = " " + test.AnotPesadaF;



                            Bookmark Q3SM = wordDoc.Bookmarks["PG"];
                            Range F1 = Q3SM.Range;
                            F1.Text = " " + test.AnotPesadaG;

                            Bookmark CSM = wordDoc.Bookmarks["PH"];
                            Range G1 = CSM.Range;
                            G1.Text = " " + test.AnotPesadaH;

                            Bookmark LSM = wordDoc.Bookmarks["PI"];
                            Range h1 = LSM.Range;
                            h1.Text = " " + test.AnotPesadaI;

                            Bookmark OSM = wordDoc.Bookmarks["PL"];
                            Range I1 = OSM.Range;
                            I1.Text = " " + test.AnotPesadaL;


                            Bookmark Q4SM = wordDoc.Bookmarks["PM"];
                            Range J1 = Q4SM.Range;
                            J1.Text = " " + test.AnotPesadaM;


                            Bookmark LMSQ3 = wordDoc.Bookmarks["PN"];
                            Range K1 = LMSQ3.Range;
                            K1.Text = " " + test.AnotPesadaN;

                            Bookmark LMSC = wordDoc.Bookmarks["PO"];
                            Range L1 = LMSC.Range;
                            L1.Text = " " + test.AnotPesadaO;

                            Bookmark LMSL = wordDoc.Bookmarks["PQ1"];
                            Range M1 = LMSL.Range;
                            M1.Text = " " + test.AnotPesadaQ1;

                            Bookmark LMSO = wordDoc.Bookmarks["PQ2"];
                            Range N1 = LMSO.Range;
                            N1.Text = " " + test.AnotPesadaQ2;

                            Bookmark LMSQ4 = wordDoc.Bookmarks["PQ3"];
                            Range O1 = LMSQ4.Range;
                            O1.Text = " " + test.AnotPesadaQ3;


                            Bookmark pts = wordDoc.Bookmarks["PQ4"];
                            Range S = pts.Range;
                            S.Text = " " + test.AnotPesadaQ4;








                            Bookmark LMSL1 = wordDoc.Bookmarks["adap"];
                            Range M11 = LMSL1.Range;
                            M11.Text = " " + test.Perfil1;

                            Bookmark LMSO1 = wordDoc.Bookmarks["emotiv"];
                            Range N11 = LMSO1.Range;
                            N11.Text = " " + test.Perfil2;

                            Bookmark LMSQ41 = wordDoc.Bookmarks["intro"];
                            Range O11 = LMSQ41.Range;
                            O11.Text = " " + test.Perfil3;


                            Bookmark pts1 = wordDoc.Bookmarks["sumi"];
                            Range S1 = pts1.Range;
                            S1.Text = " " + test.Perfil4;


                            Bookmark pts11 = wordDoc.Bookmarks["MD"];
                            Range S11 = pts11.Range;
                            S11.Text = " " + test.AnotBrutaMD;




                            Bookmark pts12 = wordDoc.Bookmarks["neuroticismo"];
                            Range S12 = pts12.Range;
                            S12.Text = " " + test.Neuroticismo;


                            Bookmark pts112 = wordDoc.Bookmarks["distorcion"];
                            Range S112 = pts112.Range;
                            S112.Text = " " + test.Distorsion;


                            wordApp.ActiveDocument.Save();
                            wordApp.ActiveDocument.Close();


                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);
                        }

                    }




                }
                catch (Exception)
                {

                    MessageBox.Show("Ha ocurrido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                                etapa = res["Etapa"].ToString();


                            }
                        }
                    }
                }

            }

        }

        private void buscarAtleta(int idAtleta)
        {
            using (mainEntities entities = new mainEntities())
            {
                var atleta = entities.DatosSujetos.Find(idAtleta);
                modalidad = atleta.Ocupacion;
                deporte = atleta.Entidad;
                edad = atleta.Edad;
            }
        }

        
    }
}
