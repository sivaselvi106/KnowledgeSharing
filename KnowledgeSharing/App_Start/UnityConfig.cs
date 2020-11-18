using System.Web.Http;
using System.Web.Mvc;
using Unity;
using KnowledgeSharing.ServiceLayer;
namespace KnowledgeSharing
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IQuestionsService, QuestionsService>();
            container.RegisterType<IUsersService, UserService>();
            container.RegisterType<ICategoriesService, CategoriesService>();
            container.RegisterType<IAnswersService, AnswersService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}