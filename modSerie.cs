using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioAsset
{
    public partial class modSerie : Form
    {
        Equipo cambiado = new Equipo();
        Equipo original = new Equipo();

        public modSerie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvAsset.DataSource = null;
            List<string> xs = new List<string>();
            dgvAsset.AllowUserToOrderColumns = true;
            //bool sortAscending = true;
            if (txtinv.Text != "")
            {
                xs.Add(txtinv.Text);
                List<EquipoExt> lst = Global.TodosLosAsset.GetListaEquipos(xs);
                dgvAsset.DataSource = lst;
                original.ID_Inv = txtinv.Text;
                original.SERIE = dgvAsset.Rows[0].Cells["SERIE"].Value.ToString();
                cambiado.ID_Inv = txtinv.Text;
            }
            //original.SERIE = dgvAsset.Rows[0].Cells["SERIE"].Value.ToString();
        }

        private void modSerie_Load(object sender, EventArgs e)
        {
            dgvAsset.AllowUserToOrderColumns = true;
            //dgvAsset.Columns["SERIE"]

        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            string serie_new = dgvAsset.Rows[0].Cells["SERIE"].Value.ToString();
            Console.WriteLine(dgvAsset.Rows[0].Cells["SERIE"].Value);
            
            if (serie_new!="")
            {
                
                cambiado.SERIE = serie_new;
                original.modserie(cambiado, original);

                DateTime FechaToma = DateTime.Now.Date;
                UpdateNota NotaNueva = new UpdateNota();
                NotaNueva.q = "addNotaAsset";
                NotaNueva.IdAsset = int.Parse(original.ID_Inv);

                NotaNueva.idAdminUser = Global.SeguridadUsr.usuario.ID;
                NotaNueva.nota = "Cambio de nro de serie : " + FechaToma.ToString("yyyy-MM-dd")+ " valor anterior: "+ original.SERIE;
                string url = Global.urlBase + "/ajaxEquipos.php";
                WebService.PostData<UpdateNota>(url, NotaNueva);

            }

        }
    }
}
