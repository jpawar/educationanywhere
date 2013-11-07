using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using EducationAnywhere.BusinessLayer;
using EducationAnywhere.CommonTypes.Interface;
using Models;

using Newtonsoft.Json;

namespace EducationAnywhere.Controllers
{

    public class RegistrationController : Controller
    {
        private readonly IUserRegistrationFacade _userRegistrationFacade;

        public RegistrationController()
        {
            _userRegistrationFacade = new UserRegistrationFacade();
        }

        public RegistrationController(IUserRegistrationFacade userRegistrationFacade)
        {
            _userRegistrationFacade = userRegistrationFacade;
        }

        //
        // GET: /Registration/

        public ActionResult Register()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public string SignIn([FromBody] User userData)
        {
            var loggedUser = _userRegistrationFacade.SignIn(userData);
            Session["UserData"] = loggedUser;
            var json = JsonConvert.SerializeObject(loggedUser);

            return json;
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Register([FromBody] User userData)
        {
            var registeredUser = _userRegistrationFacade.RegisterUser(userData);
            Session["UserData"] = registeredUser;
            return View();
        }

        [System.Web.Mvc.HttpDelete]
        public ActionResult SignIn()
        {            
            Session.Remove("UserData");
            return View();
        } 

    }
}
