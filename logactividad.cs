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
    public partial class logactividad : Form
    {
        public logactividad()
        {
            InitializeComponent();
        }

        private void logactividad_Load(object sender, EventArgs e)
        {
            Logs log = new Logs();
            List<Logs> xx = new List<Logs>();
            
            //dtdesde.Format = DateTimePickerFormat.Custom;
            //dtdesde.CustomFormat = "MMddyyyy";
            dthasta.Format= DateTimePickerFormat.Short;
            dtdesde.Format = DateTimePickerFormat.Short;
            Console.WriteLine(dtdesde.Value.ToString("dd/MM/yyyy"));
            Console.WriteLine(dtdesde.Value.Month);

            string FechaDesde = dtdesde.Value.ToString("yyyyMMdd");
            string FechaHasta= dthasta.Value.ToString("yyyyMMdd");
            xx =log.GetLog(FechaDesde, FechaHasta);
            log.TAREA = "tarea 1";
            
            //xx.Add(log);
            dgv.DataSource = xx.ToList();
            label1.Text = dgv.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logs log = new Logs();
            List<Logs> xx = new List<Logs>();
            string FechaDesde = dtdesde.Value.ToString("yyyyMMdd");
            string FechaHasta = dthasta.Value.ToString("yyyyMMdd");
            xx = log.GetLog(FechaDesde, FechaHasta);
            log.TAREA = "tarea 1";

            dgv.DataSource = xx.ToList();
            label1.Text = dgv.Rows.Count.ToString();
        }
    }
}
