using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.ViewModels;
using StackOverFlow.ServiceLayer;

namespace StackOverFlow.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService qs;
        ICategoryService cat;
       

        public HomeController(IQuestionService qs, ICategoryService cat)
        {
            this.qs = qs; this.cat = cat;
        }

        public ActionResult Index()
        
        {
            List<QuestionModel> questions = this.qs.GetQuestions().Take(10).ToList();
            return View(questions);
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            
            return View();
        }

        public ActionResult Categories()
        {
            List<CategoriesModel> categories = cat.GetCategory();
            return View(categories);
        }

        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionModel> questions = qs.GetQuestions();
            return View(questions);
        }

        public ActionResult Search(string str)
        {
            List<QuestionModel> ques = this.qs.GetQuestions().Where(x => x.QuestionName.ToLower().Contains(str.ToLower()) ||
            x.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();

            //List<QuestionModel> ques = this.qs.GetQuestions().Where(temp => temp.QuestionName.ToLower().Contains(str.ToLower()) || 
            //temp.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
            ViewBag.str = str;
            return View(ques);
        }
    }
}