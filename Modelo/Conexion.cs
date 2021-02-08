using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_BD_Aprendices.Modelo
{
    class Conexion
    {
        public static SqlConnection miConexion;
        private static string mensaje;

        public static string Mensaje { get => mensaje; }

        /// <summary>
        /// metodo que devuelve la conecion 
        /// </summary>
        /// <returns></returns>
        public static SqlConnection getconexion()
        {
            if (miConexion!=null)
            {
                return miConexion;
            }
            //llamamos la conexion 
            miConexion = new SqlConnection();
            //hacemos la conexion con la base de datos mediante el nombre del servidor
            miConexion.ConnectionString = "Data Source=DESKTOP-3F546N0\\SQLEXPRESS;" +
                " Initial Catalog=GestionAprendices;" +
                " Integrated Security=True";
           
            try
            {
                miConexion.Open();
                return miConexion;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// cerrar conexion
        /// </summary>
        public void close()
        {
            miConexion.Close();
        }



    }
}
