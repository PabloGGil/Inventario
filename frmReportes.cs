using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace InventarioAsset
{
    public partial class frmReportes : Form
    {
        List<Etiqueta> let = new List<Etiqueta>();
        public frmReportes()
        {
            InitializeComponent();
        }
        public void DataSource(List<Etiqueta> et)
        {
            let = et;
        }
        private void frmReportes_Load(object sender, EventArgs e)
        {
            CrystalReport2 cr = new CrystalReport2();

            
            
            cr.SetDataSource(let);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
