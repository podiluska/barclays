namespace FileData.Interfaces
{    
    // parses argument array into FileInformationArguments
    public interface IArgumentParser
    {
        MethodResult<FileInformationArguments> Parse(string[] args);
    }
}
