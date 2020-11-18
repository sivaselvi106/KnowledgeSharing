using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowledgeSharing.CustomFilters;
using KnowledgeSharing.ServiceLayer;
using KnowledgeSharing.ViewModels;

namespace KnowledgeSharing.Controllers
{
  //  [CustomExceptionFilter]
    public class HomeController : Controller
    {
        IQuestionsService questionService;
        ICategoriesService categoriesService;

        public HomeController(IQuestionsService questionService,ICategoriesService categoriesService)
        {
            this.questionService = questionService;
            this.categoriesService = categoriesService;
        }
            
        public ActionResult Index()
        {
            List<QuestionViewModel> questions = questionService.GetQuestions().Take(10).ToList();
            return View(questions);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<CategoryViewModel> categoriesList = categoriesService.GetCategories();
            return View(categoriesList);
        }
        //public JsonResult PageRedirect()
        //{
        //    //int userId = Convert.ToInt32(Session["CurrentUserID"]);
        //    return Json(new { Redirect = "/Account/Login" }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Questions()
        {
            List<QuestionViewModel> questions = this.questionService.GetQuestions();
            return View(questions);
        }

       public ActionResult Search(string str)
        {
            List<QuestionViewModel> questions = this.questionService.GetQuestions().Where(temp => temp.QuestionName.ToLower().Contains(str.ToLower()) ||temp.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
            ViewBag.str = str;
            return View(questions);
        }
    }
}