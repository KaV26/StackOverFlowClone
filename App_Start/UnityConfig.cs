using System.Web.Http;
using Unity;
using Unity.WebApi;
using StackOverFlow.ServiceLayer;
using System.Web.Mvc;
using Unity.Mvc5;

namespace StackOverFlow
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IAnswerService, AnswerService>();
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

        }
    }
}