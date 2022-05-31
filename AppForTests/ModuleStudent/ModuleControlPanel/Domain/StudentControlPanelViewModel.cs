using AppForTests.ModuleStudent.ModuleControlPanel.Domain.Models;
using AppForTests.Shared.Domain;
using AppForTests.Shared.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleStudent.ModuleControlPanel.Domain
{
    public class StudentControlPanelViewModel : ViewModelBase
    {
        private TestHistoryFormattedEntity[] _testHistory;
        private Test[] _tests;
        private Test _selectedTest;

        public string StudentFIO { get => AuthManager.CurrentUser.FullName; }
        public string StudentLogin { get => AuthManager.CurrentUser.Login; }
        public TestHistoryFormattedEntity[] TestsHistory { get => _testHistory; set { _testHistory = value; PropertyHasChanged(); } }
        public Test[] Tests { get => _tests; set { _tests = value; PropertyHasChanged(); } }
        public Test SelectedTest { get => _selectedTest; set { _selectedTest = value; PropertyHasChanged(); } }

        public async Task LoadTestsHistory()
        {
            using TestsDBContext dao = new();

            var user = await dao.Users.FindAsync(AuthManager.CurrentUser.Id);
            TestsHistory = user.TestsHistories
                .Select(h =>
                new TestHistoryFormattedEntity
                {
                    TestName = h.Test.Title,
                    Result = "-",
                    SpentTime = DateTimeFormatter.FromSecondsToLettersString(h.TimeSpentInSeconds),
                    ScoreString = $"{ScoreCounter.GetScore(h.Id)}/{h.Test.Questions.Count}",
                    Date = h.Date.ToShortDateString()
                })
                .ToArray();
        }

        public async Task LoadTests()
        {
            using TestsDBContext dao = new();

            Tests = await dao.Tests.ToArrayAsync();
        }

        private Command _goToTestCommand;
        public Command GoToTestCommand
        {
            get => _goToTestCommand ??= new Command(
                o =>
                {
                    NavController.NavigateToTestPage(SelectedTest.Id);
                },
                c => SelectedTest != null);
        }

        private Command _refreshCommand;
        public Command RefhreshCommand
        {
            get => _refreshCommand ??= new Command(
                async o =>
                {
                    await LoadTestsHistory();
                });
        }
    }
}
