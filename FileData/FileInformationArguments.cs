namespace FileData
{
    public enum OperationParameters
    {
        Version = 1,
        Size,
    }

    public class FileInformationArguments
    {
        public OperationParameters Operation { get; set; }

        public string FileName { get; set; }        
    }
}
