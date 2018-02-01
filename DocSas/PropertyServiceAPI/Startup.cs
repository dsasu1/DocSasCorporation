using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.IO;
using PropertyService.Domain.Managers;
using PropertyService.Domain.DataBaseContext;
using AutoMapper;
using DSCAppEssentials.Abstract;
using PropertyService.Domain.Providers;
using PropertyService.Domain.ModelView;
using PropertyService.Domain.Managers.securitymiddleware;
using PropertyService.Domain.Managers.loggingmiddleware;
using DSCAppEssentials.StorageProviders;


namespace PropertyServiceAPI
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            services.AddDbContext<PSDbContext>(x => x.UseSqlServer(SettingManager.GetPropertyServiceConnection(Configuration, "DefaultConnection")));
            services.AddScoped(typeof(IPSRepository<>), typeof(PSRepository<>));
            services.AddScoped(typeof(ISettingProvider), typeof(SettingsProvider));
            services.AddScoped(typeof(IEmailProvider), typeof(EmailProvider));
            services.AddScoped(typeof(IUserProvider), typeof(UserProvider));
            services.AddScoped(typeof(IMiscProvider), typeof(MiscProvider));
            services.AddScoped(typeof(IPropertyProvider), typeof(PropertyProvider));
            services.AddScoped(typeof(ICodeGeneratorProvider), typeof(CodeGeneratorProvider));
            services.AddScoped(typeof(IAppCommon), typeof(AppCommon));
            services.AddScoped(typeof(ICommentCardProvider), typeof(CommentCardProvider));
            services.AddScoped(typeof(IServiceRequestProvider), typeof(ServiceRequestProvider));
            services.AddScoped(typeof(INewsPostProvider), typeof(NewsPostProvider));
            services.AddScoped(typeof(IEventProvider), typeof(EventProvider));
            services.AddScoped(typeof(IReviewProvider), typeof(ReviewProvider));
            services.AddScoped(typeof(IStaffProvider), typeof(StaffProvider));
            services.AddScoped(typeof(IRoleProvider), typeof(RoleProvider));
            services.AddScoped(typeof(IMediaProvider), typeof(MediaProvider));
            services.AddScoped(typeof(IResidentProvider), typeof(ResidentProvider));
            services.AddScoped(typeof(IResidentProvider), typeof(ResidentProvider));
            services.AddScoped(typeof(IAzureStorageProvider), typeof(AzureStorageProvider));
            services.AddScoped(typeof(INotificationProvider), typeof(NotificationProvider));
            services.AddCors(options => {
                options.AddPolicy("AllowAll", build => 
                {
                    build.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors("AllowAll");
            app.UseApiKeyValidator();
            app.UseErrorLogging();
            app.UseMvc();
            

        }
    }
}
