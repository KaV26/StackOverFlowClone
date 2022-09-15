using StackOverFlow.ServiceLayer;
using StackOverFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace StackOverFlow.APIControllers
{
    public class AccountController : ApiController
    {
        IUserService us; 
        public AccountController(UserService us)
        {
            this.us = us;
        }
      
        public string Get(string email)
        {
            if (this.us.GetUserByEmail(email) != null)
            {
                return "Found";
            }
            else
                return "Not Found";
        }

       
        

       
    }
}