using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioAsset
{
    public class Eq_DatosCompra
    {
        public string OC { get; set; }      //desde joocc
        public string APEM { get; set; }    //desde joocc
        public string SOLP { get; set; }    //desde joocc
        public string FACTURA { get; set; } //desde joocc
        public string FECHA_OC { get; set; }   //desde joocc FECHA_OC <-- FECHA
        public string REMITO { get; set; }  //desde jremito
        public string PROVEEDOR { get; set; } //desde jremito
        public string FECHA_REMITO { get; set; }   //desde jremito FECHA_REMITO <-- FECHA
        //public Equipo x = new Equipo();
        
        public List<Eq_DatosCompra> getDataList(List<string> inv)
        {
            jOOCC oc = new jOOCC();
            jRemito remj = new jRemito();
            List<EquipoExt> eqaux = new List<EquipoExt>();
            Eq_DatosCompra xdata = new Eq_DatosCompra();
            eqaux = Global.TodosLosAsset.GetListaEquipos(inv);
            List<Eq_DatosCompra> data = new List<Eq_DatosCompra>();
            foreach (EquipoExt aux in eqaux)
            {
                Remito rem = new Remito();
                rem=remj.remitobyID(aux.ID_REMITO);
                if (rem.ID == null)
                {
                    xdata.APEM = null;
                    xdata.FACTURA = null;
                    xdata.SOLP = null;
                    xdata.REMITO = null;
                    xdata.PROVEEDOR = null;
                    xdata.OC = null;
                }
                else
                {
                    //oc = oc.JSONget(rem.coleccion[0].ID_OOCC);
                    OOCC ocx = new OOCC();
                    
                    oc = oc.JSONget(rem.ID_remito);
                    ocx = oc.ooccget(oc.coleccion[0].id_oocc);
                    if (oc.coleccion.Count() > 0)
                    {
                        xdata.APEM = ocx.APEM;
                        xdata.FACTURA = ocx.FACTURA;
                        xdata.SOLP = ocx.SOLP;
                        xdata.REMITO = rem.REMITO;
                        xdata.PROVEEDOR = rem.PROVEEDOR;
                        xdata.OC = ocx.OC;
                    }

                    
                }
                data.Add(xdata);
            }

            return data;
        }
        public Eq_DatosCompra getData(string inv)
        {
            jOOCC oc = new jOOCC();
            jRemito rem = new jRemito();
            List<string> invs = new List<string>();
            invs.Add(inv);
            List<EquipoExt> eqaux = new List<EquipoExt>();
            Eq_DatosCompra xdata = new Eq_DatosCompra();
            eqaux = Global.TodosLosAsset.GetListaEquipos(invs);
            List<Eq_DatosCompra> data = new List<Eq_DatosCompra>();
            foreach (EquipoExt aux in eqaux)
            {

                rem = rem.JSONget(aux.ID_REMITO);
                if (rem.coleccion.Count() ==0 )
                {
                    xdata.APEM = null;
                    xdata.FACTURA = null;
                    xdata.SOLP = null;
                    xdata.REMITO = null;
                    xdata.PROVEEDOR = null;
                    xdata.OC= null;

                }
                else
                {
                    oc = oc.JSONget(rem.coleccion[0].ID_OOCC);
                    if (oc.coleccion.Count() > 0)
                    {
                        xdata.APEM = oc.coleccion[0].APEM;
                        xdata.FACTURA = oc.coleccion[0].FACTURA;
                        xdata.SOLP = oc.coleccion[0].SOLP;
                        xdata.REMITO = rem.coleccion[0].REMITO;
                        xdata.PROVEEDOR = rem.coleccion[0].PROVEEDOR;
                        xdata.OC = oc.coleccion[0].OC;
                    }
                }
               // data.Add(xdata);
            }

            return xdata;
        }
    }
}
