using KnowlegdeSharing.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeSharing.Repositories
{

    public interface IAnswersRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void UpdateAnswerVotesCount(int answerId, int userId, int value);
        void DeleteAnswer(int answerId);
        List<Answer> GetAnswersByQuestionID(int questionId);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersRepository : IAnswersRepository
    {
        KnowledgeSharingDbContext knowledgesharingDB;
        IQuestionRepository questionRepository ;
        IVotesRepository votesRepository;

        public AnswersRepository()
        {
            knowledgesharingDB = new KnowledgeSharingDbContext();
            questionRepository = new QuestionsRepository(); 
            votesRepository = new VotesRepository();
        }

        public void InsertAnswer(Answer answer)
        {
            knowledgesharingDB.Answers.Add(answer);
            knowledgesharingDB.SaveChanges();
            questionRepository.UpdateQuestionAnswersCount(answer.QuestionID, 1);
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer updateanswer = knowledgesharingDB.Answers.Where(temp => temp.AnswerID == answer.AnswerID).FirstOrDefault();
            if (updateanswer != null)
            {
                updateanswer.AnswerText = answer.AnswerText;
                knowledgesharingDB.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int answerId, int userId, int value)
        {
            Answer updateanswer = knowledgesharingDB.Answers.Where(temp => temp.AnswerID == answerId).FirstOrDefault();
            if (updateanswer != null)
            {
                updateanswer.VotesCount += value;
                knowledgesharingDB.SaveChanges();
                questionRepository.UpdateQuestionVotesCount(updateanswer.QuestionID, value);
                votesRepository.UpdateVote(answerId, userId, value);
            }
        }

        public void DeleteAnswer(int answerId)
        {
            Answer updateanswer = knowledgesharingDB.Answers.Where(temp => temp.AnswerID == answerId).First();
            if (updateanswer != null)
            {
                knowledgesharingDB.Answers.Remove(updateanswer);
                knowledgesharingDB.SaveChanges();
                questionRepository.UpdateQuestionAnswersCount(updateanswer.QuestionID, -1);
            }
        }
        public List<Answer> GetAnswersByQuestionID(int qid)
        {
            List<Answer> updateanswer = knowledgesharingDB.Answers.Where(temp => temp.QuestionID == qid).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return updateanswer;
        }
        public List<Answer> GetAnswersByAnswerID(int answerId)
        {
            List<Answer> updateanswer = knowledgesharingDB.Answers.Where(temp => temp.AnswerID == answerId).ToList();
            return updateanswer;
        }
    }
}
