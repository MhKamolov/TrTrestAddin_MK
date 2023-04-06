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
    public partial class StructMetalRollingEditingForm : Form
    {
        public List<string> fencesNames = new List<string>();
        public List<string> fencesDescriptions = new List<string>();
        public List<string> joinList = new List<string>();
        public bool isOKBtnClicked = false;
        public bool isCloseBtnClicked = false;

        public StructMetalRollingEditingForm(List<string> wrongFences)
        {
            InitializeComponent();
            
            foreach (var item in wrongFences)
            {
                listBox1.Items.Add(item);
            }
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
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
                fencesDescriptions.Add(textBox1.Text);

                textBox1.Text = "";
                listBox1.Items.Remove(listBox1.SelectedItem);
                if (listBox1.Items.Count != 0)
                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                else
                    fillBtn.Enabled = false;

                joinList = new List<string>();
                for (int i = 0; i < fencesNames.Count; i++)
                {
                    joinList.Add(fencesNames[i] + " - " + fencesDescriptions[i]);
                }
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                MessageBox.Show("Не все типоразмеры заполнены", "Внимание!");
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
