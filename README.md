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

## References

- [Aspect-Oriented Programming : Aspect-Oriented Programming with the RealProxy Class](https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/february/aspect-oriented-programming-aspect-oriented-programming-with-the-realproxy-class)
- [Aspect Oriented Programming (AOP) in real life scenario.](https://dev.to/rafalpienkowski/aspect-oriented-programming-aop-in-real-life-scenario-3ha)
- [.NET implements AOP through Autofac and DynamicProxy](https://www.programmersought.com/article/51715520249/)
- [Using type interceptors with Autofac](https://maartenderaedemaeker.be/2017/07/23/using-type-interceptors-with-autofac/)
- [Using Autofac's Aspect Oriented Programming for instrumentation in a class factory situation.](https://github.com/harlannorth/AOPLogging)
- [AOP with Autofac and DynamicProxy2](https://nblumhardt.com/archives/aop-with-autofac-and-dynamicproxy2/)
- [AOP in .NET Core using Autofac and DynamicProxy](https://ardall.wordpress.com/2019/04/11/aop-in-net-core-using-autofac-and-dynamicproxy/)