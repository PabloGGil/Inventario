using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
//using Generico;


namespace InventarioAsset
{
    public class Jpuestos : Jpuesto
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public string idadminuser { get; set; }
       // public string q { get; set; }
        public List<Jpuesto> coleccion { get; set; }
        public Jpuesto info = new Jpuesto(); 

    }
    public class rut
    {
        public string idAdminUser { get; set; }
        public string q { get; set; }
        public Jpuesto info = new Jpuesto();
    }
    public class Jpuesto
    {
        //Tener en cuenta que en el get se recibe ID_LUGAR y en el post va idlugar
        string _url;
        public string ID { get; set; }
        public string idlugar { get; set; }
        public string id_lugar{get;set;}
        public string responsable { get; set; }
        public string descripcion { get; set; }
        public string comentario { get; set; }
        public string fechacreacion { get; set; }
        public string q { get; set; }
        public string admin { get; set; }
        //public string idAdminUser { get; set; }
        public string activo { get; set; }

        #region METODOS
        /*
        *  --- Devuelve los puestos activos ----
        *  http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=ListaLugares&d=now 
        *  --- Devuelve la historia del puesto indicado en el parámetro lu ----
           http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=ListaLugares&d=hi&lu=MAG-C-023
           --- Devuelve todos los puestos ( activos e inactivos) ----
           http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=ListaLugares&d=ALL
       */

        public void MostrarJpuesto(Jpuesto x)
        {
            Console.WriteLine("ID             : {0}", x.ID);
            Console.WriteLine("ID_Lugar       : {0}", x.idlugar);
            Console.WriteLine("Responsable    : {0}", x.responsable);
            Console.WriteLine("Fecha Creacion : {0}", x.fechacreacion);
            Console.WriteLine("Comentario     : {0}", x.comentario);
            Console.WriteLine("Descripcion    : {0}", x.descripcion);
            Console.WriteLine("Admin          : {0}", x.admin);
            Console.WriteLine("Activo         : {0}", x.activo);
        }
    public Jpuesto()
        {

            _url = Global.urlBase + "/ajaxEquipos.php";
            //http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php";

        }
        public Jpuestos JSONget(string c)
        {

            _url = _url + c;

            Jpuestos aux = new Jpuestos();
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                aux = JsonConvert.DeserializeObject<Jpuestos>(js);

            }
            return aux;
        }

     


        //public RetCode JSONPost(rut m)
        public RetCode JSONPost(Jpuesto m)
        {
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json, text/plain, *";
            rut xr = new rut();
            xr.info = m;
            xr.q = m.q;
            xr.idAdminUser = Global.SeguridadUsr.usuario.ID; 
            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json;
                //json = JsonConvert.SerializeObject(m);
                json = JsonConvert.SerializeObject(xr);
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
                RetCode estados = JsonConvert.DeserializeObject<RetCode>(js);
                oResponse.Close();
                //return js;
                return estados;
            }
        }
        #endregion
    }
    public class msgroot
    {
        public int rc { get; set; }
        public Msg msg { get; set; }
        public string idadminuser { get; set; }
        public string q { get; set; }
        public object id_parent { get; set; }
        public void mostrar(msgroot m)
        {
            Console.WriteLine("\nRC:{0}\nMensaje: {1}\n", m.rc,m.msg.ID);
        }
    }

    public class Msg
    {
        public string ID { get; set; }
    }

}
