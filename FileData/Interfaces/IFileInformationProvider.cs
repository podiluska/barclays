namespace FileData.Interfaces
{
    // Gets information about a file
    public interface IFileInformationProvider
    {
        string GetVersion(string fileName);

        int GetSize(string fileName);
    }
}
