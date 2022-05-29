using Multitest.ADOmodel;
using Multitest.AuxClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.VentanasPruebas
{
    public partial class Form16Pf : Form
    {
        List<_16PF> listPregunta16PF;
        int countList = 0;
        int valor = -1;
        int checkMode = 0;

        String sexo;
        String idUser;
        String etapa;
        public Form16Pf(String idUser, String etapa, String sexo)
        {
            InitializeComponent();
            listPregunta16PF = new List<_16PF>();



            this.sexo = sexo;
            this.etapa = etapa;
            this.idUser = idUser;
            button5.TabStop = false;
            label9.Text = "Las preguntas que se le presentan le dan la oportunidad de expresar como es Ud., cuáles son sus gustos, sus intereses, sus actividades. Como cada persona es diferente, en realidad no hay respuestas  \"correctas\" o \"incorrectas\", sino solo que son verdaderas o falsas en su caso.\r\n" +
                          "A continuación encontrará dos preguntas que sirven de ejemplo para que usted vea exactamente cómo proceder. Debajo de cada una de estas preguntas, se ofrecen tres respuestas posibles para que usted escoja una.\r\n" ;
                          

            llenarPregunta16PF();
            nextQuestion16PF();

        }



        private void llenarPregunta16PF()
        {
            _16PF a1 = new _16PF("1.Creo que mi memoria es hoy mejor que nunca.", "Cierto.", "Dudoso.", "Falso.");
            _16PF a2 = new _16PF("2.Podría vivir solo,felizmente,lejos de cualquiera ,como un ermitaño.", "Cierto.", "Dudoso.", "Falso.");
            _16PF a3 = new _16PF("3.Si yo digo que el cielo está \"abajo\" y el invierno es \"caliente\", a un criminal lo llamaría:", "Un bandolero.", "Un santo.", "Una nube.");
            _16PF a4 = new _16PF("4.Cuando veo personas sucias,desaseadas:", "Simplemente las acepto.", "Dudoso.", "Me molesta y me disgusta.");
            _16PF a5 = new _16PF("5.Me molesta escuchar a la gente decir que puede hacer algo mejor que otros:", "Sí.", "A veces.", "No.");
            _16PF a6 = new _16PF("6.En las fiestas dejo que sean otras personas las que hagan los chistes y los cuentos:", "Sí.", "A veces.", "No.");
            _16PF a7 = new _16PF("7.Cuando tengo un tiempo libre siento que mi deber es emplearlo en actividades de utilidad social.", "Sí.", "Dudoso.", "No.");
            _16PF a8 = new _16PF("8.La mayoría de las personas que veo en una fiesta indudablemente se alegran de encontrarse conmigo.", "Sí.", "A veces.", "No.");
            _16PF a9 = new _16PF("9.Como ejercicio prefiero:", "Esgrima y Baile", "Dudoso", "Lucha y Pelota");
            _16PF a10 = new _16PF("10.Me sonrío de la gran diferencia que hay entre lo que hacen las personas y lo que dicen hacer.", "Sí.", "A veces.", "No.");
            _16PF a11 = new _16PF("11.Cuando niño me sentía triste al dejar el hogar para ir a la escuela cada día.", "Sí.", "A veces.", "No.");
            _16PF a12 = new _16PF("12.Si se pasa por alto una buena observación mía:", "La dejo pasar.", "Dudoso.", "Doy a la persona la oportunidad de escucharla nuevamente.");
            _16PF a13 = new _16PF("13.Cuando alguien tiene malos modales pienso:", "Que no es asunto mío.", "Dudoso.", "Que debo mostrar a la persona que la gente lo desaprueba.");
            _16PF a14 = new _16PF("14.Al conocer una nueva persona prefiero:", "Discutir con ella sus puntos de vista políticos y sociales. ", "Dudoso.", "Que me cuente algunos buenos chistes.");
            _16PF a15 = new _16PF("15.Cuando planeo algo me gusta hacerlo totalmente solo,sin ayuda externa:", "Sí.", "A veces.", "No.");
            _16PF a16 = new _16PF("16.Evito consumir tiempo soñando acerca de: \"lo que pudiera haber sido\"", "Sí", "A veces", "No");
            _16PF a17 = new _16PF("17.Cuando voy a tomar un tren, me siento algo apresurado, tenso o ansioso, aunque sepa que tengo tiempo:", "Sí", "A veces", "No.");
            _16PF a18 = new _16PF("18.Algunas veces he tenido,aunque sea brevemente, sentimientos hostiles hacia mis padres", "Sí.", "Dudoso.", "No.");
            _16PF a19 = new _16PF("19.Yo podría ser feliz en un trabajo que requiera escuchar todo el día quejas desagradables de clientes y empleados.", "Sí.", "Dudoso.", "No.");
            _16PF a20 = new _16PF("20.Pienso que el opuesto de \"inexacto\" es:", "Casual.", "Preciso.", "Aproximado.");
            _16PF a21 = new _16PF("21.Siempre dispongo de gran cantidad de energía en el momento que lo necesito:", "Sí.", "Dudoso.", "No.");
            _16PF a22 = new _16PF("22.Me sería extremadamente penoso decir a la gente que he pedido dinero prestado:", "Sí.", "Dudoso.", "No.");
            _16PF a23 = new _16PF("23.Disfruto grandemente todas las grandes reuniones como fiestas y bailes:", "Sí.", "A veces.", "No.");
            _16PF a24 = new _16PF("24.Pienso que:", "Algunos trabajos no requieren hacerse tan cuidadosamente como otros", "Dudoso", "Cualquier trabajo debe ser realizado a \"cociencia\" si es que ha de hacerse.");
            _16PF a25 = new _16PF("25.Me disgusta la forma que algunas personas lo miran a uno en calles o tiendas:", "Sí.", "Dudoso.", "No.");
            _16PF a26 = new _16PF("26.Yo preferiría destacarme:", "Como artista.", "Dudoso.", "Como atleta.");
            _16PF a27 = new _16PF("27.Si un vecino me engaña en cosas triviales,prefiero \"hacerme de la vista gorda\" que desenmascararlo", "Sí.", "Dudoso.", "No.");
            _16PF a28 = new _16PF("28.Preferiría ver:", "Una buena película sobre los días difíciles de la guerra.", "Dudoso.", "Una comedia ingeniosa sobre la sociedad del futuro.");
            _16PF a29 = new _16PF("29.Cuando se me pone a cargo de una cosa, insisto en que mis instrucciones sean seguidas, o de lo contrario renuncio:", "Sí.", "A veces.", "No.");
            _16PF a30 = new _16PF("30.Encuentro juicioso evitar una excitación excesiva porque esto tiende a agotarme:", "Sí.", "A veces.", "No.");
            _16PF a31 = new _16PF("31.Si fuera bueno en ambas cosas preferiría jugar:", "Ajedrez", "Dudoso", "Bolos");
            _16PF a32 = new _16PF("32.Pienso que es cruel vacunar a los niños muy pequeños, aun contra enfermedades infecciosas y los padres tienen derecho a oponerse:", "Sí.", "Dudoso.", "No.");
            _16PF a33 = new _16PF("33.Tengo más fé:", "En la acción planificada", "Dudoso", "En la buena suerte");
            _16PF a34 = new _16PF("34.Siempre que lo necesito puedo olvidar mis inquietudes y responsabilidades:", "Sí.", "A veces.", "No.");
            _16PF a35 = new _16PF("35.Encuentro difícil admitir que estoy equivocado:", "Sí.", "A veces.", "No.");
            _16PF a36 = new _16PF("36.En la fábrica preferiría estar a cargo de:", "Maquinaria o mantenimiento de registros.", "Dudoso.", "Emplear y conversar con el nuevo personal.");
            _16PF a37 = new _16PF("37.Qué palabra no corresponde con las otras dos:", "Gato", "Cercano", "Sol");
            _16PF a38 = new _16PF("38.Mi salud es afectada por cambios súbitos, causado por este motivo que altere mis planes:", "Sí.", "A veces.", "No.");
            _16PF a39 = new _16PF("39.Me complace ser servido, en momentos apropiados, por sirvientes personales:", "A menudo.", "Raras veces.", "Nunca.");
            _16PF a40 = new _16PF("40.Me siento algo torpe en compañía de otras personas, de modo que no puedo hacer el buen papel que debería:", "Sí.", "A veces.", "No.");
            _16PF a41 = new _16PF("41.Pienso que las personas deberían observar las leyes morales más estrictamente de lo que lo hacen:", "Sí.", "A veces.", "No.");
            _16PF a42 = new _16PF("42.Algunas cosas me ponen tan irritado que prefiero no hablar:", "Sí.", "Dudoso.", "No.");
            _16PF a43 = new _16PF("43.Puedo realizar trabajos físicos duros sin agotarme tan pronto como otras personas:", "Sí.", "A veces.", "No.");
            _16PF a44 = new _16PF("44.Pienso que la mayoría de los testigos dicen la verdad aún cuando resulte penoso:", "Sí.", "Dudoso.", "No.");
            _16PF a45 = new _16PF("45.Considero que es más provechoso caminar de un lado a otro cuando estoy pensando:", "Sí.", "Dudoso.", "No.");
            _16PF a46 = new _16PF("46.Pienso que este país haría mejor en gastar más en:", "Armamentos.", "Dudoso.", "Educación.");
            _16PF a47 = new _16PF("47.Preferiría emplear una noche:", "En un juego difícil de cartas.", "Dudoso.", "Mirando fotos de vacaciones anteriores.");
            _16PF a48 = new _16PF("48.Preferiría leer:", "Una buena novela histórica.", "Dudoso.", " Un ensayo de un científico sobre el dominio de los recursos mundiales.");
            _16PF a49 = new _16PF("49.En realidad en el mundo existen más personas agradables que desagradables:", "Sí.", "Dudoso.", "No.");
            _16PF a50 = new _16PF("50.Honestamente pienso que estoy más planificado, enérgico y emprendedor que muchas de las personas a quienes les va tan bien como a mí:", "Sí.", "A veces.", "No.");
            _16PF a51 = new _16PF("51.Hay momentos que no me siento con la debida disposición de ánimo para ver a ninguna persona:", "Muy raramente.", "Dudoso.", "Con bastante frecuencia.");
            _16PF a52 = new _16PF("52.Cuando sé que estoy haciendo algo correcto, encuentro la tarea fácil:", "Siempre.", "A veces.", "Rara vez.");
            _16PF a53 = new _16PF("53.Yo preferiría:", "Estar en una oficina comercial,organizando y recibiendo personas.", "Dudoso.", "Ser arquitecto trazando planos en una habitación aislada.");
            _16PF a54 = new _16PF("54.El negro es al gris como el dolor a:", "La herida.", "La enfermedad.", "La incomodidad.");
            _16PF a55 = new _16PF("55.Siempre duermo profundamente sin caminar o hablar en sueños:", "Sí.", "Dudoso.", "No.");
            _16PF a56 = new _16PF("56.Puedo mirar a cualquiera directamente a la cara y decir una mentira (si es con un fin correcto):", "Sí.", "A veces.", "No.");
            _16PF a57 = new _16PF("57.Yo he participado activamente en la organización de clubes, equipos o grupos sociales:", "Sí.", "A veces.", "No.");
            _16PF a58 = new _16PF("58.Yo admiro más:", "Un hombre inteligente aunque poco confiable.", "Dudoso.", "Un hombre promedio,pero fuerte para resistir las tentaciones.");
            _16PF a59 = new _16PF("59.Cuando planteo una idea justa siempre logro que las cosas se ajusten a mi satisfacción:", "Sí.", "A veces.", "No.");
            _16PF a60 = new _16PF("60.Circunstancias desalentadoras pueden llevarme cercano a las lágrimas:", "Sí.", "A veces.", "No.");
            _16PF a61 = new _16PF("61.Pienso que muchos países son realmente más amistosos de lo que suponemos:", "Sí.", "A veces.", "No.");
            _16PF a62 = new _16PF("62.Hay momentos, todos los días, en que deseo disfrutar de mis pensamientos sin ser interrumpido por otras personas:", "Sí.", "Dudoso.", "No.");
            _16PF a63 = new _16PF("63.Me molesta ser limitado por pequeñas leyes y reglamentos, aunque admito que son realmente necesarios:", "Sí.", "Dudoso.", "No.");
            _16PF a64 = new _16PF("64.Pienso que mucha de la llamada educación moderna \"progresista\" es menos acertada que el viejo criterio de que  \"si no se pega al niño, se malcría\":", "Cierto.", "Dudoso.", "Falso.");
            _16PF a65 = new _16PF("65.En mis días escolares aprendí más:", "Concurriendo a clases.", "Dudoso.", "Leyendo un libro.");
            _16PF a66 = new _16PF("66.Evito verme envuelto en responsabilidades y organizaciones sociales:", "Cierto.", "A veces.", "Falso.");
            _16PF a67 = new _16PF("67.Cuando un problema se hace difícil y hay mucho que hacer, pruebo:", "Un problema diferente.", "Dudoso.", "Un enfoque diferente sobre el mismo problema.");
            _16PF a68 = new _16PF("68.Sufro de fuertes estados emocionales, ansiedad, ira, risa, etc. que parecen no tener causa real:", "Sí.", "A veces.", "No.");
            _16PF a69 = new _16PF("69.Hay ocasiones en que mi mente no trabaja con tanta claridad como en otras:", "Cierto.", "Dudoso.", "Falso.");
            _16PF a70 = new _16PF("70.Me agrada complacer a otras personas haciendo los compromisos en las horas que ellos desean, aunque sea algo inconveniente para mí:", "Sí.", "A veces.", "No.");
            _16PF a71 = new _16PF("71.Estimo que el número apropiado para continuar la serie 1,2,3,6,5 es:", "10.", "5.", "7.");
            _16PF a72 = new _16PF("72.Soy propenso a criticar el trabajo de otros:", "Sí.", "A veces.", "No.");
            _16PF a73 = new _16PF("73.Prefiero prescindir de algo que causaría a un camarero un gran trabajo extra:", "Sí.", "A veces.", "No.");
            _16PF a74 = new _16PF("74.Me encanta viajar, en cualquier momento:", "Sí.", "A veces.", "No.");
            _16PF a75 = new _16PF("75.A veces he estado a punto de desmayarme ante un dolor violento o a la vista de sangre:", "Sí.", "Dudoso.", "No.");
            _16PF a76 = new _16PF("76.Disfruto mucho al conversar con otras personas sobre problemas locales:", "Sí.", "A veces.", "No.");
            _16PF a77 = new _16PF("77.Preferiría ser:", "Ingeniero de construcciones.", "Dudoso.", "Profesor de teoría y costumbres sociales.");
            _16PF a78 = new _16PF("78.Tengo que refrenarme para no involucrarme demasiado en los problemas de los demás:", "Sí.", "A veces.", "No.");
            _16PF a79 = new _16PF("79.Encuentro que la conversación de mis vecinos es aburrida y tediosa:", "En la mayoría de los casos.", "Dudoso.", "Solo en algunos casos.");
            _16PF a80 = new _16PF("80.Por lo general, yo no noto la propaganda disimulada en lo que leo, a no ser que alquien me lo señale:", "Cierto.", "A veces.", "Falso.");
            _16PF a81 = new _16PF("81.Opino que todo cuento y película deben tener una moraleja:", "Sí.", "A veces.", "No.");
            _16PF a82 = new _16PF("82.Surgen más problemas de personas:", "Que cambian y modifican métodos que hasta ahora han dado resultado.", "Dudoso.", "Que rechazan nuevos métodos prometedores.");
            _16PF a83 = new _16PF("83.Algunas veces dudo usar mis propias ideas por temor a que sean poco prácticas:", "Sí.", "Dudoso.", "No.");
            _16PF a84 = new _16PF("84.Las personas \"estiradas\", estrictas, parecen no llevarse bien conmigo:", "Cierto.", "A veces.", "Falso.");
            _16PF a85 = new _16PF("85.Mi memoria no cambia mucho de un día a otro:", "Cierto.", "A veces.", "Falso.");
            _16PF a86 = new _16PF("86.Puede que yo sea menos considerado con otras persona que lo que ellas son conmigo:", "Cierto.", "A veces.", "Falso.");
            _16PF a87 = new _16PF("87.Me modero más que la mayoría de las personas en expresar cuáles son mis sentimientos:", "Sí.", "A veces.", "No.");
            _16PF a88 = new _16PF("88.Si dos manecillas de reloj se juntan exactamente cada 65 minutos, el reloj está funcionando:", "Atrasado.", "A su hora.", "Adelantado.");
            _16PF a89 = new _16PF("89.Me pongo impaciente, y comienzo a disgutarme y a alterarme cuando las personas me demoran innecesariamente:", "Cierto.", "A veces.", "Falso.");
            _16PF a90 = new _16PF("90.La gente dice que me gusta que las cosas se hagan a mi modo:", "Cierto.", "A veces.", "Falso.");
            _16PF a91 = new _16PF("91.Generalmente no diría nada si los instrumentos que me entregan para realizar un trabajo no son del todo adecuado:", "Cierto.", "A veces.", "Falso.");
            _16PF a92 = new _16PF("92.En mi hogar,cuando dispongo de un tiempo libre,yo:", "Lo uso para conversar y descansar.", "Dudoso.", "Planifico para emplearlo en trabajos especiales.");
            _16PF a93 = new _16PF("93.Soy tímido y cuidadoso para hacer nuevas amistades:", "Sí.", "A veces.", "No.");
            _16PF a94 = new _16PF("94.Creo que lo que las personas dicen en poesía podría decirse igual en prosa:", "Sí.", "A veces.", "No.");
            _16PF a95 = new _16PF("95.Sospecho que las personas que actúan amistosamente para conmigo pueden ser desleales a mis espaldas:", "Sí,generalmete.", "En ocasiones.", "No,muy rara vez");
            _16PF a96 = new _16PF("96.Pienso que aún las experiencias más dramáticas que tengo durante el año dejan mi personalidad casi lo mismo que era:", "Sí.", "A veces.", "No.");
            _16PF a97 = new _16PF("97.Tiendo a hablar más bien lentamente:", "Sí.", "A veces.", "No.");
            _16PF a98 = new _16PF("98.Siento temores irrazonables o aversiones por algunas cosas, por ejemplo,ciertos animales,lugares,etc:", "Sí.", "A veces.", "No.");
            _16PF a99 = new _16PF("99.En una tarea en grupo preferiría:", "Ser quien ensaye en la organización.", "Dudoso.", "Llevar los registros y ver que las reglas sean obervadas.");
            _16PF a100 = new _16PF("100.Para opinar bien sobre un asunto social, leería:", "Una novela sobre el tema ampliamente recomendada.", "Dudoso.", "Un texto enumerando datos,estadísticas y otros.");
            _16PF a101 = new _16PF("101.Tengo sueños bastantes fantásticos o ridículos (Durmiendo):", "Sí.", "A veces.", "No.");
            _16PF a102 = new _16PF("102.Si me dejan en una casa solitaria, después de un tiempo, tiendo a sentirme ansioso o temeroso:", "Sí.", "A veces.", "No.");
            _16PF a103 = new _16PF("103.Puedo engañar a las personas portándome amistoso cuando realmente las detesto:", "Sí .", "A veces.", "No.");
            _16PF a104 = new _16PF("104.Qué palabra no corresponde con las otras dos:", "Correr.", "Ver.", "Tocar.");
            _16PF a105 = new _16PF("105.Si la madre de María es hermana del padre de Federico,que parentezco existe entre Federico y el padre de María:", "Primo.", "Sobrino.", "Tío.");




            listPregunta16PF.Add(a1);
            listPregunta16PF.Add(a2);
            listPregunta16PF.Add(a3);
            listPregunta16PF.Add(a4);
            listPregunta16PF.Add(a5);
            listPregunta16PF.Add(a6);
            listPregunta16PF.Add(a7);
            listPregunta16PF.Add(a8);
            listPregunta16PF.Add(a9);
            listPregunta16PF.Add(a10);
            listPregunta16PF.Add(a11);
            listPregunta16PF.Add(a12);
            listPregunta16PF.Add(a13);
            listPregunta16PF.Add(a14);
            listPregunta16PF.Add(a15);
            listPregunta16PF.Add(a16);
            listPregunta16PF.Add(a17);
            listPregunta16PF.Add(a18);
            listPregunta16PF.Add(a19);
            listPregunta16PF.Add(a20);
            listPregunta16PF.Add(a21);
            listPregunta16PF.Add(a22);
            listPregunta16PF.Add(a23);
            listPregunta16PF.Add(a24);
            listPregunta16PF.Add(a25);
            listPregunta16PF.Add(a26);
            listPregunta16PF.Add(a27);
            listPregunta16PF.Add(a28);
            listPregunta16PF.Add(a29);
            listPregunta16PF.Add(a30);
            listPregunta16PF.Add(a31);
            listPregunta16PF.Add(a32);
            listPregunta16PF.Add(a33);
            listPregunta16PF.Add(a34);
            listPregunta16PF.Add(a35);
            listPregunta16PF.Add(a36);
            listPregunta16PF.Add(a37);
            listPregunta16PF.Add(a38);
            listPregunta16PF.Add(a39);
            listPregunta16PF.Add(a40);
            listPregunta16PF.Add(a41);
            listPregunta16PF.Add(a42);
            listPregunta16PF.Add(a43);
            listPregunta16PF.Add(a44);
            listPregunta16PF.Add(a45);
            listPregunta16PF.Add(a46);
            listPregunta16PF.Add(a47);
            listPregunta16PF.Add(a48);
            listPregunta16PF.Add(a49);
            listPregunta16PF.Add(a50);
            listPregunta16PF.Add(a51);
            listPregunta16PF.Add(a52);
            listPregunta16PF.Add(a53);
            listPregunta16PF.Add(a54);
            listPregunta16PF.Add(a55);
            listPregunta16PF.Add(a56);
            listPregunta16PF.Add(a57);
            listPregunta16PF.Add(a58);
            listPregunta16PF.Add(a59);
            listPregunta16PF.Add(a60);
            listPregunta16PF.Add(a61);
            listPregunta16PF.Add(a62);
            listPregunta16PF.Add(a63);
            listPregunta16PF.Add(a64);
            listPregunta16PF.Add(a65);
            listPregunta16PF.Add(a66);
            listPregunta16PF.Add(a67);
            listPregunta16PF.Add(a68);
            listPregunta16PF.Add(a69);
            listPregunta16PF.Add(a70);
            listPregunta16PF.Add(a71);
            listPregunta16PF.Add(a72);
            listPregunta16PF.Add(a73);
            listPregunta16PF.Add(a74);
            listPregunta16PF.Add(a75);
            listPregunta16PF.Add(a76);
            listPregunta16PF.Add(a77);
            listPregunta16PF.Add(a78);
            listPregunta16PF.Add(a79);
            listPregunta16PF.Add(a80);
            listPregunta16PF.Add(a81);
            listPregunta16PF.Add(a82);
            listPregunta16PF.Add(a83);
            listPregunta16PF.Add(a84);
            listPregunta16PF.Add(a85);
            listPregunta16PF.Add(a86);
            listPregunta16PF.Add(a87);
            listPregunta16PF.Add(a88);
            listPregunta16PF.Add(a89);
            listPregunta16PF.Add(a90);
            listPregunta16PF.Add(a91);
            listPregunta16PF.Add(a92);
            listPregunta16PF.Add(a93);
            listPregunta16PF.Add(a94);
            listPregunta16PF.Add(a95);
            listPregunta16PF.Add(a96);
            listPregunta16PF.Add(a97);
            listPregunta16PF.Add(a98);
            listPregunta16PF.Add(a99);
            listPregunta16PF.Add(a100);
            listPregunta16PF.Add(a101);
            listPregunta16PF.Add(a102);
            listPregunta16PF.Add(a103);
            listPregunta16PF.Add(a104);
            listPregunta16PF.Add(a105);


        }


        private void nextQuestion16PF()
        {


            if (countList != listPregunta16PF.Count)
            {


                if (checkMode == 0)
                {

                    label4.Text = listPregunta16PF[countList].Pregunta;

                    label6.Text = listPregunta16PF[countList].A;
                    label7.Text = listPregunta16PF[countList].B;
                    label8.Text = listPregunta16PF[countList].C;

                    limpiarLabelColor();
                }
                else
                {
                    limpiarLabelColor();
                    label4.Text = listPregunta16PF[countList].Pregunta;

                    String result = listPregunta16PF[countList].valor;
                    valor = Convert.ToInt32(listPregunta16PF[countList].valor);

                    if (result == "1")
                    {
                        label2.BackColor = Color.Red;

                    }
                    if (result == "2")
                    {
                        label3.BackColor = Color.Red;
                    }

                    if (result == "3")
                    {
                        label5.BackColor = Color.Red;
                    }


                }


            }
            else
            {


                calificar16Pf();

                MessageBox.Show("Prueba terminada. Debe informar al especialista", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();

                //   }

            }

        }


        private void limpiarLabelColor()
        {

            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;

            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form16Pf_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar))
            {

                int res = Convert.ToInt32(e.KeyChar.ToString());




                if (res >= 1 && res <= 3)
                {
                    limpiarLabelColor();
                    pintarForeColor(res);
                    valor = res;


                }




            }


            if (valor != -1)
            {

                if (e.KeyChar.ToString() == "\r")
                {
                    if (checkMode == 0)
                    {


                        listPregunta16PF[countList].valor = valor.ToString();


                    }
                    else
                    {

                        listPregunta16PF[countList].valor = valor.ToString();


                    }

                    countList++;
                    valor = -1;


                    nextQuestion16PF();


                }

            }
        }


        private void pintarForeColor(int res)
        {

            if (res == 1)
            {
                label2.BackColor = Color.Red;
                label2.ForeColor = Color.White;

            }
            if (res == 2)
            {
                label3.BackColor = Color.Red;
                label3.ForeColor = Color.White;
            }

            if (res == 3)
            {
                label5.BackColor = Color.Red;
                label5.ForeColor = Color.White;
            }


        }



        private void calificar16Pf()
        {


            // Aqui se calcula el factor AB 
            List<FactorAB> listFactorAB = calcularFactorAB();


            int distorision = calcularDistorsión();

            // aqui se calcula el factor AP

            List<FactorAB> listFactorAP = calcularFactorAP(listFactorAB);


            //Efecto Halo
            if (distorision == 7)
            {
                //O= 12   Q4=16
                //Q3=15  C=2  H=6 A=0 G=3  Q2=14  L=8
                listFactorAP[12].puntuacionAP += 1;
                listFactorAP[16].puntuacionAP += 1;

                listFactorAP[2].puntuacionAP -= 1;
                listFactorAP[6].puntuacionAP -= 1;
                listFactorAP[15].puntuacionAP -= 1;

            }
            if (distorision >= 8 && distorision <= 9)
            {
                listFactorAP[8].puntuacionAP += 2;

                listFactorAP[12].puntuacionAP += 2;

                listFactorAP[16].puntuacionAP += 2;

                listFactorAP[0].puntuacionAP -= 1;
                listFactorAP[2].puntuacionAP -= 1;
                listFactorAP[3].puntuacionAP -= 1;
                listFactorAP[6].puntuacionAP -= 1;
                listFactorAP[15].puntuacionAP -= 1;
            }
            if (distorision >= 10)
            {

                listFactorAP[12].puntuacionAP += 2;
                listFactorAP[16].puntuacionAP += 2;

                listFactorAP[8].puntuacionAP += 1;
                listFactorAP[14].puntuacionAP += 1;


                listFactorAP[15].puntuacionAP -= 2;
                listFactorAP[2].puntuacionAP -= 2;

                listFactorAP[0].puntuacionAP -= 1;
                listFactorAP[6].puntuacionAP -= 1;
                listFactorAP[3].puntuacionAP -= 1;



            }

             

            String distorsionFinal = "Ninguno de los Valores";

            if (distorision >= 0 && distorision <= 5)
            {
                distorsionFinal = "No distorsión";
            }

            else if (distorision >= 6 && distorision <= 8)
            {
                distorsionFinal = "Poca distorsión";
            }

            else if (distorision >= 9 && distorision <= 11)
            {
                distorsionFinal = "Alguna distorsión";
            }

            else if (distorision >= 12 && distorision <= 14)
            {
                distorsionFinal = "Distorsión.No confiar en los resultados.";
            }
           


            String factorQ1 = calcularFactorQ1(listFactorAP);
            String factorQ2 = calcularFactorQ2(listFactorAP);
            String factorQ3 = calcularFactorQ3(listFactorAP);
            String factorQ4 = calcularFactorQ4(listFactorAP);
            String neuroticismo = calcularNeuroticismo(listFactorAP);



            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {

                    Pru16pf pruR = new Pru16pf();
                    pruR.Fecha = date;
                    pruR.DuraPru = "";
                    pruR.AnotBrutaA = listFactorAP[0].puntuacionAB.ToString();
                    pruR.AnotBrutaB = listFactorAP[1].puntuacionAB.ToString();
                    pruR.AnotBrutaC = listFactorAP[2].puntuacionAB.ToString();
                    pruR.AnotBrutaE = listFactorAP[3].puntuacionAB.ToString();
                    pruR.AnotBrutaF = listFactorAP[4].puntuacionAB.ToString();
                    pruR.AnotBrutaG = listFactorAP[5].puntuacionAB.ToString();
                    pruR.AnotBrutaH = listFactorAP[6].puntuacionAB.ToString();
                    pruR.AnotBrutaI = listFactorAP[7].puntuacionAB.ToString();
                    pruR.AnotBrutaL = listFactorAP[8].puntuacionAB.ToString();
                    pruR.AnotBrutaM = listFactorAP[9].puntuacionAB.ToString();
                    pruR.AnotBrutaN = listFactorAP[10].puntuacionAB.ToString();
                    pruR.AnotBrutaO = listFactorAP[11].puntuacionAB.ToString();
                    pruR.AnotBrutaQ1 = listFactorAP[12].puntuacionAB.ToString();
                    pruR.AnotBrutaQ2 = listFactorAP[13].puntuacionAB.ToString();
                    pruR.AnotBrutaQ3 = listFactorAP[14].puntuacionAB.ToString();
                    pruR.AnotBrutaQ4 = listFactorAP[15].puntuacionAB.ToString();

                    pruR.AnotPesadaA = listFactorAP[0].puntuacionAP.ToString();
                    pruR.AnotPesadaB = listFactorAP[1].puntuacionAP.ToString();
                    pruR.AnotPesadaC = listFactorAP[2].puntuacionAP.ToString();
                    pruR.AnotPesadaE = listFactorAP[3].puntuacionAP.ToString();
                    pruR.AnotPesadaF = listFactorAP[4].puntuacionAP.ToString();
                    pruR.AnotPesadaG = listFactorAP[5].puntuacionAP.ToString();
                    pruR.AnotPesadaH = listFactorAP[6].puntuacionAP.ToString();
                    pruR.AnotPesadaI = listFactorAP[7].puntuacionAP.ToString();
                    pruR.AnotPesadaL = listFactorAP[8].puntuacionAP.ToString();
                    pruR.AnotPesadaM = listFactorAP[9].puntuacionAP.ToString();
                    pruR.AnotPesadaN = listFactorAP[10].puntuacionAP.ToString();
                    pruR.AnotPesadaO = listFactorAP[11].puntuacionAP.ToString();
                    pruR.AnotPesadaQ1 = listFactorAP[12].puntuacionAP.ToString();
                    pruR.AnotPesadaQ2 = listFactorAP[13].puntuacionAP.ToString();
                    pruR.AnotPesadaQ3 = listFactorAP[14].puntuacionAP.ToString();
                    pruR.AnotPesadaQ4 = listFactorAP[15].puntuacionAP.ToString();

                    pruR.Perfil1 = factorQ1;
                    pruR.Perfil2 = factorQ2;
                    pruR.Perfil3 = factorQ3;
                    pruR.Perfil4 = factorQ4;
                    pruR.Neuroticismo = neuroticismo;
                    pruR.Distorsion = distorsionFinal;

                    entities.Pru16pf.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<Pru16pf>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.P16pf = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.P16pf == null)
                    {

                        Pru16pf pruR = new Pru16pf();
                        pruR.Fecha = date;
                        pruR.DuraPru = "";
                        pruR.AnotBrutaA = listFactorAP[0].puntuacionAB.ToString();
                        pruR.AnotBrutaB = listFactorAP[1].puntuacionAB.ToString();
                        pruR.AnotBrutaC = listFactorAP[2].puntuacionAB.ToString();
                        pruR.AnotBrutaE = listFactorAP[3].puntuacionAB.ToString();
                        pruR.AnotBrutaF = listFactorAP[4].puntuacionAB.ToString();
                        pruR.AnotBrutaG = listFactorAP[5].puntuacionAB.ToString();
                        pruR.AnotBrutaH = listFactorAP[6].puntuacionAB.ToString();
                        pruR.AnotBrutaI = listFactorAP[7].puntuacionAB.ToString();
                        pruR.AnotBrutaL = listFactorAP[8].puntuacionAB.ToString();
                        pruR.AnotBrutaM = listFactorAP[9].puntuacionAB.ToString();
                        pruR.AnotBrutaN = listFactorAP[10].puntuacionAB.ToString();
                        pruR.AnotBrutaO = listFactorAP[11].puntuacionAB.ToString();
                        pruR.AnotBrutaQ1 = listFactorAP[12].puntuacionAB.ToString();
                        pruR.AnotBrutaQ2 = listFactorAP[13].puntuacionAB.ToString();
                        pruR.AnotBrutaQ3 = listFactorAP[14].puntuacionAB.ToString();
                        pruR.AnotBrutaQ4 = listFactorAP[15].puntuacionAB.ToString();

                        pruR.AnotPesadaA = listFactorAP[0].puntuacionAP.ToString();
                        pruR.AnotPesadaB = listFactorAP[1].puntuacionAP.ToString();
                        pruR.AnotPesadaC = listFactorAP[2].puntuacionAP.ToString();
                        pruR.AnotPesadaE = listFactorAP[3].puntuacionAP.ToString();
                        pruR.AnotPesadaF = listFactorAP[4].puntuacionAP.ToString();
                        pruR.AnotPesadaG = listFactorAP[5].puntuacionAP.ToString();
                        pruR.AnotPesadaH = listFactorAP[6].puntuacionAP.ToString();
                        pruR.AnotPesadaI = listFactorAP[7].puntuacionAP.ToString();
                        pruR.AnotPesadaL = listFactorAP[8].puntuacionAP.ToString();
                        pruR.AnotPesadaM = listFactorAP[9].puntuacionAP.ToString();
                        pruR.AnotPesadaN = listFactorAP[10].puntuacionAP.ToString();
                        pruR.AnotPesadaO = listFactorAP[11].puntuacionAP.ToString();
                        pruR.AnotPesadaQ1 = listFactorAP[12].puntuacionAP.ToString();
                        pruR.AnotPesadaQ2 = listFactorAP[13].puntuacionAP.ToString();
                        pruR.AnotPesadaQ3 = listFactorAP[14].puntuacionAP.ToString();
                        pruR.AnotPesadaQ4 = listFactorAP[15].puntuacionAP.ToString();

                        pruR.Perfil1 = factorQ1;
                        pruR.Perfil2 = factorQ2;
                        pruR.Perfil3 = factorQ3;
                        pruR.Perfil4 = factorQ4;
                        pruR.Neuroticismo = neuroticismo;
                        pruR.Distorsion = distorsionFinal;

                        entities.Pru16pf.Add(pruR);
                        entities.SaveChangesAsync();




                        var ultimo = entities.Set<Pru16pf>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.P16pf = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.Pru16pf.Where(f => f.idTest == sujetoEva.P16pf).FirstOrDefault<Pru16pf>();



                        conect.DuraPru = "";
                        conect.AnotBrutaA = listFactorAP[0].puntuacionAB.ToString();
                        conect.AnotBrutaB = listFactorAP[1].puntuacionAB.ToString();
                        conect.AnotBrutaC = listFactorAP[2].puntuacionAB.ToString();
                        conect.AnotBrutaE = listFactorAP[3].puntuacionAB.ToString();
                        conect.AnotBrutaF = listFactorAP[4].puntuacionAB.ToString();
                        conect.AnotBrutaG = listFactorAP[5].puntuacionAB.ToString();
                        conect.AnotBrutaH = listFactorAP[6].puntuacionAB.ToString();
                        conect.AnotBrutaI = listFactorAP[7].puntuacionAB.ToString();
                        conect.AnotBrutaL = listFactorAP[8].puntuacionAB.ToString();
                        conect.AnotBrutaM = listFactorAP[9].puntuacionAB.ToString();
                        conect.AnotBrutaN = listFactorAP[10].puntuacionAB.ToString();
                        conect.AnotBrutaO = listFactorAP[11].puntuacionAB.ToString();
                        conect.AnotBrutaQ1 = listFactorAP[12].puntuacionAB.ToString();
                        conect.AnotBrutaQ2 = listFactorAP[13].puntuacionAB.ToString();
                        conect.AnotBrutaQ3 = listFactorAP[14].puntuacionAB.ToString();
                        conect.AnotBrutaQ4 = listFactorAP[15].puntuacionAB.ToString();

                        conect.AnotPesadaA = listFactorAP[0].puntuacionAP.ToString();
                        conect.AnotPesadaB = listFactorAP[1].puntuacionAP.ToString();
                        conect.AnotPesadaC = listFactorAP[2].puntuacionAP.ToString();
                        conect.AnotPesadaE = listFactorAP[3].puntuacionAP.ToString();
                        conect.AnotPesadaF = listFactorAP[4].puntuacionAP.ToString();
                        conect.AnotPesadaG = listFactorAP[5].puntuacionAP.ToString();
                        conect.AnotPesadaH = listFactorAP[6].puntuacionAP.ToString();
                        conect.AnotPesadaI = listFactorAP[7].puntuacionAP.ToString();
                        conect.AnotPesadaL = listFactorAP[8].puntuacionAP.ToString();
                        conect.AnotPesadaM = listFactorAP[9].puntuacionAP.ToString();
                        conect.AnotPesadaN = listFactorAP[10].puntuacionAP.ToString();
                        conect.AnotPesadaO = listFactorAP[11].puntuacionAP.ToString();
                        conect.AnotPesadaQ1 = listFactorAP[12].puntuacionAP.ToString();
                        conect.AnotPesadaQ2 = listFactorAP[13].puntuacionAP.ToString();
                        conect.AnotPesadaQ3 = listFactorAP[14].puntuacionAP.ToString();
                        conect.AnotPesadaQ4 = listFactorAP[15].puntuacionAP.ToString();

                        conect.Perfil1 = factorQ1;
                        conect.Perfil2 = factorQ2;
                        conect.Perfil3 = factorQ3;
                        conect.Perfil4 = factorQ4;
                        conect.Neuroticismo = neuroticismo;
                        conect.Distorsion = distorsionFinal;

                        entities.SaveChangesAsync();

                    }
                }
            }


        }


        private int calcularDistorsión()
        {
            _16pfTest _16pf = new _16pfTest();
            String distorsion = "";
            int valorDistorsion = 0;

            for (int i = 0; i < _16pf.distorsion.Count; i++)
            {
                String llaveA = _16pf.puntuacionItem[_16pf.distorsion[i] - 1].Substring(0, 1);
                String llaveB = _16pf.puntuacionItem[_16pf.distorsion[i] - 1].Substring(1, 1);
                String llaveC = _16pf.puntuacionItem[_16pf.distorsion[i] - 1].Substring(2, 1);
                String res = listPregunta16PF[_16pf.distorsion[i] - 1].valor;

                if (res == "1")
                {
                    valorDistorsion += Convert.ToInt32(llaveA);
                }
                if (res == "2")
                {
                    valorDistorsion += Convert.ToInt32(llaveB);
                }
                if (res == "3")
                {
                    valorDistorsion += Convert.ToInt32(llaveC);
                }

            }


            if (valorDistorsion <= 7)
            {
                distorsion = "Baja";
            }

            if (8 <= valorDistorsion && valorDistorsion <= 9)
            {
                distorsion = "Media";
            }

            if (valorDistorsion >= 10)
            {
                distorsion = "Alta";
            }


            return valorDistorsion;
        }

        private string calcularNeuroticismo(List<FactorAB> listFactorAP)
        {

            int b = listFactorAP[1].puntuacionAP;
            int c = listFactorAP[2].puntuacionAP;
            int h = listFactorAP[6].puntuacionAP;
            int f = listFactorAP[5].puntuacionAP;
            int g = listFactorAP[3].puntuacionAP;
            int m = listFactorAP[10].puntuacionAP;
            int i = listFactorAP[7].puntuacionAP;
            int o = listFactorAP[12].puntuacionAP;

            int q4 = listFactorAP[16].puntuacionAP;
            int q1 = listFactorAP[13].puntuacionAP;


            double a1 = b + c + h + f + g;
            double a2 = m + i + o + q1 + q4;
            double x = a2 - a1 + 6.27;



            return x.ToString();

        }




        private List<FactorAB> calcularFactorAP(List<FactorAB> listFactorAB)
        {
            _16pfTest _16pf = new _16pfTest();

            List<FactorAB> listFactorAP = new List<FactorAB>();


            foreach (var item in listFactorAB)
            {
                int factorAB = item.puntuacionAB;
                String factor = item.factor;
                List<_16PfTablePercentil> factoresAP = new List<_16PfTablePercentil>();

                if (sexo == "M")
                {
                    factoresAP = _16pf.factoresAPM;
                }
                else
                {
                    factoresAP = _16pf.factoresAPF;
                }

                foreach (var item2 in factoresAP[factorAB].factores)
                {
                    bool res = false;
                    String letra = item2.Substring(0, 1);


                    if (letra == "Q")
                    {
                        letra = item2.Substring(0, 2);
                        res = true;
                    }

                    if (letra == factor)
                    {
                        if (res == true)
                        {
                            String resey = item2.Substring(2, item2.Length - 2);
                            item.puntuacionAP = Convert.ToInt32(resey);
                        }
                        else
                        {
                            item.puntuacionAP = Convert.ToInt32(item2.Substring(1, item2.Length - 1));
                            break;
                        }

                    }
                }


            }



            return listFactorAB;
        }
        private string calcularFactorQ4(List<FactorAB> listFactorAP)
        {
            int e = listFactorAP[4].puntuacionAP;
            int q2 = listFactorAP[14].puntuacionAP;
            int q1 = listFactorAP[13].puntuacionAP;
            int m = listFactorAP[10].puntuacionAP;
            int a = listFactorAP[0].puntuacionAP;
            int g = listFactorAP[5].puntuacionAP;

            double a1 = 4 * e + 3 * m + 4 * q2 + 4 * q1;
            double a2 = 3 * a + 2 * g;
            double x = a1 - a2 - 4;

            x = Math.Round(x / 10, 2);

            return x.ToString();
        }

        private string calcularFactorQ3(List<FactorAB> listFactorAP)
        {
            int c = listFactorAP[2].puntuacionAP;
            int e = listFactorAP[4].puntuacionAP;
            int f = listFactorAP[5].puntuacionAP;
            int n = listFactorAP[11].puntuacionAP;
            int a = listFactorAP[0].puntuacionAP;
            int i = listFactorAP[7].puntuacionAP;
            int m = listFactorAP[10].puntuacionAP;

            double a1 = 2 * c + 2 * e + 2 * f + 2 * n;
            double a2 = 4 * a + 6 * i + 2 * m;
            double x = a1 - a2 + 69;
            x = Math.Round(x / 10, 2);

            return x.ToString();
        }

        private string calcularFactorQ2(List<FactorAB> listFactorAP)
        {
            int a = listFactorAP[0].puntuacionAP;
            int e = listFactorAP[4].puntuacionAP;
            int f = listFactorAP[5].puntuacionAP;
            int q2 = listFactorAP[14].puntuacionAP;
            int h = listFactorAP[6].puntuacionAP;


            double a1 = 2 * a + 3 * e + 4 * f + 5 * h;
            double a2 = 2 * q2;
            double x = a1 - a2 - 11;
            x = Math.Round(x / 10, 2);

            return x.ToString();
        }

        private string calcularFactorQ1(List<FactorAB> listFactorAP)
        {
            int l = listFactorAP[8].puntuacionAP;
            int o = listFactorAP[12].puntuacionAP;
            int q4 = listFactorAP[16].puntuacionAP;
            int q3 = listFactorAP[15].puntuacionAP;
            int h = listFactorAP[6].puntuacionAP;
            int c = listFactorAP[2].puntuacionAP;

            double a1 = 2 * l + 3 * o + 4 * q4;
            double a2 = 2 * c + 2 * h + 2 * q3;
            double x = a1 - a2 + 34;
            x = Math.Round(x / 10, 2);

            return x.ToString();
        }
        private List<FactorAB> calcularFactorAB()
        {
            _16pfTest _16pf = new _16pfTest();

            int puntuacionFactorA = 0;
            int puntuacionFactorB = 0;
            int puntuacionFactorC = 0;
            int puntuacionFactorG = 0;
            int puntuacionFactorE = 0;
            int puntuacionFactorF = 0;
            int puntuacionFactorH = 0;
            int puntuacionFactorI = 0;
            int puntuacionFactorL = 0;
            int puntuacionFactorM = 0;
            int puntuacionFactorN = 0;
            int puntuacionFactorO = 0;
            int puntuacionFactorQ1 = 0;
            int puntuacionFactorQ2 = 0;
            int puntuacionFactorQ3 = 0;
            int puntuacionFactorQ4 = 0;

            List<FactorAB> listFactorAB = new List<FactorAB>();


            for (int i = 0; i < _16pf.clave.Count; i++)
            {
                String valor = _16pf.clave[i];
                String letra = valor.Substring(0, 1);
                int itemNum = 0;

                if (letra != "Q")
                {
                    itemNum = Convert.ToInt32(valor.Substring(1, valor.Length - 1));
                }
                else
                {
                    letra = valor.Substring(0, 2);
                    itemNum = Convert.ToInt32(valor.Substring(2, valor.Length - 2));
                }


                String llaveA = _16pf.puntuacionItem[itemNum - 1].Substring(0, 1);
                String llaveB = _16pf.puntuacionItem[itemNum - 1].Substring(1, 1);
                String llaveC = _16pf.puntuacionItem[itemNum - 1].Substring(2, 1);
                String res = listPregunta16PF[itemNum - 1].valor;

                String pregunta = listPregunta16PF[itemNum - 1].Pregunta;


                if (letra == "A")
                {

                    if (res == "1")
                    {
                        puntuacionFactorA += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorA += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorA += Convert.ToInt32(llaveC);
                    }


                }
                if (letra == "B")
                {
                    if (res == "1")
                    {
                        puntuacionFactorB += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorB += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorB += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "C")
                {
                    if (res == "1")
                    {
                        puntuacionFactorC += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorC += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorC += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "E")
                {
                    if (res == "1")
                    {
                        puntuacionFactorE += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorE += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorE += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "F")
                {
                    if (res == "1")
                    {
                        puntuacionFactorF += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorF += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorF += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "G")
                {
                    if (res == "1")
                    {
                        puntuacionFactorG += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorG += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorG += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "H")
                {
                    if (res == "1")
                    {
                        puntuacionFactorH += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorH += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorH += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "I")
                {
                    if (res == "1")
                    {
                        puntuacionFactorI += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorI += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorI += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "L")
                {
                    if (res == "1")
                    {
                        puntuacionFactorL += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorL += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorL += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "M")
                {
                    if (res == "1")
                    {
                        puntuacionFactorM += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorM += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorM += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "N")
                {
                    if (res == "1")
                    {
                        puntuacionFactorN += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorN += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorN += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "O")
                {
                    if (res == "1")
                    {
                        puntuacionFactorO += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorO += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorO += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "Q1")
                {
                    if (res == "1")
                    {
                        puntuacionFactorQ1 += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorQ1 += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorQ1 += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "Q2")
                {
                    if (res == "1")
                    {
                        puntuacionFactorQ2 += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorQ2 += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorQ2 += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "Q3")
                {
                    if (res == "1")
                    {
                        puntuacionFactorQ3 += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorQ3 += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorQ3 += Convert.ToInt32(llaveC);
                    }
                }
                if (letra == "Q4")
                {
                    if (res == "1")
                    {
                        puntuacionFactorQ4 += Convert.ToInt32(llaveA);
                    }

                    if (res == "2")
                    {
                        puntuacionFactorQ4 += Convert.ToInt32(llaveB);
                    }

                    if (res == "3")
                    {
                        puntuacionFactorQ4 += Convert.ToInt32(llaveC);
                    }
                }

            }

            FactorAB factor1 = new FactorAB("A", puntuacionFactorA, 0);
            FactorAB factor2 = new FactorAB("B", puntuacionFactorB, 0);
            FactorAB factor3 = new FactorAB("C", puntuacionFactorC, 0);
            FactorAB factor4 = new FactorAB("G", puntuacionFactorG, 0);
            FactorAB factor5 = new FactorAB("E", puntuacionFactorE, 0);
            FactorAB factor6 = new FactorAB("F", puntuacionFactorF, 0);
            FactorAB factor7 = new FactorAB("H", puntuacionFactorH, 0);
            FactorAB factor8 = new FactorAB("I", puntuacionFactorI, 0);
            FactorAB factor9 = new FactorAB("L", puntuacionFactorL, 0);
            FactorAB factor10 = new FactorAB("E", puntuacionFactorE, 0);
            FactorAB factor11 = new FactorAB("M", puntuacionFactorM, 0);
            FactorAB factor12 = new FactorAB("N", puntuacionFactorN, 0);
            FactorAB factor13 = new FactorAB("O", puntuacionFactorO, 0);
            FactorAB factor14 = new FactorAB("Q1", puntuacionFactorQ1, 0);
            FactorAB factor15 = new FactorAB("Q2", puntuacionFactorQ2, 0);
            FactorAB factor16 = new FactorAB("Q3", puntuacionFactorQ3, 0);
            FactorAB factor17 = new FactorAB("Q4", puntuacionFactorQ4, 0);

            listFactorAB.Add(factor1);
            listFactorAB.Add(factor2);
            listFactorAB.Add(factor3);
            listFactorAB.Add(factor4);
            listFactorAB.Add(factor5);
            listFactorAB.Add(factor6);
            listFactorAB.Add(factor7);
            listFactorAB.Add(factor8);
            listFactorAB.Add(factor9);
            listFactorAB.Add(factor10);
            listFactorAB.Add(factor11);
            listFactorAB.Add(factor12);
            listFactorAB.Add(factor13);
            listFactorAB.Add(factor14);
            listFactorAB.Add(factor15);
            listFactorAB.Add(factor16);
            listFactorAB.Add(factor17);

            return listFactorAB;
        }
    }
}
