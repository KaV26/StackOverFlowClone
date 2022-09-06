using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.ServiceLayer;
using StackOverFlow.ViewModels;

namespace StackOverFlow.Controllers
{
    public class AccountController : Controller
    {
        IUserService us; //whenever a repo/service(dependency) is injected, we need to register it in the unity.mvc file
        public AccountController(UserService us)
        {
            this.us = us;
        }
        public ActionResult Register()
        {
            return View();
        }

     
        public ActionResult Login(int id)
        {
            return View();
        }

        


    }
}
