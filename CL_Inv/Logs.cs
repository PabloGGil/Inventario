using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioAsset
{
    public class Logs
	{
		public string FECHA { get; set; }
        public string UsuarioSist { get; set; }
        public string Usuario { get; set; }
        public string TAREA { get; set; }
        public string PROCESO { get; set; }
        public string ESTADO { get; set; }
        public string OBSERVACION { get; set; }
        //RootGyU auz;

        public List<Logs> GetLog(string fechaD,string fechaH)
        {
            RootGyU rgyu = new RootGyU();
			List<Logs> ll = new List<Logs>();
			Rootlog rl = new Rootlog();
            List<jLog> jl = new List<jLog>();
            rgyu=rgyu.JSONget();
			rl = rl.JSONget("fd="+fechaD + "&fh=" + fechaH);
			jl = rl.coleccion.ToList();

			ll=jl.ConvertAll(new Converter<jLog, Logs>(CnvJlog2Log)).ToList();
            ll=ll.OrderBy(o => o.FECHA).ToList();
            return ll;
        }

        public void ToString(Logs aux)
        {
            Console.WriteLine("Fecha             : {0}", aux.FECHA.ToString());
            Console.WriteLine("Usuario de sistema: {0}", aux.UsuarioSist);
            Console.WriteLine("Usuario           : {0}", aux.Usuario);
            Console.WriteLine("tarea             : {0}", aux.TAREA);
            Console.WriteLine("Proceso           : {0}", aux.PROCESO);
            Console.WriteLine("Estado            : {0}", aux.ESTADO);
            Console.WriteLine("Observacion       : {0}", aux.OBSERVACION);

        }
       
        public static Logs CnvJlog2Log(jLog pf)
        {
            RootGyU zz = new RootGyU();
            Logs x = new Logs();
            x.ESTADO = pf.ESTADO;
            x.FECHA = pf.FECHA;
            x.Usuario = pf.ID_USER;
            x.TAREA = pf.TAREA;
            x.ESTADO = pf.ESTADO;
            // x.TIPO_ASSET = pf.TIPO_ASSET;
            x.PROCESO = pf.PROCESO;
            x.OBSERVACION = pf.OBSERVACION;
            //zz = auz.IDtoUsuario(pf.LOGIDUSER);
            x.UsuarioSist = zz.IDtoUsuario(pf.LOGIDUSER).FULLNAME;
            
            return x;
        }
    }
}
