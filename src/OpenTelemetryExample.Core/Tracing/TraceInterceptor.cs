using Castle.DynamicProxy;
using OpenTelemetry.Trace;
using OpenTelemetryExample.Core.Helpers;
using System.Diagnostics;

namespace OpenTelemetryExample.Core.Tracing
{
    public class TraceInterceptor : IInterceptor
    {
        private readonly Tracer _tracer;

        public TraceInterceptor(Tracer tracer)
        {
            _tracer = tracer;
        }

        #region IInterceptor Members         
        public void Intercept(IInvocation invocation)
        {
            var classAndMethodName = $"{invocation.TargetType.Name}-{invocation.Method.Name}";
            using var activeSpan = _tracer.StartActiveSpan(classAndMethodName);
            invocation.Proceed();
        }
        #endregion
    }
}
