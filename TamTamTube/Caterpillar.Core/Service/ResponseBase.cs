using Caterpillar.Core.Client;
using Caterpillar.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Caterpillar.Core.Service
{
    /// <summary>
    /// Base result class for service calls.
    /// </summary>
    /// <typeparam name="T">Type of data to return to client.</typeparam>
    [Serializable]
    [DataContract]
    public class ResponseBase<T>
    {
        #region Constructors

        public ResponseBase()
        {
            this.Errors = new StringToStringDictionary();
            this.Status = RequestStatus.Success;
            UserFriendlyErrorMesages = new StringToStringDictionary();
        }

        public ResponseBase(T data)
        {
            this.Success(data);
        }

        #endregion

        #region Properties
        
        [DataMember]
        /// <summary>
        /// Gets or sets wheter service request is succesfull or not
        /// </summary>
        public RequestStatus Status { get; set; }

        [DataMember]
        /// <summary>
        /// If service request is successfull, stores service result data. Otherwise, equals to null.
        /// </summary>
        public T Data { get; set; }

        [DataMember]
        /// <summary>
        /// Containse exceptions during service request
        /// </summary>
        public StringToStringDictionary Errors { get; set; }

        [DataMember]
        public StringToStringDictionary UserFriendlyErrorMesages { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create ResponseBase instance from data.
        /// </summary>
        /// <typeparam name="T">Type of data to place in ResponseBase object.</typeparam>
        /// <param name="data">Data to place in ResponseBase object.</param>
        /// <returns>Returns ResponseBase generic type.</returns>
        public static ResponseBase<T> CreateResponseWithData<T>(T data) 
        {
            ResponseBase<T> response = new ResponseBase<T>();
            response.Success(data);
            return response;
        }

        #endregion Static Methods

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
        public void Error(string errorKey, string errorMessage, bool setFirendlyErrorMessages = false)
        {
            InitializeErrorArray();
            SetResult(RequestStatus.Fail);
            if (errorKey == null)
            {
                this.Errors.Add(GetDictionaryKey(), errorMessage);
            }
            else
            {
                this.Errors.Add(errorKey, errorMessage);
            }

            if (setFirendlyErrorMessages)
            {
                if (this.UserFriendlyErrorMesages == null)
                {
                    this.UserFriendlyErrorMesages = new StringToStringDictionary();
                }
                this.UserFriendlyErrorMesages.Add(errorKey, errorMessage);
            }
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Fail and add error if error is not null.
        /// </summary>
        /// <param name="errors"></param>
        public void Error(Dictionary<string, string> errors, bool setFirendlyErrorMessages = false)
        {
            InitializeErrorArray();
            SetResult(RequestStatus.Fail);
            if (errors != null && errors.Count() > 0)
            {
                errors.ToList().ForEach(error => this.Errors.Add(error.Key, error.Value));
            }
            if (setFirendlyErrorMessages)
            {
                if (this.UserFriendlyErrorMesages == null)
                {
                    this.UserFriendlyErrorMesages = new StringToStringDictionary();
                }
                errors.ToList().ForEach(error => this.UserFriendlyErrorMesages.Add(error.Key, error.Value));
            }
        }

        /// <summary>
        /// Sets request result to ServiceRequestResult.Fail and add error if error is not null.
        /// </summary>
        /// <param name="errors"></param>
        public void Error(IEnumerable<string> errors, bool setFirendlyErrorMessages = false)
        {
            InitializeErrorArray();
            SetResult(RequestStatus.Fail);
            if (errors != null && errors.Count() > 0)
            {
                errors.ToList().ForEach(error => this.Errors.Add(GetDictionaryKey(), error));
                if (setFirendlyErrorMessages)
                {
                    if (this.UserFriendlyErrorMesages == null)
                    {
                        this.UserFriendlyErrorMesages = new StringToStringDictionary();
                    }
                    errors.ToList().ForEach(error => this.UserFriendlyErrorMesages.Add(GetDictionaryKey(), error));
                }
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
        public void Success(T data)
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
            if (this.Errors != null)
            {
                this.Errors.Clear();
                this.Errors = null;
            }

            if (this.UserFriendlyErrorMesages != null)
            {
                this.UserFriendlyErrorMesages.Clear();
                this.UserFriendlyErrorMesages = null;
            }
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

        /// <summary>
        /// Initialize errors array. If the array is null, inializes it with StringToStringDictionary instance. Otherwise, does nothing.
        /// </summary>
        private void InitializeErrorArray()
        {
            if (Errors == null)
            {
                Errors = new StringToStringDictionary();
            }
        }

        #endregion
    }
}
