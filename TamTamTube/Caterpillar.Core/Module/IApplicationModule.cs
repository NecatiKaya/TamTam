namespace Caterpillar.Core.Module
{
    /// <summary>
    /// Interface type shows that derived types should be started when the app is started
    /// </summary>
    public interface IApplicationModule
    {
        /// <summary>
        /// Configures module when application is started.
        /// </summary>
        void ConfigureModuleWhenApplicationStarted();
    }
}
