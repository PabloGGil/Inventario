using System;
using System.Diagnostics;
using System.IO;

namespace InventarioAsset
{
    public class ELog
    {
        public ELog()
        {
            bool exists = System.IO.Directory.Exists(Global.PathLog);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Global.PathLog);
            }
        
        }
        public static void save(string texto)
        {
            string fecha = System.DateTime.Now.ToString("yyyyMMdd");
            string hora = System.DateTime.Now.ToString("HH:mm:ss");
            string path = Global.PathLog + fecha + ".log";
            bool exists = System.IO.Directory.Exists(Global.PathLog);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Global.PathLog);
            }

            StreamWriter sw = new StreamWriter(path, true);

            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(hora + " --- Error de datos ---");
            sw.WriteLine(texto);
            sw.WriteLine("");

            sw.Flush();
            sw.Close();
        }

            public static void save(Object obj, Exception ex)
        {

            string fecha = System.DateTime.Now.ToString("yyyyMMdd");
            string hora = System.DateTime.Now.ToString("HH:mm:ss");
            string path = Global.PathLog + fecha + ".log";

            bool exists = System.IO.Directory.Exists(Global.PathLog);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Global.PathLog);
            }

            StreamWriter sw = new StreamWriter(path, true);

            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(obj.GetType().FullName + " " + hora);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - \nMensaje:" + ex.Message+"\nSource:"+ex.Source+"\nStack Trace:"+ex.StackTrace+"\nTarget Site"+ex.TargetSite);
            sw.WriteLine("");

            sw.Flush();
            sw.Close();
        }
    }
}
