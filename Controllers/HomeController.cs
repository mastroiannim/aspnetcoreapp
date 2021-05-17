using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace aspnetcoreapp
{
    public class HomeController : Controller
    {
        // GET: /Home
        /*public string Index()
        {
            return "This is my default action...";
        }*/

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Welcome
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        // GET: /Home/Hello/ 
        // Requires using System.Text.Encodings.Web;
        public string Hello(string name = "Guest", int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");
        }
    }
}
