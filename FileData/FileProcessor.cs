using System;
using FileData.Interfaces;

namespace FileData
{
    internal class FileProcessor : IFileProcessor
    {
        internal readonly IArgumentParser _argumentParser;
        internal readonly IFileInformationProvider _fileInformationProvider;
        internal readonly IOutputWrapper _outputWrapper;

        public FileProcessor(IArgumentParser argumentParser, IFileInformationProvider fileInformationProvider, IOutputWrapper outputWrapper)
        {
            _argumentParser = argumentParser;
            _fileInformationProvider = fileInformationProvider;
            _outputWrapper = outputWrapper;
        }

        public void GetInformation(string[] args)
        {
            var parseResults = _argumentParser.Parse(args);

            if (parseResults.IsSuccess)
            {
                string result;
                switch (parseResults.Result.Operation)
                {
                    case OperationParameters.Size:
                        result = _fileInformationProvider.GetSize(parseResults.Result.FileName).ToString();
                        break;
                    case OperationParameters.Version:
                        result = _fileInformationProvider.GetVersion(parseResults.Result.FileName);
                        break;
                    default:
                        throw new Exception();
                }
                _outputWrapper.OutputString(result);
                _outputWrapper.Wait();
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid arguments {0}", string.Join(" ", args)));
            }
        }
    }
}
