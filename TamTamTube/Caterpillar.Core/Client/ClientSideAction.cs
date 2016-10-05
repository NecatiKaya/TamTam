
namespace Caterpillar.Core.Client
{
    /// <summary>
    /// Client side actions enumeration
    /// </summary>
    public enum ClientSideAction
    {
        /// <summary>
        /// Do nothing on client side.
        /// </summary>
        None = 0,
        /// <summary>
        /// Run javascript on client side.
        /// </summary>
        RunJavascript = 1,
        /// <summary>
        /// Show error message on client side.
        /// </summary>
        ShowErrorMessage = 2,
        /// <summary>
        /// Do redirect on client side.
        /// </summary>
        Redirect = 3
    }
}
