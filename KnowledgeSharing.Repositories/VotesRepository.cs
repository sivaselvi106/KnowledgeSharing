using KnowlegdeSharing.DomainModels;
using System.Linq;

namespace KnowledgeSharing.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int answerId, int userId, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        KnowledgeSharingDbContext knowledgesharingDB;

        public VotesRepository()
        {
            knowledgesharingDB = new KnowledgeSharingDbContext();
        }

        public void UpdateVote(int answerId, int userId, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Vote vote = knowledgesharingDB.Votes.Where(temp => temp.AnswerID == answerId && temp.UserID == userId).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerID = answerId, UserID = userId, VoteValue = updateValue };
                knowledgesharingDB.Votes.Add(newVote);
            }
            knowledgesharingDB.SaveChanges();
        }
    }
}
