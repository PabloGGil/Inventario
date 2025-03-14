using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

//using CAD_Inv;

using System.Web;

namespace InventarioAsset
{

    //public class DataLogin
    //{
    //    public string user;
    //    public string password;

    //}

    //public class Login
    //{
    //    public string response { get; set; }
    //}

    //public class A_usuario
    //{
    //    public string ID { get; set; }
    //    public string USER_ID { get; set; }
    //    public string FULLNAME { get; set; }
    //    public string DESCRIPCION { get; set; }
    //    public string MAIL { get; set; }
    //    public string PERFILES { get; set; }
    //    public string LEGAJO { get; set; }
    //    public string SGID { get; set; }
    //}

    //public class JSONuserSec
    //{
    //    public string url { get; set; }
    //    public string rc { get; set; }
    //    public Login login { get; set; }
    //    // datos del usuario logueado
    //    public A_usuario usuario { get; set; }
    //    // Menues a los que tiene permiso
    //    public Menu[] menu { get; set; }
    //    // Cambio de estados permitidos
    //    public Status_Change[] status_change { get; set; }
    //    // Tipos de equipos que puede administrar el usuario logueado
    //    public Tipo_Sec[] tipo_sec { get; set; }
    //}
    //public class User_Sec
    //{
    //    // private string url;
    //    public string url { get; set; }

    //    public static JSONuserSec estados = new JSONuserSec();
    //    //Constructor
    //    public User_Sec(string ur)
    //    {
    //        url = ur;
    //    }
    //    public JSONuserSec JSONpost(DataLogin m)
    //    {

    //        //Creacion  del objeto
    //        WebRequest oRequest = WebRequest.Create(url);
    //        // Setear algunas propiedades de webrequest
    //        oRequest.Method = "post";
    //        oRequest.ContentType = "application/json;charset=UTF-8";
    //        // obtener transmision que contiene los datos de la solicitud
    //        using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
    //        {
    //            string json;

    //            json = JsonConvert.SerializeObject(m);
    //            // Escribir los datos en el datastream
    //            oSW.Write(json);
    //            oSW.Flush();
    //            oSW.Close();

    //        }
    //        // Enviar la solicitud y crear el objeto con la repuesta
    //        WebResponse oResponse = oRequest.GetResponse();
    //        //WebResponse oResponse = oRequest.GetResponse();

    //        // obtiene los datos de la respuesta
    //        using (var oSR = new StreamReader(oResponse.GetResponseStream()))
    //        {
    //            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
    //            string js = oSR.ReadToEnd();
    //            estados = JsonConvert.DeserializeObject<JSONuserSec>(js);
    //            oResponse.Close();
    //            return estados;
    //        }

    //    }
    //    public List<string> getSegxEquipo()
    //    {
    //        List<Tipo_Sec> AssCom = new List<Tipo_Sec>();
    //        List<string> tp = new List<string>();
    //        AssCom = estados.tipo_sec.ToList();


    //        tp = AssCom.ConvertAll(new Converter<Tipo_Sec, string>(TipoSec2Tipo));


    //        return tp;
    //    }
    //    public static string TipoSec2Tipo(Tipo_Sec pf)
    //    {

    //        string x;
    //        x = pf.TIPO;

    //        //new Point(((int)pf.X), ((int)pf.Y));
    //        return x;
    //    }


    //    public bool LoginOk()
    //    {
    //        Login laux = new Login();
    //        if (estados.login.response.ToUpper() == "OK")
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //    }
    //}
    //public class Menu
    //{
    //    public string ID_OPCION { get; set; }
    //    public string OPCION { get; set; }
    //    public string DESCRIPCION { get; set; }
    //    public string PADRE { get; set; }
    //    public string ORDEN { get; set; }
    //    public string ACCION { get; set; }
    //    public string IMAGE { get; set; }
    //    public string VISIBLE { get; set; }
    //}

    //public class Status_Change
    //{
    //    public string NEMOTECNICO { get; set; }
    //    public string DESCRIPCION { get; set; }
    //    public string ENTIDAD { get; set; }
    //    public string STATUS_ORIG { get; set; }
    //    public string STATUS_DEST { get; set; }
    //    public string ORDEN { get; set; }
    //    public string REQ_REMITO { get; set; }
    //    public string REQ_OS { get; set; }
    //    public string REQ_FECHA_HASTA { get; set; }
    //    public string REMITO_TITULO { get; set; }
    //    public string REMITO_OBSERVACION { get; set; }
    //    public string ASIGNAR_USR { get; set; }
    //    public string ASIGNAR_GRUPO { get; set; }
    //    public string CHG_PUESTO { get; set; }
    //}

    //public class Tipo_Sec
    //{
    //    public string ID { get; set; }
    //    public string TIPO { get; set; }
    //}



    public class TipoAsset
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public ArrTipoAsset[] coleccion { get; set; }

        public static TipoAsset GetJ(string url)
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, */*";
            TipoAsset estados;
            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                estados = JsonConvert.DeserializeObject<TipoAsset>(js);

            }
            return estados;
        }


    }
    public class ArrTipoAsset
    {
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string OBS { get; set; }
    }




    public class Marcas
    {


        public string rc { get; set; }
        public string msg { get; set; }
        public Marca[] coleccion { get; set; }
        private string url { get; set; }
        private static Marcas lvar = null;
        public Marcas(string ur)
        {
            url = ur;
        }
        public Marcas GetJ()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, */*";


            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<Marcas>(js);

            }
            return lvar;
        }
        public int Cuenta()
        {

            return lvar.coleccion.GetLength(0);
        }
    }

    public class Marca
    {
        public string MARCA { get; set; }
    }



    public class UA
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public Usuarios_Assets coleccion { get; set; }
        private string url { get; set; }
        private UA lvar = null;
        public UA(string ur)
        {
            url = ur;
        }
        public UA GetJ()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, */*";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<UA>(js);

            }
            return lvar;
        }
        public int Cuenta()
        {

            return lvar.coleccion.col.GetLength(0);
        }
    }

    public class Usuarios_Assets
    {
        public Usuario[] col { get; set; }
        public object[] puesto { get; set; }
    }

  


    //public class JSONNotas
    //{
    //    public string rc { get; set; }
    //    public string msg { get; set; }
    //    public JSONNota[] coleccion { get; set; }
    //}

    //public class Notas
    //{

    //    private string url { get; set; }
    //    private JSONNotas lvar = null;
    //    public Notas(string ur)
    //    {
    //        url = ur;
    //    }
    //    public JSONNotas JSONget()
    //    {
    //        WebRequest oRequest = WebRequest.Create(url);
    //        oRequest.Method = "get";
    //        oRequest.ContentType = "application/json, text/plain, */*";

    //        WebResponse oResponse = oRequest.GetResponse();

    //        Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
    //        using (var oSR = new StreamReader(oResponse.GetResponseStream()))
    //        {
    //            var js = oSR.ReadToEnd();
    //            lvar = JsonConvert.DeserializeObject<JSONNotas>(js);

    //        }
    //        return lvar;
    //    }
    //    public int Cuenta()
    //    {

    //        return lvar.coleccion.GetLength(0);
    //    }
    //}

    //public class JSONNota
    //{
    //    public string ID { get; set; }
    //    public string ID_ASSET { get; set; }
    //    public string NOTA { get; set; }
    //    public string STAMP_USER { get; set; }
    //    public string STAMP_DATE { get; set; }
    //    public string USER_ID { get; set; }
    //    public string FULLNAME { get; set; }
    //    public string ARCHIVO { get; set; }
    //}

    public class JSONMovimientos
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public JSONMovimiento[] coleccion { get; set; }
    }
    public class JSONMovimiento
    {
        public string FECHA { get; set; }
        public string USER_ID { get; set; }
        public string ID_ASSET { get; set; }
        public string STATUS { get; set; }
        public string DETALLE { get; set; }
    }
    //public class baja
    //{
    //    public string FECHA { get; set; }
    //    public string USER_ID { get; set; }
    //    public string ID_ASSET { get; set; }
    //    public string STATUS { get; set; }
    //    public string DETALLE { get; set; }
    //}
    //public class Movimientos
    //{


    //    private string url { get; set; }
    //    private JSONMovimientos lvar = null;
    //    public Movimientos(string ur)
    //    {
    //        url = ur;
    //    }
    //    public JSONMovimientos JSONget()
    //    {
    //        WebRequest oRequest = WebRequest.Create(url);
    //        oRequest.Method = "get";
    //        oRequest.ContentType = "application/json, text/plain, */*";

    //        WebResponse oResponse = oRequest.GetResponse();

    //        Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
    //        using (var oSR = new StreamReader(oResponse.GetResponseStream()))
    //        {
    //            var js = oSR.ReadToEnd();
    //            lvar = JsonConvert.DeserializeObject<JSONMovimientos>(js);

    //        }
    //        return lvar;
    //    }
    //    public int Cuenta()
    //    {

    //        return lvar.coleccion.GetLength(0);
    //    }
    //}

    // JSON para Coleccion Asset-Puesto-Usuario
    // ajaxEquipos.php?q=a&id=inv
    public class JSONapus
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public JSONapu[] coleccion { get; set; }
    }


    public class JSONapu
    {
        //nro de inventario del equipo
        public string ID_ASSET { get; set; }
        public string TIPO_ASSET { get; set; }
        // Descripcion del equipo
        public string DESCRIPCION { get; set; }
        //Modelo del equipo
        public string MODELO { get; set; }
        //nro de serie del equipo
        public string SERIE { get; set; }
        //Marca del equipo
        public string MARCA { get; set; }
        public string SYS_USER { get; set; }
        // Fecha de alta del equipo
        public string STAMP_DATE { get; set; }
        public string STATUS { get; set; }
        //fecha del último movimiento
        public string STATUS_DATE { get; set; }
        public string STATUS_USER { get; set; }
        public string STATUS_DET { get; set; }
        // Pueso donde se encuentra el equipo
        public string ID_PUESTO { get; set; }
        //Usuario que tiene asignado el equipo
        public string ASSING_USUARIO_ID { get; set; }
        //CC del usuario asignado
        public string CC { get; set; }
        //Legajo del usuario asignado
        public string ASSING_LEGAJO { get; set; }
        //Estado del equipo
        public string ASSING_STATUS { get; set; }
        public string OBS { get; set; }
        public string OBS_DATE { get; set; }
        public string DETALLE { get; set; }
        public string TECNICO { get; set; }
    }
    public class As_pue_usrs
    {

        private string url { get; set; }
        private static JSONapus lvar;

        public As_pue_usrs(string ur)
        {
            url = ur;
        }
        public JSONapus JSONget()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, */*";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<JSONapus>(js);

            }
            return lvar;
        }
        public int Cuenta()
        {

            return lvar.coleccion.GetLength(0);
        }
        //public Equipo getDataEquipo()
        //{
        //    Equipo aux = new Equipo();
        //    aux.DESCRIPCION = lvar.coleccion[0].DESCRIPCION;
        //    aux.ID_Inv = lvar.coleccion[0].ID_ASSET;
        //    aux.MARCA = lvar.coleccion[0].MARCA;
        //    aux.MODELO = lvar.coleccion[0].MODELO;
        //    aux.SERIE = lvar.coleccion[0].SERIE;
        //    //aux.TIPO_ASSET = lvar.coleccion[0].TIPO_ASSET;
        //    aux.Estado = lvar.coleccion[0].STATUS_DET;
        //    aux.puesto = lvar.coleccion[0].ID_PUESTO;
        //    return aux;
        //}
        public string getPuesto()
        {
            return lvar.coleccion[0].ID_PUESTO;
        }


        public Empleado getEmpleado()
        {

            Empleado auxi = new Empleado();
            auxi.ASSING_USUARIO_ID = lvar.coleccion[0].ASSING_USUARIO_ID;
            auxi.CC = lvar.coleccion[0].CC;
            auxi.ASSING_LEGAJO = lvar.coleccion[0].ASSING_LEGAJO;
            auxi.ASSING_STATUS = lvar.coleccion[0].ASSING_STATUS;

            return auxi;
        }
        //public list<Empleado> getEmpleado()
        //{
        //    List<Empleado> aux = new List<Empleado>();
        //    //Empleado aux;
        //    Empleado auxi = new Empleado();
        //    auxi.ASSING_USUARIO_ID = lvar.coleccion[0].ASSING_USUARIO_ID;
        //    auxi.CC = lvar.coleccion[0].CC;
        //    auxi.ASSING_LEGAJO = lvar.coleccion[0].ASSING_LEGAJO;
        //    auxi.ASSING_STATUS = lvar.coleccion[0].ASSING_STATUS;
        //    aux.Add(auxi);

        //    return aux;
        //}
    }

    public class Empleado
    {

        public string ASSING_USUARIO_ID { get; set; }
        public string CC { get; set; }
        public string ASSING_LEGAJO { get; set; }
        public string ASSING_STATUS { get; set; }


    }
    //public class Equipo
    //{
    //    public string ID_Inv { get; set; }
    //    //public string TIPO_ASSET { get; set; }
    //    public string DESCRIPCION { get; set; }
    //    public string MODELO { get; set; }
    //    public string SERIE { get; set; }
    //    public string MARCA { get; set; }
    //    public string Estado { get; set; }
    //    public string Fecha_Ingreso { get; set; }
    //    public string ID_REMITO { get; set; }

    //    //public string puesto { get; set; }

    //}

    public class EquipoPuesto
    {
        public string ID_ASSET { get; set; }
        //public string TIPO_ASSET { get; set; }
        public string DESCRIPCION { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string MARCA { get; set; }
        public string Estado { get; set; }
        public string Fecha_Ingreso { get; set; }
        public string ID_REMITO { get; set; }
        public string puesto;
    }

    public class As_pue_usr
    {
        //nro de inventario del equipo


        public string ID_ASSET { get; set; }
        public string TIPO_ASSET { get; set; }
        // Descripcion del equipo
        public string DESCRIPCION { get; set; }
        //Modelo del equipo
        public string MODELO { get; set; }
        //nro de serie del equipo
        public string SERIE { get; set; }
        //Marca del equipo
        public string MARCA { get; set; }
        public string SYS_USER { get; set; }
        // Fecha de alta del equipo
        public string STAMP_DATE { get; set; }
        public string STATUS { get; set; }
        //fecha del último movimiento
        public string STATUS_DATE { get; set; }
        public string STATUS_USER { get; set; }
        public string STATUS_DET { get; set; }
        // Pueso donde se encuentra el equipo
        public string ID_PUESTO { get; set; }
        //Usuario que tiene asignado el equipo
        public string ASSING_USUARIO_ID { get; set; }
        //CC del usuario asignado
        public string CC { get; set; }
        //Legajo del usuario asignado
        public string ASSING_LEGAJO { get; set; }
        //Estado del equipo
        public string ASSING_STATUS { get; set; }
        public string OBS { get; set; }
        public string OBS_DATE { get; set; }
        public string DETALLE { get; set; }
        public string TECNICO { get; set; }
    }

    //public class JSONAllAsset
    //{
    //    public string rc { get; set; }
    //    public string msg { get; set; }
    //    public AssetCompleto[] coleccion { get; set; }

    //}
    //// Clase para equipo Extendido, es decir, agrega el puesto
    //public class EquipoExt : Equipo
    //{
    //    public string Usuario { get; set; }
    //    public string Puesto { get; set; }

    //    public string UltNota { get; set; }
    //    public string Status_date { get; set; }

    //    public string detalle { get; set; }
    //}
    //public class AllAssets
    //{


    //    private string url { get; set; }
    //    private static JSONAllAsset lvar;
    //    public int indice { get; set; }
    //    //public As_pue_usrs()
    //    //{
    //    //    lvar = new As_pue_usrs();
    //    //}
    //    public AllAssets(string ur)
    //    {
    //        url = ur;
    //    }
    //    public List<EquipoExt> getInvxCaractEnPan(string marca, string modelo, string tipo)
    //    {
    //        //List<AssetCompleto> mz = new List<AssetCompleto>();
    //        //List<string> ma = new List<string>();
    //        //mz = lvar.coleccion.Where(m => (m.MARCA == marca) && (m.DESCRIPCION == tipo)).ToList();
    //        List<AssetCompleto> ma = lvar.coleccion.Where(m => ((m.ID_PUESTO == "PAN-0-000") && (m.MARCA == marca) && (m.DESCRIPCION == tipo) && (m.MODELO == modelo))).ToList();
    //        // ma = lvar.coleccion.Where(m => (m.ID_ASSET == "32271")).Select(m => m.MODELO).Distinct().ToList();
    //        //objList.Select(o => o.typeId).Distinct().ToList();
    //        //ma.Sort();
    //        List<EquipoExt> mz = ma.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

    //        return mz;
    //    }

    //    public static string Asset2Inv(AssetCompleto x)
    //    {
    //        string inv = x.ID_ASSET;
    //        return inv;
    //    }
    //    public string getLogedUser()
    //    {
    //        return null;
    //    }

    //    public JSONAllAsset JSONget()
    //    {
    //        WebRequest oRequest = WebRequest.Create(url);
    //        oRequest.Method = "get";
    //        oRequest.ContentType = "application/json, text/plain, *";

    //        WebResponse oResponse = oRequest.GetResponse();

    //        Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
    //        using (var oSR = new StreamReader(oResponse.GetResponseStream()))
    //        {
    //            var js = oSR.ReadToEnd();
    //            lvar = JsonConvert.DeserializeObject<JSONAllAsset>(js);

    //        }
    //        return lvar;
    //    }


    //    public List<EquipoExt> GetEquipo(string idequipo)
    //    {

    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        AssCom = lvar.coleccion.Where(m => m.ID_ASSET == idequipo).ToList();
    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
    //        Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + idequipo);
    //        JSONNotas jma = ma.JSONget();
    //        if(jma.coleccion.Count() !=0)
    //            lp[0].UltNota = jma.coleccion[0].NOTA;
    //        return lp;


    //    }
    //    public List<EquipoExt> getEquiposxPuesto(string puesto)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        AssCom = lvar.coleccion.Where(m => m.ID_PUESTO == puesto).ToList();

    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

    //        return lp;
    //    }

    //    public List<EquipoExt> getEquiposxUsuario(string Usuario)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        AssCom = lvar.coleccion.Where(m => (m.ASSING_USUARIO_ID ==Usuario.ToUpper())).ToList();// || (m.ASSING_USUARIO_ID.Contains(Usuario.ToUpper()))).ToList();

    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

    //        return lp;
    //    }
    //    public List<EquipoExt> getEquiposxEstado(string Estado)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        if (Estado == "BAJA")
    //        {
    //            AssCom = lvar.coleccion.Where(m => (m.STATUS_DET == "DONACION")||(m.STATUS_DET == "VENTA INTERNA")||(m.STATUS_DET == "BAJA")).ToList();

    //        }
    //        else
    //        {
                
    //            AssCom = lvar.coleccion.Where(m => m.STATUS_DET == Estado).ToList();

                
    //        }
    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
    //        return lp;
    //    }

    //    public List<EquipoExt> getEqxEstadoxFecha(string Estado,DateTime rangoMin,DateTime rangoMax)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        if (Estado == "BAJA")
    //        {
    //            AssCom = lvar.coleccion.Where(m => (m.STATUS_DET == "DONACION") || (m.STATUS_DET == "VENTA INTERNA") || (m.STATUS_DET == "BAJA")).ToList();
    //            //               AssCom = AssCom.Where(m => Convert.ToDateTime(m.STAMP_DATE) >= Convert.ToDateTime("2018-05-29 14:18")).ToList();
    //            AssCom = AssCom.Where(m => (Convert.ToDateTime(m.STATUS_DATE) >= Convert.ToDateTime(rangoMin.ToString("yyyy-MM-dd"))) && (Convert.ToDateTime(m.STATUS_DATE) <= Convert.ToDateTime(rangoMax.ToString("yyyy-MM-dd")))).ToList();
    //        }
    //        else
    //        {

    //            AssCom = lvar.coleccion.Where(m => m.STATUS_DET == Estado).ToList();
    //            AssCom = AssCom.Where(m => Convert.ToDateTime(m.STAMP_DATE) > rangoMin && Convert.ToDateTime(m.STAMP_DATE) < rangoMax).ToList();


    //        }
    //        //Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + idequipo);
    //        //JSONNotas jma = ma.JSONget();
    //        //if (jma.coleccion.Count() != 0)
    //        //    lp[0].UltNota = jma.coleccion[0].NOTA;
    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
    //        return lp;
    //    }

    //    public List<EquipoExt> getEquiposxMarca(string Marca)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        AssCom = lvar.coleccion.Where(m => m.MARCA == Marca).ToList();

    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

    //        return lp;
    //    }

    //    public List<EquipoExt> getEquiposxModelo(string Modelo)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        AssCom = lvar.coleccion.Where(m => m.MODELO == Modelo).ToList();

    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

    //        return lp;
    //    }
    //    public static EquipoExt AssetToEq(AssetCompleto pf)
    //    {

    //        EquipoExt x = new EquipoExt();
    //        x.ID_Inv = pf.ID_ASSET;
    //        x.ID_REMITO = pf.ID_REMITO;
    //        x.MARCA = pf.MARCA;
    //        x.MODELO = pf.MODELO;
    //        x.SERIE = pf.SERIE;
    //        // x.TIPO_ASSET = pf.TIPO_ASSET;
    //        x.Estado = pf.STATUS_DET;
    //        x.DESCRIPCION = pf.DESCRIPCION;
    //        x.Puesto = pf.ID_PUESTO;
    //        x.Usuario = pf.ASSING_USUARIO_ID;
    //        x.UltNota = "";
    //        x.Status_date = pf.STATUS_DATE;
    //        x.detalle = pf.DETALLE;
    //        //new Point(((int)pf.X), ((int)pf.Y));
    //        return x;
    //    }
    //    public List<EquipoExt> getSerie(string serie)
    //    {
    //        List<AssetCompleto> AssCom = new List<AssetCompleto>();
    //        AssCom = lvar.coleccion.Where(m =>(m.SERIE.Contains(serie.ToUpper()))|| (m.SERIE.Contains(serie.ToLower()))).ToList();

    //        List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

    //        return lp;
    //    }


    //    public List<string> getMarcas()
    //    {
    //        List<string> ma = new List<string>();
    //        ma = lvar.coleccion.Select(o => o.MARCA).Distinct().ToList();
    //        //objList.Select(o => o.typeId).Distinct().ToList();
    //        //ma.Sort();    
    //        return ma;
    //    }

    //    public List<string> getTipoEq()
    //    {
    //        List<string> ma = new List<string>();
    //        ma = lvar.coleccion.Select(o => o.DESCRIPCION).Distinct().ToList();
    //        //objList.Select(o => o.typeId).Distinct().ToList();
    //        //ma.Sort();    
    //        return ma;
    //    }
    //    public List<string> getModelos()
    //    {
    //        List<string> ma = new List<string>();
    //        ma = lvar.coleccion.Select(o => o.MODELO).Distinct().ToList();
    //        //objList.Select(o => o.typeId).Distinct().ToList();
    //        //ma.Sort();
    //        return ma;
    //    }

    //    public List<string> getModeloxMarca(string marca, string tipo)
    //    {
    //        if (tipo == null || marca == null)
    //        {
    //            return null;
    //        }
    //        List<AssetCompleto> mz = new List<AssetCompleto>();
    //        List<string> ma = new List<string>();
    //        //mz = lvar.coleccion.Where(m => (m.MARCA == marca) && (m.DESCRIPCION == tipo)).ToList();
    //        ma = lvar.coleccion.Where(m => (m.MARCA == marca) && (m.DESCRIPCION == tipo)).Select(m => m.MODELO).Distinct().ToList();
    //        // ma = lvar.coleccion.Where(m => (m.ID_ASSET == "32271")).Select(m => m.MODELO).Distinct().ToList();
    //        //objList.Select(o => o.typeId).Distinct().ToList();
    //        //ma.Sort();
    //        return ma;
    //    }

    //    public List<string> getMarcaxTipo(string tipo)
    //    {
    //        if (tipo == null)
    //        {
    //            return null;
    //        }
    //        List<string> ma = new List<string>();
    //        ma = lvar.coleccion.Where(m => m.DESCRIPCION == tipo).Select(m => m.MARCA).Distinct().ToList();
    //        //objList.Select(o => o.typeId).Distinct().ToList();
    //        //ma.Sort();
    //        return ma;
    //    }


    //    public int Cuenta()
    //    {
    //        return lvar.coleccion.Length;

    //    }


    //}


    //public class AssetCompleto
    //{
    //    public string ID_ASSET { get; set; }
    //    public string TIPO_ASSET { get; set; }
    //    public string DESCRIPCION { get; set; }
    //    public string ID_DATO { get; set; }
    //    public string MODELO { get; set; }
    //    public string SERIE { get; set; }
    //    public string MARCA { get; set; }
    //    public string ID_REMITO { get; set; }
    //    public string SYS_USER { get; set; }
    //    public string STAMP_DATE { get; set; }
    //    public string STATUS { get; set; }
    //    public string STATUS_DATE { get; set; }
    //    public string STATUS_USER { get; set; }
    //    public string STATUS_DET { get; set; }
    //    public string ID_PUESTO { get; set; }
    //    public string ASSING_USUARIO_ID { get; set; }
    //    public string CC { get; set; }
    //    public string ASSING_LEGAJO { get; set; }
    //    public string ASSING_STATUS { get; set; }
    //    public string DETALLE { get; set; }
    //    public string TECNICO { get; set; }
    //    public string DIRECTORIO { get; set; }
    //}


    public class Modelos
    {
        private string url { get; set; }
        private static Modelos lvar = null;
        public string rc { get; set; }
        public string msg { get; set; }
        public List<string> coleccion = new List<string>();//{ get; set; }
        public List<string> unicos = new List<string>();
        public Modelos(string ur)
        {
            url = ur;
        }


        public void agregar(string modelo)
        {
            coleccion.Add(modelo);


        }
        public IList<string> Distinct()
        {
            //public List<string> tt = new List<string>();
            IList<string> tt = this.coleccion.Distinct().ToList();
            IList<string> ttordenado = tt.OrderByDescending(o => o).ToList();
            return ttordenado;
        }
        public Modelos()
        {
        }

        //public int Cuenta()
        //{
        //    return lvar.coleccion.GetLength(0);
        //}

        public Modelos GetJ()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";


            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<Modelos>(js);

            }
            return lvar;
        }
    }

    public class Modelo
    {
        public string MODELO { get; set; }
    }

    /*--------------------------
    idAssets
    statusDest
    q
    idAdminUser
    statusOrig
    FechaHasta
    idUsuarioDestino
    descripcion
    Formulario
    ID_PUESTO
    OS
    --------------------------*/
    public class JMovEquipo
    {
        private string url { get; set; }
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
        
        
        public JMovEquipo(string ur)
        {
            url = ur;
        }
        public RetCode JPost(JMovEquipo m)
        {
            
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json, text/plain, *";

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
                RetCode estados = JsonConvert.DeserializeObject<RetCode>(js);
                oResponse.Close();
                //return js;
                return estados;
            }
        }

        public RetCode Mover(JMovEquipo m)
        {
            RetCode rc = new RetCode();
            m.q = "cas";
            m.FechaHasta = DateTime.Now.ToString(); 
            m.idAdminUser= Global.SeguridadUsr.usuario.ID;
            rc = JPost(m);
            return rc;
            
            //WebRequest oRequest = WebRequest.Create(url);
            //oRequest.Method = "POST";
            //oRequest.ContentType = "application/json, text/plain, *";

            //using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            //{
            //    string json;
            //    json = JsonConvert.SerializeObject(m);
            //    // Escribir los datos en el datastream
            //    oSW.Write(json);
            //    oSW.Flush();
            //    oSW.Close();

            //}
            //// Enviar la solicitud y crear el objeto con la repuesta
            //WebResponse oResponse = oRequest.GetResponse();
            ////WebResponse oResponse = oRequest.GetResponse();

            //// obtiene los datos de la respuesta
            //using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            //{
            //    Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            //    string js = oSR.ReadToEnd();
            //    RetCode estados = JsonConvert.DeserializeObject<RetCode>(js);
            //    oResponse.Close();
            //    //return js;
            //    return estados;
            //}
        }
        public void MostrarData(JMovEquipo x)
        {
            Console.WriteLine("Inventario :{0}", string.IsNullOrEmpty(x.idAssets.ToString()) ? "": x.idAssets.ToString());
            Console.WriteLine("ID_PUESTO  :{0}", string.IsNullOrEmpty(x.ID_PUESTO.ToString())?"": x.ID_PUESTO.ToString());
            Console.WriteLine("idUsrDest  :{0}", string.IsNullOrEmpty(x.idUsuarioDestino.ToString())?"": x.idUsuarioDestino.ToString());
            Console.WriteLine("descripcion:{0}", string.IsNullOrEmpty(x.descripcion.ToString())?"": x.descripcion.ToString());
            Console.WriteLine("OS         :{0}", string.IsNullOrEmpty(x.OS.ToString())?"": x.OS.ToString());
            Console.WriteLine("statusOrig :{0}", string.IsNullOrEmpty(x.statusOrig.ToString())?"": x.statusOrig.ToString());
            Console.WriteLine("statusDest :{0}", string.IsNullOrEmpty(x.statusDest.ToString())?"": x.statusDest.ToString());
            Console.WriteLine("q          :{0}", string.IsNullOrEmpty(x.q.ToString())?"": x.q.ToString());
            Console.WriteLine("idAdminUser:{0}", string.IsNullOrEmpty(x.idAdminUser.ToString())?"": x.idAdminUser.ToString());          
            Console.WriteLine("FechaHasta :{0}", string.IsNullOrEmpty(x.FechaHasta.ToString())?"": x.FechaHasta.ToString());
            Console.WriteLine("Formulario :{0}", string.IsNullOrEmpty(x.Formulario.ToString())?"": x.Formulario.ToString());
       }
        public RetCode MovBaja(JMovEquipo m)
        {
            string StBaja = m.statusDest;
            RetCode rc = new RetCode();
            m.q = "cas";
            //----------Mover a Sin Puesto
            m.ID_PUESTO = "SIN_PUESTO";
            //m.statusDest = "6"; // estado pañol
            //m.statusOrig = "7";
            //rc =m.JPost(m);
            //----------Dar de baja
            
            switch (StBaja.ToUpper())
            {
                case "BAJA":
                    StBaja = "13";
                    break;

                case "VENTA INTERNA":
                    StBaja = "12";
                    break;

                case "DONACION":
                    StBaja = "11";
                    break;
            }
            m.statusDest = StBaja;
            rc=m.JPost(m);
            return rc;


        }

        public RetCode MovTec()
        {
            RetCode re = new RetCode();
            return re;
        }

        public RetCode MovResponsable(JMovEquipo mv)
        {
            RetCode re = new RetCode();
            return re;
        }

        public RetCode MovCambioPuesto(JMovEquipo mv)
        {
            RetCode re = new RetCode();
            return re;
        }

        public RetCode MovEnTrans(JMovEquipo mv)
        {
            RetCode re = new RetCode();
            return re;
        }
        public RetCode StatusPan(JMovEquipo m)
        {
            string StBaja = m.statusDest;
            RetCode rc = new RetCode();
            m.q = "cas";
            //----------Mover a Sin Puesto
           // m.ID_PUESTO = "SIN_PUESTO";
            m.statusDest = "6"; // estado pañol
           // m.statusOrig = "7";
            rc = m.JPost(m);
           
            return rc;


        }

    }
    public class MovEquipo
    {

        private string url { get; set; }
        //private static MovEquipo lvar = null;
        public static JMovEquipo carlo;
        public MovEquipo(string ur)
        {
            url = ur;
        }
        public JMovEquipo GetJ()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";


            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                carlo = JsonConvert.DeserializeObject<JMovEquipo>(js);

            }
            return carlo;
        }
        public RetCode JPost(JMovEquipo m)
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json, text/plain, *";

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
                RetCode estados = JsonConvert.DeserializeObject<RetCode>(js);
                oResponse.Close();
                //return js;
                return estados;
            }
        }
    }

 

    public class Transconf1
    {
        public string token { get; set; }
        public string q { get; set; }
        public string idAdminUser { get; set; }
        public Int32 opcion { get; set; }

    }


  

    public class Lugar
    {
       // string _id_lugar;

        public int ID { get; set; }

        //public string ID_lugar
        //{
        //    get => _id_lugar;
        //    set
        //    {
        //        ;

        //    }
        //}
        public string Responsable { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCambio { get; set; }
        public string Admin { get; set; }
        //public Lugar()
        //{
        //    ID = 0;
        //    ID_lugar = "000-0-000";

        //}
        public Lugar setResponable(int Id_usuario) 
        {
            Lugar aux = new Lugar();
            return aux;
        }
        public Lugar setResponable(string Id)
        {
            Lugar aux = new Lugar();
            return aux;
        }

    }

}
