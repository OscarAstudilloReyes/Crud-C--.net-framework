using CRUD_BD_Aprendices.Modelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_BD_Aprendices.Controlador
{
    class controlador_Aprendiz
    {

        private DatosAprendices dA = new DatosAprendices();
        private string mensaje;

        public string Mensaje { get => mensaje;  }


        public bool agregar(FrmPrincipal fp  )
        {
            mensaje = "";
            string identifiacion = fp.txtidentidad1.Text;
            string nombre = fp.txtnombre.Text;
            string apellido = fp.txtapellido.Text;
            string genero = fp.cbgenero.SelectedItem.ToString();
            string correo = fp.txtcorreo.Text;
            
            DateTime fecha  = fp.date.Value;

            Aprendices unA = new Aprendices(identifiacion, nombre, apellido, genero,correo, fecha);

            bool agregado = dA.agregar(unA);
            if (agregado)
            {
                //agrega la foto , ruta de la foto , si tienes una carpeta especial indicarle aca
                File.Copy(fp.rutaFoto, identifiacion + ".jpg");
                mensaje = dA.Mensaje;

            }
            else
            {
                mensaje = dA.Mensaje;
            }

            return agregado;

        }
        //limpia la cajas de textos si no existe el aprendiz
        private void limpiar(FrmPrincipal fp)
        {
            fp.txtID.Clear();
            fp. txtidentidad1.Clear();
            fp.txtapellido.Clear();
            fp.txtnombre.Clear();
            fp.txtapellido.Clear();
            //se la añade el texto "Seleccione"
            fp.cbgenero.Text = "Seleccione";
            fp.txtcorreo.Clear();
            //le mandamos la fecha de hoy
            fp.date.Value = DateTime.Now;
            fp.ptImagen.Image = CRUD_BD_Aprendices.Properties.Resources.toma_una_foto;
        }
        public void consultar(FrmPrincipal fp)
        {
            mensaje = "";
           

            string identi = fp.txtidentidad1.Text;
            Aprendices unA = dA.consultar(identi);
            //si es diferente a null 
            if (unA!=null)
            {
                fp.txtID.Text = Convert.ToString(unA.Id);
                fp.txtnombre.Text = unA.Nombre;
                fp.txtapellido.Text = unA.Apellido;
                fp.cbgenero.Text = unA.Genero;
                fp.txtcorreo.Text = unA.Correo;
                //cb  trayendome el text del combobox
                
                fp.date.Value = Convert.ToDateTime(unA.Fecha);
                //preguntamos si el archivo existe
                if (File.Exists(unA.Identificacion+".jpg"))
                {

                    this.mensaje = "USUARIO LISTO";

                    //COLOQUEME EL EL pictobox coloca la imagen
                    fp.ptImagen.Image = Image.FromFile(unA.Identificacion + ".jpg");
                }
                else
                {
                    //si no coloca la foto anterior
                    fp.ptImagen.Image = CRUD_BD_Aprendices.Properties.Resources.toma_una_foto;
                }


               
            }
            else
            {
                this.mensaje = "no se encontro aprendiz";
                limpiar(fp);
            }
           

        }
        public void listar(FrmPrincipal fp)
        {

            fp.Table_1.DataSource = null;
            fp.Table_1.DataSource = dA.listaAprendices();
            fp.txt_Cantidad.Text =Convert.ToString( dA.Cantidad());
            
            //establesco la colimna del ID el ancho en 30
            //fp.Table_1.Columns[0].Width = 30;
            fp.Table_1.Columns[0].Visible = false;
        }

        public void Actualizar(FrmPrincipal fp)
        {
            mensaje = "";
            int ID = Convert.ToInt32(fp.txtID.Text);
            //string ID = Convert.ToString(fp.txtID.Text);
            string identifiacion = fp.txtidentidad1.Text;
            string nombre = fp.txtnombre.Text;
            string apellido = fp.txtapellido.Text;
            string genero = fp.cbgenero.Text;
            string correo = fp.txtcorreo.Text;
  
            DateTime fecha = fp.date.Value;

           

            Aprendices unA = new Aprendices(identifiacion, nombre, apellido, genero,correo, fecha);

            unA.Id = Convert.ToInt32(ID);
            bool actulizado = dA.Actualizar(unA);
            mensaje = dA.Mensaje;

        }

        public void Eliminar(FrmPrincipal fp)

        {
            mensaje = "";
            string id = fp.txtidentidad1.Text;
            bool eliminado = dA.Eliminar(id);
            if (eliminado)
            {
              
                mensaje = dA.Mensaje;
            }
            else
            {
                mensaje = dA.Mensaje;
            }
           
           
          


        }

        public void Contar(FrmPrincipal fp )
        {
            fp.txt_Cantidad.Text =  dA.Cantidad().ToString();


        }
    }
}
