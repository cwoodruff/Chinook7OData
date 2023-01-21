using Chinook.Domain.Entities;
using Chinook.Domain.Extensions;

namespace Chinook.Domain.Repositories;

public interface IPlaylistRepository : IRepository<Playlist>, IDisposable
{
    Task<List<Playlist>> GetByTrackId(int id);
}