using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace BudgetBase.Infrastructure.Common.Logging
{
    /// <summary>
    /// Writes a log entry to the database.
    /// </summary>
    public class DbLogger : ILogger
    {
        /// <summary>
        /// Instance of <see cref="DbLoggerProvider" />.
        /// </summary>
        private readonly DbLoggerProvider _dbLoggerProvider;

        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Creates a new instance of <see cref="DbLogger" />.
        /// </summary>
        /// <param name="dbLoggerProvider">Instance of <see cref="DbLoggerProvider" />.</param>
        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider, IHttpContextAccessor httpContextAccessor)
        {
            _dbLoggerProvider = dbLoggerProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Whether to log the entry.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }


        /// <summary>
        /// Used to log the entry.
        /// </summary>
        /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>
        /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>
        /// <param name="state">The event's state.</param>
        /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>
        /// <param name="formatter">A delegate that formats </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                // Don't log the entry if it's not enabled.
                return;
            }

            // Get the current thread ID to use in the log file. 
            int threadId = Thread.CurrentThread.ManagedThreadId; 

            // Get the RequestId from HttpContext
            string requestId = Activity.Current?.Id ?? _httpContextAccessor.HttpContext?.TraceIdentifier;

            // Format the log message
            string message = formatter(state, exception);

            // Store record.
            using (NpgsqlConnection connection = new NpgsqlConnection(Environment.GetEnvironmentVariable(_dbLoggerProvider.Options.ConnectionString)))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format("INSERT INTO \"{0}\" (\"Date\", \"LogLevel\", \"ThreadId\", \"RequestId\",  \"EventId\", \"EventName\", \"ExceptionMessage\", \"ExceptionStackTrace\", \"ExceptionSource\") " +
                        "VALUES (@Date, @LogLevel, @ThreadId, @RequestId, @EventId, @EventName, @ExceptionMessage, @ExceptionStackTrace, @ExceptionSource)",
                        _dbLoggerProvider.Options.LogTable);

                    command.Parameters.Add(new NpgsqlParameter("@Date", NpgsqlDbType.TimestampTz) { Value = DateTime.UtcNow });
                    command.Parameters.Add(new NpgsqlParameter("@LogLevel", logLevel.ToString()));
                    command.Parameters.Add(new NpgsqlParameter("@ThreadId", threadId));
                    command.Parameters.Add(new NpgsqlParameter("@RequestId", requestId));
                    command.Parameters.Add(new NpgsqlParameter("@EventId", eventId.Id));
                    command.Parameters.Add(new NpgsqlParameter("@EventName", eventId.Name ?? string.Empty));
                    command.Parameters.Add(new NpgsqlParameter("@ExceptionMessage", message)); // Use the log message here
                    command.Parameters.Add(new NpgsqlParameter("@ExceptionStackTrace", exception?.StackTrace ?? string.Empty));
                    command.Parameters.Add(new NpgsqlParameter("@ExceptionSource", exception?.Source ?? string.Empty));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
