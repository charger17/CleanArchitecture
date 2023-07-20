﻿using AutoFixture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRespository()
        {
            var fixture = new Fixture();
            var videos = fixture.CreateMany<Video>().ToList();

            var mockRepository = new Mock<IVideoRepository>();
            mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(videos);
            return mockRepository;
        }
    }
}
