namespace Caterpillar.Core.Session
{
    /// <summary>
    /// Base application wide session service. Implement this application service interface for session services used in projects.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Gets object from session by its key.
        /// </summary>
        /// <typeparam name="T">Type of the object to return.</typeparam>
        /// <param name="key"></param>
        /// <returns>Returns item from cahce</returns>
        T Get<T>(string key);

        /// <summary>
        /// Sets objects into session with its key.
        /// </summary>
        /// <param name="key">Session key.</param>
        /// <param name="data">Data to add session.</param>
        void Set(string key, object data);

        /// <summary>
        /// Shows that a key is already set in session.
        /// </summary>
        /// <param name="key">Key to check in session.</param>
        /// <returns>Returns true if specified key is session, otherwise returns null</returns>
        bool IsSet(string key);

        /// <summary>
        /// Gets basic session object from session service.
        /// </summary>
        /// <returns>Returns SessionObect instance stored in session.</returns>
        BasicSessionObject GetBasicSessionObject();
    }
}
