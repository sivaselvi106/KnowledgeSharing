using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowlegdeSharing.DomainModels;
using KnowledgeSharing.Repositories;
using KnowledgeSharing.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;


namespace KnowledgeSharing.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel registerVM);
        void UpdateUserDetails(EditUserDetailsViewModel editUserDetailsVM);
        void UpdateUserPassword(EditUserPasswordViewModel editUserPasswordVM);
        void DeleteUser(int userID);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int UserID);
    }
    public class UserService : IUsersService
    {
        IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }
        public int InsertUser(RegisterViewModel registerVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<RegisterViewModel, User>(registerVM);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(registerVM.Password);
            userRepository.InsertUser(user);
            int userId = userRepository.GetLatestUserID();
            return userId;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel editUserDetailsVM)
        {
        var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
        IMapper mapper = config.CreateMapper();
        User user = mapper.Map<EditUserDetailsViewModel, User>(editUserDetailsVM);
        userRepository.UpdateUserDetails(user);
        }
        public void UpdateUserPassword(EditUserPasswordViewModel editUserPasswordVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserPasswordViewModel, User>(editUserPasswordVM);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(editUserPasswordVM.Password);
            userRepository.UpdateUserPassword(user);
        }
        public void DeleteUser(int userId)
        {
            userRepository.DeleteUser(userId);
        }
        public List<UserViewModel> GetUsers()
        {
            List<User> userList = userRepository.GetUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> userVMList = mapper.Map<List<User>, List<UserViewModel>>(userList);
            return userVMList;
        }
        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            User user = userRepository.GetUsersbyEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel userVM = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                userVM = mapper.Map<User, UserViewModel>(user);
            }
            return userVM;
        }
        public UserViewModel GetUsersByEmail(string Email)
        {
            User user = userRepository.GetUsersbyEmail(Email).FirstOrDefault();
            UserViewModel userVM = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                userVM = mapper.Map<User, UserViewModel>(user);
            }
            return userVM;
        }
        public UserViewModel GetUsersByUserID(int UserID)
        {
            User user = userRepository.GetUsersbyUserID(UserID).FirstOrDefault();
            UserViewModel userVM = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                userVM = mapper.Map<User, UserViewModel>(user);
            }
            return userVM;
        }
    }
}
