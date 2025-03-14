using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using CAD_Inv;

namespace InventarioAsset
{
    public partial class frmlogin : Form
    {
       // private bool accesoOK;

        public bool accesoOK { get; set; }
        int ingresoErroneo = 0;
        public frmlogin()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            //DataLogin m = new DataLogin();
            Global.m.user = txtUsuario.Text;
            Global.m.password = txtpasswd.Text;
            User_Sec resultado = new User_Sec(Global.urlBase + "login.php");
            //JSONuserSec DataSeg = new JSONuserSec();
            Global.SeguridadUsr= new JSONuserSec();

            Global.SeguridadUsr = resultado.JSONpost(Global.m);
            Console.WriteLine(Global.m.password);
            Console.WriteLine(Global.m.user);
            if (resultado.LoginOk())
            {
                
                accesoOK = true;
                this.Close();
            }
            else
            {
                ingresoErroneo++;
                MessageBox.Show("Ingreso erroneo\nIntento:"+ingresoErroneo);
                txtpasswd.Text = "";
                accesoOK = false;
            }
            if (ingresoErroneo > 3)
            {
                MessageBox.Show("Demasiados intentos fallidos\nEl programa se cerrará");
            }
            
        }

        private void txtpasswd_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmdLogin.PerformClick();
            }
                 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpasswd_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmlogin_Load(object sender, EventArgs e)
        {

        }
    }
}
