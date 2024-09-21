using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BudgetBase.Infrastructure.Common.Logging
{
    [ProviderAlias("Database")]
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly DbLoggerOptions Options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbLoggerProvider(IOptions<DbLoggerOptions> _options, IHttpContextAccessor httpContextAccessor)
        {
            Options = _options.Value; // Stores all the options.
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Creates a new instance of the logger.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <returns>The logger.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(this, _httpContextAccessor);
        }

        public void Dispose()
        {
        }
    }
}
