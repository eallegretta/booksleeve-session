using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Sockets;

namespace BookSleeve.Session
{
    /// <summary>
    /// Defines a RedisSession that acts as a connection pool to Booksleve's RedisConnection
    /// </summary>
    public sealed class RedisSession: IRedisSession
    {
        private const string REDIS_CONNECTION_FAILED_MESSAGE = "Redis connection failed.";
        private static readonly object _syncConnectionLock = new object();
        private readonly string _connectionStringName;

        private RedisConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisSession" /> class.
        /// </summary>
        public RedisSession()
            : this("Redis")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisSession" /> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public RedisSession(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            _connection = CreateRedisConnection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisSession" /> class.
        /// </summary>
        /// <param name="redisConnectionString">The redis connection string.</param>
        public RedisSession(RedisConnectionString redisConnectionString)
        {
            _connection = CreateRedisConnectionFromConnectionString(redisConnectionString);
        }

        /// <summary>
        /// Gets a value indicating whether the redis session is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if the redis session is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsAvailable
        {
            get
            {
                try
                {
                    return GetConnection() != null;
                }
                catch (RedisException ex)
                {
                    Trace.TraceError(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The opened RedisConnection</returns>
        /// <exception cref="System.Exception">When the connection cannot be opened.</exception>
        public RedisConnection GetConnection()
        {
            lock (_syncConnectionLock)
            {
                if (_connection == null)
                {
                    _connection = CreateRedisConnection();
                }

                if (_connection.State == RedisConnectionBase.ConnectionState.Opening)
                {
                    return _connection;
                }

                if (_connection.State == RedisConnectionBase.ConnectionState.Closing || _connection.State == RedisConnectionBase.ConnectionState.Closed)
                {
                    try
                    {
                        _connection = CreateRedisConnection();
                    }
                    catch (Exception ex)
                    {
                        throw new RedisException(REDIS_CONNECTION_FAILED_MESSAGE, ex);
                    }
                }

                if (_connection.State == RedisConnectionBase.ConnectionState.New)
                {
                    try
                    {
                        var openAsync = _connection.Open();
                        _connection.Wait(openAsync);
                    }
                    catch (SocketException ex)
                    {
                        throw new RedisException(REDIS_CONNECTION_FAILED_MESSAGE, ex);
                    }
                }

                return _connection;
            }
        }

        private static RedisConnection CreateRedisConnectionFromConnectionString(RedisConnectionSettings redisConnectionString)
        {
            return new RedisConnection(
                redisConnectionString.Host,
                redisConnectionString.Port,
                redisConnectionString.IOTimeout,
                redisConnectionString.Password,
                redisConnectionString.MaxUnsent,
                redisConnectionString.AllowAdmin,
                redisConnectionString.SyncTimeout);
        }

        private RedisConnection CreateRedisConnection()
        {
            var redisConnectionString = new RedisConnectionString(_connectionStringName);

            if (redisConnectionString == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("The Redis connection string with name {0} hasn't been found on the connection strings section in the configuration file", _connectionStringName));
            }

            return CreateRedisConnectionFromConnectionString(redisConnectionString);
        }
    }
}
