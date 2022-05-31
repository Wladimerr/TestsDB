using AppForTests.ModuleStudent.ModuleTest.Domain;
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

namespace AppForTests.ModuleStudent.ModuleTest.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        private TestPageViewModel _viewModel;
        private int _testId;
        public TestPage()
        {
            InitializeComponent();

            _viewModel = (TestPageViewModel)DataContext;
        }

        public TestPage(int testId) : this()
        {
            _testId = testId;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_testId == 0)
                return;

            await _viewModel.Setup(_testId);
        }
    }
}
