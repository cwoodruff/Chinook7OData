using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Genre>> GetAllGenre()
    {
        var genres = await _genreRepository.GetAll();
        foreach (var genre in genres)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Genre-", genre.Id), genre, (TimeSpan)cacheEntryOptions);
        }
        return genres;
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        var genreCached = _cache.Get<Genre>(string.Concat("Genre-", id));

        if (genreCached != null)
        {
            return genreCached;
        }
        else
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null) return null;
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            _cache.Set(string.Concat("Genre-", genre.Id), genre, (TimeSpan)cacheEntryOptions);
            return genre;
        }
    }

    public async Task<Genre> AddGenre(Genre newGenre)
    {
        await _genreValidator.ValidateAndThrowAsync(newGenre);
        return await _genreRepository.Add(newGenre);
    }

    public async Task<bool> UpdateGenre(Genre genre)
    {
        await _genreValidator.ValidateAndThrowAsync(genre);
        return await _genreRepository.Update(genre);
    }

    public Task<bool> DeleteGenre(int id) => _genreRepository.Delete(id);
}