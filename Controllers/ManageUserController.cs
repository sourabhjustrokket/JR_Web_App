using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using JR_Web_App.Helper;

namespace JR_Web_App.Controllers
{
    public class ManageUserController : Controller
    {
        [Route("Admin/Users")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user_type") != "A")
            {
                return RedirectToAction("index", "login");
            }
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/Users";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["token"].ToString());
            try
            {
                var response = client.GetAsync(callingUrl).Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.User>>(response.Content.ReadAsStringAsync().Result);
                return View(data);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}