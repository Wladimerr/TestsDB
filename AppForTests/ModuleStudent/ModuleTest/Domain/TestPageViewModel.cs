using AppForTests.ModuleStudent.ModuleTest.Domain.Models;
using AppForTests.Shared.Domain;
using AppForTests.Shared.Models.DbModels;
using AppForTests.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace AppForTests.ModuleStudent.ModuleTest.Domain
{
    public class TestPageViewModel : ViewModelBase
    {
        private TestModel _currentTest;
        private int _currentQuestionSerialNumber = 0;
        private Timer _testTimer;
        private Timer _questionTimer;
        private DateTime _startDate;
        private string _testTimerLabel;
        private string _questionTimerLabel;

        public TestModel CurrentTest { get => _currentTest; set { _currentTest = value; PropertyHasChanged(); } }
        public string TestTimerLabel { get => _testTimerLabel; set { _testTimerLabel = value; PropertyHasChanged(); } }
        public string QuestionTimerLabel { get => _questionTimerLabel; set { _questionTimerLabel = value; PropertyHasChanged(); } }
        public QuestionModel CurrentQuestion
        {
            get
            {
                if (_currentTest != null && _currentTest.Questions.Count == 0)
                    return null;

                return _currentTest?.Questions[_currentQuestionSerialNumber];
            }
        }
        public string StudentFIO { get => AuthManager.CurrentUser.FullName; }

        private void SetQuestion(int serialNumber)
        {
            _currentQuestionSerialNumber = serialNumber;
            PropertyHasChanged("CurrentQuestion");
            if (CurrentQuestion.RemainingSeconds == -1)
            {
                _questionTimer.Enabled = false;
                QuestionTimerLabel = "Неограниченно";
            }
            else if (CurrentQuestion.RemainingSeconds > 0)
                _questionTimer.Enabled = true;

        }

        public async Task Setup(int testId)
        {
            using TestsDBContext dao = new();

            var test = await dao.Tests.FindAsync(testId);

            _currentTest = test?.Let(t =>
                new TestModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Questions = t?.Questions
                    ?.Select(q =>
                    new QuestionModel
                    {
                        Id = q.Id,
                        Title = q.Title,
                        Answers = q.Answers
                        .Select(a =>
                        new AnswerModel
                        {
                            Id = a.Id,
                            Title = a.Tilte,
                            IsRight = a.IsRight,
                            IsChecked = false
                        }).ToList(),
                        RemainingSeconds = q.MaxTime
                    }).ToList()
                });

            if (_currentTest.Questions.Any(q => q.RemainingSeconds == -1))
            {
                _currentTest.RemainingSeconds = -1;
                _currentTest.MaxTime = -1;
                TestTimerLabel = "Неограниченно";
            }
            else
            {
                _currentTest.Questions.ForEach(q =>
                {
                    _currentTest.MaxTime += q.RemainingSeconds;
                });

                _currentTest.RemainingSeconds = _currentTest.MaxTime;

                _testTimer = new Timer();
                _testTimer.Interval = 1000;
                _testTimer.AutoReset = true;
                _testTimer.Elapsed += (s, e) =>
                {
                    if (_currentTest.RemainingSeconds == 0)
                    {
                        _questionTimer.Stop();
                        _testTimer.Stop();

                        Application.Current.Dispatcher.Invoke(finishFun);
                        return;
                    }

                    _currentTest.RemainingSeconds--;
                    TestTimerLabel = _currentTest.RemainingSeconds.ToString();
                };
            }

            _questionTimer = new Timer();
            _questionTimer.Interval = 1000;
            _questionTimer.AutoReset = true;
            _questionTimer.Elapsed += (s, e) =>
            {
                var qind = CurrentTest.Questions.IndexOf(CurrentQuestion);

                if (CurrentQuestion.RemainingSeconds == 0 && qind < CurrentTest.Questions.Count - 1)
                {
                    SetQuestion(_currentQuestionSerialNumber + 1);
                    CurrentQuestion.RemainingSeconds--;
                }
                else if (CurrentQuestion.RemainingSeconds == 0 && qind == CurrentTest.Questions.Count - 1)
                {
                    //_questionTimer.Stop();
                    //_testTimer.Stop();

                    //Application.Current.Dispatcher.Invoke(finishFun);
                }
                else
                {
                    CurrentQuestion.RemainingSeconds--;
                }
                
                QuestionTimerLabel = CurrentQuestion.RemainingSeconds.ToString();
            };

            _startDate = DateTime.Now;
            _testTimer?.Start();
            SetQuestion(0);
        }

        private Command _prewQuestionCommand;
        public Command PrewQuestionCommand
        {
            get => _prewQuestionCommand ??= new Command(
                o =>
                {
                    SetQuestion(_currentQuestionSerialNumber - 1);
                },
                c => _currentQuestionSerialNumber != 0);
        }

        private Command _nextQuestionCommand;
        public Command NextQuestionCommand
        {
            get => _nextQuestionCommand ??= new Command(
                o =>
                {
                    SetQuestion(_currentQuestionSerialNumber + 1);
                },
                c => _currentTest?.Let(ct => ct.Questions.Count - 1 != _currentQuestionSerialNumber) ?? false);
        }

        private Command _backCommand;
        public Command BackCommand
        {
            get => _backCommand ??= new Command(
                o =>
                {
                    NavController.NavigateToStudentControlPanel();
                });
        }

        [STAThread]
        private void finishFun()
        {
            _fininshCommand.Execute(null);
        }

        private Command _fininshCommand;
        public Command FinishCommand
        {
            get => _fininshCommand ??= new Command(
                async o =>
                {
                    using TestsDBContext dao = new();

                    var testHistory = new TestsHistory
                    {
                        TestId = CurrentTest.Id,
                        QuestionsHistories = CurrentTest.Questions
                        .Select(q =>
                        new QuestionsHistory
                        {
                            QuestionId = q.Id,
                            AnswersHistories = q.Answers
                            .Where(a => a.IsChecked)
                            .Select(a =>
                            new AnswersHistory
                            {
                                AnswerId = a.Id,
                            }).ToArray()
                        }).ToArray(),
                        TimeSpentInSeconds = (int)(DateTime.Now - _startDate).TotalSeconds,
                        UserId = AuthManager.CurrentUser.Id,
                        Date = DateTime.Now
                    };

                    await dao.TestsHistories.AddAsync(testHistory);
                    await dao.SaveChangesAsync();

                    MessageBox.Show("Результат: " + CalcScore() + " из " + CurrentTest.Questions.Count);

                    NavController.NavigateToStudentControlPanel();
                });
        }

        private float CalcScore()
        {
            var totalScore = 0f;

            foreach (var quest in CurrentTest.Questions)
            {
                var answerScore = 0f;
                var answersCount = quest.Answers.Count;
                var rightAnswersCount = quest.Answers.Where(a => a.IsRight).Count();

                var selectedRightAnswersCount = quest.Answers.Where(a => a.IsRight && a.IsChecked).Count();
                var selectedWrongAnswersCount = quest.Answers.Where(a => (!a.IsRight) && a.IsChecked).Count();

                if (rightAnswersCount != 0)
                    answerScore += 1f * selectedRightAnswersCount / rightAnswersCount;
                else
                    answerScore = 1f;

                answerScore -= 1f * selectedWrongAnswersCount / answersCount;

                if (answerScore < 0) answerScore = 0;

                totalScore += answerScore;
            }

            return totalScore;
        }

        ~TestPageViewModel()
        {
            _testTimer?.Dispose();
            _questionTimer?.Dispose();
        }
    }
}
