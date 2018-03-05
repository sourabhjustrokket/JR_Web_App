using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using JR_Web_App.Models;
using JR_Web_App.Helper;
using System.Net;
using System.Web;

namespace JR_Web_App.Controllers
{
    public class LoginController : Controller
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
            string callingUrl = APIHelper.BaseUrl + "/Users/authenticate";

            HttpResponseMessage response = await client.PostAsJsonAsync(callingUrl, user);
            if (response.IsSuccessStatusCode)
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result) as Newtonsoft.Json.Linq.JObject;
                Response.Cookies.Append("token", data.GetValue("token").ToString(),new Microsoft.AspNetCore.Http.CookieOptions() {Expires=DateTime.Now.AddDays(7) });
                ViewBag.Message = "Login Successfully";
                return View();
            }
            else
            {
                ViewBag.Message = "Invalid Credentials";
                return View();
            }
        }
    }
}