using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowledgeSharing.ServiceLayer;
using KnowledgeSharing.ViewModels;
using KnowledgeSharing.CustomFilters;

namespace KnowledgeSharing.Controllers
{
   // [CustomExceptionFilter]
    
    public class QuestionsController : Controller
    {
        IQuestionsService questionsService;
        IAnswersService answersService;
        ICategoriesService categoriesService;

        public QuestionsController(IQuestionsService questionsService , IAnswersService answersService, ICategoriesService categoriesService)
        {
            this.questionsService  = questionsService ;
            this.answersService = answersService;
            this.categoriesService = categoriesService;
        }
        public ActionResult View(int id)
        {
            this.questionsService .UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.questionsService .GetQuestionByQuestionID(id, uid);
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                this.answersService.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionViewModel qvm = this.questionsService .GetQuestionByQuestionID(navm.QuestionID, navm.UserID);
                return View("View", qvm);
            }
        }

        [HttpPost]
        [UserAuthorizationFilterAttribute]
        public ActionResult EditAnswer(EditAnswerViewModel avm)
        {
            if (ModelState.IsValid)
            {
                avm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.answersService.UpdateAnswer(avm);
                return RedirectToAction("View", new { id = avm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return RedirectToAction("View", new { id = avm.QuestionID });
            }
        }
      
       //[UserAuthorizationFilterAttribute]
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CurrentUserID"])))
            {
                Response.Redirect("/Account/Login?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            List<CategoryViewModel> categories = this.categoriesService.GetCategories();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(NewQuestionViewModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount = 0;
                qvm.ViewsCount = 0;
                qvm.VotesCount = 0;
                qvm.QuestionDateAndTime = DateTime.Now;
                qvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.questionsService .InsertQuestion(qvm);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }

        [NonAction]
        public void UpdateCount(int AnswerID, int UserID, int value)
        {
            this.answersService.UpdateAnswerVotesCount(AnswerID, UserID, value);
        }
    }
}