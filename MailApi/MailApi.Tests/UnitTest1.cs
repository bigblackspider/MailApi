using MailApi.ServiceInterface;
using MailApi.ServiceModel;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace MailApi.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost appHost;

        public UnitTests()
        {
            appHost = new BasicAppHost(typeof (MyServices).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
                .Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void TestMail()
        {
            var mail = new MailServer.Engine();
            mail.Authenticate("Administrator", "Flipper0");
            var lis = mail.Domains;
        }

        [Test]
        public void TestMethod1()
        {
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse) service.Any(new Hello {Name = "World"});

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}