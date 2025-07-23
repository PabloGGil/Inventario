using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioAsset
{
    public partial class frmFiltro : Form
    {
        public frmFiltro()
        {
            InitializeComponent();
           
        }

        public void CargarEstado()
        {
            JsonStatuses js = new JsonStatuses();         
            js = js.GetJ();
            List<Status> lstorigen = new List<Status>();
            lstorigen = js.coleccion.ToList();
            cmbEstado.DataSource = lstorigen;
            cmbEstado.ValueMember = "ID";
            cmbEstado.DisplayMember = "DESCRIPCION";

            cmbEstado.SelectedItem = cmbEstado.Items[0].ToString();
        }
        private void frmFiltro_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Refresco rx = new Refresco();
            Refresco.RefrescarLocal();
            Cursor.Current = Cursors.Default;
            cmbTipo.DisplayMember = "Tipo";
            cmbTipo.DataSource = Global.xc.getSegxEquipo();
            cmbTipo.SelectedItem = 0;
            CargarEstado();
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMarca.SelectedItem == null || cmbTipo.SelectedItem == null)
                {
                    cmbModelo.DataSource = null;
                }
                else
                {
                    cmbModelo.DataSource = Global.TodosLosAsset.getModeloxMarca(cmbMarca.SelectedItem.ToString(), cmbTipo.SelectedItem.ToString());
                    cmbModelo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipo.SelectedItem.ToString() == null)
                {
                    cmbMarca.DataSource = null; 
                }
                else
                {
                    cmbMarca.DataSource = Global.TodosLosAsset.getMarcaxTipo(cmbTipo.SelectedValue.ToString());
                    cmbMarca.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void cmbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cmbMarca.SelectedItem == null || cmbTipo.SelectedItem == null)
            //    {
            //        cmbModelo.DataSource = null;
            //    }
            //    else
            //    {
            //        cmbModelo.DataSource = Global.TodosLosAsset.getModeloxMarca(cmbMarca.SelectedItem.ToString(), cmbTipo.SelectedItem.ToString());
            //        //cmbModelo.SelectedIndex = 0;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    InventarioAsset.ELog.save(this, ex);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EquipoExt ext = new EquipoExt();
            ext.DESCRIPCION = cmbTipo.SelectedItem.ToString();
            ext.MODELO = cmbModelo.SelectedItem.ToString();
            ext.MARCA = cmbMarca.SelectedItem.ToString();
            ext.Puesto = txtPuesto.Text;
            if (chkEstado.Checked)
            {
                ext.ID_Estado = cmbEstado.SelectedValue.ToString();
            }
            else 
            {
                ext.ID_Estado ="";
            }


                dgv.DataSource = Global.TodosLosAsset.getEquiposfiltro(ext);
            //dgv.DataSource = Global.TodosLosAsset.getEquiposfiltro(ext);
            lblCantidad.Text= label5.Text + dgv.RowCount+" Registros";
            Console.WriteLine(dgv.DataSource);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
     

     
    }
}
