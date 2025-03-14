using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;

namespace InventarioAsset
{
    public partial class frmPropiedades : Form
    {
        AD ad = new AD();

        //public frmPropiedades(object propertyItem, Form2.ActionTypes actionType)
        public frmPropiedades(object propertyItem)
        {
            InitializeComponent();

            if (propertyItem is List<Principal>)
            {
                pg.SelectedObject = ((List<Principal>)propertyItem).ToArray();
            }
            else
            {
                pg.SelectedObject = propertyItem;
            }
            //b1.Text = actionType.ToString();
            // b1.Visible = (actionType != Form2.ActionTypes.None);
        }

        private void b1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
