using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.CustomFilters;
using StackOverFlow.ServiceLayer;
using StackOverFlow.ViewModels;

namespace StackOverFlow.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService qs;
        ICategoryService cat;
        IAnswerService ans;
        
        public QuestionsController(IQuestionService qs,ICategoryService cat, IAnswerService ans)
        {
             this.qs=qs; this.cat=cat;this.ans=ans;
        }

        public ActionResult View(int id)
        {
            this.qs.UpdateQViewCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
           QuestionModel ques= this.qs.GetQuestionsById(id, uid).FirstOrDefault();
            return View(ques);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult AddAnswer(NewAnswerModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                this.ans.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionModel qvm = this.qs.GetQuestionsById(navm.QuestionID, navm.UserID).FirstOrDefault();
                return View("View", qvm);
            }
        }

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerModel editAnswer)
        {
            if(ModelState.IsValid)
            {
                editAnswer.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                ans.UpdateAnswer(editAnswer);
                return RedirectToAction("View", new { id=editAnswer.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return RedirectToAction("View", new { id = editAnswer.QuestionID });
            }
        }

        public ActionResult Create()
        {
            List<CategoriesModel> categories = this.cat.GetCategory();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult Create(NewQuestionModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount = 0;
                qvm.ViewsCount = 0;
                qvm.VotesCount = 0;
                qvm.QuestionDateAndTime = DateTime.Now;
                qvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.qs.InsertQuestion(qvm);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
    }
}
