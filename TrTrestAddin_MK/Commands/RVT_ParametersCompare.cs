using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrTrestAddin_MK.Windows;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Binding = Autodesk.Revit.DB.Binding;


namespace TrTrestAddin_MK.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class RVT_ParametersCompared : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;

            if (!doc.IsFamilyDocument)
            {
                MessageBox.Show("Пожалуйста запустите этот плагин в файле семейство");
                return Result.Failed;
            }
            ////
            //List<FamilyInstance> childNestedFamilies_test = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().ToList();
            ////printList(childNestedFamilies.Select(x => x.Name), nameof(childNestedFamilies));
            //FamilyInstance famInst_test = childNestedFamilies_test.FirstOrDefault();
            //try
            //{
            //    if (doc.FamilyManager.GetAssociatedFamilyParameter(famInst_test.LookupParameter("ADSK_Масса")) == null)
            //    {
            //        MessageBox.Show("can't find association ");

            //    }
            //    else
            //    {
            //        FamilyParameter famParam = doc.FamilyManager.GetAssociatedFamilyParameter(famInst_test.LookupParameter("ADSK_Масса"));
            //        MessageBox.Show(famParam.Definition.Name, "famParam Name");

            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //    throw;
            //}
            //return Result.Failed;

            ////

            #region Тест Вложенные семейство 

            //List<Family> family = new FilteredElementCollector(doc).OfClass(typeof(Family)).Cast<Family>().ToList();
            //family.RemoveAll(x => String.IsNullOrEmpty(x.Name));

            //List<Document> familyDocs = new List<Document>();
            ////Document familyDoc = null;
            //List<FamilyParameter> familyParameters_test = new List<FamilyParameter>();
            //List<string> strList = new List<string>();
            //try
            //{
            //    foreach (var fam in family)
            //        if (fam.IsEditable)
            //        {
            //            Document familyDoc_test = doc.EditFamily(fam);
            //            //familyDoc_test.FamilyManager.GetParameters();
            //            // Если у внутреннего семейство есть еще семейства, то оно загоружаемое (проверим это через коллектор)
            //            List<Family> family_test = new FilteredElementCollector(familyDoc_test).OfClass(typeof(Family)).Cast<Family>().ToList();
            //            family_test.RemoveAll(x => x == null || String.IsNullOrEmpty(x.Name));
            //            printList(family_test.Select(x => x.Name), fam.Name, nameof(family_test));

            //            if (family_test.Count != 0)
            //            {
            //                familyDocs.Add(doc.EditFamily(fam));
            //                //familyDoc = doc.EditFamily(fam);

            //            }
            //            strList.Add(familyDoc_test.Title);
            //        }
            //    //using (Transaction t = new Transaction(doc, "Testing"))
            //    //{
            //    //    Parameter paramToAssociate_test = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().FirstOrDefault().
            //    //        LookupParameter("ADSK_Марка");
            //    //    FamilyParameter famParam_test = doc.FamilyManager.GetParameters().Where(x => x.Definition.Name == paramToAssociate_test.Definition.Name).
            //    //        FirstOrDefault();

            //    //    t.Start();
            //    //    if (paramToAssociate_test != null)
            //    //    {
            //    //        doc.FamilyManager.AssociateElementParameterToFamilyParameter(paramToAssociate_test, famParam_test);
            //    //        MessageBox.Show("Passed");
            //    //    }
            //    //    t.Commit();

            //    //}

            //    ////MessageBox.Show(a.ToString(), "a ");
            //    //MessageBox.Show(String.Join("\n", strList) + "\nCount: " + strList.Count, "strList");
            //    //printList(familyDoc.Select(x => x.Title), nameof(familyDoc));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    throw;
            //}
            //return Result.Failed;
            #endregion

            ////


            IList<FamilyParameter> parameters = doc.FamilyManager.GetParameters();
            //printList(parameters.Select(x=> x.Definition.Name),nameof(parameters));
            //return Result.Failed;

            List<FamilyParameter> familyParameters = new List<FamilyParameter>();
            if (parameters != null)
                foreach (FamilyParameter parameter in parameters)
                    if (parameter.IsShared)
                        familyParameters.Add(parameter); // Добавляем в список sharedParameters только общие параметры из списка всех                 


            // Проверка на существование ФОП
            if (app.Application.OpenSharedParameterFile() == null)
            {
                MessageBox.Show("Не найдено ФОП");
                return Result.Failed;
            }

            List<ExternalDefinition> sharedParametersInFile = new List<ExternalDefinition>();
            foreach (DefinitionGroup group in app.Application.OpenSharedParameterFile().Groups)
                foreach (ExternalDefinition definition in group.Definitions)
                    sharedParametersInFile.Add(definition); // Добавляем в список все общие параметры из ФОП 


            // Создаю списки с параметрами для замены
            List<FamilyParameter> familyParametersToChange = new List<FamilyParameter>(); // Старые общие параметры
            List<ExternalDefinition> sharedParametersToChange = new List<ExternalDefinition>(); // Новые общие параметры
            List<ExternalDefinition> sharedParametersToRemove = new List<ExternalDefinition>(); // Дублирующие общие параметры

            foreach (ExternalDefinition definition in sharedParametersInFile)
                foreach (FamilyParameter familyParameter in familyParameters)
                {
                    string definitionGuid = definition.GUID.ToString();
                    string familyParameterGuid = familyParameter.GUID.ToString();
                    string definitionName = definition.Name;
                    string familyParameterName = familyParameter.Definition.Name;

                    ForgeTypeId specTypeId = familyParameter.Definition.GetDataType();
                    ForgeTypeId shParamSpecTypeId = definition.GetDataType();

                    if (definitionGuid == familyParameterGuid && definitionName != familyParameterName &&
                        shParamSpecTypeId == specTypeId)
                    {
                        familyParametersToChange.Add(familyParameter);
                        sharedParametersToChange.Add(definition);

                    }
                    if (definitionGuid == familyParameterGuid && definitionName == familyParameterName &&
                        shParamSpecTypeId == specTypeId)
                    {
                        sharedParametersToRemove.Add(definition); // Добавляем в список дублированные общие параметры  

                    }

                }

            //////
            //// Association | RollBack
            //using (Transaction t = new Transaction(doc, "Associating paramters"))
            //{
            //    t.Start();
            //    // RollBack type Parameters that changed to instance Parameters                 
            //    FamilyParameter newParamAsFamParam = doc.FamilyManager.GetParameters()
            //        .Where(x => x.IsShared && x.Definition.Name == "DAT_Марка").FirstOrDefault();

            //    doc.FamilyManager.MakeType(newParamAsFamParam);
            //    MessageBox.Show("Associating fam param to element in Parent family");
            //    t.Commit();
            //}
            //return Result.Failed;
            //////


            // Вызываю форму для редактирование параметров
            EditParametersForm frm = new EditParametersForm(app, familyParametersToChange, sharedParametersToChange, sharedParametersToRemove);
            frm.ShowDialog();
            if (frm.CloseBtnClicked)
                return Result.Failed;


            /// Заменяю и/или импортирую общих параметров
            int changedParNum = 0;
            int importedParNum = 0;


            #region Замена 
            ////if (familyDoc != null)
            ////{
            try
            {
                if (frm.ChangeBtnClicked && familyParametersToChange.Count != 0)
                {
                    List<FamilyInstance> familyInstances_test = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().ToList();
                    familyInstances_test.RemoveAll(x => x == null);
                    //List<FamilyInstance> associatedNestedFamInstances = new List<FamilyInstance>();
                    List<Document> familyDocs = new List<Document>();
                    List<Guid> wasTypeParamsGuid = new List<Guid>();
                    bool isAssociated = false;
                    List<FamilyParameter> unAssociatedFamParams = new List<FamilyParameter>();
                    List<string> strTest = new List<string>();

                    IDictionary<FamilyInstance, Guid> associatedNestedFamInstancesAndParams = new Dictionary<FamilyInstance, Guid>();

                    for (int i = 0; i < familyParametersToChange.Count; i++)
                    {
                        foreach (FamilyInstance famInst in familyInstances_test)
                        {
                            FamilyParameter oldParam = familyParametersToChange[i];
                            ExternalDefinition newParam = sharedParametersToChange[i];
                            BuiltInParameterGroup paramGroup_test = oldParam.Definition.ParameterGroup;
                            bool isInstance_test = oldParam.IsInstance;
                            if (!isInstance_test) { isInstance_test = true; } // Чтобы не зависимо от типа привязки парамов в родительском,
                                                                              // во вложенных привязал к экземпляру

                            if (famInst.LookupParameter(oldParam.Definition.Name) != null &&
                                doc.FamilyManager.GetAssociatedFamilyParameter(famInst.LookupParameter(oldParam.Definition.Name)) != null)
                            {
                                //MessageBox.Show(j.ToString() + "\n" + oldParam.Definition.Name, "current iteration, oldParam");

                                Document familyDoc = doc.EditFamily(famInst.Symbol.Family);
                                familyDocs.Add(familyDoc);
                                strTest.Add(famInst.Symbol.Family.Name + " | " + oldParam.Definition.Name);
                                isAssociated = true;
                                associatedNestedFamInstancesAndParams.Add(famInst, oldParam.GUID);

                                if (null != familyDoc.FamilyManager.GetParameters()
                                    .Where(x => x.IsShared && x.Definition.Name == oldParam.Definition.Name).FirstOrDefault()) // Если у семейство уже заменен параметр
                                {
                                    using (Transaction t = new Transaction(familyDoc, "famDoc DeleteAdd"))
                                    {
                                        t.Start();

                                        //MessageBox.Show(j.ToString() + "\n" + famInst.Id.ToString()+ " | " + oldParam.Definition.Name, 
                                        //    "current iteration, inst_Id, oldParam");
                                        //MessageBox.Show(j.ToString() + "\n" + oldParam.Definition.Name, "Before Delete");
                                        familyDoc.Delete(oldParam.Id);
                                        //MessageBox.Show("After Delete");
                                        //MessageBox.Show(j.ToString() + "\n" + oldParam.Definition.Name, "After Delete");

                                        familyDoc.FamilyManager.AddParameter(newParam, paramGroup_test, isInstance_test);

                                        t.Commit();


                                    }
                                }

                            }
                        }
                        if (!isAssociated) { unAssociatedFamParams.Add(familyParametersToChange[i]); }
                        isAssociated = false;

                    }
                    //MessageBox.Show("Deleted, Added in child Families");
                    //return Result.Failed;

                    //familyDocs = familyDocs.Distinct(new DocumentEqualityComparer()).ToList();
                    ////
                    //associatedNestedFamInstancesAndParams.Add(associatedNestedFamInstancesAndParams.FirstOrDefault().Key,
                    //    null);

                    //printList(associatedNestedFamInstancesAndParams.Select(x => x.Key.Name + " : " + x.Key.Id + " | " + x.Value.Definition.Name),
                    //    nameof(associatedNestedFamInstancesAndParams));

                    associatedNestedFamInstancesAndParams = associatedNestedFamInstancesAndParams.Distinct()
                        .ToDictionary(x => x.Key, x => x.Value);

                    //printList(associatedNestedFamInstancesAndParams.Select(x => x.Key.Name + " : " + x.Key.Id + " | " + x.Value.Definition.Name),
                    //    nameof(associatedNestedFamInstancesAndParams));
                    //return;

                    //printList(familyDocs.Select(x => x.Title), nameof(familyDocs));
                    //printList(strTest, nameof(strTest));
                    //printList(unAssociatedFamParams.Select(x => x.Definition.Name), nameof(unAssociatedFamParams));
                    //return Result.Failed;



                    for (int i = 0; i < familyParametersToChange.Count; i++)
                    {
                        //for (int j = 0; j < childNestedFamilies.Count; j++)
                        //{
                        //    if (doc.FamilyManager.GetAssociatedFamilyParameter(childNestedFamilies[j].
                        //        LookupParameter(familyParametersToChange[i].Definition.Name)) == null) { continue; }

                        //    Document familyDoc = doc.EditFamily(childNestedFamilies[j].Symbol.Family);

                        ////
                        ///
                        //BuiltInParameterGroup paramGroup = familyParametersToChange[i].Definition.ParameterGroup;
                        //bool isInstance = familyParametersToChange[i].IsInstance;

                        //using (Transaction t = new Transaction(familyDoc, "bufParChanging"))
                        //{
                        //    try
                        //    {
                        //        t.Start();
                        //        familyDoc.FamilyManager.ReplaceParameter(familyParametersToChange[i], "bufParam" + i.ToString(), paramGroup, isInstance);
                        //        t.Commit();

                        //    }
                        //    catch (Exception ex) { MessageBox.Show(ex.Message); }

                        //}
                        //FamilyParameter bufParam_famdoc = familyDoc.FamilyManager.get_Parameter("bufParam" + i.ToString());
                        //using (Transaction t = new Transaction(familyDoc, "famParRemoving"))
                        //{
                        //    try
                        //    {
                        //        t.Start();
                        //        familyDoc.FamilyManager.RemoveParameter(familyParametersToChange[i]);
                        //        t.Commit();

                        //    }
                        //    catch (Exception ex) { MessageBox.Show(ex.Message); }

                        //}
                        //using (Transaction t = new Transaction(familyDoc, "bufParReplacing"))
                        //{
                        //    try
                        //    {
                        //        t.Start();
                        //        familyDoc.FamilyManager.ReplaceParameter(bufParam_famdoc, sharedParametersToChange[i], paramGroup, isInstance);
                        //        changedParNum++;
                        //        t.Commit();

                        //    }
                        //    catch (Exception ex) { MessageBox.Show(ex.Message); }

                        //}

                        //// Get all oldParam references
                        FamilyParameter oldParam = familyParametersToChange[i];
                        ExternalDefinition newParam = sharedParametersToChange[i];
                        BuiltInParameterGroup paramGroup_test = oldParam.Definition.ParameterGroup;
                        bool isInstance = oldParam.IsInstance;
                        if (!isInstance) // Нужно для привязки параметра элемента к параметру семейство
                        {
                            isInstance = true;
                            wasTypeParamsGuid.Add(oldParam.GUID);

                        }

                        // Get old prarameter value
                        double oldParamValue_Double = 0;
                        ElementId oldParamValue_ElementID = null;
                        int oldParamValue_Integer = 0;
                        string oldParamValue_String = null;
                        foreach (FamilyType famType in doc.FamilyManager.Types.Cast<FamilyType>())
                        {
                            if (famType.HasValue(familyParametersToChange[i]))
                            {
                                switch (familyParametersToChange[i].StorageType)
                                {
                                    case StorageType.Integer:
                                        oldParamValue_Integer = (int)famType.AsInteger(familyParametersToChange[i]);
                                        break;

                                    case StorageType.Double:
                                        oldParamValue_Double = (double)famType.AsDouble(familyParametersToChange[i]);
                                        break;

                                    case StorageType.String:
                                        oldParamValue_String = famType.AsString(familyParametersToChange[i]);
                                        break;

                                    case StorageType.ElementId:
                                        oldParamValue_ElementID = famType.AsElementId(familyParametersToChange[i]);
                                        break;


                                }
                            }
                        }

                        string oldParamFormula = oldParam.Formula;
                        string oldParamName = oldParam.Definition.Name;
                        string newParamName = newParam.Name;

                        //// Delete, Add Parameter in Nested Family
                        //foreach (Document familyDoc in familyDocs)
                        //    using (Transaction t = new Transaction(familyDoc, "famDoc DeleteAdd"))
                        //    {
                        //        t.Start();

                        //        familyDoc.Delete(oldParam.Id);
                        //        familyDoc.FamilyManager.AddParameter(newParam, paramGroup_test, isInstance_test);

                        //        //MessageBox.Show("Deleted, Added in Nested Family");

                        //        t.Commit();


                        //    }


                        // Delete, Add Parameter in parent family and set all referencess
                        using (Transaction t = new Transaction(doc, "DeleteAdd and set refs"))
                        {
                            t.Start();

                            doc.Delete(oldParam.Id);
                            doc.FamilyManager.AddParameter(newParam, paramGroup_test, isInstance);

                            t.Commit();

                            //// Load Nested Family to Parent family
                            ////if (!firstIterationPassed)
                            ////{
                            //foreach (Document familyDoc in familyDocs)
                            //{
                            //    //foreach (FamilyInstance famInst in associatedNestedFamInstances.Distinct(new FamilyInstanceEqualityComparer()))
                            //    //{
                            //    //    Document familyDoc = doc.EditFamily(famInst.Symbol.Family);                                    
                            //    //    if (famInst.LookupParameter(newParam.Name) != null)
                            //    //    {
                            //    //        LoadOpts loadOptions = new LoadOpts();
                            //    //        familyDoc.LoadFamily(doc, loadOptions);
                            //    //    }
                            //    //}

                            //    ////if (null != familyDoc.FamilyManager.GetParameters().Where(x => x.IsShared && x.Definition.Name == newParam.Name).FirstOrDefault())
                            //    ////{
                            //    LoadOpts loadOptions = new LoadOpts();
                            //    familyDoc.LoadFamily(doc, loadOptions); // Можно улучшить количество итераций в будущем
                            //                                            //                                        //}
                            //                                            //firstIterationPassed = true;

                            //}
                            ////}

                            FamilyParameter newParamAsFamParam = doc.FamilyManager.GetParameters().
                                Where(x => x.IsShared && x.Definition.Name == newParam.Name).FirstOrDefault();

                            ////List<FamilyInstance> familyInstances = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).
                            ////     Cast<FamilyInstance>().ToList();
                            ////familyInstances.RemoveAll(x => x == null || String.IsNullOrEmpty(x.Name));


                            //Parameter paramToAssociate = famInst.LookupParameter(newParamName);
                            //Parameter paramToAssociate = childNestedFamilies[j].LookupParameter(newParamName);
                            //if /*(paramToAssociate != null)*/ (true)
                            //{


                            //// Set all oldParam references to newParam
                            t.Start();

                            // Set Value
                            if (oldParamValue_Double != 0) { doc.FamilyManager.Set(newParamAsFamParam, oldParamValue_Double); }
                            else if (oldParamValue_ElementID != null) { doc.FamilyManager.Set(newParamAsFamParam, oldParamValue_ElementID); }
                            else if (oldParamValue_Integer != 0) { doc.FamilyManager.Set(newParamAsFamParam, oldParamValue_Integer); }
                            else if (!String.IsNullOrEmpty(oldParamValue_String)) { doc.FamilyManager.Set(newParamAsFamParam, oldParamValue_String); }
                            // Set Formula
                            if (!String.IsNullOrEmpty(oldParamFormula)) { doc.FamilyManager.SetFormula(newParamAsFamParam, oldParamFormula); }

                            //// Associate ElementParameter To FamilyParameter
                            //foreach (FamilyInstance famInst in associatedNestedFamInstances)
                            //{
                            //    Parameter paramToAssociate = famInst.LookupParameter(newParamName);
                            //    if (paramToAssociate != null) { doc.FamilyManager.AssociateElementParameterToFamilyParameter(paramToAssociate, newParamAsFamParam); }
                            //}
                            //// Assosiated back to type (if changed)
                            //if (wasType) { doc.FamilyManager.MakeType(newParamAsFamParam); }

                            t.Commit();


                            //}






                        }


                        //
                        //}
                        //
                    }
                    //return Result.Failed;

                    // Load Nested Family to Parent family
                    foreach (Document familyDoc in familyDocs)
                    {
                        LoadOpts loadOptions = new LoadOpts();
                        familyDoc.LoadFamily(doc, loadOptions);

                    }

                    // Association | RollBack
                    using (Transaction t = new Transaction(doc, "Associating paramters"))
                    {
                        t.Start();
                        // Associate ElementParameter To FamilyParameter
                        foreach (ExternalDefinition newParam in sharedParametersToChange)
                        {
                            FamilyParameter newParamAsFamParam = doc.FamilyManager.GetParameters()
                                .Where(x => x.IsShared && x.Definition.Name == newParam.Name).FirstOrDefault();
                            foreach (var famInst in associatedNestedFamInstancesAndParams)
                            {
                                //Parameter paramToAssociate = famInst.LookupParameter(newParam.Name);
                                if (famInst.Value == newParamAsFamParam.GUID)
                                {
                                    Parameter paramToAssociate = famInst.Key.LookupParameter(newParam.Name);
                                    if (paramToAssociate != null)
                                        doc.FamilyManager.AssociateElementParameterToFamilyParameter(paramToAssociate, newParamAsFamParam);  // Done


                                }

                            }

                        }
                        // RollBack type Parameters that changed to instance Parameters 
                        foreach (ExternalDefinition newParam in sharedParametersToChange) // во избежание конфликтов
                        {
                            FamilyParameter newParamAsFamParam = doc.FamilyManager.GetParameters()
                                .Where(x => x.IsShared && x.Definition.Name == newParam.Name).FirstOrDefault();
                            foreach (Guid oldFamParamGuid in wasTypeParamsGuid)
                            {
                                if (oldFamParamGuid == newParamAsFamParam.GUID)
                                {
                                    doc.FamilyManager.MakeType(newParamAsFamParam);
                                    //MessageBox.Show("Associating fam param to element in Parent family");

                                    break;

                                }

                            }
                        }
                        t.Commit();
                    }



                }
                //// Update Catalogue File 
                //string catalFilePathName = doc.PathName.Replace(".rfa", ".txt");
                //if (File.Exists(catalFilePathName))
                //{
                //    string text = File.ReadAllText(catalFilePathName);
                //    text = text.Replace(oldParamName, newParamName);
                //    File.WriteAllText(catalFilePathName, text);

                //    MessageBox.Show("Type Catalogue file content changed");
                //}
                //else MessageBox.Show("Can't find Type Catalogue file in a folder");
                //////
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); throw; }

            ////}
            ////else
            ////{
            //if (frm.ChangeBtnClicked && familyParametersToChange.Count != 0)
            //    for (int i = 0; i < familyParametersToChange.Count; i++)
            //    {
            //        BuiltInParameterGroup paramGroup = familyParametersToChange[i].Definition.ParameterGroup;
            //        bool isInstance = familyParametersToChange[i].IsInstance;

            //        using (Transaction t = new Transaction(doc, "bufParChanging"))
            //        {
            //            try
            //            {
            //                t.Start();
            //                doc.FamilyManager.ReplaceParameter(familyParametersToChange[i], "bufParam" + i.ToString(), paramGroup, isInstance);
            //                t.Commit();
            //            }
            //            catch (Exception ex) { MessageBox.Show(ex.Message); }

            //        }
            //        FamilyParameter bufParam = doc.FamilyManager.get_Parameter("bufParam" + i.ToString());
            //        using (Transaction t = new Transaction(doc, "famParRemoving"))
            //        {
            //            try
            //            {
            //                t.Start();
            //                doc.FamilyManager.RemoveParameter(familyParametersToChange[i]);
            //                t.Commit();
            //            }
            //            catch (Exception ex) { MessageBox.Show(ex.Message); }

            //        }
            //        using (Transaction t = new Transaction(doc, "bufParReplacing"))
            //        {
            //            try
            //            {
            //                t.Start();
            //                doc.FamilyManager.ReplaceParameter(bufParam, sharedParametersToChange[i], paramGroup, isInstance);
            //                changedParNum++;
            //                t.Commit();
            //            }
            //            catch (Exception ex) { MessageBox.Show(ex.Message); }

            //        }

            //    }

            //}
            #endregion

            //if (frm.ImportBtnClicked && frm.SelectedParamsToImport.Count != 0)
            //    using (Transaction t = new Transaction(doc, "addParam"))
            //    {
            //        try
            //        {
            //            t.Start();

            //            foreach (ExternalDefinition definition in frm.SelectedParamsToImport)
            //            {
            //                doc.FamilyManager.AddParameter(definition, frm.ParameterGroup, frm.IsInstance);
            //                importedParNum++;
            //            }

            //            t.Commit();
            //        }
            //        catch (Exception ex) { MessageBox.Show(ex.Message); }

            //    }

            MessageBox.Show(changedParNum.ToString() + " общих параметров заменено" + "\n" +
                importedParNum.ToString() + " общих параметров импортировано");

            return Result.Succeeded;



        }

        public void printList(IEnumerable<string> list, string nameOfList)
        {
            var list_test = list.ToList();
            MessageBox.Show(String.Join("\n", list_test) + "\n \n" + nameOfList + ".Count: " + list_test.Count, nameOfList + " Items and count");

        }

        public void printList(IEnumerable<string> list, string title, string nameOfList)
        {
            var list_test = list.ToList();
            MessageBox.Show(title + "\n" + String.Join("\n", list_test) + "\n \n" + nameOfList + ".Count: " + list_test.Count, nameOfList + " Items and count");

        }

    }

    public class ShParameterEqualityComparer : IEqualityComparer<SharedParameterElement>
    {
        public bool Equals(SharedParameterElement x, SharedParameterElement y)
        {
            return x.GetDefinition().Name.Equals(y.GetDefinition().Name);
        }

        public int GetHashCode(SharedParameterElement obj)
        {
            return obj.GetDefinition().Name.GetHashCode();
        }

    }

    public class FamilyParameterEqualityComparer : IEqualityComparer<FamilyParameter>
    {
        public bool Equals(FamilyParameter x, FamilyParameter y)
        {
            return x.Definition.Name.Equals(y.Definition.Name);
        }

        public int GetHashCode(FamilyParameter obj)
        {
            return obj.Definition.Name.GetHashCode();
        }

    }

    public class DocumentEqualityComparer : IEqualityComparer<Document>
    {
        public bool Equals(Document x, Document y)
        {
            return x.Title.Equals(y.Title);
        }

        public int GetHashCode(Document obj)
        {
            return obj.Title.GetHashCode();
        }

    }
    public class FamilyInstanceEqualityComparer : IEqualityComparer<FamilyInstance>
    {
        public bool Equals(FamilyInstance x, FamilyInstance y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(FamilyInstance obj)
        {
            return obj.Name.GetHashCode();
        }

    }

    class LoadOpts : IFamilyLoadOptions
    {
        public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
        {
            overwriteParameterValues = true;
            return true;
        }

        public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
        {
            source = FamilySource.Family;
            overwriteParameterValues = true;
            return true;
        }
    }
}
