using Caterpillar.Core.Security;
using System;
using System.Collections.Generic;
namespace Caterpillar.Core.Session
{
    /// <summary>
    /// Basic object that is stored in session.
    /// </summary>
    public class BasicSessionObject
    {
        /// <summary>
        /// Culture name in format of languagecode2-country/regioncode2.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets session data.
        /// </summary>
        public object Data { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string ApplicationName { get; set; }

        public DateTime SessionStartDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid PersonId { get; set; }

        public IEnumerable<UserRight> Rights { get; set; }
    }
}
