using System.Net;

namespace Middleware;

//Bonus 2:Global exception middleware i oluşturun
public class GlobalExceptionHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            // İstek işleme devam ediyor.
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Hata:"+ex);

            var errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Internal Server Error: " + ex.Message),
                ReasonPhrase = "Exception"
            };

            return errorResponse;
        }
    }
}

