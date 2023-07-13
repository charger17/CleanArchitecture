using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<IEnumerable<Video>> GetVideoByNombre(string nombreVideo);


    }
}
