using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDomain
{
    public class DynamicProxy<TDecorated> : DispatchProxy
    {
        private TDecorated _decorated;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null) throw new ArgumentNullException(nameof(targetMethod));

            var result = targetMethod.Invoke(_decorated, args);

            return result;
        }

        public static TDecorated Create(TDecorated decorated)
        {
            var proxy = Create<TDecorated, DynamicProxy<TDecorated>>();
            (proxy as DynamicProxy<TDecorated>).SetParameters(decorated);

            return (TDecorated)proxy;
        }

        private void SetParameters(TDecorated decorated)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }
    }

    public interface ICalculator
    {
        int Add(int a, int b);
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
