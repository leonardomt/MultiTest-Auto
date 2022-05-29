using Multitest.ADOmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class _16PFAvanzado : Form
    {
        List<Label> listLabel;
        Pru16pf test;
        public _16PFAvanzado(Pru16pf test)
        {
            InitializeComponent();
            listLabel = new List<Label>();
            this.test = test;
            pintarX();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pintarX()
        {
            //foreach (var item in tableLayoutPanel9.Controls)
            //{
            //    Label lab = item as Label;

            //    if (lab.Text == "X")
            //    {
            //        lab.ForeColor = Color.DarkOrange;
            //        listLabel.Add(lab);
            //    }
            //}
        }

        private void pintarGrafico()
        {



            label53.Text = test.AnotPesadaA == "1" ? "X" : "•";
            label109.Text = test.AnotPesadaA == "2" ? "X" : "•";
            label55.Text = test.AnotPesadaA == "3" ? "X" : "•";
            label110.Text = test.AnotPesadaA == "4" ? "X" : " ";
            label111.Text = test.AnotPesadaA == "5" ? "X" : " ";
            label112.Text = test.AnotPesadaA == "6" ? "X" : " ";
            label113.Text = test.AnotPesadaA == "7" ? "X" : " ";
            label114.Text = test.AnotPesadaA == "8" ? "X" : "•";
            label115.Text = test.AnotPesadaA == "9" ? "X" : "•";
            label116.Text = test.AnotPesadaA == "10" ? "X" : "•";


            if (label53.Text == "X")
            {
                listLabel.Add(label53);

                label53.ForeColor = Color.DarkOrange;
            }

            if (label55.Text == "X")
            {
                listLabel.Add(label55);

                label55.ForeColor = Color.DarkOrange;
            }

            if (label109.Text == "X")
            {
                listLabel.Add(label109);

                label109.ForeColor = Color.DarkOrange;
            }





            if (label110.Text == "X")
                listLabel.Add(label110);
            if (label111.Text == "X")
                listLabel.Add(label111);
            if (label112.Text == "X")
                listLabel.Add(label112);
            if (label113.Text == "X")
                listLabel.Add(label113);
            if (label114.Text == "X")

            {
                listLabel.Add(label114);

                label114.ForeColor = Color.DarkOrange;
            }

            if (label115.Text == "X")
            {
                listLabel.Add(label115);

                label115.ForeColor = Color.DarkOrange;
            }


            if (label116.Text == "X")
            {
                listLabel.Add(label116);

                label116.ForeColor = Color.DarkOrange;
            }






            label56.Text = test.AnotPesadaB == "1" ? "X" : "•";
            label117.Text = test.AnotPesadaB == "2" ? "X" : "•";
            label57.Text = test.AnotPesadaB == "3" ? "X" : "•";
            label118.Text = test.AnotPesadaB == "4" ? "X" : "";
            label119.Text = test.AnotPesadaB == "5" ? "X" : "";
            label120.Text = test.AnotPesadaB == "6" ? "X" : "";
            label121.Text = test.AnotPesadaB == "7" ? "X" : "";
            label122.Text = test.AnotPesadaB == "8" ? "X" : "•";
            label123.Text = test.AnotPesadaB == "9" ? "X" : "•";
            label124.Text = test.AnotPesadaB == "10" ? "X" : "•";


            if (label56.Text == "X")
            {
                label56.ForeColor = Color.DarkOrange;
                listLabel.Add(label56);
            }
            if (label117.Text == "X")
            {
                listLabel.Add(label117);
                label117.ForeColor = Color.DarkOrange;
            }
            if (label57.Text == "X")
            {
                label57.ForeColor = Color.DarkOrange;
                listLabel.Add(label57);
            }
            if (label118.Text == "X")
                listLabel.Add(label118);
            if (label119.Text == "X")
                listLabel.Add(label119);
            if (label120.Text == "X")
                listLabel.Add(label120);
            if (label121.Text == "X")
                listLabel.Add(label121);
            if (label122.Text == "X")
            {
                label122.ForeColor = Color.DarkOrange;
                listLabel.Add(label122);

            }

            if (label123.Text == "X")
            {
                label123.ForeColor = Color.DarkOrange;
                listLabel.Add(label123);
            }
            if (label124.Text == "X")
            {
                listLabel.Add(label124);
                label124.ForeColor = Color.DarkOrange;
            }









            label58.Text = test.AnotPesadaC == "1" ? "X" : "•";
            label125.Text = test.AnotPesadaC == "2" ? "X" : "•";
            label59.Text = test.AnotPesadaC == "3" ? "X" : "•";
            label126.Text = test.AnotPesadaC == "4" ? "X" : "";
            label127.Text = test.AnotPesadaC == "5" ? "X" : "";
            label128.Text = test.AnotPesadaC == "6" ? "X" : "";
            label129.Text = test.AnotPesadaC == "7" ? "X" : "";
            label130.Text = test.AnotPesadaC == "8" ? "X" : "•";
            label131.Text = test.AnotPesadaC == "9" ? "X" : "•";
            label132.Text = test.AnotPesadaC == "10" ? "X" : "•";



            if (label58.Text == "X")
            {
                listLabel.Add(label58);
                label58.ForeColor = Color.DarkOrange;
            }
            if (label125.Text == "X")
            {
                listLabel.Add(label125);
                label125.ForeColor = Color.DarkOrange;
            }

            if (label59.Text == "X")
            {
                listLabel.Add(label59);
                label59.ForeColor = Color.DarkOrange;
            }

            if (label126.Text == "X")
                listLabel.Add(label126);
            if (label127.Text == "X")
                listLabel.Add(label127);
            if (label128.Text == "X")
                listLabel.Add(label128);
            if (label129.Text == "X")
                listLabel.Add(label129);
            if (label130.Text == "X")
            {
                listLabel.Add(label130);
                label130.ForeColor = Color.DarkOrange;
            }

            if (label131.Text == "X")
            {
                listLabel.Add(label131);
                label131.ForeColor = Color.DarkOrange;
            }

            if (label132.Text == "X")
            {
                listLabel.Add(label132);
                label132.ForeColor = Color.DarkOrange;
            }











            label60.Text = test.AnotPesadaE == "1" ? "X" : "•";
            label133.Text = test.AnotPesadaE == "2" ? "X" : "•";
            label61.Text = test.AnotPesadaE == "3" ? "X" : "•";
            label134.Text = test.AnotPesadaE == "4" ? "X" : "";
            label135.Text = test.AnotPesadaE == "5" ? "X" : "";
            label136.Text = test.AnotPesadaE == "6" ? "X" : "";
            label137.Text = test.AnotPesadaE == "7" ? "X" : "";
            label138.Text = test.AnotPesadaE == "8" ? "X" : "•";
            label139.Text = test.AnotPesadaE == "9" ? "X" : "•";
            label140.Text = test.AnotPesadaE == "10" ? "X" : "•";


            if (label60.Text == "X")
            {
                label60.ForeColor = Color.DarkOrange;
                listLabel.Add(label60);
            }

            if (label133.Text == "X")
            {
                label133.ForeColor = Color.DarkOrange;
                listLabel.Add(label133);
            }
            if (label61.Text == "X")
            {
                listLabel.Add(label61);
                label61.ForeColor = Color.DarkOrange;
            }

            if (label134.Text == "X")
                listLabel.Add(label134);
            if (label135.Text == "X")
                listLabel.Add(label135);
            if (label136.Text == "X")
                listLabel.Add(label136);
            if (label137.Text == "X")
                listLabel.Add(label137);
            if (label138.Text == "X")
            {
                listLabel.Add(label138);
                label138.ForeColor = Color.DarkOrange;
            }

            if (label139.Text == "X")
            {
                listLabel.Add(label139);
                label139.ForeColor = Color.DarkOrange;
            }

            if (label140.Text == "X")
            {
                listLabel.Add(label140);
                label140.ForeColor = Color.DarkOrange;
            }








            label62.Text = test.AnotPesadaF == "1" ? "X" : "•";
            label141.Text = test.AnotPesadaF == "2" ? "X" : "•";
            label63.Text = test.AnotPesadaF == "3" ? "X" : "•";
            label142.Text = test.AnotPesadaF == "4" ? "X" : "";
            label143.Text = test.AnotPesadaF == "5" ? "X" : "";
            label144.Text = test.AnotPesadaF == "6" ? "X" : "";
            label145.Text = test.AnotPesadaF == "7" ? "X" : "";
            label146.Text = test.AnotPesadaF == "8" ? "X" : "•";
            label147.Text = test.AnotPesadaF == "9" ? "X" : "•";
            label148.Text = test.AnotPesadaF == "10" ? "X" : "•";


            if (label62.Text == "X")
            {
                listLabel.Add(label62);
                label62.ForeColor = Color.DarkOrange;
            }
            if (label141.Text == "X")
            {
                listLabel.Add(label141);
                label141.ForeColor = Color.DarkOrange;
            }


            if (label63.Text == "X")
            {
                listLabel.Add(label63);
                label63.ForeColor = Color.DarkOrange;
            }

            if (label142.Text == "X")
                listLabel.Add(label142);
            if (label143.Text == "X")
                listLabel.Add(label143);
            if (label144.Text == "X")
                listLabel.Add(label144);
            if (label145.Text == "X")
                listLabel.Add(label145);
            if (label146.Text == "X")
            {
                listLabel.Add(label146);
                label146.ForeColor = Color.DarkOrange;
            }
            if (label147.Text == "X")
            {
                listLabel.Add(label147);
                label147.ForeColor = Color.DarkOrange;
            }

            if (label148.Text == "X")
            {
                listLabel.Add(label148);
                label148.ForeColor = Color.DarkOrange;
            }



            label164.Text = test.AnotPesadaG == "1" ? "X" : "•";
            label149.Text = test.AnotPesadaG == "2" ? "X" : "•";
            label64.Text = test.AnotPesadaG == "3" ? "X" : "•";
            label150.Text = test.AnotPesadaG == "4" ? "X" : "";
            label151.Text = test.AnotPesadaG == "5" ? "X" : "";
            label152.Text = test.AnotPesadaG == "6" ? "X" : "";
            label153.Text = test.AnotPesadaG == "7" ? "X" : "";
            label154.Text = test.AnotPesadaG == "8" ? "X" : "•";
            label155.Text = test.AnotPesadaG == "9" ? "X" : "•";
            label156.Text = test.AnotPesadaG == "10" ? "X" : "•";


            if (label164.Text == "X")
            {
                listLabel.Add(label164);
                label164.ForeColor = Color.DarkOrange;
            }
            if (label149.Text == "X")
            {
                listLabel.Add(label149);
                label149.ForeColor = Color.DarkOrange;
            }
            if (label64.Text == "X")
            {
                listLabel.Add(label64);
                label64.ForeColor = Color.DarkOrange;
            }
            if (label150.Text == "X")
                listLabel.Add(label150);
            if (label151.Text == "X")
                listLabel.Add(label151);
            if (label152.Text == "X")
                listLabel.Add(label152);
            if (label153.Text == "X")
                listLabel.Add(label153);
            if (label154.Text == "X")
            {
                listLabel.Add(label154);
                label154.ForeColor = Color.DarkOrange;
            }
            if (label155.Text == "X")
            {
                listLabel.Add(label155);
                label155.ForeColor = Color.DarkOrange;
            }
            if (label156.Text == "X")
            {
                listLabel.Add(label156);
                label156.ForeColor = Color.DarkOrange;
            }



            label66.Text = test.AnotPesadaH == "1" ? "X" : "•";
            label157.Text = test.AnotPesadaH == "2" ? "X" : "•";
            label67.Text = test.AnotPesadaH == "3" ? "X" : "•";
            label158.Text = test.AnotPesadaH == "4" ? "X" : "";
            label159.Text = test.AnotPesadaH == "5" ? "X" : "";
            label160.Text = test.AnotPesadaH == "6" ? "X" : "";
            label161.Text = test.AnotPesadaH == "7" ? "X" : "";
            label162.Text = test.AnotPesadaH == "8" ? "X" : "•";
            label163.Text = test.AnotPesadaH == "9" ? "X" : "•";
            label164.Text = test.AnotPesadaH == "10" ? "X" : "•";


            if (label66.Text == "X")
            {
                listLabel.Add(label66);
                label66.ForeColor = Color.DarkOrange;
            }
            if (label157.Text == "X")
            {
                listLabel.Add(label157);
                label157.ForeColor = Color.DarkOrange;
            }
            if (label67.Text == "X")
            {
                listLabel.Add(label67);
                label67.ForeColor = Color.DarkOrange;
            }
            if (label158.Text == "X")
                listLabel.Add(label158);
            if (label159.Text == "X")
                listLabel.Add(label159);
            if (label160.Text == "X")
                listLabel.Add(label160);
            if (label161.Text == "X")
                listLabel.Add(label161);
            if (label162.Text == "X")
            {
                listLabel.Add(label162);
                label162.ForeColor = Color.DarkOrange;
            }
            if (label163.Text == "X")
            {
                listLabel.Add(label163);
                label163.ForeColor = Color.DarkOrange;
            }
            if (label164.Text == "X")
            {
                listLabel.Add(label164);
                label164.ForeColor = Color.DarkOrange;
            }



            label68.Text = test.AnotPesadaI == "1" ? "X" : "•";
            label165.Text = test.AnotPesadaI == "2" ? "X" : "•";
            label69.Text = test.AnotPesadaI == "3" ? "X" : "•";
            label166.Text = test.AnotPesadaI == "4" ? "X" : "";
            label167.Text = test.AnotPesadaI == "5" ? "X" : "";
            label168.Text = test.AnotPesadaI == "6" ? "X" : "";
            label169.Text = test.AnotPesadaI == "7" ? "X" : "";
            label170.Text = test.AnotPesadaI == "8" ? "X" : "•";
            label171.Text = test.AnotPesadaI == "9" ? "X" : "•";
            label172.Text = test.AnotPesadaI == "10" ? "X" : "•";


            if (label68.Text == "X")
            {
                listLabel.Add(label68);
                label68.ForeColor = Color.DarkOrange;
            }
            if (label165.Text == "X")
            {
                listLabel.Add(label165);
                label165.ForeColor = Color.DarkOrange;
            }
            if (label69.Text == "X")
            {
                listLabel.Add(label69);
                label69.ForeColor = Color.DarkOrange;
            }
            if (label166.Text == "X")
                listLabel.Add(label166);
            if (label167.Text == "X")
                listLabel.Add(label167);
            if (label168.Text == "X")
                listLabel.Add(label168);
            if (label169.Text == "X")
                listLabel.Add(label169);
            if (label170.Text == "X")
            {
                listLabel.Add(label170);
                label170.ForeColor = Color.DarkOrange;
            }
            if (label171.Text == "X")
            {
                listLabel.Add(label171);
                label171.ForeColor = Color.DarkOrange;
            }
            if (label172.Text == "X")
            {
                listLabel.Add(label172);
                label172.ForeColor = Color.DarkOrange;
            }


            label70.Text = test.AnotPesadaL == "1" ? "X" : "•";
            label173.Text = test.AnotPesadaL == "2" ? "X" : "•";
            label71.Text = test.AnotPesadaL == "3" ? "X" : "•";
            label174.Text = test.AnotPesadaL == "4" ? "X" : "";
            label175.Text = test.AnotPesadaL == "5" ? "X" : "";
            label176.Text = test.AnotPesadaL == "6" ? "X" : "";
            label177.Text = test.AnotPesadaL == "7" ? "X" : "";
            label178.Text = test.AnotPesadaL == "8" ? "X" : "•";
            label179.Text = test.AnotPesadaL == "9" ? "X" : "•";
            label180.Text = test.AnotPesadaL == "10" ? "X" : "•";


            if (label70.Text == "X")
            {
                listLabel.Add(label70);
                label70.ForeColor = Color.DarkOrange;
            }
            if (label173.Text == "X")
            {
                listLabel.Add(label173);
                label173.ForeColor = Color.DarkOrange;
            }

            if (label71.Text == "X")
            {
                listLabel.Add(label71);
                label71.ForeColor = Color.DarkOrange;
            }
            if (label174.Text == "X")
                listLabel.Add(label174);
            if (label175.Text == "X")
                listLabel.Add(label175);
            if (label176.Text == "X")
                listLabel.Add(label176);
            if (label177.Text == "X")
                listLabel.Add(label177);
            if (label178.Text == "X")
            {
                listLabel.Add(label178);
                label178.ForeColor = Color.DarkOrange;
            }

            if (label179.Text == "X")
            {
                listLabel.Add(label179);
                label179.ForeColor = Color.DarkOrange;
            }

            if (label180.Text == "X")
            {
                listLabel.Add(label180);
                label180.ForeColor = Color.DarkOrange;
            }


            label75.Text = test.AnotPesadaM == "1" ? "X" : "•";
            label181.Text = test.AnotPesadaM == "2" ? "X" : "•";
            label76.Text = test.AnotPesadaM == "3" ? "X" : "•";
            label182.Text = test.AnotPesadaM == "4" ? "X" : "";
            label183.Text = test.AnotPesadaM == "5" ? "X" : "";
            label184.Text = test.AnotPesadaM == "6" ? "X" : "";
            label185.Text = test.AnotPesadaM == "7" ? "X" : "";
            label186.Text = test.AnotPesadaM == "8" ? "X" : "•";
            label187.Text = test.AnotPesadaM == "9" ? "X" : "•";
            label188.Text = test.AnotPesadaM == "10" ? "X" : "•";


            if (label75.Text == "X")
            {
                listLabel.Add(label75);
                label75.ForeColor = Color.DarkOrange;
            }
            if (label181.Text == "X")
            {
                listLabel.Add(label181);
                label181.ForeColor = Color.DarkOrange;
            }
            if (label76.Text == "X")
            {
                listLabel.Add(label76);
                label76.ForeColor = Color.DarkOrange;
            }
            if (label182.Text == "X")
                listLabel.Add(label182);
            if (label183.Text == "X")
                listLabel.Add(label183);
            if (label184.Text == "X")
                listLabel.Add(label184);
            if (label185.Text == "X")
                listLabel.Add(label185);
            if (label186.Text == "X")
            {
                listLabel.Add(label186);
                label186.ForeColor = Color.DarkOrange;
            }
            if (label187.Text == "X")
            {
                listLabel.Add(label187);
                label187.ForeColor = Color.DarkOrange;
            }
            if (label188.Text == "X")
            {
                listLabel.Add(label188);
                label188.ForeColor = Color.DarkOrange;
            }



            label77.Text = test.AnotPesadaN == "1" ? "X" : "•";
            label189.Text = test.AnotPesadaN == "2" ? "X" : "•";
            label78.Text = test.AnotPesadaN == "3" ? "X" : "•";
            label190.Text = test.AnotPesadaN == "4" ? "X" : "";
            label191.Text = test.AnotPesadaN == "5" ? "X" : "";
            label192.Text = test.AnotPesadaN == "6" ? "X" : "";
            label193.Text = test.AnotPesadaN == "7" ? "X" : "";
            label194.Text = test.AnotPesadaN == "8" ? "X" : "•";
            label195.Text = test.AnotPesadaN == "9" ? "X" : "•";
            label196.Text = test.AnotPesadaN == "10" ? "X" : "•";


            if (label77.Text == "X")
            {
                listLabel.Add(label77);
                label77.ForeColor = Color.DarkOrange;
            }
            if (label189.Text == "X")
            {
                listLabel.Add(label189);
                label189.ForeColor = Color.DarkOrange;
            }
            if (label78.Text == "X")
            {
                listLabel.Add(label78);
                label78.ForeColor = Color.DarkOrange;
            }
            if (label190.Text == "X")
                listLabel.Add(label190);
            if (label191.Text == "X")
                listLabel.Add(label191);
            if (label192.Text == "X")
                listLabel.Add(label192);
            if (label193.Text == "X")
                listLabel.Add(label193);
            if (label194.Text == "X")
            {
                listLabel.Add(label194);
                label194.ForeColor = Color.DarkOrange;
            }
            if (label195.Text == "X")
            {
                listLabel.Add(label195);
                label195.ForeColor = Color.DarkOrange;
            }
            if (label196.Text == "X")
            {
                listLabel.Add(label196);
                label196.ForeColor = Color.DarkOrange;
            }




            label79.Text = test.AnotPesadaO == "1" ? "X" : "•";
            label197.Text = test.AnotPesadaO == "2" ? "X" : "•";
            label80.Text = test.AnotPesadaO == "3" ? "X" : "•";
            label198.Text = test.AnotPesadaO == "4" ? "X" : "";
            label199.Text = test.AnotPesadaO == "5" ? "X" : "";
            label200.Text = test.AnotPesadaO == "6" ? "X" : "";
            label201.Text = test.AnotPesadaO == "7" ? "X" : "";
            label202.Text = test.AnotPesadaO == "8" ? "X" : "•";
            label203.Text = test.AnotPesadaO == "9" ? "X" : "•";
            label204.Text = test.AnotPesadaO == "10" ? "X" : "•";



            if (label79.Text == "X")
            {
                listLabel.Add(label79);
                label79.ForeColor = Color.DarkOrange;
            }
            if (label197.Text == "X")
            {
                listLabel.Add(label197);
                label197.ForeColor = Color.DarkOrange;
            }
            if (label80.Text == "X")
            {
                listLabel.Add(label80);
                label80.ForeColor = Color.DarkOrange;
            }
            if (label198.Text == "X")
                listLabel.Add(label198);
            if (label199.Text == "X")
                listLabel.Add(label199);
            if (label200.Text == "X")
                listLabel.Add(label200);
            if (label201.Text == "X")
                listLabel.Add(label201);
            if (label202.Text == "X")
            {
                listLabel.Add(label202);
                label202.ForeColor = Color.DarkOrange;
            }
            if (label203.Text == "X")
            {
                listLabel.Add(label203);
                label203.ForeColor = Color.DarkOrange;
            }
            if (label204.Text == "X")
            {
                listLabel.Add(label204);
                label204.ForeColor = Color.DarkOrange;
            }




            label81.Text = test.AnotPesadaQ1 == "1" ? "X" : "•";
            label205.Text = test.AnotPesadaQ1 == "2" ? "X" : "•";
            label82.Text = test.AnotPesadaQ1 == "3" ? "X" : "•";
            label206.Text = test.AnotPesadaQ1 == "4" ? "X" : "";
            label207.Text = test.AnotPesadaQ1 == "5" ? "X" : "";
            label208.Text = test.AnotPesadaQ1 == "6" ? "X" : "";
            label209.Text = test.AnotPesadaQ1 == "7" ? "X" : "";
            label210.Text = test.AnotPesadaQ1 == "8" ? "X" : "•";
            label211.Text = test.AnotPesadaQ1 == "9" ? "X" : "•";
            label212.Text = test.AnotPesadaQ1 == "10" ? "X" : "•";



            if (label81.Text == "X")
            {
                listLabel.Add(label81);
                label81.ForeColor = Color.DarkOrange;
            }
            if (label205.Text == "X")
            {
                listLabel.Add(label205);
                label205.ForeColor = Color.DarkOrange;
            }
            if (label82.Text == "X")
            {
                listLabel.Add(label82);
                label82.ForeColor = Color.DarkOrange;
            }
            if (label206.Text == "X")
                listLabel.Add(label206);
            if (label207.Text == "X")
                listLabel.Add(label207);
            if (label208.Text == "X")
                listLabel.Add(label208);
            if (label209.Text == "X")
                listLabel.Add(label209);
            if (label210.Text == "X")
            {
                listLabel.Add(label210);
                label210.ForeColor = Color.DarkOrange;
            }
            if (label211.Text == "X")
            {
                listLabel.Add(label211);
                label211.ForeColor = Color.DarkOrange;
            }
            if (label212.Text == "X")
            {
                listLabel.Add(label212);
                label212.ForeColor = Color.DarkOrange;
            }







            label83.Text = test.AnotPesadaQ2 == "1" ? "X" : "•";
            label213.Text = test.AnotPesadaQ2 == "2" ? "X" : "•";
            label84.Text = test.AnotPesadaQ2 == "3" ? "X" : "•";
            label214.Text = test.AnotPesadaQ2 == "4" ? "X" : "";
            label215.Text = test.AnotPesadaQ2 == "5" ? "X" : "";
            label216.Text = test.AnotPesadaQ2 == "6" ? "X" : "";
            label217.Text = test.AnotPesadaQ2 == "7" ? "X" : "";
            label218.Text = test.AnotPesadaQ2 == "8" ? "X" : "•";
            label219.Text = test.AnotPesadaQ2 == "9" ? "X" : "•";
            label220.Text = test.AnotPesadaQ2 == "10" ? "X" : "•";

            if (label83.Text == "X")
            {
                listLabel.Add(label83);
                label83.ForeColor = Color.DarkOrange;
            }
            if (label213.Text == "X")
            {
                listLabel.Add(label213);
                label213.ForeColor = Color.DarkOrange;
            }
            if (label84.Text == "X")
            {
                listLabel.Add(label84);
                label84.ForeColor = Color.DarkOrange;
            }
            if (label214.Text == "X")
                listLabel.Add(label214);
            if (label215.Text == "X")
                listLabel.Add(label215);
            if (label216.Text == "X")
                listLabel.Add(label216);
            if (label217.Text == "X")
                listLabel.Add(label217);
            if (label218.Text == "X")
            {
                listLabel.Add(label218);
                label218.ForeColor = Color.DarkOrange;
            }
            if (label219.Text == "X")
            {
                listLabel.Add(label219);
                label219.ForeColor = Color.DarkOrange;
            }
            if (label220.Text == "X")
            {
                listLabel.Add(label220);
                label220.ForeColor = Color.DarkOrange;
            }




            label85.Text = test.AnotPesadaQ3 == "1" ? "X" : "•";
            label221.Text = test.AnotPesadaQ3 == "2" ? "X" : "•";
            label86.Text = test.AnotPesadaQ3 == "3" ? "X" : "•";
            label222.Text = test.AnotPesadaQ3 == "4" ? "X" : "";
            label223.Text = test.AnotPesadaQ3 == "5" ? "X" : "";
            label224.Text = test.AnotPesadaQ3 == "6" ? "X" : "";
            label225.Text = test.AnotPesadaQ3 == "7" ? "X" : "";
            label226.Text = test.AnotPesadaQ3 == "8" ? "X" : "•";
            label227.Text = test.AnotPesadaQ3 == "9" ? "X" : "•";
            label228.Text = test.AnotPesadaQ3 == "10" ? "X" : "•";


            if (label85.Text == "X")
            {
                listLabel.Add(label85);
                label85.ForeColor = Color.DarkOrange;
            }
            if (label221.Text == "X")
            {
                listLabel.Add(label221);
                label221.ForeColor = Color.DarkOrange;
            }
            if (label86.Text == "X")
            {
                listLabel.Add(label86);
                label86.ForeColor = Color.DarkOrange;
            }
            if (label222.Text == "X")
                listLabel.Add(label222);
            if (label223.Text == "X")
                listLabel.Add(label223);
            if (label224.Text == "X")
                listLabel.Add(label224);
            if (label225.Text == "X")
                listLabel.Add(label225);
            if (label226.Text == "X")
            {
                listLabel.Add(label226);
                label226.ForeColor = Color.DarkOrange;
            }
            if (label227.Text == "X")
            {
                listLabel.Add(label227);
                label227.ForeColor = Color.DarkOrange;

            }
            if (label228.Text == "X")
            {
                listLabel.Add(label228);
                label228.ForeColor = Color.DarkOrange;

            }



            label107.Text = test.AnotPesadaQ4 == "1" ? "X" : "•";
            label108.Text = test.AnotPesadaQ4 == "2" ? "X" : "•";
            label106.Text = test.AnotPesadaQ4 == "3" ? "X" : "•";
            label87.Text = test.AnotPesadaQ4 == "4" ? "X" : "";
            label88.Text = test.AnotPesadaQ4 == "5" ? "X" : "";
            label229.Text = test.AnotPesadaQ4 == "6" ? "X" : "";
            label230.Text = test.AnotPesadaQ4 == "7" ? "X" : "";
            label231.Text = test.AnotPesadaQ4 == "8" ? "X" : "•";
            label232.Text = test.AnotPesadaQ4 == "9" ? "X" : "•";
            label233.Text = test.AnotPesadaQ4 == "10" ? "X" : "•";


            if (label107.Text == "X")
            {
                listLabel.Add(label107);
                label107.ForeColor = Color.DarkOrange;
            }
            if (label108.Text == "X")
            {
                listLabel.Add(label108);
                label108.ForeColor = Color.DarkOrange;
            }
            if (label106.Text == "X")
            {
                listLabel.Add(label106);
                label106.ForeColor = Color.DarkOrange;
            }
            if (label87.Text == "X")
                listLabel.Add(label87);
            if (label88.Text == "X")
                listLabel.Add(label88);
            if (label229.Text == "X")
                listLabel.Add(label229);
            if (label230.Text == "X")
                listLabel.Add(label230);
            if (label231.Text == "X")
            {
                listLabel.Add(label231);
                label231.ForeColor = Color.DarkOrange;
            }
            if (label232.Text == "X")
            {
                listLabel.Add(label232);
                label232.ForeColor = Color.DarkOrange;
            }
            if (label233.Text == "X")
            {
                listLabel.Add(label233);
                label233.ForeColor = Color.DarkOrange;
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

        private void _16PFAvanzado_Load(object sender, EventArgs e)
        {
            pintarGrafico();
        }
    }
}
