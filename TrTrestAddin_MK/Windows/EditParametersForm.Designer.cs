namespace TrTrestAddin_MK.Windows
{
    partial class EditParametersForm
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
            this.CloseBtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ChangeBtn = new System.Windows.Forms.Button();
            this.NewParam_TotalNum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CurParam_TotalNum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OSP_listBox = new System.Windows.Forms.ListBox();
            this.NSP_listBox = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Param_checkedLB = new System.Windows.Forms.CheckedListBox();
            this.paramsToImport_Label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Search_Text = new System.Windows.Forms.TextBox();
            this.ImportBtn = new System.Windows.Forms.Button();
            this.ParamGroup_Combo = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseBtn.Location = new System.Drawing.Point(485, 553);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(85, 35);
            this.CloseBtn.TabIndex = 25;
            this.CloseBtn.Text = "Закрыть";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ApplyBtn.Location = new System.Drawing.Point(391, 553);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(85, 35);
            this.ApplyBtn.TabIndex = 24;
            this.ApplyBtn.Text = "Применить";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(580, 542);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ChangeBtn);
            this.tabPage1.Controls.Add(this.NewParam_TotalNum);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.CurParam_TotalNum);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.OSP_listBox);
            this.tabPage1.Controls.Add(this.NSP_listBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Заменяемые параметры";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ChangeBtn
            // 
            this.ChangeBtn.AutoSize = true;
            this.ChangeBtn.Location = new System.Drawing.Point(422, 476);
            this.ChangeBtn.Name = "ChangeBtn";
            this.ChangeBtn.Size = new System.Drawing.Size(126, 23);
            this.ChangeBtn.TabIndex = 14;
            this.ChangeBtn.Text = "Заменить";
            this.ChangeBtn.UseVisualStyleBackColor = true;
            this.ChangeBtn.Click += new System.EventHandler(this.ChangeBtn_Click);
            // 
            // NewParam_TotalNum
            // 
            this.NewParam_TotalNum.AutoSize = true;
            this.NewParam_TotalNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewParam_TotalNum.Location = new System.Drawing.Point(440, 424);
            this.NewParam_TotalNum.Name = "NewParam_TotalNum";
            this.NewParam_TotalNum.Size = new System.Drawing.Size(13, 13);
            this.NewParam_TotalNum.TabIndex = 13;
            this.NewParam_TotalNum.Text = "0";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(300, 423);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Количество параметров: ";
            // 
            // CurParam_TotalNum
            // 
            this.CurParam_TotalNum.AutoSize = true;
            this.CurParam_TotalNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurParam_TotalNum.Location = new System.Drawing.Point(153, 422);
            this.CurParam_TotalNum.Name = "CurParam_TotalNum";
            this.CurParam_TotalNum.Size = new System.Drawing.Size(13, 13);
            this.CurParam_TotalNum.TabIndex = 11;
            this.CurParam_TotalNum.Text = "0";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(13, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Количество параметров: ";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(47, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Текущие общие параметры";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(344, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Новые общие параметры";
            // 
            // OSP_listBox
            // 
            this.OSP_listBox.FormattingEnabled = true;
            this.OSP_listBox.Location = new System.Drawing.Point(16, 56);
            this.OSP_listBox.Name = "OSP_listBox";
            this.OSP_listBox.Size = new System.Drawing.Size(247, 355);
            this.OSP_listBox.TabIndex = 4;
            // 
            // NSP_listBox
            // 
            this.NSP_listBox.FormattingEnabled = true;
            this.NSP_listBox.Location = new System.Drawing.Point(303, 57);
            this.NSP_listBox.Name = "NSP_listBox";
            this.NSP_listBox.Size = new System.Drawing.Size(247, 355);
            this.NSP_listBox.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.Param_checkedLB);
            this.tabPage2.Controls.Add(this.paramsToImport_Label);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.Search_Text);
            this.tabPage2.Controls.Add(this.ImportBtn);
            this.tabPage2.Controls.Add(this.ParamGroup_Combo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Импортируемые параметры";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(460, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(168, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Параметры";
            // 
            // Param_checkedLB
            // 
            this.Param_checkedLB.FormattingEnabled = true;
            this.Param_checkedLB.Location = new System.Drawing.Point(171, 109);
            this.Param_checkedLB.Name = "Param_checkedLB";
            this.Param_checkedLB.Size = new System.Drawing.Size(245, 319);
            this.Param_checkedLB.TabIndex = 16;
            this.Param_checkedLB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Param_checkedLB_ItemCheck);
            this.Param_checkedLB.SelectedValueChanged += new System.EventHandler(this.Param_checkedLB_SelectedValueChanged);
            // 
            // paramsToImport_Label
            // 
            this.paramsToImport_Label.AutoSize = true;
            this.paramsToImport_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paramsToImport_Label.Location = new System.Drawing.Point(298, 438);
            this.paramsToImport_Label.Name = "paramsToImport_Label";
            this.paramsToImport_Label.Size = new System.Drawing.Size(13, 13);
            this.paramsToImport_Label.TabIndex = 15;
            this.paramsToImport_Label.Text = "0";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(168, 438);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 23);
            this.label6.TabIndex = 14;
            this.label6.Text = "Количество параметров: ";
            // 
            // Search_Text
            // 
            this.Search_Text.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Search_Text.Location = new System.Drawing.Point(171, 58);
            this.Search_Text.Name = "Search_Text";
            this.Search_Text.Size = new System.Drawing.Size(245, 20);
            this.Search_Text.TabIndex = 3;
            this.Search_Text.Text = "Поиск";
            this.Search_Text.TextChanged += new System.EventHandler(this.Search_Text_TextChanged);
            this.Search_Text.Enter += new System.EventHandler(this.Search_Text_Enter);
            this.Search_Text.Leave += new System.EventHandler(this.Search_Text_Leave);
            // 
            // ImportBtn
            // 
            this.ImportBtn.Location = new System.Drawing.Point(422, 476);
            this.ImportBtn.Name = "ImportBtn";
            this.ImportBtn.Size = new System.Drawing.Size(126, 23);
            this.ImportBtn.TabIndex = 2;
            this.ImportBtn.Text = "Импортировать";
            this.ImportBtn.UseVisualStyleBackColor = true;
            this.ImportBtn.Click += new System.EventHandler(this.ImportBtn_Click);
            // 
            // ParamGroup_Combo
            // 
            this.ParamGroup_Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ParamGroup_Combo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ParamGroup_Combo.FormattingEnabled = true;
            this.ParamGroup_Combo.Location = new System.Drawing.Point(171, 25);
            this.ParamGroup_Combo.Name = "ParamGroup_Combo";
            this.ParamGroup_Combo.Size = new System.Drawing.Size(245, 21);
            this.ParamGroup_Combo.TabIndex = 1;
            this.ParamGroup_Combo.SelectionChangeCommitted += new System.EventHandler(this.ParamGroup_Combo_SelectionChangeCommitted);
            this.ParamGroup_Combo.Enter += new System.EventHandler(this.ParamGroup_Combo_Enter);
            // 
            // EditParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 592);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.tabControl1);
            this.Name = "EditParametersForm";
            this.Text = "EditParametersForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditParametersForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button ChangeBtn;
        private System.Windows.Forms.Label NewParam_TotalNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label CurParam_TotalNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox OSP_listBox;
        private System.Windows.Forms.ListBox NSP_listBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox Param_checkedLB;
        private System.Windows.Forms.Label paramsToImport_Label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Search_Text;
        private System.Windows.Forms.Button ImportBtn;
        private System.Windows.Forms.ComboBox ParamGroup_Combo;
        private System.Windows.Forms.Button button1;
    }
}