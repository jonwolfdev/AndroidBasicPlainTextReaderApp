using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicPlainTextReaderApp.UnitTests
{
    public class HelperUnitTests
    {
        [Fact]
        public void Test_SearchAllStrings_ShouldReturnAllMatchingTexts()
        {
            //Arrange
            string text = "Text to be searched ñ\r\nAnother line t l";
            string search = "t";
            int getFromSides = 4;
            //Act
            var list = Helper.SearchAllStrings(text, search, getFromSides);

            //Assert
            Assert.Equal(5, list.Count);
            Assert.Equal("Text to b", list[0]);
            Assert.Equal("Text to b", list[1]);
            Assert.Equal("ext to be", list[2]);
            Assert.Equal("\nAnother ", list[3]);
            Assert.Equal("ine t l", list[4]);
        }

        [Fact]
        public void Test_SearchAllStrings_ShouldReturnSameText()
        {
            //Arrange
            string text = "Teñxt";
            string search = "teñxt";
            int getFromSides = 6;
            //Act
            var list = Helper.SearchAllStrings(text, search, getFromSides);

            //Assert
            Assert.Single(list);
            Assert.Equal("Teñxt", list[0]);
        }

        [Fact]
        public void Test_SearchAllStrings_ShouldExactEndOfText()
        {
            //Arrange
            string text = "12345t12345";
            string search = "t";
            int getFromSides = 5;
            //Act
            var list = Helper.SearchAllStrings(text, search, getFromSides);

            //Assert
            Assert.Single(list);
            Assert.Equal("12345t12345", list[0]);
        }

        /// <summary>
        /// This tests the case edge where `getFromSides` is exact where string ends
        /// </summary>
        [Fact]
        public void Test_SearchAllStrings_ExactFromGetSidesOnEnd()
        {
            //Arrange
            string text = "12345t123456";
            string search = "t";
            int getFromSides = 5;
            //Act
            var list = Helper.SearchAllStrings(text, search, getFromSides);

            //Assert
            Assert.Single(list);
            Assert.Equal("12345t12345", list[0]);
        }
    }
}
