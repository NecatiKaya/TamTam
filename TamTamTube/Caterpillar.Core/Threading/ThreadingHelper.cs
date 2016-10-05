using Caterpillar.Core.Extensions;
using System;

namespace Caterpillar.Core.Threading
{
    /// <summary>
    /// Contains helper methods for threading operations
    /// </summary>
    public static class ThreadingHelper
    {
        /// <summary>
        /// Runs job in thread pool. After jo successfuly completes it's duty, successAction is called.
        /// </summary>
        /// <param name="job">Job to do in background</param>
        /// <param name="successAction">Action invoked after successfully completing job.</param>
        public static void RunInThreadPool(Action job, Action successAction, Action<System.Exception> errorAction = null)
        {
            if (job == null)
            {
                throw new ArgumentNullException(string.Format(LanguageStrings.ArgumentNullExceptionMessageFormat, "job"));
            }

            System.Threading.ThreadPool.QueueUserWorkItem(obj =>
            {
                object dummyObject = new object();
                dummyObject.RunSafely(() => 
                {
                    if (job != null)
                    {
                        job();
                        if (successAction != null)
                        {
                            successAction();
                        }
                    }
                }, errorAction);
            });
        }
    }
}
