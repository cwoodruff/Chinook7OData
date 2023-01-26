using FluentValidation;
using Chinook.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor
{
    public async Task<List<Track>> GetAllTrack()
    {
        var tracks = await _trackRepository.GetAll();
        foreach (var track in tracks)
        {
            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Track-", track.Id), track, (TimeSpan)cacheEntryOptions);
        }
        return tracks;
    }

    public async Task<Track?> GetTrackById(int id)
    {
        var trackCached = _cache.Get<Track>(string.Concat("Track-", id));

        if (trackCached != null)
        {
            return trackCached;
        }
        else
        {
            var track = await _trackRepository.GetById(id);
            if (track == null) return null;

            var cacheEntryOptions =
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800))
                    .AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(604800);
            ;
            _cache.Set(string.Concat("Track-", track.Id), track, (TimeSpan)cacheEntryOptions);
            return track;
        }
    }

    public async Task<Track> AddTrack(Track newTrack)
    {
        await _trackValidator.ValidateAndThrowAsync(newTrack);
        return await _trackRepository.Add(newTrack);
    }

    public async Task<bool> UpdateTrack(Track track)
    {
        await _trackValidator.ValidateAndThrowAsync(track);
        return await _trackRepository.Update(track);
    }

    public Task<bool> DeleteTrack(int id) => _trackRepository.Delete(id);
    
    public async Task<List<Track>?> GetTrackByAlbumId(int id) => await _trackRepository.GetByAlbumId(id);

    public async Task<List<Track>> GetTrackByGenreId(int id) => await _trackRepository.GetByGenreId(id);

    public async Task<List<Track>> GetTrackByMediaTypeId(int id) => await _trackRepository.GetByMediaTypeId(id);

    public async Task<List<Track>> GetTrackByPlaylistId(int id) => await _trackRepository.GetByPlaylistId(id);

    public async Task<List<Track>> GetTrackByArtistId(int id) => await _trackRepository.GetByArtistId(id);

    public async Task<List<Track>> GetTrackByInvoiceId(int id) => await _trackRepository.GetByInvoiceId(id);
}