using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace InventarioAsset
{

    public class jOOCC
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public OOCC[] coleccion { get; set; }

        private string _url = "";
        static jOOCC lvar = new jOOCC();

        public jOOCC()
        {
            _url = Global.urlBase + "ajaxEquipos.php?q=reoc";
        }

        public jOOCC JSONget(string id)
        {
            WebRequest oRequest = WebRequest.Create(_url + "&id=" + id);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<jOOCC>(js);

            }
            return lvar;
        }

        public OOCC ooccget(string id)
        {
            WebRequest oRequest = WebRequest.Create(Global.urlBase + "ajaxEquipos.php?q=oc&id=" + id);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<jOOCC>(js);

            }
            return lvar.coleccion[0];
        }
        public OOCC ooccByID(string id_oocc)
        {
            List<OOCC> oc = new List<OOCC>();
            oc = lvar.coleccion.Where(m => m.id_oocc == id_oocc).ToList();
            return oc[0];

        }
    }

    public class OOCC
    {

        public string ID { get; set; }
        public string OC { get; set; }
        public string APEM { get; set; }
        public string SOLP { get; set; }
        public string FECHA { get; set; }
        public string ARCHIVO { get; set; }
        public string TIPO_INGRESO { get; set; }
        public string USER_ID { get; set; }
        public string STAMP_USER { get; set; }
        public string STAMP_DATE { get; set; }
        public string CNT { get; set; }
        public string METODO_PAGO { get; set; }
        public string FACTURA { get; set; }
        public string id_oocc { get; set; }
        public string remito { get; set; }
        public string proveedor { get; set; }
    }
    public class DatoCompra
    {
        public string REMITO { get; set; }
        public string PROVEEDOR { get; set; }
        public string OC { get; set; }
        public string APEM { get; set; }
        public string SOLP { get; set; }
        public string FECHA { get; set; }

        public DatoCompra getData(string ID_remito)
        {
            // DatoCompra dc = new DatoCompra();
            if (ID_remito == null)
                return null;
            jDataCompra jdc = new jDataCompra();
            jdc = jdc.jget(ID_remito);
            if (jdc.coleccion.Count() == 0)
                return null;
            return jdc.coleccion[0];

        }
    }
    public class jDataCompra
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public DatoCompra[] coleccion { get; set; }

        public jDataCompra jget(string id_remito)
        {
            jDataCompra var = new jDataCompra();
            WebRequest oRequest = WebRequest.Create(Global.urlBase + "ajaxEquipos.php?q=datoscompra&id=" + id_remito);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                var = JsonConvert.DeserializeObject<jDataCompra>(js);

            }
            if (var.coleccion == null)
            {
                return null;
            }
            return var;
        }
    }

    public class toringa
    {
        public string OC { get; set; }
        public string ID_ASSET { get; set; }
        public string DESCRIPCION { get; set; }
        public string MODELO { get; set; }
        public string MARCA { get; set; }
        public string SERIE { get; set; }
    }
    public class jocinv
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public toringa[] coleccion { get; set; }

        public jocinv jget(string oc)
        {
            jocinv var = new jocinv();
            WebRequest oRequest = WebRequest.Create(Global.urlBase + "ajaxEquipos.php?q=oocc&oc=" + oc);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                var = JsonConvert.DeserializeObject<jocinv>(js);

            }
            if (var.coleccion == null)
            {
                return null;
            }
            return var;
        }
    }


}