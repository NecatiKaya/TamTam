
namespace Caterpillar.Core.Client
{
    /// <summary>
    /// Shows that whether a request (e.q. ajax webmethod request, mvc action request etc..) is successfull or not
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        /// Shows that request (e.q. ajax webmethod request, mvc action request etc..) is unssuccesful.
        /// </summary>
        Fail = -1,
        /// <summary>
        /// Shows that request (e.q. ajax webmethod request, mvc action request etc..) is successful.
        /// </summary>
        Success = 1
    }
}
