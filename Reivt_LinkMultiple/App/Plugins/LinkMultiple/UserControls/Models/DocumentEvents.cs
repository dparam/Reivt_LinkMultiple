using Autodesk.Revit.DB.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.Models
{
    public static class DocumentEvents
    {
        public static void OnDocumentOpening(object sender, DocumentOpeningEventArgs e)
        {
            e.Cancel();
            //this.text_box.Text += e.PathName + "\n";
        }
    }
}
