using AppForTests.Shared.Domain;
using AppForTests.Shared.Domain;
using AppForTests.Shared.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppForTests.ModuleAuth.Domain
{
    internal class LogInViewModel : ViewModelBase
    {
        private string[] _loginsList = { };
        private string _login = "";
        private string _password = "";

        public async Task Load()
        {
            await LoadLogins();
        }

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

        public string[] LoginsList
        {
            get => _loginsList;
            set { _loginsList = value; PropertyHasChanged(); }
        }

        private async Task LoadLogins()
        {
            using var dao = new TestsDBContext();
            LoginsList = await dao.Users
                .Select(u => u.Login)
                .ToArrayAsync();
        }

        private Command _loginCommand;
        public Command LoginCommand => _loginCommand ??= new Command( async (o) =>
        {
            try
            {
                await AuthManager.LogInAsync(_login, _password);

                if (AuthManager.CurrentUser.IsAdmin)
                    NavController.NavigateToAdminControlPanel();
                else
                    NavController.NavigateToStudentControlPanel();
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

            FieldsCleared?.Invoke();
        });

        private Command _goToRegCommand;
        public Command GoToRegistrationCommand => _goToRegCommand ??= new Command((o) =>
        {
            NavController.NavigateToRegistration();
        });
    }
}
