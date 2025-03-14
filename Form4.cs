using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.DirectoryServices;  //Hay que añadirlo en References
using System.DirectoryServices.AccountManagement;

namespace winLdapAv
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 //           listBox1 = List();
 //        }  

 //       public List<string> ConsultarUsuarios()
 //       {
            DirectoryEntry myLdapConnection = createDirectoryEntry();
        //    List<string> d = new List<string>();
            using (var buscadorDirectorio = new DirectorySearcher(myLdapConnection, "(&(objectClass=user)(objectCategory=person)(cn=p*))"))
            {
                try
                {
                    SearchResult result;
                    SearchResultCollection iResult = buscadorDirectorio.FindAll();
                    if (iResult != null)
                    {
                        for (int counter = 0; counter < iResult.Count; counter++)
                        {
                            result = iResult[counter];
                            if (result.Properties.Contains("samaccountname"))
                            { 
                                listBox1.Items.Add((String)result.Properties["samaccountname"][0]);

                            }
                            if (result.Properties.Contains("userAccountControl"))
                            { 
                                listBox1.Items.Add(string.Concat("Estado: ", result.Properties["userAccountControl"][0].ToString()));
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("Error: ID = " + ex.GetHashCode() + " || Mensaje = " + ex.Message.ToString());
                }
            }
            myLdapConnection.Dispose();
            
        }

        private static DirectoryEntry createDirectoryEntry()
        {
            DirectoryEntry ldapConnection = new DirectoryEntry("directorio.local");
            ldapConnection.Path = "LDAP://mgapps.metrogas.com.ar";
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            return ldapConnection;
        }
    }
}
