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
    public partial class ListaRevisado : Form
    {
        public ListaRevisado()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IO RepEstado = new IO();
            List<EquipoExt> reporteRevisados = new List<EquipoExt>();
            reporteRevisados = Global.TodosLosAsset.getEquiposRevisados();
            RepEstado.exportar(reporteRevisados, "Reporte_Encontrados_" + DateTime.Now.ToString("ddMMyyyy"));
        }

        private void ListaRevisado_Load(object sender, EventArgs e)
        {

        }
    }
}
