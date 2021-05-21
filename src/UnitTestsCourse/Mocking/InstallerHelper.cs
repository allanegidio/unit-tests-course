using System.Net;

namespace UnitTestsCourse.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;

        public bool DownloadInstaller(string customerName, string installerName)
        {
            var client = new WebClient();

            try
            {
                client.DownloadFile($"http://example.com/{0}/{1}", _setupDestinationFile);

                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }
    }
}