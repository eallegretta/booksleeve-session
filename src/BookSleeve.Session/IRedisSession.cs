namespace BookSleeve.Session
{
    /// <summary>
    /// Defines the contract for a redis session
    /// </summary>
    public interface IRedisSession
    {
        /// <summary>
        /// Gets a value indicating whether the redis session is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if the redis session is available; otherwise, <c>false</c>.
        /// </value>
        bool IsAvailable { get; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>Gets the redis connection</returns>
        RedisConnection GetConnection();
    }
}
