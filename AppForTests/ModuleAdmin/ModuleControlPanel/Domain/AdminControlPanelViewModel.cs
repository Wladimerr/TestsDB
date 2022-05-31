using AppForTests.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleAdmin.ModuleControlPanel.Domain
{
    public class AdminControlPanelViewModel : ViewModelBase
    {
        private Command _goToClientsPage;
        public Command GoToClientsPage
            => _goToClientsPage ??= new Command(_ =>
            {
                NavController.NavigateToStudentsAdminPanel();
            });

        private Command _goToTestsPage;
        public Command GoToTestsPage
            => _goToTestsPage ??= new Command(_ =>
            {
                NavController.NavigateToTestsControlPanel();
            });
    }
}
