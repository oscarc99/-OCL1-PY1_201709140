using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class actualizar
    {

        //Guardo el valor con comillas (fecha/cadena)
        String columna;
        String valor;
        public actualizar()
        {

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

        public actualizar(String col, String cal)
        {
            this.columna = col.ToUpper();
            this.valor = cal;
        }

        public String getCol()
        {
            return columna;
        }

        public String getVal()
        {
            return valor;
        }

        public void setCol(String c)
        {
            this.columna = c;

        }

        public void setValor(String v)
        {
            this.valor = v;

        }

    }
}
