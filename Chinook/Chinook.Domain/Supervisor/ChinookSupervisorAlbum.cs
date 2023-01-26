using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Album>> GetAllAlbum() // todo
    {
        var albums = await _albumRepository.GetAll();
        foreach (var album in albums)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Album-", album.Id), album, (TimeSpan)cacheEntryOptions);
        }
        return albums;
    }

    public async Task<Album?> GetAlbumById(int id)
    {
        var albumCached = _cache.Get<Album>(string.Concat("Album-", id));

        if (albumCached != null)
        {
            return albumCached;
        }
        else
        {
            var album = await _albumRepository.GetById(id);
            if (album == null) return null;

            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Album-", album.Id), album, (TimeSpan)cacheEntryOptions);
            return album;
        }
    }

    public async Task<List<Album>> GetAlbumByArtistId(int id) => await _albumRepository.GetByArtistId(id);

    public async Task<Album> AddAlbum(Album newAlbum)
    {
        await _albumValidator.ValidateAndThrowAsync(newAlbum);

        return await _albumRepository.Add(newAlbum);
    }

    public async Task<bool> UpdateAlbum(Album album)
    {
        await _albumValidator.ValidateAndThrowAsync(album);
        return await _albumRepository.Update(album);
    }

    public Task<bool> DeleteAlbum(int id) => _albumRepository.Delete(id);
}