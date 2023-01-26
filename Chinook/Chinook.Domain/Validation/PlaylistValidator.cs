using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation;

public class PlaylistValidator : AbstractValidator<Playlist>
{
    public PlaylistValidator()
    {
        RuleFor(p => p.Name).NotNull();
        RuleFor(p => p.Name).MaximumLength(120);
    }
}