using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Form = System.Windows.Forms.Form;

namespace TrTrestAddin_MK.Windows
{
    public partial class Struct_MetalRollingForm : Form
    {
        public Struct_MetalRollingForm()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
    public class ExEventClass : IExternalEventHandler   // Реализую интерфейс обработчика внешнего события
    {
        public void Execute(UIApplication app)
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }

}
