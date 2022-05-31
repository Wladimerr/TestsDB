using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleStudent.ModuleTest.Domain.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<AnswerModel> Answers { get; set; }

        public int RemainingSeconds { get; set; }
    }
}
