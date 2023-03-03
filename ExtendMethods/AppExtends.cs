using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AppMvc.Net.ExtendMethods
{
    public static class AppExtends
    {
        public static void AddStatusCodePage(this IApplicationBuilder app) // cach mở rộng phương thức IApplicationBuilder ở startup
        {
            app.UseStatusCodePages(appError =>
            {
                appError.Run(async context =>
                {
                    var respone = context.Response;
                    var code = respone.StatusCode;

                    var content = $@"
                        <!DOCTYPE html>
                        <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>Document</title>
                            </head>
                            <body>
                                <p style='color: red; font-size: 30px; text-align: center'>
                                    Có lỗi xảy ra: {code} - {(HttpStatusCode)code}
                                </p>
                            </body>
                        </html>
                    ";

                    await respone.WriteAsync(content);
                });
            }); //code 400-599
        }
    }
}
