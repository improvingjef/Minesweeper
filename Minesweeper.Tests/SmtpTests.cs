using nDumbster.smtp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class SmtpTests
    {
        [Test, Ignore]
        public void It_Should_Work()
        {
            var smtp = SimpleSmtpServer.Start(2525);
            try
            {
                var message = new MailMessage("jef@jef.com", "a@a.com", "hello, world", "you are cruel.");
                using(var client = new SmtpClient("localhost", 2525))
                {
                    client.Send(message);
                }

                Assert.AreEqual(smtp.ReceivedEmailCount, 1);

                Assert.IsTrue(true);
            }
            finally
            {
                smtp.Stop();
            }
        }
    }
}
