using SpreadsheetLight;
using System;
using System.Data;
using System.Windows.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
//using CL_Inv;
using System.Reflection;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using System.Windows.Controls;
using ICollectionView = System.ComponentModel.ICollectionView;
using System.Drawing;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;

namespace InventarioAsset
{
    public partial class ConsultaInv : Form
    {
        //GridViewColumnHeader _lastHeaderClicked = null;
        //ListSortDirection _lastDirection = ListSortDirection.Ascending;
        bool sortAscending;
        //private int pmodo;
        List<EquipoExt> lst = new List<EquipoExt>();
        // DataTable dt = new DataTable();
        // DataTable dta = new DataTable();
        //SqlConnection csql = new SqlConnection();
        //public void setModo(int modo)
        //{
        //    pmodo = modo;
        //    if (pmodo == Constants.Inventario)
        //    {

        //        this.Text = "Consulta Inventario";

        //    }
        //    if (pmodo == Constants.Asset)
        //    {
        //        this.Text = "Consulta Asset";


        //    }
        //}

        //public int getModo()
        //{
        //    return pmodo;
        //}
        public void AjustarOrdenColumnas()
        {
            try
            {

                dgvAsset.Columns["ID_Inv"].DisplayIndex = 0;
                dgvAsset.Columns["Puesto"].DisplayIndex = 1;
                dgvAsset.Columns["Usuario"].DisplayIndex = 2;
                dgvAsset.Columns["Estado"].DisplayIndex = 3;
                dgvAsset.Columns["DESCRIPCION"].DisplayIndex = 4;

                dgvAsset.Columns["MARCA"].DisplayIndex = 5;
                dgvAsset.Columns["MODELO"].DisplayIndex = 6;
                dgvAsset.Columns["SERIE"].DisplayIndex = 7;
                dgvAsset.Columns["detalle"].DisplayIndex = 8;
                dgvAsset.Columns["ID_REMITO"].DisplayIndex = 9;
                dgvAsset.Columns["Status_date"].DisplayIndex = 10;
                dgvAsset.Columns["ID_Estado"].Visible = false;
                dgvAsset.Columns["ID_Inv"].HeaderText = "Inventario";
                dgvAsset.Columns["DESCRIPCION"].HeaderText = "Tipo";
                dgvAsset.Columns["ID_REMITO"].HeaderText = "Remito Nro";
                dgvAsset.Columns["Status_date"].HeaderText = "Cambio de estado";
                dgvAsset.Columns["detalle"].HeaderText = "Detalle movimiento";
                dgvAsset.Columns["Fecha_Ingreso"].HeaderText = "Fecha Ingreso";
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        public ConsultaInv()
        {
            InitializeComponent();
        }
        string IndexDest;
        private void ConsultaInv_Load(object sender, EventArgs e)
        {
            dgvAsset.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Cursor.Current = Cursors.WaitCursor;
            //Refresco rx = new Refresco();
            Refresco.RefrescarLocal();
            Cursor.Current = Cursors.Default;
            try
            {
                //this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
                //this.WindowState = FormWindowState.Normal;
                dgvAsset.AllowUserToOrderColumns = true;
                sortAscending = true;
                //cna = InventarioAsset.BaseDatos.ConectarOra();

                //string query = "select MAQUINA.DESCR from maquina ,detalle where MAQUINA.inv = detalle.idmaquinaand detalle.idpuesto = 'PAN-0-000' GROUP BY DESCRGROUP BY DESCR ";
                cmbCriterio.Items.Add("Inventario");
                cmbCriterio.Items.Add("Marca");
                cmbCriterio.Items.Add("Modelo");
                cmbCriterio.Items.Add("Tipo Equipo");
                cmbCriterio.Items.Add("Serie");
                cmbCriterio.Items.Add("Puesto");
                cmbCriterio.Items.Add("Usuario");
                cmbCriterio.Items.Add("Estado");
                cmbCriterio.Items.Add("OOCC");
                cmbCriterio.Items.Add("Revisados");
                //cmbEquipo.DataBindings = "descr";
                cmbCriterio.DisplayMember = "Inventario";
                //cx = InventarioAsset.BaseDatos.Conectar();
                cmbCriterio.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            //string qry = null;
            //string qryAsset = null;
            List<string> vacio = new List<string>();
            //Refresco rx = new Refresco();
            //rx.RefrescarLocal();

            try
            {
                string valor = cmbValor.Text;
                cmbValor.SelectedItem = cmbValor.Text;
                // if (getModo() == Constants.Inventario)
                //{
                switch (cmbCriterio.SelectedItem.ToString())
                {
                    case "Revisados":
                        //List<EquipoExt> lst = new List<EquipoExt>();
                        lst = Global.TodosLosAsset.getEquiposRevisados();
                        dgvAsset.DataSource = lst;
                        break;
                    case "Tipo Equipo":
                        dgvAsset.DataSource = null;
                        //dgvAsset.Rows.Clear();
                        lst = Global.TodosLosAsset.getEquiposxTipo(valor);
                        dgvAsset.DataSource = lst;
                        
                        break;
                    case "Inventario":

                        vacio.Clear();
                        cmbValor.DataSource = null;
                        vacio.Add(cmbValor.Text);
                        cmbValor.DataSource = vacio;
                        // cmbValor.Text = "";
                        dgvAsset.DataSource = null;
                        List<string> xs = new List<string>();
                        if (checkBox1.Checked == true)
                        {
                            string filePath;
                            //CrearTablas();
                            OpenFileDialog openFileDialog1 = new OpenFileDialog();
                            openFileDialog1.Filter = "todos|*";
                            openFileDialog1.Title = "Abrir el archivo";
                            openFileDialog1.ShowDialog();
                            filePath = openFileDialog1.FileName;


                            using (SLDocument sl = new SLDocument(filePath))
                            {
                                int ifila = 1;
                                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(ifila, 1)))
                                {
                                    xs.Add(sl.GetCellValueAsString(ifila, 1));
                                    ifila++;
                                }

                            }
                        }
                        else
                        {
                            xs.Add(valor);
                        }
                        lst = Global.TodosLosAsset.GetListaEquipos(xs);
                        if (lst[0].ID_Inv != "0")
                        {
                            dgvAsset.DataSource = lst;
                        }
                        else
                        {
                            MessageBox.Show("el numero de inventario no existe");
                        }
                        //dgvAsset.Rows.Clear();

                        break;

                    case "Marca":
                        dgvAsset.DataSource = null;
                        //dgvAsset.Rows.Clear();
                        lst = Global.TodosLosAsset.getEquiposxMarca(valor);
                        dgvAsset.DataSource = lst;
                        break;

                    case "Modelo":
                        dgvAsset.DataSource = null;
                        lst = Global.TodosLosAsset.getEquiposxModelo(valor);
                        var bindingList = new BindingList<EquipoExt>(lst);
                        var source = new BindingSource(bindingList, null);
                        //dgvAsset.DataSource = lst
                        dgvAsset.DataSource = source;

                        break;

                    case "Serie":
                        vacio.Clear();
                        //List<string> vacio = new List<string>();
                        cmbValor.DataSource = null;
                        vacio.Add(cmbValor.Text);
                        cmbValor.DataSource = vacio;
                        dgvAsset.DataSource = null;
                        lst = Global.TodosLosAsset.getSerie(valor);
                        dgvAsset.DataSource = lst;
                        break;

                    case "Puesto":
                        vacio.Clear();
                        vacio.Add(cmbValor.Text);
                        cmbValor.DataSource = null;
                        //dgvAsset.Rows.Clear();
                        cmbValor.DataSource = vacio;
                        lst = Global.TodosLosAsset.getEquiposxPuesto(valor);
                        List<EquipoExt> studentsInDescOrder = lst.OrderBy(registro => registro.Puesto).ToList();
                        dgvAsset.DataSource = studentsInDescOrder;
                        break;

                    case "Usuario":
                        vacio.Clear();
                        //List<string> vacio = new List<string>();
                        cmbValor.DataSource = null;
                        vacio.Add(cmbValor.Text);
                        cmbValor.DataSource = vacio;
                        dgvAsset.DataSource = null;
                        lst = Global.TodosLosAsset.getEquiposxUsuario(valor);
                        dgvAsset.DataSource = lst;
                        break;
                    case "Estado":
                        string indice = cmbValor.SelectedValue.ToString();
                        lst = Global.TodosLosAsset.getEquiposxEstado(IndexDest);
                        //lst = Global.TodosLosAsset.getEquiposxEstado(IndexDest);// mbValor.SelectedIndex.ToString());
                        // lst = Global.TodosLosAsset.getEquiposxEstado(cmbValor.SelectedIndex.ToString());
                        dgvAsset.DataSource = lst;
                        AjustarOrdenColumnas();
                        break;
                    case "OOCC":
                        //string nrooc = cmbValor.SelectedValue.ToString();
                        CL_Inv.OC_INV listoc = new CL_Inv.OC_INV();

                        dgvAsset.DataSource = listoc.getData(valor);
                        break;
                    default:
                        break;
                }
                foreach (DataGridViewRow fila in dgvAsset.Rows)
                {

                    // Verifica que la celda no sea nula y evalúa su valor
                    if (fila.Cells["REVISADO"].Value != null)
                    {
                        string valorx = fila.Cells["REVISADO"].Value.ToString();

                        // Cambia el color según el valor
                        if (valorx == "1")
                        {
                            fila.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                    }
                }
                label3.Text = dgvAsset.Rows.Count + " Registros " + lst.Count;
                dgvAsset.ClearSelection();

            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //cmdBuscar_Click(sender, e);
                cmdBuscar.PerformClick();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dt = new DataTable();
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Libro de excel (*.xlsx)|*.xlsx|Libro de excel (*.xls)|*.xls";
                saveFileDialog1.Title = "Exportar el archivo";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                        (System.IO.FileStream)saveFileDialog1.OpenFile();


                    fs.Close();
                }

                dt = converter.ToDataTable(this.lst);
                SLDocument mydoc = new SLDocument();
                mydoc.AddWorksheet("Inventario");
                mydoc.ImportDataTable(1, 1, dt, true);
                // mydoc.AddWorksheet("Asset");
                // mydoc.ImportDataTable(1, 1, dta, true);
                mydoc.SaveAs(saveFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
        public List<EquipoExt> CargarDesdeArchivo()
        {
            List<EquipoExt> eqaux = new List<EquipoExt>();
            try
            {

                string filePath;
                //CrearTablas();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "todos|*";
                openFileDialog1.Title = "Abrir el archivo";
                openFileDialog1.ShowDialog();
                filePath = openFileDialog1.FileName;

                using (SLDocument sl = new SLDocument(filePath))
                {
                    EquipoExt aux = new EquipoExt();
                    int ifila = 1;
                    //Inv_Ubicacion aux = new Inv_Ubicacion();
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(ifila, 1)))
                    {
                        aux.ID_Inv = sl.GetCellValueAsString(ifila, 1);
                        aux.Puesto = sl.GetCellValueAsString(ifila, 2);
                        eqaux.Add(aux);
                        ifila++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
                eqaux = null;
            }
            return eqaux;
        }

        private void cmbCriterio_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void cmdMov_Click(object sender, EventArgs e)
        {
            frmData frm = new frmData();
            frm.Text = "Movimientos de equipos";
            Movimientos mov = new Movimientos();// (Global.urlBase + "/ajaxEquipos.php?q=assetstatus&d=" + dgvAsset.CurrentRow.Cells[3].Value.ToString());

            //Movimientos mov = new Movimientos(Global.urlBase + "/ajaxEquipos.php?q=assetstatus&d=" + dgvAsset.CurrentRow.Cells[5].Value.ToString());
            //DataMov dmov = new DataMov();
            //jMovimientos jmv = new jMovimientos();
            //dmov = jmv.JSONGet(dgvAsset.CurrentRow.Cells[5].Value.ToString());
            //mov.ListarMov(dgvAsset.CurrentRow.Cells[5].Value.ToString());
            //Movimientos mov = new Movimientos();
            string inv = dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString();
            //jMovimientos jm = new jMovimientos();
            //List<DataMov> dm = new List<DataMov>();
            //dm=jm.JSONGet(inv);


            frm.DataSource(mov.ListarMov(inv));
            frm.Show();
        }

        private void cmdNotas_Click(object sender, EventArgs e)
        {
            frmData frm = new frmData();
            frm.Text = "Notas";
            Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString());
            
            string url= Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString();
            //JSONNotas jma = ma.JSONget();
            JSONNotas nota = WebService.FetchData<JSONNotas>(url);
            frm.DataSource(nota);
            //frm.DataSource(nota.coleccion.ToArray());
            frm.Show();


        }

        private void cmdUsuario_Click(object sender, EventArgs e)
        {
            JSONUsr xusr;
            xusr = new JSONUsr();
            xusr = xusr.JSONget();
            Usuario x = xusr.getDataxUsr(dgvAsset.CurrentRow.Cells["Usuario"].Value.ToString());
            frmData frm = new frmData();
            frm.Text = "Datos de usuario";
            //As_pue_usrs apu = new As_pue_usrs(Global.urlBase + "/ajaxEquipos.php?q=a&id=" + dgvAsset.CurrentRow.Cells[0].Value.ToString());
            //JSONapus japu = apu.JSONget();
            //Empleado u = apu.getEmpleado();
            //frm.DataSource(apu.getEmpleado());
            frm.DataSource(x);
            frm.Show();
        }

        private void cmbValor_DropDown(object sender, EventArgs e)
        {

        }

        private void cmdDatosCompra_Click(object sender, EventArgs e)
        {
            //List<Eq_DatosCompra> m = new List<Eq_DatosCompra>();
            //Eq_DatosCompra dc = new Eq_DatosCompra();
            frmData frm = new frmData();
            //frm.Text = "Datos de usuario";
            //List<string> invs = new List<string>();
            //invs.Add(dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString());
            //List<Eq_DatosCompra> res = new List<Eq_DatosCompra>();
            DatoCompra dc = new DatoCompra();
            List<DatoCompra> dcl = new List<DatoCompra>();
            if (dgvAsset.CurrentRow.Cells["ID_REMITO"].Value != null)
                dc = dc.getData(dgvAsset.CurrentRow.Cells["ID_REMITO"].Value.ToString());
            dcl.Add(dc);
            frm.DataSource(dcl);
            frm.Show();
        }

        private void cmbValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmdBuscar.Focus();
                // SendKeys.Send("{TAB}");
            }
        }
        //private bool sortAscending = false;

        //private void Sort(string sortBy, ListSortDirection direction)
        //{
        //    ICollectionView dataView =
        //      System.Windows.Data.CollectionViewSource.GetDefaultView(lv.ItemsSource);

        //    dataView.SortDescriptions.Clear();
        //    SortDescription sd = new SortDescription(sortBy, direction);
        //    dataView.SortDescriptions.Add(sd);
        //    dataView.Refresh();
        //}

        private void cmbValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string carlo = cmbCriterio.SelectedItem.ToString().ToLower();
            if (carlo == "estado")
            {
                //string message = "Name: " + cmbValor.Text;
                //message += Environment.NewLine;
                //message += "CustomerId: " + cmbValor.SelectedValue;
                //MessageBox.Show(message);
                ////cmbValor.SelectedItem = cmbValor.Items[0].ToString();
                IndexDest = cmbValor.SelectedValue.ToString();
                // IndexDest =cmbValor.Items[0].ToString();
            }
        }

        private void cmbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = cmbCriterio.SelectedIndex;
            switch (cmbCriterio.SelectedItem.ToString().ToLower())
            {
                case "tipo equipo":
                    cmbValor.DataSource = Global.TodosLosAsset.getTipoEq();
                    cmbValor.SelectedItem = cmbValor.Items[0].ToString();
                    break;
                case "marca":
                    //dgvAsset.Rows.Clear();
                    //cmbValor.Sorted = true;
                    cmbValor.DataSource = Global.TodosLosAsset.getMarcas().ToArray();
                    cmbValor.SelectedItem = cmbValor.Items[0].ToString();
                    break;

                case "modelo":
                    //dgvAsset.Rows.Clear();

                    cmbValor.DataSource = Global.TodosLosAsset.getModelos().ToArray();
                    // cmbValor.SelectedItem ="S20";
                    //cmbValor.ValueMember = "ID";
                    //cmbValor.DisplayMember = "DESCRIPCION";
                    cmbValor.SelectedItem = cmbValor.Items[0].ToString();
                    break;
                case "estado":
                    JsonStatuses js = new JsonStatuses();

                    //--------------------------------------------------------
                    // Cargar combos con estados
                    //--------------------------------------------------------
                    js = js.GetJ();
                    JsonStatuses js1 = new JsonStatuses();

                    List<Status> lstorigen = new List<Status>();
                    lstorigen = js.coleccion.ToList();
                    cmbValor.DataSource = lstorigen;
                    cmbValor.ValueMember = "ID";
                    cmbValor.DisplayMember = "DESCRIPCION";

                    cmbValor.SelectedItem = cmbValor.Items[0].ToString();
                    break;
                default:
                    cmbValor.DataSource = null;
                    cmbValor.Text = "";
                    break;

            }
            Console.WriteLine(cmbValor.Items.Count);


        }

        private void dgvAsset_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in dgvAsset.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void dgvAsset_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvAsset_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //DataGridViewColumn newColumn = dgvAsset.Columns[e.ColumnIndex];
                //DataGridViewColumn oldColumn = dgvAsset.SortedColumn;
                //ListSortDirection direction;

                //// If oldColumn is null, then the DataGridView is not sorted.
                //if (oldColumn != null)
                //{
                //    // Sort the same column again, reversing the SortOrder.
                //    if (oldColumn == newColumn &&
                //        dgvAsset.SortOrder == SortOrder.Ascending)
                //    {
                //        direction = ListSortDirection.Descending;
                //    }
                //    else
                //    {
                //        // Sort a new column and remove the old SortGlyph.
                //        direction = ListSortDirection.Ascending;
                //        oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                //    }
                //}
                //else
                //{
                //    direction = ListSortDirection.Ascending;
                //}
                //dgvAsset.Sort(newColumn, direction);
                //newColumn.HeaderCell.SortGlyphDirection =
                //direction == ListSortDirection.Ascending ?
                //SortOrder.Ascending : SortOrder.Descending;
         
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvAsset_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        string inv = dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString();
                        string estado = dgvAsset.CurrentRow.Cells["Estado"].Value.ToString();
                        string idestado = "";
                        string Revisado= dgvAsset.CurrentRow.Cells["REVISADO"].Value.ToString();


                        OpcionesMov om = new OpcionesMov();
                        om.cargar();
                        idestado = om.TxtToID(estado);

                        ContextMenu cm = new ContextMenu();
                        MenuItem mi = new MenuItem();
                        mi.Text = "Mover Equipos";
                        mi.Click += xMov; //metodo al dar click
                        cm.MenuItems.Add(mi);
                        if (Revisado == "0")
                        {
                            MenuItem chkinv = new MenuItem();
                            chkinv.Click += chkmetodo;
                            chkinv.Text = "check revision";
                            cm.MenuItems.Add(chkinv);
                            // 
                            MenuItem aNoEncontrado = new MenuItem();
                            aNoEncontrado.Click += nEnc;
                            aNoEncontrado.Text = "Enviar a NO Encontrado";
                            cm.MenuItems.Add(aNoEncontrado);

                        }
                        
                        cm.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                        //}
                    }
                    break;
            }
        }
        private void nEnc(Object sender, EventArgs e)
        {
            Console.WriteLine("chech");
            Cursor.Current = Cursors.WaitCursor;
            JSONUsr xusr = new JSONUsr();
            Ninventario xc = new Ninventario();


            JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");
            string inv = dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString();
            List<Ninventario> invs = new List<Ninventario>();
            xc.id = inv;
            invs.Add(xc);
            
            mover.idAssets = invs.ToArray();
            mover.q = "cas";

            //mover.OS = "997";
            mover.descripcion = "Se mueve al puesto de NO ENCONTRADOS";
            mover.Formulario = "";
            mover.statusDest = "7";
           // mover.idUsuarioDestino = null;
            mover.statusOrig = "7"; 
            mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
            mover.ID_PUESTO = "ZZZ-0-000";
            mover.FechaHasta = DateTime.Now.ToLongDateString();

            MovEquipo moverC = new MovEquipo(Global.urlBase + "/ajaxEquipos.php");
            RetCode k = moverC.JPost(mover);

            
            //txtmensaje.Text = z.msg;

            Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
            JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();
            Cursor.Current = Cursors.Default;

        }
        private void chkmetodo(Object sender, EventArgs e)
        {
            Console.WriteLine("chech");
            string inv = dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString();
            Equipo ex = new Equipo();
            RetCode rc= ex.setRevisado(inv);
            // Agrego una nota indicando que de revisó el equipo
            DateTime FechaToma = DateTime.Now.Date;
            UpdateNota NotaNueva = new UpdateNota();
            NotaNueva.q = "addNotaAsset";
            NotaNueva.IdAsset = int.Parse(inv);

            NotaNueva.idAdminUser = Global.SeguridadUsr.usuario.ID;
            NotaNueva.nota = "Toma de Inventario : " + FechaToma.ToString("yyyy-MM-dd");
            string url = Global.urlBase + "/ajaxEquipos.php";
            WebService.PostData< UpdateNota>(url,NotaNueva);
            //if (rc.rc != "0")
            //{
            //    MessageBox.Show("Error: No se pudo actualizar");
            //}
            //Refresco rx = new Refresco();
            Refresco.RefrescarLocal();

        }


        private void xMov(Object sender, EventArgs e)
        {
            List<string> listid_equipo = new List<string>();
            int counter;
            for (counter = 0; counter < (dgvAsset.SelectedRows.Count); counter++)
            {
                listid_equipo.Add(dgvAsset.SelectedRows[counter].Cells["ID_Inv"].Value.ToString());
            }
            Console.WriteLine(listid_equipo);
            frmMovEq frm = new frmMovEq();
            frm.CargarLista(listid_equipo);
            frm.ShowDialog();

        }
        private void mnuContext_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            MessageBox.Show("entré al menu contextual con click");
            string inv = dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString();
            string estado = dgvAsset.CurrentRow.Cells["Estado"].Value.ToString();
            // string idestado = "";


        }

        private void importarDesdeExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarDesdeArchivo();
        }

        
    }
}
