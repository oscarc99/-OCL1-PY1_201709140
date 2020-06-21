using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Atributo
    {
        //INDICE DEL TIPO DE DATO
        //ENTERO   0
        //CADENA   1
        //FLOTANTE 2
        //FECHA    3
        int tipo;
        //Nombre del atributo
        String nombre;
        //VALOR A GUARDAR
        String cadena;
        double flotante;
        int entero;
        DateTime fecha;
        //Constructor
        public Atributo()
        {

        }

        public Atributo(String nombre,int type)
        {
            this.nombre = nombre.ToUpper();
            this.tipo = type;
        }

        public Atributo(String nombre, int type, String dato)
        {
            this.nombre = nombre.ToUpper();
            this.tipo = type;
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
        public int getTipo()
        {
            return tipo;
        }
        public String getNombre()
        {
            return nombre.ToUpper(); 
        }
        private String convertir(string dato)
        {
            String valor = "";
            for(int i= 1;i < dato.Length-1; i++)
            {
                valor += dato[i].ToString();
            }
            return valor;
        }

        public void setAtribute(String dato)
        {
            if (tipo == 0)
            {
                entero = Int32.Parse(dato);
            }
            else if (tipo == 1)
            {
                cadena = convertir(dato);
            }
            else if (tipo == 2)
            {
                flotante = Convert.ToDouble(dato);
            }
            else if (tipo == 3)
            {
                fecha = Convert.ToDateTime(convertir(dato));
            }
        }

        public Object getValor()
        {
            if (tipo == 0)
            {
                return entero;
            }
            else if (tipo == 1)
            {
                return cadena;
            }
            else if (tipo == 2)
            {
                return flotante;
            }
            else if (tipo == 3)
            {
                return fecha;
            }
            return null;
        }

    }
}
