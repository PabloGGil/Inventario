using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
//using System.Collections.Generic;
using System.Linq;

namespace InventarioAsset
{
    public class JSONUsr
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public Usuario[] coleccion { get; set; }


        private static JSONUsr lvar = new JSONUsr();// null;//= new JSONUsr(); //= new JSONUsr();
        private string url { get; set; }
        public JSONUsr()
        {
            url = Global.urlBase + "ajaxEquipos.php?q=user_avalables";
            //url = ur;
            //lvar = new JSONUsr();
            // lvar=JSONget();
        }
        public Usuario getDataxUsr(string Usuario)
        {
            string USUARIO = Usuario.ToUpper();
            string usuario = Usuario.ToLower();
            lvar = JSONget();
            List<Usuario> Xusers = new List<Usuario>();
            Xusers = lvar.coleccion.Where(m => ((m.USUARIO_ID == usuario) || (m.USUARIO_ID == USUARIO))).ToList();
            //List<Usuario> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
            Usuario aux = new Usuario();
            aux.ID = "-1";
            if (Xusers.Count() == 0)
            {
                return aux;
            }
            else
            {
                return Xusers[0];
            }

        }
        public Usuario getDataxID(string id)
        {
            List<Usuario> Xusers = new List<Usuario>();
            Xusers = lvar.coleccion.Where(m => m.ID == id).ToList();
            return Xusers[0];
        }
        public JSONUsr JSONget()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<JSONUsr>(js);

            }
            return lvar;
        }



    }
    public class JSONuserSec
    {
        public string url { get; set; }
        public string rc { get; set; }
        public Login login { get; set; }
        // datos del usuario logueado
        public A_usuario usuario { get; set; }
        // Menues a los que tiene permiso
        public Menu[] menu { get; set; }
        // Cambio de estados permitidos
        public Status_Change[] status_change { get; set; }
        // Tipos de equipos que puede administrar el usuario logueado
        public Tipo_Sec[] tipo_sec { get; set; }



    }
    public class Menu
    {
        //public string ID_OPCION { get; set; }
        public string OPCION { get; set; }
        public string DESCRIPCION { get; set; }
        //public string PADRE { get; set; }
        //public string ORDEN { get; set; }
        //public string ACCION { get; set; }
        //public string IMAGE { get; set; }
        //public string VISIBLE { get; set; }
    }

    public class Status_Change
    {
        public string NEMOTECNICO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ENTIDAD { get; set; }
        public string STATUS_ORIG { get; set; }
        public string STATUS_DEST { get; set; }
        public string ORDEN { get; set; }
        public string REQ_REMITO { get; set; }
        public string REQ_OS { get; set; }
        public string REQ_FECHA_HASTA { get; set; }
        public string REMITO_TITULO { get; set; }
        public string REMITO_OBSERVACION { get; set; }
        public string ASIGNAR_USR { get; set; }
        public string ASIGNAR_GRUPO { get; set; }
        public string CHG_PUESTO { get; set; }
    }

    public class Usuario
    {
        public string ID { get; set; }
        public string USUARIO_ID { get; set; }
        public string MAIL { get; set; }
        public string FULLNAME { get; set; }
        public string TEL { get; set; }
        public string CC { get; set; }
        public string DESCRIPCION { get; set; }
        public string LASTLOGON { get; set; }
        public string LOCKED { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DATE { get; set; }
        public string STAMP_USER { get; set; }
        public string STAMP_DATE { get; set; }
        public string LEGAJO { get; set; }
        public string CC_DESC { get; set; }
    }
    public class User_Sec
    {
        // private string url;
        public string url { get; set; }
        string rc { get; set; }
        public static JSONuserSec estados = new JSONuserSec();
        //Constructor
        public User_Sec(string ur)
        {
            url = ur;
        }
        public JSONuserSec JSONpost(DataLogin m)
        {

            //Creacion  del objeto
            WebRequest oRequest = WebRequest.Create(url);
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
                estados = JsonConvert.DeserializeObject<JSONuserSec>(js);
                oResponse.Close();
                return estados;
            }

        }
        public List<string> getSegxEquipo()
        {
            List<Tipo_Sec> AssCom = new List<Tipo_Sec>();
            List<string> tp = new List<string>();
            AssCom = estados.tipo_sec.ToList();


            tp = AssCom.ConvertAll(new Converter<Tipo_Sec, string>(TipoSec2Tipo));


            return tp;
        }
        public static string TipoSec2Tipo(Tipo_Sec pf)
        {

            string x;
            x = pf.TIPO;

            //new Point(((int)pf.X), ((int)pf.Y));
            return x;
        }


        public bool LoginOk()
        {
            Login laux = new Login();
            if (estados.login.response.ToUpper() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public class Tipo_Sec
    {
        public string ID { get; set; }
        public string TIPO { get; set; }

    }
    public class Login
    {
        public string response { get; set; }
    }

    public class A_usuario
    {
        public string ID { get; set; }
        public string USER_ID { get; set; }
        public string FULLNAME { get; set; }
        public string DESCRIPCION { get; set; }
        public string MAIL { get; set; }
        public string PERFILES { get; set; }
        public string LEGAJO { get; set; }
        public string SGID { get; set; }

        //public string AppUsr(string IDappUsr)
        //{

        //}
    }
    public class DataLogin
    {
        public string user;
        public string password;

    }

    public class JUsuariosSys
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public UsuariosSys[] coleccion { get; set; }
        string _url = "";
        
        static JUsuariosSys lvar=new JUsuariosSys() ;
        public JUsuariosSys()
        {
            _url = Global.urlBase + "ajaxEquipos.php?q=uygs";
           
        }
        public JUsuariosSys JSONget()
        {
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<JUsuariosSys>(js);

            }
            return lvar;
        }
        public UsuariosSys getDataxUsr(string Usuario)
        {
            string USUARIO = Usuario.ToUpper();
            string usuario = Usuario.ToLower();
            List<UsuariosSys> sysusers = new List<UsuariosSys>();
            sysusers = lvar.coleccion.Where(m => ((m.USER_ID == usuario) || (m.USER_ID == USUARIO))).ToList();
            UsuariosSys aux = new UsuariosSys();
            aux.ID = "-1";
            if (sysusers.Count() == 0)
            {
                return aux;
            }
            else
            {
                return sysusers[0];
            }

        }
        public UsuariosSys getDataxID(string id)
        {
            List<UsuariosSys> Xusers = new List<UsuariosSys>();
            Xusers = lvar.coleccion.Where(m => m.ID == id).ToList();
            return Xusers[0];
        }
    }
    public class UsuariosSys
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
