using Chinook.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<MediaType>> GetAllMediaType()
    {
        var mediaTypes = await _mediaTypeRepository.GetAll();
        foreach (var mediaType in mediaTypes)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("MediaType-", mediaType.Id), mediaType, (TimeSpan)cacheEntryOptions);
        }
        return mediaTypes;
    }

    public async Task<MediaType?> GetMediaTypeById(int id)
    {
        var mediaTypeCached = _cache.Get<MediaType>(string.Concat("MediaType-", id));

        if (mediaTypeCached != null)
        {
            return mediaTypeCached;
        }
        else
        {
            var mediaType = await _mediaTypeRepository.GetById(id);
            if (mediaType == null) return null;
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            _cache.Set(string.Concat("MediaType-", mediaType.Id), mediaType,
                (TimeSpan)cacheEntryOptions);
            return mediaType;
        }
    }

    public async Task<MediaType> AddMediaType(MediaType newMediaType)
    {
        await _mediaTypeValidator.ValidateAndThrowAsync(newMediaType);
        return await _mediaTypeRepository.Add(newMediaType);
    }

    public async Task<bool> UpdateMediaType(MediaType mediaType)
    {
        await _mediaTypeValidator.ValidateAndThrowAsync(mediaType);
        return await _mediaTypeRepository.Update(mediaType);
    }

    public Task<bool> DeleteMediaType(int id) => _mediaTypeRepository.Delete(id);
}