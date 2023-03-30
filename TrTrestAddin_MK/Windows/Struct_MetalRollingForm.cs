using System;
using Form = System.Windows.Forms.Form;

namespace TrTrestAddin_MK.Windows
{
    public partial class Struct_MetalRollingForm : Form
    {
        public int schHeight = 0;
        public bool isFormClosed = false;
        public Struct_MetalRollingForm()
        {
            InitializeComponent();
            numericUpDown1.Focus();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            schHeight = (int)numericUpDown1.Value;
            this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            isFormClosed = true;
            this.Close();
        }
    }
}
