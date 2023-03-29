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

namespace TrTrestAddin_MK.Commands
{    
    [Transaction(TransactionMode.Manual)]
    public class Struct_MetalRolling : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Struct_MetalRollingForm frm = new Struct_MetalRollingForm(commandData);
            frm.Show();
            return Result.Succeeded;
        }
    }
}
