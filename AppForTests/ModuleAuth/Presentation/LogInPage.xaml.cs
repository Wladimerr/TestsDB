using AppForTests.ModuleAuth.Domain;
using AppForTests.Shared.Utils;
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

namespace AppForTests.ModuleAuth.Presentation
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private LogInViewModel _viewModel;
        public LogInPage()
        {
            InitializeComponent();

            _viewModel = (LogInViewModel)DataContext;

            _viewModel.FieldsCleared = () =>
            {
                PasswordBox.Password = "";
            };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel?.Load();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = ((PasswordBox)sender).Password;
        }
    }
}
