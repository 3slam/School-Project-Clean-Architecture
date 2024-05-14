namespace SchoolWepApi.MiddleWares
{
    public class LimitNumberOfRequestsMiddleWare
    {
        public static int requestCount = 0;
        public static DateTime dateOfLastRequest = DateTime.Now;

        private readonly ILogger<LimitNumberOfRequestsMiddleWare> _logger;
        private readonly RequestDelegate _next;

        public LimitNumberOfRequestsMiddleWare(ILogger<LimitNumberOfRequestsMiddleWare> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            requestCount++;
            if (DateTime.Now.Subtract(dateOfLastRequest).Seconds > 10)
            {
                dateOfLastRequest = DateTime.Now;
                requestCount = 1;
                await _next(context);
            }
            else
            {
                if (requestCount > 5)
                {
                    dateOfLastRequest = DateTime.Now;
                    context.Response.StatusCode = StatusCodes.Status423Locked;
                    await context.Response.WriteAsync("Rate Limit Exceeded, You only have 5 requests for 10 seconds");
                }
                else
                {
                    dateOfLastRequest = DateTime.Now;
                    await _next(context);
                }
            }

        }
    }
}
