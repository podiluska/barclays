using System;
using System.IO;
using FileData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileDataTests
{
    [TestClass]
    public class ConsoleOutputWrapperTests
    {
        [TestMethod]
        public void WriteString_Writes_To_Console()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                var target = new ConsoleOutputWrapper();
                target.OutputString("test write");

                Assert.AreEqual("test write\r\n", writer.ToString());
            }
        }

        [TestMethod]
        public void Wait_Waits_For_NewLine()
        {
            using (var reader = new StringReader("abc\n"))
            {
                Console.SetIn(reader);

                var target = new ConsoleOutputWrapper();
                target.Wait();                
            }
        }
    }
}
