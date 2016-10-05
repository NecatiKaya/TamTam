using System;
using System.Globalization;
using System.Security.Principal;

namespace Caterpillar.Core.Common
{
    public class CaterpillarUser : IPrincipal
    {
        #region Properties

        public CultureInfo Culture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public object UserId { get; set; }

        #endregion

        #region Constructors

        public CaterpillarUser(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns full name of user.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }

        #endregion

        #region IPrincipal

        /// <summary>
        /// Gets or sets the identity of user.
        /// </summary>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Always returns false.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get user id as integer.
        /// </summary>
        /// <returns>Returns integer.</returns>
        public int GetUserIdAsInt() 
        {
            return Convert.ToInt32(this.UserId);
        }

        /// <summary>
        /// Gets user id as Guid.
        /// </summary>
        /// <returns>Returns guid.</returns>
        public Guid GetUserIdAsGuid() 
        {
            return Guid.Parse(this.UserId.ToString());
        }

        #endregion
    }
}
