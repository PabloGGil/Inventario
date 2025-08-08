using SpreadsheetLight;
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
using Excel = Microsoft.Office.Interop.Excel;
//using CAD_Inv;
//using CL_Inv;

namespace InventarioAsset
{
    public partial class frmMovEq : Form
    {
        int flagListaCargada = 0;
        public frmMovEq()
        {
            InitializeComponent();
        }
        public string statusDest;
        public string indexDest;
        ListViewItem imagenes = new ListViewItem();
        public void InitListBox(ListView lv)
        {

            lv.View = View.Details;
            lv.GridLines = true;
            lv.FullRowSelect = true;

            //Add column header
            lv.Columns.Add("Inventario", 100);
            lv.Columns.Add("Tipo", 100);
            lv.Columns.Add("Estado", 150);
            lv.Columns.Add("Marca", 150);
            lv.Columns.Add("Modelo", 150);

            lv.Columns.Add("Puesto", 70);
            lv.Columns.Add("Usuario", 70);
            lv.Columns.Add("ID_Estado", 0);
            lv.SmallImageList = imageList1;
        }
        private void frmMovEq_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Refresco rx = new Refresco();
            Refresco.RefrescarLocal();
            Cursor.Current = Cursors.Default;
            InitListBox(lvw);
            InitListBox(lvwFinal);
            //--- Deshabilitar los controles ---
            grbCampos.Enabled = false;
            cmdMover.Enabled = false;
            cmbStatus.Enabled = false;
            cmdImport.Visible = false;
           if(flagListaCargada==1)
            {
                cmdMover.Enabled = false;
                cmbStatus.Enabled = true;
                grbCampos.Enabled = false;
            }
            //-----------------------------------

            /* --------------------------------------------
             * SOLO PARA DESARROLLO
             * --------------------------------------------*/
            //if (Properties.Settings.Default.ambiente == "LAB")
            //{
            //    txtOS.Text = "9977";
            //    txtComentarios.Text = "Mover equipo a usuario";
            //    txtPuesto.Text = "PGI-L-000";
            //    txtUsuario.Text = "PGIL";
            //}

            
            string[] perfiles =  Global.SeguridadUsr.usuario.PERFILES.Split(',');
            foreach (string perfil in perfiles)
            {
                Console.WriteLine(perfil + "\n");
                if (perfil == "SYS.ADM.USER")
                {
                   cmdImport.Visible = true;
                    break;
                   
                }
               
            }

            
            
            lvw.SmallImageList = imageList1;
            lvw.View = View.Details;
        }
       
        public bool getAccessTipo(string Tipo)
        {
            bool existe = false;

            foreach(Tipo_Sec ts in Global.SeguridadUsr.tipo_sec)
            {
                if( ts.TIPO==Tipo)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
       
        public int cargarItems(List<string> xi,ListView lv)
        {
            List<EquipoExt> eqx = new List<EquipoExt>();
            eqx = Global.TodosLosAsset.GetListaEquipos(xi);
            //EquipoExt eq = new EquipoExt();
            int ok = 1;
            foreach (EquipoExt eq in eqx)
            {             
                ListViewItem itm;

                if (getAccessTipo(eq.DESCRIPCION))
                {
                    //if (eq.Count != 0)
                    //{
                        itm = new ListViewItem(eq.ID_Inv);
                        itm.Tag = eq.ID_Inv;
                        itm.SubItems.Add(eq.DESCRIPCION);
                        itm.SubItems.Add(eq.Estado);
                        itm.SubItems.Add(eq.MARCA);
                        itm.SubItems.Add(eq.MODELO);
                        itm.SubItems.Add(eq.Puesto);
                        itm.SubItems.Add(eq.Usuario);
                        itm.SubItems.Add(eq.ID_Estado);
                        //lvw.Items.Add(itm); productName = lvw.SelectedItems[0].SubItems[0].Text;
                        //price = lvw.SelectedItems[0].SubItems[1].Text;
                        //quantity = lvw.SelectedItems[0].SubItems[2].Text;
                        lv.Items.Add(itm);
                }
                else 
                {
                    MessageBox.Show("No tiene permiso para mover este tipo de asset");
                    ok = 0;
                    break;
                }
            }
            return ok;
        }

       // public void cargarItem( xi, ListView lv,int ImIndex)
        public void cargarItem(string  xi, ListView lv, int ImIndex)
        {
            List<EquipoExt> eq1 = new List<EquipoExt>();
            List<string> z = new List<string>();
            z.Add(xi);
            eq1 = Global.TodosLosAsset.GetListaEquipos(z);
            ListViewItem itm;
              
            if (eq1.Count != 0)
            {
                EquipoExt eq=eq1[0];
                    itm = new ListViewItem(eq.ID_Inv);
                    itm.Tag = eq.ID_Inv;
                    itm.SubItems.Add(eq.DESCRIPCION);
                    itm.SubItems.Add(eq.Estado);
                    itm.SubItems.Add(eq.MARCA);
                    itm.SubItems.Add(eq.MODELO);
                    itm.SubItems.Add(eq.Puesto);
                    itm.SubItems.Add(eq.Usuario);
                    itm.SubItems.Add(eq.ID_Estado);
                    itm.ImageIndex = ImIndex;
                    lv.Items.Add(itm);
                
               
            }
            else
            {
                MessageBox.Show("No esiste un equipo con el nro de inventario " + textBox1.Text);
            }
               
            
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                List<string> xi = new List<string>(); 
                xi.Add(textBox1.Text);
                List<EquipoExt> eqx = new List<EquipoExt>();
                eqx = Global.TodosLosAsset.GetListaEquipos(xi);
                //validar si el inventario existe
                if (eqx[0].ID_Inv!="0")
                {
                    // Validar que el inventario no esté en el listview
                    if (BuscarRepetido(textBox1.Text))
                    {
                        MessageBox.Show("El equipo ya está incluido para el movimiento");

                    }
                    else
                    {
                        // Si el inventario existe y no esta duplicado 
                        // -----------------------------------------------------------
                        // validar que todos los equipos tengan el mismo status origen
                        // -----------------------------------------------------------
                        if (lvw.Items.Count>0)
                        {
                            // aca entro solo si hay mas de un equipo cargado
                            // verifico el ststus del primer registro
                            //List<EquipoExt> eqx = new List<EquipoExt>();
                            eqx = Global.TodosLosAsset.GetListaEquipos(xi);
                            if (! eqx[0].ID_Estado.Equals( lvw.Items[0].SubItems[7].Text))
                            {
                                MessageBox.Show("Los equipos que se agreguen deben tener todos el mismo estado");
                                return;
                            }
                        }
                        List<string> inventarios = new List<string>();
                        inventarios.Add(textBox1.Text);

                        cargarItems(inventarios, lvw);

                        cmdMover.Enabled = false;
                        cmbStatus.Enabled = true;
                        grbCampos.Enabled = false;
                        OpcionesMov lstmov = new OpcionesMov();
                        lstmov.cargar();

                        cmbStatus.DataSource = lstmov.getOpcionesMovimiento(lvw.Items[0].SubItems[7].Text);
                        cmbStatus.ValueMember = "st_destino";
                        cmbStatus.DisplayMember = "Desc_movimiento";
                    }
                }
                else
                {
                    MessageBox.Show("No existe el nro de inventario ingresado");
                }
            }catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
            //foreach (ListBox.ObjectCollection lvic in lvw.Items)
            //    Console.WriteLine(lvic);
        }

        private void lvw_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //ContextMenu cm = new ContextMenu();
                        //cm.MenuItems.Add("Item 1");
                        //cm.MenuItems.Add("Item 2");
                        //cm.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }
        public bool BuscarRepetido(string nro)
        {
            bool encontro=false;
            for (int i = 0; i < lvw.Items.Count; i++)
            {
                if (lvw.Items[i].SubItems[0].Text == nro)
                {
                    encontro = true;
                    break;
                }
                
            }
            return encontro;
        }

        private void cmdListaPuesto_Click(object sender, EventArgs e)
        {

        }
        public void CargarLista(List<string> aMover)
        {
            int ret;
            //foreach(string xinv in aMover)
            ret=cargarItems(aMover, lvw);
            if (ret == 0)
            {  
              
                return;
            }
            flagListaCargada = 1;
            OpcionesMov lstmov = new OpcionesMov();
            lstmov.cargar();
            cmbStatus.DataSource = lstmov.getOpcionesMovimiento(lvw.Items[0].SubItems[7].Text);
            cmbStatus.ValueMember = "st_destino";
            cmbStatus.DisplayMember = "Desc_movimiento";
        }
        public static bool ValidarPuesto(string strNumber)
        {
            Regex regex = new Regex(@"^[A-Z]{3}-[A-Z0-9]{1}-\d{3}$");
            Match match = regex.Match(strNumber);

            if (match.Success)
                return true;
            else
                return false;
        }

        private void cmdMover_Click(object sender, EventArgs e)
        {
            //Validar que el puesto este bien escrito
            //if(!ValidarPuesto(txtPuesto.Text))
            //{
            //    MessageBox.Show("el puesto " + txtPuesto.Text + " está mal escrito");
            //    return;
            //}
            //Validar que el usuario ingresado exista
            //
            JSONUsr xusr = new JSONUsr();
            Usuario uss = new Usuario();
            uss = xusr.getDataxUsr(txtUsuario.Text);
            if (uss.ID =="-1")
            {
                MessageBox.Show("el usuario " + txtUsuario.Text + " no existe");
                return;
             }
            DialogResult dlgResult = MessageBox.Show("Va a mover los equipos al estado " + statusDest + "\nAcepta??","Confirmacion", MessageBoxButtons.YesNo);
            if (dlgResult == DialogResult.No)
                return;
            RetCode rx = new RetCode();
            Movimientos mv = new Movimientos();
            Ninventario inv = new Ninventario();
            List<Ninventario> ni = new List<Ninventario>();
            List<RetCodeExt> ls = new List<RetCodeExt>();
            //JSONUsr xusr = new JSONUsr();
            
            for (int i = 0; i < lvw.Items.Count; i++)
            {
                inv.id = lvw.Items[i].SubItems[0].Text;
                ni.Add(inv);
                inv = new Ninventario();
            }
            mv.Inventario = ni;
            mv.OS = txtOS.Text;
            
            mv.descripcion = "OS: " + txtOS.Text + "Comentario: " + txtComentarios.Text;
            /* ------------------NOTA-----------------------------------
             *  cuando este implementado la funcionalidad de lugares
             *  debería consultar en que puesto está el usuario para 
             *  setear el numero de puesto real
             *  --------------------------------------------------------*/
            mv.Puesto = txtPuesto.Text;//Global.SeguridadUsr.usuario.USER_ID;

            mv.Solicitante = Global.SeguridadUsr.usuario.ID;

            // xusr = xusr.JSONget();
            //JUsuariosSys xusr = new JUsuariosSys();
            xusr=xusr.JSONget();
            ////if(indexDest=="5")
            ////{
            ////    txtUsuario.Text = lvw.Items[0].SubItems[6].Text;
            ////}
              Usuario x = new Usuario();

            x = xusr.getDataxUsr(txtUsuario.Text);
            if (x.ID == "-1")
                // mv.UsrDestino = "";
                x.ID = mv.Solicitante;

            else
                mv.UsrDestino = x.ID;
            JsonStatuses om = new JsonStatuses();
            mv.statusOrigen = om.Stat2ID(lvw.Items[0].SubItems[2].Text);
           
            int ImIndex = 0;

            switch (indexDest)
            {
                //en transito
                case "5":
                    if (txtUsuario.Text == "" && mv.statusOrigen == "7")
                    {
                        MessageBox.Show("Debe ingresar el nombre del técnico");
                        return;
                    }
                    mv.Solicitante = Global.SeguridadUsr.usuario.ID;
                    //txtUsuario.Text = lvw.Items[0].SubItems[6].Text;
                    for (int i = 0; i < lvw.Items.Count; i++)
                    {
                       // if (mv.statusOrigen == "7")
                            mv.UsrDestino = txtUsuario.Text;
                        //if (mv.statusOrigen=="15")
                        //    mv.UsrDestino = lvw.Items[0].SubItems[6].Text; 
                        rx = mv.PasarAEnTRansito(mv);
                        ImIndex = (rx.rc != "0") ? 2 : 0;
                        //if (rx.rc != "0")
                        //{
                        //string errores = rx.inv + "\nError: " + rx.msg;

                        //for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    }
                   //MessageBox.Show(rx.msg);

                    //}
                    break;
              
                // Deposito
                case "6":
                    rx = mv.PasarAPanol(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;
                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;

                    break;

                    // MessageBox.Show(rx.msg);

                    
                case "7":
                    if (mv.statusOrigen == "7")
                    {
                        // Cambio de puesto. cargar en textbox de usuario el usuario actual
                        Console.WriteLine("cambio de puesto");
                        
                        x = xusr.getDataxUsr(lvw.Items[0].SubItems[6].Text);
                       // mv.UsrDestino = x.ID;
                        txtUsuario.Text = lvw.Items[0].SubItems[6].Text;
                        rx = mv.CambioDePuesto(mv);
                    }
                    else
                    {
                        rx = mv.AsignarUsrFinal(mv);
                    }
                    ImIndex = (rx.rc != "0") ? 2 : 0;
                    
                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;

                   // MessageBox.Show(rx.msg);
                    break;
                // Reparacion interna
                case "9":
                    rx = mv.RepInterna(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                   //MessageBox.Show("Va a mover los equipos al estado " + statusDest);
                    break;

                // Reparacion externa
                case "10":
                    rx = mv.RepExterna(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    break;

                    // Donacion
                case "11":
                    rx = mv.Donacion(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    break;

                // Venta interna
                case "12":
                    rx = mv.VentaInterna(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    break;

                // Baja
                case "13":
                    rx = mv.Baja(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    break;

                // Asignado tecnico
                case "15":
                    mv.UsrDestino = txtUsuario.Text;
                    //mv.Puesto = lvw.Items[0].SubItems[5].Text;
                    mv.Puesto = txtUsuario.Text;
                    //txtPuesto.Text = lvw.Items[0].SubItems[5].Text;
                    txtPuesto.Text = txtUsuario.Text;
                    RetCode ap = mv.AsignarATecnico(mv);
                    ImIndex = (ap.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    MessageBox.Show(ap.msg);
                    mv.UsrDestino = txtUsuario.Text;
                    mv.PasarAEnTRansito(mv);
                    break;

                // pendiente confirmacion tecnico
                case "16":
                    if (txtUsuario.Text == "")
                    {
                        MessageBox.Show("Debe ingresar el nombre del técnico");
                        return;
                    }
                    mv.UsrDestino = txtUsuario.Text;
                    //mv.Puesto = lvw.Items[0].SubItems[5].Text;
                    mv.Puesto= txtUsuario.Text;
                    //txtPuesto.Text = lvw.Items[0].SubItems[5].Text;
                    txtPuesto.Text= txtUsuario.Text;
                    RetCode ap1 = mv.AsignarATecnico(mv);
                    ImIndex = (ap1.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    MessageBox.Show(ap1.msg);
                    mv.UsrDestino = txtUsuario.Text;
                    mv.PasarAEnTRansito(mv);

                    break;
               
                // pendiente confirmacion usuario final
                case "17":
                    if (mv.statusOrigen == "ASIGNADO")
                    {
                        // Cambio de puesto. cargar en textbox de usuario el usuario actual
                        Console.WriteLine("cambio de titularidad");
                        mv.Puesto = lvw.Items[0].SubItems[5].Text;
                        txtPuesto.Text = lvw.Items[0].SubItems[5].Text;

                    }
                    if (txtPuesto.Text.Length==0 || txtUsuario.Text.Length==0)
                    {
                        MessageBox.Show("Para éste movimiento debe ingresar usuario y puesto");
                        return;
                    }
                    rx = mv.AsignarUsrFinal(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;

                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;
                    break;
                
                // pendiente confirmacion paniol
                case "18":
                    mv.UsrDestino = "1634";
                    rx = mv.PasarAConfirmarPanol(mv);
                    ImIndex = (rx.rc != "0") ? 2 : 0;
                    for (int i = 0; i < lvw.Items.Count; i++)
                        lvw.Items[i].ImageIndex = ImIndex;

                    break;
                // en prestamo
                case "19":
                    
                    break;
                //// Cambio de puesto
                //case "98":
                //    rx = mv.CambioDePuesto(mv);
                //    ImIndex = (rx.rc != "0") ? 2 : 0;

                //    for (int i = 0; i < lvw.Items.Count; i++)
                //        lvw.Items[i].ImageIndex = ImIndex;
                //    break;
                //// Cambio de titularidad
                //case "99":
                //   for (int i = 0; i < lvw.Items.Count; i++)
                //    {
                //        mv.Puesto = lvw.Items[i].SubItems[5].ToString();
                //        rx = mv.CambioTitularidad(mv);
                //        ImIndex = (rx.rc != "0") ? 2 : 0;
                //        lvw.Items[i].ImageIndex = ImIndex;
                //    }
                //    break;


            }


            //Refresco rf = new Refresco();
            Refresco.RefrescarLocal();
            grbCampos.Enabled = false;
            cmdMover.Enabled =false;
            cmbStatus.Enabled =false;
            //------------------------------------------------------
            // Paso los items al otro listview y los actualizo
            //------------------------------------------------------
            List<string> listinv = new List<string>();
            foreach (ListViewItem item in lvw.Items)
            {
                
            
                cargarItem(item.Text, lvwFinal,item.ImageIndex);
                lvw.Items.Remove(item);

                //lvwFinal.Items.Add(item);
                listinv.Add(item.Text);
            }
            

        }






        private void btnQuitar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvw.SelectedItems.Count; i++)
            {
                lvw.Items.Remove(lvw.SelectedItems[i]);
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = cmbStatus.SelectedIndex;
            statusDest = cmbStatus.Text;
            indexDest = cmbStatus.SelectedValue.ToString();
           
            grbCampos.Enabled = true;
            cmdMover.Enabled =true;
            cmbStatus.Enabled =true;
            switch(cmbStatus.SelectedValue)
            {
                case "5":
                    txtPuesto.Enabled = false;
             //       txtUsuario.Enabled = false;
                    break;
                case "11":
                case "12":
                case "13":
                    txtPuesto.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtUsuario.Text = "";
                    txtPuesto.Text = "Sin_Puesto";
                    break;
                case "15":
                case "16":
                    txtPuesto.Text = "";
                    txtPuesto.Enabled = false;
                    break;
                case "18":
                    txtUsuario.Enabled =false;
                    txtPuesto.Enabled = false;
                    break;
                case "98":
                    txtPuesto.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtUsuario.Text = "";
                    break;
                case "99":
                    txtPuesto.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtPuesto.Text = "";
                    break;
                default:
                    txtPuesto.Enabled = false;
                    txtUsuario.Enabled = false;
                    break;
            }
            label6.Text = statusDest + "  " + indexDest;
        }

        private void cmdListaPuesto_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdListaUsuario_Click(object sender, EventArgs e)
        {

            if ( indexDest == "17" || indexDest == "7")
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
                    txtUsuario.Text = frm.valorRetorno; //lee la propiedad
                    ; //la pone en el título
                }
               
            }
            else
            {
                RootGyU rgyu = new RootGyU();
                //List<RetCodeExt> lerr = new List<RetCodeExt>();
                rgyu = rgyu.JSONget();
                frmData frm = new frmData();
                frm.Busqueda(true);
                frm.Text = "Seleccion de usuarios";
                frm.DataSource(rgyu.coleccion.ToList());
                //frm.Show();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtUsuario.Text = frm.valorRetorno; //lee la propiedad
                    ; //la pone en el título
                }
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            try
            {

                string filePath;
                //CrearTablas();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "todos|*";
                openFileDialog1.Title = "Abrir el archivo";
                openFileDialog1.ShowDialog();
                filePath = openFileDialog1.FileName;
                List<string> ls = new List<string>();

                using (SLDocument sl = new SLDocument(filePath))
                {
                    int ifila = 1;
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(ifila, 1)))
                    {
                        ls.Add(sl.GetCellValueAsString(ifila, 1));
                        ifila++;
                    }
                    cargarItems(ls,lvw);
                }
                cmdMover.Enabled = true;
                cmbStatus.Enabled = true;
                grbCampos.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
            }
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btnAgregar.PerformClick();
            }
        }

        private void txtPuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                txtPuesto.Text = txtPuesto.Text.ToUpper();
                if (!ValidarPuesto(txtPuesto.Text))
                {
                    MessageBox.Show("el puesto " + txtPuesto.Text + " está mal escrito");
                    txtPuesto.Text = "";
                }
            }
        }

        private void btnHistMov_Click(object sender, EventArgs e)
        {
            frmData frm = new frmData();
            frm.Text = "Movimientos de equipos";
            Movimientos mov = new Movimientos();

            string inv;
            if (lvwFinal.SelectedItems.Count == 0)
            {
                MessageBox.Show("Debe tener algún equipo seleccionado para ver los movimientos");
                return;
            }
            else
                inv = lvwFinal.SelectedItems[0].ToString();
            
            //string inv = "32271";


            frm.DataSource(mov.ListarMov(inv));
            frm.Show();
        }

        private void txtPuesto_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPuesto_Leave(object sender, EventArgs e)
        {
            if (!ValidarPuesto(txtPuesto.Text))
            {
                MessageBox.Show("el puesto " + txtPuesto.Text + " está mal escrito");
                txtPuesto.Text = "";
            }

        }

        private void cmdListaPuestos_Click(object sender, EventArgs e)
        {
            Puesto p = new Puesto();
            List<Puesto> lp = new List<Puesto>();
            //lp = p.ListarTodosPuestos();
            lp = p.ListarPuestosActivos();
               frmData frm = new frmData();
            frm.Busqueda(true);
            frm.Text = "Seleccion de Puestos";
            frm.DataSource(lp);
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtPuesto.Text = frm.valorRetorno; //lee la propiedad
            //    ; //la pone en el título
            }
        }
    }
}

