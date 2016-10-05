using System;

namespace Caterpillar.Core.Extensions
{
    /// <summary>
    /// Contains core extensions for System.Object type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Wraps specified action into try cacth block, if exception is occured, then this method does the exception handling. Run this method for code clearance (no need to attach try catch block, exception handling code).
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action">Method to run in try block</param>
        /// <param name="errorAction">Method to run in cacth block when an exception is occured.</param>
        public static void RunSafely(this object obj, Action action, Action<System.Exception> errorAction = null)
        {
            try
            {
                action();
            }
            catch (System.Exception ex)
            {
                //TODO: Exception Handling.
                // Log exception to db, event log, msmq, text files...
                // Send mails, sms...

                //ExceptionManager manager = new ExceptionManager();
                //manager.Log(ex);

                if (errorAction != null)
                {
                    errorAction(ex);
                }
            }
        }

        public static T RunSafely<T>(this object obj, Func<T> action, Action<System.Exception> errorAction = null)
        {
            try
            {
                return action();
            }
            catch (System.Exception ex)
            {
                //TODO: Exception Handling.
                // Log exception to db, event log, msmq, text files...
                // Send mails, sms...

                //ExceptionManager manager = new ExceptionManager();
                //manager.Log(ex);

                if (errorAction != null)
                {
                    errorAction(ex);
                }
                return default(T);
            }
        }

        /// <summary>
        /// Checks whether specified object is null.
        /// </summary>
        /// <param name="obj">Object to check for being null.</param>
        /// <returns>Returns true if the object is null, otherwise false.</returns>
        public static bool IsNull(this object obj, bool checkForDbNull = false)
        {
            return obj == null || (checkForDbNull && DBNull.Value == obj);
        }

        /// <summary>
        /// Checks whether specified object is not null.
        /// </summary>
        /// <param name="obj">Object to check for being not null.</param>
        /// <returns>Returns true if the object is not null, otherwise false.</returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Converts object to int
        /// </summary>
        /// <param name="val">Objet to convert integer</param>
        /// <returns>Returns integer</returns>
        public static int ToInt32(this object val) 
        {
            return Convert.ToInt32(val);
        }

    }
}
