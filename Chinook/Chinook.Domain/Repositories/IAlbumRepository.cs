using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;

namespace Chinook.Domain.Repositories;

public interface IAlbumRepository : IRepository<Album>, IDisposable
{
    Task<List<Album>> GetByArtistId(int id);
}