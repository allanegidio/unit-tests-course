using System.Collections.Generic;
using FluentAssertions;
using Moq;
using UnitTestsCourse.Mocking;
using Xunit;

namespace UnitTestsCourseTests.Mocking
{
    public static class MockFakeData
    {
        public static List<Video> FakeVideoList()
        {
            var list = new List<Video>();

            list.Add(new Video() { Id = 1, IsProcessed = true, Title = "Video Processado" });
            list.Add(new Video() { Id = 2, IsProcessed = false, Title = "Video Cagado" });
            list.Add(new Video() { Id = 3, IsProcessed = false, Title = "Video Fudido" });
            list.Add(new Video() { Id = 4, IsProcessed = true, Title = "Video Processado de Boa" });

            return list;
        }
    }


    public class VideoServiceTests
    {
        [Fact]
        public void GetUnprocessedVideosAsCsv_WhenUnprocessedVideosIsEmpty_ReturnEmptyString()
        {
            var videoRepositoryMock = new Mock<IVideoRepository>();
            var fileReaderMock = new Mock<IFileReader>();

            videoRepositoryMock.Setup(rep => rep.GetUnprocessedVideos()).Returns(new List<Video>());

            var service = new VideoService(fileReaderMock.Object, videoRepositoryMock.Object);
            var result = service.GetUnprocessedVideosAsCsv();

            result.Should().BeEmpty("Because no one item returned from repository");
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_WhenHaveSomeItemsUnprocessed_ReturnIdSeparatedByComma()
        {
            var videoRepositoryMock = new Mock<IVideoRepository>();
            var fileReaderMock = new Mock<IFileReader>();

            videoRepositoryMock.Setup(rep => rep.GetUnprocessedVideos()).Returns(MockFakeData.FakeVideoList());

            var service = new VideoService(fileReaderMock.Object, videoRepositoryMock.Object);
            var result = service.GetUnprocessedVideosAsCsv();

            result.Should().BeSameAs("2,3");
        }
    }
}