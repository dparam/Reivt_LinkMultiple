using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Reivt_LinkMultiple.App.Plugins.LinkMultiple.UserControls.ViewModels;
using Reivt_LinkMultiple.App.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Reivt_LinkMultiple.App.Plugins.LinkMultiple.ExternalEvents
{
    [Transaction(TransactionMode.Manual)]
    public class ExternalEvent_LinkMultiple : IExternalEventHandler
    {
        private int _successCount;
        private int _failCount;
        private string _reportText;

        MainViewModel _mainViewModel;


        public ExternalEvent_LinkMultiple(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


        public void Execute(UIApplication uiApplication)
        {
            _reportText = "";
            _successCount = 0;
            _failCount = 0;

            Document document = uiApplication.ActiveUIDocument.Document;

            if (document.IsFamilyDocument)
            {
                MessageBox.Show("Плагин работает только с моделями");
                return;
            }

            string[] paths = _mainViewModel.PathListString.Split(new[] { '\n' });
            HashSet<string> pathsHashSet = new HashSet<string>();

            foreach (string path in paths)
                pathsHashSet.Add(path);

            //

            Transaction transaction = new Transaction(document, "Reivt_LinkMultiple - Связать несколько");
            transaction.Start();


            foreach (string path in pathsHashSet)
            {
                string newPath = path;
                if (String.IsNullOrEmpty(newPath)) continue;

                try
                {
                    if (newPath.Contains("domparketa.corp"))
                        newPath = "RSN://" + newPath;

                    ModelPath modelPath = ModelPathUtils.ConvertUserVisiblePathToModelPath(newPath);

                    RevitLinkOptions revitLinkOptions = new RevitLinkOptions(false);

                    var linkType = RevitLinkType.Create(document, modelPath, revitLinkOptions);
                    var instance = RevitLinkInstance.Create(document, linkType.ElementId, placement: ImportPlacement.Shared);

                    instance.Pinned = true;
                    _successCount++;
                }
                catch (Exception exception)
                {
                    if (exception.Message.Contains("document already contains"))
                    {
                        _reportText +=
                            newPath +
                            "\n" +
                            "Описание ошибки: В документ уже загружена данная связь" +
                            "\n\n";
                    }
                    else if (exception.Message.Contains("do not share the same coordinate system"))
                    {
                        _successCount++;
                        _reportText +=
                            newPath +
                            "\n" +
                            "Описание ошибки: Модель и связь имеют разные системы координат\n" +
                            "Связь будет загружена, но не размещена в модели" +
                            "\n\n";
                    }
                    else if (exception.Message.Contains("is not a valid Revit file"))
                    {
                        _reportText +=
                            newPath +
                            "\n" +
                            "Описание ошибки: По данному пути не найдена подходящая модель" +
                            "\n\n";
                    }
                    else
                    {
                        _reportText +=
                            newPath +
                            "\n" +
                            "Описание ошибки: " + exception.Message +
                            "\n\n";
                    }

                    _failCount++;
                    continue;
                }
            }

            transaction.Commit();

            //

            DebugWindow debugWindow = new DebugWindow(
                "Загружено: " + _successCount + "\n" +
                "Ошибок: " + _failCount + "\n\n" +
                _reportText);

            debugWindow.ShowDialog();
        }


        public string GetName()
        {
            return "ExternalEvent_LinkMultiple";
        }
    }
}
