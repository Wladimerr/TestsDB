using AppForTests.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleAdmin.ModuleTestsList.Domain.Models
{
    public class EditableQuestion : NotifiedProperties
    {
        public EditableQuestion()
        {
            Answers = new List<EditableAnswer>();
        }

        private int _id;
        private string _title;
        private int _maxTime;
        private ICollection<EditableAnswer> _anwers;

        public int Id { get => _id; set { _id = value; PropertyHasChanged(); } }
        public string Tilte { get => _title; set { _title = value; PropertyHasChanged(); } }
        public int MaxTime { get => _maxTime; set { _maxTime = value == 0 ? -1 : value; PropertyHasChanged(); } }

        public ICollection<EditableAnswer> Answers { get => _anwers; set { _anwers = value; PropertyHasChanged(); } }
    }
}
