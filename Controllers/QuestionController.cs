using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using JR_Web_App.Helper;
using JR_Web_App.Models;

namespace JR_Web_App.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ask()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ask(Question q)
        {
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/Questions";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["token"].ToString());
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(callingUrl, q);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index", "tag");
                }
                else
                {
                    ViewBag.Message = "Invalid Credentials";
                    return View();
                }
            }
            catch (Exception)
            {

            }
            return View();
        }
    }
}