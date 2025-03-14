using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using CAD_Inv;
//using CL_Inv;

namespace InventarioAsset
{
    public partial class frmPuesto : Form
    {
        int boton;
        Puesto paux = new Puesto();
        public frmPuesto()
        {
            InitializeComponent();
        }

        public void refrescar()
        {
            Puesto p = new Puesto();
            List<Puesto> aux = new List<Puesto>();
            aux= p.ListarPuestosActivos();
            dgv.DataSource = aux;
            dgv.Columns["ID"].Visible = false;
        }

        private void frmPuesto_Load(object sender, EventArgs e)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            refrescar();
            dgv.Enabled = true;
            cmdBorrar.Enabled = false;
            cmdEditar.Enabled = false;
            cmdNuevo.Enabled = true;
            groupBox1.Enabled = false;
        }

        private void cmdSelUsr_Click(object sender, EventArgs e)
        {
            JSONUsr xusr = new JSONUsr();
            xusr = xusr.JSONget();
            frmData frm = new frmData();
            frm.Busqueda(true);
            frm.Text = "Seleccion de usuarios";
            frm.DataSource(xusr.coleccion.ToList());
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtResponsable.Text = frm.valorRetorno; //lee la propiedad
                ; //la pone en el título
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JSONUsr xusr = new JSONUsr();
            xusr = xusr.JSONget();
            frmData frm = new frmData();
            frm.Busqueda(true);
            frm.Text = "Seleccion de usuarios";
            frm.DataSource(xusr.coleccion.ToList());
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtAdmin.Text = frm.valorRetorno; //lee la propiedad
                ; //la pone en el título
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            groupBox1.Enabled = true;
            txtAdmin.Text =dgv.CurrentRow.Cells["Admin"].Value.ToString();
            txtComentario.Text = dgv.CurrentRow.Cells["Comentario"].Value is null ?"" : dgv.CurrentRow.Cells["Comentario"].Value.ToString();
            txtDescripcion.Text = dgv.CurrentRow.Cells["Descripcion"].Value is null ?"":dgv.CurrentRow.Cells["Descripcion"].Value.ToString();
            txtResponsable.Text = dgv.CurrentRow.Cells["Responsable"].Value is null ? "" : dgv.CurrentRow.Cells["Responsable"].Value.ToString();
            txtNombre.Text = dgv.CurrentRow.Cells["NombrePuesto"].Value is null ? "" : dgv.CurrentRow.Cells["NombrePuesto"].Value.ToString();
            btnGuardar.Enabled = true;
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            boton = 1;
            cmdEditar.Enabled = false;
            dgv.Enabled = false;
            cmdBorrar.Enabled = false;
            groupBox1.Enabled = true;
            btnGuardar.Enabled = true;
        }
        private void cmdEditar_Click(object sender, EventArgs e)
        {
            boton = 2;
            dgv.Enabled = false;
            cmdNuevo.Enabled =false;
            cmdBorrar.Enabled =false;
            txtNombre.Enabled = false;
            groupBox1.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void cmdBorrar_Click(object sender, EventArgs e)
        {
            boton = 3;
            dgv.Enabled = false;
            cmdEditar.Enabled =false;
            cmdNuevo.Enabled =false;
            groupBox1.Enabled = false;

        }
        public void Buscar(string Nombre)
        {
            
            
         }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Puesto p = new Puesto();
            RetCode rc;
            p.id = "";
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El nombre del puesto no puede ser nulo");
                return;
            }
            // Validacion- nombre de puesto no debe estar en blanco y debe tener formato establecido
            string puesto = txtNombre.Text;
            //puesto.Replace("\n", string.Empty).Replace(".", string.Empty).Replace(" ", string.Empty);
            string patern= @"(\w{3}-[a-zA-Z0-9]{1}-\d{3})";
            MatchCollection resultados = Regex.Matches(puesto, patern);
            
            if (resultados.Count==0)
            {
                MessageBox.Show( "error-Formato incorrecto");
                return;
            }
                        
            p.NombrePuesto = txtNombre.Text.ToUpper();
            p.Responsable = txtResponsable.Text;
            p.Descripcion = txtDescripcion.Text;
            p.Comentario = txtComentario.Text;
            p.Admin = txtAdmin.Text;
            p.Activo = "1";

            switch (boton)
            {
                case 1:
                    if (p.BuscarPuesto(txtNombre.Text)!=null)
                    {
                        MessageBox.Show("el puesto " + txtNombre.Text + "ya existe");

                    }// se Puesto //if (p.NombrePuesto==)
                    else
                    {
                        rc = p.AgregarPuestos(p);
                        if (rc.rc != "0")
                            MessageBox.Show("Error: no se pudo dar de alta el puesto. \n" + rc.msg);
                    }
                    break;
                case 2:
                    p.id = paux.id;
                    rc = p.EditarPuestos(p);
                    if (rc.rc != "0")
                        MessageBox.Show("Error: No se pudo editar el puesto.\n" + rc.msg);
                    break;

                case 3:
                    p.id = paux.id;
                    rc = p.BorrarPuestos(p);
                    if (rc.rc != "0")
                        MessageBox.Show("Error: No se pudo borrar el puesto. \n" + rc.msg);
                    break;
            }
            refrescar();
            dgv.Enabled = true;
            cmdBorrar.Enabled = false;
            cmdEditar.Enabled = false;
            cmdNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            txtNombre.Enabled = true;
            groupBox1.Enabled = false;
        }

        public void LimpiarText()
        {
            txtNombre.Text = "";
            txtResponsable.Text = "";
            txtDescripcion.Text = "";
            txtComentario.Text = "";
            txtAdmin.Text = "";
        }
        private void btncancelar_Click(object sender, EventArgs e)
        {
            dgv.Enabled = true;
            cmdBorrar.Enabled = false;
            cmdEditar.Enabled = false;
            cmdNuevo.Enabled = true;
            txtNombre.Enabled = true;
            groupBox1.Enabled = false;
            LimpiarText();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Puesto pauxx = new Puesto();
            pauxx = pauxx.BuscarPuesto(txtbuscar.Text.Trim());
            if (pauxx != null)
            {
                SelFilaDGV(txtbuscar.Text.Trim());
                txtNombre.Text = pauxx.NombrePuesto;
                txtResponsable.Text = pauxx.Responsable;
                txtDescripcion.Text = pauxx.Descripcion;
                txtComentario.Text = pauxx.Comentario;
                txtAdmin.Text = pauxx.Admin;
                groupBox1.Enabled = true;

            }
            else
            {
                MessageBox.Show("No se encontró el puesto " + txtbuscar.Text);
                LimpiarText();
            }
        }
        private void SelFilaDGV(string puesto)
        {
            //this.dgv.CurrentCell = dgv[1, 8];
            //this.dgv.CurrentCell.Selected = true;
            for (int i = 0; i < dgv.Rows.Count ; i++)
            {
                string txt = dgv.Rows[i].Cells["NombrePuesto"].Value.ToString();
                //Console.WriteLine(txt);
                if (txt != puesto)
                {
                    continue;
                }
                Console.WriteLine("encontré");
                //dgv.Rows[i].Cells["NombrePuesto"].Value;
                this.dgv.CurrentCell = dgv[1, i];
                this.dgv.CurrentCell.Selected = true;
               
                break;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            //txtbuscar.Text = txtbuscar.Text.ToUpper();
        }
    }
}
