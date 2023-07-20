using CleanArchitecture.Application.Contracts.Persistence;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockVideoRespository = MockVideoRepository.GetVideoRespository();

            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRespository.Object);

            return mockUnitOfWork;
        }
    }
}
