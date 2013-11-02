using System.Web.Http;
using System.Web.Mvc;

using EducationAnywhere.BusinessLayer;
using EducationAnywhere.CommonTypes.Interface;
using Models;

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
        public ActionResult SignIn([FromBody] User userData)
        {
            return View();

            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");            
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Register([FromBody] User userData)
        {
            _userRegistrationFacade.RegisterUser(userData);
            return View();

            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");            
        }

    }
}
