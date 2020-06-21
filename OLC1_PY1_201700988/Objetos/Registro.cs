using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Registro
    {
        //ArrayList caracteristica;//Atributos de la tabla  //Titulos de columnas
        ArrayList atributos = new ArrayList();//Datos del registro

        public Registro()
        {
            atributos = new ArrayList();
        }

        public Registro(ArrayList caracteristica)
        {
            //this.caracteristica = caracteristica;
            atributos = new ArrayList();
        }

        public ArrayList getAtributo()
        {
            return atributos;

        }


        public void addAtribute(Atributo a)
        {
            atributos.Add(a);
        }

        public void clear()
        {

            atributos.Clear();
        }

        public int cantidad()
        {
            return atributos.Count;
        }


        public bool verificar(ArrayList condiciones)
        {
            //bool r=false;
            foreach (Atributo at in atributos)
            {

            }
            return true;
        }

        
        

    }
}
