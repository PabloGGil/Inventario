using System;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;



namespace InventarioAsset
{
    public partial class frmEtiquetas : Form
    {
        //OleDbConnection cn = new OleDbConnection();
        //cn = InventarioAsset.BaseDatos.Conectar();
        //string query;
        //string queryDGV;
        public frmEtiquetas()
        {
            InitializeComponent();
        }

        private void frmEtiquetas_Load(object sender, EventArgs e)
        {

            try
            {
                lvw.View = View.Details;
                lvw.GridLines = true;
                lvw.FullRowSelect = true;

                //Add column header
                lvw.Columns.Add("Inventario", 100);
                lvw.Columns.Add("Tipo", 70);
                lvw.Columns.Add("Marca", 70);
                lvw.Columns.Add("Modelo", 70);
                lvw.Columns.Add("Serie", 70);

                //lstInv.DataSource = Global.TodosLosAsset.GetEquipos();
                //lstInv.DisplayMember = "ID_Inv";
                //lstInv.ValueMember = "ID_Inv";

                //lstInv.DataSource = dt;
                //queryDGV = "SELECT * FROM TMPEQUIPOS";
                //dt = InventarioAsset.BaseDatos.RegistrosTabla(cn, queryDGV);
                //dgv.DataSource = dt;

                //cn.Close();
            }catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
            try{
                string xin = "";
            //int cont = 0;
            List<string> ls = new List<string>();
            List<EquipoExt> eq = new List<EquipoExt>();
            ls.Add(txtInv.Text);
            eq = Global.TodosLosAsset.GetListaEquipos(ls);
            ListViewItem itm;
            if (eq.Count != 0)
            {
                itm = new ListViewItem(eq[0].ID_Inv);
                //itm.SubItems.Add(eq[0].ID_Inv);
                itm.SubItems.Add(eq[0].DESCRIPCION);
                itm.SubItems.Add(eq[0].MARCA);
                itm.SubItems.Add(eq[0].MODELO);
                itm.SubItems.Add(eq[0].SERIE);
                //lvw.Items.Add(itm); productName = lvw.SelectedItems[0].SubItems[0].Text;
                //price = lvw.SelectedItems[0].SubItems[1].Text;
                //quantity = lvw.SelectedItems[0].SubItems[2].Text;
                lvw.Items.Add(itm);
            }
            else
            {
                MessageBox.Show("No se encontró el nro de inventario " + txtInv.Text);
            }
            // Agrega registro a la tabla TmpEquipos
            //for (int x = 0; x < lstInv.Items.Count; x++)
            //{
            //    // Determine if the item is selected.
            //    if (lstInv.GetSelected(x) == true)
            //    {
            //        if (cont == 0)
            //        {
            //            xin = lstInv.GetItemText(lstInv.SelectedValue);// lstInv.GetItemText(lstInv.GetSelected(x));
            //            cont++;
            //        }

            //        xin = xin + "," + lstInv.GetItemText(lstInv.SelectedValue);//lstInv.GetItemText(lstInv(x));
            //    }
            // Deselect all items that are selected.
            Console.WriteLine(xin);
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }

        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvw.SelectedItems.Count; i++)
            {
                lvw.Items.Remove(lvw.SelectedItems[i]);
            }
        }
       



        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            try{
                frmReportes newMDIChild = new frmReportes();
                List<Etiqueta> et = new List<Etiqueta>();
                Etiqueta xet = new Etiqueta();
                for(int x = 0; x < lvw.Items.Count; x++)
                {
                    xet.inv = lvw.Items[x].SubItems[0].Text;
                    xet.descripcion= lvw.Items[x].SubItems[1].Text;
                    xet.serie= lvw.Items[x].SubItems[4].Text;
                    et.Add(xet);
                }
                newMDIChild.DataSource(et);
            
                ////    // Set the Parent Form of the Child window.
                ////newMDIChild.MdiParent = this;
                ////    // Display the new form.
                newMDIChild.Show();
                //}
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
    }
    public class Etiqueta
    {
        public string inv { get; set; }
        public string descripcion { get; set; }
        public string serie { get; set; }
    }
}
