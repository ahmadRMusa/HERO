[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HERO.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HERO.App_Start.NinjectWebCommon), "Stop")]

namespace HERO.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using HERO.Scheduler;
    using Ninject;
    using Ninject.Web.Common;
    using Models;
    using Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // DB Contexts
            kernel.Bind<GymContext>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();

            // Servies
            kernel.Bind<IEmailSender>().To<SmtpMessageSender>().InRequestScope();
            kernel.Bind<ICalendarGenerator>().To<CalendarGenerator>().InRequestScope();

            // Identity
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().InRequestScope().WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf().InRequestScope().WithConstructorArgument("store", kernel.Get<IUserStore<ApplicationUser>>());
        }
    }
}
