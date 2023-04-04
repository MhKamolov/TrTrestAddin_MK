using System;
using Form = System.Windows.Forms.Form;

namespace TrTrestAddin_MK.Windows
{
    public partial class Struct_MetalRollingInputForm : Form
    {
        public int schHeight = 0;
        public bool isCloseBtnClicked = false;
        public bool isOKBtnClicked = false;
        public Struct_MetalRollingInputForm()
        {
            InitializeComponent();
            numericUpDown1.Focus();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            schHeight = (int)numericUpDown1.Value;
            isOKBtnClicked = true;
            this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            isCloseBtnClicked = true;
            this.Close();
        }        

        private void Struct_MetalRollingForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!isOKBtnClicked)
                isCloseBtnClicked = true;
        }
    }
}
