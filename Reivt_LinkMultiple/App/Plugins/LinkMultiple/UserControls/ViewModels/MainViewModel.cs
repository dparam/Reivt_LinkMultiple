using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.ExternalEvents;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Window_LinkMultiple _window;
        private Document _document = null;
        private UIApplication _uIApplication = null;


        private string pathListString = "";
        public string PathListString
        {
            get => pathListString;

            set
            {
                pathListString = value;
                OnPropertyChanged(nameof(PathListString));
            }
        }


        ExternalEvent _externalEvent_Subscribe;
        ExternalEvent _externalEvent_Unsubscribe;
        ExternalEvent _externalEvent_LinkMultiple;


        public MainViewModel(Document document, UIApplication uIApplication, Window_LinkMultiple window)
        {
            _window = window;
            _document = document;
            _uIApplication = uIApplication;

            IExternalEventHandler externalEventHandler_Subscribe = new ExternalEvent_Subscribe(this);
            IExternalEventHandler externalEventHandler_Unsubscribe = new ExternalEvent_Unsubscribe(this);
            IExternalEventHandler externalEventHandler_LinkMultiple = new ExternalEvent_LinkMultiple(this);

            _externalEvent_Subscribe = ExternalEvent.Create(externalEventHandler_Subscribe);
            _externalEvent_Unsubscribe = ExternalEvent.Create(externalEventHandler_Unsubscribe);
            _externalEvent_LinkMultiple = ExternalEvent.Create(externalEventHandler_LinkMultiple);

            _externalEvent_Subscribe.Raise();
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        // Events

        public void OnStart(object sender, RoutedEventArgs e)
        {
            _externalEvent_LinkMultiple.Raise();
            _window.Close();
        }


        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _externalEvent_Unsubscribe.Raise();
        }


        public void OnAddFile(object sender, RoutedEventArgs e)
        {
            RevitCommandId revitCommandId = RevitCommandId.LookupCommandId("ID_REVIT_FILE_OPEN");
            _uIApplication.PostCommand(revitCommandId);
        }


        internal void OnDocumentOpening(object sender, DocumentOpeningEventArgs e)
        {
            e.Cancel();
            PathListString += e.PathName + "\n";
        }
    }
}
