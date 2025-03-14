namespace InventarioAsset
{
    partial class Mapa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuScale2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScale1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMakeHotspot = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarNombrePuestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarPuestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panMap = new System.Windows.Forms.Panel();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cmbUbic = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuScale2
            // 
            this.mnuScale2.Name = "mnuScale2";
            this.mnuScale2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.mnuScale2.Size = new System.Drawing.Size(233, 34);
            this.mnuScale2.Tag = "2";
            this.mnuScale2.Text = "x2";
            this.mnuScale2.Click += new System.EventHandler(this.mnuScaleMap_Click);
            // 
            // mnuScale1
            // 
            this.mnuScale1.Name = "mnuScale1";
            this.mnuScale1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuScale1.Size = new System.Drawing.Size(233, 34);
            this.mnuScale1.Tag = "1";
            this.mnuScale1.Text = "&Normal";
            this.mnuScale1.Click += new System.EventHandler(this.mnuScaleMap_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMakeHotspot,
            this.cambiarNombrePuestoToolStripMenuItem,
            this.buscarPuestoToolStripMenuItem,
            this.editarToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(81, 30);
            this.dataToolStripMenuItem.Text = "&Mapas";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // mnuMakeHotspot
            // 
            this.mnuMakeHotspot.Name = "mnuMakeHotspot";
            this.mnuMakeHotspot.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuMakeHotspot.Size = new System.Drawing.Size(310, 34);
            this.mnuMakeHotspot.Text = "Crear Hotspot";
           // this.mnuMakeHotspot.Click += new System.EventHandler(this.mnuMakeHotspot_Click);
            // 
            // cambiarNombrePuestoToolStripMenuItem
            // 
            this.cambiarNombrePuestoToolStripMenuItem.Name = "cambiarNombrePuestoToolStripMenuItem";
            this.cambiarNombrePuestoToolStripMenuItem.Size = new System.Drawing.Size(310, 34);
            this.cambiarNombrePuestoToolStripMenuItem.Text = "Cambiar Nombre Puesto";
            this.cambiarNombrePuestoToolStripMenuItem.Click += new System.EventHandler(this.cambiarNombrePuestoToolStripMenuItem_Click);
            // 
            // buscarPuestoToolStripMenuItem
            // 
            this.buscarPuestoToolStripMenuItem.Name = "buscarPuestoToolStripMenuItem";
            this.buscarPuestoToolStripMenuItem.Size = new System.Drawing.Size(310, 34);
            this.buscarPuestoToolStripMenuItem.Text = "Buscar Puesto";
            this.buscarPuestoToolStripMenuItem.Click += new System.EventHandler(this.buscarPuestoToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.CheckOnClick = true;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(310, 34);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.Checked = true;
            this.scaleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScale1,
            this.mnuScale2});
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(75, 30);
            this.scaleToolStripMenuItem.Text = "&Escala";
            // 
            // panMap
            // 
            this.panMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panMap.AutoScroll = true;
            this.panMap.Controls.Add(this.picMap);
            this.panMap.Location = new System.Drawing.Point(213, 38);
            this.panMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panMap.Name = "panMap";
            this.panMap.Size = new System.Drawing.Size(568, 354);
            this.panMap.TabIndex = 2;
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(4, 5);
            this.picMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(144, 125);
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            this.picMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseClick);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scaleToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(781, 36);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cmbUbic
            // 
            this.cmbUbic.FormattingEnabled = true;
            this.cmbUbic.Location = new System.Drawing.Point(9, 109);
            this.cmbUbic.Name = "cmbUbic";
            this.cmbUbic.Size = new System.Drawing.Size(197, 28);
            this.cmbUbic.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cargar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 265);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(137, 26);
            this.textBox1.TabIndex = 6;
            // 
            // Mapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 402);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbUbic);
            this.Controls.Add(this.panMap);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Mapa";
            this.Text = "Mapas";
            this.Load += new System.EventHandler(this.Mapa_Load);
            this.panMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnuScale2;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.ToolStripMenuItem mnuScale1;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMakeHotspot;
        private System.Windows.Forms.ToolStripMenuItem scaleToolStripMenuItem;
        private System.Windows.Forms.Panel panMap;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cambiarNombrePuestoToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbUbic;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem buscarPuestoToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
    }
}

