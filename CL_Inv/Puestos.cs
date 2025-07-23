////using CAD_Inv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace InventarioAsset
{
    public class CTESpuesto
    {
        public const string ModoListaALL = "Todos";
        public const string ModoListaActivo = "Activos";
        public const string ModoHistorico = "Historico";
        public const string AgregarPuesto = "addLugar";
        public const string EditarPuesto = "modLugar";
        public const string BorrarPuesto = "delLugar";
    }
    public class Puesto
    {
        public string id { get; set; }
        public string NombrePuesto { get; set; }
        public string Responsable { get; set; }
        public string Comentario { get; set; }
        public string Descripcion { get; set; }
        public string Activo { get; set; }
        public string fechaCreacion { get; set; }
        public string Admin { get; set; }

        #region METODOS

      
        /*
        Console.WriteLine("ID             : {0}", x.ID);
            Console.WriteLine("ID_Lugar       : {0}", x.idlugar);
            Console.WriteLine("Responsable    : {0}", x.responsable);
            Console.WriteLine("Fecha Creacion : {0}", x.fechacreacion);
            Console.WriteLine("Comentario     : {0}", x.comentario);
            Console.WriteLine("Descripcion    : {0}", x.descripcion);
            Console.WriteLine("Admin          : {0}", x.admin);
            Console.WriteLine("Activo         : {0}", x.activo);
        */
        public static Puesto Conversor(Jpuesto pf)
        {

            Puesto x = new Puesto();
            x.id = pf.ID;
            x.NombrePuesto = pf.id_lugar;

            x.Responsable = pf.responsable;
            x.Comentario = pf.comentario;
            x.Descripcion = pf.descripcion;
            x.Activo = pf.activo;
            x.fechaCreacion = pf.fechacreacion;
            x.Admin = pf.admin;
            return x;

        }

        public List<Puesto> ListarTodosPuestos()
        {
            //string filtrex="";
            Jpuestos jp = new Jpuestos();
            List<Puesto> lp = new List<Puesto>();
           
            jp = jp.JSONget("?q=ListaLugares&d=ALL");

            lp = jp.coleccion.ConvertAll(new Converter<Jpuesto, Puesto>(Conversor));
            return lp;
        }
        public List<Puesto> ListarHistoria( string NombrePuesto)
        {
            //string filtrex = "";
            Jpuestos jp = new Jpuestos();
            List<Puesto> lp = new List<Puesto>();
            //switch (filtro)
            //{
            //    case CTESpuesto.ModoHistorico:
            //        filtrex = "?q=ListaLugares&d=hi&lu=" + NombrePuesto;

            //        break;
            //    case CTESpuesto.ModoListaALL:
            //        filtrex = "?q=ListaLugares&d=ALL";
            //        break;

            //    case CTESpuesto.ModoListaActivo:
            //        filtrex = "?q=ListaLugares&d=now";
            //        break;
            //}
            jp = jp.JSONget("?q=ListaLugares&d=hi&lu=" + NombrePuesto);

            lp = jp.coleccion.ConvertAll(new Converter<Jpuesto, Puesto>(Conversor));
            return lp;
        }

        public Puesto BuscarPuesto(string nombrePuesto)
        {
            Puesto px = new Puesto();
            List<Puesto> lp = px.ListarPuestosActivos();
            px= lp.Find(p => p.NombrePuesto.ToUpper() == nombrePuesto.ToUpper() );
            if (px == null)
                return px;
            else
                return px;
        }
        public List<Puesto> ListarPuestosActivos()
        {
           
            Jpuestos jp = new Jpuestos();
            List<Puesto> lp = new List<Puesto>();
           
            jp = jp.JSONget("?q=ListaLugares&d=now");

            lp = jp.coleccion.ConvertAll(new Converter<Jpuesto, Puesto>(Conversor));
            //lp=lp.FindAll(p => p.Activo == "1");
            lp=lp.OrderBy(p=>p.NombrePuesto).ToList();
            return lp;
        }

        public RetCode AgregarPuestos(Puesto paux)
        {
            List<Puesto> lp = new List<Puesto>();
            RetCode rc = new RetCode();
            Jpuesto np = new Jpuesto();
            np.q = CTESpuesto.AgregarPuesto;
            np.ID = paux.id;
            np.idlugar = paux.NombrePuesto;
            np.responsable = paux.Responsable;// "";
            np.descripcion = paux.Descripcion;// "";// "el puesto de sarrsami";
            np.fechacreacion= DateTime.Now.ToString();
            np.comentario = paux.Comentario;// "El polaquito";
            np.admin = paux.Admin;
            np.activo =paux.Activo;// "";
 
            rc= np.JSONPost(np);
            return rc;
        }

        public RetCode EditarPuestos(Puesto paux)
        {
            List<Puesto> lp = new List<Puesto>();
            RetCode rc = new RetCode();
            Jpuesto np = new Jpuesto();
            np.q = CTESpuesto.EditarPuesto;
            np.ID = paux.id;
            np.idlugar = paux.NombrePuesto;
            np.responsable = paux.Responsable;// "";
            np.descripcion = paux.Descripcion;// "";// "el puesto de sarrsami";
            np.comentario = paux.Comentario;// "El polaquito";
            np.admin = paux.Admin;//
            np.activo = paux.Activo;// "";
            rc=np.JSONPost(np);
            return rc;
        }

        public RetCode BorrarPuestos(Puesto paux)
        {
            List<Puesto> lp = new List<Puesto>();
            RetCode rc = new RetCode();

            Jpuesto np = new Jpuesto();
            //np = paux;
            np.q = CTESpuesto.BorrarPuesto;
            np.ID = paux.id;

            np.idlugar = paux.NombrePuesto;
            np.comentario = paux.Comentario;// "El polaquito";
            np.descripcion = paux.Descripcion;// "";// "el puesto de sarrsami";
            np.responsable = paux.Responsable;// "";
            np.admin = paux.Admin;//
            np.activo = paux.Activo;// "";
            //                                     //np.info.admin = "764";
            //                                     // np.idAdminUser = "764";
            //np.idadminuser = "764";
            //rc = np.info.JSONPost(np);
            rc=np.JSONPost(np);
            return rc;
        }
        #endregion
    }
}