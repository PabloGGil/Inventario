
namespace InventarioAsset
{
    partial class frmMovEq
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovEq));
            this.btnAgregar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvw = new System.Windows.Forms.ListView();
            this.MenuContextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mv2panol = new System.Windows.Forms.ToolStripMenuItem();
            this.Asignar2tecnico = new System.Windows.Forms.ToolStripMenuItem();
            this.AsignarResponsable = new System.Windows.Forms.ToolStripMenuItem();
            this.CambioPuesto = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmdMover = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.grbCampos = new System.Windows.Forms.GroupBox();
            this.cmdListaUsuario = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOS = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPuesto = new System.Windows.Forms.TextBox();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdImport = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lvwFinal = new System.Windows.Forms.ListView();
            this.btnHistMov = new System.Windows.Forms.Button();
            this.cmdListaPuestos = new System.Windows.Forms.Button();
            this.MenuContextual.SuspendLayout();
            this.grbCampos.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(15, 70);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(105, 27);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 44);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Inventario";
            // 
            // lvw
            // 
            this.lvw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvw.ContextMenuStrip = this.MenuContextual;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(243, 21);
            this.lvw.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(737, 224);
            this.lvw.SmallImageList = this.imageList1;
            this.lvw.TabIndex = 3;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvw_MouseDown);
            // 
            // MenuContextual
            // 
            this.MenuContextual.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MenuContextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mv2panol,
            this.Asignar2tecnico,
            this.AsignarResponsable,
            this.CambioPuesto});
            this.MenuContextual.Name = "MenuContextual";
            this.MenuContextual.Size = new System.Drawing.Size(70, 92);
            // 
            // mv2panol
            // 
            this.mv2panol.Name = "mv2panol";
            this.mv2panol.Size = new System.Drawing.Size(69, 22);
            // 
            // Asignar2tecnico
            // 
            this.Asignar2tecnico.Name = "Asignar2tecnico";
            this.Asignar2tecnico.Size = new System.Drawing.Size(69, 22);
            // 
            // AsignarResponsable
            // 
            this.AsignarResponsable.Name = "AsignarResponsable";
            this.AsignarResponsable.Size = new System.Drawing.Size(69, 22);
            // 
            // CambioPuesto
            // 
            this.CambioPuesto.Name = "CambioPuesto";
            this.CambioPuesto.Size = new System.Drawing.Size(69, 22);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "OK.ICO");
            this.imageList1.Images.SetKeyName(1, "OK1.ICO");
            this.imageList1.Images.SetKeyName(2, "Error.ico");
            this.imageList1.Images.SetKeyName(3, "Lleno.ico");
            this.imageList1.Images.SetKeyName(4, "PerfCenterCpl.ico");
            this.imageList1.Images.SetKeyName(5, "Properties.ico");
            this.imageList1.Images.SetKeyName(6, "TRFFC13.ICO");
            this.imageList1.Images.SetKeyName(7, "Users.ico");
            this.imageList1.Images.SetKeyName(8, "Vacio.ico");
            // 
            // cmdMover
            // 
            this.cmdMover.Location = new System.Drawing.Point(15, 490);
            this.cmdMover.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdMover.Name = "cmdMover";
            this.cmdMover.Size = new System.Drawing.Size(105, 27);
            this.cmdMover.TabIndex = 6;
            this.cmdMover.Text = "Mover";
            this.cmdMover.UseVisualStyleBackColor = true;
            this.cmdMover.Click += new System.EventHandler(this.cmdMover_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(124, 70);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(89, 28);
            this.btnQuitar.TabIndex = 17;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(20, 181);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(211, 24);
            this.cmbStatus.TabIndex = 19;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // grbCampos
            // 
            this.grbCampos.Controls.Add(this.cmdListaPuestos);
            this.grbCampos.Controls.Add(this.cmdListaUsuario);
            this.grbCampos.Controls.Add(this.label5);
            this.grbCampos.Controls.Add(this.label4);
            this.grbCampos.Controls.Add(this.label3);
            this.grbCampos.Controls.Add(this.label2);
            this.grbCampos.Controls.Add(this.txtOS);
            this.grbCampos.Controls.Add(this.txtUsuario);
            this.grbCampos.Controls.Add(this.txtPuesto);
            this.grbCampos.Controls.Add(this.txtComentarios);
            this.grbCampos.Enabled = false;
            this.grbCampos.Location = new System.Drawing.Point(12, 266);
            this.grbCampos.Margin = new System.Windows.Forms.Padding(4);
            this.grbCampos.Name = "grbCampos";
            this.grbCampos.Padding = new System.Windows.Forms.Padding(4);
            this.grbCampos.Size = new System.Drawing.Size(313, 207);
            this.grbCampos.TabIndex = 20;
            this.grbCampos.TabStop = false;
            this.grbCampos.Text = "groupBox1";
            // 
            // cmdListaUsuario
            // 
            this.cmdListaUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdListaUsuario.Location = new System.Drawing.Point(265, 144);
            this.cmdListaUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.cmdListaUsuario.Name = "cmdListaUsuario";
            this.cmdListaUsuario.Size = new System.Drawing.Size(37, 18);
            this.cmdListaUsuario.TabIndex = 26;
            this.cmdListaUsuario.Text = "...";
            this.cmdListaUsuario.UseVisualStyleBackColor = true;
            this.cmdListaUsuario.Click += new System.EventHandler(this.cmdListaUsuario_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 171);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = "OS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 139);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Usuario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Puesto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Comentario";
            // 
            // txtOS
            // 
            this.txtOS.Location = new System.Drawing.Point(95, 174);
            this.txtOS.Margin = new System.Windows.Forms.Padding(4);
            this.txtOS.Name = "txtOS";
            this.txtOS.Size = new System.Drawing.Size(151, 22);
            this.txtOS.TabIndex = 20;
            // 
            // txtUsuario
            // 
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Location = new System.Drawing.Point(95, 142);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(151, 22);
            this.txtUsuario.TabIndex = 19;
            // 
            // txtPuesto
            // 
            this.txtPuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPuesto.Enabled = false;
            this.txtPuesto.Location = new System.Drawing.Point(95, 110);
            this.txtPuesto.Margin = new System.Windows.Forms.Padding(4);
            this.txtPuesto.Name = "txtPuesto";
            this.txtPuesto.Size = new System.Drawing.Size(151, 22);
            this.txtPuesto.TabIndex = 18;
            this.txtPuesto.TextChanged += new System.EventHandler(this.txtPuesto_TextChanged);
            this.txtPuesto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPuesto_KeyPress);
            this.txtPuesto.Leave += new System.EventHandler(this.txtPuesto_Leave);
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(95, 30);
            this.txtComentarios.Margin = new System.Windows.Forms.Padding(4);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(208, 72);
            this.txtComentarios.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(495, 542);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "label6";
            // 
            // cmdImport
            // 
            this.cmdImport.Location = new System.Drawing.Point(20, 119);
            this.cmdImport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(193, 26);
            this.cmdImport.TabIndex = 22;
            this.cmdImport.Text = "Importar desde excel";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 161);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Acciones";
            // 
            // lvwFinal
            // 
            this.lvwFinal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwFinal.ContextMenuStrip = this.MenuContextual;
            this.lvwFinal.HideSelection = false;
            this.lvwFinal.Location = new System.Drawing.Point(337, 266);
            this.lvwFinal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvwFinal.Name = "lvwFinal";
            this.lvwFinal.Size = new System.Drawing.Size(643, 216);
            this.lvwFinal.SmallImageList = this.imageList1;
            this.lvwFinal.TabIndex = 24;
            this.lvwFinal.UseCompatibleStateImageBehavior = false;
            // 
            // btnHistMov
            // 
            this.btnHistMov.Location = new System.Drawing.Point(343, 494);
            this.btnHistMov.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistMov.Name = "btnHistMov";
            this.btnHistMov.Size = new System.Drawing.Size(105, 21);
            this.btnHistMov.TabIndex = 25;
            this.btnHistMov.Text = "Historial";
            this.btnHistMov.UseVisualStyleBackColor = true;
            this.btnHistMov.Click += new System.EventHandler(this.btnHistMov_Click);
            // 
            // cmdListaPuestos
            // 
            this.cmdListaPuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdListaPuestos.Location = new System.Drawing.Point(266, 114);
            this.cmdListaPuestos.Margin = new System.Windows.Forms.Padding(4);
            this.cmdListaPuestos.Name = "cmdListaPuestos";
            this.cmdListaPuestos.Size = new System.Drawing.Size(37, 18);
            this.cmdListaPuestos.TabIndex = 27;
            this.cmdListaPuestos.Text = "...";
            this.cmdListaPuestos.UseVisualStyleBackColor = true;
            this.cmdListaPuestos.Click += new System.EventHandler(this.cmdListaPuestos_Click);
            // 
            // frmMovEq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 561);
            this.Controls.Add(this.btnHistMov);
            this.Controls.Add(this.lvwFinal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grbCampos);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.cmdMover);
            this.Controls.Add(this.lvw);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnAgregar);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMovEq";
            this.Text = "Movimiento de Equipos";
            this.Load += new System.EventHandler(this.frmMovEq_Load);
            this.MenuContextual.ResumeLayout(false);
            this.grbCampos.ResumeLayout(false);
            this.grbCampos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ContextMenuStrip MenuContextual;
        private System.Windows.Forms.ToolStripMenuItem mv2panol;
        private System.Windows.Forms.ToolStripMenuItem Asignar2tecnico;
        private System.Windows.Forms.ToolStripMenuItem AsignarResponsable;
        private System.Windows.Forms.ToolStripMenuItem CambioPuesto;
        private System.Windows.Forms.Button cmdMover;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.GroupBox grbCampos;
        private System.Windows.Forms.Button cmdListaUsuario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOS;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPuesto;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lvwFinal;
        private System.Windows.Forms.Button btnHistMov;
        private System.Windows.Forms.Button cmdListaPuestos;
    }
}