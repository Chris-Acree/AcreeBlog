using AcreeBlog.AuthHandlers;
using AcreeBlog.Controllers;
using AcreeBlog.Data.Command.EFCoreCommandHandlers;
using AcreeBlog.Data.Query.EFCoreQueryHandlers;
using AcreeBlog.Data.Models;
using AcreeBlog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpOverrides;
using TylerRhodes.Akismet;
using WebMarkupMin.AspNetCore2;
using WebMarkupMin.Core;
using WilderMinds.MetaWeblog;

using IWmmLogger = WebMarkupMin.Core.Loggers.ILogger;
using WmmNullLogger = WebMarkupMin.Core.Loggers.NullLogger;
using Microsoft.Net.Http.Headers;
using AutoMapper;
using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Common;
using AcreeBlog.ViewModels;
using System.Collections.Generic;
using PagedList.Core;

namespace AcreeBlog
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly Container _container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // This is for a mutli-tenant Environment, so the ConnectionString Env Var name can be set
            // In AppSettings.json
            var connectionString = _configuration["ConnectionStringConfigName"];

            if (connectionString == null)
            {
                connectionString = _configuration["ConnectionString"];
                if (connectionString == null)
                {
                    throw new Exception("Unable to determine the Connection String to the database.");
                }
            }

            // register both context and repository to the dependency injection
            //services.AddApplicationInsightsTelemetry(_configuration);
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper();

            services.Configure<BagomboSettings>(_configuration.GetSection("BagomboSettings"));

            services.AddSession();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<BlogDbContext>(options => { options.UseSqlServer(connectionString); });
            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connectionString); });

            services.AddIdentity<ApplicationUser, IdentityRole>(opts => { opts.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var authBuilder = services.AddAuthentication();


            // Google GoogleOAuth
            var googleClientId = _configuration["GoogleOAuth:ClientId"];
            var googleSecret = _configuration["GoogleOAuth:Secret"];

            services.AddAuthentication().AddGoogle(opts =>
            {
                opts.ClientId = googleClientId;
                opts.ClientSecret = googleSecret;
            });



            //AutoMapper Config
            //https://dotnetthoughts.net/using-automapper-in-aspnet-core-project/
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Type sourceType, Type destinationType> ();
                //cfg.CreateMap<Models.ViewModels.Admin.ManagePostsViewModel, ViewModels.Admin.ManagePostsViewModel>();
               
                cfg.CreateMap<Author, AuthorViewModel>();
                cfg.CreateMap<BlogPostCategory, BlogPostCategoryViewModel>();
                cfg.CreateMap<BlogPostTopic, BlogPostTopicViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Topic, TopicViewModel>();
                // https://www.softwarerockstar.com/complex-object-mapping-using-automapper/
                cfg.CreateMap<BlogPost, BlogPostViewModel>()
                    .ForMember(dest => dest.AuthorViewModel, opt => opt.MapFrom( src => src.Author))
                    .ForMember(dest => dest.BlogPostCategoryViewModel, opt => opt.MapFrom(src => src.BlogPostCategory))
                    .ForMember(dest => dest.BlogPostTopicViewModel, opt => opt.MapFrom(src => src.BlogPostTopic))
                    .ForMember(dest => dest.CommentsViewModel, opt => opt.MapFrom(src => src.Comments))
                    .DisableCtorValidation();
                    // Summary:
                    //     Disable constructor validation. During mapping this map is used 
                    //     against an existing destination object and never constructed itself.

                cfg.CreateMap<PagedList<BlogPost>, PagedList<BlogPostViewModel>>();
                cfg.CreateMap<IPagedList<BlogPost>, IPagedList<BlogPostViewModel>>();


            });
            mapperConfig.AssertConfigurationIsValid();
            var mapper = mapperConfig.CreateMapper();



            var twitterKey = _configuration[$"{_configuration["TwitterKeyConfigName"]}"];
            var twitterSecret = _configuration[$"{_configuration["TwitterSecretConfigName"]}"];

            if (twitterKey != null && twitterSecret != null)
            {
                authBuilder.AddTwitter(opts =>
            {
                opts.ConsumerKey = twitterKey;
                opts.ConsumerSecret = twitterSecret;
            });
            }

            var facebookAppId = _configuration[$"{_configuration["FacebookAppIdConfigName"]}"];
            var facebookAppSecret = _configuration[$"{_configuration["FacebookAppSecretConfigName"]}"];

            if (facebookAppId != null && facebookAppSecret != null)
            {
                authBuilder.AddFacebook(opts =>
                {
                    opts.AppId = facebookAppId;
                    opts.AppSecret = facebookAppSecret;
                });
            }
            /*
            services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie.Expiration = TimeSpan.FromDays(14);
                opts.ExpireTimeSpan = TimeSpan.FromDays(14);
                opts.Cookie.Name = "SecurityLogin";
            });
            */

            // Add framework services.
            // http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
            services.AddApplicationInsightsTelemetry(_configuration);
            services.AddMvc(
                config => {
                    config.Filters.Add(typeof(CustomExceptionFilter));
                }
            );



            services.AddAntiforgery(opts => { opts.Cookie.Name = "SecurityAntiForgery"; });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("EditPolicy", policy => { policy.Requirements.Add(new SameAuthorRequirement()); });
                opts.AddPolicy("EditAuthorProfile", policy => { policy.Requirements.Add(new AuthorIsUserRequirement()); });
            });

            services.AddScoped<IAuthorizationHandler>(p => new SimpleInjectorAuthorizationHandler(_container));
      

            // HTML minification (https://github.com/Taritsyn/WebMarkupMin)
            services
                .AddWebMarkupMin(options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.DisablePoweredByHttpHeaders = true;
                })
                .AddHtmlMinification(options =>
                {
                    options.MinificationSettings.RemoveOptionalEndTags = false;
                    options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
            });

            services.AddSingleton<IWmmLogger, WmmNullLogger>(); // Used by HTML minifier

            // Bundling, minification and Sass transpilation (https://github.com/ligershark/WebOptimizer)
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.MinifyJsFiles();
                pipeline.CompileScssFiles()
                    .InlineImages(1);
            });

            IntegrateSimpleInjector(services);
        }

        public void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.RegisterCollection<IAuthorizationHandler>(new[]
                {typeof(EditBlogPostAuthorizationHandler), typeof(EditAuthorProfileAuthorizationHandler)});

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
            services.AddSimpleInjectorTagHelperActivation(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeContainer(app);
            _container.Verify();
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddFile(_configuration.GetSection("Logging"));
            app.UseStaticFiles();

            var settings = _container.GetInstance<IOptions<BagomboSettings>>();

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), settings.Value.PostImagesRelativePath)))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), settings.Value.PostImagesRelativePath));
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
            FileProvider =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                settings.Value.PostImagesRelativePath)),
                RequestPath = new PathString($"/{settings.Value.PostImagesRelativePath}")
            });

            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }

            app.UseWebOptimizer();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    var time = TimeSpan.FromDays(365);
                    context.Context.Response.Headers[HeaderNames.CacheControl] = $"max-age={time.TotalSeconds.ToString()}";
                    context.Context.Response.Headers[HeaderNames.Expires] = DateTime.UtcNow.Add(time).ToString("R");
                }
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Global Exception Handling
            // http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
            /*
            app.UseExceptionHandler(
             options => {
                 options.Run(
                 async context =>
                 {
                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                     context.Response.ContentType = "text/html";
                     var ex = context.Features.Get<IExceptionHandlerFeature>();
                     if (ex != null)
                     {
                         var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                         await context.Response.WriteAsync(err).ConfigureAwait(false);
                     }
                 });
             }
            );
            */


            // need this for metaweblog to work with SimpleInjector : (
            app.Use(async (context, next) =>
            {
                if (context.Request.Method == "POST" &&
                    context.Request.Path.StartsWithSegments("/metaweblog") &&
                    context.Request != null &&
                    context.Request.ContentType.ToLower().Contains("text/xml"))
                {
                    context.Response.ContentType = "text/xml";
                    var rdr = new StreamReader(context.Request.Body);
                    var xml = rdr.ReadToEnd();
                    //_logger.LogInformation($"Request XMLRPC: {xml}");
                    var mwlp = _container.GetInstance<MetaWeblogService>();
                    var result = mwlp.Invoke(xml);
                    //_logger.LogInformation($"Result XMLRPC: {result}");
                    await context.Response.WriteAsync(result, Encoding.UTF8);
                    return;
                }

                // Continue On
                await next.Invoke();
            });

            app.UseAuthentication();
            app.UseWebMarkupMin();
            app.UseMvcWithDefaultRoute();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            _container.Register<IImageService, FileSystemImageService>();
            _container.Register<MetaWeblogService>();
            _container.Register<IMetaWeblogProvider, MetaWebLogProvider>();
            // Cross-wire ASP.NET services (if any). For instance:
            _container.RegisterSingleton(app.ApplicationServices.GetService<ILoggerFactory>());

            _container.RegisterSingleton(new HttpClient());

            //todo: Null checking, what if Akismet isn't being used?
            _container.Register(() =>
            {
            var context = _container.GetInstance<IHttpContextAccessor>();
                var url = context?.HttpContext?.Request?.Scheme + "://" + context?.HttpContext?.Request?.Host;

                var keyConfName = _configuration["AkismetApiKeyConfigName"];
                var key = _configuration[keyConfName];

                return new AkismetClient(url, key ?? "nogo", _container.GetInstance<HttpClient>());
            });

            _container.CrossWire<IHttpContextAccessor>(app);
            _container.CrossWire<BlogDbContext>(app);
            _container.CrossWire<ApplicationDbContext>(app);
            _container.CrossWire<UserManager<ApplicationUser>>(app);
            _container.CrossWire<RoleManager<IdentityRole>>(app);
            _container.CrossWire<SignInManager<ApplicationUser>>(app);
            _container.CrossWire<IPasswordHasher<ApplicationUser>>(app);
            _container.CrossWire<IPasswordValidator<ApplicationUser>>(app);
            _container.CrossWire<IUserValidator<ApplicationUser>>(app);
            _container.CrossWire<IOptions<BagomboSettings>>(app);

            _container.CrossWire<ILogger<HomeController>>(app);
            _container.CrossWire<ILogger<AccountController>>(app);
            _container.CrossWire<ILogger<AdminController>>(app);
            _container.CrossWire<ILogger<AuthorController>>(app);
            _container.CrossWire<ILogger<MetaController>>(app);
            _container.CrossWire<ILogger<MetaWeblogService>>(app);
            _container.CrossWire<ILogger<MetaWebLogProvider>>(app);

            _container.CrossWire<IMapper>(app);

            _container.CrossWire<IAuthorizationHandler>(app);
            _container.CrossWire<IAuthorizationService>(app);

            _container.AddEFQueries();
            _container.AddEFCommands();

            // NOTE: Do prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
    }

    public sealed class SimpleInjectorAuthorizationHandler : IAuthorizationHandler
    {
        private readonly Container _container;
        public SimpleInjectorAuthorizationHandler(Container container)
        {
            this._container = container;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var handler in this._container.GetAllInstances<IAuthorizationHandler>())
                await handler.HandleAsync(context);
        }
    }
}