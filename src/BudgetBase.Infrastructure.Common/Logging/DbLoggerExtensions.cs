using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BudgetBase.Infrastructure.Common.Logging
{
    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder)
        {
            return builder.AddDbLogger(options =>
            {
                builder.Services.BuildServiceProvider()
                    .GetRequiredService<IConfiguration>()
                    .GetSection("Logging")
                    .GetSection("Database")
                    .GetSection("Options")
                    .Bind(options);
            });
        }

        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
