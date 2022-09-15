using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.ServiceLayer;


namespace StackOverFlow.APIControllers
{
    public class QuestionController : Controller
    {
        IAnswerService ans;
        IQuestionService qs;

        public QuestionController(IAnswerService ans, IQuestionService qs)
        {
            this.ans = ans; this.qs = qs;
        }
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }
        public void Post(int AnswerID, int UserID, int value)
        {
            this.ans.UpdateAVoteCount(AnswerID, UserID, value);
        }
    }
}