using AutoMapper;
using KnowledgeSharing.Repositories;
using KnowledgeSharing.ViewModels;
using KnowlegdeSharing.DomainModels;
using System.Collections.Generic;
using System.Linq;
namespace KnowledgeSharing.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel questionvm);
        void UpdateQuestionDetails(EditQuestionViewModel questionvm);
        void UpdateQuestionVotesCount(int questionid, int value);
        void UpdateQuestionAnswersCount(int questionid, int value);
        void UpdateQuestionViewsCount(int questionid, int value);
        void DeleteQuestion(int questionid);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID);
    }
    public class QuestionsService : IQuestionsService
    {
        IQuestionRepository questionRepository;

        public QuestionsService()
        {
            questionRepository = new QuestionsRepository();
        }

        public void InsertQuestion(NewQuestionViewModel questionvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionViewModel, Question>(questionvm);
            questionRepository.InsertQuestion(question);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel questionvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<EditQuestionViewModel, Question>(questionvm);
            questionRepository.UpdateQuestionDetails(question);
        }

        public void UpdateQuestionVotesCount(int questionid, int value)
        {
            questionRepository.UpdateQuestionVotesCount(questionid, value);
        }
        public void UpdateQuestionAnswersCount(int questionid, int value)
        {
            questionRepository.UpdateQuestionAnswersCount(questionid, value);
        }
        public void UpdateQuestionViewsCount(int questionid, int value)
        {
            questionRepository.UpdateQuestionViewsCount(questionid, value);
        }
        public void DeleteQuestion(int questionid)
        {
            questionRepository.DeleteQuestion(questionid);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questionList = questionRepository.GetQuestions();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> questionvm = mapper.Map<List<Question>, List<QuestionViewModel>>(questionList);
            return questionvm;
        }

        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID = 0)
        {
            Question question = questionRepository.GetQuestionByQuestionID(QuestionID).FirstOrDefault();
            QuestionViewModel questionvm = null;
            if (question != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                questionvm = mapper.Map<Question, QuestionViewModel>(question);

                foreach (var item in questionvm.Answers)
                {
                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();
                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return questionvm;
        }
    }
}
