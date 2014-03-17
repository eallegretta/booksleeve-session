namespace BookSleeve.Session
{
    /// <summary>
    /// Base class for using Redis connection settings
    /// </summary>
    public class RedisConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisConnectionSettings"/> class.
        /// </summary>
        /// <param name="host">The hostname or IP for the Redis server</param>
        /// <param name="port">The port of the Redis port</param>
        /// <param name="ioTimeout">The timeout to use during IO operations; this can usually be left unlimited</param>
        /// <param name="password">The password for Redis authentication</param>
        /// <param name="maxUnsent">The maximum number of unsent messages to enqueue before new requests are blocking calls</param>
        /// <param name="allowAdmin">Allow admin access?</param>
        /// <param name="syncTimeout">Sets the timeout for task API</param>
        public RedisConnectionSettings(string host= "127.0.0.1", int port= 6379, int ioTimeout= -1, string password= null, int maxUnsent= int.MaxValue, bool allowAdmin= false, int syncTimeout= 10000)
        {
            Host = host;
            Port = port;
            IOTimeout = ioTimeout;
            Password = password;
            MaxUnsent = maxUnsent;
            AllowAdmin = allowAdmin;
            SyncTimeout = syncTimeout;
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host { get; protected set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; protected set; }

        /// <summary>
        /// Gets or sets the io timeout.
        /// </summary>
        /// <value>
        /// The io timeout.
        /// </value>
        public int IOTimeout { get; protected set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; protected set; }

        /// <summary>
        /// Gets or sets the max unsent.
        /// </summary>
        /// <value>
        /// The max unsent.
        /// </value>
        public int MaxUnsent { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow admin].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow admin]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowAdmin { get; protected set; }

        /// <summary>
        /// Gets or sets the sync timeout.
        /// </summary>
        /// <value>
        /// The sync timeout.
        /// </value>
        public int SyncTimeout { get; protected set; }
    }
}