using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Reivt_LinkMultiple.App.Helpers;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.Views;
using System.Windows;

namespace Reivt_LinkMultiple.App.Plugins.LinkMultiple.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command_LinkMultiple_ShowWindow : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApplication = commandData.Application;
            Document document = uiApplication.ActiveUIDocument.Document;

            if (document.IsFamilyDocument)
            {
                MessageBox.Show("Плагин работает только с моделями");
                return Result.Failed;
            }

            //

            try { StatManager.WriteStat(uiApplication.Application.Username, this.GetType().Name); }
            catch (System.Exception) { }

            Window_LinkMultiple window = new Window_LinkMultiple(document, uiApplication);
            window.Show();

            //

            return Result.Succeeded;
        }
    }
}
