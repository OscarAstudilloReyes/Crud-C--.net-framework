using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_BD_Aprendices.Modelo
{
    class DatosAprendices
    {

        private static SqlConnection miConexion;
        private string mensaje;
        private SqlCommand comando;
        /// <summary>
        /// inicalizamos el constructor
        /// </summary>
        public DatosAprendices()
        {
            miConexion = Conexion.getconexion();
            comando = new SqlCommand();
        }

        public string Mensaje { get => mensaje;}

        /// <summary>
        /// metodo que agrega los registros
        /// </summary>
        /// <param name="unA"></param> objeto de tipo Aprendiz  
        /// <returns>true o false dependiendo si se puede agregar </returns>
        public bool agregar(Aprendices unA)
        {
            bool agregado = false;
            try
            {
                //limpiar los parametros para no guarde los anteriores parametros
                comando.Parameters.Clear();
                //agregamos las entecnias
                comando.Connection = miConexion;
                //hacemos la consulta sql
                comando.CommandText = "INSERT INTO aprendices VALUES " +
                    "(@identifiacion,@nombre,@apellido,@genero,@correo,@fecha)";

                comando.Parameters.AddWithValue("@identifiacion", unA.Identificacion);
                comando.Parameters.AddWithValue("@nombre", unA.Nombre);
                comando.Parameters.AddWithValue("@apellido", unA.Apellido);
                comando.Parameters.AddWithValue("@genero", unA.Genero);
                comando.Parameters.AddWithValue("@correo", unA.Correo);
                
                comando.Parameters.AddWithValue("@fecha", unA.Fecha);
                //metodo para agregar registros y Actualizar
                comando.ExecuteNonQuery();
                agregado=true;
                //si es agregado 
                if (agregado)
                {
                    mensaje = "Agregado Correctamente ";
                }
               
                //si no 
            }
            catch (SqlException ex)
            {
                mensaje ="Problemas ! "+ ex.Message;
                
            }
           
            return agregado;

        }
        /// <summary>
        /// metodo que consulta deacuerdo ala identificacion
        /// 
        /// </summary>
        /// <param name="identifica"></param>
        /// <returns>objeto de aprendiz</returns>
        public Aprendices consultar(string identifica)
        {
            //inicializamos el constructor null
            Aprendices unA = null;
            this.mensaje = "";
            try
            {
                //limpiar los parametros para no guarde los anteriores parametros
                comando.Parameters.Clear();
                //hacemos la consulta
                comando.Connection = miConexion;
                comando.CommandText = "SELECT * FROM aprendices WHERE aprIdentificacion=@identifica";
                comando.Parameters.AddWithValue("@identifica", identifica);
                //creamo el objeto que lee los registro y lo ponemos en las cajitas de textox
                SqlDataReader datos = comando.ExecuteReader();
                //lee los registros
                if (datos.Read())
                {
                    unA = new Aprendices();
                    unA.Id = Convert.ToInt32(datos["idAprendiz"]);
                    unA.Identificacion = datos["aprIdentificacion"].ToString();
                    unA.Nombre = datos["aprNombre"].ToString();
                    unA.Apellido = datos["aprApellido"].ToString();
                    unA.Genero = datos["aprGenero"].ToString();
                    unA.Correo = datos["aprCorreo"].ToString();
                    unA.Fecha = Convert.ToDateTime(datos["aprFecha"]);

                }
                datos.Close();
                
            }
            catch (Exception ex)
            {
                mensaje= "problemas al consultar"+ex.Message;
                
            }

            return unA;

        }

        public List<Aprendices> listaAprendices()
        {
            List<Aprendices> listar = new List<Aprendices>();

            mensaje = "";
            try
            {
             

                comando.Connection = miConexion;
                comando.CommandText = "SELECT * FROM aprendices";
                SqlDataReader datos = comando.ExecuteReader();

                while (datos.Read())
                {
                   Aprendices unA = new Aprendices();
                    unA.Id = Convert.ToInt32(datos["idAprendiz"]);
                    unA.Identificacion = datos["aprIdentificacion"].ToString();
                    unA.Nombre = datos["aprNombre"].ToString();
                    unA.Apellido = datos["aprApellido"].ToString();
                    unA.Genero = datos["aprGenero"].ToString();
                    unA.Correo = datos["aprCorreo"].ToString();
                    unA.Fecha = Convert.ToDateTime(datos["aprFecha"]);
                    listar.Add(unA);
                  
                }
                datos.Close();
            }
            catch (SqlException ex)
            {
                this.mensaje = ex.Message;
                
            }



            return listar;
        }

        public bool Actualizar(Aprendices unA)
        {
            this.mensaje = "";
            bool actualizado = false;
            try
            {
                comando.Parameters.Clear();
                comando.Connection = miConexion;
                //Sentecia Sql
                comando.CommandText = "UPDATE aprendices SET " +
                    "aprIdentificacion=@identifica," +
                    "aprNombre=@nombre," +
                    "aprApellido=@apellido," +
                    "aprGenero=@genero," +
                    "aprCorreo=@correo," +
                    "aprFecha=@fecha " +
                    "where idAprendiz=@Id";

                comando.Parameters.AddWithValue("@identifica", unA.Identificacion);
                comando.Parameters.AddWithValue("@nombre", unA.Nombre);
                comando.Parameters.AddWithValue("@apellido", unA.Apellido);
                comando.Parameters.AddWithValue("@genero", unA.Genero);
                comando.Parameters.AddWithValue("@correo", unA.Correo);
                comando.Parameters.AddWithValue("@fecha", unA.Fecha);
                //importante el ID
                comando.Parameters.AddWithValue("@Id", unA.Id);

                //metodo para agregar Actulizar los registros
                comando.ExecuteNonQuery();
                actualizado = true;
                if (actualizado)
                {
                    mensaje = "Actualizado con exito";
                }



            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;

            }
            return actualizado;
        }

        public  bool  Eliminar(string identifica)
        {

            bool eliminado = false;
            
            
            this.mensaje = "";

            try
            {
                comando.Parameters.Clear();
                comando.Connection = miConexion;
                comando.CommandText = "DELETE FROM aprendices WHERE aprIdentificacion =@identifica";
                //pasamos como argumento el paramentro identifica
                comando.Parameters.AddWithValue("@identifica", identifica);

                comando.ExecuteNonQuery();
                eliminado = true;
                mensaje = "Elimnado con exito";
                


            }
            catch (SqlException ex)
            {

                mensaje = ex.Message;
            }
            return eliminado;
        }

        public int Cantidad()
        {
            int x = 0;
            this.mensaje = "";
            try
            {
                comando.Parameters.Clear();
                comando.Connection = miConexion;

                comando.CommandText = "SELECT COUNT(idAprendiz) FROM aprendices";
                x =Convert.ToInt32(comando.ExecuteScalar());

                
            }
            catch(SqlException ex)
            {
                mensaje = ex.Message;
            }

            return x;
        }
    }
}
