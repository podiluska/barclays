using FileData.Interfaces;
using ThirdPartyTools;

namespace FileData
{
    internal class ThirdPartyToolsWrapper : IFileInformationProvider
    {
        public string GetVersion(string fileName)
        {
            var thirdParty = new FileDetails();
            return thirdParty.Version(fileName);
        }

        public int GetSize(string fileName)
        {
            var thirdParty = new FileDetails();
            return thirdParty.Size(fileName);
        }
    }
}
