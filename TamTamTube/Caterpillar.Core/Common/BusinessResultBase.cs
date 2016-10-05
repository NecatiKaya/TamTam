using Caterpillar.Core.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caterpillar.Core.Common
{
    public class BusinessResultBase<T>
    {
        #region | Private Members |

        /// <summary>
        /// Contains exceptions about why request is not succesful.
        /// </summary>
        private StringToStringDictionary Exceptions;

        #endregion | Private Members |

        #region Constructors

        public BusinessResultBase(bool isSuccessful)
        {
            Exceptions = new StringToStringDictionary();
            IsSuccesful = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the service succesffuly does his job.
        /// </summary>
        public bool IsSuccesful { get; set; }

        /// <summary>
        /// Gets or sets data server returns. If no data is returned, this property will be null.
        /// </summary>
        public T Result { get; private set; }

        public string ErrorMessage
        {
            get
            {
                string error = null;
                if (this.Exceptions != null && this.Exceptions.Count > 0)
                {
                    StringBuilder errorTextBuilder = new StringBuilder();
                    foreach (KeyValuePair<string, string> eachError in this.Exceptions)
                    {
                        errorTextBuilder.AppendLine(string.Format("{0} - {1}", eachError.Key, eachError.Value));
                    }
                    error = errorTextBuilder.ToString();
                }
                return error;
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Adds new exception to Exceptions property.
        /// </summary>
        /// <param name="exception">Exception to add.</param>
        public void Error(string key, string exception, T result)
        {
            if (this.Exceptions == null)
            {
                this.Exceptions = new StringToStringDictionary();
            }

            this.Exceptions.Add(key, exception);
            IsSuccesful = false;
            SetResult(result);
        }

        /// <summary>
        /// Sets the result of the response instance.
        /// </summary>
        /// <param name="result"></param>
        public void SetResult(T result)
        {
            //TODO: This place is the last place of controlling the response. Because, response is ready to be sent to client. If needed, implement control logic here.
            Result = result;
        }

        public KeyValuePair<string, string> GetFirstErrorMessage()
        {
            if (this.Exceptions != null && this.Exceptions.Count > 0)
            {
                return this.Exceptions.First();
            }
            return new KeyValuePair<string, string>();
        }

        public KeyValuePair<string, string> FirstErrorMessage
        {
            get
            {

                if (this.Exceptions != null && this.Exceptions.Count > 0)
                {
                    return this.Exceptions.First();
                }
                return new KeyValuePair<string, string>();
            }            
        }

        public void Success()
        {
            this.Exceptions.Clear();
            this.Exceptions = null;
            this.IsSuccesful = true;
        }

        public void Success(T resultData)
        {
            this.Success();
            this.Result = resultData;
        }

        #endregion
    }
}
