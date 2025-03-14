using SpreadsheetLight;
using System.Data;
using System.Windows.Forms;

namespace InventarioAsset
{
    class Ofis
    {
        public static void ExportarDatos(DataTable datalistado, string ruta)
        {

            try
            {
                SLDocument mydoc = new SLDocument();
                mydoc.AddWorksheet("Inventario");
                mydoc.AddWorksheet("Asset");
                mydoc.ImportDataTable(1, 1, datalistado, true);
                mydoc.SaveAs(ruta);
            }
            catch
            {
                throw;
            }
        }
        public static void Export2XLS(DataTable dt)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Libro de excel (*.xlsx) |Libro de excel 97-2003 (*.xls)";
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
                SLDocument mydoc = new SLDocument();
                mydoc.AddWorksheet("Inventario");
                //mydoc.AddWorksheet("Asset");
                mydoc.ImportDataTable(1, 1, dt, true);
                mydoc.SaveAs(saveFileDialog1.FileName);
            }
            //Ofis.ExportarDatos(dt,saveFileDialog1.FileName);
            catch
            {
                throw;
            }
        }
    }
}
