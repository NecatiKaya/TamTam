using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.VideoService.Validation
{
    public class VideSearchRequestValidator : AbstractValidator<VideoSearchRequest>
    {
        public VideSearchRequestValidator()
        {
            RuleFor(searchRequest => searchRequest.MaxResult)
              .LessThanOrEqualTo(1000)
              .WithMessage("Max result is 1000. Specify lower than 1000.");

            RuleFor(searchRequest => searchRequest.SearchQuery)
              .NotEmpty()
              .WithMessage("Search query must contain at least one charachter.");

            RuleFor(searchRequest => searchRequest.VerificationToken)
               .NotEmpty()
              .WithMessage("Please provide verification token.");
        }
    }
}
