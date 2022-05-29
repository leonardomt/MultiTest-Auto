using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class RavenClass
    {
        public List<String> clasificacion { get; set; }
        public List<String> rango { get; set; }
        public List<int> percentil { get; set; }
        public List<Edad> edad { get; set; }

        public RavenClass()
        {
            clasificacion = new List<String>();
            rango = new List<String>();
            percentil = new List<int>();
            edad = new List<Edad>();

            clasificacion.Add("Superior");
            clasificacion.Add("Promedio");
            clasificacion.Add("Normal Alto");
            clasificacion.Add("Normal");
            clasificacion.Add("Normal Bajo");
            clasificacion.Add("Inferior");
            clasificacion.Add("Promedio");

            rango.Add("I");
            rango.Add("II");
            rango.Add("III+");
            rango.Add("III");
            rango.Add("III-");
            rango.Add("IV");
            rango.Add("V");

            percentil.Add(95);
            percentil.Add(90);
            percentil.Add(75);
            percentil.Add(50);
            percentil.Add(25);
            percentil.Add(10);
            percentil.Add(5);


            List<int> list = new List<int>(new int[] { 58, 54, 46, 38, 30, 23, 18 });
            Edad edad1 = new Edad(13, 17, list);

            List<int> list2 = new List<int>(new int[] { 58, 54, 46, 38, 30, 23, 18 });
            Edad edad2 = new Edad(18, 22, list);

            List<int> list3 = new List<int>(new int[] { 59, 56, 48, 40, 32, 24, 20 });
            Edad edad3 = new Edad(23, 27, list);

            List<int> list4 = new List<int>(new int[] { 58, 54, 45, 36, 27, 18, 14 });
            Edad edad4 = new Edad(28, 32, list);

            List<int> list5 = new List<int>(new int[] { 56, 51, 43, 35, 26, 17, 13 });
            Edad edad5 = new Edad(33, 37, list);

            List<int> list6 = new List<int>(new int[] { 55, 50, 42, 34, 25, 17, 13 });
            Edad edad6 = new Edad(38, 42, list);


            List<int> list7 = new List<int>(new int[] { 54, 49, 41, 33, 24, 16, 12 });
            Edad edad7 = new Edad(43, 47, list);


            List<int> list8 = new List<int>(new int[] { 49, 44, 36, 28, 20, 12, 8 });
            Edad edad8 = new Edad(48, 52, list);

            List<int> list9 = new List<int>(new int[] { 45, 41, 34, 26, 19, 12, 8 });
            Edad edad9 = new Edad(53, 57, list);

            List<int> list10 = new List<int>(new int[] { 45, 41, 34, 26, 19, 12, 8 });
            Edad edad10 = new Edad(58, 62, list);

            List<int> list11 = new List<int>(new int[] { 39, 35, 30, 24, 18, 12, 8 });
            Edad edad11 = new Edad(63, 67, list);


            List<int> list12 = new List<int>(new int[] { 39, 35, 30, 23, 16, 10, 6 });
            Edad edad12 = new Edad(68, 10000, list);


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
            edad.Add(edad11);
            edad.Add(edad12);


        }



    }
}
