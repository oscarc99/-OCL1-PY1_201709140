using OLC1_PY1_201700988.Objetos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Analizadores
{
    class parser_201709140
    {
        private int posicion;
        private ArrayList errores;
        private ArrayList tokens;
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
            this.errores = new ArrayList();
            this.tokens = new ArrayList();
            this.posicion = 0;
        }
        public parser_201709140(ArrayList tokens) 
        {
            this.tokens = tokens;
            this.errores = new ArrayList();
            this.posicion = 0;
        }

        public void limpiar()
        {
            tokens.Clear();
            errores.Clear();
        }


        public bool parser(ArrayList tokens)
        {
            
            posicion = 0;
            this.tokens = tokens;
            if (tokens.Count!=0)
            {
                inicio();
            }
            
            return errores.Count==0;
        }

        public void inicio()
        {
            if (posicion <= tokens.Count-1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 4)//Crear
                {
                    C();
                    match(20);//;
                    inicio();
                }
                else if (temp.getToken() == 6)//INSERTAR
                {
                    I();
                    match(20);//;
                    inicio();
                }
                else if (temp.getToken() == 9)//SELECCIONAR
                {
                    S();
                    match(20);//;
                    inicio();
                }
                else if (temp.getToken() == 15)//ELIMINAR
                {
                    E();
                    match(20);//;
                    inicio();
                }
                else if (temp.getToken() == 16)//ACTUALIZAR
                {
                    A();
                    match(20);//;
                    inicio();
                }
                else
                {
                    if (posicion == tokens.Count)
                    {
                        //Esta al final ya no vienen mas
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

        private void I()
        {
            match(6);//Insertar
            match(7);//EN
            match(37);//ID
            match(8);//VALORES
            match(21);//(
            VALORES_();
            match(22);//)

        }

        private void VALORES_()
        {
            VALOR();
            VALOR_();
        }

        private void VALOR_()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)
                {
                    match(19);//,
                    VALOR();
                    VALOR_();
                }
                else
                {
                    //EPSILON
                }
                
            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "Se esperaba valor/es No se completo la instruccion",temp.getLexema() , temp.getFila(), temp.getColumna()));
                
            }

        }

        private void VALOR()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken()==33)//ENTERO
                {
                    match(33);
                }else if (temp.getToken() == 34)//FLOTANTE
                {
                    match(34);
                }
                else if (temp.getToken() == 35)//CADENA
                {
                    match(35);
                }
                else if (temp.getToken() == 36)//FECHAR
                {
                    match(36);
                }else if (temp.getToken() == 37)
                {
                    ID_();
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

        private void S()
        {
            match(9);//SELECCIONAR
            COLUMNAS();
            match(11);//DE
            TABLAS();
            CONDICIONES();
        }

        private void CONDICIONES()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 12)//DONDE
                {
                    match(12);//DONDE
                    ID_();
                    COMPARACION();//Signos de comparacion
                    VALOR();
                    CONDICION();

                }
                else
                {
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

        private void ID_()
        {
            match(37);//id
            ID__();
        }

        private void ID__()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 30)//.
                {
                    match(30);//.
                    match(37);//id


                }
                else
                {
                    
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECCIONAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void CONDICION()
        {

            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 13 || temp.getToken() ==  14)// Y/O
                {
                    CONDICION_();
                    ID_();
                    COMPARACION();//Signos de comparacion
                    VALOR();
                    CONDICION();

                }
                else
                {
                    
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo la CONDICION",temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void CONDICION_()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 13)//Y
                {
                    match(13);
                }
                else if (temp.getToken() == 14)//O
                {
                    match(14);
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

        private void COMPARACION()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 23)//!=
                {
                    match(23);
                }else if (temp.getToken() == 24)//=
                {
                    match(24);
                }
                else if (temp.getToken() == 25)//<=
                {
                    match(25);
                }
                else if (temp.getToken() == 26)//<
                {
                    match(26);
                }
                else if (temp.getToken() == 27)//>=
                {
                    match(27);
                }
                else if (temp.getToken() == 28)//>
                {
                    match(28);
                }
                else if (temp.getToken() == 29)//!
                {
                    match(29);
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

        private void TABLAS()
        {
            match(37);//ID
            TABLAS_();
        }

        private void TABLAS_()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)//,
                {
                    match(19);//,
                    match(37);//ID
                    TABLAS_();

                }

                else
                {
                    //EPSILON
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECIONAR",temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void COLUMNAS()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if(temp.getToken()==37)//id
                {
                    match(37);//ID
                    match(30);//.
                    ID();
                    ALIAS();
                    COLUMNAS_();

                }
                else if (temp.getToken() == 18)//*
                {
                    match(18);
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

        private void COLUMNAS_()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)//,
                {
                    match(19);//,
                    match(37);//ID
                    match(30);//.
                    ID();
                    ALIAS();
                    COLUMNAS_();

                }
                
                else
                {
                   //EPSILON
                }

            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECIONAR", temp.getLexema(),temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void ID()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 18)//18
                {
                    match(18);//COMO
                    
                }
                else if (temp.getToken() == 37)//ID
                {
                    match(37);//ID
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

        private void ALIAS()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 10)
                {
                    match(10);//COMO
                    match(37);//ID
                }
                else
                {
                    //Epsilon
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion SELECCIONAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        private void E()
        {
            match(15);//ELIMINAR
            match(11);//DE
            match(37);//ID
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 12)
                {
                    CONDICIONES();
                }
                else
                {
                    //Epsilon
                }


            }
            
            
        }

        private void A()
        {
            match(16);//ACTUALIZAR
            match(37);//ID
            match(17);//ESTABLECER
            match(21);//(
            ESTABLECER_();
            match(22);//)
            CONDICIONES();

        }

        private void ESTABLECER_()
        {
            match(37);//ID
            match(24);//=
            VALOR();
            ESTABLECER__();


        }

        private void ESTABLECER__()
        {

            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)
                {
                    match(19);//,
                    match(37);//ID
                    match(24);//=
                    VALOR();
                    ESTABLECER__();

                }
                else
                {
                    //Epsilon
                }


            }
            else
            {
                Token temp = (Token)tokens[tokens.Count - 1];
                errores.Add(new Error("Error sintactico", "No se completo instruccion ACTUALIZAR", temp.getLexema(), temp.getFila(), temp.getColumna()));
                recuperar();
            }
        }

        public void C()
        {
            match(4);//CREAR
            match(5);//TABLA
            match(37);//ID
            match(21);//(
            ATRIBUTOS();
            match(22);//)
            


        }

        private void ATRIBUTOS()
        {
            match(37);//ID
            TIPO();
            ATRIBUTOS_();
        }

        private void TIPO()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 0)//ENTERO
                {
                    match(0);
                }
                else if (temp.getToken() == 1)//CADENA
                {
                    match(1);
                }
                else if (temp.getToken() == 2)//FLOTANTE
                {
                    match(2);
                }
                else if (temp.getToken() == 3)//FECHA
                {
                    match(3);
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

        private void ATRIBUTOS_()
        {
            if (posicion <= tokens.Count - 1)
            {
                Token temp = (Token)tokens[posicion];
                if (temp.getToken() == 19)
                {
                    match(19);//,
                    match(37);//ID
                    TIPO();
                    ATRIBUTOS_();
                }
                else
                {
                    //EPSILON
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
