

using Microsoft.AspNetCore.Mvc;
using Regstration.Models;
using System.Diagnostics;

namespace newProjecct.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult About()
        {
            return View("About");
        }
        public IActionResult Services()
        {
            return View("Services");
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult SearchBuses(string to, string from, string date)
        //{
        //    var buses = rep.GetAll()
        //                   .Where(b => b.To.Equals(to, StringComparison.OrdinalIgnoreCase) &&
        //                               b.From.Equals(from, StringComparison.OrdinalIgnoreCase))
        //                   .ToList();

        //    return PartialView("_SearchResults", buses);
        //}

       


        [HttpPost]
        public IActionResult Contact(string name, string email, string comments)
        {
            // Handle form submission logic here (e.g., save to database, send email, etc.)

            // Set TempData["response"] to true to indicate success
            TempData["response"] = true;

            // Redirect to the same page to display the success message
            return RedirectToAction("Contact");
        }
        //public IActionResult Contact()
        //{
        //    return View("Contact");
        //}
    }
}

