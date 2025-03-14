using System;
using System.Data;
//using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
//using CAD_Inv;
//using CL_Inv;

namespace InventarioAsset
{
    //private int pmodo;

    public partial class frmPedido : Form
    {
        private string _IDSel { get; set; }
        //DataTable dt = new DataTable();
        //SqlConnection csql = new SqlConnection();
        string Solicitante = Global.SeguridadUsr.usuario.USER_ID;
        public frmPedido()
        {
            InitializeComponent();
        }




        private void frmPedido_Load(object sender, EventArgs e)
        {
            //string query;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbEquipo.DisplayMember = "Tipo";
               

                cmbEquipo.DataSource = Global.xc.getSegxEquipo();
                cmbEquipo.SelectedItem = "CPU";
                //csql = InventarioAsset.BaseDatos.ConectarSQL();

               
                Cursor.Current = Cursors.Default;
                this.RefrescarDGV();
            }
            catch(Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                Cursor.Current = Cursors.Default;
            }
        }

        private void RefrescarDGV()
        {
            try
            {
                Pedidos px = new Pedidos();
                dgv.DataSource = px.ListarPedido(Solicitante);
                dgv.Columns["ID"].Visible = false;

            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                Cursor.Current = Cursors.Default;
            }
        }
        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            //int ID;
            try
            {
                if ((String.IsNullOrEmpty(txtOS.Text)) && (String.IsNullOrEmpty(txtComentarios.Text)))
                {
                    MessageBox.Show("Debe ingresar una OS ó un comentario");
                    return;
                }
            

                // --- Validar si hay equipos en pañol
                List<EquipoExt> x = new List<EquipoExt>();
                x = Global.TodosLosAsset.getInvxCaractEnPan(cmbMarca.Text, cmbModelo.Text, cmbEquipo.Text);
                if (x.Count == 0)
                {
                    MessageBox.Show("No hay equipos disponibles");
                    return;
                }
                //--- Validar si el solicitante tiene mas de 5 equipos pedidos


               
                RetCode rc = new RetCode();
                Pedidos np = new Pedidos();
              
                np.os = txtOS.Text;
                np.marca = cmbMarca.Text;// "El polaquito";
                np.modelo = cmbModelo.Text;// "";// "el puesto de sarrsami";
                np.equipo = cmbEquipo.Text;// "";
                np.solicitante = Global.SeguridadUsr.usuario.USER_ID;
                np.Comentario = txtComentarios.Text;
                rc = np.AgregarPedido(np);
                if (Convert.ToInt32(rc.rc) != 0)
                {
                    MessageBox.Show("error en la carga");
                }
                this.RefrescarDGV();
                txtOS.Text = "";
                txtComentarios.Text = "";

            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }


        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _IDSel = dgv.CurrentRow.Cells["ID"].Value.ToString();
                if (dgv.CurrentRow.Cells["OS"].Value == null)
                { 
                    txtOS.Text = "";
                }
                else {
                        txtOS.Text = dgv.CurrentRow.Cells["OS"].Value.ToString();
                    }
                cmbEquipo.Text = dgv.CurrentRow.Cells["Equipo"].Value.ToString();
                cmbMarca.Text = dgv.CurrentRow.Cells["Marca"].Value.ToString();
                cmbModelo.Text = dgv.CurrentRow.Cells["Modelo"].Value.ToString();
            }
            catch ( Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void cmdQuitar_Click(object sender, EventArgs e)
        {
           

            Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (_IDSel !="")
            {

              
                RetCode rc = new RetCode();
                Pedidos np = new Pedidos();
                np.Id = _IDSel;
                rc = np.BorrarPedido(np);
                if (Convert.ToInt32(rc.rc) != 0)
                {
                    MessageBox.Show("error en la carga");
                }
                _IDSel = "";
            }
            RefrescarDGV();
        }

       // string url = "https://metrogas.atlassian.net/jira/servicedesk/projects/MDA/queues/custom/33/MDA-" + txtOS.Text;

        private void btnAbrirExp_Click(object sender, EventArgs e)
        {
            string target = "https://metrogas.atlassian.net/jira/servicedesk/projects/MDA/queues/custom/33/MDA-";
            //Use no more than one assignment when you test this code.
            //string target = "ftp://ftp.microsoft.com";
            //string target = "C:\\Program Files\\Microsoft Visual Studio\\INSTALL.HTM";
            try
            {
                System.Diagnostics.Process.Start(target+txtOS);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }


        private void cmbMarca_DropDownClosed(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cmbMarca.SelectedItem == null || cmbEquipo.SelectedItem == null)
            //    {
            //        cmbModelo.DataSource = null;
            //    }
            //    else
            //    {
            //        cmbModelo.DataSource = Global.TodosLosAsset.getModeloxMarca(cmbMarca.SelectedItem.ToString(), cmbEquipo.SelectedItem.ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    InventarioAsset.ELog.save(this, ex);
            //}
        }
    


        private void cmbEquipo_DropDownClosed(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cmbEquipo.SelectedItem.ToString() == null)
            //    {
            //        cmbModelo.DataSource = null;
            //    }
            //    else
            //    {
            //        cmbMarca.DataSource = Global.TodosLosAsset.getMarcaxTipo(cmbEquipo.SelectedValue.ToString());
            //    }
            //}catch(Exception ex)
            //{
            //    InventarioAsset.ELog.save(this, ex);
            //}
        }

        private void cmdEditar_Click(object sender, EventArgs e)
        {
            try
            {
                RetCode rc = new RetCode();
                Pedidos np = new Pedidos();
                //np.q = "addPedido";
                //np.info.ID = "77";

                np.os = txtOS.Text;
                np.marca = cmbMarca.Text;// "El polaquito";
                np.modelo = cmbModelo.Text;// "";// "el puesto de sarrsami";
                np.equipo = cmbEquipo.Text;// "";
                np.solicitante = Global.SeguridadUsr.usuario.USER_ID;
                np.Comentario = txtComentarios.Text;
                rc = np.EditarPedido(np, _IDSel);
                if (Convert.ToInt32(rc.rc) != 0)
                {
                    MessageBox.Show("error en la carga");
                }
                this.RefrescarDGV();
                _IDSel = "";
                txtOS.Text = "";
                txtComentarios.Text = "";


            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void cmbEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEquipo.SelectedItem.ToString() == null)
                {
                    cmbModelo.DataSource = null;
                }
                else
                {
                    cmbMarca.DataSource = Global.TodosLosAsset.getMarcaxTipo(cmbEquipo.SelectedValue.ToString());
                    cmbMarca.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMarca.SelectedItem == null || cmbEquipo.SelectedItem == null)
                {
                    cmbModelo.DataSource = null;
                }
                else
                {
                    cmbModelo.DataSource = Global.TodosLosAsset.getModeloxMarca(cmbMarca.SelectedItem.ToString(), cmbEquipo.SelectedItem.ToString());
                    cmbModelo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
    }
}
