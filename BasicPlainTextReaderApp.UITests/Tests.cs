using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BasicPlainTextReaderApp.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Basic Text Reader App"));
            //app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
            
            app.Invoke("OpenTextFile");
            AppResult[] results2 = app.WaitForElement(c => c.Id("InfoItemAi"));
        }

        
    }
}
