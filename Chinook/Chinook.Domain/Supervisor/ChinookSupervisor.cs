using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor;

public partial class ChinookSupervisor : IChinookSupervisor
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IInvoiceLineRepository _invoiceLineRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMediaTypeRepository _mediaTypeRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ITrackRepository _trackRepository;

    private readonly IValidator<Album> _albumValidator;
    private readonly IValidator<Artist> _artistValidator;
    private readonly IValidator<Customer> _customerValidator;
    private readonly IValidator<Employee> _employeeValidator;
    private readonly IValidator<Genre> _genreValidator;
    private readonly IValidator<Invoice> _invoiceValidator;
    private readonly IValidator<InvoiceLine> _invoiceLineValidator;
    private readonly IValidator<MediaType> _mediaTypeValidator;
    private readonly IValidator<Playlist> _playlistValidator;
    private readonly IValidator<Track> _trackValidator;

    private readonly IMemoryCache _cache;

    public ChinookSupervisor(IAlbumRepository albumRepository,
        IArtistRepository artistRepository,
        ICustomerRepository customerRepository,
        IEmployeeRepository employeeRepository,
        IGenreRepository genreRepository,
        IInvoiceLineRepository invoiceLineRepository,
        IInvoiceRepository invoiceRepository,
        IMediaTypeRepository mediaTypeRepository,
        IPlaylistRepository playlistRepository,
        ITrackRepository trackRepository,
        IValidator<Album> albumValidator,
        IValidator<Artist> artistValidator,
        IValidator<Customer> customerValidator,
        IValidator<Employee> employeeValidator,
        IValidator<Genre> genreValidator,
        IValidator<Invoice> invoiceValidator,
        IValidator<InvoiceLine> invoiceLineValidator,
        IValidator<MediaType> mediaTypeValidator,
        IValidator<Playlist> playlistValidator,
        IValidator<Track> trackValidator,
        IMemoryCache memoryCache
    )
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _customerRepository = customerRepository;
        _employeeRepository = employeeRepository;
        _genreRepository = genreRepository;
        _invoiceLineRepository = invoiceLineRepository;
        _invoiceRepository = invoiceRepository;
        _mediaTypeRepository = mediaTypeRepository;
        _playlistRepository = playlistRepository;
        _trackRepository = trackRepository;

        _albumValidator = albumValidator;
        _artistValidator = artistValidator;
        _customerValidator = customerValidator;
        _employeeValidator = employeeValidator;
        _genreValidator = genreValidator;
        _invoiceValidator = invoiceValidator;
        _invoiceLineValidator = invoiceLineValidator;
        _mediaTypeValidator = mediaTypeValidator;
        _playlistValidator = playlistValidator;
        _trackValidator = trackValidator;

        _cache = memoryCache;
    }
}