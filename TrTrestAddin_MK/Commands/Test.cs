using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TrTrestAddin_MK.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Test : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Selection sel = uidoc.Selection;
            Reference hasPickOne = sel.PickObject(ObjectType.Element);
            Element el = doc.GetElement(hasPickOne.ElementId);
            var t = el.Location;

            
            // Modify document within a transaction
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                ElementTransformUtils.CopyElement(doc, hasPickOne.ElementId, new XYZ(0, 0, 1));
                TaskDialog.Show("t", "Тест");
                
                tx.Commit();
            }
            return Result.Succeeded;
        }        
    }  
}
