using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Artist>> GetAllArtist()
    {
        var artists = await _artistRepository.GetAll();
        
        foreach (var artist in artists)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Artist-", artist.Id), artist, (TimeSpan)cacheEntryOptions);
        }
        return artists;
    }

    public async Task<Artist> GetArtistById(int id)
    {
        var artistCached = _cache.Get<Artist>(string.Concat("Artist-", id));

        if (artistCached != null)
        {
            return artistCached;
        }
        else
        {
            var artist = await _artistRepository.GetById(id);
            if (artist == null) return null!;

            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Artist-", artist.Id), artist, (TimeSpan)cacheEntryOptions);

            return artist;
        }
    }

    public async Task<Artist> AddArtist(Artist newArtist)
    {
        await _artistValidator.ValidateAndThrowAsync(newArtist);
        return await _artistRepository.Add(newArtist);
    }

    public async Task<bool> UpdateArtist(Artist artist)
    {
        await _artistValidator.ValidateAndThrowAsync(artist);
        return await _artistRepository.Update(artist);
    }

    public Task<bool> DeleteArtist(int id) => _artistRepository.Delete(id);
}