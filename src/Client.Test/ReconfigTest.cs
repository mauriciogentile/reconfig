using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reconfig.Configuration;

namespace Reconfig.Client.Test
{
    [TestClass]
    public class HttpClientTest
    {
        [TestMethod]
        public void Should_Send_Notification()
        {
            ReconfigManager.AppSettings.Clear();
            ReconfigManager.GetSection("appSettings");
        }
    }
}
