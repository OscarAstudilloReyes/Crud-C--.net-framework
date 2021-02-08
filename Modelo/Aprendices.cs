using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_BD_Aprendices.Modelo
{
    class Aprendices
    {
        private int id;
        private string identificacion;
        private string nombre;
        private string apellido;
        private string genero;
        private string correo;
        private DateTime fecha;

        //getter y setter
        public int Id { get => id; set => id = value; }
        public string Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Genero { get => genero; set => genero = value; }
        public string Correo { get => correo; set => correo = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        /// <summary>
        /// constructor inicializando los parametros
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="genero"></param>
        /// <param name="correo"></param>
        /// <param name="fecha"></param>
        public Aprendices(string identificacion, string nombre, string apellido, string genero, string correo, DateTime fecha)
        {
            this.Identificacion = identificacion;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Genero = genero;
            this.Correo = correo;
            this.Fecha = fecha;
        }

        public Aprendices()
        {
        }
    }
}
