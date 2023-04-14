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

namespace TrTrestAddin_MK.Commands
{
    /*  -                                   Задачи
        * 1. Заполнить аттрибутов "ADSK_Количества" и "ADSK_Масса" и "Наименование"   -> Выполнена 
        * 2. Управлять количество столбцов во время создания, путем преждевременное измерение их количества и поправки, т.е. добавление в случае < Matrix.Length(1) и удаление в случае > Matrix.Getlentgh(1)
               -> Выполнена
        * 3 MergeCells составляющих элементов ограждений по "ОГЛ" -> Выполнена
        * 4. Подправить размеры столбцов и строк под контента -> Выполнена
        * 5. Добавить в начале списке Строка заголовки с названием Столбцов и потом уже остальное, но это напоследок -> Выполнена
        * 6. Упорядочить по ADSK_Марка -> Выполнена 
        * 7. Исправить ситуацию с Active.ViewSchedule сделать независимым -> Выполнена
        * 8. Обновление таблицы при повторном использование -> Выполнена
        * 9. Полоса поле "Наименование подправить согласно инструкции Рената на бумаге" -> Выполнена
        * 10. Статические размеры для столбцов таблицы -> Выполнена
        * 11. TableSeсtionData.SetCellStyle() - Оформление: название шрифта, размер шрифта, Alignment -> Выполнена
        * 12. Подправить размер строк -> Выполнена
        * 13. Исправить Суммарник массы и Отображение Количество -> Выполнена         
        * 14. Исправить поле наименование, улучшить -> Выполнена
        * 15. Поле наименование "Пруток" исправить вид -> Выполнена

        * 16. Создаем программно вид спецификаций для каждой семейств ограждений (пока 4), можно сделать динамический от количество разных семейств (например в случае кровли) -> Выполнена
        * 17. Проблема с "L=0" в какой-то спецификации -> Выполнена
        * 18. На первой строке таблицы написать имя спецификации, все остальное после него: выравнивание по центру, все границу вокруг кроме нижней выключить, высота 10мм -> Выполнена
        * 19. Перенос часть таблицы после 8-го образца (Еще раз уточнить номер образца), можно создать новую спецификацию после n-ой ограждений -> Выполнено
             19.1 сортировать ограждений и их состав-эл-ты по параметру "ADSK_Позиция вед...", но список имеет и буквы и цифры -> Выполнено
             19.2 специальные str_naim и str_naim_sech_prof для мет-рещетки (наим-краткое находится в типе а не в экземпляре как "ТРУБА О") -> Выполнено
             19.3 Удаление спецификации перед срабатывание плагина, чтобы не было оставленных спецификации, если пользователь уменшил количество ограждений -> 
             19.4 Перенос после определенной высоты, а не после 8-го образца, можно считать высоту каждой строке, нужна определенная высота (475 мм пока что с учетом новой заголовки (имя спецификации)), 
                        при превышение последнюю ограждению перенести на новой спецфк-ии, даже если только одна строка превышает. 
                        Сделать регулируемую высоту для переноса -> Выполнено
        * 20. TableSeсtionData.SetCellStyle() - жирные границы для каждой ограждений -> Выполнено
        * 21. Исправить ситуацию с поле Количество в огр. Кровля -> Выполнено
        * 22. Исчезают Пробели в наименование составляющих элементов, например у огр. кровля у полосы в позиции обозначение материала - Выполнено
        * 23. В названии спецификации в конце добавить :   (Поменяйте названию) -> Выполнено
        * 24. Наследование название при обновление -> Выполнена
        * 25. Динамическое создание новых спецификаций, для любых (новых) семейств ограждений, а не только определенных (как сейчас)
                Сделать отдельных спецификаций если ограждений кровли имееют разную "Описание" (как будто это другие ограждений на уровне лоджий, поручень и т.д.) -> 
        * 26. Имя спецификации по регламенту (Ренат) - Выполнено
        * 27. Отчет по типовым ошибкам - Выполнено
        * 28. Сделать переменные для lookupParameter, потому, что название параметров могут в будущем изменятся - Выполнено

        * Если проект прошел все тесты :  - Выполнено
            * Найти все недоработанные моменты и доработать, искать в комментах - Выполнено 
            * Code Refactoring - Выполнено
            * Закоментить все, чтобы другие разрабы разбирались в данном коде - Выполнено

    ##################################################################################################################################################################################################



                       Добавленные фичи и изменение (решенные задачи)
         *                                28.11
           1 задача
           2 задача
         *                                29.11 
           3 задача
           4 задача
           5 задача
           6 задача
         *                                01.12
           7 задача                                
         *                                02.12
           8 задача     
           9 задача
           10 задача
         *                                05.12
           11 задача
           13 задача
         *                                06.12
           14 задача
         *                                12.12
           15 задача
           16 задача
         *                                17.02

           16 задача
          *                               10.03
           17 задача
           19.1 задача
           19.2 задача
                                          13.03
           18 задача
                                          15.03
           20 задача
           19.3 задача
                                          16.03
           21 задача
           22 задача
           23 задача
                                          24.03
           24 задача
                                          30.03
           19.4 задача
           19 задача
                                          31.03
           25 задача
                                          04.04
           26 задача
                                          07.04
           27 задача
           28 задача
                                      ПРОЕКТ ЗАВЕРШЕН
        */


    [Transaction(TransactionMode.Manual)]
    public class AR_RolledSteel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            #region Все используемые параметры и значения - Если название параметра изменяются, то подправить их ниже
            // LookUpParameter()
            string opisaniye = "Описание";
            string ADSK_NaznacheniyeVida = "ADSK_Назначение вида";
            string ADSK_GruppaModeli = "Группа модели";
            string ADSK_Marka = "ADSK_Марка";
            string ADSK_PozitsiyaVedEl = "ADSK_Позиция ведомость элементов";
            string ADSK_Naimenovaniye = "ADSK_Наименование";
            string ADSK_NaimenovaniyeKratk = "ADSK_Наименование краткое";
            string ADSK_RazmerDiametr = "ADSK_Размер_Диаметр";
            string ADSK_RazmerDlina = "ADSK_Размер_Длина";
            string sostoyaniyePostavki = "Состояние поставки";
            string ADSK_DiametrArmaturi = "ADSK_Диаметр арматуры";
            string klassArmaturi = "Класс арматуры";
            string ADSK_Oboznacheniye = "ADSK_Обозначение";
            string secheniyeTrubi = "Сечение трубы";
            string secheniyeUgolka = "Сечение уголка";
            string secheniyeProfilya = "Сечение профиля";
            string secheniyePolosi = "Сечение полосы";
            string tochnostProkatki = "Точность прокатки";
            string klassSerpovidnosti = "Класс серповидности";
            string ADSK_Material = "ADSK_Материал";
            string ADSK_MaterialNaimen = "ADSK_Материал наименование";
            string ADSK_MaterialOboznach = "ADSK_Материал обозначение";
            string ADSK_Massa = "ADSK_Масса";
            string ADSK_Kolichestvo = "ADSK_Количество";
            // AsValueString()
            string asVal_MetallicheskiyeKonstr = "Металлические конструкции";
            string asVal_BoltAnkerniy = "Болт анкерный";
            string asVal_ProkatArmaturniy = "Прокат арматурный для железобетонных конструкций";
            #endregion

            // Вызываю Форму
            AR_RolledSteelInputForm inputFrm = new AR_RolledSteelInputForm();
            inputFrm.ShowDialog();
            if (inputFrm.isCloseBtnClicked)
                return Result.Failed;
            int scheduleHeight = inputFrm.schHeight - 25; // убираю высоту первых двух строк (заголовок и название столбцов), так как не входят в массив (добавляются прямо в таблицу)

            #region Main Code

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");

                #region Подготовка
                // Получаю экземпляры все ограждений в проекте
                List<FamilyInstance> allFencesInstances = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StairsRailing).WhereElementIsNotElementType()
                 .Cast<FamilyInstance>().OrderBy(fam => fam.Symbol.LookupParameter(opisaniye).AsValueString()).ToList();

                // Получаю все экземпляры спецификаций в проекте (нужен для дальнешей обработки)
                List<ViewSchedule> viewSchedules = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Schedules).WhereElementIsNotElementType().
                    Where(v => v.LookupParameter(ADSK_NaznacheniyeVida) != null).Cast<ViewSchedule>().ToList();
                List<ViewSchedule> rolledSteelViewSchedules = viewSchedules.Where(v => v.LookupParameter(ADSK_NaznacheniyeVida).AsValueString() == asVal_MetallicheskiyeKonstr).ToList();


                // Удаление ненужных спецификаций (выборочное)
                if (allFencesInstances.Count != 0)
                {
                    if (rolledSteelViewSchedules.Count != 0)
                    {
                        var leftFences = allFencesInstances.Select(fam => fam.SuperComponent == null ? fam.Symbol.LookupParameter(opisaniye).AsValueString() :
                        (fam.SuperComponent as FamilyInstance).Symbol.LookupParameter(opisaniye).AsValueString()).Distinct().ToList(); // для уменьшение объема данных
                        var result = rolledSteelViewSchedules.Where(v => !leftFences.Any(s => v.Name.Contains(s))).ToList();
                        foreach (var view in result)
                        {
                            doc.Delete(view.Id);
                        }
                    }
                }
                else
                {
                    // Удаление ненужных спецификаций (Всех)
                    if (rolledSteelViewSchedules.Count != 0)
                    {
                        if (MessageBox.Show("Не найдено ни одного экземпляра ограждения в проекте. \nХотите удалить все спецификации металлических конструкций?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (var view in rolledSteelViewSchedules)
                            {
                                doc.Delete(view.Id);
                            }
                            // user clicked yes
                        }
                        else
                        {
                            // user clicked no
                            return Result.Failed;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не найдено ни одного экземпляра ограждения в проекте", "Внимание!");
                        return Result.Failed;
                    }
                }
                //

                // Проверка на отсутствие значение у параметра "Описания"
                List<string> wrongFences = new List<string>();
                foreach (var item in allFencesInstances)
                {
                    if (item.SuperComponent != null && ((item.SuperComponent as FamilyInstance).Symbol.LookupParameter(opisaniye).AsValueString() == null ||
                        (item.SuperComponent as FamilyInstance).Symbol.LookupParameter(opisaniye).AsValueString().Trim() == ""))
                    {
                        wrongFences.Add((item.SuperComponent as FamilyInstance).Symbol.Name);
                    }
                    if (item.SuperComponent == null && (item.Symbol.LookupParameter(opisaniye).AsValueString() == null || item.Symbol.LookupParameter(opisaniye).AsValueString().Trim() == ""))
                    {
                        wrongFences.Add(item.Name);
                    }
                }

                //Форма для заполнение ограждений у которых отсутствует параметр "Описания"
                AR_RolledSteelEditingForm editFrm = new AR_RolledSteelEditingForm(wrongFences);
                if (wrongFences.Count > 0)
                {
                    editFrm.ShowDialog();
                    if (editFrm.isCloseBtnClicked == true)
                    {
                        return Result.Failed;
                    }

                    for (int i = 0; i < allFencesInstances.Count; i++)
                    {
                        for (int j = 0; j < editFrm.fencesNames.Count; j++)
                        {
                            if (allFencesInstances[i].SuperComponent == null && allFencesInstances[i].Symbol.Name == editFrm.fencesNames[j])
                            {
                                allFencesInstances[i].Symbol.LookupParameter(opisaniye).Set(editFrm.fencesDescriptions[j]);
                                break;
                            }
                            if (allFencesInstances[i].SuperComponent != null && (allFencesInstances[i].SuperComponent as FamilyInstance).Symbol.Name == editFrm.fencesNames[j])
                            {
                                (allFencesInstances[i].SuperComponent as FamilyInstance).Symbol.LookupParameter(opisaniye).Set(editFrm.fencesDescriptions[j]);
                                break;
                            }
                        }
                    }
                }
                //

                // Группирую ограждений по параметру "Описания"
                List<List<FamilyInstance>> fencesInstancesByGroupModel = new List<List<FamilyInstance>>();
                fencesInstancesByGroupModel.Add(new List<FamilyInstance>());
                int nestedListIndex = 0;
                for (int i = 0; i < allFencesInstances.Count; i++)
                {
                    if (i != 0 && (allFencesInstances[i].Symbol.LookupParameter(opisaniye).AsValueString() != allFencesInstances[i - 1].Symbol.LookupParameter(opisaniye).AsValueString()))
                    {
                        fencesInstancesByGroupModel.Add(new List<FamilyInstance>());
                        nestedListIndex++;
                        fencesInstancesByGroupModel[nestedListIndex].Add(allFencesInstances[i]);
                    }
                    else
                        fencesInstancesByGroupModel[nestedListIndex].Add(allFencesInstances[i]);
                }
                #endregion


                #region Начинаю Создание спецификации
                foreach (List<FamilyInstance> item in fencesInstancesByGroupModel)
                {
                    List<FamilyInstance> fencesInstances = item;
                    string vsName = "";
                    string modelGroupValue = "";

                    if (fencesInstances.Count != 0)
                    {
                        // Проверка на существование многоуровневых ограждений                    
                        if (fencesInstances[0].SuperComponent != null)
                        {
                            fencesInstances = fencesInstances.Select(fam => fam.SuperComponent).Cast<FamilyInstance>().ToList();
                            vsName = "О_" + fencesInstances[0].Symbol.LookupParameter(opisaniye).AsValueString() + "_#";
                            modelGroupValue = fencesInstances[0].Symbol.LookupParameter(ADSK_GruppaModeli).AsValueString();
                            // Сортировка ограждений по ADSK_Marka
                            List<string> sorted_stringList = listSort(fencesInstances.Select(fam => fam.Symbol.LookupParameter(ADSK_Marka).AsValueString()).ToList());
                            fencesInstances = fencesInstances.OrderBy(fam => sorted_stringList.IndexOf(fam.Symbol.LookupParameter(ADSK_Marka).AsValueString())).ToList(); // Получаю отсортированных ограждений
                            fencesInstances = fencesInstances.Select(fam => doc.GetElement(fam.GetSubComponentIds().FirstOrDefault())).Cast<FamilyInstance>().ToList(); // Получено
                        }
                        else
                        {
                            vsName = "О_" + fencesInstances[0].Symbol.LookupParameter(opisaniye).AsValueString() + "_#";
                            modelGroupValue = fencesInstances[0].Symbol.LookupParameter(ADSK_GruppaModeli).AsValueString();
                            List<string> sorted_stringList = listSort(fencesInstances.Select(fam => fam.Symbol.LookupParameter(ADSK_Marka).AsValueString()).ToList());
                            fencesInstances = fencesInstances.OrderBy(fam => sorted_stringList.IndexOf(fam.Symbol.LookupParameter(ADSK_Marka).AsValueString())).ToList(); // Получаю отсортированных ограждений
                        }


                        #region Получаю размер строков массива

                        List<List<FamilyInstance>> listofList_FencesExamplesDistinct = new List<List<FamilyInstance>>(); // Список из несколько списков по 8-образцовых экземпляров лоджий
                        List<int> listOf_MatrixRowCount = new List<int>(); // Для получение количество строк массивов
                        bool resultFailed = false;
                        RowCountDeterMine(doc, fencesInstances, ref listofList_FencesExamplesDistinct, ref listOf_MatrixRowCount, scheduleHeight, ref resultFailed, ADSK_Marka, ADSK_PozitsiyaVedEl);
                        if (resultFailed)
                            return Result.Failed;
                        #endregion


                        #region Объявляю массив с определеннымы размерамы

                        // Массив огр. лоджий
                        List<string[,]> ListOfMatrixes = new List<string[,]>(); // Список массивов Лоджий - несколько массивов потому что несколько спецификации (каждый по 8-образцовых ограждений)
                        List<string[]> ListOfArrays_Arr_sech_prof_and_oboznach = new List<string[]>();
                        List<string[]> ListOfArrays_Arr_mat_and_mat_Oboznach = new List<string[]>();
                        ArrayDeclare(ListOfMatrixes, ListOfArrays_Arr_sech_prof_and_oboznach, ListOfArrays_Arr_mat_and_mat_Oboznach,
                            listOf_MatrixRowCount, listofList_FencesExamplesDistinct);

                        #endregion


                        #region Создаю вложенный список, для обработки данных, чтобы потом поставить в массив и добавляю суммарники для "ADSK_Количества" и ADSK_Massa 


                        List<List<List<FamilyInstance>>> ListOfListsOfLists_fencesExamples = new List<List<List<FamilyInstance>>>(); // Конечный список обрабативаемых данных - далее массив будет заполнятся именно по этому списку
                        List<List<List<int>>> ListOfListsOfLists_FExamples_eachDetail_Amount = new List<List<List<int>>>(); // Для поле "Количество" (Количество каждый сост-элемент ограждений)
                        List<List<double>> ListOfLists_FExamples_Weight_Amount = new List<List<double>>(); // Для поле "Масса изделия" (Сумма масс сост-элементов ограждений)
                        BeforeArray2DFilling(listofList_FencesExamplesDistinct, ref ListOfListsOfLists_fencesExamples, ref ListOfListsOfLists_FExamples_eachDetail_Amount, ref ListOfLists_FExamples_Weight_Amount, doc,
                            ADSK_Naimenovaniye, asVal_BoltAnkerniy, ADSK_Massa, ADSK_Kolichestvo, ADSK_PozitsiyaVedEl);

                        #endregion


                        #region Собираю все в массив, чтобы потом поставить в заголовку (как таблица) 

                        PutAllAtArray2d(ListOfListsOfLists_fencesExamples, listofList_FencesExamplesDistinct, ListOfMatrixes, ListOfListsOfLists_FExamples_eachDetail_Amount, ListOfLists_FExamples_Weight_Amount,
                            ListOfArrays_Arr_sech_prof_and_oboznach, ListOfArrays_Arr_mat_and_mat_Oboznach, doc,
                             ADSK_Naimenovaniye, asVal_BoltAnkerniy, ADSK_Marka, ADSK_PozitsiyaVedEl, ADSK_NaimenovaniyeKratk, ADSK_RazmerDiametr, ADSK_RazmerDlina,
                             asVal_ProkatArmaturniy, sostoyaniyePostavki, ADSK_DiametrArmaturi, klassArmaturi, ADSK_Massa, ADSK_Oboznacheniye, secheniyeTrubi, secheniyeUgolka,
                             secheniyeProfilya, secheniyePolosi, tochnostProkatki, klassSerpovidnosti, ADSK_Material, ADSK_MaterialNaimen, ADSK_MaterialOboznach);

                        #endregion


                        #region Этап поставки в заголовке
                        bool resultFailed_2 = false;
                        PutAtTableHeader(ListOfListsOfLists_fencesExamples, allFencesInstances, viewSchedules, ListOfMatrixes, ListOfArrays_Arr_sech_prof_and_oboznach,
                            ListOfArrays_Arr_mat_and_mat_Oboznach, vsName, modelGroupValue, doc, ref resultFailed_2, ADSK_NaznacheniyeVida);
                        if (resultFailed)
                            return Result.Failed;
                        #endregion
                    }
                }
                ////
                #endregion

                MessageBox.Show("Успешно!", "Внимание!");
                tx.Commit();
            }
            #endregion

            return Result.Succeeded;
        }

        static void RowCountDeterMine(Document doc, List<FamilyInstance> allFencesInstances, ref List<List<FamilyInstance>> listofList_FencesExamplesDistinct,
            ref List<int> listOf_MatrixRowCount, int scheduleHeight, ref bool resultFailed, string ADSK_Marka, string ADSK_PozitsiyaVedEl)
        {
            List<FamilyInstance> fencesExamplesDistinct = new List<FamilyInstance>();
            // Получаю образцовые ограждений, с каждого типа (путем удаления похожих экземпляров)

            if (allFencesInstances.FirstOrDefault().SuperComponent != null)
            {
                fencesExamplesDistinct = allFencesInstances.DistinctBy(fam => (fam.SuperComponent as FamilyInstance).Symbol.LookupParameter(ADSK_Marka).AsValueString()).ToList(); // если огр. "Мет-решетка" (она 3-х уровеневая, а все другие 2-х уровневые)
            }
            else
            {
                fencesExamplesDistinct = allFencesInstances.DistinctBy(fam => fam.Symbol.LookupParameter(ADSK_Marka).AsValueString()).ToList(); // если обычные огр.
            }
            //

            // Разделяю список образцовых ограждений на несколько таких списков по 8 образцов
            List<FamilyInstance> listFamTest = new List<FamilyInstance>();

            List<int> rowCounts = new List<int>();
            foreach (var item in fencesExamplesDistinct)
            {
                rowCounts.Add(item.GetSubComponentIds().Select(elId => doc.GetElement(elId) as FamilyInstance).DistinctBy(fam => fam.LookupParameter(ADSK_PozitsiyaVedEl).AsValueString()).Count());
            }

            int counter = 0;
            for (int i = 0; i < fencesExamplesDistinct.Count; i++)
            {
                counter += rowCounts[i];
                if (counter * 8 <= scheduleHeight) // Высота каждой строки = 8 мм
                {
                    listFamTest.Add(fencesExamplesDistinct[i]);
                }
                else if (listFamTest.Count != 0)
                {
                    listofList_FencesExamplesDistinct.Add(listFamTest);
                    listFamTest = new List<FamilyInstance>();
                    listFamTest.Add(fencesExamplesDistinct[i]);
                    counter = rowCounts[i];
                }
                else
                {
                    MessageBox.Show("Слишком маленькое значение для высоты спецификации", "Внимание!");
                    resultFailed = true;
                    return;
                }


                if (i == fencesExamplesDistinct.Count - 1)
                {
                    listofList_FencesExamplesDistinct.Add(listFamTest); // Список из несколько списков по 8-образцовых экземпляров ограждений - Получено
                }
            }
            //

            // Определяю количество строков массива путем считывание количество составляющих элементов всех ограждений в списке
            int MatrixRowCount = 0;
            foreach (var item in listofList_FencesExamplesDistinct)
            {
                foreach (FamilyInstance item_2 in item)
                {
                    List<FamilyInstance> Test = new List<FamilyInstance>();
                    foreach (var item_3 in item_2.GetSubComponentIds())
                    {
                        Test.Add(doc.GetElement(item_3) as FamilyInstance); // Получаю каждый составляющий элемент, пока для определение количество строк массива
                    }
                    MatrixRowCount += Test.DistinctBy(fam => fam.LookupParameter(ADSK_PozitsiyaVedEl).AsValueString()).Count();
                }
                listOf_MatrixRowCount.Add(MatrixRowCount); // Количество строк получено
            }
        }


        static void ArrayDeclare(List<string[,]> ListOfMatrixes, List<string[]> ListOfArrays_Arr_sech_prof_and_oboznach, List<string[]> ListOfArrays_Arr_mat_and_mat_Oboznach,
            List<int> listOf_MatrixRowCount, List<List<FamilyInstance>> listofList_FencesExamplesDistinct)
        {
            for (int i = 0; i < listofList_FencesExamplesDistinct.Count; i++)
            {
                ListOfMatrixes.Add(new string[listOf_MatrixRowCount[i] + 2, 8]);
                ListOfArrays_Arr_sech_prof_and_oboznach.Add(new string[listOf_MatrixRowCount[i]]);
                ListOfArrays_Arr_mat_and_mat_Oboznach.Add(new string[listOf_MatrixRowCount[i]]);
            }
        }


        static void BeforeArray2DFilling(List<List<FamilyInstance>> listofList_FencesExamplesDistinct,
            ref List<List<List<FamilyInstance>>> ListOfListsOfLists_fencesExamples, ref List<List<List<int>>> ListOfListsOfLists_FExamplesEachDetailAmount,
            ref List<List<double>> ListOfLists_FExamplesWeightAmount, Document doc, string ADSK_Naimenovaniye, string asVal_BoltAnkerniy, string ADSK_Massa, string ADSK_Kolichestvo, string ADSK_PozitsiyaVedEl)
        {
            for (int k = 0; k < listofList_FencesExamplesDistinct.Count; k++)
            {
                double FExamplesWeightCounter = 0;
                List<List<FamilyInstance>> ListofLists_fencesExamples = new List<List<FamilyInstance>>();
                List<List<int>> ListofLists_FExamplesEachDetailAmount = new List<List<int>>();
                List<double> list_FExamplesWeightAmount = new List<double>();
                for (int i = 0; i < listofList_FencesExamplesDistinct[k].Count; i++)
                {
                    List<FamilyInstance> famList = new List<FamilyInstance>();
                    ListofLists_fencesExamples.Add(new List<FamilyInstance>());
                    ListofLists_FExamplesEachDetailAmount.Add(new List<int>());

                    foreach (ElementId item in listofList_FencesExamplesDistinct[k][i].GetSubComponentIds()) // Итерация внутри сост-элементов каждой ограждений
                    {
                        FamilyInstance fam = doc.GetElement(item) as FamilyInstance;

                        famList.Add(fam); // Все составляющие элементы одной ограждений
                        if (fam.Symbol.LookupParameter(ADSK_Naimenovaniye).AsValueString() != asVal_BoltAnkerniy)
                        {
                            FExamplesWeightCounter += fam.LookupParameter(ADSK_Massa).AsDouble();
                        }
                    }
                    // Удаляю из Ограждения сост-элемент у которого параметр ADSK_Kolichestvo = 0 (нужно для столбца "Кол.")
                    for (int n = 0; n < famList.Count; n++)
                    {
                        foreach (Parameter item in famList[n].Parameters)
                        {
                            if (item.Definition.Name == ADSK_Kolichestvo && item.AsDouble() == 0)
                            {
                                famList.RemoveAt(n);
                                break;
                            }
                        }
                    }
                    // Получаю список сост-элементов DistinctBy ADSK_PozitsiyaVedEl
                    ListofLists_fencesExamples[i] = famList.DistinctBy(elem => elem.LookupParameter(ADSK_PozitsiyaVedEl).AsValueString()).ToList();
                    List<string> sorted_stringList = listSort(ListofLists_fencesExamples[i].Select(x => x.LookupParameter(ADSK_PozitsiyaVedEl).AsValueString()).ToList()); // Сортирую список
                    ListofLists_fencesExamples[i] = ListofLists_fencesExamples[i].OrderBy(x => sorted_stringList.IndexOf(x.LookupParameter(ADSK_PozitsiyaVedEl).AsValueString())).ToList(); // Получено

                    // Получаю Количество каждый сост-элемент
                    List<string> strList = famList.Select(fam => fam.LookupParameter(ADSK_PozitsiyaVedEl).AsValueString()).ToList(); // без DistinctBy                                                        
                    strList = listSort(strList); // Сортирую этот же список
                    ListofLists_FExamplesEachDetailAmount[i] = strList.GroupBy(x => x).Where(g => g.Count() > 0).Select(x => x.Count()).ToList(); // Получено

                    // Получаю сумма масс составляющих элементов
                    list_FExamplesWeightAmount.Add(FExamplesWeightCounter);
                    FExamplesWeightCounter = 0;
                }
                ListOfListsOfLists_FExamplesEachDetailAmount.Add(ListofLists_FExamplesEachDetailAmount);
                ListOfLists_FExamplesWeightAmount.Add(list_FExamplesWeightAmount);

                ListOfListsOfLists_fencesExamples.Add(ListofLists_fencesExamples);
            }
        }


        static void PutAllAtArray2d(List<List<List<FamilyInstance>>> ListOfListsOfLists_fencesExamples, List<List<FamilyInstance>> listofList_FencesExamplesDistinct, List<string[,]> ListOfMatrixes,
            List<List<List<int>>> ListOfListsOfLists_FExamplesEachDetailAmount, List<List<double>> ListOfLists_FExamplesWeightAmount,
            List<string[]> ListOfArrays_Arr_sech_prof_and_oboznach, List<string[]> ListOfArrays_Arr_mat_and_mat_Oboznach, Document doc,
            string ADSK_Naimenovaniye, string asVal_BoltAnkerniy, string ADSK_Marka, string ADSK_PozitsiyaVedEl, string ADSK_NaimenovaniyeKratk, string ADSK_RazmerDiametr, string ADSK_RazmerDlina,
            string asVal_ProkatArmaturniy, string sostoyaniyePostavki, string ADSK_DiametrArmaturi, string klassArmaturi, string ADSK_Massa, string ADSK_Oboznacheniye, string secheniyeTrubi, string secheniyeUgolka,
            string secheniyeProfilya, string secheniyePolosi, string tochnostProkatki, string klassSerpovidnosti, string ADSK_Material, string ADSK_MaterialNaimen, string ADSK_MaterialOboznach)
        {
            List<Material> Materials = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Materials).Cast<Material>().ToList();
            for (int k = 0; k < ListOfListsOfLists_fencesExamples.Count; k++)
            {
                int listCounter = 0;
                for (int p = 0; p < ListOfListsOfLists_fencesExamples[k].Count; p++)
                {
                    #region Если это первое ограждение
                    if (p == 0) // Если это первое ограждение
                    {
                        for (int i = 0; i < ListOfListsOfLists_fencesExamples[k][p].Count; i++) // i - как строка массива 
                        {
                            switch (ListOfListsOfLists_fencesExamples[k][p][i].Symbol.LookupParameter(ADSK_Naimenovaniye).AsValueString())
                            {
                                case "Болт анкерный":
                                    ListOfMatrixes[k][i, 0] = listofList_FencesExamplesDistinct[k][p].Symbol.LookupParameter(ADSK_Marka).AsValueString();

                                    ListOfMatrixes[k][i, 1] = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_PozitsiyaVedEl).AsValueString();

                                    //
                                    // Наименование подготовка начало
                                    string str_naim_Ank_Bolt = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                    string str_Diametr_Ank_Bolt = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_RazmerDiametr).AsValueString();
                                    string str_Dlina_Ank_Bolt = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_RazmerDlina).AsValueString();
                                    // Наименование подготовка конец

                                    ListOfMatrixes[k][i, 2] = str_naim_Ank_Bolt + "  " + str_Diametr_Ank_Bolt + "x" + str_Dlina_Ank_Bolt;
                                    ListOfMatrixes[k][i, 3] = str_naim_Ank_Bolt + "  " + str_Diametr_Ank_Bolt + "x" + str_Dlina_Ank_Bolt;
                                    ListOfMatrixes[k][i, 4] = str_naim_Ank_Bolt + "  " + str_Diametr_Ank_Bolt + "x" + str_Dlina_Ank_Bolt;
                                    //

                                    ListOfMatrixes[k][i, 5] = ListOfListsOfLists_FExamplesEachDetailAmount[k][p][i].ToString();

                                    ListOfMatrixes[k][i, 6] = "-";

                                    ListOfMatrixes[k][i, 7] = Math.Round(ListOfLists_FExamplesWeightAmount[k][p], 2).ToString();
                                    break;

                                case "Прокат арматурный для железобетонных конструкций": // Пруток
                                    ListOfMatrixes[k][i, 0] = listofList_FencesExamplesDistinct[k][p].Symbol.LookupParameter(ADSK_Marka).AsValueString();

                                    ListOfMatrixes[k][i, 1] = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_PozitsiyaVedEl).AsValueString();

                                    // Наименование подготовка начало
                                    string str_naim = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                    string str_Sost_postavk = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(sostoyaniyePostavki).AsValueString();
                                    string str_D_test = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_DiametrArmaturi).AsValueString();
                                    string str_Diametr = str_D_test.Substring(0, str_D_test.IndexOf(' '));
                                    string str_Dlina = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_RazmerDlina).AsValueString();
                                    string str_Klass_armatura = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(klassArmaturi).AsValueString();
                                    string str_Oboznach = ListOfListsOfLists_fencesExamples[k][p][i].Symbol.LookupParameter(ADSK_Oboznacheniye).AsValueString();
                                    // Наименование подготовка конец

                                    ListOfMatrixes[k][i, 2] = str_naim + "  " + str_Sost_postavk + "-" + str_Diametr + "X" + str_Dlina + "-" + str_Klass_armatura + " " + str_Oboznach;
                                    ListOfMatrixes[k][i, 3] = str_naim + "  " + str_Sost_postavk + "-" + str_Diametr + "X" + str_Dlina + "-" + str_Klass_armatura + " " + str_Oboznach;
                                    ListOfMatrixes[k][i, 4] = str_naim + "  " + str_Sost_postavk + "-" + str_Diametr + "X" + str_Dlina + "-" + str_Klass_armatura + " " + str_Oboznach;

                                    ListOfMatrixes[k][i, 5] = ListOfListsOfLists_FExamplesEachDetailAmount[k][p][i].ToString();

                                    ListOfMatrixes[k][i, 6] = Math.Round(ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_Massa).AsDouble(), 2).ToString();

                                    ListOfMatrixes[k][i, 7] = Math.Round(ListOfLists_FExamplesWeightAmount[k][p], 2).ToString();
                                    break;

                                default:
                                    // Проверка если это металлическая решетка (для ADSK_Marka)
                                    if ((listofList_FencesExamplesDistinct[k][p] as FamilyInstance).SuperComponent != null)
                                        ListOfMatrixes[k][i, 0] = (listofList_FencesExamplesDistinct[k][p].SuperComponent as FamilyInstance).Symbol.LookupParameter(ADSK_Marka).AsValueString(); // Потому что имеет дополнительный уровень                               
                                    else
                                        ListOfMatrixes[k][i, 0] = listofList_FencesExamplesDistinct[k][p].Symbol.LookupParameter(ADSK_Marka).AsValueString();
                                    // Проверка закончена
                                    ListOfMatrixes[k][i, 1] = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_PozitsiyaVedEl).AsValueString();

                                    // Наименование подготовка начало
                                    string str_naim_kratk = "";
                                    string str_sech_prof = "";
                                    string str_oboznach = "";
                                    string str_mat = "";
                                    string str_dlina = "";


                                    // Проверка на типа сост-элемента                           
                                    switch (ListOfListsOfLists_fencesExamples[k][p][i].Symbol.LookupParameter(ADSK_Naimenovaniye).AsValueString())
                                    {
                                        case "Трубы стальные бесшовные горячедеформированные": // Труба О (круглая)
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][i].Symbol.LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(secheniyeTrubi).AsValueString();
                                            break;
                                        case "Уголки стальные горячекатанные равнополочные": // Уголок
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][i].Symbol.LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(secheniyeUgolka).AsValueString();
                                            break;
                                        case "Трубы стальный профильные для металлоконструкций": // Труба ПК/ПП (не круглая)
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(secheniyeProfilya).AsValueString();
                                            break;
                                        case "Прокат сортовой стальной горячекатанный полосовой": // Полоса
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(secheniyePolosi).AsValueString() + "-" +
                                                            ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(tochnostProkatki).AsValueString() + "-" +
                                                            ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(klassSerpovidnosti).AsValueString();
                                            break;
                                    }
                                    //
                                    str_oboznach = ListOfListsOfLists_fencesExamples[k][p][i].Symbol.LookupParameter(ADSK_Oboznacheniye).AsValueString();
                                    foreach (var item in Materials)
                                    {
                                        if (ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_Material).AsValueString() == item.Name)
                                        {
                                            str_mat = item.LookupParameter(ADSK_MaterialNaimen).AsValueString() + "  " + item.LookupParameter(ADSK_MaterialOboznach).AsValueString();
                                            break;
                                        }
                                    }
                                    str_dlina = "L=" + ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_RazmerDlina).AsValueString() + "мм";
                                    string sech_prof_and_oboznach = str_sech_prof + "  " + str_oboznach;
                                    // Наименование подготовка конец

                                    ListOfMatrixes[k][i, 2] = str_naim_kratk;
                                    ListOfMatrixes[k][i, 3] = sech_prof_and_oboznach + "\n" + str_mat;
                                    ListOfArrays_Arr_sech_prof_and_oboznach[k][i] = sech_prof_and_oboznach;
                                    ListOfArrays_Arr_mat_and_mat_Oboznach[k][i] = str_mat;

                                    ListOfMatrixes[k][i, 4] = str_dlina;
                                    // 
                                    ListOfMatrixes[k][i, 5] = ListOfListsOfLists_FExamplesEachDetailAmount[k][p][i].ToString();

                                    ListOfMatrixes[k][i, 6] = Math.Round(ListOfListsOfLists_fencesExamples[k][p][i].LookupParameter(ADSK_Massa).AsDouble(), 2).ToString();

                                    ListOfMatrixes[k][i, 7] = Math.Round(ListOfLists_FExamplesWeightAmount[k][p], 2).ToString();
                                    break;
                            }
                        }
                        listCounter += ListOfListsOfLists_fencesExamples[k][p].Count;
                    }

                    #endregion
                    #region Если это НЕ первое ограждени
                    else // Если это НЕ первое ограждение (в общем все точ в точ, как в первой условии)
                    {
                        int someNumber = 0;
                        for (int i = listCounter; i < listCounter + ListOfListsOfLists_fencesExamples[k][p].Count; i++) // i - как строка массива 
                        {
                            switch (ListOfListsOfLists_fencesExamples[k][p][someNumber].Symbol.LookupParameter(ADSK_Naimenovaniye).AsValueString())
                            {
                                case "Болт анкерный":
                                    ListOfMatrixes[k][i, 0] = listofList_FencesExamplesDistinct[k][p].Symbol.LookupParameter(ADSK_Marka).AsValueString();

                                    ListOfMatrixes[k][i, 1] = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_PozitsiyaVedEl).AsValueString();

                                    //
                                    // Наименование подготовка начало
                                    string str_naim_Ank_Bolt = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                    string str_Diametr_Ank_Bolt = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_RazmerDiametr).AsValueString();
                                    string str_Dlina_Ank_Bolt = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_RazmerDlina).AsValueString();
                                    // Наименование подготовка конец

                                    ListOfMatrixes[k][i, 2] = str_naim_Ank_Bolt + "  " + str_Diametr_Ank_Bolt + "x" + str_Dlina_Ank_Bolt;
                                    ListOfMatrixes[k][i, 3] = str_naim_Ank_Bolt + "  " + str_Diametr_Ank_Bolt + "x" + str_Dlina_Ank_Bolt;
                                    ListOfMatrixes[k][i, 4] = str_naim_Ank_Bolt + "  " + str_Diametr_Ank_Bolt + "x" + str_Dlina_Ank_Bolt;

                                    ListOfMatrixes[k][i, 5] = ListOfListsOfLists_FExamplesEachDetailAmount[k][p][someNumber].ToString();

                                    ListOfMatrixes[k][i, 6] = "-";

                                    ListOfMatrixes[k][i, 7] = Math.Round(ListOfLists_FExamplesWeightAmount[k][p], 2).ToString();

                                    someNumber++;
                                    break;

                                case "Прокат арматурный для железобетонных конструкций": // Пруток
                                    ListOfMatrixes[k][i, 0] = listofList_FencesExamplesDistinct[k][p].Symbol.LookupParameter(ADSK_Marka).AsValueString();

                                    ListOfMatrixes[k][i, 1] = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_PozitsiyaVedEl).AsValueString();

                                    // Наименование подготовка начало
                                    string str_naim = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                    string str_Sost_postavk = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(sostoyaniyePostavki).AsValueString();
                                    string str_D_test = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_DiametrArmaturi).AsValueString();
                                    string str_Diametr = str_D_test.Substring(0, str_D_test.IndexOf(' '));
                                    string str_Dlina = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_RazmerDlina).AsValueString();
                                    string str_Klass_armatura = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(klassArmaturi).AsValueString();
                                    string str_Oboznach = ListOfListsOfLists_fencesExamples[k][p][someNumber].Symbol.LookupParameter(ADSK_Oboznacheniye).AsValueString();
                                    // Наименование подготовка конец
                                    ListOfMatrixes[k][i, 2] = str_naim + "  " + str_Sost_postavk + "-" + str_Diametr + "X" + str_Dlina + "-" + str_Klass_armatura + " " + str_Oboznach;
                                    ListOfMatrixes[k][i, 3] = str_naim + "  " + str_Sost_postavk + "-" + str_Diametr + "X" + str_Dlina + "-" + str_Klass_armatura + " " + str_Oboznach;
                                    ListOfMatrixes[k][i, 4] = str_naim + "  " + str_Sost_postavk + "-" + str_Diametr + "X" + str_Dlina + "-" + str_Klass_armatura + " " + str_Oboznach;

                                    ListOfMatrixes[k][i, 5] = ListOfListsOfLists_FExamplesEachDetailAmount[k][p][someNumber].ToString();

                                    ListOfMatrixes[k][i, 6] = Math.Round(ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_Massa).AsDouble(), 2).ToString();

                                    ListOfMatrixes[k][i, 7] = Math.Round(ListOfLists_FExamplesWeightAmount[k][p], 2).ToString();

                                    someNumber++;
                                    break;

                                default:
                                    // Проверка если это металлическая решетка (для ADSK_Marka)
                                    if ((listofList_FencesExamplesDistinct[k][p] as FamilyInstance).SuperComponent != null)
                                        ListOfMatrixes[k][i, 0] = (listofList_FencesExamplesDistinct[k][p].SuperComponent as FamilyInstance).Symbol.LookupParameter(ADSK_Marka).AsValueString(); // Потому что имеет дополнительный уровень                               
                                    else
                                        ListOfMatrixes[k][i, 0] = listofList_FencesExamplesDistinct[k][p].Symbol.LookupParameter(ADSK_Marka).AsValueString();
                                    // Проверка закончена

                                    ListOfMatrixes[k][i, 1] = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_PozitsiyaVedEl).AsValueString();

                                    //// Наименование подготовка начало
                                    string str_naim_kratk = "";
                                    string str_sech_prof = "";
                                    string str_oboznach = "";
                                    string str_mat = "";
                                    string str_dlina = "";

                                    //// Проверка на типа сост-элемента                            
                                    switch (ListOfListsOfLists_fencesExamples[k][p][someNumber].Symbol.LookupParameter(ADSK_Naimenovaniye).AsValueString())
                                    {
                                        case "Трубы стальные бесшовные горячедеформированные":
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][someNumber].Symbol.LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(secheniyeTrubi).AsValueString();
                                            break;
                                        case "Уголки стальные горячекатанные равнополочные":
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][someNumber].Symbol.LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(secheniyeUgolka).AsValueString();
                                            break;
                                        case "Трубы стальный профильные для металлоконструкций":
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(secheniyeProfilya).AsValueString();
                                            break;
                                        case "Прокат сортовой стальной горячекатанный полосовой":
                                            str_naim_kratk = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_NaimenovaniyeKratk).AsValueString();
                                            str_sech_prof = ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(secheniyePolosi).AsValueString() + "-" +
                                                            ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(tochnostProkatki).AsValueString() + "-" +
                                                            ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(klassSerpovidnosti).AsValueString();
                                            break;
                                    }
                                    //
                                    str_oboznach = ListOfListsOfLists_fencesExamples[k][p][someNumber].Symbol.LookupParameter(ADSK_Oboznacheniye).AsValueString();
                                    foreach (var item in Materials)
                                    {
                                        if (ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_Material).AsValueString() == item.Name)
                                        {
                                            str_mat = item.LookupParameter(ADSK_MaterialNaimen).AsValueString() + " " + item.LookupParameter(ADSK_MaterialOboznach).AsValueString();
                                        }
                                    }
                                    str_dlina = "L=" + ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_RazmerDlina).AsValueString() + "мм";

                                    string sech_prof_and_oboznach = str_sech_prof + " " + str_oboznach;
                                    //// Наименование подготовка конец

                                    ListOfMatrixes[k][i, 2] = str_naim_kratk;

                                    ListOfMatrixes[k][i, 3] = sech_prof_and_oboznach + "\n" + str_mat;

                                    ListOfArrays_Arr_sech_prof_and_oboznach[k][i] = sech_prof_and_oboznach;
                                    ListOfArrays_Arr_mat_and_mat_Oboznach[k][i] = str_mat;

                                    ListOfMatrixes[k][i, 4] = str_dlina;
                                    //

                                    ListOfMatrixes[k][i, 5] = ListOfListsOfLists_FExamplesEachDetailAmount[k][p][someNumber].ToString();

                                    ListOfMatrixes[k][i, 6] = Math.Round(ListOfListsOfLists_fencesExamples[k][p][someNumber].LookupParameter(ADSK_Massa).AsDouble(), 2).ToString();

                                    ListOfMatrixes[k][i, 7] = Math.Round(ListOfLists_FExamplesWeightAmount[k][p], 2).ToString();

                                    someNumber++;
                                    break;
                            }
                        }
                        listCounter += ListOfListsOfLists_fencesExamples[k][p].Count;
                    }
                    #endregion
                }
                // Потом объединяю их снова, это для дробной части в поле наименование таблицы
                ListOfMatrixes[k] = Array2DRowDoubler(ListOfMatrixes[k]);
                ListOfArrays_Arr_sech_prof_and_oboznach[k] = ArrayRowDoubler(ListOfArrays_Arr_sech_prof_and_oboznach[k]);
                ListOfArrays_Arr_mat_and_mat_Oboznach[k] = ArrayRowDoubler(ListOfArrays_Arr_mat_and_mat_Oboznach[k]);
            }
        }


        static void PutAtTableHeader(List<List<List<FamilyInstance>>> ListOfListsOfLists_fencesExamples, List<FamilyInstance> allFencesInstances, List<ViewSchedule> ViewSchedules,
            List<string[,]> ListOfMatrixes, List<string[]> ListOfArrays_Arr_sech_prof_and_oboznach, List<string[]> ListOfArrays_Arr_mat_and_mat_Oboznach, string vsName, string modelGroupValue,
            Document doc, ref bool resultFailed, string ADSK_NaznacheniyeVida)
        {
            // Удаление ненужных спецификаций
            foreach (var item in ViewSchedules)
            {
                if (item.IsValidObject == true && item.Name.Contains(vsName) && Convert.ToInt32(new String(item.Name.Where(Char.IsDigit).ToArray())) > ListOfListsOfLists_fencesExamples.Count)
                {
                    doc.Delete(item.Id);
                }
            }
            //

            int a = 1;
            for (int i = 0; i < ListOfListsOfLists_fencesExamples.Count; i++)
            {
                // Объвление переменных для создание спецификаций
                ViewSchedule vs = null;
                //
                string vsNameLast = "";
                vsNameLast = vsName + a.ToString();
                if (a < 10)
                    vsNameLast = vsName + "0" + a.ToString();
                //

                bool vsChecker = false;

                // Проверка на существующей спецификации и его создание в случае отсутсвие
                ElementId catId = allFencesInstances.FirstOrDefault().Category.Id; // аргумент для метода CreateSheduleWithFields()

                foreach (ViewSchedule item in ViewSchedules) // Проверяю если нужная спецификация уже существует
                {
                    if (item.IsValidObject == true && item.Name == vsNameLast)
                    {
                        vs = item;
                        vsChecker = true;
                        break;
                    }
                }

                if (!vsChecker) // Создание в случае отсутствие                
                    vs = CreateSсheduleWithFields(vsNameLast, ListOfMatrixes[i].GetLength(1), doc, catId);


                //// Приведение спецификации в порядке
                // Для Установление шрифта для заголовок
                List<Element> textNotes = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_TextNotes).WhereElementIsNotElementType().ToList();
                TextNote textNote = null;
                if (0 != textNotes.Count && null != textNotes)
                {
                    foreach (var item in textNotes)
                    {
                        if (item.Name.Trim() == "ADSK_Основной текст_2.5 (сжатый)".Trim())
                        {
                            textNote = (TextNote)item;
                            break;
                        }
                    }
                }
                vs.LookupParameter(ADSK_NaznacheniyeVida).Set(modelGroupValue);
                //
                if (textNote == null)
                {
                    MessageBox.Show("Отсутствует нужный шрифт", "Внимание!");
                    resultFailed = true;
                    return;
                }


                // Заполнение спецификация лоджий
                ScheduleFilling(vs, ListOfMatrixes[i], ListOfArrays_Arr_sech_prof_and_oboznach[i], ListOfArrays_Arr_mat_and_mat_Oboznach[i], vsNameLast, doc, catId, textNote);

                //Заканчиваю транзакцию
                a++;
            }
        }


        //
        static string[] ArrayRowDoubler(string[] Array)
        {
            string[] RowDoubledArray = new string[Array.Length * 2];
            int a = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                for (int c = 0; c < 2; c++)
                {
                    RowDoubledArray[a] = Array[i];
                    a++;
                }
            }
            return RowDoubledArray;
        } // Method for Doubling Rows of Array

        static string[,] Array2DRowDoubler(string[,] Array2D)
        {
            string[,] RowDoubled2DArray = new string[Array2D.GetLength(0) * 2, Array2D.GetLength(1)];
            int a = 0;
            for (int i = 0; i < Array2D.GetLength(0); i++)
            {
                for (int c = 0; c < 2; c++)
                {
                    for (int j = 0; j < Array2D.GetLength(1); j++)
                    {
                        RowDoubled2DArray[a, j] = Array2D[i, j];
                    }
                    a++;
                }
            }
            return RowDoubled2DArray;
        } // Method for Doubling Rows of 2D Array

        public static ViewSchedule CreateSсheduleWithFields(string name, int numberOfColumns, Document document, ElementId categoryId)
        {
            ViewSchedule vs = null;
            // Создание спецификации
            vs = ViewSchedule.CreateSchedule(document, categoryId);
            vs.Name = name;
            // Добавление поле в спецификации
            for (int i = 0; i < numberOfColumns; i++)
            {
                ScheduleDefinition definition = vs.Definition;

                SchedulableField schedulableField = definition.GetSchedulableFields()[i];
                if (schedulableField != null)
                {
                    // Добавление поле
                    definition.AddField(schedulableField);
                }
            }
            return vs;
        }

        public static void AddRegularFieldToSchedule(ViewSchedule schedule, int columnIndex, Document document)
        {
            ScheduleDefinition definition = schedule.Definition;

            SchedulableField schedulableField = definition.GetSchedulableFields()[columnIndex];

            if (schedulableField != null)
            {
                // Добавление поле
                definition.AddField(schedulableField);
            }


        }

        public static void ScheduleFilling(ViewSchedule vs, string[,] Matrix, string[] Arr_sech_prof, string[] Arr_mat_and_mat_Oboznach, string vs_Name, Document doc, ElementId categoryId,
            TextNote textNote)
        {
            if (null != vs)
            {
                vs.TitleTextTypeId = textNote.GetTypeId(); // Устанавливаю шрифт для Заголовок
                vs.Definition.ShowHeaders = false; // Скрываю Заголовки столбцов
                vs.SetCategoryHidden(categoryId, true); // Скрываю самых столбцов

                TableData td = vs.GetTableData(); // get viewschedule table data
                TableSectionData tsd = td.GetSectionData(SectionType.Header); // get header section data

                // Обновление таблицы при повторном использование                
                if (tsd.NumberOfRows > 1)
                {
                    // удаляем строки
                    for (int r = tsd.NumberOfRows - 1; r > 0; r--)
                    {
                        tsd.RemoveRow(r);
                    }
                }
                // MergeCells Объявление начато - Объединяю ячейек в нужных столбцах (Только столбец "Марка" и столбец "Масса")
                TableMergedCell mergecellsMarka = new TableMergedCell();
                mergecellsMarka.Left = 0;
                mergecellsMarka.Right = 0;
                mergecellsMarka.Top = 2;
                mergecellsMarka.Bottom = 0;

                TableMergedCell mergecellsMassa = new TableMergedCell();
                mergecellsMassa.Left = 7;
                mergecellsMassa.Right = 7;
                mergecellsMassa.Top = 2;
                mergecellsMassa.Bottom = 0;
                // MergeCells Объявление завершено


                // Заполняю ячеек
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    switch (i)
                    {
                        case 0: // Проверяю если есть уже строка то удаляю ее, и создаю новую
                                // Первая строка для название Спецификации
                            if (tsd.GetCellText(i, 0).Trim() == "") // Чтобы при повторном срабатывание плагина, название не поменялась
                            {
                                tsd.SetCellText(i, 0, vs_Name);
                            }

                            // Регулировка число столбцов (начало)
                            if (tsd.NumberOfColumns < Matrix.GetLength(1))
                            {
                                for (int m = tsd.NumberOfColumns; m < Matrix.GetLength(1); m++)
                                {
                                    tsd.InsertColumn(m);
                                }
                            }
                            else
                            {
                                for (int m = Matrix.GetLength(1); m < tsd.NumberOfColumns; m++)
                                {
                                    tsd.RemoveColumn(m);
                                }
                            }
                            // Регулировка число столбцов (конец) 

                            // Подправление размер Столбцов таблицы - начато                        
                            for (int j = 0; j < Matrix.GetLength(1); j++)
                            {
                                if (j == 0 || j == 6 || j == 7)
                                {
                                    tsd.SetColumnWidth(j, 15 / 304.8);
                                }
                                else if (j == 1 || j == 5)
                                {
                                    tsd.SetColumnWidth(j, 10 / 304.8);
                                }
                                else if (j == 2 || j == 4)
                                {
                                    tsd.SetColumnWidth(j, 14 / 304.8);
                                }
                                else if (j == 3)
                                {
                                    tsd.SetColumnWidth(j, 32 / 304.8);
                                }
                            }
                            // Подправление размер Столбцов таблицы - закончено (Подправление строк таблицы будет дальше)


                            TableMergedCell mergecellsVsName = new TableMergedCell();
                            mergecellsVsName.Left = 0;
                            mergecellsVsName.Right = 7;
                            mergecellsVsName.Top = 0;
                            mergecellsVsName.Bottom = 0;
                            tsd.MergeCells(mergecellsVsName);
                            //

                            // Подправление размер строк таблицы - header
                            tsd.SetRowHeight(i, 10 / 304.8);
                            break;

                        case 1: // Вторая строка для название столбцов таблицы
                            tsd.InsertRow(i); // Добавляю новую строку

                            // Устанавливаю название столбцов таблицы
                            tsd.SetCellText(i, 0, "Марка изделия");
                            tsd.SetCellText(i, 1, "Поз. дет.");
                            tsd.SetCellText(i, 2, "Наименование");
                            tsd.SetCellText(i, 3, "Наименование");
                            tsd.SetCellText(i, 4, "Наименование");
                            tsd.SetCellText(i, 5, "Кол.");
                            tsd.SetCellText(i, 6, "Масса 1 дет., кг");
                            tsd.SetCellText(i, 7, "Масса изделия, кг");

                            // Подправление размер строк таблицы - header
                            tsd.SetRowHeight(i, 15 / 304.8);
                            break;

                        default: // Дальше уже соновной контент (поставка из массива)
                            tsd.InsertRow(i); // Добавляю новую строку

                            for (int j = 0; j < Matrix.GetLength(1); j++)
                            {
                                if (Matrix[i - 2, j] != null)
                                {
                                    tsd.SetCellText(i, j, Matrix[i - 2, j]);  // Для соответсвие строк таблицы со строками массива (первая строка массива - это третая строка таблицы)
                                }
                            }

                            // MergeCells логика начало
                            if (tsd.GetCellText(i, 0) != tsd.GetCellText(mergecellsMarka.Top, 0))
                            {
                                mergecellsMarka.Bottom = i - 1;
                                mergecellsMassa.Bottom = i - 1;
                                tsd.MergeCells(mergecellsMarka);
                                tsd.MergeCells(mergecellsMassa);
                                mergecellsMarka.Top = i;
                                mergecellsMassa.Top = i;
                            }
                            if (i == tsd.NumberOfRows - 1)
                            {
                                mergecellsMarka.Bottom = i;
                                mergecellsMassa.Bottom = i;
                                tsd.MergeCells(mergecellsMarka);
                                tsd.MergeCells(mergecellsMassa);
                            }
                            // MergeCells логика завершено

                            // Подправление размер строк таблицы - body
                            tsd.SetRowHeight(i, 8 / 304.8);
                            break;
                    }
                }
                ////


                // Удаляю полностю пустые строки
                for (int i = 0; i < 35; i++) // На пока все норм, но надо Обдумать этот момент 
                {
                    RemoveEmptyScheduleRows(tsd);
                }

                // Форматирование ячейек
                // Get the current style of the title cell
                TableCellStyle tableCellStyle = tsd.GetTableCellStyle(1, 1);
                TableCellStyle tableCellStyle_vsName = tsd.GetTableCellStyle(1, 1);

                // Get the override options of the style
                TableCellStyleOverrideOptions overrideOptions = tableCellStyle.GetCellStyleOverrideOptions();
                TableCellStyleOverrideOptions overrideOptions_vsName = tableCellStyle_vsName.GetCellStyleOverrideOptions();

                // Horizontal Alignment
                overrideOptions.HorizontalAlignment = true;
                overrideOptions_vsName.HorizontalAlignment = true;

                // Set the overrides options on the style
                tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                tableCellStyle_vsName.SetCellStyleOverrideOptions(overrideOptions);


                // Horizontal Alignment                  
                tableCellStyle.FontHorizontalAlignment = HorizontalAlignmentStyle.Left;

                for (int i = 2; i < tsd.NumberOfRows; i++)
                {
                    tsd.SetCellStyle(i, 2, tableCellStyle); // Для остальных две поля Наименование
                }

                //// 

                // Поставляю значения для дробной части из объединенных параметров Sech_prof и mat_Oboznach
                for (int i = 2; i < tsd.NumberOfRows; i++)
                {
                    if (Arr_sech_prof[i - 2] != null && Arr_mat_and_mat_Oboznach[i - 2] != null)
                    {
                        if (i % 2 == 0) // Если строка четная
                        {
                            tsd.SetCellText(i, 3, Arr_sech_prof[i - 2]);
                        }
                        else
                        {
                            tsd.SetCellText(i, 3, Arr_mat_and_mat_Oboznach[i - 2]);
                        }
                    }
                }
                //             


                //// Стирание границ для поле наименований
                overrideOptions.BorderLineStyle = true;
                overrideOptions.BorderBottomLineStyle = false;
                overrideOptions.BorderTopLineStyle = false;
                overrideOptions.BorderLeftLineStyle = true;
                overrideOptions.BorderRightLineStyle = false;

                tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);

                for (int i = 2; i < tsd.NumberOfRows; i++)
                {
                    for (int j = 3; j < 5; j++) // Так как BorderLineStyle работает только с Left корректируем удаляемую границу
                    {
                        tsd.SetCellStyle(i, j, tableCellStyle);
                    }
                    tsd.SetRowHeight(i, 4 / 304.8); // Подправляем высоту строк после tsd.SetCellStyle()
                }
                // Стирание границ для строка название спецификации
                overrideOptions_vsName.BorderLineStyle = true;
                overrideOptions_vsName.BorderBottomLineStyle = false;
                overrideOptions_vsName.BorderTopLineStyle = true;
                overrideOptions_vsName.BorderLeftLineStyle = true;
                overrideOptions_vsName.BorderRightLineStyle = true;

                tableCellStyle_vsName.SetCellStyleOverrideOptions(overrideOptions_vsName);
                tsd.SetCellStyle(0, 0, tableCellStyle_vsName);
                //

                //// Merging дублированных строк и допольнительных столбцов
                TableMergedCell mergecellsTest = new TableMergedCell();

                // Merge Наименование Горизонтально, его три допольнительных столбца (такой подход - для отображение контента в правильной форме)
                mergecellsTest.Left = 2;
                mergecellsTest.Right = 4;
                mergecellsTest.Top = 1;
                mergecellsTest.Bottom = 1;
                tsd.MergeCells(mergecellsTest);

                // Merge всех строк кроме 4-й столбец
                for (int i = 2; i < tsd.NumberOfRows; i += 2)
                {
                    // Merge строк вертикально
                    for (int j = 1; j < 7; j++)
                    {
                        if (j != 3 || (j == 3 && (tsd.GetCellText(i, j).ToLower().Contains("анкерный") || tsd.GetCellText(i, j).ToLower().Contains("пруток")))) // Если сост-эл это анкерный болт или пруток, или это не 4-й столбец
                        {
                            mergecellsTest.Left = j;
                            mergecellsTest.Right = j;
                            mergecellsTest.Top = i;
                            mergecellsTest.Bottom = i + 1;
                            tsd.MergeCells(mergecellsTest);
                        }
                    }
                    //Merge строк горизонтально
                    for (int j = 1; j < 7; j++)
                    {
                        if (tsd.GetCellText(i, j).ToLower().Contains("анкерный") || tsd.GetCellText(i, j).ToLower().Contains("пруток")) // Если сост-эл это анкерный болт или пруток
                        {
                            for (int c = 2; c < 5; c++)
                            {
                                mergecellsTest.Left = 2;
                                mergecellsTest.Right = 4;
                                mergecellsTest.Top = i;
                                mergecellsTest.Bottom = i + 1;
                                tsd.MergeCells(mergecellsTest);
                            }
                            break;
                        }
                    }
                }
                //

                // HorizontalAlignmentStyle.Center for "L=" j == 4
                for (int i = 2; i < tsd.NumberOfRows; i++)
                {
                    tableCellStyle.FontHorizontalAlignment = HorizontalAlignmentStyle.Center;
                    tsd.SetCellStyle(i, 4, tableCellStyle); // Для остальных две поля Наименование
                }
                //


                //// Жирные границы для каждой ограждений 
                GraphicsStyle GSLineStyle = new FilteredElementCollector(doc).OfClass(typeof(GraphicsStyle)).Cast<GraphicsStyle>().FirstOrDefault(e => e.Name.Equals("<Утолщенные линии>"));
                overrideOptions.BorderLineStyle = true;
                overrideOptions.BorderBottomLineStyle = true;
                overrideOptions.BorderTopLineStyle = true;  // Only Left and Top
                overrideOptions.BorderLeftLineStyle = true;
                overrideOptions.BorderRightLineStyle = true;

                tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);

                tableCellStyle.BorderTopLineStyle = GSLineStyle.GraphicsStyleCategory.Id;
                tableCellStyle.BorderBottomLineStyle = GSLineStyle.GraphicsStyleCategory.Id;
                tableCellStyle.BorderRightLineStyle = GSLineStyle.GraphicsStyleCategory.Id;
                tableCellStyle.BorderLeftLineStyle = GSLineStyle.GraphicsStyleCategory.Id;

                for (int i = 1; i < tsd.NumberOfRows; i++)
                {
                    for (int j = 0; j < tsd.NumberOfColumns; j++)
                    {
                        if (i != 1 && i != 2)
                        {
                            overrideOptions.BorderBottomLineStyle = false;
                            overrideOptions.BorderTopLineStyle = false;
                            tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                        }
                        if (i != 1 && tsd.GetCellText(i, 0) != tsd.GetCellText(i - 1, 0)) // Каждое новое ограждение с жирной верхной границой 
                        {
                            overrideOptions.BorderBottomLineStyle = true;
                            overrideOptions.BorderTopLineStyle = true;
                            tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                        }
                        if (i == tsd.NumberOfRows - 2) // Последняя ограждение с жирной нижной границой
                        {
                            overrideOptions.BorderBottomLineStyle = true;
                            overrideOptions.BorderTopLineStyle = false;
                            tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                        }
                        tsd.SetCellStyle(i, j, tableCellStyle);
                    }
                }
                // Выравнивание 3-го столбца по левому краю (j == 2) 
                for (int i = 2; i < tsd.NumberOfRows; i++)
                {
                    tableCellStyle.FontHorizontalAlignment = HorizontalAlignmentStyle.Left;
                    overrideOptions.BorderBottomLineStyle = false;
                    overrideOptions.BorderTopLineStyle = false;
                    tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                    if (tsd.GetCellText(i, 0) != tsd.GetCellText(i - 1, 0))
                    {
                        overrideOptions.BorderBottomLineStyle = false;
                        overrideOptions.BorderTopLineStyle = true;
                        tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                    }
                    if (i == tsd.NumberOfRows - 2) // !!! *** Обработать Ситуацию, где в ограждение 1 элемент, тогда у последной ячейки и верхная и нижная граница должны быть жирным (хотя это маловероятно)
                    {
                        overrideOptions.BorderBottomLineStyle = true;
                        overrideOptions.BorderTopLineStyle = false;
                        tableCellStyle.SetCellStyleOverrideOptions(overrideOptions);
                    }
                    tsd.SetCellStyle(i, 2, tableCellStyle); // Для остальных две поля Наименование
                }
                //

                //// Жирные границы для дробной части + стирание границ
                TableCellStyle tableCellStyle_helpingColumn = tsd.GetTableCellStyle(0, 0);
                TableCellStyleOverrideOptions overrideOptions_HelpingColumn = tableCellStyle_helpingColumn.GetCellStyleOverrideOptions();

                GraphicsStyle GSlineStyle_2 = new FilteredElementCollector(doc).OfClass(typeof(GraphicsStyle)).Cast<GraphicsStyle>().FirstOrDefault(e => e.Name.Equals("<Утолщенные линии>"));
                overrideOptions_HelpingColumn.BorderBottomLineStyle = true;
                overrideOptions_HelpingColumn.BorderTopLineStyle = true;

                tableCellStyle_helpingColumn.SetCellStyleOverrideOptions(overrideOptions_HelpingColumn);

                tableCellStyle_helpingColumn.BorderTopLineStyle = GSlineStyle_2.GraphicsStyleCategory.Id;
                tableCellStyle_helpingColumn.BorderBottomLineStyle = GSlineStyle_2.GraphicsStyleCategory.Id;

                // Кроме верхных и нижных границ, еще уберем правых и левых границ в дробной части
                overrideOptions_HelpingColumn.HorizontalAlignment = true;
                tableCellStyle_helpingColumn.FontHorizontalAlignment = HorizontalAlignmentStyle.Left; // Еще раз указываем для новой стили
                for (int i = 2; i < tsd.NumberOfRows; i++)
                {
                    if (i % 2 == 0 && tsd.GetCellText(i, 0) != tsd.GetCellText(i - 1, 0)) // Каждое новое ограждение с жирной верхной границой в дробной части
                    {
                        overrideOptions_HelpingColumn.BorderBottomLineStyle = false;
                        overrideOptions_HelpingColumn.BorderTopLineStyle = true;
                        tableCellStyle_helpingColumn.SetCellStyleOverrideOptions(overrideOptions_HelpingColumn);
                    }
                    else // Иначе Обыные границы в дробной части
                    {
                        overrideOptions_HelpingColumn.BorderBottomLineStyle = false;
                        overrideOptions_HelpingColumn.BorderTopLineStyle = false;
                        tableCellStyle_helpingColumn.SetCellStyleOverrideOptions(overrideOptions_HelpingColumn);
                    }
                    if (i == tsd.NumberOfRows - 1 || i == tsd.NumberOfRows - 2) // В объеденных строк надо обращаться к первому строку в объедениние, и все изменение стиля тоже надо внести в первую строку || А к обычным строкам обрашаемся как обычно 
                                                                                // Последняя ограждение с жирной нижной границой в дробной части
                    {
                        overrideOptions_HelpingColumn.BorderBottomLineStyle = true;
                        overrideOptions_HelpingColumn.BorderTopLineStyle = false;
                        tableCellStyle_helpingColumn.SetCellStyleOverrideOptions(overrideOptions_HelpingColumn);
                    }
                    tsd.SetCellStyle(i, 3, tableCellStyle_helpingColumn);
                    tsd.SetCellStyle(i, 4, tableCellStyle_helpingColumn);
                    tsd.SetRowHeight(i, 4 / 304.8); // SetRowHeight for Doubled rows
                }
                ////
            }
        }

        public static void RemoveEmptyScheduleRows(TableSectionData tsd)
        {
            string deleteCheck = "";
            for (int i = 0; i < tsd.NumberOfRows; i++)
            {
                for (int j = 0; j < tsd.NumberOfColumns; j++)
                {
                    deleteCheck += tsd.GetCellText(i, j).Trim();
                }
                if (deleteCheck.Trim() == "")
                {
                    tsd.RemoveRow(i);
                }
                deleteCheck = "";
            }
        }


        /*
        Подаем List<string> получаем обратно этот список, уже сортированный*/
        public static List<string> listSort(List<string> list_str)
        {
            NumericComparer nc = new NumericComparer();
            string[] Array_list_str = list_str.ToArray();
            Array.Sort(Array_list_str, nc);
            List<string> list_test = Array_list_str.ToList();
            return list_test;
        }
    }


    public class NumericComparer : IComparer
    {
        public NumericComparer()
        { }

        public int Compare(object x, object y)
        {
            if ((x is string) && (y is string))
            {
                return StringLogicalComparer.Compare((string)x, (string)y);
            }
            return -1;
        }
    } // Comparer Класс для сортировки массива

    public class StringLogicalComparer
    {
        public static int Compare(string s1, string s2)
        {
            //get rid of special cases
            if ((s1 == null) && (s2 == null)) return 0;
            else if (s1 == null) return -1;
            else if (s2 == null) return 1;

            if ((s1.Equals(string.Empty) && (s2.Equals(string.Empty)))) return 0;
            else if (s1.Equals(string.Empty)) return -1;
            else if (s2.Equals(string.Empty)) return -1;

            //WE style, special case
            bool sp1 = Char.IsLetterOrDigit(s1, 0);
            bool sp2 = Char.IsLetterOrDigit(s2, 0);
            if (sp1 && !sp2) return 1;
            if (!sp1 && sp2) return -1;

            int i1 = 0, i2 = 0; //current index
            int r = 0; // temp result
            while (true)
            {
                bool c1 = Char.IsDigit(s1, i1);
                bool c2 = Char.IsDigit(s2, i2);
                if (!c1 && !c2)
                {
                    bool letter1 = Char.IsLetter(s1, i1);
                    bool letter2 = Char.IsLetter(s2, i2);
                    if ((letter1 && letter2) || (!letter1 && !letter2))
                    {
                        if (letter1 && letter2)
                        {
                            r = Char.ToLower(s1[i1]).CompareTo(Char.ToLower(s2[i2]));
                        }
                        else
                        {
                            r = s1[i1].CompareTo(s2[i2]);
                        }
                        if (r != 0) return r;
                    }
                    else if (!letter1 && letter2) return -1;
                    else if (letter1 && !letter2) return 1;
                }
                else if (c1 && c2)
                {
                    r = CompareNum(s1, ref i1, s2, ref i2);
                    if (r != 0) return r;
                }
                else if (c1)
                {
                    return -1;
                }
                else if (c2)
                {
                    return 1;
                }
                i1++;
                i2++;
                if ((i1 >= s1.Length) && (i2 >= s2.Length))
                {
                    return 0;
                }
                else if (i1 >= s1.Length)
                {
                    return -1;
                }
                else if (i2 >= s2.Length)
                {
                    return -1;
                }
            }
        }

        private static int CompareNum(string s1, ref int i1, string s2, ref int i2)
        {
            int nzStart1 = i1, nzStart2 = i2; // nz = non zero
            int end1 = i1, end2 = i2;

            ScanNumEnd(s1, i1, ref end1, ref nzStart1);
            ScanNumEnd(s2, i2, ref end2, ref nzStart2);
            int start1 = i1; i1 = end1 - 1;
            int start2 = i2; i2 = end2 - 1;

            int nzLength1 = end1 - nzStart1;
            int nzLength2 = end2 - nzStart2;

            if (nzLength1 < nzLength2) return -1;
            else if (nzLength1 > nzLength2) return 1;

            for (int j1 = nzStart1, j2 = nzStart2; j1 <= i1; j1++, j2++)
            {
                int r = s1[j1].CompareTo(s2[j2]);
                if (r != 0) return r;
            }
            // the nz parts are equal
            int length1 = end1 - start1;
            int length2 = end2 - start2;
            if (length1 == length2) return 0;
            if (length1 > length2) return -1;
            return 1;
        }

        //lookahead
        private static void ScanNumEnd(string s, int start, ref int end, ref int nzStart)
        {
            nzStart = start;
            end = start;
            bool countZeros = true;
            while (Char.IsDigit(s, end))
            {
                if (countZeros && s[end].Equals('0'))
                {
                    nzStart++;
                }
                else countZeros = false;
                end++;
                if (end >= s.Length) break;
            }
        }

    } // часть Comparer Класса
}
