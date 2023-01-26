using Chinook.Data.Repositories;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Chinook.Domain.Supervisor;
using Chinook.Domain.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Chinook.API.Configurations;

public static class ServicesConfiguration
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAlbumRepository, AlbumRepository>()
            .AddScoped<IArtistRepository, ArtistRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddScoped<IInvoiceRepository, InvoiceRepository>()
            .AddScoped<IInvoiceLineRepository, InvoiceLineRepository>()
            .AddScoped<IMediaTypeRepository, MediaTypeRepository>()
            .AddScoped<IPlaylistRepository, PlaylistRepository>()
            .AddScoped<ITrackRepository, TrackRepository>();
    }

    public static void ConfigureSupervisor(this IServiceCollection services)
    {
        services.AddScoped<IChinookSupervisor, ChinookSupervisor>();
    }
    
    public static void ConfigureValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddTransient<IValidator<Album>, AlbumValidator>()
            .AddTransient<IValidator<Artist>, ArtistValidator>()
            .AddTransient<IValidator<Customer>, CustomerValidator>()
            .AddTransient<IValidator<Employee>, EmployeeValidator>()
            .AddTransient<IValidator<Genre>, GenreValidator>()
            .AddTransient<IValidator<Invoice>, InvoiceValidator>()
            .AddTransient<IValidator<InvoiceLine>, InvoiceLineValidator>()
            .AddTransient<IValidator<MediaType>, MediaTypeValidator>()
            .AddTransient<IValidator<Playlist>, PlaylistValidator>()
            .AddTransient<IValidator<Track>, TrackValidator>();
    }
    
    public static void AddCaching(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddResponseCaching();
        services.AddMemoryCache();
    }
}