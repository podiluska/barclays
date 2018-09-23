using System.Text.RegularExpressions;
using FileData.Interfaces;

namespace FileData
{
    internal class ArgumentParser : IArgumentParser
    {
        public MethodResult<FileInformationArguments> Parse(string[] args)
        {
            MethodResult<FileInformationArguments> result;
            if (args != null && args.Length == 2)
            {
                result = ParseOperationParameter(args[0]);
                if (result.IsSuccess)
                {
                    result.Result.FileName = args[1];
                }
            }
            else
            {
                result = MethodResult<FileInformationArguments>.Fail("Invalid number of arguments");
            }
            return result;
        }

        protected internal virtual MethodResult<FileInformationArguments> ParseOperationParameter(string operationParameter)
        {
            MethodResult<FileInformationArguments> result = MethodResult<FileInformationArguments>.Fail("Invalid arguments");
            if (string.IsNullOrEmpty(operationParameter))
            {

            }
            else
            {
                var regex = new Regex("^(?<prefix>[-/–]){1,2}(?<operation>[sv])", RegexOptions.IgnoreCase);
                var results = regex.Match(operationParameter);
                if (results.Success)
                {
                    switch (results.Groups["operation"].Value.ToLowerInvariant())
                    {
                        case "v":
                            result = new MethodResult<FileInformationArguments>(
                                    new FileInformationArguments()
                                    {
                                        Operation = OperationParameters.Version,
                                    }
                                );
                            break;
                        case "s":
                            result = new MethodResult<FileInformationArguments>(
                                    new FileInformationArguments()
                                    {
                                        Operation = OperationParameters.Size,
                                    }
                                );
                            break;
                    }

                }
            }
            return result;
        }
    }
}
