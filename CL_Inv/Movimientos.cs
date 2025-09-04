using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualBasic;
using System.Windows;
////using CAD_Inv;

////using CAD_Inv;
//using ;

namespace InventarioAsset
{
    public class CTEMovimiento
    {
        public const string ST_ASIGNADO = "7";
        public const string ST_NUEVO = "6";
        public const string ST_TRANSITO = "5";
        public const string ST_DEPOSITO = "6";
        public const string ST_ASIGTEC = "15";
        public const string PUESTO_PANOL = "PAN-0-000";
        public const string ST_CONFTEC = "16";
        public const string ST_CONFUSR = "17";
        public const string ST_CONFPAN = "18";
        public const string ST_CONFPRE = "20";
        public const string ST_PRESTAM = "19";
        public const string ST_BAJA = "13";
        public const string ST_BAJA_VENTA_INTERNA = "12";
        public const string ST_BAJA_DONACION = "11";
        public const string REPARACION_I = "9";
        public const string REPARACION_E = "10";

    }
    public class OpcionesMov
    {
        public string st_origen { get; set; }
        public string st_destino { get; set; }
        public string Desc_movimiento { get; set; }
        //       
        private List<OpcionesMov> opciones = new List<OpcionesMov>();


        public void cargar()
        {
            foreach(Status_Change sc in Global.SeguridadUsr.status_change)
            {
                OpcionesMov om = new OpcionesMov();
                if (sc.ENTIDAD.ToLower() == "assets")
                {
                    om.st_destino = sc.STATUS_DEST;
                    om.st_origen = sc.STATUS_ORIG;

                    om.Desc_movimiento = sc.DESCRIPCION;
                    opciones.Add(om);
                }
            }
            ////opciones.Add(new OpcionesMov() { Indice = "1", valor = "Activo" });
            //opciones.Add(new OpcionesMov() { Indice = "5", valor = "En Transito" });
            //opciones.Add(new OpcionesMov() { Indice = "6", valor = "Pañol" });
            //opciones.Add(new OpcionesMov() { Indice = "7", valor = "Asignado a Usuario" });
            //opciones.Add(new OpcionesMov() { Indice = "9", valor = "Reparacion interna" });
            //opciones.Add(new OpcionesMov() { Indice = "19", valor = "En prestamo" });
            //opciones.Add(new OpcionesMov() { Indice = "10", valor = "Reparacion Externa" });
            //opciones.Add(new OpcionesMov() { Indice = "15", valor = "Asignado a tecnico" });
            //opciones.Add(new OpcionesMov() { Indice = "11", valor = "Baja por Donacion" });
            //opciones.Add(new OpcionesMov() { Indice = "12", valor = "Baja por venta interna" });
            //opciones.Add(new OpcionesMov() { Indice = "13", valor = "BAJA" });
            //opciones.Add(new OpcionesMov() { Indice = "99", valor = "Cambio de Titularidad" });
            //opciones.Add(new OpcionesMov() { Indice = "98", valor = "Cambio de puesto" });
        }

        public void MostrarOpciones()
        {
            foreach (OpcionesMov r in opciones)
                Console.WriteLine("Indice:{0}  valor:{1}", r.st_destino, r.Desc_movimiento);
        }

        public string TxtToID(string valor)
        {
            string ret="";
            foreach (OpcionesMov r in opciones)
                if (r.Desc_movimiento == valor)
                {
                    Console.WriteLine("Indice:{0}  valor:{1}", r.st_destino, r.Desc_movimiento);
                    ret= r.st_destino;
                    break;
                }
            //ret = r.st_destino;
            return ret;
        }

        //public string IDToTxt(string id)
        //{
        //    string ret;
        //    foreach (OpcionesMov r in opciones)
        //        r.
        //        Console.WriteLine("Indice:{0}  valor:{1}", r.st_destino, r.Desc_movimiento);
        //    return ret;
        //}
        public List<OpcionesMov> getOpcionesMovimiento(string stat_origen)
        {
            List<OpcionesMov> x = new List<OpcionesMov>();

            if (stat_origen != "ALL")
            {
                x = opciones.Where(m => (m.st_origen == stat_origen)).ToList();
                x.Select(o => o.st_destino).Distinct().ToList();
                x.Sort((p, q) => string.Compare(p.st_destino, q.st_destino));
                return x;
            }
            else
            {
                x = opciones;
               var distlist= x.GroupBy(o => o.st_destino).Select(group=>group.First()).ToList();
                distlist.Sort((p, q) => string.Compare(p.st_destino, q.st_destino));
                return distlist;
            }
            
        }
        
    }

    public class Movimientos
    {
        
        public string Solicitante { get; set; } // usuario que ejecuta el movimiento 

        public List<Ninventario> Inventario = new List<Ninventario>();// Nros de equipo 
        public string descripcion { get; set; } // Comentarios del movimiento

        public string UsrDestino { get; set; }  // Usuario al que se le asigna el equipo
        public string Puesto { get; set; }      // puesto al que se le asigna el equipo
        public string OS { get; set; }          // nro de pedido 
        public string statusOrigen { get; set; }// Estado origen
        public string statusDestino { get; set; }// Estado destino

        JsonStatuses js = new JsonStatuses();
        
            #region Metodos movimiento
            public List<DataMov> ListarMov(string Inv)
            {
                Movimientos mv = new Movimientos();
                jMovimientos jmv = new jMovimientos();
                List<DataMov> mb= jmv.JSONGet(Inv);
                //jmv.JSONGet(Inv);
                return mb;
            }

            public RetCode Baja(Movimientos mv)
            {
                RetCode rc = new RetCode();
            
                string baja = Microsoft.VisualBasic.Interaction.InputBox("ingrese el nro de inventario que va a dar de baja");
                if (baja != mv.Inventario[0].id)
                {
                    MessageBox.Show("el nro no coincide.\nNo se realiza la baja");
                    rc.rc = "2";
                    return rc ;
                }
                //RetCode rc = new RetCode();
                jMovimientos jmv = new jMovimientos();
                jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                jmv.statusDest = CTEMovimiento.ST_BAJA;
                jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                jmv.q = "cas";
                jmv.idUsuarioDestino = mv.UsrDestino;
                jmv.descripcion = mv.descripcion;
                //jmv.ID_PUESTO = mv.Puesto;
                jmv.idAssets = mv.Inventario.ToArray();
                jmv.FechaHasta = DateTime.Now.ToString();
                rc = jmv.JSONPost(jmv);
                return rc;
            }

            public RetCode Donacion(Movimientos mv)
            {
          
                RetCode rc = new RetCode();
                string stat = Microsoft.VisualBasic.Interaction.InputBox("ingrese el nro de inventario que va a donar");
                if (stat != mv.Inventario[0].id)
                {
                    MessageBox.Show("el nro no coincide.\nNo se realiza la donacion");
                    rc.rc = "2";
                    return rc;
                }
                jMovimientos jmv = new jMovimientos();
                jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                jmv.statusDest = CTEMovimiento.ST_BAJA_DONACION;
                jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                jmv.q = "cas";
                jmv.idUsuarioDestino = mv.UsrDestino;
                jmv.descripcion = mv.descripcion;
                //jmv.ID_PUESTO = mv.Puesto;
                jmv.idAssets = mv.Inventario.ToArray();
                jmv.FechaHasta = DateTime.Now.ToString();
                rc = jmv.JSONPost(jmv);
                return rc;
            }

            public RetCode VentaInterna(Movimientos mv)
            {
                RetCode rc = new RetCode();
                string baja = Microsoft.VisualBasic.Interaction.InputBox("ingrese el nro de inventario que va vender");
                if (baja != mv.Inventario[0].id)
                {
                    MessageBox.Show("el nro no coincide.\nNo se realiza la venta interna");
                    rc.rc = "2";
                    return rc;
                }
                jMovimientos jmv = new jMovimientos();
                jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                jmv.statusDest = CTEMovimiento.ST_BAJA_VENTA_INTERNA;
                jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                jmv.q = "cas";
                jmv.idUsuarioDestino = mv.UsrDestino;
                jmv.descripcion = mv.descripcion;
                //jmv.ID_PUESTO = mv.Puesto;
                jmv.idAssets = mv.Inventario.ToArray();
                jmv.FechaHasta = DateTime.Now.ToString();
                rc = jmv.JSONPost(jmv);
                return rc;
            }

            public RetCode RepInterna(Movimientos mv)
            {
                RetCode rc = new RetCode();
                jMovimientos jmv = new jMovimientos();
                jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                jmv.statusDest = CTEMovimiento.REPARACION_I;
            
                jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                jmv.q = "cas";
                jmv.idUsuarioDestino = mv.UsrDestino;
                jmv.descripcion = mv.descripcion;
                //jmv.ID_PUESTO = mv.Puesto;
                jmv.idAssets = mv.Inventario.ToArray();
                jmv.FechaHasta = DateTime.Now.ToString();
                rc = jmv.JSONPost(jmv);
                return rc;
            }
            public RetCode RepExterna(Movimientos mv)
            {
                RetCode rc = new RetCode();
                jMovimientos jmv = new jMovimientos();
                jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                jmv.statusDest = CTEMovimiento.REPARACION_E;
            
                jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                jmv.q = "cas";
                jmv.idUsuarioDestino = mv.UsrDestino;
                jmv.descripcion = mv.descripcion;
                //jmv.ID_PUESTO = mv.Puesto;
                jmv.idAssets = mv.Inventario.ToArray();
                jmv.FechaHasta = DateTime.Now.ToString();
                rc = jmv.JSONPost(jmv);
                return rc;
            }
            public RetCode AsignarATecnico(Movimientos mv)
            {
                RetCode rc = new RetCode();
                try
                {
                    JSONUsr xusr = new JSONUsr();
                    string UsrDest = mv.UsrDestino;
                    Usuario us = xusr.getDataxUsr(mv.UsrDestino);
                    if (us == null)
                        mv.UsrDestino = "";
                    else
                        mv.UsrDestino = us.ID;
                    List<RetCodeExt> lerr = new List<RetCodeExt>();
                    
                    jMovimientos jmv = new jMovimientos();
                    jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    jmv.statusDest = CTEMovimiento.ST_CONFTEC;

                    jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                    jmv.q = "cas";
                    jmv.idUsuarioDestino = mv.UsrDestino;
                    jmv.descripcion = mv.descripcion;
                    jmv.ID_PUESTO = mv.Puesto;
                    jmv.idAssets = mv.Inventario.ToArray();
                    jmv.FechaHasta = DateTime.Now.ToString();
                    rc = jmv.JSONPost(jmv);

                    // Aceptar equipo automaticamente
                    Confirmaciones xc = new Confirmaciones();
                    foreach (Ninventario x in mv.Inventario)
                    {

                        rc = xc.Aprobar(x.id);
                        RetCodeExt sux = new RetCodeExt();
                        sux.inv = x.id;
                        sux.rc = rc.rc;
                        sux.msg = rc.msg;
                        lerr.Add(sux);
                       
                    }
                 }
                catch (Exception ex)
                {
                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";

                }
               return rc ;
            }
            public RetCode CambioTitularidad(Movimientos mv)
            {
                RetCode rc = new RetCode();
                try
                {
                    
                    jMovimientos jmv = new jMovimientos();
                    jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    jmv.statusDest = CTEMovimiento.ST_CONFUSR;

                    jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                    jmv.q = "cas";
                    jmv.idUsuarioDestino = mv.UsrDestino;
                    jmv.descripcion = mv.descripcion;
                    //jmv.ID_PUESTO = mv.Puesto;
                    jmv.idAssets = mv.Inventario.ToArray();
                    jmv.FechaHasta = DateTime.Now.ToString();
                    rc = jmv.JSONPost(jmv);

 
                }
                catch (Exception ex)
                {
                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";

                }
                return rc;
            }
            public RetCode CambioDePuesto(Movimientos mv)
            {
                RetCode rc = new RetCode();
                try
                {
                    jMovimientos jmv = new jMovimientos();
                    jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    jmv.statusDest = CTEMovimiento.ST_ASIGNADO;

                    jmv.statusOrig = mv.statusOrigen;
                    jmv.q = "cas";

                    //jmv.idUsuarioDestino = mv.UsrDestino;
                    jmv.descripcion = mv.descripcion;
                    jmv.ID_PUESTO = mv.Puesto;
                    jmv.idAssets = mv.Inventario.ToArray();
                    jmv.FechaHasta = DateTime.Now.ToLongDateString();
                    rc = jmv.JSONPost(jmv);
                }
                catch (Exception ex)
                {
                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";

                }
                return rc;
            }

        
            public RetCode AsignarUsrFinal(Movimientos mv)
            {
                //List<string> lerr = new List<string>();
                RetCode rc = new RetCode();
                try
                {
                    jMovimientos jmv = new jMovimientos();
                    jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    jmv.statusDest = CTEMovimiento.ST_CONFUSR;

                    jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                    jmv.q = "cas";
                    jmv.idUsuarioDestino = mv.UsrDestino;
                    jmv.descripcion = mv.descripcion;
                    jmv.ID_PUESTO = mv.Puesto;
                    jmv.idAssets = mv.Inventario.ToArray();
                    jmv.FechaHasta = DateTime.Now.ToLongDateString();
                    rc = jmv.JSONPost(jmv);
                }

                catch (Exception ex)
                {
                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";

                }
                return rc;
            }
            public RetCode PasarAPanol(Movimientos mv)
            {
                RetCode rc = new RetCode();
                try
                {
                    jMovimientos jmv = new jMovimientos();
                    jmv.idAdminUser = Global.SeguridadUsr.usuario.ID; 
                    jmv.statusDest = CTEMovimiento.ST_DEPOSITO;
              
                    jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                    jmv.q = "cas";
                    jmv.idUsuarioDestino = "";
                    //jmv.descripcion = mv.descripcion;
                    jmv.ID_PUESTO = "PAN-0-000";
                    jmv.Formulario = "";
                    //jmv.statusOrig = CTEMovimiento.ST_ASIGNADO;
                    jmv.idAssets = mv.Inventario.ToArray();
                    jmv.FechaHasta = DateTime.Now.ToLongDateString();
                    rc = jmv.JSONPost(jmv);
                }
                catch(Exception ex)
                {
               
                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";
                }
                return rc;
            }

            public RetCode PasarAConfirmarPanol(Movimientos mv)
            {
                RetCode rc = new RetCode();
                try
                {
                    jMovimientos jmv = new jMovimientos();
                    jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    jmv.statusDest = CTEMovimiento.ST_CONFPAN;

                    jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                    jmv.q = "cas";
                    jmv.idUsuarioDestino = mv.UsrDestino;
                    //jmv.descripcion = mv.descripcion;
                    //jmv.ID_PUESTO = mv.Puesto;
                    jmv.Formulario = "";
                    //jmv.statusOrig = CTEMovimiento.ST_ASIGNADO;
                    jmv.idAssets = mv.Inventario.ToArray();
                    jmv.FechaHasta = DateTime.Now.ToLongDateString();
                    rc = jmv.JSONPost(jmv);
                    
                }
                catch (Exception ex)
                {

                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";
                }
            return rc;
        }

            public RetCode PasarAEnTRansito(Movimientos mv)
            {
                RetCode rc = new RetCode();
                try
                {
                    RootGyU rgyu = new RootGyU();
                //List<RetCodeExt> lerr = new List<RetCodeExt>();
                    //rgyu = rgyu.JSONget();
                    jMovimientos jmv = new jMovimientos();
                    //jmv.idUsuarioDestino=mv.UsrDestino;
                    jmv.idAdminUser = rgyu.UsuariotoID(mv.UsrDestino).ID; // mv.UsrDestino;
                    //jmv.idAdminUser = Global.SeguridadUsr.usuario.ID;
                    jmv.statusDest = CTEMovimiento.ST_TRANSITO;
                    jmv.statusOrig = js.Stat2ID(mv.statusOrigen);
                    jmv.q = "cas";
                    //jmv.idUsuarioDestino = mv.UsrDestino;
                    jmv.descripcion = mv.descripcion;
                    //jmv.ID_PUESTO = "PAN-0-000"; // mv.Puesto;
                    //jmv.Formulario = "";
                    //jmv.statusOrig = CTEMovimiento.ST_ASIGNADO;
                    jmv.idAssets = mv.Inventario.ToArray();
                    //jmv.FechaHasta = DateTime.Now.ToLongDateString();
                    rc = jmv.JSONPost(jmv);
                }
                catch(Exception ex)
                {
                    InventarioAsset.ELog.save(this, ex);
                    rc.rc = "1";
                }
            return rc;
        }
        


    #endregion
    }

    public class Confirmaciones
        {
            public string Inventario { get; set; }
            public string Solicitante { get; set; }
            public string fecha { get; set; }
            public string Estado { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string UsrDestino { get; set; }
            public string Nombre { get; set; }
            public string Puesto { get; set; }

            private List<Tokens> lt = new List<Tokens>();
            private List<Confirmaciones> lc = new List<Confirmaciones>();



            #region Metodos confirmacion

            public RetCode Rechazar(string inv)
            {
                jconfirma jc = new jconfirma();
                RetCode rcod = new RetCode();
                Tokens tk = new Tokens();
                string tkx = jc.GetTokenxInv(inv);

                EnvioConf ec = new EnvioConf();
                ec.idAdminUser = tk._sys_usr;
                ec.opcion = 3;
                ec.q = "adm-pr";
                ec.token = tkx;
                rcod = ec.JSONPost(ec);
                return rcod;
                //}

            }

            public RetCode Aprobar(string inv)
            {
                jconfirma jc = new jconfirma();
                RetCode rcod = new RetCode();
                Tokens tk = new Tokens();
                string tkx=jc.GetTokenxInv(inv);
                EnvioConf ec = new EnvioConf();
                ec.idAdminUser = Global.SeguridadUsr.usuario.ID;// tk._sys_usr;
                ec.opcion = 1;
                ec.q = "adm-pr";
                ec.token = tkx;
                rcod = ec.JSONPost(ec);
                return rcod;
            }

            // Devuelve la lista de equipos pendiente de aprobacion por usuario
            public List<Confirmaciones> ListarPendientesxUsr(string Usuario)
            {
                List<Confirmaciones> laux = new List<Confirmaciones>();
                jconfirma jc = new jconfirma();
                Rootobject ro = jc.GetJTodos();
            
                User auxUs = new User();
                Sys_Users auxsys = new Sys_Users();
                Asset auxeq = new Asset();
                JDescripciones jdes = new JDescripciones();
                Tokens t = new Tokens();

                foreach (Transconf tc in ro.coleccion.transconf)
                {
                    Confirmaciones auxi = new Confirmaciones();
                    auxi.Inventario = tc.ID_ASSET;
                   auxi.fecha = tc.FECHA;
                   auxi.Puesto = tc.PUESTO;
                   //laux.Add(auxi);
                   //if (string.IsNullOrEmpty(Usuario))
                   //{ tc.ID_USUARIO}
                   auxUs = ro.coleccion.users.Single(o => o.ID == tc.ID_USUARIO);
                   auxi.UsrDestino = auxUs.FULLNAME;
                   auxsys = ro.coleccion.sys_users.Single(o => o.ID == tc.SYS_USER);
                   auxi.Solicitante = auxsys.FULLNAME;
                   auxeq = ro.coleccion.assets.Single(o => o.ID_ASSET == tc.ID_ASSET);
                   auxi.Marca = auxeq.MARCA;
                   auxi.Modelo = auxeq.MODELO;
                   auxi.Nombre = jdes.DescripcionxID(auxeq.TIPO_ASSET);
                    auxi.Estado = auxeq.STATUS_DET;
                   laux.Add(auxi);
                   t._inv = tc.ID_ASSET;
                   t._token = tc.TOKEN;
                   t._sys_usr = tc.SYS_USER;
                   lt.Add(t);
                }

                return laux;
            }
        #endregion

        }

}
