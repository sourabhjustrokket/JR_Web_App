using JR_Web_App.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JR_Web_App.CustomMiddleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.Count > 0)
            {
                try
                {
                    string token = context.Request.Cookies["token"].ToString();
                    if (token.Length > 0)
                    {
                        if (string.IsNullOrEmpty(context.Session.GetString("user_type")))
                        {
                            HttpClient client = new HttpClient();
                            string callingUrl = APIHelper.BaseUrl + "/Users/GetUserInfo";
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                            try
                            {
                                var response = client.GetAsync(callingUrl).Result;
                                var data = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result) as Newtonsoft.Json.Linq.JObject;
                                context.Session.SetString("user_type", data.GetValue("userType").ToString());
                                context.Session.SetString("username", data.GetValue("username").ToString());
                                context.Session.SetString("email", data.GetValue("email").ToString());
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            await _next.Invoke(context);

            // Clean up.
        }
    }
    public static class AuthnticationMiddleWareExtensions
    {
        public static IApplicationBuilder UseAuthenticationFromApi(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
