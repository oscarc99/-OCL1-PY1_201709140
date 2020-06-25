using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Seleccion
    {
        String alias;
        String columna;
        String tabla;

        public Seleccion()
        {
        }

        public Seleccion(string columna, string tabla)
        {
            this.columna = columna;
            this.tabla = tabla;
            alias = "";
        }

        public Seleccion(string alias, string columna, string tabla)
        {
            this.alias = alias;
            this.columna = columna;
            this.tabla = tabla;
        }

        public String getColumn()
        {
            return this.columna;
        }

        public String getAlias()
        {
            return this.alias;
        }

        public String getTabla()
        {
            return this.tabla;
        }

    }
}
