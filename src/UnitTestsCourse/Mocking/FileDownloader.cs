using System.Net;

namespace UnitTestsCourse.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }

    public class FileDownloader : IFileDownloader
    {
        public WebClient _webClient;

        public FileDownloader(WebClient webClient = null)
        {
            _webClient = webClient ?? new WebClient();
        }

        public void DownloadFile(string url, string path)
        {
            _webClient.DownloadFile(url, path);
        }

    }
}