using Chinook.API.Configurations;
using Chinook.Data;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConnectionProvider(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureSupervisor();
builder.Services.ConfigureValidators();
builder.Services.AddCaching(builder.Configuration);

builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(3);
    options.AddRouteComponents(
        "odata",
        GetEdmModel());
    //options.RouteOptions.EnableControllerNameCaseInsensitive = true;
});

var app = builder.Build();

app.UseODataRouteDebug();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Album>("Albums");
    builder.EntitySet<Artist>("Artists");
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<Employee>("Employees");
    builder.EntitySet<Genre>("Genres");
    builder.EntitySet<Invoice>("Invoices");
    builder.EntitySet<InvoiceLine>("InvoiceLines");
    builder.EntitySet<MediaType>("MediaTypes");
    builder.EntitySet<Playlist>("Playlists");
    builder.EntitySet<Track>("Tracks");
    var model = builder.GetEdmModel();
    return model;
}