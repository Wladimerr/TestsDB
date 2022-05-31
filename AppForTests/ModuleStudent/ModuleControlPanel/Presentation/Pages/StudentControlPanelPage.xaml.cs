using AppForTests.ModuleStudent.ModuleControlPanel.Domain;
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

namespace AppForTests.ModuleStudent.ModuleControlPanel.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentControlPanelPage.xaml
    /// </summary>
    public partial class StudentControlPanelPage : Page
    {
        private StudentControlPanelViewModel _viewModel;
        public StudentControlPanelPage()
        {
            InitializeComponent();

            _viewModel = (StudentControlPanelViewModel)DataContext;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadTests();
            await _viewModel.LoadTestsHistory();
        }
    }
}
