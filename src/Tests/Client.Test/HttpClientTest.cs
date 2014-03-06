using System;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ringo.Norif.Client.Test
{
    [TestClass]
    public class HttpClientTest
    {
        [TestMethod]
        public void Send_Notification_Sync()
        {
            var notif = new Notification
            {
                Priority = MailPriority.High,
                Recipients = new Recipient[] { new Recipient { Email = "mgentile@iadb.org" } },
                Sender = "mgentile@iadb.org",
                Subject = "Workflow",
                Template = "Example",
                Tokens = new Token[] { new Token { Name = "InformalGreeting", Value = "Hola!" } }
            };

            RestClient target = new RestClient("http://localhost/notifications/api");
            target.Notifications().Post(notif);
        }

        [TestMethod]
        public void Send_Notification_Async()
        {
            var notif = new Notification
            {
                Priority = MailPriority.High,
                Recipients = new Recipient[] { new Recipient { Email = "mgentile@iadb.org" } },
                Sender = "mgentile@iadb.org",
                Subject = "Workflow",
                Template = "Example",
                Tokens = new Token[] { new Token { Name = "InformalGreeting", Value = "Hola!" } }
            };

            RestClient target = new RestClient("http://localhost/notifications/api");
            var request = target.Notifications().PostAsync(notif);
            request.ContinueWith((x) =>
            {
                x.Result.EnsureSuccessStatusCode();
            });
            request.Wait();
        }

        [TestMethod]
        public void Send_Notification_Using_AltTemplate()
        {
            var notif = new Notification
            {
                Priority = MailPriority.High,
                Recipients = new Recipient[] { new Recipient { Email = "mgentile@iadb.org" } },
                Sender = "mgentile@iadb.org",
                AlternativeTemplate = "document distribution",
                Template = "document distribution cancelled",
                Tokens = new Token[] { 
                    new Token { Name = "State", Value = "Cancelled" },
                    new Token { Name = "Name", Value = "Document Distribution" },
                    new Token { Name = "Url", Value = "http://google.com" },
                    new Token { Name = "FinalDate", Value = "" }
                }
            };

            RestClient target = new RestClient("http://localhost/notifications/api");
            var request = target.Notifications().PostAsync(notif);
            var handler = request.ContinueWith((x) =>
            {
                x.Result.EnsureSuccessStatusCode();
            });
            handler.Wait();
        }
    }
}
