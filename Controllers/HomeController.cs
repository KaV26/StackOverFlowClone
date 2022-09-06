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

        public HomeController(IQuestionService qs)
        {
            this.qs = qs;
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
            //ViewBag.Message = "Your contact page.";

            return View();
        }
        

    }
}