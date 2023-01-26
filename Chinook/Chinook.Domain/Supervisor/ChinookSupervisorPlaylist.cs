using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Playlist>> GetAllPlaylist()
    {
        var playlists = await _playlistRepository.GetAll();
        foreach (var playList in playlists)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Playlist-", playList.Id), playList, (TimeSpan)cacheEntryOptions);
        }
        return playlists;
    }

    public async Task<Playlist> GetPlaylistById(int id)
    {
        var playListCached = _cache.Get<Playlist>(string.Concat("Playlist-", id));

        if (playListCached != null)
        {
            return playListCached;
        }
        else
        {
            var playlist = await _playlistRepository.GetById(id);
            if (playlist == null) return null!;
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            _cache.Set(string.Concat("Playlist-", playlist.Id), playlist,
                (TimeSpan)cacheEntryOptions);
            return playlist;
        }
    }

    public async Task<Playlist> AddPlaylist(Playlist newPlaylist)
    {
        await _playlistValidator.ValidateAndThrowAsync(newPlaylist);
        return await _playlistRepository.Add(newPlaylist);
    }

    public async Task<bool> UpdatePlaylist(Playlist playlist)
    {
        await _playlistValidator.ValidateAndThrowAsync(playlist);
        return await _playlistRepository.Update(playlist);
    }

    public Task<bool> DeletePlaylist(int id)
        => _playlistRepository.Delete(id);
}