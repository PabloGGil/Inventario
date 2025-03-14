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

    public class RootSegMenues
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public SegMenues[] coleccion { get; set; }
    }

    public class SegMenues
    {
        public string ID_OPCION { get; set; }
        public string OPCION { get; set; }
        public string DESCRIPCION { get; set; }
        public string PADRE { get; set; }
        public string ORDEN { get; set; }
        public string VISIBLE { get; set; }
        public string PERFILES { get; set; }
    }

    public class RootGyU
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public GyU[] coleccion { get; set; }
        string _url = "";
        private static RootGyU lvar = new RootGyU();

        public RootGyU JSONget()
        {

            //RootGyU lvar = new RootGyU();
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<RootGyU>(js);

            }
            return lvar;
        }

        public RootGyU()
        {
            _url = Global.urlBase + "/ajaxEquipos.php?q=uygs";
        }

        public GyU UsuariotoID(string usuario)
        {
            RootGyU rgyu = new RootGyU();
            rgyu = rgyu.JSONget();
            List<GyU> aux = new List<GyU>();

            aux = rgyu.coleccion.Where(m => m.USER_ID.ToUpper() == usuario.ToUpper()).ToList();
            if (aux.Count == 0)
                return null;
            else
                return aux[0];
        }
        public GyU IDtoUsuario(string ID)
        {
            RootGyU rgyu = new RootGyU();
            rgyu = rgyu.JSONget();
            GyU gx = new GyU();
            List<GyU> aux = new List<GyU>();
            aux = rgyu.coleccion.Where(m => m.ID == ID).ToList();
            if (aux.Count == 0)
            {
                gx.FULLNAME = "";
                return gx;
            }
            else
                return aux[0];
        }
    }

    public class GyU
    {
        public string ID { get; set; }
        public string USER_ID { get; set; }
        public string FULLNAME { get; set; }
        public string DESCRIPCION { get; set; }
        public string STATUS { get; set; }
        public string PERFILES { get; set; }
        public string MAIL { get; set; }

        public string GRUPO { get; set; }


        public string user_id { get; set; }
    }


    public class RootUyG
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public Uygs[] Coleccion { get; set; }
    }

    public class Uygs
    {
        public string ID { get; set; }
        public string USER_ID { get; set; }
        public string FULLNAME { get; set; }
        public string DESCRIPCION { get; set; }
        public string STATUS { get; set; }
        public string PERFILES { get; set; }
        public string MAIL { get; set; }
    }


}
