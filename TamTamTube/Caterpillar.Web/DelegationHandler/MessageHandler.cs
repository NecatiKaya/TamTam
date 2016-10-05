using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caterpillar.Web.DelegationHandler
{
    /// <summary>
    /// WebApi message Logging. 
    /// https://weblogs.asp.net/fredriknormen/log-message-request-and-response-in-asp-net-webapi
    /// </summary>
    public abstract class MessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string corrId = Guid.NewGuid().ToString();
            string requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);
            byte[] requestMessage = await request.Content.ReadAsByteArrayAsync();
            await IncommingMessageAsync(corrId, requestInfo, requestMessage);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage = await response.Content.ReadAsByteArrayAsync();
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            await OutgoingMessageAsync(corrId, requestInfo, responseMessage);
            return response;
        }


        protected abstract Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message);

        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);
    }
}
