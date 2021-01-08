using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Cresce.Core;
using Cresce.Core.Authentication;
using Cresce.Core.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace System.Runtime.CompilerServices
{
    public class IsExternalInit
    {
    }
}

namespace Cresce.Api
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
            services
                .AddControllers(options =>
                {
                    options.ModelBinderProviders.Insert(0, new AuthorizedUserBinderProvider());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddMvc(options =>
            {
                options.Filters.Add(new HttpExceptionFilter());
                options.Filters.Add(new UnauthorizedExceptionFilter());
            });

            GatewaysConfiguration.RegisterServices(services);
            ServicesConfiguration.RegisterServices(services);
            //GatewaysConfiguration.RegisterDbContext(services, settings.ConnectionString);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public class HttpExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (!(context.Exception is HttpRequestException)) return;

            context.Result = new UnauthorizedResult();
            context.ExceptionHandled = true;
        }
    }

    public class UnauthorizedExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            if (!(context.Exception is UnauthorizedException)) return;

            context.Result = new UnauthorizedResult();
            context.ExceptionHandled = true;
        }
    }
}
