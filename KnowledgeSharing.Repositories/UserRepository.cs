using KnowlegdeSharing.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeSharing.Repositories
{
    public interface IUserRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        void DeleteUser(int userId);
        List<User> GetUsers();
        List<User> GetUsersbyEmailAndPassword(string Email, string Password);
        List<User> GetUsersbyEmail(string Email);
        List<User> GetUsersbyUserID(int UserId);
        int GetLatestUserID();
    }
    public class UserRepository : IUserRepository
    {
        KnowledgeSharingDbContext  knowledgesharingDB;

        public UserRepository()
        {
            knowledgesharingDB = new KnowledgeSharingDbContext();
        }

        public void InsertUser(User user)
        { 
            knowledgesharingDB.Users.Add(user);
            knowledgesharingDB.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User changeuser = knowledgesharingDB.Users.Where(temp => temp.UserID == user.UserID).FirstOrDefault();
            if(changeuser != null)
            {
                changeuser.Name = user.Name;
                changeuser.Mobile = user.Mobile;
                knowledgesharingDB.SaveChanges();
            }
        }
        public void UpdateUserPassword(User user)
        {
            User changeuser = knowledgesharingDB.Users.Where(temp => temp.UserID == user.UserID).FirstOrDefault();
            if (changeuser != null)
            {
                changeuser.PasswordHash = user.PasswordHash;
                knowledgesharingDB.SaveChanges();
            }
        }
        public void DeleteUser(int userId)
        {
            User changeuser = knowledgesharingDB.Users.Where(temp => temp.UserID == userId).FirstOrDefault();
            if (changeuser != null)
            {
                knowledgesharingDB.Users.Remove(changeuser);
                knowledgesharingDB.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> userList = knowledgesharingDB.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();

            return userList;
        }
        public List<User> GetUsersbyEmailAndPassword(string Email, string Password)
        {
            List<User> userList = knowledgesharingDB.Users.Where(temp => temp.Email == Email && temp.PasswordHash == Password).OrderBy(temp => temp.Name).ToList();
            return userList;
        }
        public List<User> GetUsersbyEmail(string Email)
        {
            List<User> userList = knowledgesharingDB.Users.Where(temp => temp.Email == Email).OrderBy(temp => temp.Name).ToList();
            return userList;
        }
        public List<User> GetUsersbyUserID(int UserId)
        {
            List<User> userList = knowledgesharingDB.Users.Where(temp => temp.UserID == UserId).ToList();
            return userList;
        }
        public int GetLatestUserID()
        {
            int latestUserId = knowledgesharingDB.Users.Select(temp => temp.UserID).Max();
            return latestUserId;
        }
    }
}
