using FluentValidation;

namespace Application.Features.Report.Send;

public class SendReportCommandValidator : AbstractValidator<SendReportCommand>
{
    public SendReportCommandValidator()
    {
        RuleFor(c => c.Dto)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(c => c.Dto.Reason)
                    .IsInEnum().WithMessage("Невірний формат причини для скарги")
                    .When(c => c.Dto.Reason.HasValue);

                RuleFor(c => c.Dto.CustomReason)
                    .MaximumLength(255).WithMessage("Інша причина не може перевищувати 255 символів")
                    .When(c => !string.IsNullOrWhiteSpace(c.Dto.CustomReason))
                    .WithMessage("Інша причина не може бути пустою");

                RuleFor(c => c.Dto)
                    .Must(dto => dto.Reason.HasValue || !string.IsNullOrWhiteSpace(dto.CustomReason))
                    .WithMessage("Потрібно вказати хоть одну причину для скарги");

                RuleFor(c => c.Dto)
                    .Must(dto => !(dto.Reason.HasValue && !string.IsNullOrWhiteSpace(dto.CustomReason)))
                    .WithMessage("Можна лише одну причину скарги");
            });
    }
}