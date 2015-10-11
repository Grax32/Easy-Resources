using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;
using Grax.EasyResource;

namespace EasyResourceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetStringResourceTest()
        {
            string resource1 = EasyResources.GetStringResource<UnitTest1>("\\Resources\\textfile.txt");
            string resource2 = EasyResources.GetStringResource<UnitTest1>("/Resources/textfile.txt");
            string resource3 = EasyResources.GetStringResource<UnitTest1>("EasyResourceTestsNamespace.Resources.textfile.txt");

            Assert.AreEqual("This is a test text file.", resource1);
            Assert.AreEqual("This is a test text file.", resource2);
            Assert.AreEqual("This is a test text file.", resource3);
        }

        [TestMethod]
        public void GetBinaryResourceTest()
        {
            byte[] startsWithExpected = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0 };
            byte[] endsWithExpected = { 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 };

            byte[] resource = EasyResources.GetResource<UnitTest1>("/Resources/Image.png");
            Assert.AreEqual(42956, resource.Length);

            var startsWith = resource.Take(10).ToArray();
            var endsWith = resource.Skip(resource.Length - 10).Take(10).ToArray();

            Assert.IsTrue(startsWithExpected.SequenceEqual(startsWith));
            Assert.IsTrue(endsWithExpected.SequenceEqual(endsWith));
        }

        [TestMethod]
        public void GetAssemblyStringResourceTest()
        {
            var assembly = Assembly.GetExecutingAssembly();

            string resource1 = assembly.GetStringResource("\\Resources\\textfile.txt");
            string resource2 = assembly.GetStringResource("/Resources/textfile.txt");
            string resource3 = assembly.GetStringResource("EasyResourceTestsNamespace.Resources.textfile.txt");

            Assert.AreEqual("This is a test text file.", resource1);
            Assert.AreEqual("This is a test text file.", resource2);
            Assert.AreEqual("This is a test text file.", resource3);
        }

        [TestMethod]
        public void GetAssemblyBinaryResourceTest()
        {
            var assembly = Assembly.GetExecutingAssembly();
            byte[] startsWithExpected = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0 };
            byte[] endsWithExpected = { 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 };

            byte[] resource = assembly.GetResource("/Resources/Image.png");
            Assert.AreEqual(42956, resource.Length);

            var startsWith = resource.Take(10).ToArray();
            var endsWith = resource.Skip(resource.Length - 10).Take(10).ToArray();

            Assert.IsTrue(startsWithExpected.SequenceEqual(startsWith));
            Assert.IsTrue(endsWithExpected.SequenceEqual(endsWith));
        }
    }
}
