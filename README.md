# ProxySample

Some sample code to understand AOP in C# .NET.

## Log4Net

[LOGGING WITH LOG4NET IN .NET CORE 3.0 CONSOLE APP](https://jakubwajs.wordpress.com/2019/11/28/logging-with-log4net-in-net-core-3-0-console-app/)

Install Package

```sh
PM> Install-Package log4net
```

Add configuration file

```xml
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
	<root>
		<level value="ALL" />
		<appender-ref ref="console" />
		<appender-ref ref="file" />
	</root>
	<appender name="console" type="log4net.Appender.ConsoleAppender">
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%date %level %logger - %message%newline" />
		</layout>
	</appender>
	<appender name="file" type="log4net.Appender.RollingFileAppender">
		<file value="proxyconsole.log" />
		<appendToFile value="true" />
		<rollingStyle value="Size" />
		<maxSizeRollBackups value="5" />
		<maximumFileSize value="10MB" />
		<staticLogFileName value="true" />
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%date [%thread] %level %logger - %message%newline" />
		</layout>
	</appender>
</log4net>
```

Load configuration

```csharp
using System;
using System.IO;
using log4net;
using log4net.Config;
using ProxyDomain;

namespace ProxyConsole
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            
            _log.Info("Log4Net working");
  
            Console.ReadLine();
        }
    }
}
```

## xUnit

[Capturing Output](https://xunit.net/docs/capturing-output)

## .NET Core

[RealProxy in dotnet core?](https://stackoverflow.com/questions/38467753/realproxy-in-dotnet-core)

> [It looks like RealProxy won't come to .NET Core/Standard](https://github.com/dotnet/corefx/issues/4091). In the issue, a Microsoft developer suggests [DispatchProxy](https://github.com/dotnet/corefx/tree/master/src/System.Reflection.DispatchProxy) as an alternative.
> 
> An alternative is the DispatchProxy, which has a wonderful example here: http://www.c-sharpcorner.com/article/aspect-oriented-programming-in-c-sharp-using-dispatchproxy/.
> 
> If we simplify the code, this is what we get:

> ```csharp
> public class LoggingDecorator<T> : DispatchProxy
> {
>     private T _decorated;
> 
>     protected override object Invoke(MethodInfo targetMethod, object[] args)
>     {
>         try
>         {
>             LogBefore(targetMethod, args);
> 
>             var result = targetMethod.Invoke(_decorated, args);
> 
>             LogAfter(targetMethod, args, result);
>             return result;
>         }
>         catch (Exception ex) when (ex is TargetInvocationException)
>         {
>             LogException(ex.InnerException ?? ex, targetMethod);
>             throw ex.InnerException ?? ex;
>         }
>     }
> 
>     public static T Create(T decorated)
>     {
>         object proxy = Create<T, LoggingDecorator<T>>();
>         ((LoggingDecorator<T>)proxy).SetParameters(decorated);
> 
>         return (T)proxy;
>     }
> 
>     private void SetParameters(T decorated)
>     {
>         if (decorated == null)
>         {
>             throw new ArgumentNullException(nameof(decorated));
>         }
>         _decorated = decorated;
>     }
> 
>     private void LogException(Exception exception, MethodInfo methodInfo = null)
>     {
>         Console.WriteLine($"Class {_decorated.GetType().FullName}, Method > {methodInfo.Name} threw exception:\n{exception}");
>     }
> 
>     private void LogAfter(MethodInfo methodInfo, object[] args, object result)
>     {
>         Console.WriteLine($"Class {_decorated.GetType().FullName}, Method > {methodInfo.Name} executed, Output: {result}");
>     }
> 
>     private void LogBefore(MethodInfo methodInfo, object[] args)
>     {
>         Console.WriteLine($"Class {_decorated.GetType().FullName}, Method > {methodInfo.Name} is executing");
>     }
> }
> ```
> 
> So if we have an example class Calculator with a corresponding interface (not shown here):
> 
> ```csharp
> public class Calculator : ICalculator
> {
>     public int Add(int a, int b)
>     {
>         return a + b;
>     }
> }
> ```
> 
> we can simply use it like this
> 
> ```csharp
> static void Main(string[] args)
> {
>     var decoratedCalculator = LoggingDecorator<ICalculator>.Create(new  Calculator>());
>     decoratedCalculator.Add(3, 5);
>     Console.ReadKey();
> }
> ```



## References

- [Aspect-Oriented Programming : Aspect-Oriented Programming with the RealProxy Class](https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/february/aspect-oriented-programming-aspect-oriented-programming-with-the-realproxy-class)
- [Aspect Oriented Programming (AOP) in real life scenario.](https://dev.to/rafalpienkowski/aspect-oriented-programming-aop-in-real-life-scenario-3ha)
- [.NET implements AOP through Autofac and DynamicProxy](https://www.programmersought.com/article/51715520249/)
- [Using type interceptors with Autofac](https://maartenderaedemaeker.be/2017/07/23/using-type-interceptors-with-autofac/)
- [Using Autofac's Aspect Oriented Programming for instrumentation in a class factory situation.](https://github.com/harlannorth/AOPLogging)
- [AOP with Autofac and DynamicProxy2](https://nblumhardt.com/archives/aop-with-autofac-and-dynamicproxy2/)
- [AOP in .NET Core using Autofac and DynamicProxy](https://ardall.wordpress.com/2019/04/11/aop-in-net-core-using-autofac-and-dynamicproxy/)
- [Using DI with DispatchProxy based decorators in C# .NET Core](https://medium.com/@nik96a/using-di-with-dispatchproxy-based-decorators-in-c-net-core-ac02f02c5fe5)
- [Decorators in .NET Core with Dependency Injection](https://greatrexpectations.com/2018/10/25/decorators-in-net-core-with-dependency-injection)
- [Aspect Oriented Programming In C# With RealProxy class](https://www.c-sharpcorner.com/article/aspect-oriented-programming-in-c-sharp-with-realproxy/)
- [Using RealProxy for Logging](https://kaylaniam.wordpress.com/2015/01/10/using-realproxy-for-logging/)