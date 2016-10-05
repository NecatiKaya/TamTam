using Caterpillar.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caterpillar.Web.DelegationHandler
{
    public class MessageLoggingHandler : MessageHandler
    {
        private ILogger _logger = null;

        public MessageLoggingHandler() : base()
        {

        }

        public MessageLoggingHandler(ILogger logger) : base()
        {
            _logger = logger;
        }

        protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {
                string request = string.Format("{0} - Request: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message));
                _logger.Log(request);
            });
        }

        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {
                string response = string.Format("{0} - Response: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message));
                _logger.Log(response);
            });
        }
    }
}
