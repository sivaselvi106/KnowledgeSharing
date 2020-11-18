using AutoMapper;
using KnowledgeSharing.Repositories;
using KnowledgeSharing.ViewModels;
using KnowlegdeSharing.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeSharing.ServiceLayer
{
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel answervm);
        void UpdateAnswer(EditAnswerViewModel answervm);
        void UpdateAnswerVotesCount(int answerid, int userid, int value);
        void DeleteAnswer(int answerid);
        List<AnswerViewModel> GetAnswersByQuestionID(int qid);
        AnswerViewModel GetAnswerByAnswerID(int AnswerID);
    }
    public class AnswersService : IAnswersService
    {
        IAnswersRepository answersRepository;

        public AnswersService()
        {
            answersRepository = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel answervm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<NewAnswerViewModel, Answer>(answervm);
            answersRepository.InsertAnswer(answer);
        }
        public void UpdateAnswer(EditAnswerViewModel answervm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<EditAnswerViewModel, Answer>(answervm);
            answersRepository.UpdateAnswer(answer);
        }
        public void UpdateAnswerVotesCount(int answerid, int userid, int value)
        {
            answersRepository.UpdateAnswerVotesCount(answerid, userid, value);
        }
        public void DeleteAnswer(int answerid)
        {
            answersRepository.DeleteAnswer(answerid);
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int qid)
        {
            List<Answer> answerList = answersRepository.GetAnswersByQuestionID(qid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> answervmList = mapper.Map<List<Answer>, List<AnswerViewModel>>(answerList);
            return answervmList;
        }

        public AnswerViewModel GetAnswerByAnswerID(int AnswerID)
        {
            Answer answer = answersRepository.GetAnswersByAnswerID(AnswerID).FirstOrDefault();
            AnswerViewModel answervm = null;
            if (answer != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                answervm = mapper.Map<Answer, AnswerViewModel>(answer);
            }
            return answervm;
        }
    }
}
