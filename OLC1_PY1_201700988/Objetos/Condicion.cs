using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Condicion
    {
        bool estado = false;
        //comparacion signo valor

        String tabla;
        String columna;//columa a comparar
        int comparacion;//Signo que se utilizara para comparar
        ///Valor
        ///Indice del tipo
        ///0 entero
        ///1 cadena
        ///2 flotante
        ///3 fecha
        ///5 tabla
        ///37 columna
        /// si es 37 
        /// tabla
        /// columna
        int tipoVal;
        //Valor independiente
        String cadena;
        double flotante;
        int entero;
        DateTime fecha;
        String tablaVal;
        String columnaVal;
        public bool esValor()
        {
            if(tablaVal.Equals("") && columnaVal.Equals("") || tablaVal==null && columnaVal == null)
            {
                return true;
            }
            return false;
        }

        public Condicion()
        {

        }
        public int getCompara()
        {
            return this.comparacion;
        }

        public void setEstado(bool e)
        {
            this.estado = e;
        }
        public String getColumna()
        {
            return this.columna;
        }
        public String getTabla()
        {
            return this.tabla;
        }

        public String getTablaVal()
        {
            return this.tablaVal;

        }

        public String getColumnaVal()
        {
            return this.columnaVal;
        }
        public Condicion(String tabla, String columna, int signo,  String tablaVal, String columnaVal, int type)//722692
        {
            this.tabla = tabla;
            this.columna = columna;
            this.comparacion = signo;
            this.tipoVal = type;
            this.tablaVal = tablaVal;
            this.columnaVal = columnaVal;
        }

        public Condicion(String tabla, String columna, int signo, int type, String tablaVal, String columnaVal)//722692
        {
            this.tabla = tabla;
            this.columna = columna;
            this.comparacion = signo;
            this.tipoVal = type;
            this.tablaVal = tablaVal;
            this.columnaVal = columnaVal;
        }
        

        public Condicion(String tabla, String columna, int signo, int type, String dato)
        {
            //unicamente si es un valor en especifico 
            tablaVal="";
            columnaVal="";
            this.tabla = tabla;
            this.columna = columna;
            this.comparacion = signo;
            this.tipoVal = type;
            if (type == 0)
            {
                entero = Int32.Parse(dato);
            }
            else if (type == 1)
            {
                cadena = convertir(dato);
            }
            else if (type == 2)
            {
                flotante = Convert.ToDouble(dato);
            }
            else if (type == 3)
            {

                fecha = Convert.ToDateTime(convertir(dato));
            }

        }

        public bool getEstado()
        {
            return estado;
        }
        public void setValor(int type, String dato)
        {
            if (type == 0)
            {
                entero = Int32.Parse(dato);
            }
            else if (type == 1)
            {
                cadena = convertir(dato);
            }
            else if (type == 2)
            {
                flotante = Convert.ToDouble(dato);
            }
            else if (type == 3)
            {

                fecha = Convert.ToDateTime(convertir(dato));
            }
        }
        public Object getValor()
        {
            if (tipoVal == 0)
            {
                return entero;
            }
            else if (tipoVal == 1)
            {
                return cadena;
            }
            else if (tipoVal == 2)
            {
                return flotante;
            }
            else if (tipoVal == 3)
            {
                return fecha;
            }
            return null;
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
    }
}
