using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrTrestAddin_MK.Windows
{
    public partial class AR_RolledSteelEditingForm : Form
    {
        public List<string> fencesNames = new List<string>();
        public List<string> fencesDescriptions = new List<string>();
        public List<string> joinList = new List<string>();
        public bool isOKBtnClicked = false;
        public bool isCloseBtnClicked = false;

        public AR_RolledSteelEditingForm(List<string> wrongFences)
        {
            InitializeComponent();

            foreach (var item in wrongFences)
            {
                listBox1.Items.Add(item);
            }
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            //listBox1.SelectionMode = SelectionMode.MultiSimple;
        }

        private void fillBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Поле 'Опиcание' пусто", "Внимание!");
            }
            else
            {
                fencesNames.Add(listBox1.SelectedItem.ToString());
                fencesDescriptions.Add(textBox1.Text.TrimEnd());

                for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    fencesNames.Add(listBox1.SelectedItems[i].ToString());
                    listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
                    fencesDescriptions.Add(textBox1.Text);
                }

                textBox1.Text = "";
                if (listBox1.Items.Count != 0)
                    if (listBox1.SelectedIndex != listBox1.Items.Count - 1)
                        listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    else
                        listBox1.SelectedIndex = 0;
                else
                    fillBtn.Enabled = false;
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Не все типоразмеры заполнены." +
                    "\nХотите продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    isOKBtnClicked = true;
                    this.Close();
                }
            }
            else
            {
                isOKBtnClicked = true;
                this.Close();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            isCloseBtnClicked = true;
            this.Close();
        }

        private void Struct_MetalRollingEditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isOKBtnClicked)
                isCloseBtnClicked = true;
        }
    }
}
