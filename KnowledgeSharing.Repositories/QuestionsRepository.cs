using KnowlegdeSharing.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeSharing.Repositories
{
    public interface IQuestionRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int questionId, int value);
        void UpdateQuestionAnswersCount(int questionId, int value);
        void UpdateQuestionViewsCount(int questionId, int value);
        void DeleteQuestion(int questionId);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionID(int QuestionID);
    }
    public class QuestionsRepository : IQuestionRepository
    {
        KnowledgeSharingDbContext knowledgesharingDB;
        public QuestionsRepository()
        {
            knowledgesharingDB = new KnowledgeSharingDbContext();
        }

        public void InsertQuestion(Question question)
        {
            knowledgesharingDB.Questions.Add(question);
            knowledgesharingDB.SaveChanges();
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question changeQuestion = knowledgesharingDB.Questions.Where(temp => temp.QuestionID == question.QuestionID).FirstOrDefault();
            if (changeQuestion != null)
            {
                changeQuestion.QuestionName = question.QuestionName;
                changeQuestion.QuestionDateAndTime = question.QuestionDateAndTime;
                changeQuestion.CategoryID = question.CategoryID;
                knowledgesharingDB.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int questionId, int value)
        {
            Question changeQuestion = knowledgesharingDB.Questions.Where(temp => temp.QuestionID == questionId).FirstOrDefault();
            if (changeQuestion != null)
            {
                changeQuestion.VotesCount += value;
                knowledgesharingDB.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersCount(int questionId, int value)
        {
            Question changeQuestion = knowledgesharingDB.Questions.Where(temp => temp.QuestionID == questionId).FirstOrDefault();
            if (changeQuestion != null)
            {
                changeQuestion.AnswersCount += value;
                knowledgesharingDB.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int questionId, int value)
        {
            Question changeQuestion = knowledgesharingDB.Questions.Where(temp => temp.QuestionID == questionId).FirstOrDefault();
            if (changeQuestion != null)
            {
                changeQuestion.ViewsCount += value;
                knowledgesharingDB.SaveChanges();
            }
        }

        public void DeleteQuestion(int questionId)
        {
            Question changeQuestion = knowledgesharingDB.Questions.Where(temp => temp.QuestionID == questionId).FirstOrDefault();
            if (changeQuestion != null)
            {
                knowledgesharingDB.Questions.Remove(changeQuestion);
                knowledgesharingDB.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> changeQuestion = knowledgesharingDB.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return changeQuestion;
        }

        public List<Question> GetQuestionByQuestionID(int QuestionID)
        {
            List<Question> changeQuestion = knowledgesharingDB.Questions.Where(temp => temp.QuestionID == QuestionID).ToList();
            return changeQuestion;
        }
    }
}
