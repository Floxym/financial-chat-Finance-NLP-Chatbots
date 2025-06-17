using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Financial.Chat.Domain.Shared.Handler
{
    public class LoggingHttpHandler : DelegatingHandler
    {
        private const string DEFAULT_HEADER_NAME_REFERENCE_ID = "reference-id";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);


            var uri = request.RequestUri;
            string referenceId = null, requestBody = null, responseBody = null;
            var responseStatus = response.StatusCode;

            try
            {
                if (request.Content != null)
                    requestBody = await request.Content.ReadAsStringAsync();

                if (response.Content != null)
                    responseBody = await response.Content.ReadAsStringAsync();

                if (request.Headers.TryGetValues(DEFAULT_HEADER_NAME_REFERENCE_ID, out var values))
                    referenceId = values?.FirstOrDefault();
            }
            catch (Exception)
            {
                //
            }
            return response;
        }
    }
}
