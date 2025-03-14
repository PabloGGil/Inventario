namespace InventarioAsset
{
    partial class frmPropiedades
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
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.b1 = new System.Windows.Forms.Button();
            this.b2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pg
            // 
            this.pg.Location = new System.Drawing.Point(1, 1);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(779, 544);
            this.pg.TabIndex = 0;
            // 
            // b1
            // 
            this.b1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.b1.Location = new System.Drawing.Point(0, 551);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(781, 23);
            this.b1.TabIndex = 6;
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // b2
            // 
            this.b2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.b2.Location = new System.Drawing.Point(0, 574);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(781, 23);
            this.b2.TabIndex = 5;
            this.b2.Text = "Cerrar";
            this.b2.UseVisualStyleBackColor = true;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 597);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.b2);
            this.Controls.Add(this.pg);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pg;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Button b2;
    }
}