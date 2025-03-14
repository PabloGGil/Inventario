using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioAsset.CL_Inv
{
    public class OC_INV
    {

        public string OC { get; set; }      //desde joocc
        public string inv { get; set; }    //desde joocc
        public string marca { get; set; }    //desde joocc
        public string modelo { get; set; } //desde joocc
        public string descripcion { get; set; }   //desde joocc FECHA_OC <-- FECHA
        public string serie { get; set; }  //desde jremito
       

        public List<OC_INV> getData( string oc)
        {
            jocinv ocl = new jocinv();
            ocl=ocl.jget(oc);
            List<OC_INV> data = new List<OC_INV>();

            foreach (toringa aux in ocl.coleccion)
            {
                OC_INV xdata = new OC_INV();
                xdata.OC = aux.OC;
                xdata.inv = aux.ID_ASSET;
                xdata.marca = aux.MARCA;
                xdata.modelo = aux.MODELO;
                xdata.serie = aux.SERIE;
                xdata.descripcion = aux.DESCRIPCION;
                data.Add(xdata);
            }

            return data;
        }
    }
}
