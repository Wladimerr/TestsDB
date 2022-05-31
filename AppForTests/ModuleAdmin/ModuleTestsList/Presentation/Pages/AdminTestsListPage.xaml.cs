using AppForTests.ModuleAdmin.ModuleTestsList.Domain;
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

namespace AppForTests.ModuleAdmin.ModuleTestsList.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminTestsListPage.xaml
    /// </summary>
    public partial class AdminTestsListPage : Page
    {
        private readonly AdminTestListViewModel _viewModel;
        public AdminTestsListPage()
        {
            InitializeComponent();

            _viewModel = (AdminTestListViewModel)DataContext;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadTests();
        }
    }
}
