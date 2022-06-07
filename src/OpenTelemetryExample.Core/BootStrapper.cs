using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OpenTelemetryExample.Core.Clients;
using OpenTelemetryExample.Core.IoC;
using OpenTelemetryExample.Core.Kafka;
using OpenTelemetryExample.Core.Repositories.Product;
using OpenTelemetryExample.Core.Services;
using OpenTelemetryExample.Core.Tracing;

namespace OpenTelemetryExample.Core
{
    public static class BootStrapper
    {
        public static IWindsorContainer InitializeContainer()
        {
            var container = ContainerManager.WindsorContainer;

            #region NO INTERCEPTOR
            container.Register(Component.For<TraceInterceptor>().LifeStyle.Transient);

            container.Register(Component.For<IKafkaProducerService>()
                .ImplementedBy<KafkaProducerService>()
                .LifestyleSingleton()
                .Interceptors<TraceInterceptor>());

            container.Register(Component.For<IProductService>()
                .ImplementedBy<ProductService>()
                .LifestyleSingleton()
                .Interceptors<TraceInterceptor>());

            container.Register(Component.For<IProductRepository>()
                .ImplementedBy<ProductRepository>()
                .LifestyleTransient());

            container.Register(Component.For<IApi1Client>()
                .ImplementedBy<Api1Client>()
                .LifestyleSingleton());
            #endregion

            #region NO INTERCEPTOR
            //container.Register(Component.For<TraceInterceptor>().LifeStyle.Transient);

            //container.Register(Component.For<IKafkaProducerService>()
            //    .ImplementedBy<KafkaProducerService>()
            //    .LifestyleSingleton());

            //container.Register(Component.For<IProductService>()
            //    .ImplementedBy<ProductService>()
            //    .LifestyleSingleton());

            //container.Register(Component.For<IProductRepository>()
            //    .ImplementedBy<ProductRepository>()
            //    .LifestyleTransient());

            //container.Register(Component.For<IApi1Client>()
            //    .ImplementedBy<Api1Client>()
            //    .LifestyleSingleton());
            #endregion

            return container;
        }
    }
}
