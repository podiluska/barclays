using System;
using System.Linq;
using FileData;
using FileData.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FileDataTests
{
    [TestClass]
    public class FileProcessorTests
    {
        private readonly MockRepository _mockFactory = new MockRepository(MockBehavior.Strict);

        [TestMethod]
        public void Constructor_Sets_Dependencies()
        {
            var argumentParser = _mockFactory.Create<IArgumentParser>().Object;
            var fileInformation = _mockFactory.Create<IFileInformationProvider>().Object;
            var outputWrapper = _mockFactory.Create<IOutputWrapper>().Object;

            var target = new FileProcessor(argumentParser, fileInformation, outputWrapper);

            Assert.AreEqual(argumentParser, target._argumentParser);
            Assert.AreEqual(fileInformation, target._fileInformationProvider);
            Assert.AreEqual(outputWrapper, target._outputWrapper);
        }

        [TestMethod]
        public void Invalid_Arguments_Throws_Argument_Exception()
        {
            var invalidArguments = new string[] { "fail" };
            var argumentParser = _mockFactory.Create<IArgumentParser>();
            argumentParser.Setup(ap => ap.Parse(It.Is<string[]>(s => s == invalidArguments))).Returns(MethodResult<FileInformationArguments>.Fail("error"));

            var target = new FileProcessor(argumentParser.Object, null, null);

            try
            {
                target.GetInformation(invalidArguments);
                Assert.Fail();
            }
            catch (ArgumentException)
            {

            }
            catch
            {
                Assert.Fail();
            }

            argumentParser.VerifyAll();
        }

        [TestMethod]
        public void File_Size_Argument_Outputs_File_Size()
        {
            var fileSizeArguments = new string[] { "file size" };
            var result = new FileInformationArguments()
            {
                FileName = "the file",
                Operation = OperationParameters.Size,
            };
            var expectedSize = 102982;

            var argumentParser = _mockFactory.Create<IArgumentParser>();
            argumentParser.Setup(ap => ap.Parse(It.Is<string[]>(s => s == fileSizeArguments))).Returns(new MethodResult<FileInformationArguments>(result));

            var fileInformation = _mockFactory.Create<IFileInformationProvider>();
            fileInformation.Setup(fi => fi.GetSize(It.Is<string>(s => s == result.FileName))).Returns(expectedSize);

            var outputWrapper = _mockFactory.Create<IOutputWrapper>();
            outputWrapper.Setup(o => o.OutputString(It.Is<string>(s => s == expectedSize.ToString())));
            outputWrapper.Setup(o => o.Wait());

            var target = new FileProcessor(argumentParser.Object, fileInformation.Object, outputWrapper.Object);

            target.GetInformation(fileSizeArguments);

            argumentParser.VerifyAll();
            outputWrapper.VerifyAll();
            fileInformation.VerifyAll();
        }

        [TestMethod]
        public void File_Version_Argument_Outputs_File_Version()
        {
            var fileVersionArguments = new string[] { "file", "version" };
            var result = new FileInformationArguments()
            {
                FileName = "the file.name",
                Operation = OperationParameters.Version,
            };

            var expectedVersion = "v1.2.3";

            var argumentParser = _mockFactory.Create<IArgumentParser>();
            argumentParser.Setup(ap => ap.Parse(It.Is<string[]>(s => s == fileVersionArguments))).Returns(new MethodResult<FileInformationArguments>(result));

            var fileInformation = _mockFactory.Create<IFileInformationProvider>();
            fileInformation.Setup(fi => fi.GetVersion(It.Is<string>(s => s == result.FileName))).Returns(expectedVersion);

            var outputWrapper = _mockFactory.Create<IOutputWrapper>();
            outputWrapper.Setup(o => o.OutputString(It.Is<string>(s => s == expectedVersion)));
            outputWrapper.Setup(o => o.Wait());

            var target = new FileProcessor(argumentParser.Object, fileInformation.Object, outputWrapper.Object);

            target.GetInformation(fileVersionArguments);

            argumentParser.VerifyAll();
            outputWrapper.VerifyAll();
            fileInformation.VerifyAll();
        }

        [TestMethod]
        public void Unknown_Operation_Throws_Exception()
        {
            var newOperationArguments = new string[] { "undefined" };

            var unknownOperation = new FileInformationArguments()
            {
                Operation =  (OperationParameters)Enum.GetValues(typeof(OperationParameters)).Cast<int>().Min() - 1,
                FileName = "file.name",
            };

            var argumentParser = _mockFactory.Create<IArgumentParser>();
            argumentParser.Setup(ap => ap.Parse(It.Is<string[]>(s => s == newOperationArguments))).Returns(new MethodResult<FileInformationArguments>(unknownOperation));

            var target = new FileProcessor(argumentParser.Object, null, null);

            try
            {
                target.GetInformation(newOperationArguments);
                Assert.Fail();
            }
            catch
            {

            }

            argumentParser.VerifyAll();
        }


    }
}
