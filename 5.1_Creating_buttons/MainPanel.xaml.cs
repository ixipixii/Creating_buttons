using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Creating_buttons
{
    /// <summary>
    /// Логика взаимодействия для MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        private ExternalCommandData _commandData;
        public MainPanel(ExternalCommandData commandData)
        {
            InitializeComponent();
            _commandData = commandData;
        }

        private void SelectCountPipe(object sender, RoutedEventArgs e)
        {
            VM.Hide();
            var uiapp = _commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            Document doc = _commandData.Application.ActiveUIDocument.Document;

            var collector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeCurves)
                .WhereElementIsNotElementType()
                .ToList();

            TaskDialog.Show("Вывод", $"Количество всех труб в проекте: {collector.Count}");
            VM.ShowDialog();

        }

        private void SelectVolumeWall(object sender, RoutedEventArgs e)
        {
            VM.Hide();
            var uiapp = _commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            Document doc = _commandData.Application.ActiveUIDocument.Document;

            var collector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType()
                .ToList();

            double volumeSum = 0.0;

            foreach(var element in collector)
            {
                volumeSum += UnitUtils.ConvertFromInternalUnits(
                                        element.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(),
                                        UnitTypeId.CubicMeters);
            }

            TaskDialog.Show("Вывод", $"Объём всех стен в проекте: {volumeSum}");
            VM.ShowDialog();
        }

        private void SelectCountDoors(object sender, RoutedEventArgs e)
        {
            VM.Hide();
            var uiapp = _commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            Document doc = _commandData.Application.ActiveUIDocument.Document;

            var collector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .ToList();

            TaskDialog.Show("Вывод", $"Количество всех дверей в проекте: {collector.Count}");
            VM.ShowDialog();
        }
    }
}
