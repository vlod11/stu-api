using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace UniHub.WebApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AllowFilesGettingFromServer(this IApplicationBuilder app,
                                            IHostingEnvironment env, string path)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    ServeUnknownFileTypes = true,
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                    },
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), path)),
                    RequestPath = new PathString("/" + path)
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    ServeUnknownFileTypes = true,
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                    },
                    FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), path)),
                    RequestPath = new PathString("/" + path)
                });
            }
        }
    }
}