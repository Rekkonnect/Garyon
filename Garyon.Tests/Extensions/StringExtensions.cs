using Garyon.Extensions;
using NUnit.Framework;

namespace Garyon.Tests.Extensions
{
    public class StringExtensions
    {
        [Test]
        public void GetPascalCaseWords()
        {
            Assert.AreEqual("Pascal Case", "PascalCase".GetPascalCaseWordsString());
            Assert.AreEqual("I Interface Name", "IInterfaceName".GetPascalCaseWordsString());
            Assert.AreEqual("XML Parser", "XMLParser".GetPascalCaseWordsString());
            Assert.AreEqual("Some XML Parser", "SomeXMLParser".GetPascalCaseWordsString());
            Assert.AreEqual("Some XML", "SomeXML".GetPascalCaseWordsString());
            Assert.AreEqual("XML", "XML".GetPascalCaseWordsString());

            Assert.AreEqual("Vector 128 Parser", "Vector128Parser".GetPascalCaseWordsString(true));
            Assert.AreEqual("Vector128 Parser", "Vector128Parser".GetPascalCaseWordsString(false));
            Assert.AreEqual("Vector 128", "Vector128".GetPascalCaseWordsString(true));
            Assert.AreEqual("Vector128", "Vector128".GetPascalCaseWordsString(false));
        }
    }
}
