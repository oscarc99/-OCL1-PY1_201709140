using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Token
    {
        private int token;
        private String lexema;
        private String descripcion;
        private int fila;
        private int columna;

        public Token()
        {

        }

        public Token(int token, String lexema, String descripcion, int fila, int columna)
        {
            this.token = token;
            this.lexema = lexema;
            this.descripcion = descripcion;
            this.fila = fila;
            this.columna = columna;
        }

        public int getToken()
        {
            return this.token;
        }

        public String getLexema()
        {
            return this.lexema;
        }

        public String getDescrp()
        {
            return descripcion;
        }

        public int getFila()
        {
            return this.fila;
        }

        public int getColumna()
        {
            return this.columna;
        }



    }
}
