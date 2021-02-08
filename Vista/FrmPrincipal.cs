using CRUD_BD_Aprendices.Controlador;
using CRUD_BD_Aprendices.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_BD_Aprendices
{
    public partial class FrmPrincipal : Form
    {
        private controlador_Aprendiz cA = new controlador_Aprendiz();
        public string rutaFoto;
        public bool fotoAsignada = false;
        public bool validarImg=false;
         

        public FrmPrincipal()
        {
            InitializeComponent();
            cA.listar(this);
            CajasVisibles(false);
            //establecer la columna id de ancho en 30

           //estableciendo un taga a la imagen
            //ptImagen.Image = Properties.Resources.toma_una_foto;
            //ptImagen.Tag = "toma_una_foto";


        }
        /// <summary>
        /// Retorna bool la visibilidad de la caja de texto del ID y el ID de la tabla
        /// </summary>
        /// <param name="visible"></param>
        public void CajasVisibles(bool visible)
        {
            this.txtID.Visible = visible;
            this.label8.Visible = visible;


        }




        private void button1_Click(object sender, EventArgs e)
        {
            //prueba para verificar si funciono o no la conexion ala bd
            SqlConnection obj = Conexion.getconexion();
            //si el obj es igual a vacio retorna el mesaje y no confirmamos la conexion
            if (obj == null)
            {
                MessageBox.Show(Conexion.Mensaje);
            }
            else
            {
                MessageBox.Show("conexion con exito ala base de datos gestionAprendices");
            }
        }

        private void btn_Registrar_Click(object sender, EventArgs e)
        {
           
           
            if (txtidentidad1.Text != string.Empty &&
                txtnombre.Text != string.Empty &&
                txtapellido.Text != string.Empty &&
                txtcorreo.Text != string.Empty &&
                cbgenero.SelectedIndex > 0 &&
                date.Value != null)
            {
               
                  //comrpobamos si agrego la foto correctamente
                    if (fotoAsignada)
                    {

                        cA.agregar(this);
                        MessageBox.Show(cA.Mensaje, "Agregar aprendiz");
                        limpiar();
                        cA.listar(this);
                    }
                    else
                    {
                        MessageBox.Show("Falta foto por seleccionar Fotografia");
                    }

            }
            else
            {
                MessageBox.Show("Cajas vacias intente otra vez!");
            }
        }

        private void limpiar()
        {
            txtID.Clear();
            txtidentidad1.Clear();
            txtapellido.Clear();
            txtnombre.Clear();
            txtapellido.Clear();
            cbgenero.SelectedIndex = 0;
            txtcorreo.Clear();
            //le mandamos la fecha de hoy
            date.Value = DateTime.Now;
            ptImagen.Image = CRUD_BD_Aprendices.Properties.Resources.toma_una_foto;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (txtidentidad1.Text != string.Empty)
            {
                cA.consultar(this);
                MessageBox.Show(cA.Mensaje);
            }
            else
            {
                MessageBox.Show("ingresa identificacion para buscar");
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (txtidentidad1.Text != string.Empty)
            {
                if (txtnombre.Text != string.Empty && txtcorreo.Text != string.Empty &&
                    txtapellido.Text != string.Empty && cbgenero.Text != "Seleccione")
                {
                    cA.Actualizar(this);
                    cA.listar(this);
                    MessageBox.Show(cA.Mensaje);
                }
                else
                {
                    MessageBox.Show("No puedes actualizar campos vacios!");
                }

            }
            else
            {
                MessageBox.Show("Consulte primero el aprendiz");
            }

        }

        private void btnElimiar_Click(object sender, EventArgs e)
        {
            if (txtidentidad1.Text != string.Empty)
            {
                DialogResult op = MessageBox.Show("Seguro que deseas eliminar aprendiz",
                    "Eliminar", MessageBoxButtons.YesNo);

                if (op == DialogResult.Yes)
                {
                    cA.Eliminar(this);
                    limpiar();
                    cA.listar(this);
                    MessageBox.Show(cA.Mensaje);


                }
            }
            else
            {
                MessageBox.Show("Consulte para eliminar");
            }
        }

        private void Table_1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Metodo que devuelve la fila con el identifiacion sea mayor a 17la pinta de color coral
            //foreach (DataGridViewRow row in Table_1.Rows)
            //{
            //    string dato = Convert.ToString(row.Cells[1].Value);


            //    if (Convert.ToInt32(row.Cells[0].Value)>=17)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Coral;
            //        //
            //    }
            //    else
            //    {
            //        // row.DefaultCellStyle.BackColor = Color.LightSalmon; 
            //        row.DefaultCellStyle.BackColor = Color.Aqua;

            //    }


            //}

            //Otra forma
            foreach (DataGridViewRow row in Table_1.Rows)
            {
                string dato = Convert.ToString(row.Cells[4].Value);

                if (dato == "Masculino")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(217, 91, 91);
                    //Esctablecer color ala celda en especifico
                    row.Cells[4].Style.BackColor = Color.FromArgb(219, 139, 85);
                    //Establece color ala letra
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    //Establecer color ala celda de la posicion
                    row.Cells[4].Style.ForeColor = Color.White;

                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(181, 235, 247);
                }




            }

        }
        private void Table_1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Table_1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void tabla_2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {




        }

        private void ptImagen_Click(object sender, EventArgs e)
        {
            fotoAsignada = false;
            if (openFileFoto.ShowDialog() == DialogResult.OK)
            {
                //el la varaible guado la ruta de lafoto
                rutaFoto = openFileFoto.FileName;
                //le colocamos la imamgem ala foto
                ptImagen.Image = Image.FromFile(openFileFoto.FileName);
                //comprobacion si escojio una foto
                fotoAsignada = true;
            }
            else
            {
                MessageBox.Show("no seleccionaste nada");
                fotoAsignada = false;
            }
        }

        private void Table_1_MouseClick(object sender, MouseEventArgs e)
        {
            //eventeo que aldar click la tabla se ponen los datos atomaticamente
            int fila = Table_1.CurrentRow.Index;
            //1. obtengo la fila  y despues se coloca ahi dependiendo de la identficacion
            txtidentidad1.Text = Table_1.Rows[fila].Cells[1].Value.ToString();
            //llamo el metodo que consulta los datos
            cA.consultar(this);
        }

        private void txtidentidad1_TextChanged(object sender, EventArgs e)
        {
            //Validar solo numeros 
            //if (System.Text.RegularExpressions.Regex.IsMatch(txtidentidad1.Text, "[^0-9]"))
            //{
            //    MessageBox.Show("Solo numeros por favor.");
            //    txtidentidad1.Text = txtidentidad1.Text.Remove(txtidentidad1.Text.Length - 1);
            //}

        }

        private void txtidentidad1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Validar solo numeros
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo numero por favor","Error"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {
            //otra forma mas corta

            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtnombre.Text, "^[a-zA-Z ]"))
            //{
            //    MessageBox.Show("Solo letras por favor");
            //    txtnombre.Text.Remove(txtnombre.Text.Length - 1);
            //}
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo letras por favor", "Error"
                   , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void txtapellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo letras por favor", "Error"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
       




        }
    }
}
