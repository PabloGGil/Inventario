using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using System.Windows.Forms;



namespace InventarioAsset
{
    public partial class frmBajas : Form
    {
       
        OleDbConnection cx = new OleDbConnection();
        //string DGVquery = "";
        DataTable dt = new DataTable();
        string Usuario = Global.SeguridadUsr.usuario.USER_ID;
        List<EquipoExt> eqx = new List<EquipoExt>();
        List<EquipoCompras> eqcs = new List<EquipoCompras>();
        public frmBajas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Cursor.Current = Cursors.WaitCursor;
            EquipoCompras eqc = new EquipoCompras();
            try
            {
                List<string> linv = new List<string>(); 
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                eqx= Global.TodosLosAsset.getEqxEstadoxFecha("BAJA", dtdesde.Value, dthasta.Value);
                foreach (EquipoExt aux in eqx)
                {
                    linv.Add(aux.ID_Inv);
                }
                
                 eqcs=eqc.getDataCompras(linv);
               // dt = converter.ToDataTable(eqx);
               // dt = converter.ToDataTable(eqcs);


                dgView1.DataSource = eqcs;// Global.TodosLosAsset.getEqxEstadoxFecha("BAJA", dtdesde.Value, dthasta.Value);
                label3.Visible = true;
                label3.Text = dgView1.Rows.Count + " Registros";
                AjustarOrdenColumnas();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
            }
        }

        public void AjustarOrdenColumnas()
        {
            try
            {
                // Ocultar columnas
                dgView1.Columns["Usuario"].Visible = false;
                dgView1.Columns["Puesto"].Visible = false;
                dgView1.Columns["status_date"].Visible = false;
                dgView1.Columns["Detalle"].Visible = true;
                dgView1.Columns["id_remito"].Visible = false;
         //       dgView1.Columns["id_detalle"].Visible = false;


                // Ordenar 
                dgView1.Columns["ID_Inv"].DisplayIndex = 0;
                dgView1.Columns["MARCA"].DisplayIndex = 1;
                dgView1.Columns["MODELO"].DisplayIndex = 2;
                dgView1.Columns["DESCRIPCION"].DisplayIndex = 3;
                dgView1.Columns["SERIE"].DisplayIndex = 4;
                dgView1.Columns["Estado"].DisplayIndex = 5;
                dgView1.Columns["Detalle"].DisplayIndex = 6;
                dgView1.Columns["OC"].DisplayIndex = 7;
                dgView1.Columns["APEM"].DisplayIndex = 8;
                dgView1.Columns["SOLP"].DisplayIndex = 9;
                dgView1.Columns["FACTURA"].DisplayIndex = 10;
                dgView1.Columns["FECHA_OC"].DisplayIndex = 11;
                dgView1.Columns["REMITO"].DisplayIndex = 12;

                dgView1.Columns["Detalle"].HeaderText = "Observaciones";
                dgView1.Columns["ID_Inv"].HeaderText = "Inventario";
                dgView1.Columns["DESCRIPCION"].HeaderText = "Tipo";
                
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

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
                dt = converter.ToDataTable(eqcs);
                SLDocument mydoc = new SLDocument();
                mydoc.AddWorksheet("Inventario");
                mydoc.ImportDataTable(1, 1, dt, true);
               
                mydoc.SaveAs(saveFileDialog1.FileName);
                //SLDocument mydoc = new SLDocument();
                //mydoc.AddWorksheet("Inventario");
                ////mydoc.AddWorksheet("Asset");
                //mydoc.ImportDataTable(1, 1, dt, true);
                //mydoc.SaveAs(saveFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
            }
            //Ofis.ExportarDatos(dt,saveFileDialog1.FileName);
        }
    }

    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}