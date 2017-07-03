using System.Linq;
using Castle.Core.Internal;
using Castle.Windsor;

namespace CallCenter.Client.WinApp.Configuration.Windsor
{
    public static class WindsorExtensions
    {
        public static void BuildUp(this IWindsorContainer container, object instance)
        {
            instance.GetType().GetProperties()
                .Where(property => property.CanWrite && property.PropertyType.IsPublic)
                .Where(property => container.Kernel.HasComponent(property.PropertyType))
                .ForEach(property => property.SetValue(instance, container.Resolve(property.PropertyType), null));
        }
    }
}
