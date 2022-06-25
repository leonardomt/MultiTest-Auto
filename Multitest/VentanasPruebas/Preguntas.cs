
using Multitest.AuxClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using Multitest.ADOmodel;

namespace Multitest
{
    public partial class Preguntas : Form
    {
        public String prueba = null;


        List<_16PF> listPregunta16PF;
        List<String> listPreguntas;

        List<AuxPreguntas> listRespuesta;


        String idUser;
        int valor = -1;
        String totalTiempoTest = "";
        int checkMode = 0;
        int countList = 0;
        String tipoIdare;
        String etapa = null;
        int edad;
        Stopwatch time = null;
        String sexo;
        bool updateTest;
        public Preguntas(string tipoIdare, String prueba, bool updateTest, String idUser, String etapa, String sexo, int edad)
        {

            InitializeComponent();
            this.button5.TabStop = false;
            this.tipoIdare = tipoIdare;
            this.updateTest = updateTest;
            this.edad = edad;
            this.sexo = sexo;
            this.etapa = etapa;
            this.idUser = idUser;
            listPregunta16PF = new List<_16PF>();
            time = new Stopwatch();
            richTextBox1.TabStop = false;

            listPreguntas = new List<String>();
            listRespuesta = new List<AuxPreguntas>();
            this.prueba = prueba;

            if (prueba == "Test de Motivos Deportivos de Butt")
                label1.Text = prueba;
            else
                label1.Text = "Test de " + prueba;

            iniciarPrueba();

        }



        private void iniciarPrueba()
        {
            if (prueba == "IDARE (Situacional)")
            {


                label3.Text = "Algunas expresiones que las personas usan para describirse aparecen abajo. Lea cada frase y seleccione el número que indique cómo se siente ahora mismo, o sea, en estos momentos.\r\n" +
                               "No hay contestaciones buenas o malas. No emplee mucho tiempo en cada frase, pero trate de dar la respuesta que mejor describa sus sentimientos ahora.";
                //  label2.Text = "Seleccione una respuesta para esta pregunta tecleando (1,2,3,4) y presionando ENTER después de seleccionarla";


                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;


                label5.Text = "No en lo absoluto";
                label6.Text = "Un poco";
                label7.Text = "Bastante";
                label8.Text = "Mucho";



                llenarPrguntaIdareSituacional();
                nextQuestion();


            }

            if (prueba == "IDARE (Rasgo)")
            {


                label3.Text = "Algunas expresiones que las personas usan para describirse aparecen abajo. Lea cada frase y seleccione el número que indique cómo se siente generalmente. No hay contestaciones buenas o malas. No emplee mucho tiempo en cada frase, pero describa cómo se siente generalmente.";
                // label2.Text = "Seleccione una respuesta para esta pregunta tecleando (1,2,3,4) y presionando ENTER después de seleccionarla";


                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;


                label5.Text = "Casi Nunca";
                label6.Text = "Algunas veces";
                label7.Text = "Frecuentemente ";
                label8.Text = "Casi siempre";



                llenarPrguntaIdareRasgo();
                nextQuestion();


            }

            if (prueba == "Eysenck")
            {


                label3.Text = "A continuación encontrará algunas preguntas que hacen referencia a su manera de proceder, de sentir y de actuar.Lea cada una de las reguntas y decida si aplica a usted mismo, indica su modo de actuar o sentir. Seleccione SI o NO.\r\n" +
                               ". Trabaje rápidamente y no emplee demasiado tiempo en cada pregunta, es preferible su primera reacción, espontánea, y no una contestación largamente meditada y pensada.Normalmente se tarda unos pocos minutos en contestar el cuestionario. Conteste todas las preguntas sin omitir ninguna.\r\n" +
                               "Trabaje rápidamente y recuerde contestar todas las preguntas. No hay respuestas correctas o incorrectas, esta no es una prueba de inteligencia o habilidad, sino simplemente una apreciación de su modo de actuar.\r\n" +
                                "AHORA, COMIENCE.";

                //  label2.Text = "Seleccione una respuesta para esta pregunta tecleando 1 ó 2 y presionando ENTER después de seleccionarla.";


                label8.Visible = true;
                label6.Visible = true;


                label13.Visible = true;
                label11.Visible = true;



                label8.Text = "No";
                label6.Text = "Sí";

                label11.Text = "1.";
                label13.Text = "2.";

                llenarPrguntaEysenck();
                nextQuestion();

            }

            if (prueba == "Catell")
            {
                label3.Text = "A continuación encontrará cuarenta preguntas en relación con las dificultades por las que pasan la mayoría de las personas en algún momento de su vida. Si usted selecciona (sí, no, etc.) con franqueza y sinceridad ayudará a comprender cualquier problema que usted tenga.\r\n" +
                               "Comience con los dos ejemplos que están abajo para practicar. Seleccionando cualquiera de las casillas de abajo, permite conocer si la pregunta está de acuerdo con su modo de ser, o no, en qué forma, etc.";

                // label2.Text = "Seleccione una respuesta para esta pregunta tecleando 1 ó 2 ó 3 y presionando ENTER después de seleccionarla";

                label10.Visible = true;
                label5.Visible = true;
                label12.Visible = true;

                label7.Visible = true;

                label14.Visible = true;
                label9.Visible = true;



                label5.Text = "Sí";

                label9.Text = "No";

                label14.Text = "3.";

                label7.Text = "Dudoso";
                label12.Text = "2.";


                llenaPreguntaCatell();
                nextQuestion();

            }


            if (prueba == "POMS")
            {
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;


                label5.Text = "Nada.";
                label6.Text = "Un poco.";
                label7.Text = "Moderadamente.";
                label8.Text = "Bastante.";
                label9.Text = "Muchísimo.";

                label3.Text = "A continnuación se presentarán un conjunto de palabras que describen sensaciones que tiene la gente. Por favor lea cada una" +
                    "cuidadosamente. Después seleccione uno de los números el que mejor describa COMO TE HAS SENTIDO DURANTE LA SEMANA PASADA INCLUYENDO EL DÍA DE HOY.";

                llenarPreguntaPoms();
                nextQuestion();
            }

            if (prueba == "Actitud ante la Competencia")
            {
                label3.Text = "Imagínese de la  forma  más  clara  y  lógica  posible  la   próxima competencia  y responda a cada una de las opiniones que se relacionan más abajo. Si está de acuerdo con ellas responda \"SI\". En caso de pensar de otra forma, responda \"NO\". No debe pensar mucho tiempo en cada pregunta";
                //  label2.Text = "Seleccione una respuesta para esta pregunta tecleando 1 ó 2 y presionando ENTER después de seleccionarla.";


                label8.Visible = true;
                label6.Visible = true;


                label13.Visible = true;
                label11.Visible = true;


                label8.Text = "No";
                label6.Text = "Sí";

                label11.Text = "1.";
                label13.Text = "2.";


                llenarPreguntaActiAnteCompet();
                nextQuestion();
            }


            if (prueba == "Cualidades Motivacionales Deportivas")
            {
                label3.Text = "La motivación del deportista no solo debe ser intensa, sino exhibir determinados acentos que la distingan.  El cuestionario que nos ocupa persigue explorarlos con deportistas como tú.De antemano te agradecemos tu colaboración. " +
                               "A continuación, te presentamos una breve narración sobre aspectos de la vida de un deportista imaginario.Léela detenidamente y trata de comprender su situación como si fuera propia. \r\n \r\n" +
                               "“Un deportista de excelentes condiciones y prestigio enfrenta las consecuencias de una lesión que amenaza seriamente su destacada carrera deportiva. Durante más de un año se ve alejado de las competencias y, por lo complejo de su especialidad, muchos empiezan a dudar de su regreso exitoso." +
                                "Con enormes sacrificios y gran voluntad supera las limitaciones físicas y las barreras subjetivas que le imponen quienes perdieron la confianza en él." +
                                "Poco a poco comienza a rescatar el dominio técnico y, por fin, ante una competencia internacional de gran importancia, se siente en condiciones de reeditar sus mejores rendimientos.”\r\n" +
                                "A continuación aparecen posibles reacciones o conductas que adoptará ese deportista en la importante competencia. En cada selecciona “SI” o “NO” teniendo en cuenta la situación escrita en la narración" + " No dejes de responder ningún Ítem.";

                //label2.Text = "Seleccione una respuesta para esta pregunta tecleando 1 ó 2 y presionando ENTER después de seleccionarla.";


                label8.Visible = true;
                label6.Visible = true;


                label13.Visible = true;
                label11.Visible = true;


                label8.Text = "No";
                label6.Text = "Sí";

                label11.Text = "1.";
                label13.Text = "2.";

                llenarPruebaCualiMotivDepor();

                nextQuestion();
            }

            if (prueba == "Ansiedad Precompetitiva CSAI-2R")
            {


                label3.Text = "Las declaraciones que se presentan a continuación describen sus posibles sentimientos antes de una competencia. Lea cada una de las afirmaciones y marque el número correspondiente a su respuesta para indicar cómo se siente ahora, en este momento.No hay respuestas correctas o incorrectas y trate de no dedicar tanto tiempo para responderlas.";
                // label2.Text = "Seleccione una respuesta para esta pregunta tecleando (1,2,3,4) y presionando ENTER después de seleccionarla";


                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;


                label5.Text = "Nada";
                label6.Text = "Un poco";
                label7.Text = "Moderadamente";
                label8.Text = "Mucho";



                llenarPreguntaAnsiedadMartens();
                nextQuestion();


            }

            if (prueba == "IDETEM-1")
            {
                label3.Text = "Para conocer su temperamento debe analizar el nivel de correspondencia de cada actitud reflejada en el test con la forma en que habitualmente " +
                    "usted se manifiesta dándole un valor de: " + "\n" +
                     "5 puntos si siempre actúa de esa manera" + "\n" +
                     "4 puntos si casi siempre" + "\n" +
                     "3 puntos si es a veces" + "\n" +
                     "2 puntos si pocas veces" + "\n" +
                     "1 punto si nunca es así" + "\n" +
                "No se detenga mucho tiempo para dar la respuesta. El éxito del resultado depende de la sinceridad con que usted responda.";

                //   label2.Text = "Seleccione una respuesta para esta pregunta tecleando 1 ó 2 y presionando ENTER después de seleccionarla.";


                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;


                label5.Text = "Nunca es así.";
                label6.Text = "Pocas veces.";
                label7.Text = "A veces.";
                label8.Text = "Casi siempre.";
                label9.Text = "Siempre.";

                llenarPruebaIDETEM();
                nextQuestion();
            }

            if (prueba == "Inventario Psicológico de Ejecución Deportiva (IPED)")
            {
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;


                label10.Text = "5.";
                label11.Text = "4.";
                label12.Text = "3.";
                label13.Text = "2.";
                label14.Text = "1.";


                label5.Text = "Casi siempre.";
                label6.Text = "  ";
                label7.Text = "  ";
                label8.Text = "  ";
                label9.Text = "Casi nunca.";

                label3.Text = "A continuación encontrarás una serie de afirmaciones referidas a tus pensamientos, sentimientos, actitudes o comportamientos durante los entrenamientos y/ o competiciones. Nos gustaría conocer hasta qué punto te sientes identificado con estas afirmaciones. No existen respuestas correctas o incorrectas, malas o buenas, verdaderas o falsas, únicamente deseamos conocer tu opinión a este respecto. Lee atentamente cada frase y decide la frecuencia con la que crees que se produce cada una de ellas. Selecciona la respuesta que más se aproxime a tus preferencias. No emplees mucho tiempo en cada respuesta";

                //   label2.Text = "Seleccione una respuesta para esta pregunta presionando 1,2,3,4,5 y depués de seleccionar presione Enter.";

                llenarPregunIPED();
                nextQuestion();
            }


            if (prueba == "Test de Motivos Deportivos de Butt")
            {
                label3.Text = "Contesta todas las preguntas marcando sí o no. Si la pregunta no se le puede aplicar a su deporte responda NO ya que Ud. no ha tenido la sensación. Si cree que la mejor respuesta es" +
                               " algunas veces marque Sí.";


                //      label2.Text = "Seleccione una respuesta para esta pregunta tecleando 1 ó 2 y presionando ENTER después de seleccionarla.";
                tableLayoutPanel17.Visible = true;

                label8.Visible = true;
                label6.Visible = true;


                label13.Visible = true;
                label11.Visible = true;


                label8.Text = "No";
                label6.Text = "Sí";

                label11.Text = "1.";
                label13.Text = "2.";

                llenarPruebaButt();
                nextQuestion();
            }

            if (prueba == "Cualidades Volitivas en el Deporte")
            {

                //       
                label3.Text = "A continuación encontrarás proposiciones que hacen referencia a la manera de pensar o actuar en los entrenamientos o competencias. Te pedimos que respondas a ellas considerando tus experiencias más recientes. Tu respuesta no será juzgada como buena o mala, se emplearán para conocer mejor tus características y así poder ayudarte. Seleccione el número que corresponde a la respuesta deseada.  Siempre: 4, casi siempre: 3, algunas veces: 2, casi nunca: 1 , nunca: 0 ";


                //label2.Text = "Seleccione una respuesta para esta pregunta tecleando (1,2,3,4) y presionando ENTER después de seleccionarla";


                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;

                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;


                label10.Text = "4";
                label11.Text = "3";
                label12.Text = "2";
                label13.Text = "1";
                label14.Text = "0";


                label5.Text = "Siempre";
                label6.Text = "Casi siempre";
                label7.Text = "Algunas veces";
                label8.Text = "Casi nunca";
                label9.Text = "Nunca";



                llenarPreguntaCualidadVolitiva();
                nextQuestion();


            }

        }

        private void llenarPregunIPED()
        {
            listPreguntas.Add("1.Me veo más como un perdedor que como un ganador durante las competiciones.");
            listPreguntas.Add("2.Me enfado y frustro durante la competición.");
            listPreguntas.Add("3.Llego a distraerme y perder mi concentración durante la competición.");
            listPreguntas.Add("4.Antes de la competición, me imagino a mí mismo ejecutando mis acciones y rindiendo perfectamente.");
            listPreguntas.Add("5.Estoy muy motivado para dar lo mejor de mí en la competición.");
            listPreguntas.Add("6.Puedo mantener emociones positivas durante la competición.");
            listPreguntas.Add("7.Durante la competición pienso positivamente.");
            listPreguntas.Add("8.Creo en mí mismo como deportista.");
            listPreguntas.Add("9.Me pongo nervioso durante la competición.");
            listPreguntas.Add("10.En los momentos críticos de la competición  me da la impresión de que mi cabeza va muy deprisa.");
            listPreguntas.Add("11.Practico mentalmente mis habilidades físicas.");
            listPreguntas.Add("12.Trabajo y entreno duro gracias a los objetivos que yo me he fijado como deportista.");
            listPreguntas.Add("13.Disfruto durante la competición, aunque me encuentre con la presencia de dificultades.");
            listPreguntas.Add("14.Durante la competición mantengo autoconversaciones de carácter negativo.");
            listPreguntas.Add("15.Pierdo mi confianza fácilmente.");
            listPreguntas.Add("16.Los errores durante la competición me hacen sentir y pensar negativamente.");
            listPreguntas.Add("17.Puedo controlar rápidamente mis emociones y recuperar la concentración.");
            listPreguntas.Add("18.Para mí es fácil pensar fotográficamente(en imágenes) acerca de mi deporte.");
            listPreguntas.Add("19.No necesito que me empujen a entrenar duro y competir con intensidad.Yo soy mi mejor elemento de motivación.");
            listPreguntas.Add("20.Cuando las cosas se vuelven contra mí durante la competición, tiendo a desinflarme emocionalmente.");
            listPreguntas.Add("21.Empleo todo mi esfuerzo durante la competición, pase lo que pase.");
            listPreguntas.Add("22.Puedo rendir por encima de mi talento y mis habilidades.");
            listPreguntas.Add("23.Durante la competición siento que mis músculos se tensan y creo que no me van a responder.");
            listPreguntas.Add("24.Me tomo respiros durante la competición.");
            listPreguntas.Add("25.Antes de la competición, me visualizo superando situaciones difíciles y ejecutando acciones complejas.");
            listPreguntas.Add("26.Daría lo que fuera por desarrollar todo mi potencial y alcanzar la cumbre como deportista.");
            listPreguntas.Add("27.Entreno con una intensidad alta y positiva.");
            listPreguntas.Add("28.Controlando mi pensamiento soy capaz de transformar estados de humor negativos en positivos.");
            listPreguntas.Add("29.Soy un competidor mentalmente tenaz.");
            listPreguntas.Add("30.Cuando compito, las situaciones incontrolables, como el viento, las trampas de los contrarios, los malos arbitrajes, me alteran y hacen que me derrumbe.");
            listPreguntas.Add("31.Durante la competición pienso en errores pasados o en oportunidades perdidas.");
            listPreguntas.Add("32.Durante la competición utilizo imágenes que me ayudan a rendir mejor.");
            listPreguntas.Add("33.Estoy aburrido.");
            listPreguntas.Add("34.Las situaciones difíciles para mí suponen un desafío y me inspiran.");
            listPreguntas.Add("35.Mi entrenador diría de mí que tengo una buena actitud.");
            listPreguntas.Add("36.La imagen que proyecto al exterior es de ser un luchador.");
            listPreguntas.Add("37.Puedo permanecer tranquilo durante la competición pese a que aparezcan problemas perturbadores.");
            listPreguntas.Add("38.Mi concentración se rompe fácilmente");
            listPreguntas.Add("39.Cuando me visualizo compitiendo o entrenando, puedo ver y sentir las cosas muy vivamente.");
            listPreguntas.Add("40.Al despertar por las mañanas me siento excitado en relación con los entrenamientos y competiciones.");
            listPreguntas.Add("41.Practicar este deporte me aporta un sentido genuino de disfrute y realización.");
            listPreguntas.Add("42.Yo puedo convertir una crisis en una oportunidad.");

        }

        private void llenarPruebaIDETEM()
        {
            listPreguntas.Add("1 - Sus movimientos habituales son ágiles y precisos. ");
            listPreguntas.Add("2 - Se ofende con facilidad.  ");
            listPreguntas.Add("3 - Es lento y minucioso en sus análisis.");
            listPreguntas.Add("4 - Ante el fracaso, se desconsuela.");
            listPreguntas.Add("5 - Su estado de ánimo habitualmente es vivo.");
            listPreguntas.Add("6 - Es inestable en sus propósitos.");
            listPreguntas.Add("7 - Le inquieta dejar una tarea inconclusa.");
            listPreguntas.Add("8 - Suele sentir nostalgia ante los cambios de tiempo.");
            listPreguntas.Add("9 - Capta rápidamente lo nuevo.");
            listPreguntas.Add("10 - Se impacienta con facilidad.");
            listPreguntas.Add("11 - Es metódico y ordenado.");
            listPreguntas.Add("12 - Es muy sensible.");
            listPreguntas.Add("13 - Se caracteriza por hablar claro y rápido.");
            listPreguntas.Add("14 - Frecuentemente es brusco al tratar a las personas.");
            listPreguntas.Add("15 - Se mantiene ecuánime ante situaciones desagradables imprevistas.");
            listPreguntas.Add("16 - Se siente mal cuando está en un ambiente desconocido.");
            listPreguntas.Add("17 - Escucha con paciencia.");
            listPreguntas.Add("18 - Suele tomar decisiones precipitadamente.");
            listPreguntas.Add("19 - Los reveses no afectan su actividad.");
            listPreguntas.Add("20 - Muchas veces se muestra vacilante, inseguro al tomar decisiones.");
            listPreguntas.Add("21 - Establece amistades con facilidad.");
            listPreguntas.Add("22 - Cuando algo le molesta se muestra agresivo.");
            listPreguntas.Add("23 - Generalmente se mantiene paciente, aún cuando tiene motivos para desesperarse.");
            listPreguntas.Add("24 - Frecuentemente manifiesta estados depresivos.");
            listPreguntas.Add("25 - Posee una alta capacidad para asimilar el estrés.");
            listPreguntas.Add("26 - Una actividad extremadamente intensa, fuerte o prolongada, le produce una disminución brusca de su resistencia para mantenerse en ella.");
            listPreguntas.Add("27 - Posee un gran poder de concentración.");
            listPreguntas.Add("28 - Prefiere la soledad.");


            listPreguntas.Add("29 - Se adapta con facilidad a las nuevas situaciones.");
            listPreguntas.Add("30 - Es notablemente efusivo, vehemente, expresivo.");
            listPreguntas.Add("31 - Puede realizar una tarea intelectual prolongadamente sin que se fatigue psíquicamente.");
            listPreguntas.Add("32 - Le indispone el alto ritmo de trabajo.");
            listPreguntas.Add("33 - Es calculadamente decidido en sus actos.");
            listPreguntas.Add("34 - Frecuentemente es impulsivo, irrefrenable e impetuoso.");
            listPreguntas.Add("35 - Demora en fijar los nuevos conocimientos y desarrollar habilidades complejas.");
            listPreguntas.Add("36 - Es susceptible. Fácilmente lastiman sus sentimientos.");
            listPreguntas.Add("37 - Reacciona con rapidez y serenidad ante estímulos repentinos en situaciones de tensión.");
            listPreguntas.Add("38 - Generalmente, en situaciones decisivas, suele perder el control de la actividad que realiza en ese momento.");
            listPreguntas.Add("39 - Su actividad general es lenta.");
            listPreguntas.Add("40 - Las fuertes tensiones disminuyen el nivel de su actividad.");

            listPreguntas.Add("41 - Muestra iniciativa con facilidad.");
            listPreguntas.Add("42 - Usted es muy inquieto.");
            listPreguntas.Add("43 - Posee un alto control emocional.");
            listPreguntas.Add("44 - Los estímulos fuertes le provocan una gran inhibición de su actividad.");
            listPreguntas.Add("45 - Se caracteriza por tener un buen sentido del humor.");
            listPreguntas.Add("46 - Generalmente cambia con facilidad el foco de su atención.");
            listPreguntas.Add("47 - Se abstrae fácilmente en sus pensamientos.");
            listPreguntas.Add("48 - Es muy impresionable, por eso evitas las situaciones de riesgo o peligro.");
            listPreguntas.Add("49 - Se sobrepone fácilmente a los problemas.");
            listPreguntas.Add("50 - Le es difícil controlar sus desajustes emocionales.");
            listPreguntas.Add("51 - Su lenguaje es pausado y poco expresivo.");
            listPreguntas.Add("52 - Es retraído al iniciar relaciones humanas.");
            listPreguntas.Add("53 - Posee gran agilidad mental.");
            listPreguntas.Add("54 - Generalmente es rígido en su análisis.");
            listPreguntas.Add("55 - Se caracteriza por su gran perseverancia.");
            listPreguntas.Add("56 - Se le dificulta la adaptación a situaciones cambiantes.");


        }

        private void llenarPreguntaAnsiedadMartens()
        {
            listPreguntas.Add("1- Me siento nervioso.");
            listPreguntas.Add("2- Estoy preocupado de no rendir tan bien en esta competencia como podría hacerlo.");
            listPreguntas.Add("3- Confío en mi.");
            listPreguntas.Add("4- Siento mi cuerpo tenso.");
            listPreguntas.Add("5- Me preocupa perder.");
            listPreguntas.Add("6- Siento mi estómago tenso.");
            listPreguntas.Add("7- Estoy seguro de lograr mi objetivo.");
            listPreguntas.Add("8- Me preocupa llegar a bloquearme cuando estoy bajo presión. ");
            listPreguntas.Add("9- Mi corazón esta acelerado.");
            listPreguntas.Add("10- Estoy confiado en que tendré un buen desempeño.");
            listPreguntas.Add("11- Me preocupa tener un bajo rendimiento.");
            listPreguntas.Add("12- Siento un nudo en el estómago");
            listPreguntas.Add("13- Estoy confiado porque me visualizo alcanzando mi objetivo.");
            listPreguntas.Add("14- Me preocupa que los demás se decepcionen de mi rendimiento.");
            listPreguntas.Add("15- Mis manos estan sudando.");
            listPreguntas.Add("16- Estoy seguro de tener un buen rendimiento bajo presión.");
            listPreguntas.Add("17- Siento mi cuerpo apretado/rígido.");

        }


        private void llenarPreguntaCualidadVolitiva()
        {
            listPreguntas.Add("1- Mientras compito me resulta fácil analizar mi propia actuación y realizar los ajustes necesarios sin ayuda de otros.");
            listPreguntas.Add("2- En situaciones muy importantes suelo decidir sin pensar demasiado.");
            listPreguntas.Add("3- Algunos impedimentos externos hacen que no pueda cumplir con los propósitos que se me exige en los entrenamientos.");
            listPreguntas.Add("4- Cuando estoy en medio de un entrenamiento rutinario, me distraigo con facilidad con cualquier otra cosa.");
            listPreguntas.Add("5- Me molesto cuando las cosas no me salen como quiero.");
            listPreguntas.Add("6- Me resulta fácil idear y efectuar tareas nuevas fuera del entrenamiento para contribuir con la preparación que planifica mi entrenador.");
            listPreguntas.Add("7- Pudiera tomar mejores decisiones sin embargo prefiero actuar con rapidez y sin pensar demasiado tiempo en los riesgos.");
            listPreguntas.Add("8- Solo si existen razones que lo justifican puedo aceptar que obtener el éxito en la competencia es casi imposible.");
            listPreguntas.Add("9- Cuando amanezco con pocos ánimos o desmotivado las cosas no me salen bien, aunque haga un esfuerzo autoanimándome.");
            listPreguntas.Add("10- Mis habilidades técnicas las puedo realizar siempre sin equivocarme en las competencias.  ");
            listPreguntas.Add("11- En una situación decisiva me resultaría cómodo idear y asumir la responsabilidad de una estrategia competitiva novedosa.");
            listPreguntas.Add("12- En situaciones competitivas inesperadas me arriesgo a actuar por intuición .");
            listPreguntas.Add("13- Cuando las cosas no van saliendo bien en los entrenamientos es preferible parar y hacer otras,  que insistir hasta que salga.");
            listPreguntas.Add("14- Durante las competencias las situaciones que aparecen me obligan a alejarme de las orientaciones del plan táctico que me ofrece mi entrenador.");
            listPreguntas.Add("15- He tenido algunos fracasos porque no me he esforzado lo suficiente.");
            listPreguntas.Add("16- Me mueve más el entusiasmo que me puede provocar el grupo hacia una actividad,  que mis propios sentimientos y sensaciones.");
            listPreguntas.Add("17- Me caracteriza la rapidez en situaciones competitivas donde a otros les toma más tiempo decidir.");

            listPreguntas.Add("18- Cuando el éxito de la tarea depende más de factores que no tienen que ver conmigo no vale la pena esforzarme.");
            listPreguntas.Add("19- Me demoro en retomar las tareas que constituyen mis responsabilidades cuando estoy disfrutando de una actividad mucho más agradable para mí.  ");
            listPreguntas.Add("20- Amanezco con pocas ganas de ir a los entrenamientos.");
            listPreguntas.Add("21- Las valoraciones acerca de mis posibilidades de ganar una competencia se basan más en mis opiniones que en las de mi entrenador.");
            listPreguntas.Add("22- Analizo todas las variantes de solución a los problemas que se me presentan en la competencia,  antes de decidir qué hacer.");
            listPreguntas.Add("23- Cuando no logro obtener mi propósito en la competencia es por una razón justificada (al menos para mí).");
            listPreguntas.Add("24- Cuando tengo otras cuestiones importantes pendientes necesito atenderlas aunque me encuentre entrenando, pues no me permiten concentrarme en las tareas.");
            listPreguntas.Add("25- La mayoría de mis actos coinciden más con mi forma de pensar que con lo que otros esperan de mí.");
            listPreguntas.Add("26- En situaciones definitorias optaría por evitar decidir, antes que tomar decisiones donde un posible error pueda comprometer el resultado final.");

            listPreguntas.Add("27- El entrenador piensa que soy un atleta que termina cansado en los entrenamientos porque tiendo a trabajar duro hasta el final.");
            listPreguntas.Add("28- Los estímulos distractores a mi alrededor afectan el aprovechamiento de mi entrenamiento.");



        }

        private void llenarPreguntaActiAnteCompet()
        {
            listPreguntas.Add("1- Estoy listo(a) para demostrar un alto rendimiento.");
            listPreguntas.Add("2- Estoy mejor preparado para esta competencia que mis contrarios.");
            listPreguntas.Add("3- Deseo demostrar altos resultados en esta competencia.");
            listPreguntas.Add("4- Temo hacer quedar mal al equipo.");
            listPreguntas.Add("5- Estoy físicamente bien preparado para esta competencia.");
            listPreguntas.Add("6- En la competencia habrá muchos y diversos contrarios.");
            listPreguntas.Add("7- Esta es una competencia muy importante para mí.");
            listPreguntas.Add("8- En este momento mis relaciones con el entrenador son tensas. ");
            listPreguntas.Add("9- Estoy en buena forma deportiva.");
            listPreguntas.Add("10- Conozco mal a mis contrarios.");
            listPreguntas.Add("11- Esta es una competencia muy decisiva para mí.");
            listPreguntas.Add("12- Los conflictos con mis compañeros de equipo me molestan para prepararme debidamente en esta competencia.");
            listPreguntas.Add("13- Estoy seguro de que puedo ejecutar las tareas que se me han encomendado en esta competencia.");
            listPreguntas.Add("14- Le temo a mis contrarios.");
            listPreguntas.Add("15- Pienso que esta es una competencia difícil.");
            listPreguntas.Add("16- Mi exitosa actuación en la competencia es importante para todo el equipo");
            listPreguntas.Add("17- Estoy contento con los resultados de mi última competencia.");
            listPreguntas.Add("18- En la próxima competencia tendré contrarios difíciles de vencer.");
            listPreguntas.Add("19- Me es importante actuar bien en esta competencia.");
            listPreguntas.Add("20- Me parece que mis compañeros de equipo no creen en mis posibilidades de tener éxito.");
            listPreguntas.Add("21- Estoy seguro(a) de mis fuerzas.");
            listPreguntas.Add("22- Yo le gané a mis contrarios anteriormente.");
            listPreguntas.Add("23- Pienso constantemente en la próxima competencia.");
            listPreguntas.Add("24- En esta competencia temo defraudar a mis entrenadores.");
            listPreguntas.Add("25- Estoy bien preparado técnicamente para la próxima competencia.");
            listPreguntas.Add("26- Entre mis contrarios hay algunos de los que no sé nada.");
            listPreguntas.Add("27- Espero con impaciencia la próxima competencia.");
            listPreguntas.Add("28- El entrenador valora altamente mi preparación para esta competencia.");


        }

        private void llenarPreguntaPoms()
        {
            listPreguntas.Add("1.Amistoso.");
            listPreguntas.Add("2.Tenso.");
            listPreguntas.Add("3.Disgustado.");
            listPreguntas.Add("4.Cansado.");
            listPreguntas.Add("5.Infeliz.");
            listPreguntas.Add("6.Franco.");
            listPreguntas.Add("7.Animado.");
            listPreguntas.Add("8.Confundido.");
            listPreguntas.Add("9.Aflijido.");
            listPreguntas.Add("10.Agitado.");
            listPreguntas.Add("11.Apático.");
            listPreguntas.Add("12.Enojado.");
            listPreguntas.Add("13.Considerado con otros.");
            listPreguntas.Add("14.Triste.");
            listPreguntas.Add("15.Activo.");
            listPreguntas.Add("16.A punto de estallar.");
            listPreguntas.Add("17.Irritable.");
            listPreguntas.Add("18.Abatido.");
            listPreguntas.Add("19.Enérgico.");
            listPreguntas.Add("20.Asustado.");
            listPreguntas.Add("21.Deseperanzado.");
            listPreguntas.Add("22.Relajado.");
            listPreguntas.Add("23.Torpe.");
            listPreguntas.Add("24.Rencoroso.");
            listPreguntas.Add("25.Cariñoso.");
            listPreguntas.Add("26.Intranquilo.");
            listPreguntas.Add("27.Inquieto.");
            listPreguntas.Add("28.Incapaz de concentrarme.");
            listPreguntas.Add("29.Fatigado.");
            listPreguntas.Add("30.Cooperador.");
            listPreguntas.Add("31.Molesto.");
            listPreguntas.Add("32.Desanimado.");
            listPreguntas.Add("33.Resentido.");
            listPreguntas.Add("34.Nervioso.");
            listPreguntas.Add("35.Solo.");
            listPreguntas.Add("36.Desdichado.");
            listPreguntas.Add("37.Aturdido.");
            listPreguntas.Add("38.Alegre.");
            listPreguntas.Add("39.Amargo.");
            listPreguntas.Add("40.Desfallecido.");
            listPreguntas.Add("41.Ansioso.");
            listPreguntas.Add("42.Inclinado a reñir.");
            listPreguntas.Add("43.Bondadoso.");
            listPreguntas.Add("44.Deprimido.");
            listPreguntas.Add("45.Desesperado.");
            listPreguntas.Add("46.Haragán.");
            listPreguntas.Add("47.Rebelde.");
            listPreguntas.Add("48.Desamparado.");
            listPreguntas.Add("49.Aburrido.");
            listPreguntas.Add("50.Desorientado.");
            listPreguntas.Add("51.Alerta.");
            listPreguntas.Add("52.Decepcionado.");
            listPreguntas.Add("53.Fusioso.");
            listPreguntas.Add("54.Eficiente.");
            listPreguntas.Add("55.Confiado.");
            listPreguntas.Add("56.Llena de energía.");
            listPreguntas.Add("57.De mal genio.");
            listPreguntas.Add("58.Inútil.");
            listPreguntas.Add("59.Olvidadizo.");
            listPreguntas.Add("60.Despreocupado.");
            listPreguntas.Add("61.Aterrorizado.");
            listPreguntas.Add("62.Culpable.");
            listPreguntas.Add("63.Vigoroso.");
            listPreguntas.Add("64.Ineguro.");
            listPreguntas.Add("65.Agotado.");



        }

        private void llenarPrguntaIdareRasgo()
        {
            listPreguntas.Add("1.Me siento bien.");
            listPreguntas.Add("2.Me siento cansado rápidamente.");
            listPreguntas.Add("3.Siento ganas de llorar.");
            listPreguntas.Add("4.Quisiera ser tan feliz como otros perecen serlo.");
            listPreguntas.Add("5.Pierdo oportunidades por no poder decidirme rápidamente.");
            listPreguntas.Add("6.Me siento descansado.");
            listPreguntas.Add("7.Soy una persona \"tranquila\",\"serena\",\"sosegada\".");
            listPreguntas.Add("8.Siento que las dificultades se me amontonan al punto de no poder superarlas.");
            listPreguntas.Add("9.Me preocupo demasiado por cosas sin importancia.");
            listPreguntas.Add("10.Soy feliz.");
            listPreguntas.Add("11.Tomo las cosas muy a pecho.");
            listPreguntas.Add("12.Me falta confianza en mí mismo.");
            listPreguntas.Add("13.Me siento seguro.");
            listPreguntas.Add("14.Trato de sacarle el cuerpo a las crisis y dificultades.");
            listPreguntas.Add("15.Me siento melancólico.");
            listPreguntas.Add("16.Me siento satisfecho.");
            listPreguntas.Add("17.Algunas ideas poco importantes pasan por mi mente y me molestan.");
            listPreguntas.Add("18.Me afectan tanto los desengaños que no me los puedo quitar de la cabeza.");
            listPreguntas.Add("19.Soy una persona estable.");
            listPreguntas.Add("20.Cuando pienso en los asuntos que tengo entre manos me pongo tenso y alterado.");

        }

        private void llenarPrguntaIdareSituacional()
        {
            listPreguntas.Add("1.Me siento calmado.");
            listPreguntas.Add("2.Me siento seguro.");
            listPreguntas.Add("3.Estoy tenso.");
            listPreguntas.Add("4.Estoy contrariado.");
            listPreguntas.Add("5.Estoy a gusto.");
            listPreguntas.Add("6.Me siento alterado.");
            listPreguntas.Add("7.Estoy preocupado actualmente por algún posible contratiempo.");
            listPreguntas.Add("8.Me siento descansado.");
            listPreguntas.Add("9.Me siento ansioso.");
            listPreguntas.Add("10.Me siento cómodo.");
            listPreguntas.Add("11.Me siento con confianza en mí mismo.");
            listPreguntas.Add("12.Me siento nervioso.");
            listPreguntas.Add("13.Me siento agitado.");
            listPreguntas.Add("14.Me siento \"a punto de explotar\".");
            listPreguntas.Add("15.Me siento reposado.");
            listPreguntas.Add("16.Me siento satisfecho.");
            listPreguntas.Add("17.Estoy preocupado.");
            listPreguntas.Add("18.Me siento muy excitado y aturdido.");
            listPreguntas.Add("19.Me siento alegre.");
            listPreguntas.Add("20.Me siento bien.");



        }
        private void llenarPrguntaEysenck()
        {
            listPreguntas.Add("1.¿Le gusta mucho salir?");
            listPreguntas.Add("2.¿Se siente unas veces rebosante de energía y decaído otras?");
            listPreguntas.Add("3.¿Se queda usted apartado  o aislado de los demás en las fiestas o reuniones?");
            listPreguntas.Add("4.¿Necesita a menudo amistades comprensivas que lo animen?");
            listPreguntas.Add("5.¿Le agradan las tareas en que debe trabajar aislado?");
            listPreguntas.Add("6.¿Habla algunas veces sobre cosas que desconoce completamente?");
            listPreguntas.Add("7.¿Se preocupa a menudo por las cosas que no debería haber hecho o dicho?");
            listPreguntas.Add("8.¿Le agrada a usted las bromas entre amigos?");
            listPreguntas.Add("9.¿Se preocupa usted mucho tiempo después de haber sufrido una experiencia desagradable?");
            listPreguntas.Add("10.¿Es usted activo y emprendedor?");
            listPreguntas.Add("11.¿Se despierta varias veces en la noche?");
            listPreguntas.Add("12.¿Ha hecho alguna vez algo de lo que tenga que avergonzarse?");
            listPreguntas.Add("13.¿Se siente molesto cuando no se viste como los demás?");
            listPreguntas.Add("14.¿Piensa usted con frecuencia en su pasado?");
            listPreguntas.Add("15.¿Se detiene muy a menudo a meditar sus pensamientos y sentimientos?");
            listPreguntas.Add("16.Cuando está disgustado ¿necesita algún amigo para contárselo?");
            listPreguntas.Add("17.Generalmente, Ud. ¿Puede usted \"soltarse\" y divertirse mucho en una fiesta alegre?");
            listPreguntas.Add("18.Si al hacer una compra le despacharan de más por equivocación, ¿lo devolvería aunque supiera que nadie podría descubrirlo?");
            listPreguntas.Add("19.¿Se siente usted a menudo cansado e indiferente sin ninguna razón para ello?");
            listPreguntas.Add("20.¿Acostumbra usted a decir la primera cosa que se le ocurrre?");
            listPreguntas.Add("21.¿Se siente de pronto tímido cuando desea hablar a una persona atractiva que le es desconocida?");
            listPreguntas.Add("22.¿Prefiere usted planear las cosas mejor que hacerlas?");
            listPreguntas.Add("23.¿Siente palpitaciones o latidos en el corazón?");
            listPreguntas.Add("24.¿Son todos sus hábitos buenos y deseables?");
            listPreguntas.Add("25.Cuando se ve envuelto en una discusión,¿prefiere \"llevarla hasta el final\" antes de permanecer callado,esperando que de alguna forma se calme?");
            listPreguntas.Add("26.¿Se considera usted una persona nerviosa?");
            listPreguntas.Add("27.¿Le gusta conversar con personas que no conoce y que encuentra casualmente?");
            listPreguntas.Add("28.¿Ocurre con frecuecia que toma usted sus decisiones demasiado tarde?");
            listPreguntas.Add("29.¿Se siente seguro de sí cuando tiene que hablar en público?");
            listPreguntas.Add("30.¿Chismea algunas veces?");
            listPreguntas.Add("31.¿Ha perdido usted a menudo horas de sueño, a causa de sus preocupaciones?");
            listPreguntas.Add("32.¿Es usted vivaracho?");
            listPreguntas.Add("33.¿Está usted con frecuencia \"en la luna\"?");
            listPreguntas.Add("34.Cuando hace nuevas amistades ¿es normalmente usted quien da el primer paso o el primero que invita?");
            listPreguntas.Add("35.¿Se siente molesto o preocupado con frecuencia por sentimientos de culpabilidad?");
            listPreguntas.Add("36.¿Es usted una persona que nunca está de mal humor?");
            listPreguntas.Add("37.¿Se llamaría a sí mismo una persona afortunada?");
            listPreguntas.Add("38.¿Se preocupa por cosas terribles que pudieran sucederle?");
            listPreguntas.Add("39.¿Prefiere quedarse en casa a asistir a una fiesta o reunión aburrida?");
            listPreguntas.Add("40.¿Se mete usted en líos con frecuencia por hacer las cosas sin pensar?");
            listPreguntas.Add("41.¿Su osadía lo llevaría a hacer casi cualquier cosa?");
            listPreguntas.Add("42.¿Ha llegado tarde a una cita o al trabajo?");
            listPreguntas.Add("43.¿Es usted una persona irritable?");
            listPreguntas.Add("44.¿Por lo general hace y dice las cosas rápidamente, sin detenerse a pensar?");
            listPreguntas.Add("45.¿Se siente usted alguna veces triste y otras alegres, sin motivo aparente?");
            listPreguntas.Add("46.¿Le gusta a usted hacer bromas a otras personas?");
            listPreguntas.Add("47.¿Cuando se despierta por las mañanas se siente agotado?");
            listPreguntas.Add("48.¿Ha sentido usted en alguna ocasión deseos de no asistir al trabajo?");
            listPreguntas.Add("49.¿Se sentiría mal si no estuviera rodeado de otras personas la mayor parte del tiempo?");
            listPreguntas.Add("50.¿Le cuesta trabajo conciliar el sueño por la noche?");
            listPreguntas.Add("51.¿Le gusta trabajar solo?");
            listPreguntas.Add("52.¿Le dan ataques de temblores o estremecimientos?");
            listPreguntas.Add("53.¿Le agrada mucho bullicio y agitación a su alrededor?");
            listPreguntas.Add("54.¿Se siente usted algunas veces enfadado?");
            listPreguntas.Add("55.¿Realiza sin deseos la mayor parte de las cosas que hace diariamente?");
            listPreguntas.Add("56¿Prefiere tener pocos amigos, pero selectos?");
            listPreguntas.Add("57.¿Tiene usted vértigos?");
        }
        private void llenaPreguntaCatell()
        {
            listPreguntas.Add("1.¿Tiende a cambiar rápidamente mi ínteres por las personas y diversiones?");
            listPreguntas.Add("2.¿Puedo yo mantenerme sereno aún cuando no piense muy bien de mí?");
            listPreguntas.Add("3.¿Cuando voy a exponer mis argumentos, me gusta esperar hasta estar seguro de estar en lo cierto?");
            listPreguntas.Add("4.¿Permito yo a veces que los celos influyan sobre mis acciones?");
            listPreguntas.Add("5.Si tuviera que vivir de nuevo mi vida");
            listPreguntas.Add("6.¿Admito yo la actuación de mis padres en todos los asuntos importantes?");
            listPreguntas.Add("7.¿Encuentro duro que me digan que no, aún cuando yo sepa que lo que pido es imposible?");
            listPreguntas.Add("8.Cuando alguien se muestra conmigo más amigable de lo que se podría esperar, ¿dudo yo de su honradez?");
            listPreguntas.Add("9.¿Cómo eran mis padres al exigir e imponer obediencia?");
            listPreguntas.Add("10.¿Necesito yo a mis amigos más de lo que ellos aparentan necesitar de mí?");
            listPreguntas.Add("11.¿Estoy yo seguro de poderme controlar ante una emergencia?");
            listPreguntas.Add("12.¿Le tenía yo miedo a la oscuridad cundo era niño?");
            listPreguntas.Add("13.¿Me dicen algunas veces que demuestro demasiado nerviosismo en la voz y en los gestos?");
            listPreguntas.Add("14.Si la gente se aprovecha de mi amistad,yo");
            listPreguntas.Add("15.El tipo de crítica personal que mucha gente hace¿Lejos de ayudarme me perturba?");
            listPreguntas.Add("16.¿Me da a mí de pronto ira con la gente?");
            listPreguntas.Add("17.¿Me siento yo intranquilo, como si quisiera algo pero no supiera qué?");
            listPreguntas.Add("18.¿Dudo yo a veces que las personas estén realmente interesadas en lo que estoy diciendo?");
            listPreguntas.Add("19.¿He tenido yo sentimientos vagos de mala salud, como dolores imprecisos o malas digestiones o palpitaciones u otros?");
            listPreguntas.Add("20.¿Me enojo tanto al discutir con algunas personas que casi no me atrevo a hablar?");
            listPreguntas.Add("21.¿Gasto yo más energía que preocupándome por algo que tenga que hacer, que la energíaque gastan otros en hacerlo?");
            listPreguntas.Add("22.¿Me preocupo por no distraerme y no olvidarme de los detalles?");
            listPreguntas.Add("23.A pesar de que tropiece con obstáculos difíciles y desagradables,¿siempre persevero e insisto en mis propósitos originales?");
            listPreguntas.Add("24.¿Tiendo yo a estar nervioso y confundido antes las contrariedades?");
            listPreguntas.Add("25.¿Tengo yo a veces sueños muy vividos que perturban mi descanso?");
            listPreguntas.Add("26.¿Tengo yo siempre suficiente energía cuando confronto dificultades?");
            listPreguntas.Add("27.¿Me siento a veces como obligado a contar el número de cosas que veo,sin ningún propósito particular?");
            listPreguntas.Add("28.¿La mayoría de las personas son un poco raras aunque no les guste admitirlo?");
            listPreguntas.Add("29.Cuando cometo un error social embarazoso, ¿Puedo olvidarlo pronto?");
            listPreguntas.Add("30.¿Me siento yo malhumorado y no deseo ver a nadie?");
            listPreguntas.Add("31.¿Cuando mis cosas salen mal casi lloro?");
            listPreguntas.Add("32.¿A pesar de estar de estar en una reunión social,¿me siento a veces agobiado por sentimientos de soledad e insignificancia?");
            listPreguntas.Add("33.Cuando despierto por la noche, ¿debido a las preocupaciones tengo dificultad en dormirme otra vez?");
            listPreguntas.Add("34.¿Se mantiene mi ánimo generalmente alto a pesar de cuantas dificultades confronte?");
            listPreguntas.Add("35.¿Tengo yo a veces sentimientos de culpa o remordimiento    por pequeños asuntos?");
            listPreguntas.Add("36.¿Se me ponen mis nervios de punta que ciertos sonidos, por ejemplo una bisagra chirriante,se me inaguantable y me da escalofríos?");
            listPreguntas.Add("37.¿Cuando algo me saca de quicio, recobro la calma rápidamente?");
            listPreguntas.Add("38.¿Habitualmente me duermo fácilmente?");
            listPreguntas.Add("39.¿Me preocupo e inquieto a veces al pensar en mis asuntos e intereses inmediato?");
            listPreguntas.Add("40.¿Me preocupo e inquieto a veces al pensar en mis asuntos e intereses inmediato?");
        }
        private void llenarPregunta16PF()
        {
            _16PF a1 = new _16PF("1.Creo que mi memoria es hoy mejor qu nunca.", "Cierto.", "Dudoso.", "Falso.");
            _16PF a2 = new _16PF("2.Podria vivir solo,felizmente,lejos de cualquiera ,como un ermitaño.", "Cierto.", "Dudoso.", "Falso.");
            _16PF a3 = new _16PF("3.Si yo digo que el cielo esta abajo y el invierno es caliente, a un criminal lo llamaría:", "Un bandolero.", "Un santo.", "Una nube.");
            _16PF a4 = new _16PF("4.Cuando veo personas sucias,desaseadas:", "Simplemente las acepto.", "Dudoso.", "Me molesta y me disgusta.");
            _16PF a5 = new _16PF("5.Me molesta escuchar a la gente decir que puede hacer algo mejor que nosotros:", "Sí.", "A veces.", "No.");
            _16PF a6 = new _16PF("6.En las fiestas dejo que sean otras personas las que hagan los chistes y los cuentos:", "Sí.", "A veces.", "No.");
            _16PF a7 = new _16PF("7.Cuando tengo tiempo libre siento que mi deber es emplearlo en actividades de utilidad social.", "Sí.", "Dudoso.", "No.");
            _16PF a8 = new _16PF("8.La mayoría de las personas que veo en una fiesta indudablemente se alegran de encontrarse conmigo.", "Sí.", "A veces.", "No.");
            _16PF a9 = new _16PF("9.Como ejercicio prefiero:", "Esgrima y Baile", "Dudoso", "Lucha y Pelota");
            _16PF a10 = new _16PF("10.Me sonrio de la gran diferencia que hay entre lo que hacen las personas y lo que dicen hacer.", "Sí.", "A veces.", "No.");
            _16PF a11 = new _16PF("11.Cuando niño me sentía triste al dejar el hogar para ir a la escuela cada día.", "Sí.", "A veces.", "No.");
            _16PF a12 = new _16PF("12.Si se pasa por alto una buena observación mía:", "Lo dejo pasar.", "Dudoso.", "Doy a la persona la oportunidad de escucharla nuevamente.");
            _16PF a13 = new _16PF("13.Cuando alguien tiene malos modales pienso:", "Que no es asunto mío.", "Dudoso.", "Que debo mostrar a la persona que la gente lo desaprueba.");
            _16PF a14 = new _16PF("14.Al conocer una nueva persona prefiero:", "Discutir con ella sus puntos de vista políticos y sociales. ", "Dudoso.", "Que me cuente algunos buenos chistes.");
            _16PF a15 = new _16PF("15.Cuando planeo algo me gusta hacerlo totalmente solo,sin ayuda externa:", "Sí.", "A veces.", "No.");
            _16PF a16 = new _16PF("16.Evito consumir tiempo soñando acerca de: Lo que pudiera haber sido", "Sí", "A veces", "No");
            _16PF a17 = new _16PF("17.Cuando voy a tomar un tren, me siento algo apresurado, tenso o ansioso, aunque sepa que tengo tiempo:", "Sí", "A veces", "No.");
            _16PF a18 = new _16PF("18.Algunas veces he tenido,aunque sea brevemente, sentimientos hotiles hacia mis padres", "Sí.", "Dudoso.", "No.");
            _16PF a19 = new _16PF("19.Yo podría ser feliz en un trabajo que requiera escuchar todo el día quejas desagradables de clientes y empleados.", "Sí.", "Dudoso.", "No.");
            _16PF a20 = new _16PF("20.Pienso que el opuesto de \"inexacto\" es:", "Casual.", "Preciso.", "Aproximado.");
            _16PF a21 = new _16PF("21.Siempre dispongo de gran cantidad de energía en el momento que lo necesito:", "Sí.", "Dudoso.", "No.");
            _16PF a22 = new _16PF("22.Me sería extremadamente penoso decir a la gente que he pedido dinero prestado:", "Sí.", "Dudoso.", "No.");
            _16PF a23 = new _16PF("23.Disfruto grandemente todas las grandes reuniones como fiestas y bailes:", "Sí.", "A veces.", "No.");
            _16PF a24 = new _16PF("24.Pienso que:", "Algunos trabajos no requieren hacerse cuidadosamente como otros", "Dudoso", "Cualquier trabajo debe ser realizado a \"cociencia\" sí es que ha de hacerse.");
            _16PF a25 = new _16PF("25.Me disgusta la forma que algunas personas lo miran a uno en calles o tiendas:", "Sí.", "Dudoso.", "No.");
            _16PF a26 = new _16PF("26.Yo preferiría destacarme:", "Como artista.", "Dudoso.", "Como atleta.");
            _16PF a27 = new _16PF("27.Si un vecino me engaña en coas triviales,prefiero \"hacerme de la vista gorda\" que desenmascararlo", "Sí.", "Dudoso.", "No.");
            _16PF a28 = new _16PF("28.Preferiría ver:", "Una buena película sobre los días difíciles de la guerra.", "Dudoso.", "Una comedia ingeniosa sobre la sociedad del futuro.");
            _16PF a29 = new _16PF("29.Cuando se me pone a cargo de una cosa, insisto en que mis instrucciones sean seguidas de lo contrario renuncio:", "Sí.", "A veces.", "No.");
            _16PF a30 = new _16PF("30.Encuentro juicioso evitar una exitación excesiva porque esto tiende a agotarme:", "Sí.", "A veces.", "No.");
            _16PF a31 = new _16PF("31.Si fuera bueno en ambas cosas preferiría jugar:", "Ajedrez", "Dudoso", "Bolos");
            _16PF a32 = new _16PF("32.Pienso que es cruel vacunar a los niños muy pequeños, aun contra enfermedades infecciosas y los padres tienen derecho a oponerse:", "Sí.", "Dudoso.", "No.");
            _16PF a33 = new _16PF("33.Tengo más fé:", "En la acción planificada", "Dudoso", "En la buena suerte");
            _16PF a34 = new _16PF("34.Siempre que lo necesito puedo olvidar mis inquietudes y responsabilidades:", "Sí.", "A veces.", "No.");
            _16PF a35 = new _16PF("35.Encuentro difícil admitir que estoy equivocado:", "Si.", "A veces.", "No.");
            _16PF a36 = new _16PF("36.En la fábrica preferiría estar a cargo de:", "Maquinaria o mantenimiento de registros.", "Dudoso.", "Emplear y conversar con el nuevo personal.");
            _16PF a37 = new _16PF("37.Qué palabra no corresponde con las otras dos:", "Gato", "Cercano", "Sol");
            _16PF a38 = new _16PF("38.Mi salud es afectada por cambios súbitos, causado por este motivo que altere mis planes:", "Sí.", "A veces.", "No.");
            _16PF a39 = new _16PF("39.Me complace ser servido, en momentos apropiados, por sirvientes personales:", "A menudo.", "Raras veces.", "Nunca.");
            _16PF a40 = new _16PF("40.Me siento algo torpe en compañía de otras personas de modo que no puedo hacer el buen papel que debería:", "Sí.", "A veces.", "No.");
            _16PF a41 = new _16PF("41.Pienso que las personas deberían observar las leyes morales más estrictamente de lo que lo hacen:", "Sí.", "A veces.", "No.");
            _16PF a42 = new _16PF("42.Algunas cosas me ponen tan irritado que prefiero no hablar:", "Sí.", "Dudoso.", "No.");
            _16PF a43 = new _16PF("43.Puedo realizar trabajos físicos duros sin agotarme tan pronto como otras personas:", "Sí.", "A veces.", "No.");
            _16PF a44 = new _16PF("44.Pienso que la mayoría de los testigos dicen la verdad aún cuando resulte penoso:", "Sí.", "Dudoso.", "No.");
            _16PF a45 = new _16PF("45.Considero que es mas provechoso caminar de un lado a otro cuando estoy pensando:", "Sí.", "Dudoso.", "No.");
            _16PF a46 = new _16PF("46.Pienso que este país haría mejor en gastar más en:", "Armamento.", "Dudoso.", "Educación.");
            _16PF a47 = new _16PF("47.Preferiría emplear una noche:", "En un juego difícil de carta.", "Dudoso.", "Mirando fotos de vacaciones anteriores.");
            _16PF a48 = new _16PF("48.Preferiría leer:", "Una buena novela histórica.", "Dudoso.", " Un ensayo de un científico sobre el dominio de los recursos naturales.");
            _16PF a49 = new _16PF("49.En realidad en el mundo existen más personas agradables que desagradables:", "Si.", "Dudoso.", "No.");
            _16PF a50 = new _16PF("50.Honestamente pienso que estoy más planificado, enérgico y emprendedor que muchas de las personas a quienes les va tan bien como a mi:", "Sí.", "A veces.", "No.");
            _16PF a51 = new _16PF("51.Hay momentos que no me siento con la debida disposición de ánimo para ver a ninguna persona:", "Muy raramente.", "Dudoso.", "Con bastante frecuencia.");
            _16PF a52 = new _16PF("52.Cuando estoy haciendo algo correcto, encuentro la tarea fácil:", "Siempre.", "A veces.", "Rara vez.");
            _16PF a53 = new _16PF("53.Yo preferiría:", "Estar en una oficina comercial,organizando y recibiendo personas.", "Dudoso.", "Ser arquitecto trazando planos en una habitación aislada.");
            _16PF a54 = new _16PF("54.El negro es al gris como el dolor es a:", "La herida.", "La enfermedad.", "La incomodidad.");
            _16PF a55 = new _16PF("55.Siempre duermo profundamente sin caminar o hablar en sueños:", "Sí.", "Dudoso.", "No.");
            _16PF a56 = new _16PF("56.Puedo mirar a cualquiera directamente a la cara y decir una mentira (si es con un fin correcto):", "Sí.", "A veces.", "No.");
            _16PF a57 = new _16PF("57.Yo he participado activamente en la organización de clubes, equipos o grupos sociales:", "Sí.", "A veces.", "No.");
            _16PF a58 = new _16PF("58.Yo admiro más:", "Un hombre inteligente aunque poco confiable.", "Dudoso.", "Un hombre promedio,pero fuerte pa resistir las tentaciones.");
            _16PF a59 = new _16PF("59.Cuando planteo una idea justa siempre logro que las cosas se ajusten a mí satisfacción:", "Sí.", "A veces.", "No.");
            _16PF a60 = new _16PF("60.Circunstacias desalentadora pueden llevarme cercano a las lágrimas:", "Sí.", "A veces.", "No.");
            _16PF a61 = new _16PF("61.Pienso que muchos países son realmente más amistosos de lo que suponemos:", "Sí.", "A veces.", "No.");
            _16PF a62 = new _16PF("62.Hay momentos, todos los días, en que deseo disfrutar de mis pensamientos sin ser interrumpido por otras persona:", "Sí.", "Dudoso.", "No.");
            _16PF a63 = new _16PF("63.Me molesta ser limitado por pequeñas leyes y reglamentos, aunque admito que son realmente necesarios:", "Sí.", "Dudoso.", "No.");
            _16PF a64 = new _16PF("64.Pienso que mucha de la llamada educación moderna \"progresista\" es menos acertada que el viejo criterio de que  \"sí no se pega el niño, se malcría\":", "Cierto.", "Dudoso.", "Falso.");
            _16PF a65 = new _16PF("65.En mis días escolares aprendí más:", "Concurriendo a clases.", "Dudoso.", "Leyendo un libro.");
            _16PF a66 = new _16PF("66.Evito verme envuelto en responsabilidades y organizaciones sociales:", "Cierto.", "A veces.", "Falso.");
            _16PF a67 = new _16PF("67.Cuando un problema se hace difícil y hay mucho que hacer, pruebo:", "Un problema diferente.", "Dudoso.", "Un enfoque diferente sobre él mismo problema.");
            _16PF a68 = new _16PF("68.Sufro de fuertes estados emocionales, ansiedad, ira, risa, etc. que parecen no tener causa real:", "Sí.", "A veces.", "No.");
            _16PF a69 = new _16PF("69.Hay ocaciones en que mi mente no trabaja con tanta claridad como en otras:", "Cierto.", "Dudoso.", "Falso.");
            _16PF a70 = new _16PF("70.Me agrada complacer a otras prsonas haciendo los compromisos en las horas que ellos desean, aunque sea algo incoveniente para mí:", "Sí.", "A veces.", "No.");
            _16PF a71 = new _16PF("71.Estimo que número apropiado para continuar la serie 1,2,3,6,5 es:", "10.", "5.", "7.");
            _16PF a72 = new _16PF("72.Soy propenso a criticar el trabajo de otros:", "Sí.", "A veces.", "No.");
            _16PF a73 = new _16PF("73.Prefiero prescindir de algo que causaría a un camarero un gran trabajo extra:", "Sí.", "A veces.", "No.");
            _16PF a74 = new _16PF("74.Me encata viajar, en cualquier momento:", "Sí.", "A veces.", "No.");
            _16PF a75 = new _16PF("75.A veces he estado apunto de desmayarme ante un dolor o a la vista de sangre:", "Sí.", "Dudoso.", "No.");
            _16PF a76 = new _16PF("76.Disfruto mucho al conversar con otras personas sobre problemas locales:", "Sí.", "A veces.", "No.");
            _16PF a77 = new _16PF("77.Preferiría ser:", "Ingeniero en construcciones.", "Dudoso.", "Profesor de teoría y costumbres sociales.");
            _16PF a78 = new _16PF("78.Tengo que refrenarme para no involucrarme demasiado en los problemas de los demás:", "Sí.", "A veces.", "No.");
            _16PF a79 = new _16PF("79.Encuentro que la conversación de mis vecinos es aburrida y tediosa:", "En la mayoría de los casos.", "Dudoso.", "Sólo en algunos casos.");
            _16PF a80 = new _16PF("80.Por lo general yo no noto la propaganda disimulada en lo que leo, a no ser que alquien me lo señale:", "Cierto.", "A veces.", "Falso.");
            _16PF a81 = new _16PF("81.Opino que todo cuento y película deben tener una moraleja:", "Sí.", "A veces.", "No.");
            _16PF a82 = new _16PF("82.Surgen más problemas de personas:", "Que cambian y modifican métodos que hasta ahora han dado resultado.", "Dudoso.", "Que rechazan nuevos metodos prometedores.");
            _16PF a83 = new _16PF("83.Algunas veces dudo usar mis propias ideas por temor a que sea poco práctica:", "Sí.", "Dudoso.", "No.");
            _16PF a84 = new _16PF("84.Las personas \"estiradas\", estrictas, parecen no llevarse bien conmigo:", "Cierto.", "A veces.", "Falso.");
            _16PF a85 = new _16PF("85.Mi memoria no cambia mucho de un día a otro:", "Cierto.", "A veces.", "No.");
            _16PF a86 = new _16PF("86.Puede que yo sea menos considerado con otras persona que lo que ellas son conmigo:", "Cierto.", "A veces.", "Falso.");
            _16PF a87 = new _16PF("87.Me modero más que la mayoría de las personas en expresar cuáles son mis sentimientos:", "Sí.", "A veces.", "No.");
            _16PF a88 = new _16PF("88.Si dos manecillas de reloj se juntan exactamente cada 65 minutos, el reloj está funcionando:", "Atrasado.", "A su hora.", "Adelantado.");
            _16PF a89 = new _16PF("89.Me pongo impaciente, y comienzo a disgutarme y alterarme cuando las personas me demoran innecesariamente:", "Cierto.", "A veces.", "Falso.");
            _16PF a90 = new _16PF("90.La gente dice que me gusta que las cosas se hagan a mi modo:", "Cierto.", "A veces.", "Falso.");
            _16PF a91 = new _16PF("91.Generalmente no haría nada sí los instrumentos que me entregan para realizar un trabajo no son del todo adecuado:", "Cierto.", "A veces.", "Falso.");
            _16PF a92 = new _16PF("92.En mi hogar,cuando dispongo de un tiempo libre,yo:", "Lo uso para conversar y descansar.", "Dudoso.", "Planifico para emplearlo en trabajos especiales.");
            _16PF a93 = new _16PF("93.Soy tímido y cuidadoso para hacer nuevas amistades:", "Sí.", "A veces.", "No.");
            _16PF a94 = new _16PF("94.Creo qe las personas dicen en poesía podría decirse igual en prosa:", "Sí.", "A veces.", "No.");
            _16PF a95 = new _16PF("95.Sospecho que las perosnas que actuan amistosamente para conmigo pueden ser desleales a mis espaldas:", "Si,generalmete.", "En ocasiones.", "No,muy rara vez");
            _16PF a96 = new _16PF("96.Pienso que aún las experiencias más dramáticas que tengo durante el año dejan mi personalidad casi lo mismo que era:", "Sí.", "A veces.", "No.");
            _16PF a97 = new _16PF("97.Tiendo hablar más bien lentamente:", "Sí.", "A veces.", "No.");
            _16PF a98 = new _16PF("98.Siento temores irrazonables o aversiones por alguna cosa,por ejemplo,ciertos animales,lugares,etc:", "Sí.", "A veces.", "No.");
            _16PF a99 = new _16PF("99.En una tarea en grupo preferiría:", "Ser quien ensaye en la organización.", "Dudoso.", "Llevar los registros y ver que las reglas sean obervadas.");
            _16PF a100 = new _16PF("100.Para opinar bien sobre un asunto social, leería:", "UNa novela sobre el tema ampliamente recomendado.", "Dudoso.", "Un texto enumerando datos,estadísticas y otros.");
            _16PF a101 = new _16PF("101.Tengo sueños bastantes fantásticos o ridículos (Durmiendo):", "Sí.", "A veces.", "No.");
            _16PF a102 = new _16PF("102.Si me dejan en una casa solitaria, después de un tiempo, tiendo a sentirme ansioso o temeroso:", "Sí.", "A veces.", "No.");
            _16PF a103 = new _16PF("103.Puedo engañar a las personas portándome ammistoso cuando realmente las detesto:", "Sí .", "A veces.", "No.");
            _16PF a104 = new _16PF("104.Que palabra no corresponde con las otras dos:", "Correr.", "Ver.", "Tocar.");
            _16PF a105 = new _16PF("105.Si la madre de María es hermana del padre de Federíco,que parentezco existe entre Federico y el padre de María:", "Primo.", "Sobrino.", "Tío.");




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
        private void llenarPreguntaFactoHumanno()
        {
            listPreguntas.Add("1.Concentración en un trabajo de detalle.");
            listPreguntas.Add("2.Habilidad para tomar desiciones en las tareas bajo su responsabilidad.");
            listPreguntas.Add("3.Persistencia para trabajar en forma continua en trabajo de rutina.");
            listPreguntas.Add("4.Habilidad para organizar diferentes tipos de gentes.");
            listPreguntas.Add("5.Necesidad de ser diplomático y coopertaivo.");
            listPreguntas.Add("6.Desición para actuar sin presedentes.");
            listPreguntas.Add("7.Creatividad para generar nuevas ideas.");
            listPreguntas.Add("8.Habilidad para iniciar relaciones con extraños.");
            listPreguntas.Add("9.Constancia de seguir un patrón de trabajo establecido.");
            listPreguntas.Add("10.Necesidad de tener al jefe disponible para ayuda.");
            listPreguntas.Add("11.Seguridad y dominio para expresar con fluidez.");
            listPreguntas.Add("12.Capacidad de seguir un sistema a la perfección.");
            listPreguntas.Add("13.Habilidad para resolver conflictos humanos.");
            listPreguntas.Add("14.Necesidad de pertenecer a un mismo lugar de trabajo.");
            listPreguntas.Add("15.Ritmo y coordinación de un trabjo repetitivo.");
            listPreguntas.Add("16.Capacidad para hacer frente a interrupciones y cambios durante el trabajo.");
            listPreguntas.Add("17.Ser cautelosos al calcular riesgos.");
            listPreguntas.Add("18.Poder motivacional para hacer que la gente actúe.");
            listPreguntas.Add("19.Habilidad para superar objeciones.");
            listPreguntas.Add("20.Visión para planear el futuro en gran escala.");
            listPreguntas.Add("21.Habilidad para persuadir a otras personas hacia nuestro punto de vista.");
            listPreguntas.Add("22.Cautelosos en la toma de desiciones que pueden setar presedentes.");
            listPreguntas.Add("23.Paciencia para seguir instrucciones detalladas.");
            listPreguntas.Add("24.Satisfacción para mantenerse al nivel del puesto actual");
        }
        private void llenarPruebaButt()
        {
            listPreguntas.Add("1.Indiferente y cansado.");
            listPreguntas.Add("2.Decidido a ser el primero.");
            listPreguntas.Add("3.Emocionado.");
            listPreguntas.Add("4.Como queriendo ayudar a otro a mejorar.");
            listPreguntas.Add("5.Lleno de energía.");
            listPreguntas.Add("6.Irritable sin razón alguna.");
            listPreguntas.Add("7.Como si ganar fuese muy importante para ti.");
            listPreguntas.Add("8.Como parte de o muy amigable hacia el grupo (compañero, equipo o club).");
            listPreguntas.Add("9.Impulsivo.");
            listPreguntas.Add("10.Irritado porque alguien lo hizo mejor que tú.");
            listPreguntas.Add("11.Más feliz que nunca.");
            listPreguntas.Add("12.Culpable por no hacerlo mejor.");
            listPreguntas.Add("13.Poderoso.");
            listPreguntas.Add("14.Muy nervioso.");
            listPreguntas.Add("15.Complacido porque alguien lo hizo bien.");
            listPreguntas.Add("16.Que estabas haciendo más de lo que podías.");
            listPreguntas.Add("17.Que querías llorar.");
            listPreguntas.Add("18.Con deseo de botar a alguien.");
            listPreguntas.Add("19.Más interesado en tu deporte que en otra cosa.");
            listPreguntas.Add("20.Disgustado porque no ganaste.");
            listPreguntas.Add("21.Con ganas de hacer algo por el equipo y el grupo.");
            listPreguntas.Add("22.Como si te quisieras fajar con el que se interponga en tú camino (empujando, golpeando).");
            listPreguntas.Add("23.Tú has logrado algo (una destreza) bastante nueva para ti.");
            listPreguntas.Add("24.Como si los otros estuvieran obteniendo más de lo que merecen (más que la parte justa de atención o recompensa) .");
            listPreguntas.Add("25.Como para felicitar a alguien porque lo hizo bien.");

        }

        private void llenarPruebaCualiMotivDepor()
        {
            listPreguntas.Add("1.-A pesar del tiempo que estuvo alejado de las competencias, buscará sentirse el favorito para ganar.");
            listPreguntas.Add("2.Procurará disfrutar del dominio técnico más que pensar en el resultado.  El éxito se deriva de un buen trabajo.");
            listPreguntas.Add("3.Frente a una competencia muy importante lo arriesgará todo, sin pensar en el tiempo que estuvo alejado ni en  las consecuencias de un fracaso.");
            listPreguntas.Add("4.Aunque se siente muy bien y es una competencia muy importante, considerará que estuvo alejado y será cauteloso, tratando más de evitar un fracaso que lograr un éxito arriesgado.");
            listPreguntas.Add("5.Decide permanecer en el deporte para intentar la hazaña (objetivamente posible) de pasar a la historia de su especialidad como el único que ha logrado hacer su mejor rendimiento después de una ausencia tan prolongada.");
            listPreguntas.Add("6.Decide permanecer en el deporte y, aunque resulta objetivamente posible intentar la hazaña de mejorar su rendimiento, cree que es mejor competir bien y no aspirar por el momento a nada extraordinario.");
            listPreguntas.Add("7.No le preocupa la ejecución porque practica el deporte esencialmente por placer.");
            listPreguntas.Add("8.Decide permanecer en el deporte pues lo beneficia en varios aspectos de su vida que son relevantes para él.");
            listPreguntas.Add("9.Se afianzará en la fama que volverá a tener con una participación victoriosa.");
            listPreguntas.Add("10.Buscará, sobre todo, rescatar su liderazgo técnico.");
            listPreguntas.Add("11.Intentará demostrarle a sus detractores que estaban equivocados.  Podrá vengarse de quienes dudaron de él.");
            listPreguntas.Add("12.La repercusión que su éxito puede tener para su equipo será su mayor motivación.");
            listPreguntas.Add("13.Ganar la competencia le reportará beneficios y mejoraría su nivel de vida personal y familiar.");
            listPreguntas.Add("14.Buscará con su actuación en esa competencia el reconocimiento de todos.");
            listPreguntas.Add("15.No se propone ganar esa competencia, no es el momento para ello.");
            listPreguntas.Add("16.Lo que más aprecia de la ocasión es volver a sentirse en ambiente competitivo, disfrutar de lo que más le gusta de su deporte.");
            listPreguntas.Add("17.Lo que más aprecia de la ocasión es la oportunidad de rescatar su popularidad y el nivel de vida de un campeón.");
            listPreguntas.Add("18.Piensa en todas las necesidades que pudiera satisfacer al obtener una victoria.");
            listPreguntas.Add("19.Persigue volver a ser quien era y gozar del respeto de los demás.");
            listPreguntas.Add("20.Busca la satisfacción de sí mismo y el rescate de su autoestima con su retorno.");
            listPreguntas.Add("21.Se propone ganar para mostrar su victoria a quienes lo creen sin posibilidades.");
            listPreguntas.Add("22.Se inspira sobretodo en la significación social y patriótica de su actuación.");
            listPreguntas.Add("23.Piensa que la posibilidad de una derrota está presente debido al tiempo que estuvo alejado, y no se propone una victoria inmediata.");
            listPreguntas.Add("24.Prevalece en su mente la realización de movimientos técnicos limpios y elegantes.");
            listPreguntas.Add("25.- En el transcurso de la competencia se apoyará fuertemente en la seguridad de que los problemas quedaron atrás y buscará la victoria a toda costa.");
            listPreguntas.Add("26.Contribuir con los resultados a una buena posición de su delegación en la lucha por países es su motor impulsor");
            listPreguntas.Add("27.En su mente sólo existe la posibilidad de la victoria y su único objetivo es ganar la competencia");
            listPreguntas.Add("28.Más que pensar en un resultado, su objetivo será lograr el mejor grado de perfección en sus ejecuciones");
            listPreguntas.Add("29.La convicción de que la victoria es el único resultado posible lo motiva. Nada lo alejará del objetivo de ganar");
            listPreguntas.Add("30.Será prudente y no pondrá en peligro su recuperación física. No es el momento oportuno para arriesgarlo todo por una victoria");
            listPreguntas.Add("31.Pretenderá lograr un resultado histórico como cierre de una etapa de esfuerzo máximo y perfeccionamiento en su especialidad.");
            listPreguntas.Add("32.Considerará que es suficiente un buen resultado sin grandes pretensiones, que demuestre que aún se puede contar con él.");
            listPreguntas.Add("33.Siente que el disfrute de competir y de hacer el mayor esfuerzo es lo que más lo motiva en esta oportunidad, sin pensar en otros beneficios");
            listPreguntas.Add("34.Lo estimula volver a competir y ganar, pues le permitirá mantener todos los beneficios de ser un campeón");
            listPreguntas.Add("35.Lo que representa un estímulo para él es el reconocimiento y la admiración de las personas que lo ven retornar a la competición, luego de su prolongada ausencia");
            listPreguntas.Add("36.Lo más importante para él es volver a sentirse seguro de sí mismo y con dominio para lograr sus objetivos y metas al enfrentarse a esta competencia");
            listPreguntas.Add("37.Las recompensas materiales que obtendría mediante un buen resultado constituyen la fuente que lo impulsa a competir bien");
            listPreguntas.Add("38.Hacer desaparecer toda duda y desconfianza en sus capacidades y potencialidades en los demás, constituye algo de mucha importancia para él en esta competencia. ");
            listPreguntas.Add("39-Contribuir con su resultado a una buena posición de su delegación en la lucha por países es su motor impulsor.");

            listPreguntas.Add("40.El deportista persigue el éxito, sin atender de manera especial las acciones técnicas que deben conducirlo a él. ");
            listPreguntas.Add("41.Más que ganar, lo que desea al competir es sentirse capaz de lograr un alto grado de perfección en su actuación.");
            listPreguntas.Add("42.Competirá sin escatimar esfuerzos ni energías en pos de la victoria. No contempla la posibilidad de perder en la lid.");
            listPreguntas.Add("43.Durante la competencia piensa en la posibilidad de un fracaso y puede ser cauteloso al tomar decisiones o realizar ejecuciones.");
            listPreguntas.Add("44.Es más importante para él buscar una maestría técnica por el momento.");
            listPreguntas.Add("45.Teniendo en cuenta las lesiones anteriores, considera que debe esperar a futuras competencias para lograr un resultado exitoso y no esperar nada importante en esta.");
            listPreguntas.Add("46.Disfrutar de la práctica deportiva y sentir nuevamente las emociones competitivas es suficiente para él en esta competencia.");
            listPreguntas.Add("47.Su status deportivo le reporta satisfacciones extras que le interesa mantener, por lo que esta competencia será una oportunidad para lograrlo.");
            listPreguntas.Add("48.El reconocimiento nacional e internacional que puede alcanzar con una victoria después de la prolongada ausencia, será lo que más lo impulse a competir.");
            listPreguntas.Add("49.En su mente siempre está la idea de mantenerse como figura estelar y ser visto como referencia en el deporte.");
            listPreguntas.Add("50.Mantener el status de vida que ha alcanzado con su exitosa carrera, es lo que lo estimula a recuperar su nivel deportivo.");
            listPreguntas.Add("51.El orgullo de mantener una posición cimera dentro del ranking de su especialidad lo motiva de manera especial.");
            listPreguntas.Add("52.Piensa sobre todo en la importancia de su victoria para sus compañeros de equipo, entrenadores y colectivo que intervino en su preparación.");



        }
        private void llenarPreguntaLoehr()
        {
            listPreguntas.Add("1.Me veo más como perdedor que como ganador en una competencia.");
            listPreguntas.Add("2.Me enojo y frustro en la competencia.");
            listPreguntas.Add("3.Me distraigo y pierdo la concentración en la competencia.");
            listPreguntas.Add("4.Antes de competir me veo riendome perfectamente.");
            listPreguntas.Add("5.Estoy altamente motivado para competir lo mejor que pueda.");
            listPreguntas.Add("6.Puedo mantener una afluencia de energía durante la competencia.");
            listPreguntas.Add("7.Soy un pensador positivo durante la competencia.");
            listPreguntas.Add("8.Creo en mí mismo como competidor.");
            listPreguntas.Add("9.Me pongo nervioso o miedoso en la competencia.");
            listPreguntas.Add("10.Parece que mi cabeza se acelera a 100 km por hora durante los momentos críticos de la competencia.");
            listPreguntas.Add("11.Practico mentalmente mis actividades físicas.");
            listPreguntas.Add("12.Las metas que me he impuesto como deportista me hacen trabajar mucho.");
            listPreguntas.Add("13.Puedo disfrutar de la competncia aunque me enfrente a muchos problemas difíciles.");
            listPreguntas.Add("14.Me digo cosas negativas durante la competencia.");
            listPreguntas.Add("15.Pierdo la confianza rápidamente.");
            listPreguntas.Add("16.Las equivocaciones me llevan a sentir.");
            listPreguntas.Add("17.Puedo borrar emociones que interfieran negativamente y volver a concentrarme.");
            listPreguntas.Add("18.La visualización de mi deporte me es fácil.");
            listPreguntas.Add("19.No me tienen que empujar para competir o entrar fuertemente. Yo me estimulo solo.");
            listPreguntas.Add("20.Tiendo a sentirme aplastado emocionalmente cuando las cosas se vuelven en mi contra.");
            listPreguntas.Add("21.Hago un 100% de esfuerzo cuando compito sin importarme nada.");
            listPreguntas.Add("22.Puedo rendir en el pico máximo de mi talento y habitabilidad.");
            listPreguntas.Add("23.Mis músculos se tensionan demasiado durante la competencia.");
            listPreguntas.Add("24.Mi mente se aleja del combate, partido o carrera durante la competencia.");
            listPreguntas.Add("25.Me visualizo en situaciones difíciles previo a la competecia.");
            listPreguntas.Add("26.Estoy a dar todo lo necesario para llegar a mi máximo potencial como atleta.");
            listPreguntas.Add("27.Estreno con alta intensidad positiva.");
            listPreguntas.Add("28.Puedo cambiar estados emocionales negativos a positivos por medio del control mental.");
            listPreguntas.Add("29.Soy un competidor con fortaleza mental.");
            listPreguntas.Add("30.Hechos incontrolables como el miedo, oponentes tramposos y malos hábito me perturban.");
            listPreguntas.Add("31.Mientras juego me acuesto pensando en equivocaciones pasadas u oportunidades perdidas.");
            listPreguntas.Add("32.Uso imágenes mientras compito que me ayuden a competir mejor.");
            listPreguntas.Add("33.Me aburro y me agoto.");
            listPreguntas.Add("34.Me lleno de sensacioines y desafíos y me inspiro en situaciones difíciles.");
            listPreguntas.Add("35.Mis entrendores dirían que yo tengo una buena actitud.");
            listPreguntas.Add("36.Yo proyecto la imagen de un luchador confiado.");
            listPreguntas.Add("37.Puedo mantenerme calmado durante la competencia cuando estoy confundido por problemas.");
            listPreguntas.Add("38.Mi concentración se rompe fácilmente.");
            listPreguntas.Add("39.Cuando me veo compitiendo puedo ver y sentir las cosas vívidamente.");
            listPreguntas.Add("40.Me despierto por la mañana y estoy realmente excitado por competir y entrenar.");
            listPreguntas.Add("41.Competir en este deportem da una sensación genuina de alegría y plenitud.");
            listPreguntas.Add("42.Puede transformar una crisis en oportunindad.");



        }

        private void nextQuestion()
        {

            if (countList != listPreguntas.Count)
            {
                if (checkMode == 0)
                {

                    label4.Text = listPreguntas[countList].ToString();
                    limpiarLabelColor();


                    if (label4.Text == "21.Me siento bien.")
                        if (prueba == "Idare" && tipoIdare == "Idare(Situacional y de Raso)")
                        {
                            label3.Text = "Trate de dar respuesta a la opción que mejor describa sus sentimientos \"GENERALMENTE\" ";
                            label5.Text = "Casi nunca";
                            label6.Text = "Algunas veces";
                            label7.Text = "Frecuentemente";
                            label8.Text = "Casi siempre";
                        }


                }
                else
                {
                    limpiarLabelColor();

                    label4.Text = listRespuesta[countList].pregunta;
                    valor = Convert.ToInt32(listRespuesta[countList].valor);


                    String result = listRespuesta[countList].valor;

                    pintarForeColor(valor);



                    if (prueba == "Idare" && tipoIdare == "Idare(Situacional y de Raso)")
                    {
                        if (label4.Text == "1.Me siento calmado.")
                        {

                            label3.Text = "Trate de dar respuesta a la opción que mejor describa sus sentimientos \"AHORA\" ";
                            label5.Text = "No en lo absoluto";
                            label6.Text = "Un poco";
                            label7.Text = "Bastante";
                            label8.Text = "Mucho";

                        }
                        if (label4.Text == "21.Me siento bien.")

                        {
                            label3.Text = "Trate de dar respuesta a la opción que mejor describa sus sentimientos \"GENERALMENTE\" ";
                            label5.Text = "Casi nunca";
                            label6.Text = "Algunas veces";
                            label7.Text = "Frecuentemente";
                            label8.Text = "Casi siempre";
                        }
                    }




                }


            }
            else
            {



                time.Stop();
                totalTiempoTest = time.ElapsedMilliseconds.ToString();

                if (prueba == "IDARE (Situacional)")
                {
                    salvarIdareSituacional();
                }

                if (prueba == "IDARE (Rasgo)")
                {
                    salvarIdareRasgo();
                }

                if (prueba == "Actitud ante la Competencia")
                {
                    salvarActitudCompetencia();
                }

                if (prueba == "Cualidades Motivacionales Deportivas")
                {
                    salvarCualidadesMotivacionales();
                }



                if (prueba == "Test de Motivos Deportivos de Butt")
                {
                    salvarButt();
                }

                if (prueba == "Inventario Psicológico de Ejecución Deportiva (IPED)")
                {
                    salvarIPED();
                }

                if (prueba == "Eysenck")
                {
                    salvarEysenck();
                }

                if (prueba == "IDETEM-1")
                {
                    salvarIdetem();
                }

                if (prueba == "Catell")
                {
                    salvarCatell();
                }

                if (prueba == "Cualidades Volitivas en el Deporte")
                {
                    salvarCualidadesVolitivas();
                }

                if (prueba == "POMS")
                {
                    salvarPOMS();
                }

                if (prueba == "Ansiedad Precompetitiva CSAI-2R")
                {
                    salvarAnsiedaPreCompetitiva();
                }

                MessageBox.Show("Prueba terminada. Debe informar al especialista", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.Close();
                //  }

            }

        }

        private void salvarAnsiedaPreCompetitiva()
        {
            int AS = 0;
            int AC = 0;
            int ACF = 0;

            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if (realPos == 1 || realPos == 4 || realPos == 6 || realPos == 9 || realPos == 12 || realPos == 15 || realPos == 17)
                {
                    AS += Convert.ToInt32(listRespuesta[i].valor);
                }
                if (realPos == 2 || realPos == 5 || realPos == 8 || realPos == 11 || realPos == 14)
                {
                    AC += Convert.ToInt32(listRespuesta[i].valor);
                }
                if (realPos == 3 || realPos == 7 || realPos == 10 || realPos == 13 || realPos == 16)
                {
                    ACF += Convert.ToInt32(listRespuesta[i].valor);
                }
            }


            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");


            using (mainEntities entities = new mainEntities())
            {

                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);


                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    AnsiedadCompetitiva pruR = new AnsiedadCompetitiva();

                    pruR.Fecha = date;
                    pruR.AC = AC.ToString();
                    pruR.ACF = ACF.ToString();
                    pruR.AS = AS.ToString();
                    
                    entities.AnsiedadCompetitiva.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<AnsiedadCompetitiva>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PAnsiedadCompetitiva = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PAnsiedadCompetitiva == null)
                    {
                        AnsiedadCompetitiva pruR = new AnsiedadCompetitiva();

                       
                        pruR.Fecha = date;
                        pruR.AC = AC.ToString();
                        pruR.ACF = ACF.ToString();
                        pruR.AS = AS.ToString();


                        entities.AnsiedadCompetitiva.Add(pruR);
                        entities.SaveChangesAsync();


                        var ultimo = entities.Set<AnsiedadCompetitiva>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PAnsiedadCompetitiva = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.AnsiedadCompetitiva.Where(f => f.idTest == sujetoEva.PAnsiedadCompetitiva).FirstOrDefault<AnsiedadCompetitiva>();


                        conect.Fecha = date;
                        conect.AC = AC.ToString();
                        conect.ACF = ACF.ToString();
                        conect.AS = AS.ToString();


                        entities.SaveChangesAsync();

                    }
                }
            }




        }

        private void salvarPOMS()
        {
            int tensionAnsiedad = 0;
            int depresMelanco = 0;
            int angustiHostili = 0;
            int vigorActivi = 0;
            int fatigaInerc = 0;
            int ConfOrienta = 0;
            int amistosidad = 0;


            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if (realPos == 2 || realPos == 10 || realPos == 16 || realPos == 20 || realPos == 22 || realPos == 26 || realPos == 27 || realPos == 34 || realPos == 41)
                {
                    if (realPos == 22)
                        tensionAnsiedad -= 1;
                    else
                        tensionAnsiedad += 1;
                }


                if (realPos == 5 || realPos == 9 || realPos == 14 || realPos == 18 || realPos == 21 || realPos == 23 || realPos == 32 || realPos == 35 || realPos == 36 || realPos == 44 || realPos == 45 || realPos == 48 || realPos == 58 || realPos == 61 || realPos == 62)
                {
                    depresMelanco += 1;
                }


                if (realPos == 3 || realPos == 12 || realPos == 17 || realPos == 24 || realPos == 31 || realPos == 33 || realPos == 39 || realPos == 42 || realPos == 47 || realPos == 52 || realPos == 53 || realPos == 57)
                {
                    angustiHostili += 1;
                }

                if (realPos == 7 || realPos == 15 || realPos == 19 || realPos == 38 || realPos == 51 || realPos == 56 || realPos == 60 || realPos == 63)
                {
                    vigorActivi += 1;
                }

                if (realPos == 4 || realPos == 11 || realPos == 29 || realPos == 40 || realPos == 46 || realPos == 49 || realPos == 65)
                {
                    fatigaInerc += 1;
                }

                if (realPos == 8 || realPos == 28 || realPos == 37 || realPos == 50 || realPos == 54 || realPos == 59 || realPos == 64)
                {
                    if (realPos == 54)
                        ConfOrienta -= 1;
                    else
                        ConfOrienta += 1;
                }

                if (realPos == 1 || realPos == 6 || realPos == 13 || realPos == 25 || realPos == 30 || realPos == 43 || realPos == 55)
                {
                    amistosidad += 1;
                }
            }


            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");


            using (mainEntities entities = new mainEntities())
            {

                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);


                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    PruPoms pruR = new PruPoms();

                    pruR.Fecha = date;
                    pruR.TensionAnsiedad = tensionAnsiedad.ToString();
                    pruR.DepresionMelancolia = depresMelanco.ToString();
                    pruR.AngustiaHostilidad = angustiHostili.ToString();
                    pruR.VigorActividad = vigorActivi.ToString();
                    pruR.FatigaInercia = fatigaInerc.ToString();
                    pruR.ConfusionDesorient = ConfOrienta.ToString();
                    pruR.Amistosidad = amistosidad.ToString();

                    entities.PruPoms.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruPoms>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PPoms = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PPoms == null)
                    {
                        PruPoms pruR = new PruPoms();

                        pruR.Fecha = date;
                        pruR.TensionAnsiedad = tensionAnsiedad.ToString();
                        pruR.DepresionMelancolia = depresMelanco.ToString();
                        pruR.AngustiaHostilidad = angustiHostili.ToString();
                        pruR.VigorActividad = vigorActivi.ToString();
                        pruR.FatigaInercia = fatigaInerc.ToString();
                        pruR.ConfusionDesorient = ConfOrienta.ToString();
                        pruR.Amistosidad = amistosidad.ToString();

                        entities.PruPoms.Add(pruR);
                        entities.SaveChangesAsync();


                        var ultimo = entities.Set<PruPoms>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PPoms = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruPoms.Where(f => f.idTest == sujetoEva.PPoms).FirstOrDefault<PruPoms>();


                        conect.Fecha = date;
                        conect.TensionAnsiedad = tensionAnsiedad.ToString();
                        conect.DepresionMelancolia = depresMelanco.ToString();
                        conect.AngustiaHostilidad = angustiHostili.ToString();
                        conect.VigorActividad = vigorActivi.ToString();
                        conect.FatigaInercia = fatigaInerc.ToString();
                        conect.ConfusionDesorient = ConfOrienta.ToString();
                        conect.Amistosidad = amistosidad.ToString();


                        entities.SaveChangesAsync();

                    }
                }
            }


        }

        private void salvarIdareRasgo()
        {

            int[] sum3Arr = new int[] { 1, 2, 3, 4, 7, 8, 10, 11, 13, 14, 16, 17, 19 };
            int[] sum4Arr = new int[] { 0, 5, 6, 9, 12, 15, 18 };
            string ansiedadRasgo = "";

            int sum3 = 0;
            int sum4 = 0;

            int j = 0;

            while (j < 13)
            {

                sum3 += Convert.ToInt32(listRespuesta[sum3Arr[j]].valor);

                if (j < 7)
                    sum4 += Convert.ToInt32(listRespuesta[sum4Arr[j]].valor);

                j++;
            }


            int calRasgo = sum3 - sum4 + 35;

            if (calRasgo <= 30)
                ansiedadRasgo = "Nivel Bajo";
            if (calRasgo > 31 && calRasgo <= 44)
                ansiedadRasgo = "Nivel Medio";
            if (calRasgo >= 45)
                ansiedadRasgo = "Nivel Alto";




            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");


            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);


                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    PruIdareRago pruR = new PruIdareRago();

                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;
                    pruR.DiagAnsRasgo = ansiedadRasgo;
                    pruR.PAnsiedadRasgo = calRasgo.ToString();


                    entities.PruIdareRago.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruIdareRago>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PIdareRasgo = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PIdareRasgo == null)
                    {
                        PruIdareRago pruR = new PruIdareRago();

                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;
                        pruR.DiagAnsRasgo = ansiedadRasgo;
                        pruR.PAnsiedadRasgo = calRasgo.ToString();



                        entities.PruIdareRago.Add(pruR);
                        entities.SaveChangesAsync();


                        var ultimo = entities.Set<PruIdareRago>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PIdareRasgo = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruIdareRago.Where(f => f.idTest == sujetoEva.PIdareRasgo).FirstOrDefault<PruIdareRago>();


                        conect.DuraPru = totalTiempoTest;
                        conect.DiagAnsRasgo = ansiedadRasgo;
                        conect.PAnsiedadRasgo = calRasgo.ToString();


                        entities.SaveChangesAsync();

                    }
                }
            }

        }

        private void salvarCualidadesVolitivas()
        {
            int autoInd = 0;
            int tenaReso = 0;
            int persePersi = 0;
            int autodoAutoc = 0;
            int falseamiento = 0;

            String autoIndEvaluacion = "";
            String tenaResoEvaluacion = "";
            String persePersiEvaluacion = "";
            String autodoAutocEvaluacion = "";


            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if (realPos == 1 || realPos == 6 || realPos == 11 || realPos == 16 || realPos == 21 || realPos == 25)
                {
                    if (realPos == 16 || realPos == 25)
                    {
                        autoInd += 4 - Convert.ToInt32(listRespuesta[i].valor);
                    }
                    else
                    {
                        autoInd += Convert.ToInt32(listRespuesta[i].valor);
                    }

                }
                if (realPos == 2 || realPos == 7 || realPos == 12 || realPos == 17 || realPos == 22 || realPos == 26)
                {
                    if (realPos == 22 || realPos == 26)
                    {
                        tenaReso += 4 - Convert.ToInt32(listRespuesta[i].valor);
                    }
                    else
                    {
                        tenaReso += Convert.ToInt32(listRespuesta[i].valor);
                    }

                }
                if (realPos == 3 || realPos == 8 || realPos == 13 || realPos == 18 || realPos == 23 || realPos == 27)
                {
                    if (realPos != 27)
                    {
                        persePersi += 4 - Convert.ToInt32(listRespuesta[i].valor);
                    }
                    else
                    {
                        persePersi += Convert.ToInt32(listRespuesta[i].valor);
                    }


                }
                if (realPos == 4 || realPos == 9 || realPos == 14 || realPos == 19 || realPos == 24 || realPos == 28)
                {
                    autodoAutoc += 4 - Convert.ToInt32(listRespuesta[i].valor);
                }


                //Falseamiento
                if (realPos == 5 || realPos == 10 || realPos == 15 || realPos == 20)
                {
                    int aux = Convert.ToInt32(listRespuesta[i].valor);

                    if (realPos == 5 && aux == 0)
                    {
                        falseamiento += 24;
                    }

                    if (realPos == 10 && aux == 4)
                    {
                        falseamiento += 23;
                    }

                    if (realPos == 15 && aux == 0)
                    {
                        falseamiento += 21;
                    }

                    if (realPos == 20 && aux == 0)
                    {
                        falseamiento += 24;
                    }

                }

            }

            if (autoInd >= 15 && autoInd <= 21)
            {
                autoIndEvaluacion = "Alta presencia";
            }
            if (autoInd >= 13 && autoInd <= 14)
            {
                autoIndEvaluacion = "Media presencia";
            }
            if (autoInd >= 0 && autoInd <= 12)
            {
                autoIndEvaluacion = "Baja presencia";
            }



            if (tenaReso >= 13 && tenaReso <= 20)
            {
                tenaResoEvaluacion = "Alta presencia";
            }
            if (tenaReso >= 10 && tenaReso <= 12)
            {
                tenaResoEvaluacion = "Media presencia";
            }
            if (tenaReso >= 0 && tenaReso <= 9)
            {
                tenaResoEvaluacion = "Baja presencia";
            }


            if (persePersi >= 19 && persePersi <= 24)
            {
                persePersiEvaluacion = "Alta presencia";
            }
            if (persePersi >= 16 && persePersi <= 18)
            {
                persePersiEvaluacion = "Media presencia";
            }
            if (persePersi >= 0 && persePersi <= 15)
            {
                persePersiEvaluacion = "Baja presencia";
            }



            if (autodoAutoc >= 16 && autodoAutoc <= 20)
            {
                autodoAutocEvaluacion = "Alta presencia";
            }
            if (autodoAutoc >= 13 && autodoAutoc <= 15)
            {
                autodoAutocEvaluacion = "Media presencia";
            }
            if (autodoAutoc >= 0 && autodoAutoc <= 12)
            {
                autodoAutocEvaluacion = "Baja presencia";
            }



            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {

                    CualiVolitivasDep pruR = new CualiVolitivasDep();

                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;
                    pruR.PtoPersePersis = persePersi.ToString();
                    pruR.PtoAutodAutocon = autodoAutoc.ToString();
                    pruR.PtoAutoIndepen = autoInd.ToString();
                    pruR.PtoTenacidadResol = tenaReso.ToString();

                    pruR.persePersis = persePersiEvaluacion;
                    pruR.autodAutocon = autodoAutocEvaluacion;
                    pruR.autoIndepen = autoIndEvaluacion;
                    pruR.tenacidadResol = tenaResoEvaluacion;

                    pruR.Falseamiento = falseamiento == 0 ? "No" : falseamiento.ToString() + "%";

                    entities.CualiVolitivasDep.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<CualiVolitivasDep>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PCualiVolitiv = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PCualiVolitiv == null)
                    {

                        CualiVolitivasDep pruR = new CualiVolitivasDep();

                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;
                        pruR.PtoPersePersis = persePersi.ToString();
                        pruR.PtoAutodAutocon = autodoAutoc.ToString();
                        pruR.PtoAutoIndepen = autoInd.ToString();
                        pruR.PtoTenacidadResol = tenaReso.ToString();

                        pruR.persePersis = persePersiEvaluacion;
                        pruR.autodAutocon = autoIndEvaluacion;
                        pruR.autoIndepen = autoIndEvaluacion;
                        pruR.tenacidadResol = tenaResoEvaluacion;

                        pruR.Falseamiento = falseamiento == 0 ? "No" : falseamiento.ToString() + " %";

                        entities.CualiVolitivasDep.Add(pruR);
                        entities.SaveChangesAsync();




                        var ultimo = entities.Set<CualiVolitivasDep>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PCualiVolitiv = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.CualiVolitivasDep.Where(f => f.idTest == sujetoEva.PCualiVolitiv).FirstOrDefault<CualiVolitivasDep>();



                        conect.DuraPru = totalTiempoTest;
                        conect.PtoPersePersis = persePersi.ToString();
                        conect.PtoAutodAutocon = autodoAutoc.ToString();
                        conect.PtoAutoIndepen = autoInd.ToString();
                        conect.PtoTenacidadResol = tenaReso.ToString();

                        conect.persePersis = persePersiEvaluacion;
                        conect.autodAutocon = autoIndEvaluacion;
                        conect.autoIndepen = autoIndEvaluacion;
                        conect.tenacidadResol = tenaResoEvaluacion;

                        conect.Falseamiento = falseamiento.ToString() == "0" ? "No" : "Si";

                        entities.SaveChangesAsync();

                    }
                }
            }

        }

        private void salvarCatell()
        {
            int factorLatQ3 = 0;
            int factorLatC = 0;
            int factorLatL = 0;
            int factorLatO = 0;
            int factorLatQ4 = 0;

            int factorManQ3 = 0;
            int factorManC = 0;
            int factorManL = 0;
            int factorManO = 0;
            int factorManQ4 = 0;

            int factorQ3Corregido = 0;
            int factorCCorregido = 0;
            int factorLCorregido = 0;
            int factorOCorregido = 0;
            int factorQ4Corregido = 0;



            int stenAnsiedad = 0;

            int LMQ3 = 0;
            int LMC = 0;
            int LMO = 0;
            int LMQ4 = 0;
            int LML = 0;
            int ansiedadTotal = 0;
            int cantPuntEdad = 0;

            int realPos = 0;
            //Anotaciones brutas Latente Manifiesta
            for (int i = 0; i < listRespuesta.Count; i++)
            {
                realPos = i + 1;
                if (realPos <= 20)
                {
                    //FactorLatente
                    //factor Q3
                    if (realPos == 1 || realPos == 21)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatQ3 = factorLatQ3 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ3 = factorLatQ3 + 1;
                        }

                    }
                    if (realPos == 2 || realPos == 22)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ3 = factorLatQ3 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatQ3 = factorLatQ3 + 2;
                        }
                    }
                    if (realPos == 3 || realPos == 23)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ3 = factorLatQ3 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatQ3 = factorLatQ3 + 2;
                        }
                    }
                    if (realPos == 4 || realPos == 24)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatQ3 = factorLatQ3 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ3 = factorLatQ3 + 1;
                        }
                    }

                    //Factor C
                    if (realPos == 5 || realPos == 25)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatC = factorLatC + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatC = factorLatC + 1;
                        }
                    }

                    if (realPos == 6 || realPos == 26)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatC = factorLatC + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatC = factorLatC + 2;
                        }
                    }
                    if (realPos == 7 || realPos == 27)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatC = factorLatC + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatC = factorLatC + 1;
                        }
                    }

                    //FactorAB L
                    if (realPos == 8 || realPos == 28)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatL = factorLatL + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatL = factorLatL + 1;
                        }
                    }
                    if (realPos == 9 || realPos == 29)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatL = factorLatL + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatL = factorLatL + 2;
                        }
                    }

                    //Factor O

                    if (realPos == 10 || realPos == 30)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatO = factorLatO + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatO = factorLatO + 2;
                        }
                    }
                    if (realPos == 11 || realPos == 31)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatO = factorLatO + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatO = factorLatO + 2;
                        }
                    }
                    if (realPos == 12 || realPos == 32)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatO = factorLatO + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatO = factorLatO + 1;
                        }
                    }
                    if (realPos == 13 || realPos == 33)

                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatO = factorLatO + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatO = factorLatO + 1;
                        }
                    }
                    if (realPos == 14 || realPos == 34)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatO = factorLatO + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatO = factorLatO + 2;
                        }
                    }
                    if (realPos == 15 || realPos == 35)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatO = factorLatO + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatO = factorLatO + 1;
                        }
                    }


                    //FactorQ4
                    if (realPos == 16 /*|| realPos == 36*/)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatQ4 = factorLatQ4 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ4 = factorLatQ4 + 1;
                        }

                    }
                    if (realPos == 17 /*|| realPos == 37*/)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ4 = factorLatQ4 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatQ4 = factorLatQ4 + 2;
                        }
                    }
                    if (realPos == 18 /*|| realPos == 38*/)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatQ4 = factorLatQ4 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ4 = factorLatQ4 + 1;
                        }
                    }
                    if (realPos == 19 || realPos == 39)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ4 = factorLatQ4 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorLatQ4 = factorLatQ4 + 2;
                        }
                    }
                    if (realPos == 20 || realPos == 40)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorLatQ4 = factorLatQ4 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorLatQ4 = factorLatQ4 + 1;
                        }
                    }
                }
                else
                {
                    //Factor Manifiesta
                    //factor Q3
                    if (realPos == 1 || realPos == 21)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManQ3 = factorManQ3 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ3 = factorManQ3 + 1;
                        }

                    }
                    if (realPos == 2 || realPos == 22)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ3 = factorManQ3 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManQ3 = factorManQ3 + 2;
                        }
                    }
                    if (realPos == 3 || realPos == 23)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ3 = factorManQ3 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManQ3 = factorManQ3 + 2;
                        }
                    }
                    if (realPos == 4 || realPos == 24)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManQ3 = factorManQ3 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ3 = factorManQ3 + 1;
                        }
                    }

                    //Factor C
                    if (realPos == 5 || realPos == 25)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManC = factorManC + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManC = factorManC + 1;
                        }
                    }
                    if (realPos == 6 || realPos == 26)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManC = factorManC + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManC = factorManC + 2;
                        }
                    }
                    if (realPos == 7 || realPos == 27)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManC = factorManC + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManC = factorManC + 1;
                        }
                    }

                    //Factor L
                    if (realPos == 8 || realPos == 28)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManL = factorManL + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManL = factorManL + 1;
                        }
                    }
                    if (realPos == 9 || realPos == 29)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManL = factorManL + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManL = factorManL + 2;
                        }
                    }

                    //Factor O

                    if (realPos == 10 || realPos == 30)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManO = factorManO + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManO = factorManO + 2;
                        }
                    }
                    if (realPos == 11 || realPos == 31)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManO = factorManO + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManO = factorManO + 2;
                        }
                    }
                    if (realPos == 12 || realPos == 32)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManO = factorManO + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManO = factorManO + 1;
                        }
                    }
                    if (realPos == 13 || realPos == 33)

                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManO = factorManO + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManO = factorManO + 1;
                        }
                    }
                    if (realPos == 14 || realPos == 34)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManO = factorManO + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManO = factorManO + 2;
                        }
                    }
                    if (realPos == 15 || realPos == 35)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManO = factorManO + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManO = factorManO + 1;
                        }
                    }


                    //FactorQ4
                    if (realPos == 16 || realPos == 36)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManQ4 = factorManQ4 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ4 = factorManQ4 + 1;
                        }

                    }
                    if (realPos == 17 || realPos == 37)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ4 = factorManQ4 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManQ4 = factorManQ4 + 2;
                        }
                    }
                    if (realPos == 18 || realPos == 38)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManQ4 = factorManQ4 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ4 = factorManQ4 + 1;
                        }
                    }
                    if (realPos == 19 || realPos == 39)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ4 = factorManQ4 + 1;
                        }
                        if (listRespuesta[i].valor == "3")
                        {
                            factorManQ4 = factorManQ4 + 2;
                        }
                    }
                    if (realPos == 20 || realPos == 40)
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            factorManQ4 = factorManQ4 + 2;
                        }
                        if (listRespuesta[i].valor == "2")
                        {
                            factorManQ4 = factorManQ4 + 1;
                        }
                    }
                }
            }


            LMQ3 = factorLatQ3 + factorManQ3;
            LMC = factorLatC + factorManC;
            LMO = factorLatO + factorManO;
            LMQ4 = factorLatQ4 + factorManQ4;
            LML = factorLatL + factorManL;



            //Primera correccion segun el genero  y la edad
            factorCCorregido = LMC;
            factorOCorregido = LMO;
            factorLCorregido = LML;
            factorQ3Corregido = LMQ3;
            factorQ4Corregido = LMQ4;


            if (sexo == "F")
            {
                factorCCorregido = LMC - 1;
                factorOCorregido = LMO - 1;
            }

            if (edad > 31)
            {
                cantPuntEdad = CalcularEdadPuntos(edad);// edad - 21;

                factorOCorregido += cantPuntEdad;
                factorQ4Corregido += cantPuntEdad;
            }




            //Ansidad total suma de todos los factores corregidos con la ed
            ansiedadTotal = factorLCorregido + factorQ3Corregido + factorQ4Corregido + factorCCorregido + factorOCorregido;

            //Correccion de la ansiedad total segun la edad

            if (edad >= 15 && edad <= 18)
                ansiedadTotal -= 1;
            if (edad >= 23 && edad <= 27)
                ansiedadTotal += 1;
            if (edad >= 28 && edad <= 32)
                ansiedadTotal += 2;
            if (edad >= 33 && edad <= 37)
                ansiedadTotal += 3;
            if (edad >= 38)
                ansiedadTotal += 4;

            //Segunda correccion puntuacion total

            if (sexo == "M")
            {
                if (ansiedadTotal >= 0 && ansiedadTotal <= 4)
                {
                    stenAnsiedad = 1;
                }
                if (ansiedadTotal >= 5 && ansiedadTotal <= 10)
                {
                    stenAnsiedad = 2;
                }
                if (ansiedadTotal >= 11 && ansiedadTotal <= 14)
                {
                    stenAnsiedad = 3;
                }
                if (ansiedadTotal >= 15 && ansiedadTotal <= 20)
                {
                    stenAnsiedad = 4;
                }
                if (ansiedadTotal >= 21 && ansiedadTotal <= 26)
                {
                    stenAnsiedad = 5;
                }
                if (ansiedadTotal >= 27 && ansiedadTotal <= 32)
                {
                    stenAnsiedad = 6;
                }
                if (ansiedadTotal >= 33 && ansiedadTotal <= 37)
                {
                    stenAnsiedad = 7;
                }
                if (ansiedadTotal >= 38 && ansiedadTotal <= 45)
                {
                    stenAnsiedad = 8;
                }
                if (ansiedadTotal >= 46 && ansiedadTotal <= 54)
                {
                    stenAnsiedad = 9;
                }
                if (ansiedadTotal >= 55 && ansiedadTotal <= 80)
                {
                    stenAnsiedad = 10;
                }
            }
            else
            {
                if (ansiedadTotal >= 0 && ansiedadTotal <= 7)
                {
                    stenAnsiedad = 1;
                }
                if (ansiedadTotal >= 8 && ansiedadTotal <= 13)
                {
                    stenAnsiedad = 2;
                }
                if (ansiedadTotal >= 14 && ansiedadTotal <= 17)
                {
                    stenAnsiedad = 3;
                }
                if (ansiedadTotal >= 18 && ansiedadTotal <= 23)
                {
                    stenAnsiedad = 4;
                }
                if (ansiedadTotal >= 24 && ansiedadTotal <= 29)
                {
                    stenAnsiedad = 5;
                }
                if (ansiedadTotal >= 30 && ansiedadTotal <= 35)
                {
                    stenAnsiedad = 6;
                }
                if (ansiedadTotal >= 36 && ansiedadTotal <= 40)
                {
                    stenAnsiedad = 7;
                }
                if (ansiedadTotal >= 41 && ansiedadTotal <= 48)
                {
                    stenAnsiedad = 8;
                }
                if (ansiedadTotal >= 49 && ansiedadTotal <= 58)
                {
                    stenAnsiedad = 9;
                }
                if (ansiedadTotal >= 59 && ansiedadTotal <= 80)
                {
                    stenAnsiedad = 10;
                }
            }



            String clasificacionAnsiedad = "";
            //clasificacion total segun la ansiedad

            if (stenAnsiedad == 3 || stenAnsiedad == 2)
            {
                clasificacionAnsiedad = "Posilmente perezosos y sub-motivados. Capaces de tolerar ocupaciones con muchas crisis recurrentes y tensiones";
            }
            if (stenAnsiedad == 4)
            {
                clasificacionAnsiedad = "Esencialmente normales en lo que la ansiedad se refiere";
            }
            if (stenAnsiedad == 7)
            {
                clasificacionAnsiedad = "Neurótico de ansiedad inferior";
            }
            if (stenAnsiedad == 8)
            {
                clasificacionAnsiedad = "Anotación neurótica promedio";
            }
            if (stenAnsiedad == 9)
            {
                clasificacionAnsiedad = "Ansiedad elevada. Necesidad de asesoramiento o psicoterapia";
            }
            if (stenAnsiedad == 10)
            {
                clasificacionAnsiedad = "Ansiedad muy elevada. Anotación neuróica ansiosa";
            }



            double stensQ3Latente = 0;
            double stensQ3Manifiesta = 0;

            double stensQ4Latente = 0;
            double stensQ4Manifiesta = 0;

            double stensOLatente = 0;
            double stensOManifiesta = 0;


            double stensCLatente = 0;
            double stensCManifiesta = 0;

            double stensLLatente = 0;
            double stensLManifiesta = 0;


            double stensCorregidoQ3 = 0;
            double stensCorregidoQ4 = 0;
            double stensCorregidoL = 0;
            double stensCorregidoC = 0;
            double stensCorregidoO = 0;
            //Conversion a stens 

            //Q3
            if (factorLatQ3 == 0)
            {
                stensQ3Latente = 1;
            }
            if (factorLatQ3 == 1)
            {
                stensQ3Latente = 2;
            }
            if (factorLatQ3 == 2 || factorLatQ3 == 3)
            {
                stensQ3Latente = 3;
            }
            if (factorLatQ3 == 4)
            {
                stensQ3Latente = 4;
            }
            if (factorLatQ3 == 5 || factorLatQ3 == 6)
            {
                stensQ3Latente = 5;
            }
            if (factorLatQ3 == 7)
            {
                stensQ3Latente = 6;
            }
            if (factorLatQ3 == 8 || factorLatQ3 == 9)
            {
                stensQ3Latente = 7;
            }
            if (factorLatQ3 == 10)
            {
                stensQ3Latente = 8;
            }
            if (factorLatQ3 == 11 || factorLatQ3 == 12)
            {
                stensQ3Latente = 9;
            }
            if (factorLatQ3 >= 13 || factorLatQ3 <= 16)
            {
                stensQ3Latente = 10;
            }
            if (factorManQ3 == 0)
            {
                stensQ3Manifiesta = 1;
            }
            if (factorManQ3 == 1)
            {
                stensQ3Manifiesta = 2;
            }
            if (factorManQ3 == 2 || factorManQ3 == 3)
            {
                stensQ3Manifiesta = 3;
            }
            if (factorManQ3 == 4)
            {
                stensQ3Manifiesta = 4;
            }
            if (factorManQ3 == 5 || factorManQ3 == 6)
            {
                stensQ3Manifiesta = 5;
            }
            if (factorManQ3 == 7)
            {
                stensQ3Manifiesta = 6;
            }
            if (factorManQ3 == 8 || factorManQ3 == 9)
            {
                stensQ3Manifiesta = 7;
            }
            if (factorManQ3 == 10)
            {
                stensQ3Manifiesta = 8;
            }
            if (factorManQ3 == 11 || factorManQ3 == 12)
            {
                stensQ3Manifiesta = 9;
            }
            if (factorManQ3 >= 13 || factorManQ3 <= 16)
            {
                stensQ3Manifiesta = 10;
            }

            //stens q4

            if (factorLatQ4 == 0)
            {
                stensQ4Latente = 1;
            }
            if (factorLatQ4 == 1 || factorLatQ4 == 2)
            {
                stensQ4Latente = 2;
            }
            if (factorLatQ4 == 3)
            {
                stensQ4Latente = 3;
            }
            if (factorLatQ4 == 4 || factorLatQ4 == 5)
            {
                stensQ4Latente = 4;
            }
            if (factorLatQ4 == 6 || factorLatQ4 == 7)
            {
                stensQ4Latente = 5;
            }
            if (factorLatQ4 == 8)
            {
                stensQ4Latente = 6;
            }
            if (factorLatQ4 == 9 || factorLatQ4 == 10)
            {
                stensQ4Latente = 7;
            }
            if (factorLatQ4 == 11 || factorLatQ4 == 12)
            {
                stensQ4Latente = 8;
            }
            if (factorLatQ4 == 13)
            {
                stensQ4Latente = 9;
            }
            if (factorLatQ4 >= 14 || factorLatQ4 <= 20)
            {
                stensQ4Latente = 10;
            }
            if (factorManQ4 == 0)
            {
                stensQ4Manifiesta = 1;
            }
            if (factorManQ4 == 1 || factorManQ4 == 2)
            {
                stensQ4Manifiesta = 2;
            }
            if (factorManQ4 == 3)
            {
                stensQ4Manifiesta = 3;
            }
            if (factorManQ4 == 4 || factorManQ4 == 5)
            {
                stensQ4Manifiesta = 4;
            }
            if (factorManQ4 == 6 || factorManQ4 == 7)
            {
                stensQ4Manifiesta = 5;
            }
            if (factorManQ4 == 8)
            {
                stensQ4Manifiesta = 6;
            }
            if (factorManQ4 == 9 || factorManQ4 == 10)
            {
                stensQ4Manifiesta = 7;
            }
            if (factorManQ4 == 11 || factorManQ4 == 12)
            {
                stensQ4Manifiesta = 8;
            }
            if (factorManQ4 == 13)
            {
                stensQ4Manifiesta = 9;
            }
            if (factorManQ4 >= 14 || factorManQ4 <= 20)
            {
                stensQ4Manifiesta = 10;
            }


            //stens L

            if (factorLatL == 0)
            {
                stensLLatente = 1;
            }
            if (factorLatL == 1)
            {
                stensLLatente = 3;
            }
            if (factorLatL == 2)
            {
                stensLLatente = 4;
            }
            if (factorLatL == 3)
            {
                stensLLatente = 5;
            }
            if (factorLatL == 4)
            {
                stensLLatente = 6;
            }
            if (factorLatL == 5)
            {
                stensLLatente = 7;
            }
            if (factorLatL == 6)
            {
                stensLLatente = 8;
            }
            if (factorLatL == 7)
            {
                stensLLatente = 9;
            }
            if (factorLatL >= 8 || factorLatL <= 12)
            {
                stensLLatente = 10;
            }
            if (factorManL == 0)
            {
                stensLManifiesta = 1;
            }
            if (factorManL == 1)
            {
                stensLManifiesta = 3;
            }
            if (factorManL == 2)
            {
                stensLManifiesta = 4;
            }
            if (factorManL == 3)
            {
                stensLManifiesta = 5;
            }
            if (factorManL == 4)
            {
                stensLManifiesta = 6;
            }
            if (factorManL == 5)
            {
                stensLManifiesta = 7;
            }
            if (factorManL == 6)
            {
                stensLManifiesta = 8;
            }
            if (factorManL == 7)
            {
                stensLManifiesta = 9;
            }
            if (factorManL >= 8 || factorManL <= 12)
            {
                stensLManifiesta = 10;
            }


            //Stens O

            if (factorLatO == 0)
            {
                stensOLatente = 0.5;
            }
            if (factorLatO == 1 || factorLatO == 2)
            {
                stensOLatente = 1;
            }
            if (factorLatO == 3)
            {
                stensOLatente = 2;
            }
            if (factorLatO == 4 || factorLatO == 5)
            {
                stensOLatente = 3;
            }
            if (factorLatO == 6 || factorLatO == 7)
            {
                stensOLatente = 4;
            }
            if (factorLatO == 8 || factorLatO == 9)
            {
                stensOLatente = 5;
            }
            if (factorLatO == 10 || factorLatO == 11)
            {
                stensOLatente = 6;
            }
            if (factorLatO == 12)
            {
                stensOLatente = 7;
            }
            if (factorLatO == 13 || factorLatO == 14)
            {
                stensOLatente = 8;
            }
            if (factorLatO == 15 || factorLatO == 16)
            {
                stensOLatente = 9;
            }
            if (factorManO == 0)
            {
                stensOManifiesta = 0.5;
            }
            if (factorManO == 1 || factorManO == 2)
            {
                stensOManifiesta = 1;
            }
            if (factorManO == 3)
            {
                stensOManifiesta = 2;
            }
            if (factorManO == 4 || factorManO == 5)
            {
                stensOManifiesta = 3;
            }
            if (factorManO == 6 || factorManO == 7)
            {
                stensOManifiesta = 4;
            }
            if (factorManO == 8 || factorManO == 9)
            {
                stensOManifiesta = 5;
            }
            if (factorManO == 10 || factorManO == 11)
            {
                stensOManifiesta = 6;
            }
            if (factorManO == 12)
            {
                stensOManifiesta = 7;
            }
            if (factorManO == 13 || factorManO == 14)
            {
                stensOManifiesta = 8;
            }
            if (factorManO == 15 || factorManO == 16)
            {
                stensOManifiesta = 9;
            }


            //Stens C

            if (factorLatC == 0)
            {
                stensCLatente = 1.5;
            }
            if (factorLatC == 1)
            {
                stensCLatente = 3;
            }
            if (factorLatC == 2)
            {
                stensCLatente = 4;
            }
            if (factorLatC == 3)
            {
                stensCLatente = 5;
            }
            if (factorLatC == 4)
            {
                stensCLatente = 6;
            }
            if (factorLatC == 5)
            {
                stensCLatente = 7;
            }
            if (factorLatC == 6)
            {
                stensCLatente = 8;
            }
            if (factorLatC == 7)
            {
                stensCLatente = 9;
            }
            if (factorLatC >= 8 && factorLatC <= 12)
            {
                stensCLatente = 8;
            }
            if (factorManC == 0)
            {
                stensCManifiesta = 1.5;
            }
            if (factorManC == 1)
            {
                stensCManifiesta = 3;
            }
            if (factorManC == 2)
            {
                stensCManifiesta = 4;
            }
            if (factorManC == 3)
            {
                stensCManifiesta = 5;
            }
            if (factorManC == 4)
            {
                stensCManifiesta = 6;
            }
            if (factorManC == 5)
            {
                stensCManifiesta = 7;
            }
            if (factorManC == 6)
            {
                stensCManifiesta = 8;
            }
            if (factorManC == 7)
            {
                stensCManifiesta = 9;
            }
            if (factorManC >= 8 && factorManC <= 12)
            {
                stensCManifiesta = 8;
            }




            //stens rectificados

            //Q3
            if (factorQ3Corregido == 0)
            {
                stensCorregidoQ3 = 1;
            }
            if (factorQ3Corregido == 1)
            {
                stensCorregidoQ3 = 2;
            }
            if (factorQ3Corregido == 2 || factorQ3Corregido == 3)
            {
                stensCorregidoQ3 = 3;
            }
            if (factorQ3Corregido == 4)
            {
                stensCorregidoQ3 = 4;
            }
            if (factorQ3Corregido == 5 || factorQ3Corregido == 6)
            {
                stensCorregidoQ3 = 5;
            }
            if (factorQ3Corregido == 7)
            {
                stensCorregidoQ3 = 6;
            }
            if (factorQ3Corregido == 8 || factorQ3Corregido == 9)
            {
                stensCorregidoQ3 = 7;
            }
            if (factorQ3Corregido == 10)
            {
                stensCorregidoQ3 = 8;
            }
            if (factorQ3Corregido == 11 || factorQ3Corregido == 12)
            {
                stensCorregidoQ3 = 9;
            }
            if (factorQ3Corregido >= 13 || factorQ3Corregido <= 16)
            {
                stensCorregidoQ3 = 10;
            }

            //Q4
            if (factorQ4Corregido == 0)
            {
                stensCorregidoQ4 = 1;
            }
            if (factorQ4Corregido == 1 || factorQ4Corregido == 2)
            {
                stensCorregidoQ4 = 2;
            }
            if (factorQ4Corregido == 3)
            {
                stensCorregidoQ4 = 3;
            }
            if (factorQ4Corregido == 4 || factorQ4Corregido == 5)
            {
                stensCorregidoQ4 = 4;
            }
            if (factorQ4Corregido == 6 || factorQ4Corregido == 7)
            {
                stensCorregidoQ4 = 5;
            }
            if (factorQ4Corregido == 8)
            {
                stensCorregidoQ4 = 6;
            }
            if (factorQ4Corregido == 9 || factorQ4Corregido == 10)
            {
                stensCorregidoQ4 = 7;
            }
            if (factorQ4Corregido == 11 || factorQ4Corregido == 12)
            {
                stensCorregidoQ4 = 8;
            }
            if (factorQ4Corregido == 13)
            {
                stensCorregidoQ4 = 9;
            }
            if (factorQ4Corregido >= 14 || factorQ4Corregido <= 20)
            {
                stensCorregidoQ4 = 10;
            }

            //L
            if (factorLCorregido == 0)
            {
                stensCorregidoL = 1;
            }
            if (factorLCorregido == 1)
            {
                stensCorregidoL = 3;
            }
            if (factorLCorregido == 2)
            {
                stensCorregidoL = 4;
            }
            if (factorLCorregido == 3)
            {
                stensCorregidoL = 5;
            }
            if (factorLCorregido == 4)
            {
                stensCorregidoL = 6;
            }
            if (factorLCorregido == 5)
            {
                stensCorregidoL = 7;
            }
            if (factorLCorregido == 6)
            {
                stensCorregidoL = 8;
            }
            if (factorLCorregido == 7)
            {
                stensCorregidoL = 9;
            }
            if (factorLCorregido >= 8 || factorLCorregido <= 12)
            {
                stensCorregidoL = 10;
            }


            //O
            if (factorOCorregido == 0)
            {
                stensCorregidoO = 0.5;
            }
            if (factorOCorregido == 1 || factorOCorregido == 2)
            {
                stensCorregidoO = 1;
            }
            if (factorOCorregido == 3)
            {
                stensCorregidoO = 2;
            }
            if (factorOCorregido == 4 || factorOCorregido == 5)
            {
                stensCorregidoO = 3;
            }
            if (factorOCorregido == 6 || factorOCorregido == 7)
            {
                stensCorregidoO = 4;
            }
            if (factorOCorregido == 8 || factorOCorregido == 9)
            {
                stensCorregidoO = 5;
            }
            if (factorOCorregido == 10 || factorOCorregido == 11)
            {
                stensCorregidoO = 6;
            }
            if (factorOCorregido == 12)
            {
                stensCorregidoO = 7;
            }
            if (factorOCorregido == 13 || factorOCorregido == 14)
            {
                stensCorregidoO = 8;
            }
            if (factorOCorregido == 15 || factorOCorregido == 16)
            {
                stensCorregidoO = 9;
            }



            //Stens C

            if (factorCCorregido == 0)
            {
                stensCorregidoC = 1.5;
            }
            if (factorCCorregido == 1)
            {
                stensCorregidoC = 3;
            }
            if (factorCCorregido == 2)
            {
                stensCorregidoC = 4;
            }
            if (factorCCorregido == 3)
            {
                stensCorregidoC = 5;
            }
            if (factorCCorregido == 4)
            {
                stensCorregidoC = 6;
            }
            if (factorCCorregido == 5)
            {
                stensCorregidoC = 7;
            }
            if (factorCCorregido == 6)
            {
                stensCorregidoC = 8;
            }
            if (factorCCorregido == 7)
            {
                stensCorregidoC = 9;
            }
            if (factorCCorregido >= 8 && factorCCorregido <= 12)
            {
                stensCorregidoC = 8;
            }



            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {





                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    PruCatell pruR = new PruCatell();

                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;

                    pruR.PBrutaLatC = factorLatC.ToString();
                    pruR.PBrutaLatL = factorLatL.ToString();
                    pruR.PBrutaLatO = factorLatO.ToString();
                    pruR.PBrutaLatQ3 = factorLatQ3.ToString();
                    pruR.PBrutaLatQ4 = factorLatQ4.ToString();

                    pruR.PBrutaManQ4 = factorManQ4.ToString();
                    pruR.PBrutaManC = factorManC.ToString();
                    pruR.PBrutaManL = factorManL.ToString();
                    pruR.PBrutaManO = factorManO.ToString();
                    pruR.PBrutaManQ3 = factorManQ3.ToString();

                    pruR.PBrutaLatManQ4 = factorQ4Corregido.ToString();
                    pruR.PBrutaLatManQ3 = factorQ3Corregido.ToString();
                    pruR.PBrutaLatManL = factorLCorregido.ToString();
                    pruR.PBrutaLatManO = factorOCorregido.ToString();
                    pruR.PBrutaLaManC = factorCCorregido.ToString();

                    pruR.PBrutaTotal = Convert.ToString(LMQ3 + LMC + LMO + LMQ4 + LML);

                    pruR.PStensLatC = stensCLatente.ToString();
                    pruR.PStensLatL = stensLLatente.ToString();
                    pruR.PStensLatO = stensOLatente.ToString();
                    pruR.PStensLatQ3 = stensQ3Latente.ToString();
                    pruR.PStensLatQ4 = stensQ4Latente.ToString();

                    pruR.PStensManC = stensCManifiesta.ToString();
                    pruR.PStensManL = stensLManifiesta.ToString();
                    pruR.PStensManO = stensOManifiesta.ToString();
                    pruR.PStensManQ3 = stensQ3Manifiesta.ToString();
                    pruR.PStensManQ4 = stensQ4Manifiesta.ToString();

                    pruR.PStensLatManQ3 = stensCorregidoQ3.ToString();
                    pruR.PStensLatManQ4 = stensCorregidoQ4.ToString();
                    pruR.PStensLatManO = stensCorregidoO.ToString();
                    pruR.PStensLatManL = stensCorregidoL.ToString();
                    pruR.PStensLatManC = stensCorregidoC.ToString();

                    pruR.IntSico = clasificacionAnsiedad;
                    pruR.PuntTotalLE = ansiedadTotal.ToString();
                    pruR.PStensTotal = stenAnsiedad.ToString();

                    entities.PruCatell.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruCatell>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PCatell = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PCatell == null)
                    {

                        PruCatell pruR = new PruCatell();

                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;

                        pruR.PBrutaLatC = factorLatC.ToString();
                        pruR.PBrutaLatL = factorLatL.ToString();
                        pruR.PBrutaLatO = factorLatO.ToString();
                        pruR.PBrutaLatQ3 = factorLatQ3.ToString();
                        pruR.PBrutaLatQ4 = factorLatQ4.ToString();

                        pruR.PBrutaManQ4 = factorManQ4.ToString();
                        pruR.PBrutaManC = factorManC.ToString();
                        pruR.PBrutaManL = factorManL.ToString();
                        pruR.PBrutaManO = factorManO.ToString();
                        pruR.PBrutaManQ3 = factorManQ3.ToString();

                        pruR.PBrutaLatManQ4 = factorQ4Corregido.ToString();
                        pruR.PBrutaLatManQ3 = factorQ3Corregido.ToString();
                        pruR.PBrutaLatManL = factorLCorregido.ToString();
                        pruR.PBrutaLatManO = factorOCorregido.ToString();
                        pruR.PBrutaLaManC = factorCCorregido.ToString();

                        pruR.PBrutaTotal = Convert.ToString(LMQ3 + LMC + LMO + LMQ4 + LML);

                        pruR.PStensLatC = stensCLatente.ToString();
                        pruR.PStensLatL = stensLLatente.ToString();
                        pruR.PStensLatO = stensOLatente.ToString();
                        pruR.PStensLatQ3 = stensQ3Latente.ToString();
                        pruR.PStensLatQ4 = stensQ4Latente.ToString();

                        pruR.PStensManC = stensCManifiesta.ToString();
                        pruR.PStensManL = stensLManifiesta.ToString();
                        pruR.PStensManO = stensOManifiesta.ToString();
                        pruR.PStensManQ3 = stensQ3Manifiesta.ToString();
                        pruR.PStensManQ4 = stensQ4Manifiesta.ToString();

                        pruR.PStensLatManQ3 = stensCorregidoQ3.ToString();
                        pruR.PStensLatManQ4 = stensCorregidoQ4.ToString();
                        pruR.PStensLatManO = stensCorregidoO.ToString();
                        pruR.PStensLatManL = stensCorregidoL.ToString();
                        pruR.PStensLatManC = stensCorregidoC.ToString();

                        pruR.IntSico = clasificacionAnsiedad;
                        pruR.PuntTotalLE = ansiedadTotal.ToString();
                        pruR.PStensTotal = stenAnsiedad.ToString();

                        entities.PruCatell.Add(pruR);
                        entities.SaveChangesAsync();


                        var ultimo = entities.Set<PruCatell>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PCatell = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruCatell.Where(f => f.idTest == sujetoEva.PCatell).FirstOrDefault<PruCatell>();

                        conect.PBrutaLatC = factorLatC.ToString();
                        conect.PBrutaLatL = factorLatL.ToString();
                        conect.PBrutaLatO = factorLatO.ToString();
                        conect.PBrutaLatQ3 = factorLatQ3.ToString();
                        conect.PBrutaLatQ4 = factorLatQ4.ToString();

                        conect.PBrutaManQ4 = factorManQ4.ToString();
                        conect.PBrutaManC = factorManC.ToString();
                        conect.PBrutaManL = factorManL.ToString();
                        conect.PBrutaManO = factorManO.ToString();
                        conect.PBrutaManQ3 = factorManQ3.ToString();

                        conect.PBrutaLatManQ4 = factorQ4Corregido.ToString();
                        conect.PBrutaLatManQ3 = factorQ3Corregido.ToString();
                        conect.PBrutaLatManL = factorLCorregido.ToString();
                        conect.PBrutaLatManO = factorOCorregido.ToString();
                        conect.PBrutaLaManC = factorCCorregido.ToString();

                        conect.PBrutaTotal = Convert.ToString(LMQ3 + LMC + LMO + LMQ4 + LML);

                        conect.PStensLatC = stensCLatente.ToString();
                        conect.PStensLatL = stensLLatente.ToString();
                        conect.PStensLatO = stensOLatente.ToString();
                        conect.PStensLatQ3 = stensQ3Latente.ToString();
                        conect.PStensLatQ4 = stensQ4Latente.ToString();

                        conect.PStensManC = stensCManifiesta.ToString();
                        conect.PStensManL = stensLManifiesta.ToString();
                        conect.PStensManO = stensOManifiesta.ToString();
                        conect.PStensManQ3 = stensQ3Manifiesta.ToString();
                        conect.PStensManQ4 = stensQ4Manifiesta.ToString();


                        conect.PStensLatManQ3 = stensCorregidoQ3.ToString();
                        conect.PStensLatManQ4 = stensCorregidoQ4.ToString();
                        conect.PStensLatManO = stensCorregidoO.ToString();
                        conect.PStensLatManL = stensCorregidoL.ToString();
                        conect.PStensLatManC = stensCorregidoC.ToString();


                        conect.IntSico = clasificacionAnsiedad;
                        conect.PuntTotalLE = ansiedadTotal.ToString();
                        conect.PStensTotal = stenAnsiedad.ToString();


                        entities.SaveChangesAsync();

                    }
                }
            }

        }

        private int CalcularEdadPuntos(int edad)
        {
            int val = 0;
            if (edad < 41)
            {
                val = 1;
            }

            if (edad < 51)
            {
                val = 2;
            }
            if (edad < 61)
            {
                val = 3;
            }
            if (edad < 71)
            {
                val = 4;
            }
            if (edad < 81)
            {
                val = 5;
            }
            if (edad < 91)
            {
                val = 6;
            }
            if (edad < 101)
            {
                val = 7;
            }
            return val;
        }

        private void salvarIdetem()
        {
            int sanguineo = 0;
            int colerico = 0;
            int flematico = 0;
            int melancolico = 0;

            double Sporciento = 0;
            double Cporciento = 0;
            double Fporciento = 0;
            double Mporciento = 0;

            int equilibrio = 0;
            int deseQuilExita = 0;
            int deseQuilInibicion = 0;
            int fuerza = 0;
            int debilidad = 0;
            int movilidad = 0;
            int inercia = 0;
            int dinamiPsi = 0;
            int pocoDinamiPsi = 0;
            int labilidad = 0;

            int actividad = 0;
            int reacModer = 0;
            int reacAlta = 0;
            int resisAlta = 0;
            int resisBaja = 0;
            int ritmPsiRapido = 0;
            int ritmPsiLento = 0;
            int sensibilidad = 0;
            int pocaSensibi = 0;
            int extroversion = 0;
            int introversion = 0;
            int plazticidad = 0;
            int rigidez = 0;



            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;
                if (realPos == 1 || realPos == 5 || realPos == 9 || realPos == 13 || realPos == 17 || realPos == 21 || realPos == 25 || realPos == 29 || realPos == 33 || realPos == 37 || realPos == 41 || realPos == 45 || realPos == 49 || realPos == 53)
                {
                    sanguineo += Convert.ToInt32(listRespuesta[i].valor);

                }

                if (realPos == 2 || realPos == 6 || realPos == 10 || realPos == 14 || realPos == 18 || realPos == 22 || realPos == 26 || realPos == 30 || realPos == 34 || realPos == 38 || realPos == 42 || realPos == 46 || realPos == 50 || realPos == 54)
                {
                    colerico += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 3 || realPos == 7 || realPos == 11 || realPos == 15 || realPos == 19 || realPos == 23 || realPos == 27 || realPos == 31 || realPos == 35 || realPos == 39 || realPos == 43 || realPos == 47 || realPos == 51 || realPos == 55)
                {
                    flematico += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 4 || realPos == 8 || realPos == 12 || realPos == 16 || realPos == 20 || realPos == 24 || realPos == 28 || realPos == 32 || realPos == 36 || realPos == 40 || realPos == 44 || realPos == 48 || realPos == 52 || realPos == 56)
                {
                    melancolico += Convert.ToInt32(listRespuesta[i].valor);
                }




                int valor = Convert.ToInt32(listRespuesta[i].valor);

                if ((realPos == 1 || realPos == 15 || realPos == 17 || realPos == 19 || realPos == 23 || realPos == 25 || realPos == 27 || realPos == 29 || realPos == 31 || realPos == 33 || realPos == 37 || realPos == 43 || realPos == 45 || realPos == 47 || realPos == 49 || realPos == 51) && (valor == 5 || valor == 4))
                {
                    equilibrio++;
                }

                if ((realPos == 2 || realPos == 10 || realPos == 14 || realPos == 18 || realPos == 20 || realPos == 22 || realPos == 34 || realPos == 36 || realPos == 38 || realPos == 40 || realPos == 42 || realPos == 48 || realPos == 50) && (valor == 5 || valor == 4))
                {
                    deseQuilExita++;
                }

                if ((realPos == 20 || realPos == 24 || realPos == 32 || realPos == 36 || realPos == 38 || realPos == 40 || realPos == 44 || realPos == 48 || realPos == 50) && (valor == 5 || valor == 4))
                {
                    deseQuilInibicion++;
                }

                if ((realPos == 5 || realPos == 17 || realPos == 19 || realPos == 25 || realPos == 27 || realPos == 31 || realPos == 43 || realPos == 49 || realPos == 55) && (valor == 5 || valor == 4))
                {
                    fuerza++;
                }

                if ((realPos == 4 || realPos == 10 || realPos == 24 || realPos == 26 || realPos == 32 || realPos == 36 || realPos == 40 || realPos == 44 || realPos == 50) && (valor == 5 || valor == 4))
                {
                    debilidad++;
                }

                if ((realPos == 1 || realPos == 5 || realPos == 6 || realPos == 9 || realPos == 13 || realPos == 15 || realPos == 25 || realPos == 29 || realPos == 37 || realPos == 41 || realPos == 42 || realPos == 46 || realPos == 49 || realPos == 53) && (valor == 5 || valor == 4))
                {
                    movilidad++;
                }

                if ((realPos == 3 || realPos == 24 || realPos == 27 || realPos == 35 || realPos == 39 || realPos == 44 || realPos == 47 || realPos == 48 || realPos == 50 || realPos == 56) && (valor == 5 || valor == 4))
                {
                    inercia++;
                }

                if ((realPos == 3 || realPos == 35 || realPos == 44) && (valor == 5 || valor == 4))
                {
                    pocoDinamiPsi++;
                }

                if ((realPos == 1 || realPos == 9 || realPos == 30 || realPos == 37 || realPos == 53) && (valor == 5 || valor == 4))
                {
                    labilidad++;
                }






                if ((realPos == 1 || realPos == 5 || realPos == 15 || realPos == 19 || realPos == 31 || realPos == 41 || realPos == 42 || realPos == 45) && (valor == 5 || valor == 4))
                {
                    actividad++;
                }

                if ((realPos == 2 || realPos == 5 || realPos == 12 || realPos == 15 || realPos == 25 || realPos == 30 || realPos == 42 || realPos == 43 || realPos == 45) && (valor == 5 || valor == 4))
                {
                    reacModer++;
                }

                if ((realPos == 10 || realPos == 14 || realPos == 20 || realPos == 22 || realPos == 24 || realPos == 34 || realPos == 36 || realPos == 43 || realPos == 48 || realPos == 50) && (valor == 5 || valor == 4))
                {
                    reacAlta++;
                }

                if ((realPos == 15 || realPos == 19 || realPos == 23 || realPos == 25 || realPos == 27 || realPos == 31 || realPos == 37 || realPos == 43 || realPos == 49 || realPos == 55) && (valor == 5 || valor == 4))
                {
                    resisAlta++;
                }

                if ((realPos == 24 || realPos == 26 || realPos == 32 || realPos == 38 || realPos == 40 || realPos == 44) && (valor == 5 || valor == 4))
                {
                    resisBaja++;
                }

                if ((realPos == 9 || realPos == 13 || realPos == 37 || realPos == 53 || realPos == 41) && (valor == 5 || valor == 4))
                {
                    ritmPsiRapido++;
                }

                if ((realPos == 3 || realPos == 35 || realPos == 39 || realPos == 51) && (valor == 5 || valor == 4))
                {
                    ritmPsiLento++;
                }

                if ((realPos == 19) && (valor == 5 || valor == 4))
                {
                    pocaSensibi++;
                }

                if ((realPos == 19) && (valor == 5 || valor == 4))
                {
                    pocaSensibi++;
                }

                if ((realPos == 5 || realPos == 21 || realPos == 29 || realPos == 30 || realPos == 41 || realPos == 45) && (valor == 5 || valor == 4))
                {
                    extroversion++;
                }

                if ((realPos == 16 || realPos == 28 || realPos == 47 || realPos == 52) && (valor == 5 || valor == 4))
                {
                    introversion++;
                }

                if ((realPos == 6 || realPos == 9 || realPos == 17 || realPos == 29 || realPos == 37 || realPos == 41 || realPos == 45 || realPos == 47 || realPos == 49 || realPos == 53) && (valor == 5 || valor == 4))
                {
                    plazticidad++;
                }

                if ((realPos == 7 || realPos == 11 || realPos == 18 || realPos == 54 || realPos == 56) && (valor == 5 || valor == 4))
                {
                    rigidez++;
                }


            }



            Sporciento = Math.Round(sanguineo / 70.0, 2) * 100;
            Cporciento = Math.Round(colerico / 70.0, 2) * 100;
            Fporciento = Math.Round(flematico / 70.0, 2) * 100;
            Mporciento = Math.Round(melancolico / 70.0, 2) * 100;




            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {


                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {

                    Idetem pruR = new Idetem();

                    pruR.Fecha = date;
                    pruR.durPrub = totalTiempoTest;
                    pruR.sanguineo = sanguineo.ToString();
                    pruR.colerico = colerico.ToString();
                    pruR.flematico = flematico.ToString();
                    pruR.melancolico = melancolico.ToString();

                    pruR.porcientoSanguineo = Sporciento.ToString();
                    pruR.porcientoColerico = Cporciento.ToString();
                    pruR.porcientoMelancolico = Mporciento.ToString();
                    pruR.porcientoFlematico = Fporciento.ToString();

                    pruR.equilibrio = equilibrio.ToString();
                    pruR.deseqExita = deseQuilExita.ToString();
                    pruR.deseqInhibi = deseQuilInibicion.ToString();
                    pruR.fuerza = fuerza.ToString();
                    pruR.debilidad = debilidad.ToString();
                    pruR.movilidad = movilidad.ToString();
                    pruR.inercia = inercia.ToString();
                    pruR.dinamPsiq = dinamiPsi.ToString();
                    pruR.pocoDinaPsiq = pocoDinamiPsi.ToString();
                    pruR.labilidad = labilidad.ToString();


                    pruR.actividad = actividad.ToString();
                    pruR.reactModer = reacModer.ToString();
                    pruR.reactAlta = reacAlta.ToString();
                    pruR.resisAlta = resisAlta.ToString();
                    pruR.resisBaja = resisBaja.ToString();
                    pruR.ritmPsiRap = ritmPsiRapido.ToString();
                    pruR.ritmPsiLent = ritmPsiLento.ToString();
                    pruR.sensibilidad = sensibilidad.ToString();
                    pruR.pocaSensi = pocaSensibi.ToString();
                    pruR.extroversion = extroversion.ToString();
                    pruR.introversion = introversion.ToString();
                    pruR.plasticidad = plazticidad.ToString();
                    pruR.rigidez = rigidez.ToString();


                    entities.Idetem.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<Idetem>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PIdetem = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PIdetem == null)
                    {

                        Idetem pruR = new Idetem();

                        pruR.Fecha = date;
                        pruR.durPrub = totalTiempoTest;
                        pruR.sanguineo = sanguineo.ToString();
                        pruR.colerico = colerico.ToString();
                        pruR.flematico = flematico.ToString();
                        pruR.melancolico = melancolico.ToString();

                        pruR.porcientoSanguineo = Sporciento.ToString();
                        pruR.porcientoColerico = Cporciento.ToString();
                        pruR.porcientoMelancolico = Mporciento.ToString();
                        pruR.porcientoFlematico = Fporciento.ToString();

                        pruR.equilibrio = equilibrio.ToString();
                        pruR.deseqExita = deseQuilExita.ToString();
                        pruR.deseqInhibi = deseQuilInibicion.ToString();
                        pruR.fuerza = fuerza.ToString();
                        pruR.debilidad = debilidad.ToString();
                        pruR.movilidad = movilidad.ToString();
                        pruR.inercia = inercia.ToString();
                        pruR.dinamPsiq = dinamiPsi.ToString();
                        pruR.pocoDinaPsiq = pocoDinamiPsi.ToString();
                        pruR.labilidad = labilidad.ToString();


                        pruR.actividad = actividad.ToString();
                        pruR.reactModer = reacModer.ToString();
                        pruR.reactAlta = reacAlta.ToString();
                        pruR.resisAlta = resisAlta.ToString();
                        pruR.resisBaja = resisBaja.ToString();
                        pruR.ritmPsiRap = ritmPsiRapido.ToString();
                        pruR.ritmPsiLent = ritmPsiLento.ToString();
                        pruR.sensibilidad = sensibilidad.ToString();
                        pruR.pocaSensi = pocaSensibi.ToString();
                        pruR.extroversion = extroversion.ToString();
                        pruR.introversion = introversion.ToString();
                        pruR.plasticidad = plazticidad.ToString();
                        pruR.rigidez = rigidez.ToString();


                        entities.Idetem.Add(pruR);
                        entities.SaveChangesAsync();



                        var ultimo = entities.Set<Idetem>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PIdetem = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.Idetem.Where(f => f.idTest == sujetoEva.PIdetem).FirstOrDefault<Idetem>();


                        conect.durPrub = totalTiempoTest;
                        conect.sanguineo = sanguineo.ToString();
                        conect.colerico = colerico.ToString();
                        conect.flematico = flematico.ToString();
                        conect.melancolico = melancolico.ToString();

                        conect.porcientoSanguineo = Sporciento.ToString();
                        conect.porcientoColerico = Cporciento.ToString();
                        conect.porcientoMelancolico = Mporciento.ToString();
                        conect.porcientoFlematico = Fporciento.ToString();

                        conect.equilibrio = equilibrio.ToString();
                        conect.deseqExita = deseQuilExita.ToString();
                        conect.deseqInhibi = deseQuilInibicion.ToString();
                        conect.fuerza = fuerza.ToString();
                        conect.debilidad = debilidad.ToString();
                        conect.movilidad = movilidad.ToString();
                        conect.inercia = inercia.ToString();
                        conect.dinamPsiq = dinamiPsi.ToString();
                        conect.pocoDinaPsiq = pocoDinamiPsi.ToString();
                        conect.labilidad = labilidad.ToString();


                        conect.actividad = actividad.ToString();
                        conect.reactModer = reacModer.ToString();
                        conect.reactAlta = reacAlta.ToString();
                        conect.resisAlta = resisAlta.ToString();
                        conect.resisBaja = resisBaja.ToString();
                        conect.ritmPsiRap = ritmPsiRapido.ToString();
                        conect.ritmPsiLent = ritmPsiLento.ToString();
                        conect.sensibilidad = sensibilidad.ToString();
                        conect.pocaSensi = pocaSensibi.ToString();
                        conect.extroversion = extroversion.ToString();
                        conect.introversion = introversion.ToString();
                        conect.plasticidad = plazticidad.ToString();
                        conect.rigidez = rigidez.ToString();


                        entities.SaveChangesAsync();

                    }
                }
            }


        }

        private void salvarEysenck()
        {
            int neuroticismo = 0;
            int extroversion = 0;
            int sinceridad = 0;
            string diagNeuro = "";
            string diagExtro = "";

            string diagLetra = "";
            string diagCuadrante = "";

            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if ((realPos == 2 || realPos == 4 || realPos == 7 || realPos == 9 || realPos == 11 || realPos == 14
                    || realPos == 16 || realPos == 19 || realPos == 21 || realPos == 23 || realPos == 26 || realPos == 28
                    || realPos == 31 || realPos == 33 || realPos == 35 || realPos == 38 || realPos == 40 || realPos == 43
                    || realPos == 45 || realPos == 47 || realPos == 50 || realPos == 52 || realPos == 55 || realPos == 57) && listRespuesta[i].valor == "1")
                {
                    neuroticismo++;
                }


                if ((realPos == 1 || realPos == 8 || realPos == 10 || realPos == 13 || realPos == 17 || realPos == 20
                    || realPos == 25 || realPos == 27 || realPos == 29 || realPos == 32 || realPos == 34 || realPos == 37
                    || realPos == 41 || realPos == 44 || realPos == 46 || realPos == 49 || realPos == 53) && listRespuesta[i].valor == "1")
                {
                    extroversion++;
                }


                if ((realPos == 3 || realPos == 5 || realPos == 15 || realPos == 22 || realPos == 39 || realPos == 51
                  || realPos == 56) && listRespuesta[i].valor == "2")
                {
                    extroversion++;
                }


                if ((realPos == 18 || realPos == 24 || realPos == 36) && listRespuesta[i].valor == "1")
                {
                    sinceridad++;
                }

                if ((realPos == 6 || realPos == 12 || realPos == 30 || realPos == 42 || realPos == 48 || realPos == 54) && listRespuesta[i].valor == "2")
                {
                    sinceridad++;
                }



            }

            if (neuroticismo > 10.5)
            {
                diagNeuro = "Neurótico";
            }
            else
            {
                diagNeuro = "Estable";
            }

            if (extroversion > 14.2)
            {
                diagExtro = "Extrovertido";
            }
            else
            {
                diagExtro = "Introvertido";
            }


            if (neuroticismo > 15.1 && neuroticismo < 26 && extroversion > 2 && extroversion < 6.4)
            {
                diagLetra = "Reacción Ansiosa-Depresiva";
            }

            if (neuroticismo > 15.1 && neuroticismo < 26 && extroversion > 6.4 && extroversion < 10.3)
            {
                diagLetra = "Reacción de Ansiedad";
            }

            if (neuroticismo > 15.1 && neuroticismo < 26 && extroversion > 10.3 && extroversion < 14.2)
            {
                diagLetra = "Reacción de conversión o hipocondría";
            }

            if (neuroticismo > 15.1 && neuroticismo < 26 && extroversion > 14.2 && extroversion < 18.1)
            {
                diagLetra = "Histerias disociativas y Psicopatías";
            }

            if (neuroticismo > 10.5 && neuroticismo < 15.1 && extroversion > 2 && extroversion < 6.4)
            {
                diagLetra = "Estados depresivos";
            }


            if (neuroticismo >= 10 && neuroticismo <= 26 && extroversion >= 2 && extroversion <= 14)
            {
                diagCuadrante = "Melancólico";
            }


            if (neuroticismo >= 10 && neuroticismo <= 26 && extroversion >= 14 && extroversion <= 26)
            {
                diagCuadrante = "Colérico";
            }



            if (neuroticismo <= 10 && neuroticismo >= 2 && extroversion >= 2 && extroversion <= 14)
            {
                diagCuadrante = "Flemático";
            }


            if (neuroticismo <= 10 && neuroticismo >= 2 && extroversion >= 14 && extroversion <= 26)
            {
                diagCuadrante = "Sanguíneo";
            }

            if (diagCuadrante == "")
                diagCuadrante = "No determinado";

            if (diagLetra == "")
                diagLetra = "Ninguno";


            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    PruEysenk pruR = new PruEysenk();

                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;
                    pruR.Neuroticismo = neuroticismo.ToString();
                    pruR.Extroversion = extroversion.ToString();
                    pruR.Sinceridad = sinceridad.ToString();
                    pruR.DiagnosticoLetra = diagLetra.ToString();
                    pruR.DiagCuadrante = diagCuadrante.ToString();
                    pruR.DiagNeurotic = diagNeuro;
                    pruR.DiagExtrove = diagExtro;

                    entities.PruEysenk.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruEysenk>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PEysenk = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PEysenk == null)
                    {

                        PruEysenk pruR = new PruEysenk();

                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;
                        pruR.Neuroticismo = neuroticismo.ToString();
                        pruR.Extroversion = extroversion.ToString();
                        pruR.Sinceridad = sinceridad.ToString();
                        pruR.DiagnosticoLetra = diagLetra.ToString();
                        pruR.DiagCuadrante = diagCuadrante.ToString();
                        pruR.DiagNeurotic = diagNeuro;
                        pruR.DiagExtrove = diagExtro;

                        entities.PruEysenk.Add(pruR);
                        entities.SaveChangesAsync();

                        var ultimo = entities.Set<PruEysenk>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PEysenk = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruEysenk.Where(f => f.idTest == sujetoEva.PEysenk).FirstOrDefault<PruEysenk>();


                        conect.DuraPru = totalTiempoTest;
                        conect.Neuroticismo = neuroticismo.ToString();
                        conect.Extroversion = extroversion.ToString();
                        conect.Sinceridad = sinceridad.ToString();
                        conect.DiagnosticoLetra = diagLetra.ToString();
                        conect.DiagCuadrante = diagCuadrante.ToString();
                        conect.DiagNeurotic = diagNeuro;
                        conect.DiagExtrove = diagExtro;

                        entities.SaveChangesAsync();

                    }
                }
            }

        }



        private void salvarIPED()
        {
            int autoConfianza = 0;
            int contAfrontNega = 0;
            int contAten = 0;
            int contVisuo = 0;
            int nivelMot = 0;
            int contAfroPos = 0;
            int contActitud = 0;

            String calAutoConfianza = "";
            String calContAfrontNega = "";
            String calContAten = "";
            String calContVisuo = "";
            String calNivelMot = "";
            String calContAfroPos = "";
            String calContActitud = "";

            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if (realPos == 1 || realPos == 8 || realPos == 15 || realPos == 22 || realPos == 29 || realPos == 36)
                {
                    autoConfianza += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 2 || realPos == 9 || realPos == 16 || realPos == 23 || realPos == 30 || realPos == 37)
                {
                    contAfrontNega += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 3 || realPos == 10 || realPos == 17 || realPos == 24 || realPos == 31 || realPos == 38)
                {
                    contAten += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 4 || realPos == 11 || realPos == 18 || realPos == 25 || realPos == 32 || realPos == 39)
                {
                    contVisuo += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 5 || realPos == 12 || realPos == 19 || realPos == 26 || realPos == 33 || realPos == 40)
                {
                    nivelMot += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 6 || realPos == 13 || realPos == 20 || realPos == 27 || realPos == 34 || realPos == 41)
                {
                    contAfroPos += Convert.ToInt32(listRespuesta[i].valor);
                }

                if (realPos == 7 || realPos == 14 || realPos == 21 || realPos == 28 || realPos == 35 || realPos == 42)
                {
                    contActitud += Convert.ToInt32(listRespuesta[i].valor);
                }


            }

            int pFinal = autoConfianza + contAfrontNega + contAten + contVisuo + nivelMot + contAfroPos + contActitud;

            string claFinal = pFinal >= 26 && pFinal <= 30 ? "Alta" : "";
            claFinal = pFinal >= 20 && pFinal <= 25 ? "Media" : claFinal;
            claFinal = pFinal >= 16 && pFinal <= 19 ? "Baja" : claFinal;



            if (autoConfianza >= 0 && autoConfianza <= 19)
            {
                calAutoConfianza = "Baja";
            }

            if (autoConfianza >= 20 && autoConfianza <= 25)
            {
                calAutoConfianza = "Media";
            }

            if (autoConfianza >= 26 && autoConfianza <= 30)
            {
                calAutoConfianza = "Alta";
            }



            if (contAfrontNega >= 0 && contAfrontNega <= 19)
            {
                calContAfrontNega = "Baja";
            }

            if (contAfrontNega >= 20 && contAfrontNega <= 25)
            {
                calContAfrontNega = "Media";
            }

            if (contAfrontNega >= 26 && contAfrontNega <= 30)
            {
                calContAfrontNega = "Alta";
            }




            if (contAten >= 0 && contAten <= 19)
            {
                calContAten = "Baja";
            }

            if (contAten >= 20 && contAten <= 25)
            {
                calContAten = "Media";
            }

            if (contAten >= 26 && contAten <= 30)
            {
                calContAten = "Alta";
            }




            if (contVisuo >= 0 && contVisuo <= 19)
            {
                calContVisuo = "Baja";
            }

            if (contVisuo >= 20 && contVisuo <= 25)
            {
                calContVisuo = "Media";
            }

            if (contVisuo >= 26 && contVisuo <= 30)
            {
                calContVisuo = "Alta";
            }






            if (nivelMot >= 0 && nivelMot <= 19)
            {
                calNivelMot = "Baja";
            }

            if (nivelMot >= 20 && nivelMot <= 25)
            {
                calNivelMot = "Media";
            }

            if (nivelMot >= 26 && nivelMot <= 30)
            {
                calNivelMot = "Alta";
            }




            if (contAfroPos >= 0 && contAfroPos <= 19)
            {
                calContAfroPos = "Baja";
            }

            if (contAfroPos >= 20 && contAfroPos <= 25)
            {
                calContAfroPos = "Media";
            }

            if (contAfroPos >= 26 && contAfroPos <= 30)
            {
                calContAfroPos = "Alta";
            }





            if (contActitud >= 0 && contActitud <= 19)
            {
                calContActitud = "Baja";
            }

            if (contActitud >= 20 && contActitud <= 25)
            {
                calContActitud = "Media";
            }

            if (contActitud >= 26 && contActitud <= 30)
            {
                calContActitud = "Alta";
            }




            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    Iped pruR = new Iped();

                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;
                    pruR.ContVisuoimag = contVisuo.ToString();
                    pruR.Autoconfianza = autoConfianza.ToString();
                    pruR.ContActitudinal = contActitud.ToString();
                    pruR.ContAfronNegativ = contAfrontNega.ToString();
                    pruR.ContAfrontPositiv = contAfroPos.ToString();
                    pruR.ContAtencional = contAten.ToString();
                    pruR.NivelMotiv = nivelMot.ToString();
                    pruR.calFinal = claFinal;

                    pruR.calAutoConfianza = calAutoConfianza;
                    pruR.calContActitud = calContActitud;
                    pruR.calContAfrontNega = calContAfrontNega;
                    pruR.calContAfroPos = calContAfroPos;
                    pruR.calContAten = calContAten;
                    pruR.calContVisuo = calContVisuo;
                    pruR.calNivelMot = calNivelMot;



                    entities.Iped.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<Iped>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PIped = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PIped == null)
                    {
                        Iped pruR = new Iped();

                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;
                        pruR.ContVisuoimag = contVisuo.ToString();
                        pruR.Autoconfianza = autoConfianza.ToString();
                        pruR.ContActitudinal = contActitud.ToString();
                        pruR.ContAfronNegativ = contAfrontNega.ToString();
                        pruR.ContAfrontPositiv = contAfroPos.ToString();
                        pruR.ContAtencional = contAten.ToString();
                        pruR.NivelMotiv = nivelMot.ToString();
                        pruR.calFinal = claFinal;


                        pruR.calAutoConfianza = calAutoConfianza;
                        pruR.calContActitud = calContActitud;
                        pruR.calContAfrontNega = calContAfrontNega;
                        pruR.calContAfroPos = calContAfroPos;
                        pruR.calContAten = calContAten;
                        pruR.calContVisuo = calContVisuo;
                        pruR.calNivelMot = calNivelMot;


                        entities.Iped.Add(pruR);
                        entities.SaveChangesAsync();

                        var ultimo = entities.Set<Iped>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PIped = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.Iped.Where(f => f.idTest == sujetoEva.PIped).FirstOrDefault<Iped>();


                        conect.Fecha = date;
                        conect.DuraPru = totalTiempoTest;
                        conect.ContVisuoimag = contVisuo.ToString();
                        conect.Autoconfianza = autoConfianza.ToString();
                        conect.ContActitudinal = contActitud.ToString();
                        conect.ContAfronNegativ = contAfrontNega.ToString();
                        conect.ContAfrontPositiv = contAfroPos.ToString();
                        conect.ContAtencional = contAten.ToString();
                        conect.NivelMotiv = nivelMot.ToString();
                        conect.calFinal = claFinal;


                        conect.calAutoConfianza = calAutoConfianza;
                        conect.calContActitud = calContActitud;
                        conect.calContAfrontNega = calContAfrontNega;
                        conect.calContAfroPos = calContAfroPos;
                        conect.calContAten = calContAten;
                        conect.calContVisuo = calContVisuo;
                        conect.calNivelMot = calNivelMot;

                        entities.SaveChangesAsync();

                    }
                }
            }
        }

        private void salvarButt()
        {
            int conflictivo = 0;
            int rivalidad = 0;
            int suficiencia = 0;
            int cooperacion = 0;
            int agresividad = 0;

            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if ((realPos == 1 || realPos == 6 || realPos == 12 || realPos == 14 || realPos == 17) && listRespuesta[i].valor == "1")
                {
                    conflictivo++;
                }

                if ((realPos == 2 || realPos == 7 || realPos == 10 || realPos == 20 || realPos == 24) && listRespuesta[i].valor == "1")
                {
                    rivalidad++;
                }

                if ((realPos == 3 || realPos == 11 || realPos == 16 || realPos == 19 || realPos == 23) && listRespuesta[i].valor == "1")
                {
                    suficiencia++;
                }

                if ((realPos == 4 || realPos == 8 || realPos == 15 || realPos == 21 || realPos == 25) && listRespuesta[i].valor == "1")
                {
                    cooperacion++;
                }

                if ((realPos == 5 || realPos == 9 || realPos == 13 || realPos == 18 || realPos == 22) && listRespuesta[i].valor == "1")
                {
                    agresividad++;
                }

            }



            string conflictoS = conflictivo >= 2 ? conflictivo + "ptos (Significativo)" : conflictivo + "pto (Normal)";
            string rivalidadS = rivalidad >= 3 ? rivalidad + "ptos (Significativo)" : rivalidad + "ptos (No Significativo)";
            string suficienciaS = suficiencia >= 3 ? suficiencia + "ptos (Significativo)" : suficiencia + "ptos (No Significativo)";
            string agresividadS = agresividad >= 3 ? agresividad + "ptos (Significativo)" : agresividad + "ptos (No Significativo)";
            string cooperacionS = cooperacion >= 4 ? cooperacion + "ptos (Significativo)" : cooperacion + "ptos (No Significativo)";



            int pFinal = rivalidad + suficiencia + agresividad + cooperacion;


            String resFinal = "";

            if (pFinal <= 11)
            {
                resFinal = "Baja Motivación";
            }

            if (pFinal >= 12 && pFinal <= 14)
            {
                resFinal = "Media Motivación";
            }

            if (pFinal >= 15 && pFinal <= 20)
            {
                resFinal = "Alta Motivación";
            }



            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    MotivDeporButt pruR = new MotivDeporButt();

                    pruR.Fecha = date;
                    pruR.DuraPrub = totalTiempoTest;
                    pruR.Conflicto = conflictoS;
                    pruR.Rivalidad = rivalidadS;
                    pruR.Suficiencia = suficienciaS;
                    pruR.Agresividad = agresividadS;
                    pruR.Cooperacion = cooperacionS;
                    pruR.calFilna = resFinal;
                    pruR.Pregunta = "Durante los últimos meses mientras participas (entrenas o compites) en " + richTextBox1.Text;
                    pruR.PuntuacionTotal = pFinal.ToString();


                    entities.MotivDeporButt.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<MotivDeporButt>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PMotivDepButt = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PMotivDepButt == null)
                    {

                        MotivDeporButt pruR = new MotivDeporButt();

                        pruR.Fecha = date;
                        pruR.DuraPrub = totalTiempoTest;
                        pruR.Conflicto = conflictoS;
                        pruR.Rivalidad = rivalidadS;
                        pruR.Suficiencia = suficienciaS;
                        pruR.Agresividad = agresividadS;
                        pruR.calFilna = resFinal;
                        pruR.Cooperacion = cooperacionS;
                        pruR.Pregunta = "Durante los últimos meses mientras participas (entrenas o compites) en " + richTextBox1.Text;
                        pruR.PuntuacionTotal = pFinal.ToString();


                        entities.MotivDeporButt.Add(pruR);
                        entities.SaveChangesAsync();

                        var ultimo = entities.Set<MotivDeporButt>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PMotivDepButt = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.MotivDeporButt.Where(f => f.idTest == sujetoEva.PMotivDepButt).FirstOrDefault<MotivDeporButt>();


                        conect.Fecha = date;
                        conect.DuraPrub = totalTiempoTest;
                        conect.Conflicto = conflictoS;
                        conect.Rivalidad = rivalidadS;
                        conect.Suficiencia = suficienciaS;
                        conect.Agresividad = agresividadS;
                        conect.calFilna = resFinal;
                        conect.Cooperacion = cooperacionS;
                        conect.Pregunta = "Durante los últimos meses mientras participas (entrenas o compites) en " + richTextBox1.Text;
                        conect.PuntuacionTotal = pFinal.ToString();

                        
                        entities.SaveChangesAsync();

                    }
                }

            }
        }

        private void salvarCualidadesMotivacionales()
        {
            int motivLogro = 0;
            int motivNoLogro = 0;
            int motivIntrinseca = 0;
            int motivExtrins = 0;
            int expectExito = 0;
            int expectEficaci = 0;
            int motivAproExit = 0;
            int motivEnvFrac = 0;
            int motivMateri = 0;
            int motivReco = 0;
            int motivAutoDep = 0;
            int motivAutoPers = 0;
            int motivSupra = 0;


            String motivLogroS = "";
            String motivNoLogroS = "";
            String motivIntrinsecaS = "";
            String motivExtrinsS = "";
            String expectExitoS = "";
            String expectEficaciS = "";
            String motivAproExitS = "";
            String motivEnvFracS = "";
            String motivMateriS = "";
            String motivRecoS = "";
            String motivAutoDepS = "";
            String motivAutoPersS = "";
            String motivSupraS = "";


            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if ((realPos == 5 || realPos == 14 || realPos == 31 || realPos == 44) && listRespuesta[i].valor == "1")
                {
                    motivLogro++;

                }

                if ((realPos == 6 || realPos == 15 || realPos == 32 || realPos == 45) && listRespuesta[i].valor == "1")
                {
                    motivNoLogro++;

                }

                if ((realPos == 7 || realPos == 16 || realPos == 33 || realPos == 46) && listRespuesta[i].valor == "1")
                {
                    motivIntrinseca++;

                }

                if ((realPos == 8 || realPos == 17 || realPos == 34 || realPos == 47) && listRespuesta[i].valor == "1")
                {
                    motivExtrins++;

                }

                if ((realPos == 1 || realPos == 23 || realPos == 27 || realPos == 40) && listRespuesta[i].valor == "1")
                {
                    expectExito++;
                }

                if ((realPos == 2 || realPos == 24 || realPos == 28 || realPos == 41) && listRespuesta[i].valor == "1")
                {
                    expectEficaci++;
                }

                if ((realPos == 3 || realPos == 25 || realPos == 29 || realPos == 42) && listRespuesta[i].valor == "1")
                {
                    motivAproExit++;
                }

                if ((realPos == 4 || realPos == 26 || realPos == 30 || realPos == 43) && listRespuesta[i].valor == "1")
                {
                    motivEnvFrac++;
                }

                if ((realPos == 13 || realPos == 18 || realPos == 37 || realPos == 50) && listRespuesta[i].valor == "1")
                {
                    motivMateri++;
                }

                if ((realPos == 9 || realPos == 19 || realPos == 35 || realPos == 48) && listRespuesta[i].valor == "1")
                {
                    motivReco++;
                }

                if ((realPos == 10 || realPos == 20 || realPos == 38 || realPos == 49) && listRespuesta[i].valor == "1")
                {
                    motivAutoDep++;
                }

                if ((realPos == 11 || realPos == 21 || realPos == 36 || realPos == 51) && listRespuesta[i].valor == "1")
                {
                    motivAutoPers++;
                }

                if ((realPos == 12 || realPos == 22 || realPos == 39 || realPos == 52) && listRespuesta[i].valor == "1")
                {
                    motivSupra++;
                }

            }


            if (motivLogro == 0)
            {
                motivLogroS = "Nula";
            }

            if (motivLogro == 1)
            {
                motivLogroS = "Baja";
            }
            if (motivLogro == 2)
            {
                motivLogroS = "Media";
            }
            if (motivLogro == 3)
            {
                motivLogroS = "Alta";
            }
            if (motivLogro == 4)
            {
                motivLogroS = "Muy Alta";
            }







            if (motivNoLogro == 0)
            {
                motivNoLogroS = "Nula";
            }
            if (motivNoLogro == 1)
            {
                motivNoLogroS = "Baja";
            }
            if (motivNoLogro == 2)
            {
                motivNoLogroS = "Media";
            }
            if (motivNoLogro == 3)
            {
                motivNoLogroS = "Alta";
            }
            if (motivNoLogro == 4)
            {
                motivNoLogroS = "Muy Alta";
            }



            if (motivIntrinseca == 0)
            {
                motivIntrinsecaS = "Nula";
            }
            if (motivIntrinseca == 1)
            {
                motivIntrinsecaS = "Baja";
            }
            if (motivIntrinseca == 2)
            {
                motivIntrinsecaS = "Media";
            }
            if (motivIntrinseca == 3)
            {
                motivIntrinsecaS = "Alta";
            }
            if (motivIntrinseca == 4)
            {
                motivIntrinsecaS = "Muy Alta";
            }





            if (motivExtrins == 0)
            {
                motivExtrinsS = "Nula";
            }
            if (motivExtrins == 1)
            {
                motivExtrinsS = "Baja";
            }
            if (motivExtrins == 2)
            {
                motivExtrinsS = "Media";
            }
            if (motivExtrins == 3)
            {
                motivExtrinsS = "Alta";
            }
            if (motivExtrins == 4)
            {
                motivExtrinsS = "Muy Alta";
            }






            if (expectExito == 0)
            {
                expectExitoS = "Nula";
            }
            if (expectExito == 1)
            {
                expectExitoS = "Baja";
            }
            if (expectExito == 2)
            {
                expectExitoS = "Media";
            }
            if (expectExito == 3)
            {
                expectExitoS = "Alta";
            }
            if (expectExito == 4)
            {
                expectExitoS = "Muy Alta";
            }







            if (expectEficaci == 0)
            {
                expectEficaciS = "Nula";
            }
            if (expectEficaci == 1)
            {
                expectEficaciS = "Baja";
            }
            if (expectEficaci == 2)
            {
                expectEficaciS = "Media";
            }
            if (expectEficaci == 3)
            {
                expectEficaciS = "Alta";
            }
            if (expectEficaci == 4)
            {
                expectEficaciS = "Muy Alta";
            }





            if (motivAproExit == 0)
            {
                motivAproExitS = "Nula";
            }
            if (motivAproExit == 1)
            {
                motivAproExitS = "Baja";
            }
            if (motivAproExit == 2)
            {
                motivAproExitS = "Media";
            }
            if (motivAproExit == 3)
            {
                motivAproExitS = "Alta";
            }
            if (motivAproExit == 4)
            {
                motivAproExitS = "Muy Alta";
            }




            if (motivEnvFrac == 0)
            {
                motivEnvFracS = "Nula";
            }
            if (motivEnvFrac == 1)
            {
                motivEnvFracS = "Baja";
            }
            if (motivEnvFrac == 2)
            {
                motivEnvFracS = "Media";
            }
            if (motivEnvFrac == 3)
            {
                motivEnvFracS = "Alta";
            }
            if (motivEnvFrac == 4)
            {
                motivEnvFracS = "Muy Alta";
            }




            if (motivMateri == 0)
            {
                motivMateriS = "Nula";
            }
            if (motivMateri == 1)
            {
                motivMateriS = "Baja";
            }
            if (motivMateri == 2)
            {
                motivMateriS = "Media";
            }
            if (motivMateri == 3)
            {
                motivMateriS = "Alta";
            }
            if (motivMateri == 4)
            {
                motivMateriS = "Muy Alta";
            }






            if (motivReco == 0)
            {
                motivRecoS = "Nula";
            }
            if (motivReco == 1)
            {
                motivRecoS = "Baja";
            }
            if (motivReco == 2)
            {
                motivRecoS = "Media";
            }
            if (motivReco == 3)
            {
                motivRecoS = "Alta";
            }
            if (motivReco == 4)
            {
                motivRecoS = "Muy Alta";
            }




            if (motivAutoDep == 0)
            {
                motivAutoDepS = "Nula";
            }
            if (motivAutoDep == 1)
            {
                motivAutoDepS = "Baja";
            }
            if (motivAutoDep == 2)
            {
                motivAutoDepS = "Media";
            }
            if (motivAutoDep == 3)
            {
                motivAutoDepS = "Alta";
            }
            if (motivAutoDep == 4)
            {
                motivAutoDepS = "Muy Alta";
            }




            if (motivAutoPers == 0)
            {
                motivAutoPersS = "Nula";
            }
            if (motivAutoPers == 1)
            {
                motivAutoPersS = "Baja";
            }
            if (motivAutoPers == 2)
            {
                motivAutoPersS = "Media";
            }
            if (motivAutoPers == 3)
            {
                motivAutoPersS = "Alta";
            }
            if (motivAutoPers == 4)
            {
                motivAutoPersS = "Muy Alta";
            }






            if (motivSupra == 0)
            {
                motivSupraS = "Nula";
            }
            if (motivSupra == 1)
            {
                motivSupraS = "Baja";
            }
            if (motivSupra == 2)
            {
                motivSupraS = "Media";
            }
            if (motivSupra == 3)
            {
                motivSupraS = "Alta";
            }
            if (motivSupra == 4)
            {
                motivSupraS = "Muy Alta";
            }





            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");


            using (mainEntities entities = new mainEntities())
            {


                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    CualidMotivDeportiv pruR = new CualidMotivDeportiv();
                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;
                    pruR.motivLogro = motivLogroS;
                    pruR.noMotivLogro = motivNoLogroS;
                    pruR.motivIntrínseca = motivIntrinsecaS;
                    pruR.motivExtrínseca = motivExtrinsS;
                    pruR.expecExito = expectExitoS;
                    pruR.expecEficacia = expectEficaciS;
                    pruR.motivAproExito = motivAproExitS;
                    pruR.movEvitarFracaso = motivEnvFracS;
                    pruR.motivMater = motivMateriS;
                    pruR.motivRecono = motivRecoS;
                    pruR.motivAutoDeportiva = motivAutoDepS;
                    pruR.motivAutoPersono = motivAutoPersS;
                    pruR.motivSuprain = motivSupraS;

                    entities.CualidMotivDeportiv.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<CualidMotivDeportiv>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PCualidMotivDepor = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.CualidMotivDeportiv == null)
                    {
                        CualidMotivDeportiv pruR = new CualidMotivDeportiv();
                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;
                        pruR.motivLogro = motivLogroS;
                        pruR.noMotivLogro = motivNoLogroS;
                        pruR.motivIntrínseca = motivIntrinsecaS;
                        pruR.motivExtrínseca = motivExtrinsS;
                        pruR.expecExito = expectExitoS;
                        pruR.expecEficacia = expectEficaciS;
                        pruR.motivAproExito = motivAproExitS;
                        pruR.movEvitarFracaso = motivEnvFracS;
                        pruR.motivMater = motivMateriS;
                        pruR.motivRecono = motivRecoS;
                        pruR.motivAutoDeportiva = motivAutoDepS;
                        pruR.motivAutoPersono = motivAutoPersS;
                        pruR.motivSuprain = motivSupraS;

                        entities.CualidMotivDeportiv.Add(pruR);
                        entities.SaveChangesAsync();

                        var ultimo = entities.Set<CualidMotivDeportiv>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PCualidMotivDepor = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.CualidMotivDeportiv.Where(f => f.idTest == sujetoEva.PCualidMotivDepor).FirstOrDefault<CualidMotivDeportiv>();


                        conect.DuraPru = totalTiempoTest;
                        conect.motivLogro = motivLogroS;
                        conect.noMotivLogro = motivNoLogroS;
                        conect.motivIntrínseca = motivIntrinsecaS;
                        conect.motivExtrínseca = motivExtrinsS;
                        conect.expecExito = expectExitoS;
                        conect.expecEficacia = expectEficaciS;
                        conect.motivAproExito = motivAproExitS;
                        conect.movEvitarFracaso = motivEnvFracS;
                        conect.motivMater = motivMateriS;
                        conect.motivRecono = motivRecoS;
                        conect.motivAutoDeportiva = motivAutoDepS;
                        conect.motivAutoPersono = motivAutoPersS;
                        conect.motivSuprain = motivSupraS;

                        entities.SaveChangesAsync();

                    }
                }
            }

        }

        private void salvarActitudCompetencia()
        {
            int certeza = 0;
            int contrarios = 0;

            int significacion = 0;
            int opinion = 0;
            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            for (int i = 0; i < listRespuesta.Count; i++)
            {
                int realPos = i + 1;

                if (realPos == 1 || realPos == 5 || realPos == 9 || realPos == 13 || realPos == 17 || realPos == 21 || realPos == 25)
                {
                    if (listRespuesta[i].valor == "2")
                    {
                        certeza++;
                    }
                }

                if (realPos == 2 || realPos == 22 || realPos == 6 || realPos == 10 || realPos == 14 || realPos == 18 || realPos == 26)
                {
                    if (realPos == 2 || realPos == 22)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            contrarios++;
                        }
                    }
                    else
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            contrarios++;
                        }
                    }
                }

                if (realPos == 3 || realPos == 7 || realPos == 11 || realPos == 15 || realPos == 19 || realPos == 23 || realPos == 27)
                {
                    if (listRespuesta[i].valor == "1")
                    {
                        significacion++;
                    }
                }

                if (realPos == 4 || realPos == 8 || realPos == 12 || realPos == 20 || realPos == 24 || realPos == 16 || realPos == 28)
                {
                    if (realPos == 16 || realPos == 28)
                    {
                        if (listRespuesta[i].valor == "2")
                        {
                            opinion++;
                        }
                    }
                    else
                    {
                        if (listRespuesta[i].valor == "1")
                        {
                            opinion++;
                        }
                    }
                }
            }


            string ce = "";
            string sig = "";
            string opi = "";
            string con = "";

            if (certeza >= 0 && certeza <= 3)
            {
                ce = "Alta.Seguro de sus fuerzas";
            }
            if (certeza == 4)
            {
                ce = "Promedio";
            }
            if (certeza >= 5 && certeza <= 7)
            {
                ce = "Baja.No está seguro de sus fuerzas";
            }



            if (significacion >= 0 && significacion <= 3)
            {
                sig = "Baja.Con pocos deseos de participar";
            }
            if (significacion == 4)
            {
                sig = "Promedio";
            }
            if (significacion >= 5 && significacion <= 7)
            {
                sig = "Alta y con gran deseo de participar";
            }

            if (opinion >= 0 && opinion <= 3)
            {
                opi = "Alta con alta orientación";
            }
            if (opinion == 4)
            {
                opi = "Promedio";
            }
            if (opinion >= 5 && opinion <= 7)
            {
                opi = "Baja con baja orientación";
            }

            if (contrarios >= 0 && contrarios <= 3)
            {
                con = "Baja con subvaloración de la fuerza del contrario";
            }
            if (contrarios == 4)
            {
                con = "Promedio o igual posibilidad";
            }
            if (contrarios >= 5 && contrarios <= 7)
            {
                con = "Alta con alta valoración de las fuerzas del contrario";
            }



            using (mainEntities entities = new mainEntities())
            {


                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);

                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    ActitudAnteCompetencia pruR = new ActitudAnteCompetencia();
                    pruR.Fecha = date;
                    pruR.DuraPrub = totalTiempoTest;
                    pruR.Opinion = opi;
                    pruR.Significacion = sig;
                    pruR.Certeza = ce;
                    pruR.Contrario = con;






                    pruR.ptoCerteza = certeza.ToString();
                    pruR.ptoContrio = contrarios.ToString();
                    pruR.ptoSignificacion = significacion.ToString();
                    pruR.ptoOpinion = opinion.ToString();

                    entities.ActitudAnteCompetencia.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<ActitudAnteCompetencia>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PActiAnteComp = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.ActitudAnteCompetencia == null)
                    {
                        ActitudAnteCompetencia pruR = new ActitudAnteCompetencia();
                        pruR.Fecha = date;
                        pruR.DuraPrub = totalTiempoTest;
                        pruR.Opinion = opi;
                        pruR.Significacion = sig;
                        pruR.Certeza = ce;
                        pruR.Contrario = con;



                        pruR.ptoCerteza = certeza.ToString();
                        pruR.ptoContrio = contrarios.ToString();
                        pruR.ptoSignificacion = significacion.ToString();
                        pruR.ptoOpinion = opinion.ToString();

                        entities.ActitudAnteCompetencia.Add(pruR);
                        entities.SaveChangesAsync();

                        var ultimo = entities.Set<ActitudAnteCompetencia>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PActiAnteComp = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.ActitudAnteCompetencia.Where(f => f.idTest == sujetoEva.PActiAnteComp).FirstOrDefault<ActitudAnteCompetencia>();

                        conect.DuraPrub = totalTiempoTest;
                        conect.Opinion = opi;
                        conect.Significacion = sig;
                        conect.Certeza = ce;
                        conect.Contrario = con;


                        conect.ptoCerteza = certeza.ToString();
                        conect.ptoContrio = contrarios.ToString();
                        conect.ptoSignificacion = significacion.ToString();
                        conect.ptoOpinion = opinion.ToString();

                        entities.SaveChangesAsync();

                    }
                }
            }
        }






        private void limpiarLabelColor()
        {

            label10.BackColor = Color.Transparent;
            label11.BackColor = Color.Transparent;
            label12.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;
            label14.BackColor = Color.Transparent;



            label10.ForeColor = Color.Black;
            label11.ForeColor = Color.Black;
            label12.ForeColor = Color.Black;
            label13.ForeColor = Color.Black;
            label14.ForeColor = Color.Black;

        }



        private void Preguntas_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (Char.IsNumber(e.KeyChar))
            {

                int res = Convert.ToInt32(e.KeyChar.ToString());

                if (!time.IsRunning)
                    time.Start();

                if (prueba == "IDARE (Situacional)" || prueba == "IDARE (Rasgo)" || prueba == "Ansiedad Precompetitiva CSAI-2R")
                {
                    if (res >= 1 && res <= 4)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);

                        valor = res;

                    }
                }

                if (prueba == "Eysenck")
                {
                    if (res >= 1 && res <= 2)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);

                        valor = res;


                    }
                }

                if (prueba == "Catell")
                {

                    if (res >= 1 && res <= 3)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);

                        valor = res;


                    }


                }

                if (prueba == "16PF")
                {
                    if (res >= 1 && res <= 3)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);
                        valor = res;


                    }
                }

                if (prueba == "Factor Humano" || prueba == "POMS" || prueba == "I.R de Loehr" ||
                    prueba == "Inventario Psicológico de Ejecución Deportiva (IPED)" || prueba == "IDETEM-1")
                {
                    if (res >= 1 && res <= 5)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);

                        valor = res;


                    }
                }

                if (prueba == "Actitud ante la Competencia" || prueba == "Test de Motivos Deportivos de Butt"
                    || prueba == "Cualidades Motivacionales Deportivas")
                {
                    if (res >= 1 && res <= 2)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);

                        valor = res;

                    }
                }

                if (prueba == "Cualidades Volitivas en el Deporte")
                {
                    if (res >= 0 && res <= 4)
                    {
                        limpiarLabelColor();
                        pintarForeColor(res);

                        valor = res;

                    }
                }



            }


            if (valor != -1)
            {

                if (e.KeyChar.ToString() == "\r")
                {
                    if (checkMode == 0)
                    {
                        // label3.Visible = false;
                        if (prueba == "16PF")
                        {

                            listPregunta16PF[countList].valor = valor.ToString();
                        }
                        else
                        {

                            // listPregunta16PF[countList].valor= valor.ToString();

                            AuxPreguntas aux = null;

                            aux = new AuxPreguntas(valor.ToString(), listPreguntas[countList]);
                            listRespuesta.Add(aux);
                        }

                    }
                    else
                    {
                        if (prueba == "16PF")
                        {
                            listPregunta16PF[countList].valor = valor.ToString();
                        }

                        else
                        {
                            listRespuesta[countList].valor = valor.ToString();
                        }


                    }

                    countList++;
                    valor = -1;

                    nextQuestion();

                }

            }


        }

        private void pintarForeColor(int res)
        {
            if (prueba == "IDARE (Situacional)" || prueba == "IDARE (Rasgo)" || prueba == "Ansiedad Precompetitiva CSAI-2R")
            {


                if (res == 1)
                {
                    label10.BackColor = Color.Red;
                    label10.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label11.BackColor = Color.Red;
                    label11.ForeColor = Color.White;
                }

                if (res == 3)
                {
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                }

                if (res == 4)
                {
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                }


                valor = res;

            }

            if (prueba == "Eysenck")
            {

                if (res == 1)
                {
                    label11.BackColor = Color.Red;
                    label11.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                }


                valor = res;



            }

            if (prueba == "Catell")
            {


                if (res == 1)
                {
                    label10.BackColor = Color.Red;
                    label10.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                }

                if (res == 3)
                {
                    label14.BackColor = Color.Red;
                    label14.ForeColor = Color.White;
                }







            }

            if (prueba == "16PF")
            {

                if (res == 1)
                {
                    label10.BackColor = Color.Red;
                    label10.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                }

                if (res == 3)
                {
                    label14.BackColor = Color.Red;
                    label14.ForeColor = Color.White;
                }


            }

            if (prueba == "Factor Humano" || prueba == "POMS" || prueba == "I.R de Loehr" || prueba == "IDETEM-1")
            {


                if (res == 1)
                {
                    label10.BackColor = Color.Red;
                    label10.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label11.BackColor = Color.Red;
                    label11.ForeColor = Color.White;
                }

                if (res == 3)
                {
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                }

                if (res == 4)
                {
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                }

                if (res == 5)
                {
                    label14.BackColor = Color.Red;
                    label14.ForeColor = Color.White;
                }


            }

            if (prueba == "Actitud ante la Competencia" || prueba == "Test de Motivos Deportivos de Butt" || prueba == "Cualidades Motivacionales Deportivas")
            {
                if (res == 1)
                {
                    label11.BackColor = Color.Red;
                    label11.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                }

            }

            if (prueba == "Inventario Psicológico de Ejecución Deportiva (IPED)")
            {
                if (res == 1)
                {
                    label14.BackColor = Color.Red;
                    label14.ForeColor = Color.White;

                }
                if (res == 2)
                {
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                }

                if (res == 3)
                {
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                }

                if (res == 4)
                {
                    label11.BackColor = Color.Red;
                    label11.ForeColor = Color.White;
                }

                if (res == 5)
                {
                    label10.BackColor = Color.Red;
                    label10.ForeColor = Color.White;
                }
            }

            if (prueba == "Cualidades Volitivas en el Deporte")
            {
                if (res == 0)
                {
                    label14.BackColor = Color.Red;
                    label14.ForeColor = Color.White;

                }
                if (res == 1)
                {
                    label13.BackColor = Color.Red;
                    label13.ForeColor = Color.White;
                }

                if (res == 2)
                {
                    label12.BackColor = Color.Red;
                    label12.ForeColor = Color.White;
                }

                if (res == 3)
                {
                    label11.BackColor = Color.Red;
                    label11.ForeColor = Color.White;
                }

                if (res == 4)
                {
                    label10.BackColor = Color.Red;
                    label10.ForeColor = Color.White;
                }
            }


        }

        private void Preguntas_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (XtraMessageBox.Show("No ha terminado la prueba.Desea salir sin guardar los resultados", "Exito", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK))


        }

        private void Preguntas_Load(object sender, EventArgs e)
        {

        }





        private void salvarIdareSituacional()
        {


            int[] sum1Arr = new int[] { 2, 3, 5, 6, 8, 11, 12, 13, 16, 17 };

            int[] sum2Arr = new int[] { 0, 1, 4, 7, 9, 10, 14, 15, 18, 19 };



            int sum1 = 0;
            int sum2 = 0;



            int i = 0;

            string ansiedadSituacional = "";


            while (i < 10)
            {
                sum1 += Convert.ToInt32(listRespuesta[sum1Arr[i]].valor);
                sum2 += Convert.ToInt32(listRespuesta[sum2Arr[i]].valor);

                i++;
            }

            int calSituacional = sum1 - sum2 + 50;


            if (calSituacional <= 30)
                ansiedadSituacional = "Nivel Bajo";
            if (calSituacional > 30 && calSituacional <= 44)
                ansiedadSituacional = "Nivel Medio";
            if (calSituacional >= 45)
                ansiedadSituacional = "Nivel Alto";




            String date = DateTime.Now.Date.ToString("yyyy-MM-dd");


            using (mainEntities entities = new mainEntities())
            {



                int idPerson = Convert.ToInt32(idUser);
                int idEtapa = Convert.ToInt32(etapa);


                var sujetoEva = entities.SujetosEvaluados.Where(s => s.Fecha == date).Where(f => f.idSujeto == idPerson).Where(g => g.Etapa == idEtapa).FirstOrDefault<SujetosEvaluados>();

                if (sujetoEva == null)
                {


                    PruIdareSituacional pruR = new PruIdareSituacional();

                    pruR.Fecha = date;
                    pruR.DuraPru = totalTiempoTest;

                    pruR.PAnsiedadSituacional = calSituacional.ToString();
                    pruR.DiagAnsSituacional = ansiedadSituacional;

                    entities.PruIdareSituacional.Add(pruR);
                    entities.SaveChangesAsync();



                    var ultimo = entities.Set<PruIdareSituacional>().OrderByDescending(t => t.idTest).FirstOrDefault();

                    SujetosEvaluados pru = new SujetosEvaluados();

                    pru.Fecha = date;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.PIdareSitua = ultimo.idTest;
                    pru.Etapa = Convert.ToInt32(etapa);
                    pru.idSujeto = Convert.ToInt32(idPerson);
                    entities.SujetosEvaluados.Add(pru);
                    entities.SaveChangesAsync();


                }
                else
                {
                    if (sujetoEva.PIdareSitua == null)
                    {
                        PruIdareSituacional pruR = new PruIdareSituacional();

                        pruR.Fecha = date;
                        pruR.DuraPru = totalTiempoTest;

                        pruR.PAnsiedadSituacional = calSituacional.ToString();
                        pruR.DiagAnsSituacional = ansiedadSituacional;


                        entities.PruIdareSituacional.Add(pruR);
                        entities.SaveChangesAsync();


                        var ultimo = entities.Set<PruIdareSituacional>().OrderByDescending(t => t.idTest).FirstOrDefault();


                        sujetoEva.PIdareSitua = ultimo.idTest;
                        entities.SaveChangesAsync();

                    }
                    else
                    {
                        var conect = entities.PruIdareSituacional.Where(f => f.idTest == sujetoEva.PIdareSitua).FirstOrDefault<PruIdareSituacional>();


                        conect.DuraPru = totalTiempoTest;

                        conect.PAnsiedadSituacional = calSituacional.ToString();
                        conect.DiagAnsSituacional = ansiedadSituacional;


                        entities.SaveChangesAsync();

                    }
                }
            }

        }



        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {

                if (e.KeyChar != (char)Keys.Space)
                {
                    e.Handled = true;
                    return;
                }


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}