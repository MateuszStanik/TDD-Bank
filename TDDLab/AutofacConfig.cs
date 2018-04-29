using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace TDDLab
{
    public class AutofacConfig
    {
        public static void ConfigureBuilder()
        {
            var builder = new ContainerBuilder();

            //builder.reRegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
       
            builder.RegisterType<EFDbContext>().AsImplementedInterfaces();

     

            var container = builder.Build();
            //var config = GlobalConfiguration.Configuration;
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
