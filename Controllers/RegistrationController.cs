using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JR_Web_App.Models;
using System.Net.Http;
using JR_Web_App.Helper;

namespace JR_Web_App.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            user.Username = user.Email;
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/Users";

            HttpResponseMessage response = await client.PostAsJsonAsync(callingUrl, user);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index","login");
            }
            else
            {
                ViewBag.Message = "Registration Failed";
                return View();
            }
        }
    }
}