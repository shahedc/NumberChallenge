using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NumberChallenge.Models;
using System;
using System.Text.RegularExpressions;

namespace NumberChallenge
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);
            services.AddBot<NumberBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

                var middleware = options.Middleware;
                // Add middleware to send an appropriate message to the user if an exception occurs
                middleware.Add(new CatchExceptionMiddleware<Exception>(async (context, exception) =>
                {
                    await context.SendActivity("Sorry, it looks like something went wrong!");
                }));

                // Add middleware to send periodic typing activities until the bot responds. The initial
                // delay before sending a typing activity and the frequency of additional activities can also be specified
                middleware.Add(new ShowTypingMiddleware());
                middleware.Add(new UserState<UserData>(new MemoryStorage()));
                middleware.Add(new ConversationState<ConversationData>(new MemoryStorage()));
                
                middleware.Add(new RegExpRecognizerMiddleware()
                                .AddIntent("showGames", new Regex("show game(?:s)*(.*)", RegexOptions.IgnoreCase))
                            );

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}