using AppForTests.ModuleAuth.Domain;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private RegisterViewModel _viewModel;
        public RegistrationPage()
        {
            InitializeComponent();

            _viewModel = (RegisterViewModel)DataContext;

            _viewModel.FieldsCleared = () =>
            {
                PasswordBox.Password = "";
                ConfirmPasswordBox.Password = "";
            };
        }

        private void PasswordBox_ConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;
        }
    }
}
