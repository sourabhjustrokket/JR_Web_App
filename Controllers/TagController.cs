using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using JR_Web_App.Helper;
using JR_Web_App.Models;

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
            string callingUrl = APIHelper.BaseUrl + "/Tags/" + id;
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
        public async Task<IActionResult> Create(Tag tag)
        {
            try
            {
                // TODO: Add insert logic here
                HttpClient client = new HttpClient();
                string callingUrl = APIHelper.BaseUrl + "/Tags";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["token"].ToString());
                HttpResponseMessage response = await client.PostAsJsonAsync(callingUrl, tag);
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
        [Route("Admin/Tag/CreateTagRelations")]
        public ActionResult CreateTagRelationship()
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
                TagRelations tr = new TagRelations();
                tr.FamilyTagMembers = data;
                return View(tr);
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [Route("Admin/Tag/CreateTagRelations")]
        public ActionResult CreateTagRelationship(TagRelations tagRelations)
        {
            if (HttpContext.Session.GetString("user_type") != "A")
            {
                return RedirectToAction("index", "login");
            }
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/TagRelatoinship";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["token"].ToString());
            try
            {
                var response = client.PostAsJsonAsync(callingUrl,tagRelations).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json("done");
                }
            }
            catch (Exception ex)
            {

            }
            return Json("error");
        }
        [HttpGet]
        [Route("Tag/GetSecondaryTags")]
        public ActionResult GetSecondaryTags(string search="")
        {
            HttpClient client = new HttpClient();
            string callingUrl = APIHelper.BaseUrl + "/Tags/SearchSecondaryTag?searchTag="+search;
            var response = client.GetAsync(callingUrl).Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Tag>>(response.Content.ReadAsStringAsync().Result);
            return Ok(data);
        }
    }
}