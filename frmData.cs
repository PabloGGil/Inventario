using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using CAD_Inv;

namespace InventarioAsset
{
    public partial class frmData : Form
    {
        string SearchCol = "";
        int indexsearch = 0;
        bool valor { get; set; }
        int indice { get; set; }
        public frmData()
        {
            InitializeComponent();
        }
        public string valorRetorno { get; set; }
        private void frmData_Load(object sender, EventArgs e)
        {
            valor = false;
            indice = 0;
            cmdAceptar.Enabled = false;
        }

        public void DataSource(List<DataMov> Lista)
        {
            dgv.DataSource = Lista;
            cmdAceptar.Visible = false;
        }

        public void DataSource(List<DatoCompra> Lista)
        {
            dgv.DataSource = Lista;
            cmdAceptar.Visible = false;
        }

        public void DataSource(List<string> Lista)
        {
            dgv.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.HeaderText = "Perfiles";
            column2.Name = "Texto";
            column2.DataPropertyName = "Valor";
            dgv.Columns.Add(column2);
            dgv.DataSource = Lista.Select(x => new { Valor = x }).ToList(); 
            cmdAceptar.Visible = false;
        }
        public void DataSource(JSONNotas Lista)
        {
            dgv.DataSource = Lista.coleccion;
            cmdAceptar.Visible = false;
        }

        public void DataSource(List<GyU> Lista)
        {
            dgv.DataSource = Lista;
            cmdAceptar.Visible = true;
            SearchCol = "USER_ID";
        }

        public void DataSource (List<Logs> Lista)
        {
            dgv.DataSource = Lista;
            cmdAceptar.Visible = false;
        }
        public void DataSource(Usuario Lista)
        {
            List<Usuario> x = new List<Usuario>();
            x.Add(Lista);
            dgv.DataSource = x;
            SearchCol = "USUARIO_ID";


        }
        public void DataSource(List<Usuario> x)
        {
           
            dgv.DataSource = x;
            SearchCol = "USUARIO_ID";

        }

        public void DataSource(List<Puesto> x)
        {

            dgv.DataSource = x;
            SearchCol = "NombrePuesto";

        }
        public void Busqueda(bool valor)
        {
           
                label1.Visible = valor;
                txtBuscar.Visible = valor;
                cmdAceptar.Visible = valor;
            
        }
        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            try
            {
//                valorRetorno = dgv.CurrentRow.Cells["USUARIO_ID"].Value.ToString();
                valorRetorno = dgv.CurrentRow.Cells[SearchCol].Value.ToString();

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            //foreach(DataGridViewRow Row in dgv.Rows)
            //{
            //    int strFila = Row.Index;
            //    string Valor = Convert.ToString(Row.Cells["USUARIO_ID"].Value);

            //    if (Valor.Contains( this.txtBuscar.Text))
            //    {
            //        dgv.Rows[strFila].DefaultCellStyle.BackColor = Color.Red;
            //    }
            //}
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //cmdBuscar_Click(sender, e);
                indexsearch=Busqueda(indexsearch+1);
                cmdAceptar.Enabled = true;
            }
        }

        public int Busqueda(int indice)
        {
            int i;
            //foreach (DataGridViewRow Row in dgv.Rows)
            try
            {
                for (i = indice; i < dgv.Rows.Count; i++)
                {
                    //DataGridViewRow Row= dgv.Rows
                    int strFila = i;
                    Console.WriteLine(i);
                    string Valor = Convert.ToString(dgv.Rows[i].Cells[SearchCol].Value).ToUpper();
                    //string Valor = Convert.ToString(dgv.Rows[i].Cells["USUARIO_ID"].Value);
                    if (Valor.Contains(txtBuscar.Text.ToUpper()))
                    {
                        dgv.CurrentCell = dgv.Rows[i].Cells[1];//= Row.Cells[strFila];
                        dgv.Rows[i].Selected = true;

                        indice++;
                        break;
                    }
                }
                if (i == dgv.Rows.Count)
                    MessageBox.Show("No hay mas coincidencias");
                return i;
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                return 0;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtBuscar.Text = Convert.ToString(dgv.Rows[e.RowIndex].Cells[SearchCol].Value);
                //txtBuscar.Text = Convert.ToString(dgv.Rows[e.RowIndex].Cells["USUARIO_ID"].Value);
                cmdAceptar.Enabled = true;
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
    }
}
