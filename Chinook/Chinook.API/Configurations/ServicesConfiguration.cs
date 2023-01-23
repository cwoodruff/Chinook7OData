using Chinook.Data.Repositories;
using Chinook.Domain.ApiModels;
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
            .AddTransient<IValidator<AlbumApiModel>, AlbumValidator>()
            .AddTransient<IValidator<ArtistApiModel>, ArtistValidator>()
            .AddTransient<IValidator<CustomerApiModel>, CustomerValidator>()
            .AddTransient<IValidator<EmployeeApiModel>, EmployeeValidator>()
            .AddTransient<IValidator<GenreApiModel>, GenreValidator>()
            .AddTransient<IValidator<InvoiceApiModel>, InvoiceValidator>()
            .AddTransient<IValidator<InvoiceLineApiModel>, InvoiceLineValidator>()
            .AddTransient<IValidator<MediaTypeApiModel>, MediaTypeValidator>()
            .AddTransient<IValidator<PlaylistApiModel>, PlaylistValidator>()
            .AddTransient<IValidator<TrackApiModel>, TrackValidator>();
    }
    
    public static void AddCaching(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddResponseCaching();
        services.AddMemoryCache();
    }
}