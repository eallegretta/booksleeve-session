using System.Configuration;

namespace BookSleeve.Session
{
    /// <summary>
    /// Represents a Redis Connection String configuration
    /// </summary>
    public class RedisConnectionString : RedisConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisConnectionString" /> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public RedisConnectionString(string connectionStringName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ConfigurationErrorsException("The connection string name cannot be null or whitespace");
            }

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            InitializeFromConnectionString(connectionString);
        }

        private RedisConnectionString()
        {
        }

        /// <summary>
        /// Parses the connection string and returns a new instance of the RedisConnectionString class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>A new instance of the RedisConnectionString class with the parsed connection string.</returns>
        public static RedisConnectionString Parse(string connectionString)
        {
            var connString = new RedisConnectionString();
            connString.InitializeFromConnectionString(connectionString);
            return connString;
        }

        private void InitializeFromConnectionString(string connectionString)
        {
            foreach (var item in connectionString.Split(';'))
            {
                if (!item.Contains("="))
                {
                    continue;
                }

                var keyValue = item.Split('=');
                string key = keyValue[0];
                string value = keyValue[1];
                switch (key.Trim().ToUpperInvariant())
                {
                    case "HOST":
                        Host = value;
                        break;
                    case "PORT":
                        int port;
                        if (int.TryParse(value, out port))
                        {
                            Port = port;
                        }

                        break;
                    case "IOTIMEOUT":
                        int ioTimeout;
                        if (int.TryParse(value, out ioTimeout))
                        {
                            IOTimeout = ioTimeout;
                        }

                        break;
                    case "PASSWORD":
                        Password = value;
                        break;
                    case "MAXUNSENT":
                        int maxUnsent;
                        if (int.TryParse(value, out maxUnsent))
                        {
                            MaxUnsent = maxUnsent;
                        }

                        break;
                    case "ALLOWADMIN":
                        bool allowAdmin;
                        if (bool.TryParse(value, out allowAdmin))
                        {
                            AllowAdmin = allowAdmin;
                        }

                        break;
                    case "SYNCTIMEOUT":
                        int syncTimeout;
                        if (int.TryParse(value, out syncTimeout))
                        {
                            SyncTimeout = syncTimeout;
                        }

                        break;
                }
            }
        }  
    }
}
