using AppForTests.ModuleAdmin.ModuleControlPanel.Presentation.Pages;
using AppForTests.ModuleAdmin.ModuleTestsList.Presentation.Pages;
using AppForTests.ModuleAuth.Presentation;
using AppForTests.ModuleStudent.ModuleControlPanel.Presentation;
using AppForTests.ModuleStudent.ModuleControlPanel.Presentation.Pages;
using AppForTests.ModuleStudent.ModuleTest.Presentation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppForTests.Shared.Domain
{
    public static class NavController
    {
        private static Frame _frame;

        public static Action<string> OnNavigated { get; set; }

        public static void Setup(Frame frame)
        {
            _frame = frame;
        }

        public static void NavigateToLogIn()
        {
            _frame.Content = new LogInPage();
            OnNavigated?.Invoke("Вход");
        }

        public static void NavigateToRegistration()
        {
            _frame.Content = new RegistrationPage();
            OnNavigated?.Invoke("Регистрация");
        }

        public static void NavigateToStudentControlPanel()
        {
            _frame.Content = new StudentControlPanelPage();
            OnNavigated?.Invoke("Панель управления студента");
        }

        public static void NavigateToTestPage(int testId)
        {
            _frame.Content = new TestPage(testId);
            OnNavigated?.Invoke("Тестирование");
        }

        public static void NavigateToStudentsAdminPanel()
        {
            //_frame.Content = new StudentControlPanelPage();
            OnNavigated?.Invoke("Панель управления студентами");
        }

        public static void NavigateToTestsControlPanel()
        {
            _frame.Content = new AdminTestsListPage();
            OnNavigated?.Invoke("Панель управления тестами");
        }

        public static void NavigateToAdminControlPanel()
        {
            _frame.Content = new AdminControlPanelPage();
            OnNavigated?.Invoke("Панель управления администратора");
        }
    }
}
