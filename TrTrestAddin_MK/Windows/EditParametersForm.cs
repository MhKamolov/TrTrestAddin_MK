using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;

namespace TrTrestAddin_MK.Windows
{
    public partial class EditParametersForm : Form
    {
        public List<ExternalDefinition> SelectedParamsToImport = new List<ExternalDefinition>();
        public bool OKBtnClicked = false;
        public bool CloseBtnClicked = false;
        public bool ChangeBtnClicked = false;
        public bool ImportBtnClicked = false;

        private UIApplication _app;
        private List<ExternalDefinition> _allSharedParams = new List<ExternalDefinition>();
        private List<ExternalDefinition> _newSharedParams = new List<ExternalDefinition>();
        private List<ExternalDefinition> _sharedParamsToRemove = new List<ExternalDefinition>();
        private List<string> _selectedParams = new List<string>();
        private List<string> _forParam_checkedLB = new List<string>(); // вспомогательный список
        private int _paramsToImportNum = 0;
        private int _selectedParamsNum = 0;

        private List<string> _selectedParams_copy = new List<string>();

        public EditParametersForm(UIApplication uiapp, List<FamilyParameter> oldSharedParams,
            List<ExternalDefinition> newSharedParams, List<ExternalDefinition> sharedParamsToRemove)
        {
            InitializeComponent();

            _app = uiapp;
            _newSharedParams = newSharedParams;
            _sharedParamsToRemove = sharedParamsToRemove;

            // Украшение элементов управлений
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Search_Text.Text = "Поиск";
            Search_Text.ForeColor = System.Drawing.Color.Gray;
            Param_checkedLB.CheckOnClick = true;

            /// Заполнение элементов
            // 1 вкладка
            for (int i = 0; i < oldSharedParams.Count; i++)
            {
                OSP_listBox.Items.Add(oldSharedParams[i].Definition.Name);
                NSP_listBox.Items.Add(_newSharedParams[i].Name);

            }

            // 2 вкладка
            ParamGroup_Combo.Items.Add("Все");
            foreach (DefinitionGroup group in _app.Application.OpenSharedParameterFile().Groups)
            {
                ParamGroup_Combo.Items.Add(group.Name);
                foreach (ExternalDefinition definition in group.Definitions)
                {
                    Param_checkedLB.Items.Add(definition.Name);
                    _allSharedParams.Add(definition);
                }
            }

            // Удаление заменяемые параметры из импортированных
            if (_newSharedParams.Count != 0)
            {
                foreach (var definition in _newSharedParams)
                    foreach (var item in Param_checkedLB.Items)
                        if (definition.Name == item.ToString())
                        {
                            Param_checkedLB.Items.RemoveAt(Param_checkedLB.Items.IndexOf(item));
                            break;
                        }
            }
            else
            {
                foreach (var definition in _sharedParamsToRemove)
                    foreach (var item in Param_checkedLB.Items)
                        if (definition.Name == item.ToString())
                        {
                            Param_checkedLB.Items.RemoveAt(Param_checkedLB.Items.IndexOf(item));
                            break;
                        }
            }

            _forParam_checkedLB.Clear();
            foreach (var item in Param_checkedLB.Items)
                _forParam_checkedLB.Add(item.ToString());

            ///
            if (_newSharedParams.Count == 0)
                ChangeBtn.Enabled = false;

            _paramsToImportNum = Param_checkedLB.Items.Count; // Общая Количество параметров после удаление
            ParamGroup_Combo.SelectedIndex = 0; // Выбор значение "Все" по умолчанию в списке группы 

            CurParam_TotalNum.Text = OSP_listBox.Items.Count.ToString();
            NewParam_TotalNum.Text = NSP_listBox.Items.Count.ToString();
            paramsToImport_Label.Text = _paramsToImportNum.ToString() + " | " +
                "Выбранные: " + _selectedParamsNum.ToString();



        }

        private void ParamGroup_Combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Очистка перед заполнение список параметров
            Param_checkedLB.Items.Clear();
            _forParam_checkedLB.Clear();

            // Заполнение checkedListBox (Параметры из ФОП по группам)
            foreach (DefinitionGroup group in _app.Application.OpenSharedParameterFile().Groups)
                if (ParamGroup_Combo.Text == "Все")
                {
                    foreach (ExternalDefinition definition in group.Definitions)
                        Param_checkedLB.Items.Add(definition.Name);
                }
                else if (group.Name == ParamGroup_Combo.Text)
                {
                    foreach (ExternalDefinition definition in group.Definitions)
                        Param_checkedLB.Items.Add(definition.Name);
                    break;
                }

            // Удаление заменяемые параметры из импортированных (каждый раз при фильтре)
            if (_newSharedParams.Count != 0)
            {
                foreach (var definition in _newSharedParams)
                    foreach (var item in Param_checkedLB.Items)
                        if (definition.Name == item.ToString())
                        {
                            Param_checkedLB.Items.RemoveAt(Param_checkedLB.Items.IndexOf(item));
                            break;
                        }
            }
            else
            {
                foreach (var definition in _sharedParamsToRemove)
                    foreach (var item in Param_checkedLB.Items)
                        if (definition.Name == item.ToString())
                        {
                            Param_checkedLB.Items.RemoveAt(Param_checkedLB.Items.IndexOf(item));
                            break;
                        }
            }
            //_selectedParams.RemoveAll   // Начать отсюда!

            foreach (var item in Param_checkedLB.Items)
                _forParam_checkedLB.Add(item.ToString());


            if (_selectedParams.Count != 0)
            {
                foreach (string str in _selectedParams)
                {
                    foreach (var item in Param_checkedLB.Items)
                        if (str == item.ToString())
                        {
                            Param_checkedLB.SetItemChecked(Param_checkedLB.Items.IndexOf(item.ToString()), true); // Поставлю галочку
                            break;

                        }
                }

                //for (int i = _selectedParams.Count - 1; i >= 0; i--)
                //{
                //    for (int j = 0; j < Param_checkedLB.Items.Count; j++)
                //    {
                //        if (_selectedParams[i] == Param_checkedLB.Items[j].ToString()
                //            && Param_checkedLB.GetItemChecked(j) == false)
                //        {
                //            _selectedParams.RemoveAt(i); // Удаляю из _selectedParams отмененные параметры
                //            break;
                //        }
                //    }
                //}


            }


            _selectedParamsNum = _selectedParams.Count;

            paramsToImport_Label.Text = _paramsToImportNum.ToString() + " | " +
                "Выбранные: " + _selectedParamsNum.ToString();

            _selectedParams_copy = _selectedParams;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = new List<string>();
            //t.Add(_selectedParams[i]);

            for (int i = _selectedParams.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < Param_checkedLB.Items.Count; j++)
                {
                    if (_selectedParams[i] == Param_checkedLB.Items[j].ToString()
                        && Param_checkedLB.GetItemChecked(j) == false)
                    {
                        _selectedParams.RemoveAt(i);
                        break;
                    }
                }
            }

            //MessageBox.Show(String.Join("\n", t), "Catched");
            //MessageBox.Show(String.Join("\n", _selectedParams));

        }

        private void ParamGroup_Combo_Enter(object sender, EventArgs e)
        {
            var for_checkedItems = Param_checkedLB.CheckedItems.Cast<string>().ToList();
            foreach (string str in for_checkedItems)
                _selectedParams.Add(str);

            _selectedParams = _selectedParams.Distinct().ToList();

            for (int i = _selectedParams.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < Param_checkedLB.Items.Count; j++)
                {
                    if (_selectedParams[i] == Param_checkedLB.Items[j].ToString()
                        && Param_checkedLB.GetItemChecked(j) == false)
                    {
                        _selectedParams.RemoveAt(i); // Удаляю из _selectedParams отмененные параметры
                        break;
                    }
                }
            }


        }

        private void Param_checkedLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                ++_selectedParamsNum;


            if (e.NewValue == CheckState.Unchecked)
                --_selectedParamsNum;


            paramsToImport_Label.Text = _paramsToImportNum.ToString() + " | " +
                "Выбранные: " + _selectedParamsNum.ToString();



        }

        private void Param_checkedLB_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (var item in Param_checkedLB.CheckedItems)
                _selectedParams.Add(item.ToString());

            _selectedParams = _selectedParams.Distinct().ToList();

        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            ChangeBtnClicked = true;
            ChangeBtn.Enabled = false;

        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            foreach (var item in Param_checkedLB.CheckedItems)
                _selectedParams.Add(item.ToString());

            _selectedParams = _selectedParams.Distinct().ToList();

            if (_selectedParams.Count != 0)
            {
                foreach (string str in _selectedParams)
                    foreach (ExternalDefinition definition in _allSharedParams)
                        if (str == definition.Name)
                        {
                            SelectedParamsToImport.Add(definition);
                            break;
                        }


                ImportBtnClicked = true;

                ImportBtn.Enabled = false;
                Param_checkedLB.Enabled = false;
                Search_Text.Enabled = false;
                ParamGroup_Combo.Enabled = false;

            }
            else
                MessageBox.Show("Не выбран параметр");

        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            OKBtnClicked = true;
            this.Close();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            CloseBtnClicked = true;
            this.Close();

        }


        private void EditParametersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!OKBtnClicked)
                CloseBtnClicked = true;

        }


        private void Search_Text_Enter(object sender, EventArgs e)
        {
            _selectedParams_copy = _selectedParams;

            //if (Search_Text.Text == "Поиск")
            //{
            //    Search_Text.Text = "";
            //    Search_Text.ForeColor = System.Drawing.Color.Black;
            //}

        }

        private void Search_Text_Leave(object sender, EventArgs e)
        {
            //if (Search_Text.Text.Trim() == "")
            //{
            //    Search_Text.Text = "Поиск";
            //    Search_Text.ForeColor = System.Drawing.Color.Gray;

            //    Param_checkedLB.Items.Clear();
            //    foreach (string str in _forParam_checkedLB)                
            //        Param_checkedLB.Items.Add(str);

            //    // добавить выбранные ранее элементы
            //    if (_selectedParams.Count != 0)
            //        foreach (string str in _selectedParams)
            //            foreach (var item in Param_checkedLB.Items)
            //                if (str == item.ToString())
            //                {
            //                    Param_checkedLB.SetItemChecked(Param_checkedLB.Items.IndexOf(item.ToString()), true);
            //                    break;
            //                }
            //}

        }
        private void Search_Text_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Search_Text.Text) == false)
            {
                Param_checkedLB.Items.Clear();
                foreach (string str in _forParam_checkedLB)
                    if (str.ToLower().Contains(Search_Text.Text.ToLower()))
                        Param_checkedLB.Items.Add(str);

                // добавить выбранные ранее элементы 
                if (_selectedParams.Count != 0)
                {
                    foreach (string str in _selectedParams)
                    {
                        foreach (var item in Param_checkedLB.Items)
                            if (str == item.ToString() /*&& Param_checkedLB.GetItemChecked(Param_checkedLB.Items.IndexOf(item)) != false*/)
                            {
                                Param_checkedLB.SetItemChecked(Param_checkedLB.Items.IndexOf(item.ToString()), true); // Поставлю галочку
                                break;

                            }
                    }

                    for (int i = _selectedParams.Count - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < Param_checkedLB.Items.Count; j++)
                        {
                            if (_selectedParams[i] == Param_checkedLB.Items[j].ToString()
                                && Param_checkedLB.GetItemChecked(j) == false)
                            {
                                Param_checkedLB.SetItemChecked(j, false); // Поставлю галочку

                                //_selectedParams.RemoveAt(i); // Удаляю из _selectedParams отмененные параметры
                                break;
                            }
                        }
                    }

                }
            }
            else
            {
                Param_checkedLB.Items.Clear();
                foreach (string str in _forParam_checkedLB)
                    Param_checkedLB.Items.Add(str);

                // добавить выбранные ранее элементы 
                if (_selectedParams.Count != 0)
                {
                    foreach (string str in _selectedParams)
                    {
                        foreach (var item in Param_checkedLB.Items)
                            if (str == item.ToString() /*&& Param_checkedLB.GetItemChecked(Param_checkedLB.Items.IndexOf(item)) != false*/)
                            {
                                Param_checkedLB.SetItemChecked(Param_checkedLB.Items.IndexOf(item.ToString()), true); // Поставлю галочку
                                break;

                            }
                    }



                    for (int i = _selectedParams.Count - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < Param_checkedLB.Items.Count; j++)
                        {
                            if (_selectedParams[i] == Param_checkedLB.Items[j].ToString()
                                && Param_checkedLB.GetItemChecked(j) == false)
                            {
                                Param_checkedLB.SetItemChecked(j, false); // Поставлю галочку

                                //_selectedParams.RemoveAt(i); // Удаляю из _selectedParams отмененные параметры
                                break;
                            }
                        }
                    }

                }

            }





        }



    }


}

