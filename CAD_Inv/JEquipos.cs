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
    public class JSONAllAsset
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public AssetCompleto[] coleccion { get; set; }

    }
    // Clase para equipo Extendido, es decir, agrega el puesto
    public class EquipoExt : Equipo
    {
        public string Usuario { get; set; }
        public string Puesto { get; set; }

       // public string UltNota { get; set; }
        public string Status_date { get; set; }

        public string detalle { get; set; }
    }
    public class JSONNotas
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public JSONNota[] coleccion { get; set; }
    }
    public class UpdateNota
    {
        [JsonProperty("ID_ASSET")]
        public int IdAsset { get; set; }

        [JsonProperty("nota")]
        public string nota { get; set; }

        [JsonProperty("idAdminUser")]
        public string idAdminUser { get; set; }
        [JsonProperty("q")]
        public string q { get; set; }

    }
    public class Notas
    {

        private string url { get; set; }
        private JSONNotas lvar = null;
        public Notas(string ur)
        {
            url = ur;
        }

        
        //public JSONNotas JSONget()
        //{
           
        //    oRequest.Method = "get";
        //    oRequest.ContentType = "application/json, text/plain, */*";

        //    WebResponse oResponse = oRequest.GetResponse();

        //    Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
        //    using (var oSR = new StreamReader(oResponse.GetResponseStream()))
        //    {
        //        var js = oSR.ReadToEnd();
        //        lvar = JsonConvert.DeserializeObject<JSONNotas>(js);

        //    }
        //    return lvar;
        //}

        //public RetCode JSONPost(JSONNotas jx)
        //{
        //    WebRequest oRequest = WebRequest.Create(url);
        //    oRequest.Method = "POST";
        //    oRequest.ContentType = "application/json, text/plain, *";

        //    using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
        //    {
        //        string json;
        //        json = JsonConvert.SerializeObject(jx);
        //        // Escribir los datos en el datastream
        //        oSW.Write(json);
        //        oSW.Flush();
        //        oSW.Close();

        //    }
        //    // Enviar la solicitud y crear el objeto con la repuesta
        //    WebResponse oResponse = oRequest.GetResponse();
        //    //WebResponse oResponse = oRequest.GetResponse();

        //    // obtiene los datos de la respuesta
        //    using (var oSR = new StreamReader(oResponse.GetResponseStream()))
        //    {
        //        Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
        //        string js = oSR.ReadToEnd();
        //        RetCode estados = JsonConvert.DeserializeObject<RetCode>(js);
        //        oResponse.Close();
        //        //return js;
        //        return estados;
        //    }
        //}
        public int Cuenta()
        {

            return lvar.coleccion.GetLength(0);
        }
        public void addNota()
        {
            //Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + idequipo);
            //JSONNotas jma = ma.JSONget();
            //          "q": "addNotaAsset",
            //"ID_ASSET": "17118",
            //"nota": "notita",
            //"idAdminUser":"764"
        }
    }

    public class JSONNota
    {
        public string ID { get; set; }
        public string ID_ASSET { get; set; }
        public string NOTA { get; set; }
        public string STAMP_USER { get; set; }
        public string STAMP_DATE { get; set; }
        public string USER_ID { get; set; }
        public string FULLNAME { get; set; }
        public string ARCHIVO { get; set; }
    }
    public class AllAssets
    {


        private string url { get; set; }
        private static JSONAllAsset lvar;
        public int indice { get; set; }
        //public As_pue_usrs()
        //{
        //    lvar = new As_pue_usrs();
        //}
        public AllAssets(string ur)
        {
            url = ur;
        }

      

        public List<EquipoExt> getInvxCaractEnPan(string marca, string modelo, string tipo)
        {
            //List<AssetCompleto> mz = new List<AssetCompleto>();
            //List<string> ma = new List<string>();
            //mz = lvar.coleccion.Where(m => (m.MARCA == marca) && (m.DESCRIPCION == tipo)).ToList();
            List<AssetCompleto> ma = lvar.coleccion.Where(m => ((m.STATUS == "6") && (m.MARCA == marca) && (m.DESCRIPCION == tipo) && (m.MODELO == modelo))).ToList();
            // ma = lvar.coleccion.Where(m => (m.ID_ASSET == "32271")).Select(m => m.MODELO).Distinct().ToList();
            //objList.Select(o => o.typeId).Distinct().ToList();
            //ma.Sort();
            List<EquipoExt> mz = ma.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return mz;
        }

        public static string Asset2Inv(AssetCompleto x)
        {
            string inv = x.ID_ASSET;
            return inv;
        }
        public string getLogedUser()
        {
            return null;
        }

        //public JSONAllAsset JSONget()
         public JSONAllAsset JSONget()
        {
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js =  oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<JSONAllAsset>(js);

            }
            return lvar;
        }
        public List<EquipoExt> GetEquipos()
        {

            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.STATUS_DET != "DONACION") && (m.STATUS_DET != "VENTA INTERNA") && (m.STATUS_DET != "BAJA")).ToList();
            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
            //Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + idequipo);
            //JSONNotas jma = ma.JSONget();
            //if (jma.coleccion.Count() != 0)
            //    lp[0].UltNota = jma.coleccion[0].NOTA;
            return lp;


        }

        public List<EquipoExt> GetListaEquipos(List<string> inv)
        {
            List<EquipoExt> lp = new List<EquipoExt>();
            EquipoExt aux=new EquipoExt();
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            foreach (string idequipo in inv)
            {
                AssCom = lvar.coleccion.Where(m => m.ID_ASSET == idequipo).ToList();
                //  lp=  AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq)); 
                if (AssCom.Count > 0)
                { 
                    aux = AssetToEq(AssCom[0]); 
                }
                else
                {
                    aux.ID_Inv = "0";// idequipo;
                    aux.DESCRIPCION = "";// "No existe equipo";
                }
                //Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + idequipo);
                //JSONNotas jma = ma.JSONget();
                //if (jma.coleccion.Count() != 0)
                //    lp[0].UltNota = jma.coleccion[0].NOTA;
                lp.Add(aux);
            }
            return lp;


        }
        public EquipoExt GetEquipo(string inv)
        {
            EquipoExt lp = new EquipoExt();
            //EquipoExt aux = new EquipoExt();
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            
                AssCom = lvar.coleccion.Where(m => m.ID_ASSET == inv).ToList();
                //  lp=  AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq)); 
                if (AssCom.Count > 0)
                {
                    lp = AssetToEq(AssCom[0]);
                }
                else
                {
                    lp.ID_Inv = "0";// idequipo;
                    lp.DESCRIPCION = "";// "No existe equipo";
                }
               
               //
            
            return lp;


        }
        public List<EquipoExt> getEquiposxPuesto(string puesto)
        {
            puesto = puesto.ToUpper();
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.ID_PUESTO != null) && (m.ID_PUESTO.Contains(puesto))).ToList();

            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }

        public List<EquipoExt> getEquiposxTipo(string tipo)
        {
            tipo = tipo.ToUpper();
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.DESCRIPCION==tipo)).ToList();

            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }

        public List<EquipoExt> getEquiposxUsuario(string Usuario)
        {

            string USUARIO = Usuario.ToUpper();
            string usuario = Usuario.ToLower();

            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.ASSING_USUARIO_ID != null) && ((m.ASSING_USUARIO_ID.Contains(usuario)) || (m.ASSING_USUARIO_ID.Contains(USUARIO)))).ToList();// || (m.ASSING_USUARIO_ID.Contains(Usuario.ToUpper()))).ToList();

            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }
        public List<EquipoExt> getEquiposxEstado(string Estado)
        {
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            if (Estado == "BAJA")
            {
                AssCom = lvar.coleccion.Where(m => (m.STATUS_DET == "DONACION") || (m.STATUS_DET == "VENTA INTERNA") || (m.STATUS_DET == "BAJA")).OrderByDescending(p=>p.ID_ASSET).ToList();

            }
            else
            {

                AssCom = lvar.coleccion.Where(m => m.STATUS == Estado).OrderByDescending(p => p.ID_ASSET).ToList();


            }
            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
            return lp;
        }

        public List<EquipoExt> getEquiposRevisados()
        {
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.REVISADO=="1" || m.ID_PUESTO=="ZZZ-0-000")).OrderByDescending(p=>p.ID_ASSET).ToList();
            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
            return lp;
        }
        public List<EquipoExt> getEqxEstadoxFecha(string Estado, DateTime rangoMin, DateTime rangoMax)
        {
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            if (Estado == "BAJA")
            {
                AssCom = lvar.coleccion.Where(m => (m.STATUS_DET == "DONACION") || (m.STATUS_DET == "VENTA INTERNA") || (m.STATUS_DET == "BAJA")).ToList();
                //               AssCom = AssCom.Where(m => Convert.ToDateTime(m.STAMP_DATE) >= Convert.ToDateTime("2018-05-29 14:18")).ToList();
                AssCom = AssCom.Where(m => (Convert.ToDateTime(m.STATUS_DATE) >= Convert.ToDateTime(rangoMin.ToString("yyyy-MM-dd"))) && (Convert.ToDateTime(m.STATUS_DATE) <= Convert.ToDateTime(rangoMax.ToString("yyyy-MM-dd")))).ToList();
            }
            else
            {

                AssCom = lvar.coleccion.Where(m => m.STATUS_DET == Estado).ToList();
                AssCom = AssCom.Where(m => Convert.ToDateTime(m.STAMP_DATE) > rangoMin && Convert.ToDateTime(m.STAMP_DATE) < rangoMax).ToList();


            }
            //Notas ma = new Notas(Global.urlBase + "/ajaxEquipos.php?q=anotas&d=" + idequipo);
            //JSONNotas jma = ma.JSONget();
            //if (jma.coleccion.Count() != 0)
            //    lp[0].UltNota = jma.coleccion[0].NOTA;
            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));
            return lp;
        }

        public List<EquipoExt> getEquiposxMarca(string Marca)
        {
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.MARCA != null) && (m.MARCA == Marca)).ToList();

            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }


        public List<EquipoExt> getEquiposxModelo(string Modelo)
        {
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.MODELO != null) && (m.MODELO == Modelo)).ToList();

            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }
        public static EquipoExt AssetToEq(AssetCompleto pf)
        {

            EquipoExt x = new EquipoExt();
            x.ID_Inv = pf.ID_ASSET;
            x.ID_REMITO = pf.ID_REMITO;
            x.MARCA = pf.MARCA;
            x.MODELO = pf.MODELO;
            x.SERIE = pf.SERIE;
            // x.TIPO_ASSET = pf.TIPO_ASSET;
            x.Estado = pf.STATUS_DET;
            x.DESCRIPCION = pf.DESCRIPCION;
            x.Puesto = pf.ID_PUESTO;
            x.Usuario = pf.ASSING_USUARIO_ID;
            //x.UltNota = "";
            x.Status_date = pf.STATUS_DATE;
            x.detalle = pf.DETALLE;
            x.ID_Estado = pf.STATUS;
            x.REVISADO = pf.REVISADO;
            //new Point(((int)pf.X), ((int)pf.Y));
            return x;
        }
        public List<EquipoExt> getSerie(string serie)
        {
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.SERIE.Contains(serie.ToUpper())) || (m.SERIE.Contains(serie.ToLower()))).ToList();

            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }


        public List<string> getMarcas()
        {
            List<string> ma = new List<string>();
            ma = lvar.coleccion.Select(o => o.MARCA).Distinct().ToList();
            ma.RemoveAll(string.IsNullOrEmpty);
            //objList.Select(o => o.typeId).Distinct().ToList();
            //ma.Sort();
            ma = ma.OrderBy(o => o).ToList();
            return ma;
        }

        public List<string> getTipoEq()
        {
            List<string> ma = new List<string>();
            ma = lvar.coleccion.Select(o => o.DESCRIPCION).Distinct().ToList();
            // ma = ma.Select(o => o != null);
            ma.RemoveAll(string.IsNullOrEmpty);
            //objList.Select(o => o.typeId).Distinct().ToList();
            //ma.Sort();    
            ma = ma.OrderBy(o => o).ToList();
            return ma;
        }
        public List<string> getModelos()
        {
            List<string> ma = new List<string>();
            ma = lvar.coleccion.Select(o => o.MODELO).Distinct().ToList();
            //ma = ma.Select(o => o != null);
            ma.RemoveAll(string.IsNullOrEmpty);
            ma = ma.OrderBy(o => o).ToList();
            //objList.Select(o => o.typeId).Distinct().ToList();
            //ma.Sort();

            return ma;
        }

        public List<string> getModeloxMarca(string marca, string tipo)
        {
            if (tipo == null || marca == null)
            {
                return null;
            }
            List<AssetCompleto> mz = new List<AssetCompleto>();
            List<string> ma = new List<string>();
            //mz = lvar.coleccion.Where(m => (m.MARCA == marca) && (m.DESCRIPCION == tipo)).ToList();
            ma = lvar.coleccion.Where(m => (m.MARCA == marca) && (m.DESCRIPCION == tipo)).Select(m => m.MODELO).Distinct().ToList();
            // ma = lvar.coleccion.Where(m => (m.ID_ASSET == "32271")).Select(m => m.MODELO).Distinct().ToList();
            //objList.Select(o => o.typeId).Distinct().ToList();
            //ma.Sort();
            return ma;
        }


        public List<string> getMarcaxTipo(string tipo)
        {
            if (tipo == null)
            {
                return null;
            }
            List<string> ma = new List<string>();
            ma = lvar.coleccion.Where(m => m.DESCRIPCION == tipo).Select(m => m.MARCA).Distinct().ToList();
            //objList.Select(o => o.typeId).Distinct().ToList();
            //ma.Sort();
            return ma;
        }

        public List<EquipoExt> getEquiposfiltro(EquipoExt ext)
        {
            if (ext.DESCRIPCION == null)
            {
                return null;
            }
            List<AssetCompleto> AssCom = new List<AssetCompleto>();
            AssCom = lvar.coleccion.Where(m => (m.MODELO == ext.MODELO) && (m.MARCA == ext.MARCA)  && (m.DESCRIPCION == ext.DESCRIPCION)).ToList();
            if (!string.IsNullOrEmpty(ext.ID_Estado))
            {
                AssCom = AssCom.Where(m => (m.STATUS == ext.ID_Estado)).ToList();
            }
            if (!string.IsNullOrEmpty(ext.Puesto))
            {
                AssCom = AssCom.Where(m => (m.ID_PUESTO==ext.Puesto)).ToList();
            }


            List<EquipoExt> lp = AssCom.ConvertAll(new Converter<AssetCompleto, EquipoExt>(AssetToEq));

            return lp;
        }

        public int Cuenta()
        {
            return lvar.coleccion.Length;

        }


    }


    public class AssetCompleto
    {
        public string ID_ASSET { get; set; }
        public string TIPO_ASSET { get; set; }
        public string DESCRIPCION { get; set; }
        public string ID_DATO { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string MARCA { get; set; }
        public string ID_REMITO { get; set; }
        public string SYS_USER { get; set; }
        public string STAMP_DATE { get; set; }
        public string STATUS { get; set; }
        public string STATUS_DATE { get; set; }
        public string STATUS_USER { get; set; }
        public string STATUS_DET { get; set; }
        public string ID_PUESTO { get; set; }
        public string ASSING_USUARIO_ID { get; set; }
        public string CC { get; set; }
        public string ASSING_LEGAJO { get; set; }
        public string ASSING_STATUS { get; set; }
        public string DETALLE { get; set; }
        public string TECNICO { get; set; }
        public string DIRECTORIO { get; set; }
        public string REVISADO { get; set; }
        public string FECHA_REV { get; set; }

    }
    public class Equipo
    {
        public string ID_Inv { get; set; }
        //public string TIPO_ASSET { get; set; }
        public string Estado { get; set; }
        public string DESCRIPCION { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string MARCA { get; set; }

        public string Fecha_Ingreso { get; set; }
        public string ID_REMITO { get; set; }

        public string ID_Estado { get; set; }

        public string REVISADO { get; set; }
        private string _url;

        public Equipo()
        {

            _url = Global.urlBase + "/ajaxEquipos.php?";

        }


        public RetCode JSONPost(jRevisado jx)
        {
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/json, text/plain, *";

            using (var oSW = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json;
                json = JsonConvert.SerializeObject(jx);
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
        public RetCode setRevisado(string nro_inv)
        {
            jRevisado zx =new jRevisado();
            zx.q = "modrevision";
            // SI el valor del inventario es -1 significa que se va a reiniciar el revis
            if(nro_inv == "-1")
            {
                zx.id_asset = nro_inv;
                zx.valor = "1";
                zx.reset = "si";
            }
            else
            {
                zx.id_asset = nro_inv;
                zx.valor = "1";
            }
            //zx.id_asset = nro_inv;
            //zx.valor = "1";
            RetCode rc = JSONPost(zx);
            return rc;
        }

        public void modserie(Equipo datanew, Equipo dataold)
        {
            jmodserie zx = new jmodserie();
            zx.q = "modserie";
            // SI el valor del inventario es -1 significa que se va a reiniciar el revis
            zx.id_asset = datanew.ID_Inv;
            zx.nuevoserial = datanew.SERIE;

            string url = Global.urlBase + "/ajaxEquipos.php";
            WebService.PostData<jmodserie>(url,zx);
            
        }

        public void UnsetRevisado(string nro_inv)
        {
            jRevisado zx = new jRevisado();
            zx.q = "modrevision";
            zx.id_asset = nro_inv;
            zx.valor = "0";
            RetCode rc = JSONPost(zx);
        }

    }

    public class jmodserie
    {
        public string q;
       
        public string id_asset;
        public string nuevoserial;
    }
    public class jRevisado 
    {
        public string q;
        public string valor;
        public string id_asset;
        public string reset;
    
    }


    public class RootPermisoXTipo
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public PermisoXTipo[] coleccion { get; set; }
    }

    public class PermisoXTipo
    {
        public string ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string PERFILES { get; set; }
        public string OBS_DAYS { get; set; }
        private string _url;

        public PermisoXTipo(string url)
        {
            _url = url + "/ajaxEquipos.php?q=tipop";
        }
        static List<PermisoXTipo> xvar;// = new List<PermisoXTipo>();
        public List<PermisoXTipo> JSONget()
        {
            RootPermisoXTipo lvar = new RootPermisoXTipo();
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                lvar = JsonConvert.DeserializeObject<RootPermisoXTipo>(js);

            }
            xvar= lvar.coleccion.ToList();
            return lvar.coleccion.ToList();
        }
        public void Imprimir()
        {

        }

      
        public class Perfiles
        {
            Ninventario[] col1 { get; set; }
        }
    }

    public class EquipoCompras:EquipoExt
    {
        // dataEq;
        public string OC { get; set; }      //desde joocc
        public string APEM { get; set; }    //desde joocc
        public string SOLP { get; set; }    //desde joocc
        public string FACTURA { get; set; } //desde joocc
        public string FECHA_OC { get; set; }   //desde joocc FECHA_OC <-- FECHA
        public string REMITO { get; set; }  //desde jremito
        public string PROVEEDOR { get; set; } //desde jremito
        public string FECHA_REMITO { get; set; }
        public Eq_DatosCompra datosCompra;
        public List<EquipoCompras> getDataCompras(List<string> invs)
        {
            EquipoExt eqx = new EquipoExt();
            List<EquipoCompras> resultado = new List<EquipoCompras>();
            //List<Eq_DatosCompra> m = new List<Eq_DatosCompra>();
            //Eq_DatosCompra dc = new Eq_DatosCompra();
           // EquipoCompras lpm = new EquipoCompras();
            
            foreach (string invaux in invs)
            {
                DatoCompra dc = new DatoCompra();
                EquipoCompras lpm = new EquipoCompras();
                eqx = Global.TodosLosAsset.GetEquipo(invaux);
                dc = dc.getData(eqx.ID_REMITO);
                if (dc!=null)
                {
                    
                    lpm.OC = dc.OC;
                    lpm.APEM = dc.APEM;
                    lpm.SOLP = dc.SOLP;
                    lpm.FACTURA = "";
                    lpm.FECHA_OC = dc.FECHA;
                    lpm.REMITO = dc.REMITO;
                    lpm.PROVEEDOR = dc.PROVEEDOR;
                }
                else
                {
                    lpm.OC = "";
                    lpm.APEM = "";
                    lpm.SOLP = "";
                    lpm.FACTURA = "";
                    lpm.FECHA_OC = "";
                    lpm.REMITO = "";
                    lpm.PROVEEDOR = "";
                }   
                
                


                lpm.DESCRIPCION=eqx.DESCRIPCION;
                lpm.ID_Inv = eqx.ID_Inv;
                lpm.MARCA = eqx.MARCA;
                lpm.MODELO = eqx.MODELO;
                lpm.detalle = eqx.detalle;
                lpm.SERIE = eqx.SERIE;
                lpm.Estado = eqx.Estado;
               
                resultado.Add(lpm);
            }
            return resultado;

        }
    }


    /* -------------------------------------------------------------- */
   

}
