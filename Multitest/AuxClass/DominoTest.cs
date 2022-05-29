using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class DominoTest
    {
        public List<Edad> edad { set; get; }
        public List<int> percentil { set; get; }

        public DominoTest()

        {
            edad = new List<Edad>();
            percentil = new List<int>(new int[] { 95, 90, 75, 50, 25, 10, 5 });

            List<int> list = new List<int>(new int[] { 46, 43, 37, 30, 24, 18, 14 });
            Edad edad1 = new Edad(13, 17, list);

            List<int> list2 = new List<int>(new int[] { 46, 43, 37, 30, 24, 18, 14 });
            Edad edad2 = new Edad(18, 22, list2);

            List<int> list3 = new List<int>(new int[] { 47, 45, 38, 32, 26, 19, 16 });
            Edad edad3 = new Edad(23, 27, list3);

            List<int> list4 = new List<int>(new int[] { 46, 43, 36, 29, 22, 14, 11 });
            Edad edad4 = new Edad(28, 32, list4);

            List<int> list5 = new List<int>(new int[] { 45, 41, 34, 28, 21, 14, 10 });
            Edad edad5 = new Edad(33, 37, list5);


            List<int> list6 = new List<int>(new int[] { 44, 40, 34, 27, 20, 14, 10 });
            Edad edad6 = new Edad(38, 42, list6);


            List<int> list7 = new List<int>(new int[] { 43, 39, 33, 26, 19, 13, 10 });
            Edad edad7 = new Edad(43, 47, list7);

            List<int> list8 = new List<int>(new int[] { 39, 35, 29, 22, 16, 10, 6 });
            Edad edad8 = new Edad(48, 52, list8);


            List<int> list9 = new List<int>(new int[] { 36, 33, 27, 21, 15, 10, 6 });
            Edad edad9 = new Edad(53, 57, list9);

            List<int> list10 = new List<int>(new int[] { 36, 33, 27, 21, 15, 10, 6 });
            Edad edad10 = new Edad(58, 62, list10);
            
           

            List<int> list13 = new List<int>(new int[] { 31, 28, 24, 19, 14, 10, 6 });
            Edad edad13 = new Edad(63, 67, list13);

            List<int> list14 = new List<int>(new int[] { 31, 28, 24, 18, 13, 8, 5 });
            Edad edad14 = new Edad(68, 1000, list14);

            edad.Add(edad1);
            edad.Add(edad2);
            edad.Add(edad3);
            edad.Add(edad4);

            edad.Add(edad5);
            edad.Add(edad6);
            edad.Add(edad7);
            edad.Add(edad8);
            edad.Add(edad9);
            edad.Add(edad10);
             
            edad.Add(edad13);
            edad.Add(edad14);



        }
    }
}
