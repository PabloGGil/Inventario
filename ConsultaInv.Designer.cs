
namespace InventarioAsset
{
    partial class ConsultaInv
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
            this.components = new System.ComponentModel.Container();
            this.cmbCriterio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdBuscar = new System.Windows.Forms.Button();
            this.dgvAsset = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdMov = new System.Windows.Forms.Button();
            this.cmdNotas = new System.Windows.Forms.Button();
            this.cmdUsuario = new System.Windows.Forms.Button();
            this.cmdDatosCompra = new System.Windows.Forms.Button();
            this.cmbValor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importarDesdeExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsset)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCriterio
            // 
            this.cmbCriterio.FormattingEnabled = true;
            this.cmbCriterio.Location = new System.Drawing.Point(22, 36);
            this.cmbCriterio.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.cmbCriterio.Name = "cmbCriterio";
            this.cmbCriterio.Size = new System.Drawing.Size(136, 21);
            this.cmbCriterio.TabIndex = 0;
            this.cmbCriterio.SelectedIndexChanged += new System.EventHandler(this.cmbCriterio_SelectedIndexChanged);
            this.cmbCriterio.DropDownClosed += new System.EventHandler(this.cmbCriterio_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Criterio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Valor a buscar";
            // 
            // cmdBuscar
            // 
            this.cmdBuscar.Location = new System.Drawing.Point(23, 168);
            this.cmdBuscar.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.cmdBuscar.Name = "cmdBuscar";
            this.cmdBuscar.Size = new System.Drawing.Size(140, 29);
            this.cmdBuscar.TabIndex = 4;
            this.cmdBuscar.Text = "Buscar";
            this.cmdBuscar.UseVisualStyleBackColor = true;
            this.cmdBuscar.Click += new System.EventHandler(this.cmdBuscar_Click);
            // 
            // dgvAsset
            // 
            this.dgvAsset.AllowUserToAddRows = false;
            this.dgvAsset.AllowUserToDeleteRows = false;
            this.dgvAsset.AllowUserToOrderColumns = true;
            this.dgvAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAsset.ColumnHeadersHeight = 29;
            this.dgvAsset.Location = new System.Drawing.Point(206, 28);
            this.dgvAsset.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvAsset.Name = "dgvAsset";
            this.dgvAsset.ReadOnly = true;
            this.dgvAsset.RowHeadersWidth = 51;
            this.dgvAsset.RowTemplate.Height = 24;
            this.dgvAsset.Size = new System.Drawing.Size(474, 356);
            this.dgvAsset.TabIndex = 8;
            this.dgvAsset.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAsset_CellContentClick);
            this.dgvAsset.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAsset_ColumnHeaderMouseClick);
            this.dgvAsset.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvAsset_DataBindingComplete);
            this.dgvAsset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvAsset_MouseDown);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(23, 129);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 27);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Exportar";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cmdMov
            // 
            this.cmdMov.Location = new System.Drawing.Point(23, 207);
            this.cmdMov.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdMov.Name = "cmdMov";
            this.cmdMov.Size = new System.Drawing.Size(141, 34);
            this.cmdMov.TabIndex = 19;
            this.cmdMov.Text = "Movimientos";
            this.cmdMov.UseVisualStyleBackColor = true;
            this.cmdMov.Click += new System.EventHandler(this.cmdMov_Click);
            // 
            // cmdNotas
            // 
            this.cmdNotas.Location = new System.Drawing.Point(23, 254);
            this.cmdNotas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdNotas.Name = "cmdNotas";
            this.cmdNotas.Size = new System.Drawing.Size(141, 34);
            this.cmdNotas.TabIndex = 20;
            this.cmdNotas.Text = "Notas";
            this.cmdNotas.UseVisualStyleBackColor = true;
            this.cmdNotas.Click += new System.EventHandler(this.cmdNotas_Click);
            // 
            // cmdUsuario
            // 
            this.cmdUsuario.Location = new System.Drawing.Point(23, 300);
            this.cmdUsuario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdUsuario.Name = "cmdUsuario";
            this.cmdUsuario.Size = new System.Drawing.Size(141, 38);
            this.cmdUsuario.TabIndex = 21;
            this.cmdUsuario.Text = "Datos Usuario";
            this.cmdUsuario.UseVisualStyleBackColor = true;
            this.cmdUsuario.Click += new System.EventHandler(this.cmdUsuario_Click);
            // 
            // cmdDatosCompra
            // 
            this.cmdDatosCompra.Location = new System.Drawing.Point(23, 350);
            this.cmdDatosCompra.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdDatosCompra.Name = "cmdDatosCompra";
            this.cmdDatosCompra.Size = new System.Drawing.Size(141, 34);
            this.cmdDatosCompra.TabIndex = 22;
            this.cmdDatosCompra.Text = "Datos compra";
            this.cmdDatosCompra.UseVisualStyleBackColor = true;
            this.cmdDatosCompra.Click += new System.EventHandler(this.cmdDatosCompra_Click);
            // 
            // cmbValor
            // 
            this.cmbValor.FormattingEnabled = true;
            this.cmbValor.Location = new System.Drawing.Point(23, 79);
            this.cmbValor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbValor.Name = "cmbValor";
            this.cmbValor.Size = new System.Drawing.Size(141, 21);
            this.cmbValor.TabIndex = 23;
            this.cmbValor.DropDown += new System.EventHandler(this.cmbValor_DropDown);
            this.cmbValor.SelectedIndexChanged += new System.EventHandler(this.cmbValor_SelectedIndexChanged);
            this.cmbValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbValor_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 24;
            this.label3.Text = "label3";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(23, 102);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 19);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Cargar de archivo";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // mnuContext
            // 
            this.mnuContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importarDesdeExcelToolStripMenuItem});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(219, 28);
            this.mnuContext.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuContext_ItemClicked);
            // 
            // importarDesdeExcelToolStripMenuItem
            // 
            this.importarDesdeExcelToolStripMenuItem.Name = "importarDesdeExcelToolStripMenuItem";
            this.importarDesdeExcelToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.importarDesdeExcelToolStripMenuItem.Text = "Importar desde excel";
            this.importarDesdeExcelToolStripMenuItem.Click += new System.EventHandler(this.importarDesdeExcelToolStripMenuItem_Click);
            // 
            // ConsultaInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 404);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbValor);
            this.Controls.Add(this.cmdDatosCompra);
            this.Controls.Add(this.cmdUsuario);
            this.Controls.Add(this.cmdNotas);
            this.Controls.Add(this.cmdMov);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvAsset);
            this.Controls.Add(this.cmdBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCriterio);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "ConsultaInv";
            this.Text = "Consulta Inventario";
            this.Load += new System.EventHandler(this.ConsultaInv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsset)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCriterio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdBuscar;
        private System.Windows.Forms.DataGridView dgvAsset;
//        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdMov;
        private System.Windows.Forms.Button cmdNotas;
        private System.Windows.Forms.Button cmdUsuario;
        private System.Windows.Forms.Button cmdDatosCompra;
        private System.Windows.Forms.ComboBox cmbValor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem importarDesdeExcelToolStripMenuItem;
    }
}