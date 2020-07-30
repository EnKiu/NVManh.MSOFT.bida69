using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSOFT.bida69.core.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}