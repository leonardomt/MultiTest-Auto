using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class _16pfTest
    {
        public List<String> puntuacionItem { get; set; }
        public List<String> clave { get; set; }
        public List<int> distorsion { get; set; }

        public List<_16PfTablePercentil> factoresAPF { get; set; }
        public List<_16PfTablePercentil> factoresAPM { get; set; }

        public _16pfTest()
        {
            puntuacionItem = new List<String>(new String[] { "210", "012", "010", "210", "012", "012", "210", "210", "210", "210", "012", "012", "012", "210", "210", "210", "210", "012", "010", "000", "210", "012", "210", "012", "012", "010", "012", "012", "210", "210", "210", "012", "210", "012", "012", "010", "010", "012", "210", "002", "210", "012", "012", "012", "210", "010", "012", "012", "200", "210", "012", "210", "010", "000", "200", "002", "210", "012", "210", "210", "012", "210", "010", "210", "012", "020", "012", "210", "012", "010", "000", "012", "012", "010", "010", "210", "012", "210", "210", "012", "012", "002", "012", "012", "012", "012", "010", "000", "012", "210", "012", "012", "012", "012", "210", "012", "012", "210", "020", "012", "012", "210", "012", "000", "010" });
            clave = new List<String>(new string[] { "A2", "A19", "A36", "A53", "A70", "A87", "A104", "B3", "B20", "B37", "B54", "B71", "B88", "B105", "C4", "C21", "C38", "C55", "C72", "C89", "E5", "E22", "E39", "E56", "E73", "E90", "F6", "F23", "F40", "F57", "F74", "F91", "G7", "G24", "G41", "G58", "G75", "G92", "H8", "H25", "H42", "H59", "H76", "H93", "I9", "I26", "I43", "I60", "I77", "I94", "L10", "L27", "L44", "L61", "L78", "L95", "M11", "M28", "M45", "M62", "M79", "M96", "N12", "N29", "N46", "N63", "N80", "N97", "O13", "O30", "O47", "O64", "O81", "O98", "Q114", "Q131", "Q148", "Q165", "Q182", "Q199", "Q215", "Q232", "Q249", "Q266", "Q283", "Q2100", "Q316", "Q333", "Q350", "Q367", "Q384", "Q3101", "Q417", "Q434", "Q451", "Q468", "Q485", "Q4102" });
            distorsion = new List<int>(new int[] { 1, 18, 35, 52, 69, 86, 103 });

            factoresAPF = new List<_16PfTablePercentil>();
            factoresAPM = new List<_16PfTablePercentil>();

            List<String> ap0List = new List<String>(new String[] { "A1", "B1", "C1", "E1", "F1", "G1", "H1", "I1", "L1", "M1", "N1", "O1", "Q11", "Q21", "Q31", "Q41" });
            _16PfTablePercentil ap0 = new _16PfTablePercentil(0, ap0List);

            List<String> ap1List = new List<String>(new String[] { "A1", "B3", "C1", "E3", "F1", "G1", "H1", "I1", "L2", "M1", "N2", "O2", "Q12", "Q21", "Q31", "Q41" });
            _16PfTablePercentil ap1 = new _16PfTablePercentil(1, ap1List);

            List<String> ap2List = new List<String>(new String[] { "A1", "B4", "C1", "E4", "F2", "G1", "H1", "I1", "L3", "M2", "N3", "O2", "Q13", "Q21", "Q31", "Q42" });
            _16PfTablePercentil ap2 = new _16PfTablePercentil(2, ap2List);

            List<String> ap3List = new List<String>(new String[] { "A2", "B5", "C2", "E5", "F3", "G1", "H2", "I1", "L4", "M3", "N4", "O3", "Q14", "Q21", "Q32", "Q43" });
            _16PfTablePercentil ap3 = new _16PfTablePercentil(3, ap3List);

            List<String> ap4List = new List<String>(new String[] { "A2", "B6", "C3", "E6", "F3", "G2", "H3", "I3", "L5", "M4", "N5", "O4", "Q15", "Q21", "Q33", "Q44" });
            _16PfTablePercentil ap4 = new _16PfTablePercentil(4, ap4List);

            List<String> ap5List = new List<String>(new String[] { "A3", "B7", "C4", "E7", "F4", "G3", "H3", "I3", "L6", "M5", "N6", "O5", "Q16", "Q22", "Q33", "Q45" });
            _16PfTablePercentil ap5 = new _16PfTablePercentil(5, ap5List);

            List<String> ap6List = new List<String>(new String[] { "A4", "B8", "C5", "E8", "F5", "G4", "H5", "I4", "L7", "M6", "N7", "O6", "Q17", "Q24", "Q34", "Q46" });
            _16PfTablePercentil ap6 = new _16PfTablePercentil(6, ap6List);

            List<String> ap7List = new List<String>(new String[] { "A5", "B9", "C6", "E9", "F6", "G6", "H6", "I5", "L8", "M7", "N8", "O7", "Q18", "Q25", "Q35", "Q47" });

            _16PfTablePercentil ap7 = new _16PfTablePercentil(7, ap7List);

            List<String> ap8List = new List<String>(new String[] { "A6", "B10", "C6", "E10", "F7", "G7", "H6", "I7", "L8", "M8", "N9", "O8", "Q18", "Q26", "Q36", "Q47" });
            _16PfTablePercentil ap8 = new _16PfTablePercentil(8, ap8List);

            List<String> ap9List = new List<String>(new String[] { "A7", "B0", "C7", "E10", "F7", "G7", "H8", "I7", "L9", "M9", "N9", "O9", "Q19", "Q27", "Q37", "Q48" });
            _16PfTablePercentil ap9 = new _16PfTablePercentil(9, ap9List);

            List<String> ap10List = new List<String>(new String[] { "A8", "B0", "C8", "E10", "F8", "G8", "H9", "I10", "L10", "M10", "N10", "O10", "Q110", "Q28", "Q38", "Q49" });
            _16PfTablePercentil ap10 = new _16PfTablePercentil(10, ap10List);


            List<String> ap11List = new List<String>(new String[] { "A9", "B0", "C9", "E10", "F9", "G9", "H9", "I9", "L9", "M10", "N10", "O10", "Q110", "Q29", "Q39", "Q410" });
            _16PfTablePercentil ap11 = new _16PfTablePercentil(11, ap11List);


            List<String> ap12List = new List<String>(new String[] { "A10", "B0", "C10", "E10", "F10", "G10", "H10", "I10", "L10", "M10", "N10", "O10", "Q110", "Q210", "Q310", "Q410" });
            _16PfTablePercentil ap12 = new _16PfTablePercentil(12, ap12List);


            factoresAPF.Add(ap0);
            factoresAPF.Add(ap1);
            factoresAPF.Add(ap2);
            factoresAPF.Add(ap3);
            factoresAPF.Add(ap4);
            factoresAPF.Add(ap5);
            factoresAPF.Add(ap6);
            factoresAPF.Add(ap7);
            factoresAPF.Add(ap8);
            factoresAPF.Add(ap9);
            factoresAPF.Add(ap10);
            factoresAPF.Add(ap11);
            factoresAPF.Add(ap12);



            List<String> ap0ListM = new List<String>(new String[] { "A1", "B1", "C1", "E1", "F1", "G1", "H1", "I1", "L1", "M1", "N1", "O1", "Q11", "Q21", "Q31", "Q41" });
            _16PfTablePercentil ap0M = new _16PfTablePercentil(0, ap0ListM);

            List<String> ap1ListM = new List<String>(new String[] { "A1", "B3", "C1", "E2", "F1", "G1", "H1", "I2", "L1", "M1", "N1", "O2", "Q12", "Q21", "Q31", "Q42" });
            _16PfTablePercentil ap1M = new _16PfTablePercentil(1, ap1ListM);

            List<String> ap2ListM = new List<String>(new String[] { "A1", "B4", "C1", "E3", "F1", "G1", "H1", "I3", "L2", "M2", "N2", "O3", "Q13", "Q21", "Q31", "Q43" });
            _16PfTablePercentil ap2M = new _16PfTablePercentil(2, ap2ListM);

            List<String> ap3ListM = new List<String>(new String[] { "A2", "B5", "C2", "E4", "F2", "G2", "H2", "I4", "L4", "M3", "N3", "O4", "Q13", "Q21", "Q31", "Q44" });
            _16PfTablePercentil ap3M = new _16PfTablePercentil(3, ap3ListM);

            List<String> ap4ListM = new List<String>(new String[] { "A2", "B6", "C3", "E5", "F3", "G3", "H3", "I5", "L5", "M4", "N4", "O5", "Q14", "Q21", "Q32", "Q45" });
            _16PfTablePercentil ap4M = new _16PfTablePercentil(4, ap4ListM);

            List<String> ap5ListM = new List<String>(new String[] { "A3", "B7", "C3", "E5", "F4", "G4", "H3", "I6", "L6", "M5", "N5", "O6", "Q15", "Q22", "Q33", "Q46" });
            _16PfTablePercentil ap5M = new _16PfTablePercentil(5, ap5ListM);

            List<String> ap6ListM = new List<String>(new String[] { "A4", "B8", "C4", "E6", "F4", "G5", "H4", "I7", "L7", "M6", "N6", "O7", "Q16", "Q23", "Q34", "Q47" });
            _16PfTablePercentil ap6M = new _16PfTablePercentil(6, ap6ListM);

            List<String> ap7ListM = new List<String>(new String[] { "A5", "B9", "C5", "E7", "F5", "G6", "H5", "I8", "L8", "M7", "N7", "O8", "Q17", "Q24", "Q34", "Q47" });
            _16PfTablePercentil ap7M = new _16PfTablePercentil(7, ap7ListM);

            List<String> ap8ListM = new List<String>(new String[] { "A6", "B10", "C6", "E8", "F6", "G7", "H6", "I9", "L9", "M8", "N8", "O9", "Q18", "Q25", "Q35", "Q49" });
            _16PfTablePercentil ap8M = new _16PfTablePercentil(8, ap8ListM);

            List<String> ap9ListM = new List<String>(new String[] { "A7", "B0", "C7", "E9", "F6", "G8", "H7", "I10", "L10", "M9", "N9", "O10", "Q19", "Q26", "Q36", "Q49" });
            _16PfTablePercentil ap9M = new _16PfTablePercentil(9, ap9ListM);

            List<String> ap10ListM = new List<String>(new String[] { "A8", "B0", "C8", "E9", "F7", "G8", "H7", "I10", "L10", "M10", "N10", "O10", "Q19", "Q28", "Q37", "Q410" });
            _16PfTablePercentil ap10M = new _16PfTablePercentil(10, ap10ListM);


            List<String> ap11ListM = new List<String>(new String[] { "A9", "B0", "C9", "E10", "F8", "G9", "H8", "I10", "L10", "M10", "N10", "O10", "Q110", "Q29", "Q38", "Q410" });
            _16PfTablePercentil ap11M = new _16PfTablePercentil(11, ap11ListM);


            List<String> ap12ListM = new List<String>(new String[] { "A10", "B0", "C10", "E10", "F10", "G10", "H10", "I10", "L10", "M10", "N10", "O10", "Q110", "Q210", "Q310", "Q410" });
            _16PfTablePercentil ap12M = new _16PfTablePercentil(12, ap12ListM);


            factoresAPM.Add(ap0);
            factoresAPM.Add(ap1);
            factoresAPM.Add(ap2);
            factoresAPM.Add(ap3);
            factoresAPM.Add(ap4);
            factoresAPM.Add(ap5);
            factoresAPM.Add(ap6);
            factoresAPM.Add(ap7);
            factoresAPM.Add(ap8);
            factoresAPM.Add(ap9);
            factoresAPM.Add(ap10);
            factoresAPM.Add(ap11);
            factoresAPM.Add(ap12);



            /*


            List<String> ap0List = new List<String>(new String[] { "A1", "B1", "C1", "E1", "F1", "G1", "H1", "I1", "L1", "M1", "N1", "O11", "Q11", "Q21", "Q31", "Q41" });
            _16PfTablePercentil ap0 = new _16PfTablePercentil(0, ap0List);

            List<String> ap1List = new List<String>(new String[] { "A1", "B2", "C1", "E3", "F1", "G1", "H1", "I2", "L2", "M1", "N2", "O2", "Q12", "Q21", "Q31", "Q42" });
            _16PfTablePercentil ap1 = new _16PfTablePercentil(1, ap1List);

            List<String> ap2List = new List<String>(new String[] { "A1", "B3", "C1", "E4", "F1", "G1", "H1", "I3", "L3", "M2", "N3", "O3", "Q13", "Q21", "Q31", "Q43" });
            _16PfTablePercentil ap2 = new _16PfTablePercentil(2, ap2List);

            List<String> ap3List = new List<String>(new String[] { "A1", "B4", "C2", "E5", "F2", "G2", "H2", "I4", "L4", "M3", "N4", "O4", "Q14", "Q21", "Q32", "Q44" });
            _16PfTablePercentil ap3 = new _16PfTablePercentil(3, ap3List);

            List<String> ap4List = new List<String>(new String[] { "A2", "B5", "C3", "E5", "F3", "G3", "H3", "I4", "L5", "M4", "N5", "O5", "Q14", "Q21", "Q32", "Q45" });
            _16PfTablePercentil ap4 = new _16PfTablePercentil(4, ap4List);

            List<String> ap5List = new List<String>(new String[] { "A3", "B5", "C4", "E6", "F4", "G4", "H4", "I5", "L6", "M5", "N6", "O6", "Q15", "Q22", "Q33", "Q45" });
            _16PfTablePercentil ap5 = new _16PfTablePercentil(5, ap5List);

            List<String> ap6List = new List<String>(new String[] { "A4", "B6", "C5", "E7", "F4", "G5", "H4", "I6", "L7", "M6", "N7", "O7", "Q16", "Q24", "Q34", "Q46" });
            _16PfTablePercentil ap6 = new _16PfTablePercentil(6, ap6List);

            List<String> ap7List = new List<String>(new String[] { "A5", "B7", "C5", "E8", "F5", "G5", "H5", "I6", "L8", "M7", "N8", "O8", "Q17", "Q25", "Q35", "Q47" });
            _16PfTablePercentil ap7 = new _16PfTablePercentil(7, ap7List);

            List<String> ap8List = new List<String>(new String[] { "A6", "B8", "C6", "E9", "F6", "G6", "H6", "I7", "L9", "M8", "N8", "O8", "Q18", "Q26", "Q36", "Q48" });
            _16PfTablePercentil ap8 = new _16PfTablePercentil(8, ap8List);

            List<String> ap9List = new List<String>(new String[] { "A7", "B9", "C7", "E9", "F7", "G7", "H7", "I8", "L9", "M9", "N9", "O9", "Q19", "Q27", "Q37", "Q49" });
            _16PfTablePercentil ap9 = new _16PfTablePercentil(9, ap9List);

            List<String> ap10List = new List<String>(new String[] { "A8", "B10", "C8", "E10", "F8", "G8", "H8", "I9", "L10", "M10", "N10", "O10", "Q110", "Q28", "Q38", "Q410" });
            _16PfTablePercentil ap10 = new _16PfTablePercentil(10, ap10List);


            List<String> ap11List = new List<String>(new String[] { "A9", "B10", "C9", "E10", "F9", "G9", "H9", "I10", "L10", "M10", "N10", "O10", "Q110", "Q28", "Q38", "Q410" });
            _16PfTablePercentil ap11 = new _16PfTablePercentil(11, ap11List);


            List<String> ap12List = new List<String>(new String[] { "A10", "B10", "C10", "E10", "F10", "G10", "H10", "I10", "L10", "M10", "N10", "O10", "Q110", "Q210", "Q310", "Q410" });
            _16PfTablePercentil ap12 = new _16PfTablePercentil(12, ap12List);


            */
        }
    }
}
