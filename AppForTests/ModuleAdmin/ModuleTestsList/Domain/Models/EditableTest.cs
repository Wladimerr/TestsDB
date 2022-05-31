using AppForTests.Shared.Domain;
using AppForTests.Shared.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleAdmin.ModuleTestsList.Domain.Models
{
    public class EditableTest : NotifiedProperties
    {
        public EditableTest()
        {
            Questions = new List<EditableQuestion>();
        }

        private int _id;
        private string _title;
        private ICollection<EditableQuestion> _questions;

        public int Id { get => _id; set { _id = value; PropertyHasChanged(); } }
        public string Tilte { get => _title; set { _title = value; PropertyHasChanged(); } }

        public virtual ICollection<EditableQuestion> Questions { get => _questions; set { _questions = value; PropertyHasChanged(); } }
    }
}
