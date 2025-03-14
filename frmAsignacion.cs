using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
//using CAD_Inv;
//using CL_Inv;




namespace InventarioAsset
{
    public partial class frmAsignacion : Form
    {
        Refresco rx = new Refresco();
        private int pmodo;
        string Usuario = Global.SeguridadUsr.usuario.USER_ID;
        Pedidos paux = new Pedidos();
        public frmAsignacion()
        {
            InitializeComponent();
        }

        public void setModo(int modo)
        {
            pmodo = modo;
            if (pmodo == Constants.Asigna)
            {

                this.Text = "Asignacion de equipos";
                button1.Text = "Asigna";
            }
            if (pmodo == Constants.Aprueba)
            {
                this.Text = "Aceptación de equipos";
                button1.Text = "Acepta";

            }
        }

        public int getModo()
        {
            return pmodo;
        }
        private void frmAsignacion_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            rx.RefrescarLocal();
            Cursor.Current = Cursors.Default;
            if (getModo() == Constants.Asigna)
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                button2.Visible = false;
            }
            if (getModo() == Constants.Aprueba)
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                textBox2.Visible = false;
                label1.Visible = false;
                button2.Text = "Rechazar";
                listBox1.Visible = false;
                
            }
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.RefrescarData();
        }

        private void RefrescarData()
        {
            bool strpanolero = false;
            try
            {
                if (getModo() == Constants.Asigna)
                {
                    Pedidos px = new Pedidos();
                    dgv.DataSource = px.ListarPedido("ALL");
                    dgv.Columns["ID"].Visible = false;
                }
                if (getModo() == Constants.Aprueba)
                {
                    List<string> perfiles = new List<string>();
                    perfiles = Global.SeguridadUsr.usuario.PERFILES.Split(',').ToList();
                    foreach (string str in perfiles)
                        if (str.ToUpper() == "PAÑOL" || str.ToUpper()=="R-COMUNIC")
                            strpanolero = true;
                    Confirmaciones konf = new Confirmaciones();
                    if (strpanolero)
                    {
                        dgv.DataSource = konf.ListarPendientesxUsr("ALL");
                    }
                    else
                    {
                        dgv.DataSource = konf.ListarPendientesxUsr(Global.SeguridadUsr.usuario.ID);
                    }
                    
                }
               
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Confirmaciones xc = new Confirmaciones();
            //JSONUsr xusr = new JSONUsr(Global.urlBase + "/ajaxEquipos.php?q=u");
            //xusr = xusr.JSONget();
            
            try
            {
                JSONUsr xusr = new JSONUsr();
                xusr = xusr.JSONget();
                string usr = Global.SeguridadUsr.usuario.USER_ID;
                DateTime ahora = DateTime.Now;
                if (getModo() == Constants.Asigna)
                {

                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Debe ingresar un valor para el inventario");
                        return;
                    }
                    //--- validacion para que el inventario asignado se encuentre en pañol y que sea de la marca y modelo solicitada

                    List<EquipoExt> res=Global.TodosLosAsset.getInvxCaractEnPan(paux.marca, paux.modelo, paux.equipo);
                    EquipoExt px = new EquipoExt();
                    px=res.Find(m => m.ID_Inv == textBox2.Text);
                    if (px== null)
                    {

                        MessageBox.Show("El nro de inventario " + textBox2.Text + " no se encuentra en pañol  ");
                        return;
                    }

                    //--- fin de validacion

                    RetCode z = new RetCode();
                    Movimientos mv = new Movimientos();
                    List<Ninventario> ni = new List<Ninventario>();
                    List<RetCodeExt> ls = new List<RetCodeExt>();
                    List<Ninventario> inv = new List<Ninventario>();
                    Ninventario xc = new Ninventario();
                    xc.id = textBox2.Text;
                    inv.Add(xc);
                    mv.Inventario = inv;
                    mv.descripcion = "OS:" + paux.os + "- FechaSolicitud: " + paux.FECHASOL + "Comentario:";
                  // mv.statusOrigen
                    mv.Puesto = paux.solicitante; 
                    mv.Solicitante = Global.SeguridadUsr.usuario.ID;
                    Usuario x = xusr.getDataxUsr(paux.solicitante);
                    string xx = paux.solicitante;
                    mv.UsrDestino = x.ID;

                    z = mv.AsignarATecnico(mv);
                    //foreach (RetCodeExt rx in ls)
                    //{
                        
                        if (z.rc=="0")
                        {
                            MessageBox.Show("Asignacion exitosa del inv ");
                            RetCode z1 = paux.BorrarPedido(paux);
                        }
                        else
                        {
                            MessageBox.Show("Error en la asignacion del equipo " );
                        }
                        paux.clear(ref paux);
                    mv.UsrDestino = xx;
                    mv.PasarAEnTRansito(mv);

                    //}
                    // ACTUALIZA LOS ESTADOS EN LA COLECCION LOCAL


                    //Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
                    //JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();
                    textBox2.Text = "";
                }
                if (getModo() == Constants.Aprueba)
                {
                    RetCode rc = new RetCode();
                    Confirmaciones xc = new Confirmaciones();
                    foreach(DataGridViewRow row  in dgv.SelectedRows)
                    {
                        Console.WriteLine(dgv.SelectedRows.Count);
                        rc = xc.Aprobar(row.Cells["Inventario"].Value.ToString());
                        if (rc.rc != "0")
                        {
                            MessageBox.Show(rc.msg);// lerr.Add(x.id);
                        }
                    }
                    // ACTUALIZA LOS ESTADOS EN LA COLECCION LOCAL
                    
                    
                    //Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
                    //JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();

                }
                this.RefrescarData();
                // ACTUALIZA LOS ESTADOS EN LA COLECCION LOCAL
                Cursor.Current = Cursors.WaitCursor;
                rx.RefrescarLocal();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InventarioAsset.ELog.save(this, ex);
            }
        }

       

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (getModo() == Constants.Aprueba)
            //{
            //    dgv.CurrentRow.Cells[1].Value = 1;

            //}
            //else
            //{
            //    listBox1.DisplayMember = "id_inv";
            //    listBox1.DataSource = Global.TodosLosAsset.getInvxCaractEnPan(paux.marca, paux.modelo, paux.equipo);
            //    paux.Id = dgv.CurrentRow.Cells["ID"].Value.ToString();
            //    paux.os = dgv.CurrentRow.Cells["OS"].Value.ToString();
            //    paux.equipo = dgv.CurrentRow.Cells["Equipo"].Value.ToString();
            //    paux.marca = dgv.CurrentRow.Cells["Marca"].Value.ToString();
            //    paux.modelo = dgv.CurrentRow.Cells["Modelo"].Value.ToString();
            //    paux.solicitante = dgv.CurrentRow.Cells["Solicitante"].Value.ToString();
            //}
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (getModo() == Constants.Aprueba)
            {
                dgv.CurrentRow.Cells[1].Value = 1;

            }
            else
            {
                paux.Id = dgv.CurrentRow.Cells["ID"].Value.ToString();
                paux.os = dgv.CurrentRow.Cells["OS"].Value.ToString();
                paux.equipo = dgv.CurrentRow.Cells["Equipo"].Value.ToString();
                paux.marca = dgv.CurrentRow.Cells["Marca"].Value.ToString();
                paux.modelo = dgv.CurrentRow.Cells["Modelo"].Value.ToString();
                paux.solicitante = dgv.CurrentRow.Cells["Solicitante"].Value.ToString();

                listBox1.DisplayMember = "id_inv";
                listBox1.DataSource = Global.TodosLosAsset.getInvxCaractEnPan(paux.marca, paux.modelo, paux.equipo);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RetCode rc = new RetCode();
            Confirmaciones xc = new Confirmaciones();
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                rc = xc.Rechazar(row.Cells["Inventario"].Value.ToString());
                if (rc.rc != "0")
                {
                    MessageBox.Show(rc.msg);// lerr.Add(x.id);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = listBox1.Text.ToString();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }

}




