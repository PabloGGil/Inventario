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
//using CL_Inv;


namespace InventarioAsset
{
    public partial class frmRelevamiento : Form
    {
        DataTable dt = new DataTable();

        /* -----------------------------
        Comentario
        ComentarioCarga
        ComentarioReleva
        error
        Estado
        FechaCarga
        FechaRelevo
        id
        Inventario
        PowerappID
        Puesto
        PuestoOLD
        Relevador
        Usuario
        UsuarioOLD
        -----------------------------*/
        public class Registro
        {
            public bool error;
            private int _id;
            private DateTime _FechaRelevo;
            private DateTime _FechaCarga;
            private string _inventario;
            private string _usuario;
            private string _puesto;

            public DateTime FechaRelevo
            {
                get => _FechaRelevo;
                set
                {
                    _FechaRelevo = value;
                }
            }

            public DateTime FechaCarga
            {
                get => _FechaCarga;
                set
                {
                    _FechaCarga = value;
                }
            }

            public int id
            {
                get => _id;
                set
                {
                    if (value == 0)
                    {
                        _id = 0;
                    }
                    else
                    {
                        _id = value;
                    }
                }
            }
            public string Usuario
            {
                get => _usuario;
                set
                {
                    error = false;
                    if (string.IsNullOrEmpty(value))
                    {
                        _usuario = "Error-falta Usuario";
                        error = true;

                    }
                    else if (value.Length > 8 & value!="usr_puestocomun")
                    {
                        _usuario = "error-nro de caracteres incorrecto";
                        error = true;
                    }
                    else
                    {
                        _usuario = value;
                    }
                }
            }
            public string Inventario
            {

                get => _inventario;
                set
                {
                    bool r = value.All(char.IsDigit);
                    if (string.IsNullOrEmpty(value))
                    {
                        _inventario = "error- INventario vacio";
                        error = true;
                    }
                    else if (value == "0")
                    {
                        _inventario = "error-Inventario no existe en asset";
                        error = true;
                    }
                    else
                    {
                       
                       
                            Console.WriteLine(r);
                            _inventario = value.Replace("\n", string.Empty).Replace(".", string.Empty).Replace(" ", string.Empty);
                            r = _inventario.All(char.IsDigit);
                            _inventario = Parser(_inventario, @"(\d+)");
                            if (string.IsNullOrEmpty(_inventario) || !r)
                            {
                                _inventario = "error-Formato incorrecto";
                                error = true;
                            }
                        
                    }
                    Console.WriteLine(_inventario);
                }
                
            }
            public string Puesto
            {

                get => _puesto;
                set
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        _puesto = "error-Puesto vacio";
                        error = true;
                    }
                    else
                    {
                        _puesto = value;
                        _puesto.Replace("\n", string.Empty).Replace(".", string.Empty).Replace(" ", string.Empty);
                        _puesto = Parser(_puesto, @"(\w{3}-[a-zA-Z0-9]{1}-\d{3})");
                        if (string.IsNullOrEmpty(_puesto))
                        {
                            _puesto = "error-Formato incorrecto";
                            error = true;
                        }
                    }
                }

            }
            public string Comentario { get; set; }
            //public DateTime FechaRelevo { get; set; }
            //public DateTime FechaCarga { get; set; }
            public string PowerappID { get; set; }
            public string Relevador { get; set; }
            public int Estado { get; set; }
            public string ComentarioCarga { get; set; }
            public string ComentarioReleva { get; set; }
            public string PuestoOLD { get; set; }
            public string UsuarioOLD { get; set; }

            public Registro()
            {
                error = false;
                Puesto = "";
                PuestoOLD = "";
                UsuarioOLD = "";
                ComentarioCarga = "";
                ComentarioReleva = "";
                FechaCarga = DateTime.Now;
                FechaRelevo = DateTime.Now;
                Usuario = "";
                Inventario = "1";
                Comentario = "";
                Estado = 0;
                id = 0;
                PowerappID = "";
                Puesto = "";
                Relevador = "";
            }

            public void mostrarData(Registro x)
            {
                Console.WriteLine("Fila Excel : {0}", x.id);
                Console.WriteLine("Usuario    : {0}", x.Usuario);
                Console.WriteLine("Inventario : {0}", x.Inventario);
                Console.WriteLine("Puesto     : {0}", x.Puesto);
                Console.WriteLine("Comentario : {0}", x.Comentario);
                Console.WriteLine("fecha Rele : {0}", x.FechaRelevo);
                Console.WriteLine("fecha carga: {0}", x.FechaCarga);
                Console.WriteLine("powerapp   : {0}", x.PowerappID);
                Console.WriteLine("relevador  : {0}", x.Relevador);
                Console.WriteLine("cargado    : {0}", x.Estado);
                Console.WriteLine("Comen Carga: {0}", x.ComentarioCarga);
                Console.WriteLine("Comen Relev: {0}", x.ComentarioReleva);
                Console.WriteLine("Usuario OLD: {0}", x.UsuarioOLD);
                Console.WriteLine("Puesto  OLD: {0}\n", x.PuestoOLD);
            }
            public void EjecutaQry(SqlCommand comm, Registro r)
            {
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("@ID", r.id);
                comm.Parameters.AddWithValue("@Usuario", r.Usuario);
                comm.Parameters.AddWithValue("@Inventario", r.Inventario);
                comm.Parameters.AddWithValue("@Puesto", r.Puesto);
                comm.Parameters.AddWithValue("@Comentario", r.Comentario);
                comm.Parameters.AddWithValue("@FechaRelevo", r.FechaRelevo);// FechaRelevo);
                comm.Parameters.AddWithValue("@FechaCarga", r.FechaCarga);
                comm.Parameters.AddWithValue("@PowerappID", r.PowerappID);
                comm.Parameters.AddWithValue("@Relevador", r.Relevador);
                comm.Parameters.AddWithValue("@Estado", r.Estado);
                comm.Parameters.AddWithValue("@ComentarioCarga", r.ComentarioCarga);
                comm.Parameters.AddWithValue("@ComentarioReleva", r.ComentarioReleva);
                comm.Parameters.AddWithValue("@PuestoOLD", r.PuestoOLD);
                comm.Parameters.AddWithValue("@UsuarioOLD", r.UsuarioOLD);
                comm.ExecuteNonQuery();
                //return comm;
            }
            
            public Registro Fila2Registro(DataRow dr)
            {
                Registro rx = new Registro();
                rx.id = Convert.ToInt32(dr["ID"].ToString());
                rx.Usuario = dr["Usuario"].ToString();
                rx.Inventario = dr["Inventario"].ToString();
                rx.Puesto = dr["Puesto"].ToString();
                rx.Comentario = dr["Comentario"].ToString();
                rx.FechaRelevo = Convert.ToDateTime(dr["FechaRelevo"]);
                rx.FechaCarga = Convert.ToDateTime(dr["FechaCarga"]);
                rx.PowerappID = dr["PowerappID"].ToString();
                rx.Relevador = dr["Relevador"].ToString();
                rx.Estado = Convert.ToInt32(dr["Estado"].ToString());
                rx.ComentarioReleva = dr["ComentarioReleva"].ToString();
                rx.ComentarioCarga = dr["ComentarioCarga"].ToString();
                rx.PuestoOLD = dr["PuestoOLD"].ToString();
                rx.UsuarioOLD = dr["UsuarioOLD"].ToString();
                return rx;
                //}
            }
        }
        public frmRelevamiento()
        {
            InitializeComponent();
        }

        
       DataTable TabReleva;

        public static string Parser(string cadena,string patern)
        {
            
            MatchCollection resultados = Regex.Matches(cadena, patern);
            if (resultados.Count == 0)
            {
                return null;
            }
            else
            {
                return resultados[0].Groups[1].Value;
            }
        }
        private void frmRelevamiento_Load(object sender, EventArgs e)
        {
            RefrescaStatus();
        }


        public RetCode MoverEq(Registro re)
        {
            //JSONUsr xusr = new JSONUsr(Global.urlBase + "/ajaxEquipos.php?q=u");
            //xusr = xusr.JSONget();
            List<Ninventario> inv = new List<Ninventario>();
            Ninventario xc = new Ninventario();
            xc.id = re.Inventario;
            inv.Add(xc);
            MovEquipo moverC = new MovEquipo(Global.urlBase + "/ajaxEquipos.php");
            JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");

            mover.Formulario = "";
            mover.idAssets = inv.ToArray();
            mover.statusDest = "17"; //asignar a tecnico
            mover.q = "cas";
            mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
            mover.statusOrig = "7";  //pañol
            //mover.FechaHasta = "2022-03-10T15:27:43.881Z";
            mover.FechaHasta = DateTime.Now.ToLongDateString();
            Usuario x = xusr.getDataxUsr(re.Usuario);
            if (x != null)
                mover.idUsuarioDestino = x.ID;
            else
                mover.idUsuarioDestino = "983";
            mover.descripcion = "Relevamiento del inventario en MG ";
            mover.ID_PUESTO = re.Puesto;
            if (re.Puesto == "PAN-0-000") 
            { 
                mover.statusDest = "6"; 
            }
           // mover.OS = "22";
            RetCode k = moverC.JPost(mover);
            //mover.MostrarData(mover);

            //            // txtmensaje.Text = x.putJ(x).ToString();
            inv = null;
            return k;
        }

        JSONUsr xusr;
        public void CrearTablas()
        {
            DataColumn columna;//, colInv;
           // DataRow rowRel, rowInv;
            TabReleva = new DataTable("relevamiento");

            columna = new DataColumn();
            columna.DataType = typeof(string);
            columna.ColumnName = "Usuario";
            TabReleva.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = typeof(string);
            columna.ColumnName = "Inventario";
            TabReleva.Columns.Add(columna);

            columna = new DataColumn();
            columna.DataType = typeof(string);
            columna.ColumnName = "Puesto";
            TabReleva.Columns.Add(columna);
            
            columna = new DataColumn();
            columna.DataType = typeof(int);
            columna.ColumnName = "FilaExcel";
            TabReleva.Columns.Add(columna);
         }

        /*
        //-----------------------------------------------------
        // Toma los registros de la base y los impacta en Asset
        //-----------------------------------------------------
        */
        private void btnImpactar_Click(object sender, EventArgs e)
        {
            int avance;
            int cant;
            int i = 0;
            //int estado = 0;

            try
            {
                progressBar1.Value = 0;
                label7.Text = "0%";

                xusr = new JSONUsr();

                xusr = xusr.JSONget();

                SqlConnection csql = new SqlConnection();
                csql = InventarioAsset.BaseDatos.ConectarSQL();
                string qryID = "select * from [INVENTAT1].[dbo].[Relavamiento] where estado=1 ";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
                Registro r = new Registro();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = csql;
                cmd.CommandText = "update [INVENTAT1].[dbo].[Relavamiento] set estado= @estado where ID= @fila";
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@fila", SqlDbType.Int);


                qryID = "select count(ID) cuenta from [INVENTAT1].[dbo].[Relavamiento] where estado=1 ";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
                cant = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
                progressBar1.Maximum = cant;


                qryID = "select * from [INVENTAT1].[dbo].[Relavamiento] where estado=1 ";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);

                foreach (DataRow dr in dt.Rows)
                {

                    Console.WriteLine(dr["ID"].ToString());


                    // Realizo el movimiento en ASSET
                    RetCode x = MoverEq(r.Fila2Registro(dr));
                    Console.WriteLine(x.msg);

                    cmd.Parameters["@fila"].Value = dr["ID"];
                    if (x.msg == "ok")
                    {
                        //estado = 2;
                        cmd.Parameters["@estado"].Value = 2;
                    }
                    else
                    {
                        //estado = 4;
                        cmd.Parameters["@estado"].Value = 4;
                    }
                    cmd.ExecuteNonQuery();

                    //Actualizo la barra de progreso
                    avance = (int)(i + 1) * 100 / cant;
                    progressBar1.Increment(1);
                    label7.Text = progressBar1.Value.ToString() + "%";
                    Console.WriteLine(avance);

                    i++;
                    //if (i == 500)
                    //    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
            }
        }

        //-----------------------------------------------------
        // Acepta los movimientos realizados
        //-----------------------------------------------------
        public void AceptarEquipo()
        {

            try
            {
                progressBar1.Value = 0;
                Cursor.Current = Cursors.WaitCursor;
                Transconf1 z = new Transconf1();
                //Confirmaciones konf = new Confirmaciones(Global.urlBase + "/ajaxEquipos.php?q=tconfirm");
                //Confirmaciones aplicar = new Confirmaciones(Global.urlBase + "/ajaxEquipos.php?");
                //JSONConfirma jkonf = konf.JSONget();
                Confirmaciones konf = new Confirmaciones();
                List<Confirmaciones> lc = konf.ListarPendientesxUsr(Global.SeguridadUsr.usuario.ID);
                
                int cant = lc.Count;
                progressBar1.Maximum = cant;

                for (int i = 0; i < cant; i++)
                {
                    //if (jkonf.coleccion.transconf[i].SYS_USER == Global.SeguridadUsr.usuario.ID)
                    //{
                    RetCode result=konf.Aprobar(lc[i].Inventario);
                        //   z.q = "adm-pr";
                        //z.idAdminUser = Global.SeguridadUsr.usuario.ID;
                        //z.token = jkonf.coleccion.transconf[i].token;
                        //z.opcion = 1;
                        //RetCode result = aplicar.JSONPost(z);
                        Console.WriteLine("Inv: {0}\nMensaje: {1}", lc[i].Inventario, result.msg);
                        //int avance = (int)(i + 1) * 100 / cant;
                        progressBar1.Increment(1);
                        //Console.WriteLine(avance);
                        //MessageBox.Show(result);
                        label7.Text = progressBar1.Value.ToString() + "%";
                    //}
                }
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
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

        //-----------------------------------------------------
        // Carga las filas del excel en la base de datos
        //-----------------------------------------------------
        private void btnCarga_Click(object sender, EventArgs e)
        {

            
            Registro reg = new Registro();
            DataRow rowRel;
            int avance = 0;
 
            progressBar1.Value = 0;
            label7.Text = "0%";
            int nerr=0;

            string filePath = @"C:\Users\pgil\OneDrive - MetroGAS S.A\Inventario\Carga de datos\LTS_INV_prod_2022.07.15.xlsx";
            //string JonyPath = @"https://metrogasar-my.sharepoint.com/personal/jrios_metrogas_com_ar/Documents/Inventario/LTS_INV.xlsx";
            ////https://metrogasar-my.sharepoint.com/:x:/r/personal/pgil_metrogas_com_ar/_layouts/15/Doc.aspx?sourcedoc=%7BAB7DF913-0297-40C3-BD0D-516BFE29DE07%7D&file=LTS_INV.xlsx&action=default&mobileredirect=true

           

            try
           {
                label10.Text = "";
                label4.Text = "";
                label6.Text = "";

                CrearTablas();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "todos|*";
                openFileDialog1.Title = "Abrir el archivo";
                openFileDialog1.ShowDialog();
                filePath= openFileDialog1.FileName;

                SqlConnection csql = new SqlConnection();
                //iRow indica la Fila donde comienzan los datos válidos
                int iRow=0;
                string qryID = "select max(ID) from [INVENTAT1].[dbo].[Relavamiento]";
                csql = InventarioAsset.BaseDatos.ConectarSQL();
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
 
                // Determino cual es la ultima fila del excel que esta cargada en la tabla
                if (dt.Rows[0].ItemArray[0] == System.DBNull.Value)
                {
                    iRow = 5;
                    //reg.id = iRow;
                }
                else
                {
                    //iRow= Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) + 1;
                    iRow = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString())+1 ;
                    //reg.id =  Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) +1;
                    //reg.id = reg.id + 1;
                }
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = csql;
                using (SLDocument sl = new SLDocument(filePath))
                {
                    int TotalFilas = iRow;
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(TotalFilas, 2)))
                    {
                        TotalFilas++;
                    }
                    // ---- pre seleccion si no es la planilla original
                   // iRow = 5;
                    progressBar1.Maximum = TotalFilas-6;
                    
                    Cursor.Current = Cursors.WaitCursor;
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 2)))
                    {
                        //if (iRow ==2022 || iRow == 23)
                        //    Console.WriteLine("aca tengo error");
                        reg.id = iRow+ TotalFilas;
                        reg.Usuario = sl.GetCellValueAsString(iRow, 2);
                        reg.Inventario = sl.GetCellValueAsString(iRow, 3);
                        reg.Puesto = sl.GetCellValueAsString(iRow, 4).ToUpper();
                        reg.Comentario= sl.GetCellValueAsString(iRow, 5);
                        reg.Relevador= sl.GetCellValueAsString(iRow, 6);
                        string x = sl.GetCellValueAsString(iRow, 7);
                        if (string.IsNullOrEmpty(x))
                            reg.FechaRelevo = Convert.ToDateTime("01/01/1900");
                        else
                             reg.FechaRelevo = DateTime.FromOADate(Convert.ToDouble(sl.GetCellValueAsString(iRow, 7)));
                            //reg.FechaRelevo = Convert.ToDateTime(sl.GetCellValueAsString(iRow, 7));
                        reg.PowerappID = sl.GetCellValueAsString(iRow, 8);
                        // Buscar la info existente en el Asset
                        List<EquipoExt> eq = new List<EquipoExt>();
                        if (iRow == 13)
                            Console.WriteLine("error#");
                        if (!reg.Inventario.Contains("error"))                           
                    //        eq=Global.TodosLosAsset.GetListaEquipos(reg.Inventario);

                        if (eq.Count != 0 )
                        {
                            if (eq[0].Usuario == null)
                            {
                                reg.UsuarioOLD = "";
                            }
                            else
                            {
                                reg.UsuarioOLD = eq[0].Usuario;
                            }
                            if (eq[0].Puesto == null)
                            {
                                reg.PuestoOLD = "";
                            }
                            else
                            {
                                reg.PuestoOLD = eq[0].Puesto;
                            }
                           
                        }
                        else
                        {
                            reg.Inventario="0";
                            reg.UsuarioOLD = "";
                            reg.PuestoOLD = "";
                        }

                        DateTime ahora = DateTime.Now;
                        reg.FechaCarga = ahora;
                        
                        if (reg.error)
                        {
                            rowRel = TabReleva.NewRow();
                            reg.Estado =Constants.ERROR_RELEV;
                            rowRel["Usuario"] = reg.Usuario;
                            rowRel["inventario"] = reg.Inventario;
                            rowRel["puesto"] = reg.Puesto;
                            rowRel["FilaExcel"] = reg.id;
                            TabReleva.Rows.Add(rowRel);
                            nerr++;
                        }
                        else
                        {
                            reg.Estado = Constants.RELEVADO;
                        }
                        string qry = "insert into [INVENTAT1].[dbo].[Relavamiento] " +
                            "(ID,Usuario,Inventario,Puesto,Comentario,FechaRelevo,FechaCarga,PowerappID,Relevador,Estado,ComentarioReleva,ComentarioCarga,PuestoOLD,UsuarioOLD) " +
                            "values" +
                            "(@ID,@Usuario,@Inventario,@Puesto,@Comentario,@FechaRelevo,@FechaCarga,@PowerappID,@Relevador,@Estado,@ComentarioReleva,@ComentarioCarga,@PuestoOLD,@UsuarioOLD)";
                        cmd.CommandText =qry;
                        
                        
                        reg.mostrarData(reg);
                        reg.EjecutaQry(cmd,reg);
                        
                        //reg.id = iRow;
                        avance = (int)(iRow + 1) * 100 / TotalFilas;
                        progressBar1.Step=1;
                        //progressBar1.Value = avance;
                        label7.Text = progressBar1.Value.ToString() + "%";
                        //progressBar1.Increment (1);
                        //label7.Text = progressBar1.Value.ToString() +"%";
                        //i++;
                        iRow++;
                    }
                    
                }
                Cursor.Current = Cursors.Default;
                dgvReleva.DataSource = TabReleva;
                Console.WriteLine("total de errores: {0}", TabReleva.Rows.Count);
               
                

                RefrescaStatus();
                // -----------Query para borrar los repetidos, deja solo la ultima aparición del inventario
                //string qry1 = "delete from[INVENTAT1].[dbo].[Relavamiento]" +
                //             "where id in (select min(ID) from[INVENTAT1].[dbo].[Relavamiento]" +
                //             "group by inventario having  count(Inventario) > 1)";
                //SqlCommand cmd1 = new SqlCommand();
                //cmd1.Connection = csql;
                //cmd1.CommandText = qry1;
                //cmd1.ExecuteNonQuery();
                
            }
            catch (Exception ex)
           {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
           }
        }

        public void RefrescaStatus()
        {

            DataTable dt = new DataTable();
            SqlConnection csql = new SqlConnection();
            csql=InventarioAsset.BaseDatos.ConectarSQL();
            int iRow = 5;
            int nrel = 0;
            int nok = 0;
            int nerr = 0;
            string qryID;
            qryID = "select count(ID) from [INVENTAT1].[dbo].[Relavamiento]";
            dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
            Console.WriteLine(dt.Rows[0].ItemArray[0].ToString());

            nrel = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) - iRow;
            //nrel = iRow - reg.id;
            label10.Text = nrel.ToString();
            label21.Text = dt.Rows[0].ItemArray[0].ToString();

            qryID = "select count(ID) from [INVENTAT1].[dbo].[Relavamiento] where estado=3";
            dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
            Console.WriteLine(dt.Rows[0].ItemArray[0].ToString());
            //nerr = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) - iRow;
            label17.Text = dt.Rows[0].ItemArray[0].ToString();
            label6.Text = nrel.ToString();

            qryID = "select count(ID) from [INVENTAT1].[dbo].[Relavamiento] where estado=1";
            dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
            Console.WriteLine(dt.Rows[0].ItemArray[0].ToString());
            //nok = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) - iRow;
            nok = nrel - nerr;
            label4.Text = nok.ToString();
            label19.Text = dt.Rows[0].ItemArray[0].ToString();
        }
        private void cmdConfirma_Click(object sender, EventArgs e)
        {
            AceptarEquipo();
        }

        private void dgvReleva_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //dgvInv.Rows[1].DefaultCellStyle.BackColor = Color.Red;
        }

        private void cmdExportar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlConnection csql = new SqlConnection();
            
                 csql= InventarioAsset.BaseDatos.ConectarSQL();
                string qryID;
                qryID = "select * from [INVENTAT1].[dbo].[Relavamiento] where estado=3";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
                Ofis.Export2XLS(dt);
            csql.Close();
            csql = null;
                
        }

        public RetCode MoverEq1(JMovEquipo re)
        {
            //JSONUsr xusr = new JSONUsr(Global.urlBase + "/ajaxEquipos.php?q=u");
            //xusr = xusr.JSONget();
            //List<Ninventario> inv = new List<Ninventario>();
            //Ninventario xc = new Ninventario();
            //xc.id = re.Inventario;
            //inv.Add(xc);

            //JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");

            //mover.Formulario = "";
            //mover.idAssets = inv.ToArray();
            //mover.statusDest = "17"; //asignar a tecnico
            //mover.q = "cas";
            //mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
            //mover.statusOrig = "7";  //pañol
            ////mover.FechaHasta = "2022-03-10T15:27:43.881Z";
            //mover.FechaHasta = DateTime.Now.ToLongDateString();
            //Usuario x = xusr.getDataxUsr(re.Usuario);
            //if (x != null)
            //    mover.idUsuarioDestino = x.ID;
            //else
            //    mover.idUsuarioDestino = "983";
            //mover.descripcion = "Relevamiento del inventario en MG ";
            //mover.ID_PUESTO = re.Puesto;
            //mover.OS = "22";
            MovEquipo moverC = new MovEquipo(Global.urlBase + "/ajaxEquipos.php");
            RetCode k = moverC.JPost(re);
            //re.MostrarData(re);

            //            // txtmensaje.Text = x.putJ(x).ToString();
            //inv = null;
            return k;
        }
        private void cmdUsrpcom_Click(object sender, EventArgs e)
        {
            int avance;
            int cant;
            int i = 0;
           // int estado = 0;

            try
            {
                progressBar1.Value = 0;
                label7.Text = "0%";

                xusr = new JSONUsr();

                xusr = xusr.JSONget();

                SqlConnection csql = new SqlConnection();
                csql = InventarioAsset.BaseDatos.ConectarSQL();
                string qryID = "select * from [INVENTAT1].[dbo].[Relavamiento] where usuario in ('usr-pcom','usr_pcom') ";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
                Registro r = new Registro();
                SqlCommand cmd = new SqlCommand();
                //cmd.Connection = csql;
                //cmd.CommandText = "update [INVENTAT1].[dbo].[Relavamiento] set estado= @estado where ID= @fila";
                //cmd.Parameters.Add("@estado", SqlDbType.Int);
                //cmd.Parameters.Add("@fila", SqlDbType.Int);


                qryID = "select count(ID) cuenta from [INVENTAT1].[dbo].[Relavamiento] where usuario in ('usr-pcom','usr_pcom') ";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);
                cant = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
                progressBar1.Maximum = cant;


                qryID = "select * from [INVENTAT1].[dbo].[Relavamiento] where usuario in ('usr-pcom','usr_pcom') ";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(csql, qryID);

                foreach (DataRow dr in dt.Rows)
                {

                    Console.WriteLine(dr["ID"].ToString());
                    RetCode z = new RetCode();
                    List<Ninventario> inv = new List<Ninventario>();
                    Ninventario xc = new Ninventario();
                    xc.id = dr["Inventario"].ToString();
                    inv.Add(xc);
                    JMovEquipo mover = new JMovEquipo(Global.urlBase + "/ajaxEquipos.php");

                    //inv.Add(xc);
                    mover.idAssets = inv.ToArray();
                    mover.q = "cas";

                    //mover.OS = "";
                    mover.descripcion = "Cambio de titularidad";
                    mover.FechaHasta = DateTime.Now.ToString();
                   // mover.Formulario = "";
                    // mover.idAssets = inv.ToArray();
                    mover.statusDest = "17";
                    mover.idUsuarioDestino = "14582";
                    // mover.statusOrig = cmbStatusOri.SelectedIndex.ToString(); 
                    mover.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    //mover.ID_PUESTO = "LAM-A-002";
                    mover.FechaHasta = DateTime.Now.ToLongDateString();

                    // Realizo el movimiento en ASSET
                    RetCode x = MoverEq1(mover);
                    Console.WriteLine(x.msg);

                    

                    //Actualizo la barra de progreso
                    avance = (int)(i + 1) * 100 / cant;
                    progressBar1.Increment(1);
                    label7.Text = progressBar1.Value.ToString() + "%";
                    Console.WriteLine(avance);

                    i++;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
            }
        }
    }
}
