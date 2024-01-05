using AutoMapper;
using Jabil.Pico.Web.App_Start;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Linq;
using Unity;

namespace Jabil.Pico.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var repository = new UnityContainer();
              RegisterTypes(repository);
              return repository;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.

            container.RegisterType<ApplicationContext>();
            container.RegisterInstance<IMapper>(AutoMapperConfig.InitAutoMapper());

            // Register service types
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.Equals("Jabil.Pico.Web.BLL"));
            foreach (var assembly in assemblies)
            {
                var services = assembly.GetTypes().Where(x => !x.IsInterface && x.Name.EndsWith("Service"));
                var iservices = assembly.GetTypes().Where(x => x.IsInterface && x.Name.EndsWith("Service"));
                foreach (var service in services)
                {
                    var iservice = iservices.FirstOrDefault(x => x.Name.Equals($"I{service.Name}"));
                    if (iservice != null)
                    {
                        container.RegisterType(iservice, service);
                    }
                    else
                    {
                        container.RegisterType(service);
                    }
                }
            }
        }
    }
}