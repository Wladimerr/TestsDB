using AppForTests.Shared.Domain;
using AppForTests.Shared.Domain;
using AppForTests.Shared.Models;
using AppForTests.Shared.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppForTests.ModuleAuth.Domain
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _login = "";
        private string _fio = "";
        private string _password = "";
        private string _confirmPassword = "";

        public string Login
        {
            get => _login;
            set { _login = value; PropertyHasChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; PropertyHasChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; PropertyHasChanged(); }
        }

        public string FIO
        {
            get => _fio;
            set { _fio = value; PropertyHasChanged(); }
        }

        private Command _registerCommand;
        public Command RegisterCommand => _registerCommand ??= new Command(async (o) =>
        {
            try
            {
                await AuthManager.RegisterAsync(new UserRegisterModel
                {
                    Login = _login,
                    Password = _password,
                    ConfirmPassword = _confirmPassword,
                    FIO = _fio
                });

                MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public Action FieldsCleared { get; set; }

        private Command _clearCommand;
        public Command ClearCommand => _clearCommand ??= new Command((o) =>
        {
            Password = "";
            Login = "";
            ConfirmPassword = "";
            FIO = "";

            FieldsCleared?.Invoke();
        });

        private Command _goBack;
        public Command GoBack
            => _goBack ??= new Command(_ =>
            {
                NavController.NavigateToLogIn();
            });
    }
}
