﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AppForTests.Shared.Models.DbModels
{
    public partial class AnswersHistory
    {
        public int Id { get; set; }
        public int QuestionHistoryId { get; set; }
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual QuestionsHistory QuestionHistory { get; set; }
    }
}