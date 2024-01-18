using Autodesk.Revit.UI;
using Reivt_LinkMultiple.App.Ribbons;
using System.Collections.Generic;


namespace Reivt_LinkMultiple.App
{
    public class MainApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication uiControlledApp)
        {
            string tabName = "Reivt_LinkMultiple";

            RibbonPanel ribbonPanel = uiControlledApp.CreateRibbonPanel(tabName);
            CreateRibbonPanel(uiControlledApp, ribbonPanel);
            return Result.Succeeded;
        }


        public Result OnShutdown(UIControlledApplication uiControlledApp)
        {
            return Result.Succeeded;
        }


        //


        public void CreateRibbonPanel(UIControlledApplication uiControlledApp, RibbonPanel ribbonPanel)
        {
            var b32 = RibbonPanelHelpers.CreateButton(
                "BTN_LinkMultiple",
                "Связать\nнесколько",
                "Выбрать несколько моделей для одновременной загрузки связей по общим координатам",
                "Reivt_LinkMultiple.App.Plugins.LinkMultiple.Commands.Command_LinkMultiple_ShowWindow",
                "icon_LinkMultiple_Small.ico");

            ribbonPanel.AddItem(b32);
        }
    }
}
