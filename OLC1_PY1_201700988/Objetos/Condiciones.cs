using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Condiciones
    {
        bool estado = false;
        ArrayList condiciones = new ArrayList();

        public Condiciones()
        {
            estado = false;
            condiciones = new ArrayList();
        }

        public bool getEstado()
        {
            foreach(Condicion ct in condiciones)
            {
                if (!ct.getEstado())
                {
                    
                    return false;
                }
            }

            return true;
        }

        public void revisar()
        {

        }

        public void setEstado(bool e)
        {
            this.estado = e;
        }


        public Condiciones(ArrayList condiciones)
        {
            this.condiciones = condiciones;
            estado = false;

        }
        public ArrayList getCondiciones()
        {
            return this.condiciones;
        }


        public void addC(Condicion c)
        {
            this.condiciones.Add(c);
        }

        public int size()
        {
            return this.condiciones.Count;
        }

        public void falso()
        {
            foreach (Condicion con in condiciones)
            {
                con.setEstado(false);
            }
           
        }
    }
}
