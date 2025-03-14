using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
//using Generico;

namespace InventarioAsset
{
    public  class CTEJpedido
    {
        public const string ModoListaALL="Todos";
        public const string ModoListaOS= "OS";
        public const string AgregarPedido = "addPedido";
        public const string EditarPedido = "modPedido";
        public const string BorrarPedido = "delPedido";
    }

    public class Jpedidos : Jpedido
    {
        public string rc { get; set; }
        public string msg { get; set; }
        public string idadminuser { get; set; }
        public string q { get; set; }
        public List<Jpedidos> coleccion { get; set; }

        public Jpedido info=new Jpedido();
       // public Jpedidos() { };
    }
    

    public class Jpedido
    {
        public string _url;
        public string ID { get; set; }
        public string os { get; set; }
        public string solicitante { get; set; }
        public string equipo { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string FECHASOL { get; set; }

        public string comentario { get; set; }



        #region METODOS
        /*
        *  --- Devuelve los puestos activos ----
        *  http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=ListaPedidos&d=ALL
        *  --- Devuelve la historia del puesto indicado en el parámetro lu ----
        *   http://mglab010.metrogas.com.ar/pgil/vista/ajax/ajaxEquipos.php?q=ListaPedidos&d=hi&os=88
            addPedido
            modPedido
            delPedido

       */

        public Jpedido()
        {

            _url = Global.urlBase + "/ajaxEquipos.php?";

        }
        public void Mostrarpedido(Jpedido x)
        {
            Console.WriteLine("ID             : {0}", x.ID);
            Console.WriteLine("OS             : {0}", x.os);
            Console.WriteLine("Solicitante    : {0}", x.solicitante);
            Console.WriteLine("Equipo         : {0}", x.equipo);
            Console.WriteLine("Marca          : {0}", x.marca);
            Console.WriteLine("Modelo         : {0}", x.modelo);
            Console.WriteLine("Fecha Creacion : {0}", x.FECHASOL);
        }
        
        public Jpedidos JSONget(string c, string os)
        {

            string valor = "";
            switch (c)
            {

                case CTEJpedido.ModoListaOS:
                    valor = "q=ListaPedidos&d=hi&os="+os;
                    break;

                case CTEJpedido.ModoListaALL:
                    valor = "q=ListaPedidos&d=ALL";
                    break;
            }
            _url = _url + valor;

            Jpedidos aux = new Jpedidos();
            WebRequest oRequest = WebRequest.Create(_url);
            oRequest.Method = "get";
            oRequest.ContentType = "application/json, text/plain, *";

            WebResponse oResponse = oRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)oResponse).StatusDescription);
            using (var oSR = new StreamReader(oResponse.GetResponseStream()))
            {
                var js = oSR.ReadToEnd();
                aux = JsonConvert.DeserializeObject<Jpedidos>(js);

            }
            return aux;
        }




        public RetCode JSONPost(Jpedidos m)
        {
            WebRequest oRequest = WebRequest.Create(_url);
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
       
        #endregion
    }
   
}