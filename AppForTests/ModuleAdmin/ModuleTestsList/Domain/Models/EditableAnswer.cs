using AppForTests.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleAdmin.ModuleTestsList.Domain.Models
{
    public class EditableAnswer : NotifiedProperties
    {
        private int _id;
        private string _title = "";
        private bool _isRight;
        public int Id { get => _id; set { _id = value; PropertyHasChanged(); } }
        public string Tilte { get => _title; set { _title = value; PropertyHasChanged(); } }
        public bool IsRight { get => _isRight; set { _isRight = value; PropertyHasChanged(); } }
    }
}
