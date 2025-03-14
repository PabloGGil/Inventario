// #define DRAW_HOTSPOTS

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InventarioAsset
{
    public partial class Mapa : Form
    {

        public Mapa()
        {
            InitializeComponent();
        }
        Boolean ModoEdicion = false;
        // The map.
        private Bitmap Map;
        Graphics gr;
        
        //string Usuario = null;
        SqlConnection cns = new SqlConnection();
        DataTable dt = new DataTable();
        string mapaSel;
        // The hotspots.
        private List<Rectangle> Hotspots = new List<Rectangle>();

        // The current scale.
        private float MapScale;

        private Dictionary<string, Rectangle> puestos = new Dictionary<string, Rectangle>();
        // Exit.


        // Prepare the map for first viewing.
        private void Mapa_Load(object sender, EventArgs e)
        {

            try
            {
                string Usuario = Global.SeguridadUsr.usuario.USER_ID;
                //string grupo = MDIParent1.Grupo(Usuario);
                //grupo = "Soporte";
                //if (grupo == "Admin")
                //{
                //    dataToolStripMenuItem.Visible = true;
                //    textBox1.Visible = true;
                //    ModoEdicion = true;
                //}
                //else
                //{
                //    dataToolStripMenuItem.Visible = false;
                //    textBox1.Visible = false;
                //    ModoEdicion = false;
                //}
                ModoEdicion = ModoEdicion && editarToolStripMenuItem.Checked;
                // Cargar Combo con ubicaciones
                cns = InventarioAsset.BaseDatos.ConectarSQL();
                string cmbquery = "select descripcion,archivo from inventat1.dbo.mapas  order by descripcion";
                dt = InventarioAsset.BaseDatos.RegistrosTablaSQL(cns, cmbquery);
                cmbUbic.DataSource = dt;
                cmbUbic.DisplayMember = "descripcion";
                cmbUbic.ValueMember = "Archivo";
                cmbUbic.SelectedValue = "omb1P";
                cns.Close();
                mapaSel = Global.PathRes + cmbUbic.SelectedValue.ToString();
                CargarMapas(mapaSel);

            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                Cursor.Current = Cursors.Default;
            }
        }

        public void CargarMapas(string ruta)
        {
            //ruta contiene la ruta y el nombre del archivo que tiene la imagen y los hotspot.
            // Para la imagen habrá que agregarle el .png y al hotspot el .txt
            try
            {


                string Imagen = ruta + ".png";

                Map = new Bitmap(Imagen, true);
                CargarHotspot(ruta + ".txt");
                puestos.Count();
                CargarMapa(ruta + ".png");

                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(128, 0, 255, 0));



                gr = Graphics.FromImage(Map);
                foreach (KeyValuePair<string, Rectangle> puesto in puestos)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", puesto.Key, puesto.Value);
                    gr.FillRectangle(semiTransBrush, puesto.Value);
                }
                //gr.FillRectangle(semiTransBrush, puestos["OMB-S-001"]);

                picMap.SizeMode = PictureBoxSizeMode.Zoom;
                picMap.Image = Map;


                // Start at small scale.
                SetMapScale(mnuScale2);
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
            }
        }
        public void CargarHotspot(string Archivo)
        {
            Console.WriteLine(Archivo);
            if (File.Exists(Archivo) == false)
            {

                MessageBox.Show("No existe el archivo " + Archivo, "Error");
                //this.Close();
            }

            //borrar todos los Hotspot del diccionario
            try
            {
                EliminarAllHotspot();

                string linea;
                StreamReader sr = new StreamReader(Archivo);

                string[] rx;
                linea = sr.ReadLine();
                //Continue to read until you reach end of file
                while (linea != null)
                {
                    rx = linea.Split(',');
                    puestos.Add(rx[0], new Rectangle(Int32.Parse(rx[1]), Int32.Parse(rx[2]), Int32.Parse(rx[3]), Int32.Parse(rx[4])));
                    linea = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                Cursor.Current = Cursors.Default;
            }
        }

        public void CargarMapa(string Archivo)
        {
            Console.WriteLine(@Archivo);
            if (!File.Exists(Archivo))
            {
                MessageBox.Show("No existe el archivo " + Archivo, "Error");
                //InventarioAsset.ELog.save(this, "No existe el archivo " + Archivo + "Error");
                // this.Close();
            }
            try
            {

                picMap.SizeMode = PictureBoxSizeMode.Zoom;

                picMap.Image = Image.FromFile(Archivo);
                SetMapScale(mnuScale2);
            }
            catch (Exception ex)
            {
                InventarioAsset.ELog.save(this, ex);
                Cursor.Current = Cursors.Default;
            }
        }
        // Scale the map.
        private void mnuScaleMap_Click(object sender, EventArgs e)
        {
            SetMapScale(sender as ToolStripMenuItem);

        }

        private void SetMapScale(ToolStripMenuItem checked_item)
        {
            // Select the correct menu item.
            foreach (ToolStripMenuItem item in
                scaleToolStripMenuItem.DropDownItems)
                item.Checked = (item == checked_item);

            // Scale the map.
            MapScale = float.Parse(checked_item.Tag.ToString());
            picMap.Size = new Size(
                (int)(Map.Width * MapScale),
                (int)(Map.Height * MapScale));
        }

        // See if we're over a hotspot.
        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            // See if we're over a hotspot.
            if (UbicacionAtPoint(e.Location) != null)
                picMap.Cursor = Cursors.Hand;
            else
                picMap.Cursor = Cursors.Default;
        }

        // See if we clicked a hotspot.
        private void picMap_MouseClick(object sender, MouseEventArgs e)
        {
            string xi = UbicacionAtPoint(e.Location);
            //ModoEdicion = false;
            switch (e.Button)
            {

                case MouseButtons.Left:
                    if (ModoEdicion)
                    {
                        DialogResult d;
                        string PuestoNuevo;
                        d = MessageBox.Show("Cambia el nombre del puesto?\n" + xi, "", MessageBoxButtons.YesNoCancel);
                        if (d == DialogResult.Yes)
                        {
                            PuestoNuevo = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo nombre");
                            //Verificar que el nombre del puesto no exista
                            if (puestos.ContainsKey(PuestoNuevo.ToUpper()))
                            {
                                MessageBox.Show("el puesto" + PuestoNuevo + "existe");
                                SolidBrush blueBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));


                                gr.FillRectangle(blueBrush, (Rectangle)puestos[PuestoNuevo]);
                                picMap.Refresh();
                            }
                            else
                            {
                                ActualizarPuesto(PuestoNuevo, xi, mapaSel + ".txt");
                            }
                        }

                    }
                    else
                    {
                        if (xi != null)
                        {
                            DataTable dt = new DataTable();
                            OleDbConnection cn = new OleDbConnection();
                            cn = InventarioAsset.BaseDatos.Conectar();
                            string qry = "Select D.idmaquina,M.Descr from detalle D,Maquina M where D.idmaquina=M.inv and D.idpuesto='" + xi + "'";
                            dt = InventarioAsset.BaseDatos.RegistrosTabla(cn, qry);
                            string texto = xi;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                texto = texto + "\n" + dt.Rows[i]["idmaquina"].ToString() + "\t" + dt.Rows[i]["Descr"].ToString();
                            }
                            MessageBox.Show(texto);
                            cn.Close();
                        }
                    }
                    break;

                case MouseButtons.Right:
                    // Right click
                    if (ModoEdicion)
                    {
                        picMap.BorderStyle = BorderStyle.FixedSingle;
                        picMap.MouseMove -= picMap_MouseMove;
                        picMap.MouseClick -= picMap_MouseClick;
                        picMap.MouseDown += makeHotspot_MouseDown;
                        picMap.Cursor = Cursors.Cross;
                    }
                    break;
        }
    }

        

        private string UbicacionAtPoint(Point mouse_point)
        {
            // Adjust for the current map scale.
            mouse_point = new Point(
                (int)(mouse_point.X / MapScale),
                (int)(mouse_point.Y / MapScale));

           

            foreach (KeyValuePair<string, Rectangle> puesto in puestos)

                if (puesto.Value.Contains(mouse_point)) return puesto.Key;


            //// We didn't find a hotspot that contains the point.
            return null;
        }
       

        // Begin defining a hotspot.
        private Point HotspotStart, HotspotEnd;
        private Bitmap HotspotBm;
        private Graphics HotspotGr;
        private void makeHotspot_MouseDown(object sender, MouseEventArgs e)
        {
            HotspotStart = e.Location;
            picMap.MouseDown -= makeHotspot_MouseDown;
            picMap.MouseMove += makeHotspot_MouseMove;
            picMap.MouseUp += makeHotspot_MouseUp;

            // Get ready to draw a selection rectangle.
            HotspotBm = (Bitmap)Map.Clone();
            HotspotGr = Graphics.FromImage(HotspotBm);
            picMap.Image = HotspotBm;
        }

        // Draw a selection rectangle.
        private void makeHotspot_MouseMove(object sender, MouseEventArgs e)
        {
            // Save the new point.
            HotspotEnd = e.Location;

            // Draw the selection rectangle.
            HotspotGr.DrawImage(Map, 0, 0);
            float x = Math.Min(HotspotStart.X, HotspotEnd.X) * MapScale;
            float y = Math.Min(HotspotStart.Y, HotspotEnd.Y) * MapScale;
            float wid = Math.Abs(HotspotStart.X - HotspotEnd.X) * MapScale;
            float hgt = Math.Abs(HotspotStart.Y - HotspotEnd.Y) * MapScale;
            using (Pen thin_pen = new Pen(Color.Red, 1 * MapScale))
            {
                thin_pen.DashStyle = DashStyle.Dash;
                HotspotGr.DrawRectangle(thin_pen, x, y, wid, hgt);
            }

            picMap.Refresh();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void ActualizarPuesto(string Nuevo, string Anterior,string Archivo)
        {
            string linea;

            string fecha = System.DateTime.Now.ToString("yyyyMMdd");
            //abro los archivos
            StreamReader sr = new StreamReader(Global.PathRes+Archivo+".txt");
            StreamWriter sw1 = new StreamWriter(Global.PathRes + "tmp.txt");
            //Resguardo el original
            System.IO.File.Copy(Global.PathRes + Archivo + ".txt", Global.PathRes + Archivo + fecha + ".txt", true);
            //Cierro los archivos

            string[] rx;
            //Read the first line of text
            linea = sr.ReadLine();
            //Continue to read until you reach end of file
            while (linea != null)
            {

                rx = linea.Split(',');
                if (rx[0].CompareTo(Anterior) == 0)
                {
                    string nuevalinea = Nuevo + "," + rx[1] + "," + rx[2] + "," + rx[3] + "," + rx[4];
                    sw1.WriteLine(nuevalinea);
                }
                else
                {
                    sw1.WriteLine(linea);
                }
                linea = sr.ReadLine();
            }
            //close the file
            sr.Close();
            sw1.Close();
            System.IO.File.Delete(Global.PathRes + Archivo + ".txt");
            System.IO.File.Move(Global.PathRes +"tmp.txt", Global.PathRes +Archivo+".txt");



            //Recargo hotspot
            CargarHotspot("c");
        }

        public void EliminarAllHotspot()
        {
            foreach (KeyValuePair<string, Rectangle> k in puestos.ToList())
            {
                puestos.Remove(k.Key);
            }
        }
        
       

        private void cambiarNombrePuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (cambiarNombrePuestoToolStripMenuItem.Checked)
            {
                cambiarNombrePuestoToolStripMenuItem.Checked = false;
                ModoEdicion = false;
            }
            else
            {
                cambiarNombrePuestoToolStripMenuItem.Checked = true;
                ModoEdicion = true;
            }
        }

        public void BuscarPuesto(string ubicacion)
        {

            foreach (KeyValuePair<string, Rectangle> puesto in puestos)
            {
                Console.WriteLine(puesto.Value);
                if (puesto.Key.Contains(ubicacion))
                {


                    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
                    //using (Graphics gr = Graphics.FromImage(Map))

                    //{
                    gr = Graphics.FromImage(Map);
                    //foreach (Rectangle hotspot in Hotspots)

                    gr.FillRectangle(semiTransBrush, puesto.Value);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mapaSel = Global.PathRes + cmbUbic.SelectedValue.ToString();
            CargarMapas(mapaSel);
            //CargarHotspot(Global.PathRes + cmbUbic.SelectedValue.ToString() + ".txt");
        }

        private void buscarPuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string PuestoaBuscar = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el Puesto");
            BuscarPuesto(PuestoaBuscar);
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModoEdicion = editarToolStripMenuItem.Checked;
        }

        private void makeHotspot_MouseUp(object sender, MouseEventArgs e)
        {
            // End hotspot definition mode.
            picMap.MouseMove -= makeHotspot_MouseMove;
            picMap.MouseUp -= makeHotspot_MouseUp;
            picMap.MouseMove += picMap_MouseMove;
            picMap.MouseClick += picMap_MouseClick;
            picMap.Image = Map;
            picMap.Cursor = Cursors.Default;
            picMap.Refresh();

            // Display the hotspot definition.
            float x = Math.Min(HotspotStart.X, HotspotEnd.X) / MapScale;
            float y = Math.Min(HotspotStart.Y, HotspotEnd.Y) / MapScale;
            float wid = Math.Abs(HotspotStart.X - HotspotEnd.X) / MapScale;
            float hgt = Math.Abs(HotspotStart.Y - HotspotEnd.Y) / MapScale;

            if (ModoEdicion)
            {
                string linea1 = textBox1.Text +"-*," + (int)x + "," + (int)y + "," + (int)wid + "," + (int)hgt;

               
                File.AppendAllText(mapaSel +".txt", linea1 + Environment.NewLine);
               
                // Create rectangle.
                Rectangle rect = new Rectangle((int)x, (int)y, (int)wid, (int)hgt);
                gr.FillRectangle(Brushes.LightGoldenrodYellow, rect);
            }
        }


    }
}
