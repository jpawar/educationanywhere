using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using Models;

namespace EducationAnywhere.Controllers
{
    public class BaseController : Controller
    {
        protected User IsUserInSession()
        {
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            return user;
        }
    }
}
