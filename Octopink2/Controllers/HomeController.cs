using Octopink2.Models;
using Octopink2.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Octopink2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var viewModel = new StudioViewModel
            {
                Artists = db.Artist.ToList(),
                Products = db.Product.ToList()
            };
            return View(viewModel);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string psw)
        {
            using (var context = new ModelDBContext())
            {
                var user = context.User.FirstOrDefault(u => u.Email == email && u.Psw == psw);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    if (user.Admin)
                    {
                        TempData["Login"] = "Benvenuto/a " + user.Nome + " " + user.Cognome + " [Admin]";
                        return RedirectToAction("Create", "Products");
                    }
                    else
                    {
                        TempData["Login"] = "Benvenuto/a " + user.Nome + " " + user.Cognome;
                        return RedirectToAction("Index", "Products");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email o password errati");
                    return View();
                }
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User utente)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ModelDBContext())
                {
                    context.User.Add(utente);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Products");
            }
            // Se il modello non è valido, torna alla vista di registrazione mostrando gli errori
            return View(utente);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            TempData["LogoutMess"] = "Logout effettuato con successo";
            return RedirectToAction("Index");
        }
    }
}
