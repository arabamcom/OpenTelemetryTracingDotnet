using Castle.Windsor;

namespace OpenTelemetryExample.Core.IoC
{
    public static class ContainerManager
    {
        public static IWindsorContainer WindsorContainer { get; }

        static ContainerManager()
        {
            WindsorContainer = new WindsorContainer();
        }

        public static bool IsRegistered<TService>() => WindsorContainer.Kernel.HasComponent(typeof(TService));
        public static TService Resolve<TService>() => WindsorContainer.Resolve<TService>();
    }
}
