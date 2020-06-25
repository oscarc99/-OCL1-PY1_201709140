using OLC1_PY1_201700988.Objetos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC1_PY1_201700988.Analizadores
{
    class parser_201709140
    {
        private int posicion;
        private ArrayList errores;
        private ArrayList tokens;
        String dir="";
        String dot= "digraph G { \n"+
        "node[shape = circle fontname = Arial]; \n";
        public ArrayList getTokens()
        {
            return tokens;
        }

        public ArrayList getErrores()
        {
            return errores;
        }


        public parser_201709140()
        {
            dot = "digraph G { \n" +
       "node[shape = circle fontname = Arial]; \n";
            dir = "";
            this.errores = new ArrayList();
            this.tokens = new ArrayList();
            this.posicion = 0;
        }
        public parser_201709140(ArrayList tokens) 
        {
            dot = "digraph G { \n" +
       "node[shape = circle fontname = Arial]; \n";
            dir = "";
            this.tokens = tokens;
            this.errores = new ArrayList();
            this.posicion = 0;
        }

        public void limpiar()
        {
            tokens.Clear();
            errores.Clear();
            dot = "digraph G { \n" +
      "node[shape = circle fontname = Arial]; \n";
            dir = "";
        }


        public bool parser(ArrayList tokens)
        {
            
            posicion = 0;
            this.tokens = tokens;
            if (tokens.Count!=0)
            {
                dot += "node" + 0 + " [label=\"INICIO\"]; \n";
                inicio("0");
                
            }
            
            return errores.Count==0;
        }

        public void inicio(String pos)
        {
            if (posicion <= tokens.Count-1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 4)//Crear
                {
                    
                    dot += "node" + pos + "1 [label=\"C\"]; \n";
                    C(pos+"1");

                    dot += "node" + pos + "2 [label=\";\"]; \n";
                    match(20);//;
                    
                    dot += "node" + pos + "3 [label=\"INICIO\"]; \n";
                    inicio(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                }
                else if (temp.getToken() == 6)//INSERTAR
                {
                    dot += "node" + pos + "1 [label=\"I\"]; \n";
                    I(pos+"1");
                    dot += "node" + pos + "2 [label=\";\"]; \n";
                    match(20);//;
                    dot += "node" + pos + "3 [label=\"INICIO\"]; \n";
                    inicio(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                }
                else if (temp.getToken() == 9)//SELECCIONAR
                {
                    dot += "node" + pos + "1 [label=\"S\"]; \n";
                    S(pos+"1");
                    dot += "node" + pos + "2 [label=\";\"]; \n";
                    match(20);//;
                    dot += "node" + pos + "3 [label=\"INICIO\"]; \n";
                    inicio(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                }
                else if (temp.getToken() == 15)//ELIMINAR
                {
                    dot += "node" + pos + "1 [label=\"E\"]; \n";
                    E(pos+"1");
                    dot += "node" + pos + "2 [label=\";\"]; \n";
                    match(20);//;
                    dot += "node" + pos + "3 [label=\"INICIO\"]; \n";
                    inicio(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                }
                else if (temp.getToken() == 16)//ACTUALIZAR
                {
                    dot += "node" + pos + "1 [label=\"A\"]; \n";
                    A(pos+"1");
                    dot += "node" + pos + "2 [label=\";\"]; \n";
                    match(20);//;
                    dot += "node" + pos + "3 [label=\"INICIO\"]; \n";
                    inicio(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";

                }
                else
                {
                    if (posicion == tokens.Count)
                    {
                        //Esta al final ya no vienen mas
                        dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                        dir += "node" + pos + "-> node" + pos + "1;\n";
                    }
                    else
                    {
                        //ERROR se esperaba CREAR/INSERTAR/SELECCIONAR/ELIMINAR/ACTUALIZAR
                        errores.Add(new Error("Error sintactico", "Se esperaba palabra reservada CREAR/INSERTAR/ELIMINAR/SELECCIONAR/ACTUALIZAR",temp.getLexema(), temp.getFila(), temp.getColumna()));
                        recuperar();

                    }
                }
            }
            
        }
        public String getDot()
        {
            dot += "\n" + dir;
            dot += "}";
            return dot;
        }

        public String valor()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 35)//Cadena
                {
                    return convertir(temp.getLexema());
                }
                else
                {
                    return temp.getLexema();
                }

                


            }
            return "";
        }
        private String convertir(string dato)
        {
            String valor = "";
            for (int i = 1; i < dato.Length - 1; i++)
            {
                valor += dato[i].ToString();
            }
            return valor;
        }

        private void I(String pos)
        {
            dot += "node" + pos + "1 [label=\""+valor()+"\"]; \n";
            match(6);//Insertar
            dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
            match(7);//EN
            dot += "node" + pos + "3 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "4 [label=\"" + valor() + "\"]; \n";
            match(8);//VALORES
            dot += "node" + pos + "5 [label=\"" + valor() + "\"]; \n";
            match(21);//(
            dot += "node" + pos + "6 [label=\"VALORES'\"]; \n";
            VALORES_(pos+"6");
            dot += "node" + pos + "7 [label=\"" + valor() + "\"]; \n";
            match(22);//)
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
            dir += "node" + pos + "-> node" + pos + "4;\n";
            dir += "node" + pos + "-> node" + pos + "5;\n";
            dir += "node" + pos + "-> node" + pos + "6;\n";
            dir += "node" + pos + "-> node" + pos + "7;\n";

        }

        private void VALORES_(String pos)
        {
            dot += "node" + pos + "1 [label=\"VALOR\"]; \n";
            VALOR(pos+"1");
            dot += "node" + pos + "2 [label=\"VALOR'\"]; \n";
            VALOR_(pos + "2");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
        }

        private void VALOR_(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)
                {
                    dot += "node" + pos + "1 [label=\"" + temp.getLexema() + "\"]; \n";
                    match(19);//,
                    dot += "node" + pos + "2 [label=\"VALOR\"]; \n";
                    VALOR(pos+"2");
                    dot += "node" + pos + "3 [label=\"VALOR'\"]; \n";
                    VALOR_(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    
                }
                else
                {
                    //EPSILON
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                
            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "Se esperaba valor/es No se completo la instruccion",temp.getLexema() , temp.getFila(), temp.getColumna()));
                
            }

        }

        private void VALOR(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken()==33)//ENTERO
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(33);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 34)//FLOTANTE
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(34);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 35)//CADENA
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(35);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 36)//FECHAR
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(36);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 37)
                {
                    dot += "node" + pos + "1 [label=\"ID'\"]; \n";
                    ID_(pos+"1");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else
                {
                    errores.Add(new Error("Error sintactico", "Se esperaba valor/es en especifico ENTERO/FLOTANTE/CADENA/FECHA", temp.getLexema(),temp.getFila(), temp.getColumna()));
                    recuperar();
                }
            }
            else
            {
                Token temp = (Token)tokens[tokens.Count-1];
                errores.Add(new Error("Error sintactico", "Se esperaba valor/es","No se completo la instruccion",temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void S(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(9);//SELECCIONAR
            dot += "node" + pos + "2 [label=\"COLUMNAS\"]; \n";
            COLUMNAS(pos+"2");
            dot += "node" + pos + "3 [label=\"" + valor() + "\"]; \n";
            match(11);//DE
            dot += "node" + pos + "4 [label=\"TABLAS\"]; \n";
            TABLAS(pos+"4");
            dot += "node" + pos + "5 [label=\"CONDICIONES\"]; \n";
            CONDICIONES(pos+"5");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
            dir += "node" + pos + "-> node" + pos + "4;\n";
            dir += "node" + pos + "-> node" + pos + "5;\n";
        }

        private void CONDICIONES(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 12)//DONDE
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(12);//DONDE
                    dot += "node" + pos + "2 [label=\"ID'\"]; \n";
                    ID_(pos+"2");
                    dot += "node" + pos + "3 [label=\"COMPARACION\"]; \n";
                    COMPARACION(pos+"3");//Signos de comparacion
                    dot += "node" + pos + "4 [label=\"VALOR\"]; \n";
                    VALOR(pos+"4");
                    dot += "node" + pos + "5 [label=\"CONDICION\"]; \n";
                    CONDICION(pos+ "5");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    dir += "node" + pos + "-> node" + pos + "4;\n";
                    dir += "node" + pos + "-> node" + pos + "5;\n";


                }
                else
                {
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    //                    errores.Add(new Error("Error sintactico", "Se esperaba condicion/es DONDE + la condicion",temp.getLexema(), temp.getFila(), temp.getColumna()));
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECIONAR","Falta de condicion", temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void ID_(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(37);//id
            dot += "node" + pos + "2 [label=\"ID''\"]; \n";
            ID__(pos+"2");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
        }

        private void ID__(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 30)//.
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(30);//.
                    dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
                    ID(pos+"2");//ID
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";

                }
                else
                {
                    //EPSILON
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECCIONAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void CONDICION(String pos)
        {

            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 13 || temp.getToken() ==  14)// Y/O
                {
                    dot += "node" + pos + "1 [label=\"CONDICION'\"]; \n";
                    CONDICION_(pos+"1");
                    dot += "node" + pos + "2 [label=\"ID'\"]; \n";
                    ID_(pos+"2");
                    dot += "node" + pos + "3 [label=\"COMPARACION\"]; \n";
                    COMPARACION(pos+"3");//Signos de comparacion
                    dot += "node" + pos + "4 [label=\"VALOR\"]; \n";
                    VALOR(pos+"4");
                    dot += "node" + pos + "5 [label=\"CONDICION\"]; \n";
                    CONDICION(pos+"5");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    dir += "node" + pos + "-> node" + pos + "4;\n";
                    dir += "node" + pos + "-> node" + pos + "5;\n";
                    

                }
                else
                {
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo la CONDICION",temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void CONDICION_(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {

                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 13)//Y
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(13);
                    dir += "node" + pos + "-> node" + pos + "1;\n";

                }
                else if (temp.getToken() == 14)//O
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(14);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                

                else
                {
                    errores.Add(new Error("Error sintactico", "Se esperaba signo Y/O", temp.getLexema(), temp.getFila(), temp.getColumna()));
                    recuperar();
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "Se esperaba Y/O", "No se completo la condicion", temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void COMPARACION(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 23)//!=
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(23);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 24)//=
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(24);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 25)//<=
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(25);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 26)//<
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(26);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 27)//>=
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(27);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 28)//>
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(28);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 29)//!
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(29);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }

                else
                {
                    errores.Add(new Error("Error sintactico", "No se completo la condicion", temp.getLexema(), temp.getFila(), temp.getColumna()));
                    recuperar();
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "Falta signo comparacion", "No se completo la condicion", temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void TABLAS(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "2 [label=\"TABLAS'\"]; \n";
            TABLAS_(pos+"2");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";

        }

        private void TABLAS_(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)//,
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(19);//,
                    dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
                    match(37);//ID
                    dot += "node" + pos + "3 [label=\"TABLAS'\"]; \n";
                    TABLAS_(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    

                }

                else
                {
                    //EPSILON
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECIONAR",temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void COLUMNAS(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken()==37)//id
                {
                    
                    dot += "node" + pos + "1 [label=\"ID'\"]; \n";
                    ID_(pos+"1");
                    dot += "node" + pos + "2 [label=\"ALIAS\"]; \n";
                    ALIAS(pos+"2");
                    dot += "node" + pos + "3 [label=\"COLUMNAS'\"]; \n";
                    COLUMNAS_(pos+"3");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                   
                }
                else if (temp.getToken() == 18)//*
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(18);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else
                {
                    //ERROR
                    errores.Add(new Error("Error sintactico", "Se esperaba COLUMNAS para la seleccion",temp.getLexema(), temp.getFila(), temp.getColumna()));
                    recuperar();
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECIONAR",temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void COLUMNAS_(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)//,
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(19);//,
                    dot += "node" + pos + "2 [label=\"ID'\"]; \n";
                    ID_(pos + "2");
                    dot += "node" + pos + "3 [label=\"ALIAS\"]; \n";
                    ALIAS(pos+"3");
                    dot += "node" + pos + "4 [label=\"COLUMNAS'\"]; \n";
                    COLUMNAS_(pos+"4");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    dir += "node" + pos + "-> node" + pos + "4;\n";
                    

                }
                
                else
                {
                    //EPSILON
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECIONAR", temp.getLexema(),temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void ID(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 18)//*
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(18);//COMO
                    dir += "node" + pos + "-> node" + pos + "1;\n";

                }
                else if (temp.getToken() == 37)//ID
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(37);//ID
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else
                {
                    errores.Add(new Error("Error sintactico", "Se esperaba ID ó *", temp.getLexema(), temp.getFila(), temp.getColumna()));
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECCIONAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void ALIAS(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 10)
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(10);//COMO
                    dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
                    match(37);//ID
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                }
                else
                {
                    //Epsilon
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECCIONAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void E(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(15);//ELIMINAR
            dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
            match(11);//DE
            dot += "node" + pos + "3 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "4 [label=\"CONDICIONES\"]; \n";
            CONDICIONES(pos + "4");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
            dir += "node" + pos + "-> node" + pos + "4;\n";


        }

        private void A(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(16);//ACTUALIZAR
            dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "3 [label=\"" + valor() + "\"]; \n";
            match(17);//ESTABLECER
            dot += "node" + pos + "4 [label=\"" + valor() + "\"]; \n";
            match(21);//(
            dot += "node" + pos + "5 [label=\"ESTABLECER\"]; \n";
            ESTABLECER_(pos+"5");
            dot += "node" + pos + "6 [label=\"" + valor() + "\"]; \n";
            match(22);//)
            dot += "node" + pos + "7 [label=\"CONDICIONES\"]; \n";
            CONDICIONES(pos+"7");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
            dir += "node" + pos + "-> node" + pos + "4;\n";
            dir += "node" + pos + "-> node" + pos + "5;\n";
            dir += "node" + pos + "-> node" + pos + "6;\n";
            dir += "node" + pos + "-> node" + pos + "7;\n";
            

        }

        private void ESTABLECER_(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
            match(24);//=
            dot += "node" + pos + "3 [label=\"VALOR\"]; \n";
            VALOR(pos+"3");
            dot += "node" + pos + "4 [label=\"ESTABLECER''\"]; \n";
            ESTABLECER__(pos+"4");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
            dir += "node" + pos + "-> node" + pos + "4;\n";


        }

        private void ESTABLECER__(String pos)
        {

            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(19);//,
                    dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
                    match(37);//ID
                    dot += "node" + pos + "3 [label=\"" + valor() + "\"]; \n";
                    match(24);//=
                    dot += "node" + pos + "4 [label=\"VALOR\"]; \n";
                    VALOR(pos+"4");
                    dot += "node" + pos + "5 [label=\"ESTABLECER''\"]; \n";
                    ESTABLECER__(pos+"5");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    dir += "node" + pos + "-> node" + pos + "4;\n";
                    dir += "node" + pos + "-> node" + pos + "5;\n";

                }
                else
                {
                    //Epsilon
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion ACTUALIZAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        public void C(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(4);//CREAR
            dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
            match(5);//TABLA
            dot += "node" + pos + "3 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "4 [label=\"" + valor() + "\"]; \n";
            match(21);//(
            dot += "node" + pos + "5 [label=\"" + valor() + "\"]; \n";
            ATRIBUTOS(pos+"5");
            dot += "node" + pos + "6 [label=\"" + valor() + "\"]; \n";
            match(22);//)
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
            dir += "node" + pos + "-> node" + pos + "4;\n";
            dir += "node" + pos + "-> node" + pos + "5;\n";
            dir += "node" + pos + "-> node" + pos + "6;\n";


        }

        private void ATRIBUTOS(String pos)
        {
            dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
            match(37);//ID
            dot += "node" + pos + "2 [label=\"TIPO\"]; \n";
            TIPO(pos+"2");
            dot += "node" + pos + "3 [label=\"ATRIBUTO'\"]; \n";
            ATRIBUTOS_(pos+"3");
            dir += "node" + pos + "-> node" + pos + "1;\n";
            dir += "node" + pos + "-> node" + pos + "2;\n";
            dir += "node" + pos + "-> node" + pos + "3;\n";
        }

        private void TIPO(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {

                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 0)//ENTERO
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(0);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 1)//CADENA
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(1);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 2)//FLOTANTE
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(2);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else if (temp.getToken() == 3)//FECHA
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(3);
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
                else
                {
                    //error se esperaba ENTERO/CADENA/FLOTANTE/FECHA
                    errores.Add(new Error("Error sintactico", "Se esperaba un TIPO de dato", temp.getLexema(), temp.getFila(), temp.getColumna()));
                    recuperar();
                }
            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "Se esperaba un TIPO de dato", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
                
            
        }

        private void ATRIBUTOS_(String pos)
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                
                if (temp.getToken() == 31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                    
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)
                {
                    dot += "node" + pos + "1 [label=\"" + valor() + "\"]; \n";
                    match(19);//,
                    dot += "node" + pos + "2 [label=\"" + valor() + "\"]; \n";
                    match(37);//ID
                    dot += "node" + pos + "3 [label=\"TIPO\"]; \n";
                    TIPO(pos+"3");
                    dot += "node" + pos + "4 [label=\"ATRIBUTO'\"]; \n";
                    ATRIBUTOS_(pos+"4");
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                    dir += "node" + pos + "-> node" + pos + "2;\n";
                    dir += "node" + pos + "-> node" + pos + "3;\n";
                    dir += "node" + pos + "-> node" + pos + "4;\n";
                }
                else
                {
                    //EPSILON
                    dot += "node" + pos + "1 [label=\"εpsilon\"]; \n";
                    dir += "node" + pos + "-> node" + pos + "1;\n";
                }
            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se termino de ingresar atributos", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
               

        }

        public void match(int token)
        {
            if(posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() ==31 || temp.getToken() == 32)//Si es comentario 
                {
                    Console.WriteLine("");
                    do{
                        posicion++;
                        temp = (Token)tokens[posicion];

                    } while (temp.getToken() == 31 && temp.getToken() == 32);
                }
                temp = (Token)tokens[posicion];
                if (temp.getToken() == token)
                {
                    posicion++;
                }
                else
                {
                    errores.Add(new Error("Error sintactico", "Se esperaba " + getToken(token), temp.getLexema(), temp.getFila(), temp.getColumna()));
                    recuperar();
                }
            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo la instruccion, se esperaba " + getToken(token), temp.getLexema(), temp.getFila(), temp.getColumna()));
            }
            
        }

        private void recuperar()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                
                while (temp.getToken() != 20)
                {

                    if (posicion < tokens.Count - 1)
                    {
                        posicion++;
                        temp = (Token)tokens[posicion];
                    }
                    else
                    {
                        Console.WriteLine("NO SE LOGRO RECUPERAR ");
                        MessageBox.Show("No se logro recuperar del error", "Error lexico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    
                }
            }
        }

        private String getToken(int token)
        {
            if(token == 0)
            {
                return "ENTERO";
            }else if (token == 1)
            {
                return "CADENA";
            }
            else if (token == 2)
            {
                return "FLOTANTE";
            }
            else if (token == 3)
            {
                return "FECHA";
            }
            else if (token == 4)
            {
                return "CREAR";
            }
            else if (token == 5)
            {
                return "TABLA";
            }
            else if (token == 6)
            {
                return "INSERTAR";
            }
            else if (token == 7)
            {
                return "EN";
            }
            else if (token == 8)
            {
                return "VALORES";
            }
            else if (token == 9)
            {
                return "SELECCIONAR";
            }
            else if (token == 10)
            {
                return "COMO";
            }
            else if (token == 11)
            {
                return "DE";
            }
            else if (token == 12)
            {
                return "DONDE";
            }
            else if (token == 13)
            {
                return "Y";
            }
            else if (token == 14)
            {
                return "O";
            }
            else if (token == 15)
            {
                return "ELIMINAR";
            }
            else if (token == 16)
            {
                return "ACTUALIZAR";
            }
            else if (token == 17)
            {
                return "ESTABLECER";
            }
            else if (token == 18)
            {
                return "*";
            }
            else if (token == 19)
            {
                return ",";
            }
            else if (token == 20)
            {
                return ";";
            }
            else if (token == 21)
            {
                return "(";
            }
            else if (token == 22)
            {
                return ")";
            }
            else if (token == 23)
            {
                return "!=";
            }
            else if (token == 24)
            {
                return "=";
            }
            else if (token == 25)
            {
                return "<=";
            }
            else if (token == 26)
            {
                return "<";
            }
            else if (token == 27)
            {
                return ">=";
            }
            else if (token == 28)
            {
                return ">";
            }
            else if (token == 29)
            {
                return "!";
            }
            else if (token == 30)
            {
                return ".";
            }
            else if (token == 31)
            {
                return "COMENTARIO";
            }
            else if (token == 32)
            {
                return "COMENTARIO MULTILINEA";
            }
            else if (token == 33)
            {
                return "ENTERO";
            }
            else if (token == 34)
            {
                return "FLOTANTE";
            }
            else if (token == 35)
            {
                return "CADENA";
            }
            else if (token == 36)
            {
                return "FECHA";
            }
            else if (token == 37)
            {
                return "IDENTIFICADOR";
            }
            
            return "";

        }
    }
    
    

}
