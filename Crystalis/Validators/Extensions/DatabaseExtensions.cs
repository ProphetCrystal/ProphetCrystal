using Crystalis.Validators.Custom;
using FluentValidation;

namespace PaymentsService.Validators.Extensions;

public static class DatabaseExtensions
{
    public static IRuleBuilderOptions<T, TProperty> Unique<T, TProperty> (this IRuleBuilder<T, TProperty> ruleBuilder, string Table, string Column, IServiceProvider serviceProvider, string? ignoredId = null, string ignoredColumn = "Id") => ruleBuilder.SetValidator(new UniqueValidator<T, TProperty>(Table, Column, serviceProvider, ignoredId, ignoredColumn));
    public static IRuleBuilderOptions<T, TProperty> Exists<T, TProperty> (this IRuleBuilder<T, TProperty> ruleBuilder, string Table, string Column, IServiceProvider serviceProvider) => ruleBuilder.SetValidator(new ExistsValidator<T, TProperty>(Table, Column, serviceProvider));
}