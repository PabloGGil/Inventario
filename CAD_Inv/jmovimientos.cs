using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
//using Generico;
using System.Linq;


namespace InventarioAsset
{
    public class jconfirma
    {
        public string _url { get; set; }


        public jconfirma()
        {

            _url = Global.urlBase + "/ajaxEquipos.php?";

        }
        public Rootobject GetJTodos()
        {
            WebRequest oRequest = WebRequest.Create(_url + "q=tconfirm");
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            Rootobject lvar = new Rootobject();
            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<Rootobject>(js);

            }
            return lvar;
        }
        public string GetTokenxInv(string inv)
        {
            try
            {
                List<Transconf> lt = new List<Transconf>();
                jconfirma jc = new jconfirma();
                lt = jc.GetJTodos().coleccion.transconf.ToList();
                Transconf tc = lt.Single(o => o.ID_ASSET == inv);
                return tc.TOKEN;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error");
                InventarioAsset.ELog.save(this, ex);
                return null;
            }
        }
        public RetCode JSONPost(Tokens tk)
        {
            RetCode rccode = new RetCode();
            //Creacion  del objeto
            WebRequest oRequest = WebRequest.Create(_url);
            // Setear algunas propiedades de webrequest
            oRequest.Method = "post";
            oRequest.ContentType = "application/json;charset=UTF-8";
            // obtener transmision que contiene los datos de la solicitud
            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json;

                json = JsonConvert.SerializeObject(tk);
                // Escribir los datos en el datastream
                oSW.Write(json);
                oSW.Flush();
                oSW.Close();

            }
            // Enviar la solicitud y crear el objeto con la repuesta
            WebResponse oResponse = oRequest.GetResponse();
            //WebResponse oResponse = oRequest.GetResponse();

            // obtiene los datos de la respuesta
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
                string js = oSR.ReadToEnd();
                rccode = JsonConvert.DeserializeObject<RetCode>(js);
                oResponse.Close();
                return rccode;
            }

        }
    }

    //--------------------------------------------------------------------
    // STATUS
    //http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=s
    //
    //--------------------------------------------------------------------
    public class JsonStatuses
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public Status[] coleccion { get; set; }

        private JsonStatuses lvar = null;
        private string _url { get; set; }
        public JsonStatuses()
        {
            _url = Global.urlBase + "ajaxEquipos.php?q=s";
           // GetJ();
        }

        public string Stat2ID(string statusDesc)
        {
            string ret = "" ;
            this.GetJ();
            foreach (Status st in lvar.coleccion)
            {
                if(st.DESCRIPCION==statusDesc)
                {
                    ret = st.ID;
                    break;
                }
            }
            return ret;
        }
        public string ID2Stat(string statusID)
        {
            string ret="";
            this.GetJ();
            foreach (Status st in lvar.coleccion)
            {
                if (st.ID == statusID)
                {
                    ret = st.ID;
                    break;
                }
            }
            return ret;
        }
        public JsonStatuses GetJ()
        {
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";


            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<JsonStatuses>(js);

            }
            return lvar;
        }
    }

    public class Status
    {
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string NEMOTECNICO { get; set; }
    }

    //public class Rootobject
    //{
    //    public string rc { get; set; }
    //    public string msg { get; set; }
    //    public Coleccion[] coleccion { get; set; }
    //}

    //public class Status
    //{
    //    public string ID { get; set; }
    //    public string DESCRIPCION { get; set; }
    //    public string NEMOTECNICO { get; set; }
    //}

    
    
    public class jMovimientos
    {
        public string _url { get; set; }
        public Ninventario[] idAssets { get; set; }
        public string ID_PUESTO { get; set; }
        public string idUsuarioDestino { get; set; }
        public string descripcion { get; set; }
        public string OS { get; set; }
        public string statusOrig { get; set; }
        public string statusDest { get; set; }
        public string q { get; set; }
        public string idAdminUser { get; set; }
        public string FechaHasta { get; set; }
        public string Formulario { get; set; }

        public class JSONMovimientos
        {
            public string rc { get; set; }
            public string msg { get; set; }
            public DataMov[] coleccion { get; set; }
        }
        public jMovimientos()
        {
            _url = Global.urlBase + "/ajaxEquipos.php?";
        }

        public List<DataMov> JSONGet(string Inv)
        {
            List<DataMov> jmovL = new List<DataMov>();
            WebRequest oRequest = WebRequest.Create(_url + "q=assetstatus&d=" + Inv);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, */*";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                JSONMovimientos lvar = JsonConvert.DeserializeObject<JSONMovimientos>(js);
                jmovL = lvar.coleccion.ToList();
            }

            return jmovL;
        }

        public RetCode JSONPost(jMovimientos jm)
        {
            RetCode rc = new RetCode();
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json, text/plain, *";
            
            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json;
                json = JsonConvert.SerializeObject(jm);
                // Escribir los datos en el datastream
                oSW.Write(json);
                oSW.Flush();
                oSW.Close();

            }
            // Enviar la solicitud y crear el objeto con la repuesta
            WebResponse oResponse = oRequest.GetResponse();
            //WebResponse oResponse = oRequest.GetResponse();

            // obtiene los datos de la respuesta
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
                string js = oSR.ReadToEnd();
                rc = JsonConvert.DeserializeObject<RetCode>(js);
                oResponse.Close();
                //return js;
                return rc;
            }

        }
    }

    public class Ninventario
    {
        public string id { get; set; }
    }

    public class EnvioConf
    {
        public string _url { get; set; }
        public string q { get; set; }
        public string idAdminUser { get; set; }
        public string token { get; set; }
        public int opcion { get; set; }

        public EnvioConf()
        {
            _url = Global.urlBase + "/ajaxEquipos.php?";
        }
        public RetCode JSONPost(EnvioConf m)
        {
            RetCode rccode = new RetCode();
            //Creacion  del objeto
            WebRequest oRequest = WebRequest.Create(_url);
            // Setear algunas propiedades de webrequest
            oRequest.Method = "post";
            oRequest.ContentType = "application/json;charset=UTF-8";
            // obtener transmision que contiene los datos de la solicitud
            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json;

                json = JsonConvert.SerializeObject(m);
                // Escribir los datos en el datastream
                oSW.Write(json);
                oSW.Flush();
                oSW.Close();

            }
            // Enviar la solicitud y crear el objeto con la repuesta
            WebResponse oResponse = oRequest.GetResponse();
            //WebResponse oResponse = oRequest.GetResponse();

            // obtiene los datos de la respuesta
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
                string js = oSR.ReadToEnd();
                rccode = JsonConvert.DeserializeObject<RetCode>(js);
                oResponse.Close();
                return rccode;
            }
        }
    }

    public class DataMov
    {
        public string FECHA { get; set; }
        public string USER_id { get; set; }
        public string ID_ASSET { get; set; }
        public string STATUS { get; set; }
        public string DETALLE { get; set; }
    }

    public class Rootobject
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public Coleccion coleccion { get; set; }
    }

    public class Coleccion
    {
        public Transconf[] transconf { get; set; }
        public User[] users { get; set; }
        public Sys_Users[] sys_users { get; set; }
        public Asset[] assets { get; set; }
    }

    public class Transconf
    {
        public string ID_ASSET { get; set; }
        public string ID_USUARIO { get; set; }
        public string TOKEN { get; set; }
        public string FECHA { get; set; }
        public string SYS_USER { get; set; }
        public string PUESTO { get; set; }
    }

    public class User
    {
        public string ID { get; set; }
        public string USUARIO_ID { get; set; }
        public string MAIL { get; set; }
        public string FULLNAME { get; set; }
        public string TEL { get; set; }
        public string LEGAJO { get; set; }
        public string CC { get; set; }
        public string CC_DESC { get; set; }
        public string DESCRIPCION { get; set; }
        public string LASTLOGON { get; set; }
        public string LOCKED { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DATE { get; set; }
        public string STAMP_USER { get; set; }
        public string STAMP_DATE { get; set; }
        public string lamparon { get; set; }
    }

    public class Sys_Users
    {
        public string ID { get; set; }
        public string USER_ID { get; set; }
        public string MAIL { get; set; }
        public string FULLNAME { get; set; }
        public string DESCRIPCION { get; set; }
        public string STAMP_DATE { get; set; }
        public string STAMP_USER { get; set; }
        public string STATUS { get; set; }
    }

    public class Asset
    {
        public string ID_ASSET { get; set; }
        public string TIPO_ASSET { get; set; }
        public string ID_DATO { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string MARCA { get; set; }
        public string SYS_USER { get; set; }
        public string STAMP_DATE { get; set; }
        public string ID_REMITO { get; set; }
        public string STATUS_DET { get; set; }
    }

    public class JDescripciones
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public JDescripcion[] coleccion { get; set; }
        public string _url;

        #region Metodos

        public JDescripciones()
        {
            _url = Global.urlBase + "ajaxEquipos.php?&q=t";


        }
        public string DescripcionxID(string ID)
        {
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            JDescripciones lvar = new JDescripciones();
            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<JDescripciones>(js);
            }
            JDescripcion auxd = new JDescripcion();
            auxd = lvar.coleccion.Single(o => o.ID == ID);
            return auxd.DESCRIPCION;
        }
        #endregion
    }

    public class JDescripcion
    {
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string OBS { get; set; }
    }
    public class Tokens
    {
        public string _token { get; set; }
        public string _inv { get; set; }

        public string _sys_usr { get; set; }


    }


}
