using System;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace InventarioAsset
{
    public class AD
    {
        #region "DirectoryServices"

        public DirectoryEntry createDirectoryEntry(String path, String user, String pass)
        {
            DirectoryEntry ldapConnection = new DirectoryEntry(path, user, pass, AuthenticationTypes.FastBind);

            return ldapConnection;
        }

        public DirectoryEntry GetDirectoryObject(string path, string user, string pass, ref string cErr)
        {
            cErr = "";
            DirectoryEntry de;

            try
            {
                de = new DirectoryEntry(path, user, pass, AuthenticationTypes.Secure);
            }
            catch (Exception ex)
            {
                cErr = ex.Message;
                de = null;
            }
            return de;
        }

        public DirectoryEntry GetUser(string path, string admUser, string admPass, string UserName, ref string cErr)
        {
            cErr = "";
            DirectoryEntry de = null;

            try
            {
                de = GetDirectoryObject(path, admUser, admPass, ref cErr);
                DirectorySearcher deSearch = new DirectorySearcher();
                deSearch.SearchRoot = de;

                deSearch.Filter = "(&(objectClass=user)(SAMAccountName=" + UserName + "))";
                deSearch.SearchScope = SearchScope.Subtree;
                SearchResult results = deSearch.FindOne();

                if (results != null)
                    de = new DirectoryEntry(results.Path, admUser, admPass, AuthenticationTypes.Secure);
                else
                    de = null;
            }
            catch (Exception ex)
            {
                cErr = ex.Message;
                de = null;
            }
            return de;
        }

        #endregion

        #region "AccountManagement"

        #region "Usuarios"

        public static string IDUsuario()
        {
            string username;
            try
            {
                WindowsIdentity identidad = WindowsIdentity.GetCurrent();
                 username = identidad.Name;
            }
            catch
            {
                username = null;
                throw;
            }
            return username;
        }
        public PrincipalSearchResult<Principal> buscaUsu(UserPrincipal usu)
        {
            try
            {
                PrincipalSearcher buscaCta = new PrincipalSearcher();           //Para ejecutar la consulta 
                buscaCta.QueryFilter = usu;                                     //Debemos indicar el filtro
                PrincipalSearchResult<Principal> results = buscaCta.FindAll();  //Lanzamos la consulta

                return results; //Devolvemos el resultado
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                return null;
            }
        }



        #endregion

        #region "Grupos"

        public PrincipalSearchResult<Principal> buscaGrp(GroupPrincipal grp)
        {
            try
            {
                PrincipalSearcher buscaGrp = new PrincipalSearcher();           //Para ejecutar la consulta 
                buscaGrp.QueryFilter = grp;                                     //Debemos indicar el filtro
                PrincipalSearchResult<Principal> results = buscaGrp.FindAll();  //Lanzamos la consulta

                return results; //Devolvemos el resultado
            }
            catch
            {
                throw;
                //return null;
            }
        }



        #endregion

        #endregion

    }

}
