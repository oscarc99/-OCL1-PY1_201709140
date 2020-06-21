using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC1_PY1_201700988.Objetos
{
    class Tabla
    {
        String nombre;
        Registro caracteristica;
        ArrayList registros = new ArrayList();
        public Tabla()
        {

        }
        public void eliminar()
        {
            registros.Clear();
        }

        public void eliminar(int index)
        {
            registros.RemoveAt(index);
        }
        public Tabla(String nombre)
        {
            this.nombre = nombre.ToUpper(); 
            registros = new ArrayList();
        }
        public Tabla(String nombre, Registro caracteristica)
        {
            this.nombre = nombre.ToUpper(); ;
            this.caracteristica = caracteristica;
            registros = new ArrayList();
        }
        public void setCaracteristica(Registro r)
        {
            this.caracteristica = r;
        }

        public void addRegistro(Registro r)
        {
            registros.Add(r);
        }

        public ArrayList getRegistros()
        {
            return registros;
        }

        public String getNombre()
        {
            return nombre;
        }

        public int noRegistros()
        {
            return registros.Count;
        }
        public Registro getCaracteristicas()
        {
            return caracteristica;
        }

    }
}
