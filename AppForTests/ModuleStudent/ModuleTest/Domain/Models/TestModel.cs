using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleStudent.ModuleTest.Domain.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public int RemainingSeconds { get; set; }
        public int MaxTime { get; set; }
    }
}
