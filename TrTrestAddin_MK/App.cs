#region Namespaces
using Autodesk.Revit.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using TrTrestAddin_MK.Commands;

#endregion

namespace TrTrestAddin_MK
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            string tabName = "Третий Трест_МК";
            a.CreateRibbonTab(tabName);
            
            #region AR
            RibbonPanel AR_panel = a.CreateRibbonPanel(tabName, "АР");
            
            // creating the buttons 
            PushButtonData roomDecoration = new PushButtonData("Room_Decoration", "Отделка Помещений", Assembly.GetExecutingAssembly().Location, typeof(AR_RoomDecoration).FullName);
            roomDecoration.ToolTip = "Отделка Помещений";
            PushButton roomDecorationBtn = AR_panel.AddItem(roomDecoration) as PushButton;

            PushButtonData rolledSteel = new PushButtonData("AR_RolledSteel", "Металлопрокат", Assembly.GetExecutingAssembly().Location, typeof(AR_RolledSteel).FullName);
            rolledSteel.ToolTip = "Металлопрокат";
            PushButton rolledSteelBtn = AR_panel.AddItem(rolledSteel) as PushButton;

            // setting image to button
            Image roomDecorationImg = Properties.Resources.RoomDecorationPic;
            roomDecorationBtn.LargeImage = ConvertToBitmap(roomDecorationImg, new Size(32, 32));
            roomDecorationBtn.Image = ConvertToBitmap(roomDecorationImg, new Size(16, 16));

            Image rolledSteelImg = Properties.Resources.AR_RolledSteel;
            rolledSteelBtn.LargeImage = ConvertToBitmap(rolledSteelImg, new Size(32, 32));
            rolledSteelBtn.Image = ConvertToBitmap(rolledSteelImg, new Size(16, 16));
            #endregion


            #region Struct 
            //RibbonPanel KR_panel = a.CreateRibbonPanel(tabName, "КР");

            //// creating the buttons 
            //PushButtonData metallRolling = new PushButtonData("Metall_Rolling", "Металлопрокат", Assembly.GetExecutingAssembly().Location, typeof(Struct_RolledSteel).FullName);
            //metallRolling.ToolTip = "Металлопрокат";
            //PushButton metallRollingBtn = KR_panel.AddItem(metallRolling) as PushButton;

            //// setting image to button
            //Image metalRollingImg = Properties.Resources.RoomDecorationPic;
            //metallRollingBtn.LargeImage = ConvertToBitmap(metalRollingImg, new Size(32, 32));
            //metallRollingBtn.Image = ConvertToBitmap(metalRollingImg, new Size(16, 16));
            #endregion


            #region Test 
            RibbonPanel TestPanel = a.CreateRibbonPanel(tabName, "Тест");

            // creating the buttons 
            PushButtonData test = new PushButtonData("Test", "Тест", Assembly.GetExecutingAssembly().Location, typeof(RVT_ParametersCompared).FullName);
            test.ToolTip = "Тест";
            PushButton testBtn = TestPanel.AddItem(test) as PushButton;
            #endregion

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        public BitmapImage ConvertToBitmap(Image img, Size size)
        {
            img = (Image)(new Bitmap(img, size));
            using (MemoryStream memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
