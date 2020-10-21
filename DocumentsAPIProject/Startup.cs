using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using DI;
using InfraDALContracts;
using MessangerContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLServerInfraDAL;

namespace DocumentsAPIProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)   
        {
            services.AddControllers()
                    .AddJsonOptions(options => {options.JsonSerializerOptions.PropertyNamingPolicy = null;  });
            services.AddTransient<ISocket, WebSocketAdapter>();
            services.AddSingleton<IMessanger, WebSocketMessangerAdapter>();
            services.AddTransient<IInfraDal, SQLDAL>();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dlls");
            var resolver = new Resolver(path, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var id = context.Request.Query["id"];
                        var messanger = app.ApplicationServices.GetService<IMessanger>();
                        var webSocketAdapter = new WebSocketAdapter();
                        webSocketAdapter.Socket = webSocket;
                        await messanger.Add(id, webSocketAdapter);
                        await messanger.Send(id,new MessageBody() {marker= "add succsfuly" });
                        await webSocket.ReceiveAsync(new Memory<byte>(), CancellationToken.None);

                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });





            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
