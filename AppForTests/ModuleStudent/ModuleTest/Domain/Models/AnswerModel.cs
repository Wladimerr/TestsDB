using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleStudent.ModuleTest.Domain.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsRight { get; set; }
        public bool IsChecked { get; set; }
    }
}
