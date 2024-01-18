using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.Models;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.ViewModels;
using System;
using System.Windows;


namespace Reivt_LinkMultiple.App.Plugins.LinkMultiple.ExternalEvents
{
    [Transaction(TransactionMode.Manual)]
    public class ExternalEvent_Subscribe : IExternalEventHandler
    {
        MainViewModel _mainViewModel;


        public ExternalEvent_Subscribe(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


        public void Execute(UIApplication app)
        {
            try
            {
                app.Application.DocumentOpening += _mainViewModel.OnDocumentOpening;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "LinkMultiple");
                return;
            }
        }


        public string GetName()
        {
            return "ExternalEvent_Subscribe";
        }
    }
}
