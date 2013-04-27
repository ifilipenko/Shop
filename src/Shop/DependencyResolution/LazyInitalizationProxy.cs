using System;
using LinFu.DynamicProxy;

namespace Shop.DependencyResolution
{
    public class LazyInitalizationProxy<T> : IInterceptor
    {
        public static T Create(Func<T> targetFactory)
        {
            var proxyFactory = new ProxyFactory();
            return proxyFactory.CreateProxy<T>(new LazyInitalizationProxy<T>(targetFactory));
        }
       
        private readonly Lazy<T> _instanceHolder;

        private LazyInitalizationProxy(Func<T> factory)
        {
            _instanceHolder = new Lazy<T>(factory);
        }

        public object Intercept(InvocationInfo invocationInfo)
        {
            return invocationInfo.TargetMethod.Invoke(_instanceHolder.Value, invocationInfo.Arguments);
        }
    }
}