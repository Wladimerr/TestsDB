using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Domain
{
    public static class ScoreCounter
    {
        public static float GetScore(int testHistoryId)
        {
            var totalScore = 0f;

            using TestsDBContext dao = new();

            var testHistory = dao.TestsHistories.Find(testHistoryId);

            foreach (var question in testHistory.Test.Questions)
            {
                var questionHistory = testHistory.QuestionsHistories.FirstOrDefault(q => q.QuestionId == question.Id);
                var questionScore = 0f;
                var answersCount = question.Answers.Count;
                var rightAnswersCount = question.Answers.Where(a => a.IsRight).Count();

                var selectedRightAnswersCount = question.Answers
                    .Where(a => a.IsRight && questionHistory.AnswersHistories.Any(ah => ah.AnswerId == a.Id))
                    .Count();
                var selectedWrongAnswersCount = question.Answers
                    .Where(a => (!a.IsRight) && questionHistory.AnswersHistories.Any(ah => ah.AnswerId == a.Id))
                    .Count();

                if (rightAnswersCount != 0)
                    questionScore += 1f * selectedRightAnswersCount / rightAnswersCount;
                else
                    questionScore = 1f;

                questionScore -= 1f * selectedWrongAnswersCount / answersCount;

                if (questionScore < 0) questionScore = 0;

                totalScore += questionScore;
            }

            return totalScore;
        }
    }
}
