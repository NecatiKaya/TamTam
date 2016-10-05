using Caterpillar.Core.Exception;

namespace Caterpillar.Core.Data
{
    /// <summary>
    /// Connection string builder for Ms Sql Server
    /// </summary>
    public sealed class SqlConnectionStringBuilder : ConnectionStringBuilderBase
    {
        #region ConnectionStringBuilderBase

        /// <summary>
        /// Builds connection string for Ms Sql Server
        /// </summary>
        /// <returns></returns>
        public override string Build()
        {
            if (string.IsNullOrWhiteSpace(this.Server))
            {
                throw new ArgumentNullOrEmptyException(LanguageStrings.ServerInformationMustBeSetForConnectionString);
            }

            if (!this.IntegratedSecurity)
            {
                if (string.IsNullOrWhiteSpace(this.UserId))
                {
                    throw new ArgumentNullOrEmptyException(LanguageStrings.UserIdInformationMustBeSetForConnectionString);
                }

                if (string.IsNullOrWhiteSpace(this.Password))
                {
                    throw new ArgumentNullOrEmptyException(LanguageStrings.PasswordInformationMustBeSetForConnectionString);

                }
            }

            string connectionString = string.Format("server={0};", this.Server);
            if (IntegratedSecurity)
            {
                connectionString += "Integrated Security = true;";
            }
            else
            {
                connectionString += string.Format("User Id = {0}; Password = {1};", this.UserId, this.Password);
            }
            return connectionString;
        }

        #endregion
    }
}
