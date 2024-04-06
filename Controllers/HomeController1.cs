using Microsoft.AspNetCore.Mvc;
using Sportsclub.Models;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace Sportsclub.Controllers
{
    public class HomeController1 : Controller
    {
        private readonly ContactModel contObj;
        private readonly Signupmodel signupObj;
        private readonly MemberModel memberObj;

        public HomeController1()
        {
            contObj = new ContactModel();
            signupObj = new Signupmodel();
            memberObj = new MemberModel();
        }

        public IActionResult Index()
        {
            List<ContactModel> contactData = contObj.GetData();
            List<Signupmodel> signupData = signupObj.GetData();
            ViewData["Contacts"] = contactData;
            ViewData["Signups"] = signupData;

            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Signin(Signupmodel signup)
        {
            if (ModelState.IsValid)
            {
                bool result = signupObj.Insert(signup);
                if (result)
                {
                    return RedirectToAction("Home");
                }
                else
                {
                    TempData["msg"] = "Not Added. Something went wrong";
                }
            }
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactModel cont)
        {
            if (ModelState.IsValid)
            {
                bool result = contObj.Insert(cont);
                if (result)
                {
                    TempData["msg"] = "Added Successfully";
                }
                else
                {
                    TempData["msg"] = "Not Added. Something went wrong";
                }
            }
            
            return View();
        }

        public IActionResult Memberform()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Memberform(MemberModel cont1)
        {
            if (ModelState.IsValid)
            {
                bool result = memberObj.insert(cont1);
                if (result)
                {
                    return RedirectToAction("Activity");
                }
                else
                {
                    TempData["msg"] = "Not Added. Something went wrong";
                }
            }

            return View();
        }


        public IActionResult Activity()
        {
            return View();
        }


        public IActionResult Cricket()
        {
            return View();
        }

        public IActionResult Cricket1()
        {
            return View();
        }

        public IActionResult Football()
        {
            return View();
        }

        public IActionResult Football1()
        {
            return View();
        }

        public IActionResult Tabletennis()
        {
            return View();
        }

        public IActionResult Tabletennis1()
        {
            return View();
        }
        public IActionResult Vollyball()
        {
            return View();
        }

        public IActionResult Vollyball1()
        {
            return View();
        }
       

      

       


    }
}
