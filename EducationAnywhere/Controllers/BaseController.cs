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
                var rd = Redirect("~/Error/Index.cshtml");
                //throw new HttpRequestException("You are not logged in");                
            }

            return user;
        }

        protected bool IsAuthorized()
        {
            bool isOk = true;

            var user = Session["UserData"] as User;

            if (user != null)
            {
                var role = (Enums.Role)Enum.Parse(typeof(Enums.Role), user.Role);

                if ( role != Enums.Role.Teacher)
                {                    
                    isOk = false;                    
                }
            }

            return isOk;
        }
    }
}
