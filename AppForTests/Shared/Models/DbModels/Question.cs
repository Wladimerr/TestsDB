﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AppForTests.Shared.Models.DbModels
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            QuestionsHistories = new HashSet<QuestionsHistory>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public string Title { get; set; }
        public int MaxTime { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionsHistory> QuestionsHistories { get; set; }
    }
}