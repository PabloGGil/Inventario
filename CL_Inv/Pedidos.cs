using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
////using CAD_Inv;

namespace InventarioAsset
{
    public class Pedidos
    {
        public string Id { get; set; }
        public string os { get; set; }
        public string solicitante { get; set; }
        public string equipo { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string FECHASOL { get; set; }
        public string Comentario { get; set; }
        #region METODOS
       


        //public List<clase destino> conversion()
        //{
        //    List<clase origen> AssCom = new List<clase origen>();
        //    AssCom = lvar.coleccion.Where(m => m.MODELO == Modelo).ToList();

        //    List<clase destino> lp = AssCom.ConvertAll(new Converter<clase origen, clase destino>(FuncionConversora));
        //}
        //public static clase destino FuncionConversora(clase origen pf)
        //{

        //    EquipoExt x = new EquipoExt();
        //    x.ID_Inv = pf.ID_ASSET;
        //    x.ID_REMITO = pf.ID_REMITO;
        //    x.MARCA = pf.MARCA;
        //    x.MODELO = pf.MODELO;
        //    x.SERIE = pf.SERIE;
        //    // x.TIPO_ASSET = pf.TIPO_ASSET;
        //    x.Estado = pf.STATUS_DET;
        //    x.DESCRIPCION = pf.DESCRIPCION;
        //    x.Puesto = pf.ID_PUESTO;
        //    x.Usuario = pf.ASSING_USUARIO_ID;
        //    x.UltNota = "";
        //    x.Status_date = pf.STATUS_DATE;
        //    x.detalle = pf.DETALLE;
        //    //new Point(((int)pf.X), ((int)pf.Y));
        //    return x;
        //}

        public void clear(ref Pedidos pe)
        {
            pe.solicitante = "";
            pe.marca = "";
            pe.modelo = "";
            pe.equipo = "";
            pe.FECHASOL = "";
            pe.os = "";
        }

        public static Pedidos Conversor(Jpedido pf)
        {

            Pedidos x = new Pedidos();
            x.os = pf.os;
            x.equipo = pf.equipo;
            x.marca = pf.marca;
            x.modelo = pf.modelo;
            x.FECHASOL = pf.FECHASOL;
            x.solicitante = pf.solicitante;
            x.Comentario = pf.comentario;
            x.Id = pf.ID;
            //    x.SERIE = pf.SERIE;
            //    // x.TIPO_ASSET = pf.TIPO_ASSET;
            //    x.Estado = pf.STATUS_DET;
            //    x.DESCRIPCION = pf.DESCRIPCION;
            //    x.Puesto = pf.ID_PUESTO;mañana martes????
            //    x.Usuario = pf.ASSING_USUARIO_ID;
            //    x.UltNota = "";
            //    x.Status_date = pf.STATUS_DATE;
            //    x.detalle = pf.DETALLE;
            //    //new Point(((int)pf.X), ((int)pf.Y));
            return x;
        }

        public List<Pedidos> ListarPedido(string usuario)
        {
            // new List<Pedidos>();
            Jpedidos jp = new Jpedidos();
            jp = jp.JSONget(CTEJpedido.ModoListaALL, "");
            if (usuario != "ALL")
            {
                jp.coleccion = jp.coleccion.Where(o => o.solicitante == usuario).ToList();
            }
            List<Pedidos> lp = jp.coleccion.ConvertAll(new Converter<Jpedidos, Pedidos>(Conversor));
            return lp;
        }

        
        public RetCode AgregarPedido(Pedidos py)
        {
           
            RetCode rc = new RetCode();
            Jpedidos np = new Jpedidos();
            np.q = CTEJpedido.AgregarPedido;
            np.info.ID = "";

            np.info.os = py.os;
            np.info.marca = py.marca;// "El polaquito";
            np.info.modelo = py.modelo;// "";// "el puesto de sarrsami";
            np.info.equipo = py.equipo;// "";
            np.info.solicitante = py.solicitante;//np.info.activo = txtactivo.Text;// "";
                                                 //np.info.admin = "764";
                                                 // np.idAdminUser = "764";
            np.info.comentario = py.Comentario;
            np.idadminuser = py.solicitante;//py.solicitante;
            //rc = np.info.JSONPost(np);
            np.info.JSONPost(np);
            return rc;
        }
        

        public RetCode EditarPedido(Pedidos py,string ID)
        {
            RetCode rc = new RetCode();
            Jpedidos np = new Jpedidos();
            np.q = CTEJpedido.EditarPedido;
            np.info.ID = ID;

            np.info.os = py.os;
            np.info.marca = py.marca;// "El polaquito";
            np.info.modelo = py.modelo;// "";// "el puesto de sarrsami";
            np.info.equipo = py.equipo;// "";
            np.info.solicitante = py.solicitante;//np.info.activo = txtactivo.Text;// "";
                                                 //np.info.admin = "764";

            np.info.comentario = py.Comentario;// np.idAdminUser = "764";
            np.idadminuser = py.solicitante;
            //rc = np.info.JSONPost(np);
            np.info.JSONPost(np);
            return rc;
        }

        public RetCode BorrarPedido(Pedidos py)
        {
            RetCode rc = new RetCode();
            Jpedidos np = new Jpedidos();
            np.q = CTEJpedido.BorrarPedido;
            np.info.ID = py.Id;

            //np.info.os = py.os;
            //np.info.marca = py.marca;// "El polaquito";
            //np.info.modelo = py.modelo;// "";// "el puesto de sarrsami";
            //np.info.equipo = py.equipo;// "";
            //np.info.solicitante = py.solicitante;//np.info.activo = txtactivo.Text;// "";
                                                 //np.info.admin = "764";
                                                 // np.idAdminUser = "764";
            np.idadminuser = py.solicitante;
            //rc = np.info.JSONPost(np);
            rc =np.info.JSONPost(np);
            return rc;
        }
        #endregion
    }
}
