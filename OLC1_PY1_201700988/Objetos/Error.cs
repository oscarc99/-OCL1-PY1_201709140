using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Error
    {
        private String tipo;
        private String error;
        private String descripcion;
        private int fila;
        private int columna;

        public Error(string tipo, string error, int fila, int columna)
        {
            this.tipo = tipo;
            this.error = error;
            this.fila = fila;
            this.columna = columna;
        }
        public Error(string tipo, String descripcion, String error, int fila, int columna)
        {
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.error = error;
            this.fila = fila;
            this.columna = columna;
        }

        public Error()
        {
        }

        public String getTipo()
        {
            return this.tipo;
        }

        public String getError()
        {
            return this.error;
        }

        public int getFila()
        {
            return this.fila;
        }
        public String getDescipcion()
        {
            return this.descripcion;
        }

        public int getColumna()
        {
            return this.columna;
        }

        public void setTipo(String tipo)
        {
            this.tipo = tipo;
        }

        public void setError(String error)
        {
            this.error = error;
        }

        public void setFila(int fila)
        {
            this.fila = fila;
        }

        public void setDescipcion(String descripcion)
        {
            this.descripcion = descripcion;
        }
    }
}
