using System;
using FileData.Interfaces;

namespace FileData
{
    internal class ConsoleOutputWrapper : IOutputWrapper
    {

        public void OutputString(string output)
        {
            Console.WriteLine(output);
        }

        public void Wait()
        {
            Console.ReadLine();
        }
    }
}
