using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
//using CAD_Inv;

namespace InventarioAsset
{
    class BaseDatos
    {

        public static OleDbConnection Conectar()
        {
            try
            {
                //OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\\\mgiap030\\usmt\\PGIL\\Instalador GU\\inventario.mdb; Persist Security Info = True; Jet OLEDB:Database Password = dylan01");

                // Proveedor de 32 bits
                //OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\users\\pgil\\Mis_Programas\\visual basic\\Inventario\\Inventario.mdb;Persist Security Info=True;Jet OLEDB:Database Password=dylan01");
                // Base productiva
                OleDbConnection cn = new OleDbConnection(Global.strConnBaseAccess);

                // proveedor de 64 bits
                //OleDbConnection cn = new OleDbConnection("Provider=Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\\users\\pgil\\Mis_Programas\\visual basic\\Inventario\\Inventario.accdb;Persist Security Info=True;Jet OLEDB:Database Password=dylan01");

                //OleDbConnection cn = new OleDbConnection("Provider = MSDASQL.1; Persist Security Info = False; Data Source = 12");
                //OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=S:\\TI\\TECNOLOGIA\\Microinf\\Inventario\\Base de inventario\\Inventario.mdb;Persist Security Info=True;Jet OLEDB:Database Password=dylan01");
                cn.Open();
                return cn;
            }
            catch
            {
                throw;

            }
        }
        public static SqlConnection ConectarSQL()
        {
            try
            {
                //SqlConnection cn = new SqlConnection("Data Source=mgddb011\\G4I_INENTAT1,1898;Initial Catalog=INVENTAT1;Integrated Security=True");
                SqlConnection cn = new SqlConnection(Global.strConnBaseSQL);
                cn.Open();
                return cn;
            }
            catch
            {
                throw;

            }
        }


        //public static OracleConnection ConectarOra()
        //{
        //    try
        //    {

        //        //const string connectionString = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = invP1)(PORT = 1943)))(CONNECT_DATA =(SERVICE_NAME = INVP1.metrogas.com.ar))); User Id=pgil;Password=metrogas2022;";
        //        //string connectionstring = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        //        //    try
        //        //    {
        //        OracleConnection cn = new OracleConnection(Global.strConnBaseOracle);
        //        cn.Open();
        //        return cn;
        //    }
        //    catch
        //    {
        //        // MessageBox.Show(ex.Message);
        //        throw;

        //    }
        //}

        //public static DataTable RegistrosTablaOra(OracleConnection cn, string qry)
        //{
        //    try
        //    {
        //        using (OracleDataAdapter da = new OracleDataAdapter(qry, cn))
        //        {
        //            DataSet ds = new DataSet();
        //            DataTable dt = new DataTable("Tabla");
        //            da.Fill(dt);
        //            return dt;
        //        }
        //    }
        //    catch
        //    {
        //        throw;

        //    }
        //}
        //public static DataRowCollection Registros(OleDbConnection cn, string qry)
        //{

        //    OleDbDataAdapter da = new OleDbDataAdapter(qry, cn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "maquina");
        //    return ds.Tables[0].Rows;
        //}

        public static DataTable RegistrosTabla(OleDbConnection cn, string qry)
        {
            try
            {
                using (OleDbDataAdapter da = new OleDbDataAdapter(qry, cn))
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable("Tabla");
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;

            }
        }
        public static DataTable RegistrosTablaSQL(SqlConnection cn, string qry)
        {

            try
            {


                using (SqlDataAdapter da = new SqlDataAdapter(qry, cn))
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable("Tabla");
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;

            }
        }


    }
}
