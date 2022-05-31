using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.ModuleStudent.ModuleControlPanel.Domain.Models
{
    public class TestHistoryFormattedEntity
    {
        public string TestName { get; set; }
        public string Result { get; set; }
        public string SpentTime { get; set; }
        public string ScoreString { get; set; }
        public string Date { get; set; }
    }
}
