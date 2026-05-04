using DoctorIA.Models;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace DoctorIA.Validator;

public class PatientRequestValidator : AbstractValidator<PatientRequest>
{
    public PatientRequestValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100)
            .When(x => x.Age.HasValue);

        RuleFor(x => x.Gender)
            .Must(g => g == "Male" || g == "Female" || g == "Other")
            .When(x => !string.IsNullOrEmpty(x.Gender));

        RuleFor(x => x.BloodPressure)
            .Matches(@"^\d{2,3}/\d{2,3}$")
            .When(x => !string.IsNullOrEmpty(x.BloodPressure));

        RuleFor(x => x.HeartRate)
            .InclusiveBetween(0, 250)
            .When(x => x.HeartRate.HasValue);

        RuleFor(x => x.Temperature)
            .InclusiveBetween(30, 45)
            .When(x => x.Temperature.HasValue);

        RuleFor(x => x.Symptoms)
            .Must(list => list == null || list.All(s => !string.IsNullOrWhiteSpace(s)))
            .WithMessage("Sintomas não podem conter valores vazios");

        RuleFor(x => x.ListHistoric)
            .Must(list => list == null || list.All(s => !string.IsNullOrWhiteSpace(s)))
            .WithMessage("Histórico inválido");
    }
}