using FileData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FileDataTests
{
    [TestClass]
    public class ArgumentParserTests
    {
        [TestMethod]
        public void Null_Arguments_Returns_False()
        {
            var target = new ArgumentParser();

            var result = target.Parse(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Empty_Arguments_Returns_False()
        {
            var arguments = new string[] { };

            var target = new ArgumentParser();

            var result = target.Parse(arguments);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Single_Argument_Returns_False()
        {
            var arguments = new string[] { "single" };

            var target = new ArgumentParser();

            var result = target.Parse(arguments);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Three_Arguments_Returns_False()
        {
            var arguments = new string[] { "one", "two", "three" };

            var target = new ArgumentParser();

            var result = target.Parse(arguments);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Valid_First_Argument_Returns_True()
        {
            var arguments = new string[] { "operation", "filename" };

            var mockTarget = new Mock<ArgumentParser>();
            mockTarget.CallBase = true;
            mockTarget.Setup(t => t.ParseOperationParameter(It.Is<string>(s => s == "operation")))
                .Returns(new MethodResult<FileInformationArguments>(new FileInformationArguments()
                {
                    Operation = OperationParameters.Size,
                }));
            var target = mockTarget.Object;
            var result = target.Parse(arguments);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(OperationParameters.Size, result.Result.Operation);
            Assert.AreEqual("filename", result.Result.FileName);

            mockTarget.VerifyAll();
        }

        [TestMethod]
        public void Invalid_First_Argument_Returns_False()
        {
            var arguments = new string[] { "operation", "filename" };

            var mockTarget = new Mock<ArgumentParser>();
            mockTarget.CallBase = true;
            mockTarget.Setup(t => t.ParseOperationParameter(It.Is<string>(s => s == "operation"))).Returns(MethodResult<FileInformationArguments>.Fail("error"));
            var target = mockTarget.Object;
            var result = target.Parse(arguments);

            Assert.IsFalse(result.IsSuccess);

            mockTarget.VerifyAll();
        }

        [TestMethod]
        public void Null_Operation_Parameter_Returns_False()
        {
            var target = new ArgumentParser();

            var result = target.ParseOperationParameter(null);

            Assert.IsFalse(result.IsSuccess);             
        }

        [TestMethod]
        public void Empty_Operation_Parameter_Returns_False()
        {
            var target = new ArgumentParser();

            var result = target.ParseOperationParameter(string.Empty);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Single_Character_Operation_Parameter_Returns_False()
        {
            var tests = new string[] { "-", "/", "a", "v", "s" };
            foreach (var test in tests)
            {
                var target = new ArgumentParser();

                var result = target.ParseOperationParameter(test);

                Assert.IsFalse(result.IsSuccess);
            }
        }

        [TestMethod]
        public void Valid_Two_Character_Version_Operation_Parameter_Returns_True_And_Sets_Operation()
        {
            var tests = new string[] { "-v", "/v", "–v", "-V", "/V", "–V", "--v", "--version", "--V", "--Version"};
            foreach (var test in tests)
            {
                var target = new ArgumentParser();

                var result = target.ParseOperationParameter(test);

                Assert.IsTrue(result.IsSuccess);
                Assert.AreEqual(OperationParameters.Version, result.Result.Operation);
            }
        }

        [TestMethod]
        public void Valid_Two_Character_Size_Operation_Parameter_Returns_True_And_Sets_Operation()
        {
            var tests = new string[] { "-s", "/s", "–s", "-S", "/S", "–S", "--size", "--S", "--SIZE" };
            foreach (var test in tests)
            {
                var target = new ArgumentParser();

                var result = target.ParseOperationParameter(test);

                Assert.IsTrue(result.IsSuccess);
                Assert.AreEqual(OperationParameters.Size, result.Result.Operation);
            }
        }

        [TestMethod]
        public void Invalid_Two_Character_Size_Operation_Parameter_Returns_False()
        {
            var tests = new string[] { "-T", "/q", "–p", "--", "v-", "av", "bs", "..", "ab", "--Cat" };
            foreach (var test in tests)
            {
                var target = new ArgumentParser();

                var result = target.ParseOperationParameter(test);

                Assert.IsFalse(result.IsSuccess);
            }
        }        
    }
}
