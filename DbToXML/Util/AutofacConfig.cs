using Autofac;
using Autofac.Integration.Mvc;
using Data;
using Data.Models;
using Data.Repository;
using System.Web.Mvc;

namespace DbToXML.Util
{
    public class AutofacConfig 
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<CarsRepository>().As<IRepository<Car>>().WithParameter("context", new Context());
            builder.RegisterType<YearsRepository>().As<IRepository<Year>>().WithParameter("context", new Context());
            builder.RegisterType<AccountRepository>().As<IAccount>().WithParameter("context", new Context());

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}