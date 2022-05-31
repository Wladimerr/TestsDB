using AppForTests.ModuleAdmin.ModuleTestsList.Domain.Models;
using AppForTests.Shared.Domain;
using AppForTests.Shared.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleAdmin.ModuleTestsList.Domain
{
    public class AdminTestListViewModel : ViewModelBase
    {
        private Test[] _tests;
        private EditableTest _selectedEditableTest;
        private Test _selectedTest;
        private EditableQuestion _selectedQuestion;
        private readonly TestsDBContext _dao;

        public AdminTestListViewModel()
        {
            _dao = new TestsDBContext();
        }

        public Test[] Tests { get => _tests; set { _tests = value; PropertyHasChanged(); } }
        public Test SelectedTest 
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;
                PropertyHasChanged();

                SelectedEditableTest = new EditableTest
                {
                    Id = _selectedTest.Id,
                    Tilte = _selectedTest.Title,
                    Questions = _selectedTest.Questions
                    .Select(q =>
                        new EditableQuestion
                        {
                            Id = q.Id,
                            Tilte = q.Title,
                            MaxTime = q.MaxTime,
                            Answers = q.Answers
                            .Select(a =>
                             new EditableAnswer
                             {
                                 Id = a.Id,
                                 Tilte = a.Tilte,
                                 IsRight = a.IsRight
                             }).ToList()
                        }).ToList()
                };
            }
        }
        public EditableTest SelectedEditableTest { get => _selectedEditableTest; set { _selectedEditableTest = value; PropertyHasChanged(); } }
        public EditableQuestion SelectedQuestion { get => _selectedQuestion; set { _selectedQuestion = value; PropertyHasChanged(); } }

        public async Task LoadTests()
        {
            Tests = await _dao.Tests.ToArrayAsync();
        }

        private Command _save;
        public Command Save
            => _save ??= new Command(async _ =>
            {
                _selectedTest.Title = _selectedEditableTest.Tilte;  

                var addedQ = _selectedEditableTest.Questions.Where(q => !_selectedTest.Questions.Any(sq => sq.Id == q.Id));
                var removedQ = _selectedTest.Questions.Where(q => !_selectedEditableTest.Questions.Any(sq => sq.Id == q.Id));
                var remainQ = _selectedEditableTest.Questions.Where(q => _selectedTest.Questions.Any(sq => sq.Id == q.Id));

                foreach(var q in remainQ)
                {
                    var entity = _selectedTest.Questions.FirstOrDefault(qs => qs.Id == q.Id);

                    entity.Title = q.Tilte;
                    entity.MaxTime = q.MaxTime;

                    var addedA = q.Answers.Where(a => !entity.Answers.Any(ans => ans.Id == a.Id));
                    var removedA = entity.Answers.Where(a => !q.Answers.Any(ans => ans.Id == a.Id));
                    var remainA = q.Answers.Where(a => entity.Answers.Any(ans => ans.Id == a.Id));

                    foreach(var a in remainA)
                    {
                        var entityA = entity.Answers.FirstOrDefault(ans => ans.Id == a.Id);

                        entityA.Tilte = a.Tilte;
                        entityA.IsRight = a.IsRight;
                    }
                }

                await _dao.SaveChangesAsync();
                await LoadTests();
            });
    }
}
