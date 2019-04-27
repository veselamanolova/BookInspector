
namespace BookInspector.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using BookInspector.Services.Contracts;
    using BookInspector.Services;
    using BookInspector.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using BookInspector.Web.Mappers.Contracts;
    using BookInspector.Web.Models;
    using BookInspector.Data.Models;
    using BookInspector.Web.Mappers;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<BookInspectorContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IBookViewModelMapper<Book, BookViewModel>, BookViewModelMapper>();

            services.AddScoped<IBookInspectorContext, BookInspectorContext>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IUserService, UserService>();

            // services.AddSingleton<IViewModelMapper<Book, BookViewModel>, ViewModelMapper>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            // app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
