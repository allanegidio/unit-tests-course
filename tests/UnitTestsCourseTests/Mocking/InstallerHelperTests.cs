using System;
using System.Net;
using FluentAssertions;
using Moq;
using UnitTestsCourse.Mocking;
using Xunit;

namespace UnitTestsCourseTests.Mocking
{
    public class InstallerHelperTests
    {
        [Fact]
        public void DownloadInstaller_AllDownloadFile_ReturnsTrue()
        {
            var fileDownloader = new Mock<IFileDownloader>();

            var service = new InstallerHelper(fileDownloader.Object);
            var result = service.DownloadInstaller("customer", "installer");

            result.Should().BeTrue("Because file was downloader with success");
        }

        [Fact]
        public void DownloadInstaller_AllFailsDownloadFile_ReturnsFalse()
        {
            var fileDownloader = new Mock<IFileDownloader>();
            fileDownloader.Setup(fd =>
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new WebException());

            var service = new InstallerHelper(fileDownloader.Object);
            var result = service.DownloadInstaller("customer", "installer");

            result.Should().BeFalse("Because file was downloader with error");
        }
    }
}