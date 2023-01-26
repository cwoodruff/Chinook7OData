using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation;

public class AlbumValidator : AbstractValidator<Album>
{
    public AlbumValidator()
    {
        RuleFor(a => a.Title).NotNull();
        RuleFor(a => a.Title).MinimumLength(3);
        RuleFor(a => a.Title).MaximumLength(160);
        RuleFor(a => a.ArtistId).NotNull();
    }
}