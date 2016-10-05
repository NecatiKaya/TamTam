using Caterpillar.Core.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Caterpillar.Core.Client
{
    /// <summary>
    /// Common ajax request result object. After succesful ajax request this object is can be used in ajax scripts.
    /// </summary>
    public class JsonResultBase
    {
        #region Constructors

        public JsonResultBase()
        {
            this.Errors = new StringToStringDictionary();
            this.Status = RequestStatus.Fail;
        } 

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets wheter service request is succesfull or not
        /// </summary>
        public RequestStatus Status { get; set; }

        /// <summary>
        /// If service request is successfull, stores service result data. Otherwise, equals to null
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Containse exceptions during service request
        /// </summary>
        public StringToStringDictionary Errors { get; set; }

        /// <summary>
        /// Gets or sets ClientSideAction that shows what to do on client side.
        /// </summary>
        public ClientSideAction ClientSideAction { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets request result to ServiceRequestResult.Fail and add error if error is not null.
        /// </summary>
        /// <param name="errorMessage"></param>
        public void Error(string errorMessage)
        {
            Error(null, errorMessage);
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Fail and add error if error is not null.
        /// </summary>
        /// <param name="errorKey"></param>
        /// <param name="errorMessage"></param>
        public void Error(string errorKey, string errorMessage)
        {
            SetResult(RequestStatus.Fail);
            if (errorKey == null)
            {
                this.Errors.Add(GetDictionaryKey(), errorMessage);
            }
            else
            {
                this.Errors.Add(errorKey, errorMessage);
            }
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Fail and add error if error is not null.
        /// </summary>
        /// <param name="errors"></param>
        public void Error(Dictionary<string, string> errors)
        {
            SetResult(RequestStatus.Fail);
            if (errors != null && errors.Count() > 0)
            {
                errors.ToList().ForEach(error => this.Errors.Add(error.Key, error.Value));
            }
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Fail and add error if error is not null.
        /// </summary>
        /// <param name="errors"></param>
        public void Error(IEnumerable<string> errors)
        {
            SetResult(RequestStatus.Fail);
            if (errors != null && errors.Count() > 0)
            {
                errors.ToList().ForEach(error => this.Errors.Add(GetDictionaryKey(), error));
            }
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Success.
        /// </summary>
        /// <param name="data"></param>
        public void Success()
        {
            SetResult(RequestStatus.Success);
            ClearErrors();
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Success and sets the data.
        /// </summary>
        /// <param name="data"></param>
        public void Success(object data)
        {
            SetResult(RequestStatus.Success);
            this.Data = data;
            ClearErrors();
        }

        /// <summary>
        /// Clears errors
        /// </summary>
        public void ClearErrors()
        {
            this.Errors.Clear();
        }

        public static JsonResultBase GenericError(string errorMessage, string errorKey = "0") 
        {
            JsonResultBase jrb = new JsonResultBase() { Data = null, Status = RequestStatus.Fail, Errors = new StringToStringDictionary() { { errorKey, errorMessage } } };
            return jrb;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Sets the object's RequestResult property.
        /// </summary>
        /// <param name="requestResult"></param>
        private void SetResult(RequestStatus requestResult)
        {
            this.Status = requestResult;
        }

        /// <summary>
        /// Gets a dictionary key.
        /// </summary>
        /// <returns></returns>
        private string GetDictionaryKey() 
        {
            return (this.Errors.Count + 1).ToString();
        }

        #endregion
    }
}
