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
    public class jRemito
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public Remito[] coleccion { get; set; }
            
        private string _url = "";
        static jRemito lvar = new jRemito();

        public jRemito()
        {
            _url = Global.urlBase + "ajaxEquipos.php?q=redeo";
            //_url = "http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=reoc";
        }

        public jRemito JSONget(string idremito)
        {
            WebRequest oRequest = WebRequest.Create(_url);// + "&id=" + idremito);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<jRemito>(js);

            }
            return lvar;
        }
        public Remito remitobyID(string id_remito)
        {
            this.JSONget("");
            Remito rx = new Remito();
            List<Remito> ma = lvar.coleccion.Where(m => m.ID == id_remito).ToList();

            return ma[0];
        }
    }

    public class Remito
    {
        public string ID { get; set; }
        public string ID_remito { get; set;}
        public string ID_OOCC { get; set; }
        public string REMITO { get; set; }
        public string PROVEEDOR { get; set; }
        public string FECHA { get; set; }
        public string ARCHIVO { get; set; }
        public string STAMP_USER { get; set; }
        public string STAMP_DATE { get; set; }

       
    } 
}
