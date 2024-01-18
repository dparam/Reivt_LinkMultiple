using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;

namespace Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.Views
{
    public partial class Window_LinkMultiple : Window
    {
        private MainViewModel _mainViewModel = null;


        public Window_LinkMultiple(Document document, UIApplication uIApplication)
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(document, uIApplication, this);
            DataContext = _mainViewModel;

            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.Owner = ComponentManager.ApplicationWindow;
        }


        // Events


        private void OnStart(object sender, RoutedEventArgs e)
        {
            _mainViewModel.OnStart(sender, e);
        }


        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void OnAddFile(object sender, RoutedEventArgs e)
        {
            _mainViewModel.OnAddFile(sender, e);
        }


        // Subscribe events

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _mainViewModel.OnWindowClosing(sender, e);
        }
    }
}
