using System.Web.Security;
using System.Web.Mvc;
using DbToXML.ViewModel;
using Data.Repository;
using System.Linq;

namespace DbToXML.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _repository;

        public AccountController(IAccount repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public ActionResult Login() => View("Login");
        
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) {
                var user = _repository.CheckUser(model.Name, model.Password); // поиск пользователя в бд
                
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Cars");
                }
                else
                {
                    ModelState.AddModelError("", Resources.Resource.ErrorLogin);
                }
            }
            
            return View("Login", model);
        }

        [HttpGet]
        public ActionResult Register() => View("Register", CreateView());

        [HttpPost]
        public ActionResult Register(AccountViewModel model)
        {
            if(ModelState.IsValid)
            {
                var newUser = _repository.GetUser(model.Name);
                   
                if (newUser == null)
                {
                    if (newUser != _repository.Add(model.Name, model.Password, model.RoleId)) // если пользователь удачно добавлен в бд
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, false);
                        return RedirectToAction("Index", "Cars");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Resources.Resource.ErrorRegistration);
                }
            }
            return View("Register", CreateView());
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Cars");
        }

        private AccountViewModel CreateView()
        {
            var select = _repository.GetRoles().Select(a => new SelectListItem { Text = a.Name.ToString(), Value = a.Id.ToString() }).ToList();
            return new AccountViewModel { Roles = select };
        }
    }
}