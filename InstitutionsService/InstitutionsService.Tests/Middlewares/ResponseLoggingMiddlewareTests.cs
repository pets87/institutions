using InstitutionsService.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace InstitutionsService.Tests.Middlewares
{
    [TestClass]
    public class ResponseLoggingMiddlewareTests
    {
        private Mock<ILogger<ResponseLoggingMiddleware>> mockLogger;
        private ResponseLoggingMiddleware middleware;

        [TestInitialize]
        public void TestInitialize()
        {
            mockLogger = new Mock<ILogger<ResponseLoggingMiddleware>>();
            middleware = new ResponseLoggingMiddleware(async (HttpContext context) =>
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("{ \"message\": \"Success\" }");
            }, mockLogger.Object);
        }

        [TestMethod]
        public async Task InvokeAsync_LogsResponseDetails()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            context.Response.Headers["Content-Type"] = "application/json";

            await middleware.InvokeAsync(context);

            var logMessage = mockLogger.Invocations[0].Arguments[2].ToString();

            Assert.IsTrue(logMessage.Contains("Outgoing Response: 200"));
            Assert.IsTrue(logMessage.Contains("Content-Type: application/json"));
            Assert.IsTrue(logMessage.Contains("Response Body: { \"message\": \"Success\" }"));
        }

        [TestMethod]
        public async Task InvokeAsync_LogsResponseWithEmptyBody()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            context.Response.Headers["Accept"] = "application/json";

            var middlewareWithEmptyResponse = new ResponseLoggingMiddleware(async (HttpContext ctx) =>
            {
                ctx.Response.StatusCode = 204;
                await Task.CompletedTask;
            }, mockLogger.Object);

            await middlewareWithEmptyResponse.InvokeAsync(context);

            var logMessage = mockLogger.Invocations[0].Arguments[2].ToString();

            Assert.IsTrue(logMessage.Contains("Outgoing Response: 204"));
            Assert.IsTrue(logMessage.Contains("Accept: application/json"));
            Assert.IsFalse(logMessage.Contains("Response Body:"));
        }

        [TestMethod]
        public async Task InvokeAsync_CallsNextMiddleware()
        {
            var nextCalled = false;
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var middlewareWithNext = new ResponseLoggingMiddleware(async (HttpContext ctx) =>
            {
                nextCalled = true; // Set flag when the next middleware is called
                await Task.CompletedTask; // Simulate async operation
            }, mockLogger.Object);

            await middlewareWithNext.InvokeAsync(context);

            Assert.IsTrue(nextCalled, "The next middleware was not called.");
        }
    }
}
