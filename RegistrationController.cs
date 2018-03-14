using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class RegistrationController : Controller
    {
        BakeryEntities db = new BakeryEntities();

        // GET: Registration
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Register([Bind(Include ="LastName, FirstName, Email, " +
                                                    "Password, " +
                                                    "Phone")] NewPerson p)
        {
            int result = db.uRegister(p.LastName, p.FirstName, p.Email,
                                         p.Phone);

            if(result != -1)
            {
                return RedirectToAction("Success");
            }

            return RedirectToAction("Failure");
        }

        public ActionResult Success()
        {
            Message successMsg =  new Message();
            successMsg.MessageText = "Thanks for registering.";
            return View("Result", successMsg);
        }

        public ActionResult Failure()
        {
            Message failureMsg = new Message();
            failureMsg.MessageText = "Sorry, but something seems to have gone wrong with the registration.";
            return View("Result", failureMsg);
        }
    }
}
