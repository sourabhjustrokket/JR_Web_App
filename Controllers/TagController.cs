using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using JR_Web_App.Helper;

namespace JR_Web_App.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        [Route("Admin/Tag")]
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("user_type") != "A")
            {
                return RedirectToAction("index", "login");
            }
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/Tags";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["token"].ToString());
            try
            {
                var response = client.GetAsync(callingUrl).Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Tag>>(response.Content.ReadAsStringAsync().Result);
                return View(data);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [Route("Admin/Tag/{id}")]
        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("user_type") != "A")
            {
                return RedirectToAction("index", "login");
            }
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/Tags/"+id;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["token"].ToString());
            try
            {
                var response = client.GetAsync(callingUrl).Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Tag>(response.Content.ReadAsStringAsync().Result);
                return View(data);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [Route("Admin/Tag/Create")]
        // GET: Tag/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("user_type") != "A")
            {
                return RedirectToAction("index", "login");
            }
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [Route("Admin/Tag/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tag/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}