using InstitutionsService.Middlewares;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Logging;
using System.Text;

namespace InstitutionsService.Tests.Middlewares
{
    [TestClass]
    public class RequestLoggingMiddlewareTests
    {
        private Mock<ILogger<RequestLoggingMiddleware>> mockLogger;
        private RequestLoggingMiddleware middleware;

        [TestInitialize]
        public void TestInitialize()
        {
            mockLogger = new Mock<ILogger<RequestLoggingMiddleware>>();
            middleware = new RequestLoggingMiddleware(async (HttpContext context) => { }, mockLogger.Object);
        }

        [TestMethod]
        public async Task InvokeAsync_LogsRequestDetails()
        {
            var context = new DefaultHttpContext();
            context.Request.Method = "POST";
            context.Request.Path = "/api/test";
            context.Request.Headers["Content-Type"] = "application/json";
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{ \"key\": \"value\" }"));
            context.Request.EnableBuffering();

            await middleware.InvokeAsync(context);

            var logMessage = mockLogger.Invocations[0].Arguments[2].ToString();

            Assert.IsTrue(logMessage.Contains("Incoming Request: POST /api/test"));
            Assert.IsTrue(logMessage.Contains("Content-Type: application/json"));
            Assert.IsTrue(logMessage.Contains("Request Body: { \"key\": \"value\" }"));
        }

        [TestMethod]
        public async Task InvokeAsync_LogsRequestWithEmptyBody()
        {
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/api/test";
            context.Request.Headers["Accept"] = "application/json";
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            context.Request.EnableBuffering();

            await middleware.InvokeAsync(context);

            var logMessage = mockLogger.Invocations[0].Arguments[2].ToString();

            Assert.IsTrue(logMessage.Contains("Incoming Request: GET /api/test"));
            Assert.IsTrue(logMessage.Contains("Accept: application/json"));
            Assert.IsFalse(logMessage.Contains("Request Body:"));
        }

        [TestMethod]
        public async Task InvokeAsync_CallsNextMiddleware()
        {
            // Arrange
            var nextCalled = false;
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/api/test";

            var middlewareWithNext = new RequestLoggingMiddleware(async (HttpContext ctx) =>
            {
                nextCalled = true; // Set flag when the next middleware is called
                await Task.CompletedTask; // Simulate async operation
            }, mockLogger.Object);

            await middlewareWithNext.InvokeAsync(context);

            Assert.IsTrue(nextCalled, "The next middleware was not called.");
        }
    }
}
