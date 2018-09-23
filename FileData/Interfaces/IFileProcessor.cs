namespace FileData.Interfaces
{
/*
 
·	Takes in two arguments (argument 1 = functionality to perform, argument 2 = filename)
·	If the first argument is anyone of –v, --v, /v, --version then return the version of the file (use FileDetails.Version to get the version number, don’t worry about accessing the file or checking if it exists etc.)
·	If the first argument is anyone of –s, --s, /s, --size the return the size of the file (use FileDetails.Size)
 
*/
    interface IFileProcessor
    {
        void GetInformation(string[] args);
    }
}
