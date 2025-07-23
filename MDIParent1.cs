using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.DirectoryServices.AccountManagement;
using Serilog.Sinks.SystemConsole;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using CAD_Inv;
using SpreadsheetLight;

namespace InventarioAsset
{
    
    public partial class MDIParent1 : Form
    {
        public string IDUsuario;
        private int childFormNumber = 0;
        
        DataLogin m = new DataLogin();
        public MDIParent1()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

      
       

     

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

       

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Run(new Form5());
            ConsultaInv newMDIChild = new ConsultaInv();
           // Form1 newMDIChild = new Form1();
            //newMDIChild.setModo(Constants.Inventario);
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();

        }

        

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPedido newMDIChild = new frmPedido();
  
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        
        
        private void MDIParent1_Load(object sender, EventArgs e)
        {
           try
            {
                Console.WriteLine("cifrado:");

                Global.PathAPP = Path.Combine(Application.StartupPath);

                Global.PathRes = Global.PathAPP + @"\Resources\";
                Global.PathLog = @"\Tmp\Logs\";

                //Si el directorio de log no existe lo crea
                if (!Directory.Exists(Global.PathLog))
                    Directory.CreateDirectory(Global.PathLog);


                Global.BaseSQL = @" mgddb012.mgapps.metrogas.com.ar\G4I_INENTAT1";
                Global.strConnBaseSQL = "Data Source = " + Global.BaseSQL + ",1898; User ID = app_inventario; Password = qhy00hVAFNH!9sbqGQo3470N";

                string Ambiente = Properties.Settings.Default.ambiente;
                
                switch (Ambiente )
                {
                    case "PROD":
                        Global.urlBase = "https://assets.metrogas.com.ar/vista/ajax/";
                        toolStripStatusLabel1.Text = "Produccion";
                        //toolStripStatusLabel1.ForeColor = Color.White;
                        //label1.Text = "Produccion";
                        //label1.BackColor = Color.Red;
                        break;
                    case "TEST":
                        Global.urlBase = "http://mgiap048.metrogas.com.ar/vista/ajax/";
                        toolStripStatusLabel1.Text = "Testing";
                        //label1.BackColor = Color.Yellow;
                        break;
                    case "LAB":
                        Global.urlBase = "http://mglab010.metrogas.com.ar/pgil/vista/ajax/";
                        toolStripStatusLabel1.Text = "Laboratorio";
                        //label1.BackColor = Color.LightGreen;
                        break;
                }
               
                if (!chkConn(Global.urlBase))
                {
                    MessageBox.Show("No hay conexion a " + Global.urlBase);
                    this.Close();
                        return;
                }
                //label1.Text = Ambiente;
                //toolStripStatusLabel1.Text = Global.PathAPP;

     //           Global.xc = new User_Sec(Global.urlBase + "/login.php");
                //Global.SeguridadUsr = Global.xc.JSONpost(Global.m);


                if (Properties.Settings.Default.Login == true)
                {


                    using (frmlogin frm = new frmlogin())
                    {
                        frm.ShowDialog();
                        if (!frm.accesoOK)
                        {
                            MessageBox.Show("Acceso denegado\nEl programa se cerrara");

                            this.Close();

                        }
                        else
                        {
                           MessageBox.Show("Ingreso Exitoso" );
                        }
                        Global.xc = new User_Sec(Global.urlBase + "/login.php");
                        Global.SeguridadUsr = Global.xc.JSONpost(Global.m);
                    }
                }
                else // SI estamos desarrollando tengo que hardcodear el login
                {
                    Seguridad cif = new Seguridad();
                    Global.m.user = "nborucki";
                    string cachus = "d2kcqQfdOl1pfY6Tm8+NVA==";
                    Global.m.password = cif.Descifrar(cachus);
                    /* --------------------------------------------------------
                     * Cifrar
                     * --------------------------------------------------------*/

                    //string textoplano = "";
                    //Console.WriteLine(cif.Cifrar(textoplano));

                    Global.xc = new User_Sec(Global.urlBase + "/login.php");
                   Global.SeguridadUsr = Global.xc.JSONpost(Global.m);
                   


                    string k = Global.SeguridadUsr.login.response;
                    if (k.Contains("error"))
                    {
                        MessageBox.Show("La contraseña es incorrecta\nEl programa se cerrara");
                        this.Close();
                    }

                   
                }

                //// --- Asignar permisos sobre los menúes
                foreach (ToolStripMenuItem mnuitOpcion in this.menuStrip.Items)
                {
                    if (mnuitOpcion.Name== "controlDeMovimientosToolStripMenuItem")
                        Console.WriteLine(mnuitOpcion.Name);
                    
                    if (mnuitOpcion.DropDownItems.Count > 0)
                    {
                         this.CambiarOpcionesMenu(mnuitOpcion.DropDownItems);
                      

                    }
                }
                tempToolStripMenuItem.Visible = true;
                tempToolStripMenuItem.Enabled = false;
                // -- Jarcodeo permisos menu filtro y menu ayuda -- //
                filtrosToolStripMenuItem.Enabled = true;
                helpMenu.Enabled = true;
                aboutToolStripMenuItem.Enabled = true;

                //Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
                //JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();

                //Refresco rx = new Refresco();
                //rx.RefrescarLocal();

                toolStripMenuItem1.Text =  Global.SeguridadUsr.usuario.USER_ID;
                            
                
                // --- Mostrar pendientes del usuario
                

            }
            catch (Exception ex)
            {
                ELog.save(this, ex);
            }
        }
        
        public bool chkConn(string conn)
        {
            bool Estado = false;
            System.Uri Url = new System.Uri(conn);

            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objetoResp;

            try
            {
                objetoResp = WebRequest.GetResponse();
                Estado = true;
                objetoResp.Close();
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                Estado = false;
            }
            WebRequest = null;
            return Estado;
        }
        private void CambiarOpcionesMenu(ToolStripItemCollection colOpcionesMenu)
        {
            foreach (ToolStripItem itmOpcion in colOpcionesMenu)
            {
               
                bool encontro = false;
               
                ToolStripItem encontrado;
                foreach (Menu mnu in Global.SeguridadUsr.menu)
                {
                    //if (mnu.OPCION == "resetRevisionToolStripMenuItem")
                    //{
                    //    Console.WriteLine(mnu.OPCION);
                    //}
                    if (mnu.OPCION == itmOpcion.Name)
                    {
                        encontro = true;
                        encontrado= itmOpcion;
                        if (((ToolStripMenuItem)itmOpcion).DropDownItems.Count > 0)
                        {

                            this.CambiarOpcionesMenu(((ToolStripMenuItem)itmOpcion).DropDownItems);
                            Console.WriteLine(itmOpcion.Name);
                            //itmOpcion.Enabled = true;
                        }
                        break;
                    }
                }
                itmOpcion.Enabled = encontro;

            }
        }
        //private void asignacinToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmAsignacion newMDIChild = new frmAsignacion();
        //    newMDIChild.setModo(Constants.Asigna);

        //    // Set the Parent Form of the Child window.
        //    newMDIChild.MdiParent = this;
        //    // Display the new form.
        //    newMDIChild.Show();
        //}



        //private void aprobacionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmAsignacion newMDIChild = new frmAsignacion();
        //    newMDIChild.setModo(Constants.Aprueba);
        //    // Set the Parent Form of the Child window.
        //    newMDIChild.MdiParent = this;
        //    // Display the new form.
        //    newMDIChild.Show();
        //}

        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEtiquetas newMDIChild = new frmEtiquetas();

            //// Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            //// Display the new form.
            newMDIChild.Show();


        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 newMDIChild = new AboutBox1();

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void bajasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBajas newMDIChild = new frmBajas();

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void mapasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mapa newMDIChild = new Mapa();

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void propiedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrincipalContext contexto = new PrincipalContext(ContextType.Domain, "mgapps.metrogas.com.ar", "CN=USERS,DC=MGAPPS,DC=METROGAS,DC=COM,DC=AR");
            GroupPrincipal insGroupPrincipal = new GroupPrincipal(contexto);
            frmPropiedades insFrmProperties = new frmPropiedades(insGroupPrincipal);
            insFrmProperties.ShowDialog();
            //if (insFrmProperties.DialogResult == DialogResult.OK)
            //{
            //    buscaGrupos(insGroupPrincipal);
            //}
        }

       
        private void tempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelevamiento newMDIChild = new frmRelevamiento();

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void movimientosManualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmMovimiento newMDIChild = new frmMovimiento();

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void confirmacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignacion newMDIChild = new frmAsignacion();
            newMDIChild.setModo(Constants.Aprueba);
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void MovbastoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMovEq newMDIChild = new frmMovEq();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void aBMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPuesto newMDIChild = new frmPuesto();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void aprobacionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAsignacion newMDIChild = new frmAsignacion();
            newMDIChild.setModo(Constants.Aprueba);
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void ABMPuestoMnu_Click(object sender, EventArgs e)
        {
            frmPuesto newMDIChild = new frmPuesto();
            //newMDIChild.setModo(Constants.Aprueba);
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmData frm = new frmData();
            frm.Text = "Grupos asignados";
            //Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + dgvAsset.CurrentRow.Cells["ID_Inv"].Value.ToString());
            //JSONNotas jma = ma.JSONget();
            //frm.DataSource(jma);
            List<string> perfiles = new List<string>();
            perfiles = Global.SeguridadUsr.usuario.PERFILES.Split(',').ToList();
            frm.DataSource(perfiles);
            frm.Show();
        }

        private void logDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controlDeMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logactividad frm = new logactividad();

            // Set the Parent Form of the Child window.
            frm.MdiParent = this;
            //Logs log = new Logs();
            //List<Logs> xx = log.GetLog("20221220", "20221228");
            // Display the new form.
            //frm.DataSource( xx);

            frm.Show();
            //Logs log = new Logs();
            //List<Logs> xx = log.MostrarLog("20221201", "20221228");
            //Console.WriteLine("hola");
        }

        private void asignacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsignacion newMDIChild = new frmAsignacion();
            newMDIChild.setModo(Constants.Asigna);

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void asignadosAMiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Equipos que tengo asignados
        }

        private void resetRevisionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dlgResult = MessageBox.Show("Esta a punto de resetear la información de la revision. Acepta??", "Confirmacion", MessageBoxButtons.YesNo);
            if (dlgResult == DialogResult.No)
                return;
            
            IO RepEstado = new IO();
           // DateTime fecha = new DateTime( );
            List<EquipoExt> reporteRevisados = new List<EquipoExt>();
            reporteRevisados = Global.TodosLosAsset.getEquiposRevisados();
            RepEstado.exportar(reporteRevisados,"Reporte_Encontrados_"+ DateTime.Now.ToString("ddMMyyyy"));
            Equipo ex = new Equipo();


            RetCode rc = ex.setRevisado("-1");
            // Agrego una nota indicando que de revisó el equipo
            //DateTime FechaToma = DateTime.Now.Date;
            //UpdateNota NotaNueva = new UpdateNota();
            //NotaNueva.q = "addNotaAsset";
            //NotaNueva.IdAsset = int.Parse(inv);

            //NotaNueva.idAdminUser = Global.SeguridadUsr.usuario.ID;
            //NotaNueva.nota = "Toma de Inventario : " + FechaToma.ToString("yyyy-MM-dd");
            //string url = Global.urlBase + "/ajaxEquipos.php";
            //WebService.PostData<UpdateNota>(url, NotaNueva);
        }

        private void aBMDeTiposToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ABMEquipoMnu_Click(object sender, EventArgs e)
        {

        }

        private void modSerieToolStripMenuItem_Click(object sender, EventArgs e)
        {

            modSerie newMDIChild = new modSerie();
            //newMDIChild.setModo(Constants.Asigna);

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void todosLosEquiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            List<EquipoExt> todos = new List<EquipoExt>();
            todos = Global.TodosLosAsset.GetEquipos();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Libro de excel (*.xlsx)|*.xlsx|Libro de excel (*.xls)|*.xls";
            saveFileDialog1.Title = "Guardar el archivo";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();


                fs.Close();
            }
            Cursor.Current = Cursors.WaitCursor;
            dt = converter.ToDataTable(todos);
            SLDocument mydoc = new SLDocument();
            mydoc.AddWorksheet("Inventario");
            mydoc.ImportDataTable(1, 1, dt, true);

            mydoc.SaveAs(saveFileDialog1.FileName);
            Cursor.Current = Cursors.Default;
        }

        private void aBMDeEstadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listadoDeRevisadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogResult dlgResult = MessageBox.Show("Esta a punto de resetear la información de la revision. Acepta??", "Confirmacion", MessageBoxButtons.YesNo);
            //if (dlgResult == DialogResult.No)
            //    return;

            IO RepEstado = new IO();
            // DateTime fecha = new DateTime( );
            List<EquipoExt> reporteRevisados = new List<EquipoExt>();
            reporteRevisados = Global.TodosLosAsset.getEquiposRevisados();
            RepEstado.exportar(reporteRevisados, "Reporte_Encontrados_" + DateTime.Now.ToString("ddMMyyyy"));
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {

        }

        private void filtrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmFiltro frm = new frmFiltro();
            frmFiltro newMDIChild = new frmFiltro();
            //newMDIChild.setModo(Constants.Asigna);

            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
            //frm.Show();
        }
    }


    //public static class Global
    //{
    //    public static string PathAPP, PathRes, PathLog;

    //    public static string strConnBaseOracle, strConnBaseSQL, strConnBaseAccess;
    //    public static string BaseOracle,BaseSQL,BaseAccess;
    //    // Variable global que tiene la seguridad del usuario
    //    public static JSONuserSec SeguridadUsr;
    //    public static User_Sec xc;
    //    public static string urlBase;
    //    public static AllAssets TodosLosAsset;
    //}
    public static class Constants
    {
        public const int Asigna = 1;
        public const int Aprueba = 2;
        public const int Inventario = 1;
        public const int Asset = 2;
        public const string REPORTES = "C:\\Users\\pgil\\Mis_Programas\\C#\\InventarioAsset\\winLdapAv\\Reporte";
        public const int TEST = 1;
        public const int SIN_RELEVAR = 0;
        public const int RELEVADO = 1;
        public const int CARGADO = 2;
        public const int ERROR_RELEV = 3;
        public const int ERROR_CARGA = 4;
    }
    public static class Global
    {
        public static  DataLogin m = new DataLogin();
        public static string PathAPP, PathRes, PathLog;
        public static List<PermisoXTipo> pxt;

        public static string strConnBaseOracle, strConnBaseSQL, strConnBaseAccess;
        public static string BaseOracle, BaseSQL, BaseAccess;
        // Variable global que tiene la seguridad del usuario
        public static JSONuserSec SeguridadUsr { get; set; }
        public static User_Sec xc { get; set; }
        public static string urlBase;
        public static AllAssets TodosLosAsset;
        

    }
}
