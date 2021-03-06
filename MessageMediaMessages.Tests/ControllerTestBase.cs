using System;
using NUnit.Framework;
using MessageMedia.Messages.Helpers;

namespace MessageMedia.Messages
{
	[TestFixture]
    public class ControllerTestBase
    {
        //Test setup
        public const int REQUEST_TIMEOUT = 60;
        protected const double ASSERT_PRECISION = 0.1;
        public TimeSpan globalTimeout = TimeSpan.FromSeconds(REQUEST_TIMEOUT);

        protected HttpCallBackEventsHandler httpCallBackHandler = new HttpCallBackEventsHandler();

        [SetUp]
        public void SetUp()
        {
            //hooking events for catching http requests and responses
            GetClient().SharedHttpClient.OnBeforeHttpRequestEvent += httpCallBackHandler.OnBeforeHttpRequestEventHandler;
            GetClient().SharedHttpClient.OnAfterHttpResponseEvent += httpCallBackHandler.OnAfterHttpResponseEventHandler;
		}

		// Singleton instance of client for all test classes
		private static MessageMediaMessagesClient client;
        private static object clientSync = new object();

        /// <summary>
        /// Get client instance
        /// </summary>
        /// <returns></returns>
        public static MessageMediaMessagesClient GetClient()
        {
            lock (clientSync)
            {
                if (client == null)
                {
			Configuration.BasicAuthUserName = Environment.GetEnvironmentVariable("MessageMediaApiTestsKey");
			Configuration.BasicAuthPassword = Environment.GetEnvironmentVariable("MessageMediaApiTestsSecret");
			client = new MessageMediaMessagesClient();
		}

			return client;
            }
        }
    }
}
