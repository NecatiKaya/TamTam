
namespace Caterpillar.Core.Data
{
    /// <summary>
    /// Builds connection string for database servers
    /// </summary>
    public abstract class ConnectionStringBuilderBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets server name.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets database name.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets user id for connection credential.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets password for connection credential.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets whether the connection string doesn't need to contain credential.
        /// </summary>
        public bool IntegratedSecurity { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds connection string
        /// </summary>
        /// <returns>Returns connection string in string instance.</returns>
        public abstract string Build();

        #endregion
    }
}
