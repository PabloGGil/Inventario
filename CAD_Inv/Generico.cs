using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SpreadsheetLight;

using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace InventarioAsset
{


    public class Rootlog
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public jLog[] coleccion { get; set; }

        static Rootlog lvar;//= new Rootlog();
        string _url;

        public Rootlog()
        {
            _url = Global.urlBase + "/ajaxEquipos.php?q=log.admin&filtros=Asset&";
              // "http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=log.admin&filtros=Asset&";
        }
        public Rootlog JSONget(string filtro)
        {
            
            if (string.IsNullOrEmpty(filtro))
            {
                //seteo el filtro por default, fecha de hoy
                DateTime hoy = DateTime.Now;
                string x = hoy.Year.ToString() + hoy.Month.ToString()+ hoy.Day.ToString();
                filtro="fd="+x+"&fh="+x;

            }
            

            WebRequest oRequest = WebRequest.Create(_url+filtro);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<Rootlog>(js);

            }
            return lvar;
        }
    }

    public class jLog
    {
        public string FECHA { get; set; }
        public string IDLOG { get; set; }
        public string ID { get; set; }
        public string LOGIDUSER { get; set; }
        public string ID_USER { get; set; }
        public string TAREA { get; set; }
        public string PROCESO { get; set; }
        public string ESTADO { get; set; }
        public string OBSERVACION { get; set; }
    }

    public static class TimeHelper
    {
        public static bool HasSecondsElapsed(DateTime startTime, double seconds)
        {
            return (DateTime.Now - startTime).TotalSeconds > seconds;
        }
    }
    public  static class Refresco
    {
        static DateTime ultimoRefresco;
        
       
        public static void RefrescarLocal()
        {
            DateTime start = DateTime.Now;

            if (ultimoRefresco == null || TimeHelper.HasSecondsElapsed(ultimoRefresco, 30)) 
            {
                Global.TodosLosAsset = new AllAssets(Global.urlBase + "/ajaxEquipos.php?q=a");
                JSONAllAsset jmaa = Global.TodosLosAsset.JSONget();
                ultimoRefresco = DateTime.Now;
            }
            
        }
    }


    public static class CTEStatus
    {
        public const string INDEFINIDO = "-99";
        public const string ACTIVO = "1";
        public const string INACTIVO = "2";
        public const string E_ORDEN_COMPRA = "3";
        public const string INGRESO_ASSET = "4";
        public const string EN_TRANSITO = "5";
        public const string DEPOSITO = "6";
        public const string ASIGNADO = "7";
        public const string REPARACION = "8";
        public const string REPARACION_I = "9";
        public const string REPARACION_E = "10";
        public const string DONACION = "11";
        public const string VENTA_INTERNA = "12";
        public const string SCRAP = "13";
        public const string ASIGNADA_SIN_USUARIO = "14";
        public const string ASIGNADO_TECNICO = "15";
        public const string A_CONF_ASING_TEC = "16";
        public const string A_CONF_ASING_USR = "17";
        public const string A_CONF_ASING_PAN = "18";
        public const string EN_PRESTAMO = "19";
        public const string A_CONF_ASING_PRESTAMO = "20";
    }

    public class IO
    {
        public void  exportar(List<EquipoExt> lst,string archivo)
        {
            //List<EquipoExt> lst = new List<EquipoExt>();
            try
            {
                //lst = Global.TodosLosAsset.GetListaEquipos(xs);
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

                dt = converter.ToDataTable(lst);
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
        public void importar()
        {

        }
    }
    public class RetCode
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public object id_parent { get; set; }
    }
    public class RetCodeExt : RetCode
    {
        public string inv { get; set; }

    }

    public static class AMBIENTES
    {
        public const string LAB = "http://mglab010.metrogas.com.ar/pgil/vista/ajax";
        public const string TEST = "http://mgiap048.metrogas.com.ar/pgil/vista/ajax";
        public const string PROD = "https://assets.metrogas.com.ar/vista/ajax";
    }
    public class WebService
    {
        public static T FetchData<T>(string url) where T : class
        {
            try
            {
                // Crear la solicitud web
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "GET";
                oRequest.ContentType = "application/json, text/plain, */*";

                // Obtener la respuesta
                using (WebResponse oResponse = oRequest.GetResponse())
                {
                    Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);

                    // Leer el contenido de la respuesta
                    using (var oSR = new StreamReader(oResponse.GetResponseStream()))
                    {
                        var jsonResponse = oSR.ReadToEnd();

                        // Deserializar la respuesta JSON en la clase especificada
                        return JsonConvert.DeserializeObject<T>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // Manejo básico de errores
            }
        }

        public static T PostData<T>(string url, object postData) where T : class
        {
            try
            {
                // Crear la solicitud web
                WebRequest oRequest = WebRequest.Create(url);
                oRequest.Method = "POST";
                oRequest.ContentType = "application/json";

                // Serializar los datos a enviar como JSON
                string jsonPayload = JsonConvert.SerializeObject(postData);

                // Escribir los datos en el flujo de solicitud
                using (var requestStream = oRequest.GetRequestStream())
                {
                    using (var streamWriter = new StreamWriter(requestStream))
                    {
                        streamWriter.Write(jsonPayload);
                        streamWriter.Flush();
                    }
                }

                // Obtener la respuesta
                using (WebResponse oResponse = oRequest.GetResponse())
                {
                    Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);

                    // Leer el contenido de la respuesta
                    using (var oSR = new StreamReader(oResponse.GetResponseStream()))
                    {
                        var jsonResponse = oSR.ReadToEnd();

                        // Deserializar la respuesta JSON en la clase especificada
                        return JsonConvert.DeserializeObject<T>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // Manejo básico de errores
            }
        }
    }

}
