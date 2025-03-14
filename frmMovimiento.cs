using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetLight;
using System.Text.RegularExpressions;
using Excel=Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
//using CAD_Inv;


namespace InventarioAsset
{
    
    public partial class frmMovimiento : Form
    {
        List<string> listinv = new List<string>();
        //List<Inv_Ubicacion> auxl = new List<Inv_Ubicacion>();
        public class Inv_Ubicacion
        {
            public string inv { get; set; }
            public string puesto { get; set; }

        }
        public class Equipo
        {
            private string _status;
            public string idAssets { get; set; }
            public string status 
            {
                get => _status;
                set
                {
                    switch (value.ToUpper())
                    {
                        case "BAJA":
                            _status = "13";
                            break;

                        case "VENTA INTERNA":
                            _status = "12";
                            break;

                        case "DONACION":
                            _status = "11";
                            break;
                    }
                }
            }
            public string q { get; set; }
            public string idAdminUser { get; set; }
            //mover.statusOrig = "6";  //pañol
            //mover.FechaHasta = "2022-03-10T15:27:43.881Z";

            public string FechaBaja { get; set; }
            //mover.FechaHasta = DateTime.Now.ToLongDateString();
            public string descripcion { get; set; }
            //Usuario x = xusr.getDataxUsr(re.Usuario);
            //if (x != null)
            //    mover.idUsuarioDestino = x.ID;
            //else
            //    mover.idUsuarioDestino = "1503";
            ////mover.descripcion = re.Comentario;
            public string ID_PUESTO { get; set; }
           
        }
        public frmMovimiento()
        {
            InitializeComponent();
        }

        
        //DataTable TabReleva, TabInv20;

        

        private void frmMovimiento_Load(object sender, EventArgs e)
        {
            JsonStatuses js = new JsonStatuses();

            try
            {
                //--------------------------------------------------------
                // Cargar combos con estados
                //--------------------------------------------------------
                js = js.GetJ();
                JsonStatuses js1 = new JsonStatuses();

                List<Status> lstorigen = new List<Status>();
                lstorigen = js.coleccion.ToList();
                cmbStatusOri.DataSource = lstorigen;
                cmbStatusOri.ValueMember = "ID";
                cmbStatusOri.DisplayMember = "DESCRIPCION";

                cmbStatusDest.DataSource = js.coleccion;
                cmbStatusDest.ValueMember = "ID";
                cmbStatusDest.DisplayMember = "DESCRIPCION";
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
                
            }
        }

        private void btnImpactar_Click(object sender, EventArgs e)
        {
            try
            {          
                RetCode z = new RetCode();
                List<Ninventario> inv = new List<Ninventario>();
                Ninventario xc = new Ninventario();
                //xc.id = txtInventario.Text;
                //inv.Add(xc);
                JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");
                //---------------------------------------------
                string filePath = @"C:\Users\pgil\bajas_prod.xlsx";

                string path = Global.PathLog + "\\logBajas.log";
                StreamWriter sw = new StreamWriter(path, true);

                //StackTrace stacktrace = new StackTrace();
                //sw.WriteLine(obj.GetType().FullName + " " + hora);
                //sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - \nMensaje:" + ex.Message + "\nSource:" + ex.Source + "\nStack Trace:" + ex.StackTrace + "\nTarget Site" + ex.TargetSite);
                //sw.WriteLine("");

                //sw.Flush();
                //sw.Close();
                sw.WriteLine("Fecha;Estado ; Mensaje;Inventario ; Puesto ;Descripcion");
                using (SLDocument sl = new SLDocument(filePath))
                {
                    int iRow = 1;
                    Cursor.Current = Cursors.WaitCursor;
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                    {

                        xc.id = sl.GetCellValueAsString(iRow, 1);
                        inv.Add(xc);
                        mover.idAssets= inv.ToArray();
                    
                        mover.descripcion = sl.GetCellValueAsString(iRow, 2);
                        mover.FechaHasta = DateTime.FromOADate(Convert.ToDouble(sl.GetCellValueAsString(iRow, 3))).ToString();
                        mover.Formulario = "";
                       // mover.idAssets = inv.ToArray();
                        mover.statusDest = sl.GetCellValueAsString(iRow, 4);
                        mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
                        //mover.descripcion = "prueba de baja";

                        z = mover.MovBaja(mover);
                        sw.WriteLine(DateTime.Now.ToString() +";"+z.rc + ";"+ z.msg +";"+mover.idAssets[0].id.ToString()+";"+mover.ID_PUESTO+";"+mover.descripcion);
                        inv.Clear();
                        iRow++;
                        sw.Flush();

                    }
                    sw.Flush();
                    sw.Close();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);

            }
        }
 
 

        
        public static Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
 
 

        private void button1_Click_1(object sender, EventArgs e)
        {
            RetCode z = new RetCode();
            List<Ninventario> inv = new List<Ninventario>();
            Ninventario xc = new Ninventario();
            xc.id = txtInventario.Text;
            inv.Add(xc);
            JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");
            
            inv.Add(xc);
            mover.idAssets = inv.ToArray();
            mover.q = "cas";
            mover.descripcion = "pasar a sin_puesto";
            mover.FechaHasta = DateTime.Now.ToString();
            mover.Formulario = "";
            // mover.idAssets = inv.ToArray();
            mover.statusDest = "13";
           // mover.statusOrig = cmbStatusOri.SelectedIndex.ToString(); 
            mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
            mover.ID_PUESTO = "SIN_ASIGNAR";
            //mover.descripcion = "prueba de baja";
            //z=mover.JPost(mover);

            z = mover.MovBaja(mover);
                //txtmensaje.Text = z.msg;

            //RetCode k = MoverEq(z);
            //txtmensaje.Text = k.msg.ToString();
            Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
            JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();

        }
        public RetCode MoverEq(JMovEquipo re)
        {
            
            MovEquipo moverC = new MovEquipo(Global.urlBase + "/ajaxEquipos.php");
            RetCode k = moverC.JPost(re);
            
            return k;
        }
        private void cmdStatusPan_Click(object sender, EventArgs e)
        {
            RetCode z = new RetCode();
            List<Ninventario> inv = new List<Ninventario>();
           
            Ninventario xc = new Ninventario();
            xc.id = txtInventario.Text;
            inv.Add(xc);
            JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");
            //---------------------------------------------
           
                Cursor.Current = Cursors.WaitCursor;
           

            
                    mover.idAssets = inv.ToArray();

                    
                    mover.FechaHasta = DateTime.Now.ToString();
                    mover.Formulario = "";
            // mover.idAssets = inv.ToArray();
            mover.statusDest = "6";
                    mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    //mover.descripcion = "prueba de baja";

                    z = mover.StatusPan(mover);
           // txtmensaje.Text = z.msg;
                    inv.Clear();
            Cursor.Current = Cursors.Default;
        }

        private void dgvReleva_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //dgvInv.Rows[1].DefaultCellStyle.BackColor = Color.Red;
        }

       

        private void cmdMover_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            JSONUsr xusr = new JSONUsr();

            xusr = xusr.JSONget();
            Usuario x = new Usuario();
            if (txtusuario.Text != "")
            {
                x = xusr.getDataxUsr(txtusuario.Text);
            }
            else
            {
                x.ID = "";
            }
            List<Ninventario> inv = new List<Ninventario>();
            foreach (string aux in listinv)
            {
                Ninventario xc = new Ninventario();
                xc.id = aux;
                inv.Add(xc);
            }
            //Ninventario xc = new Ninventario();

            //Ninventario xc = new Ninventario();
            RetCode z = new RetCode();
            //List<Ninventario> inv = new List<Ninventario>();
            //Ninventario xc = new Ninventario();
            //xc.id = txtInventario.Text;
            //inv.Add(xc);
            JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");

            //inv.Add(xc);
            mover.idAssets = inv.ToArray();
            mover.q = "cas";

            //mover.OS = "997";
            mover.descripcion = "Cambio de titularidad";
            mover.Formulario = "";
            mover.statusDest = cmbStatusDest.SelectedIndex.ToString();
            mover.idUsuarioDestino = x.ID;
            // mover.statusOrig = cmbStatusOri.SelectedIndex.ToString(); 
            mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
            // mover.ID_PUESTO = txtpuesto.Text;
            // mover.FechaHasta = DateTime.Now.ToLongDateString();

            z = MoverEq(mover);
            //txtmensaje.Text = z.msg;

            Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
            JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();
            Cursor.Current = Cursors.Default;
        }

        public List<Inv_Ubicacion> CargarDesdeArchivo()
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
                
                List<Inv_Ubicacion> auxlist = new List<Inv_Ubicacion>();
                //auxlist.Add(new Inv_Ubicacion() { inv = "11878", puesto = "lom-0-001" });
                //auxlist.Add(new Inv_Ubicacion() { inv = "21798", puesto = "lam-0-000" });
                //auxlist.Add(new Inv_Ubicacion() { inv = "27777", puesto = "mon-8-009" });
                using (SLDocument sl = new SLDocument(filePath))
                {
                    int ifila = 1;
                    
                    //List<Inv_Ubicacion> auxl = new List<Inv_Ubicacion>();
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(ifila, 1)))
                    {
                        Inv_Ubicacion aux = new Inv_Ubicacion();
                        aux.inv = sl.GetCellValueAsString(ifila, 1);
                        aux.puesto = sl.GetCellValueAsString(ifila, 2);
                        auxlist.Add(aux);
                        ifila++;
                    }
                }
                return auxlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
                return null;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

            // CargarDesdeArchivo();
            List<Inv_Ubicacion> auxl = new List<Inv_Ubicacion>();
            RetCode rc = new RetCode();
            Movimientos auxm = new Movimientos();
            auxm.Solicitante= Global.SeguridadUsr.usuario.ID;
            auxm.statusDestino = "7";
            auxm.statusOrigen = "7";
            auxm.descripcion = "Asignacion masiva de puesto por relevamiento 2022";    
            auxl=CargarDesdeArchivo();
            List<Ninventario> invl = new List<Ninventario>();
            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < auxl.Count; i++)
            {
                Console.WriteLine("Inv: " + auxl[i].inv + "\nPuesto: " + auxl[i].puesto);
            }
                for (int i=0;i<auxl.Count;i++)
            {
                //Inv_Ubicacion x = new Inv_Ubicacion();
                label8.Text = Convert.ToString(i);
                Ninventario ni = new Ninventario();
                //x = auxl[i];
                Console.WriteLine("Inv: " + auxl[i].inv + "\nPuesto: " +auxl[i].puesto);
                ni.id = auxl[i].inv;
                invl.Add( ni);
                auxm.Puesto = auxl[i].puesto;
                auxm.Inventario = invl;
                rc=auxm.CambioDePuesto(auxm);
                if (rc.rc !="0")
                {
                    string msgerr = "Error : Inv: " + auxl[i].inv + " puesto: " + auxl[i].puesto;
                    //MessageBox.Show(msgerr);
                    InventarioAsset.ELog.save(msgerr);
                }
                invl.Clear();
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
