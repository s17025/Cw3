using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cw3.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            next = _next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            if(context.Request != null)
            {
                string path = context.Request.Path; // api/students
                string method = context.Request.Method; //get post ...
                string query = context.Request.QueryString.ToString();
                string bodyStr = string.Empty;

                using(var reader = new StreamReader(context.Request.Body, Encoding.UTF8, false, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                using(System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\Szkoła\APBD\Zajęcia 6\requestsLog.txt", true))
                {
                    file.WriteLine(path);
                    file.WriteLine(method);
                    file.WriteLine(query);
                    file.WriteLine(bodyStr);
                }
                //zapisac do pliku
                //zapisac do db
            }

            if (_next == null) await _next(context);
        }
    }
}
