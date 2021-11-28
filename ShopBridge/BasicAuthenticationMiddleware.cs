using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ShopBridge.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IUserDetails userDetails;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository, IUserDetails userDetails)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            if(!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic"))
            {
                string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                string credentials = encoding.GetString(Convert.FromBase64String(encodedCredentials));
                int index = credentials.IndexOf(":");
                string username = credentials.Substring(0,index);
                string password = credentials.Substring(index+1);

                var userId = await userRepository.GetUserId(username, password);
                if (userId == 0)
                {
                   httpContext.Response.StatusCode = 401;
                   return;
                }
                userDetails.UserId = userId;
                userDetails.UserName = username;
                //httpContext.Session.SetString("UserId",userId.ToString());
            }
            else
            {
                httpContext.Response.StatusCode = 401;
                return;
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
