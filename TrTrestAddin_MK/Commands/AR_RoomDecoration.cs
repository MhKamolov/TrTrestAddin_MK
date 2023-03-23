using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;
using TrTrestAddin_MK.Windows;

namespace TrTrestAddin_MK.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class AR_RoomDecoration : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            Room vp = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms).WhereElementIsNotElementType().Cast<Room>().FirstOrDefault();
            if (null == vp) 
                return Result.Failed;
              
            AR_RoomDecorationForm frm = new AR_RoomDecorationForm(commandData);
            frm.Show();
             
            //ElementId elid_room = new ElementId(450779 /*450781*/);
            //ElementId elid_1 = new ElementId(432842);
            //ElementId elid_2 = new ElementId(420255);
            //////Room room = doc.GetElement(elid_room) as  Room;
            //////Wall wall_1 = doc.GetElement(elid_1 ) as Wall;
            //////Wall wall_2 = doc.GetElement(elid_2) as Wall;
            //////SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
            //////List<BoundarySegment> bsegList = room.GetBoundarySegments(opt).Select(seg => seg.Select(s => s)).FirstOrDefault().ToList();

            //////BoundarySegment bseg_1 = bsegList.Where(s => s.ElementId == elid_1).FirstOrDefault();
            //////BoundarySegment bseg_2 = bsegList.Where(s => s.ElementId == elid_2).FirstOrDefault();

            //////var firstPoint = bseg_1.GetCurve()/*.CreateOffset(wall_1.Width * (-1) / 2, new XYZ(0, 0, 1))*/.GetEndPoint(1);
            //////var secondPoint = bseg_2.GetCurve()/*.CreateOffset(wall_2.Width * (-1) / 2, new XYZ(0, 0, 1))*/.GetEndPoint(0);

            ////421827
            ////417867
            //// r 450733

            //Room room = doc.GetElement(elid_room) as Room;
            //Wall wall_1 = doc.GetElement(elid_1) as Wall;
            //Wall wall_2 = doc.GetElement(elid_2) as Wall;
            //SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
            //List<BoundarySegment> bsegList = room.GetBoundarySegments(opt).Select(seg => seg.Select(s => s)).FirstOrDefault().ToList();

            //BoundarySegment bseg_1 = bsegList.Where(s => s.ElementId == elid_1).FirstOrDefault();
            //BoundarySegment bseg_2 = bsegList.Where(s => s.ElementId == elid_2).FirstOrDefault();

            //var firstPoint = bseg_1.GetCurve()/*.CreateOffset(wall_1.Width * (-1) / 2, new XYZ(0, 0, 1))*/.GetEndPoint(1);
            //var secondPoint = bseg_2.GetCurve()/*.CreateOffset(wall_2.Width * (-1) / 2, new XYZ(0, 0, 1))*/.GetEndPoint(0);

            //var firstPointOffset = bseg_1.GetCurve().CreateOffset(wall_1.Width * (-1) / 2, new XYZ(0, 0, 1)).GetEndPoint(1);
            //var secondPointOffset = bseg_2.GetCurve().CreateOffset(wall_2.Width * (-1) / 2, new XYZ(0, 0, 1)).GetEndPoint(0);

            //Curve curve_1 = Line.CreateBound(bseg_1.GetCurve().GetEndPoint(0), bseg_1.GetCurve().GetEndPoint(1));
            //Curve curve_2 = Line.CreateBound(bseg_2.GetCurve().GetEndPoint(0), bseg_2.GetCurve().GetEndPoint(1));

            ////TaskDialog.Show("t", "Room_Decoration.cs is Working");
            //TaskDialog.Show("t", firstPoint + " - " + secondPoint + "\n");

            //TaskDialog.Show("t", "Offset: " + firstPointOffset + " - " + secondPointOffset + "\n");

            //WallType wallType = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType().
            //        Where(w => w.Name.Equals("ADSK_Отделка_Условная_20")).FirstOrDefault() as WallType;

            //ElementId wallType_Id = wallType.Id;
            //double wallType_width = wallType.Width;



            //using (Transaction tx = new Transaction(doc, "Test"))
            //{
            //    tx.Start("Transaction Start");

            //    ElementId levelId_1 = wall_1.LevelId;
            //    Wall mainWall_1 = Wall.Create(doc,
            //                       curve_1,
            //                       wallType_Id,
            //                       levelId_1,
            //                       4,
            //                       0,
            //                       false,
            //                       false);

            //    ElementId levelId_2 = wall_2.LevelId;
            //    Wall mainWall_2 = Wall.Create(doc,
            //                           curve_2,
            //                           wallType_Id,
            //                           levelId_2,
            //                           4,
            //                           0,
            //                           false,
            //                           false);
            //    tx.Commit();
            //}
            //Reference refer = uidoc.Selection.PickObject(ObjectType.Element);
            //Wall mainWall = doc.GetElement(refer) as Wall;450781
            //TaskDialog.Show("t", mainWall.GetMaterialIds(false).FirstOrDefault().ToString()  + "  -  " + mainWall.GetMaterialIds(false).Count.ToString());

            return Result.Succeeded;
        }

    }
}
