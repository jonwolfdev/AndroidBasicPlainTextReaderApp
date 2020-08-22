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
        readonly Platform platform;

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
            
            Assert.IsTrue(results.Any());
            var elem = app.Query("Made by @jonwolfdev").FirstOrDefault();
            Assert.IsNotNull(elem);
        }

        [Test]
        public void OpenFileIsDisplayedAndSearchWorks()
        {
            string fileContents = "Test file\nTest file line 2\n";
            string filePath = "/data/user/0/dev.jonwolf.basicplaintextreaderapp/files/test.txt";
            string searchPopupText = "Enter the text to search for";
            string firstSearch = "file";
            string secondSearch = "line";
            string buttonPopupInfoClose = "button2";
            string buttonPopupSearchSearch = "button1";
            app.Invoke("OpenTextFile");

            AppResult[] results2 = app.WaitForElement(c => c.Marked(filePath));
            Assert.AreEqual(1, results2.Count());


            var label = app.Query(fileContents).FirstOrDefault();
            Assert.IsNotNull(label);
            Assert.AreEqual(fileContents, label.Text);
            app.Tap("Info");

            // Popup message
            AppResult[] resultsMsg = app.WaitForElement(c => c.Marked("message"));
            Assert.AreEqual(1, resultsMsg.Length);

            Assert.IsTrue(resultsMsg[0].Text.Contains("Type: text/plain"));
            Assert.IsTrue(resultsMsg[0].Text.Contains("DataPath: " + filePath));

            // Close button
            app.Tap(buttonPopupInfoClose);
            app.WaitForNoElement("message");
            {
                app.Tap("Search");
                // Popup search
                var searchPopup = app.WaitForElement("message");
                Assert.IsTrue(searchPopup.Any());

                Assert.AreEqual(searchPopupText, searchPopup[0].Text);
                app.EnterText(firstSearch);
                // Search button
                app.Tap(buttonPopupSearchSearch);

                app.WaitForElement("Found 2 matches");

                var elemMatches = app.WaitForElement(fileContents);
                Assert.AreEqual(2, elemMatches.Length);
            }

            // Search again but within the same search page
            {
                app.Tap("Search");
                // Popup search
                var searchPopup = app.WaitForElement("message");
                Assert.IsTrue(searchPopup.Any());

                Assert.AreEqual(searchPopupText, searchPopup[0].Text);
                app.EnterText(secondSearch);
                // Search button
                app.Tap(buttonPopupSearchSearch);

                app.WaitForElement("Found 1 matches");

                var elemMatches = app.WaitForElement(fileContents);
                Assert.AreEqual(1, elemMatches.Length);
            }

            app.Back();

            AppResult[] resultsBack = app.WaitForElement(c => c.Marked(filePath));
            Assert.AreEqual(1, resultsBack.Count());
        }

        
    }
}
