using FluentValidation;
using FluentValidation.Validators;

namespace Crystalis.Validators.Custom;

public class ExistsValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly string _column;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _table;

    public ExistsValidator (string Table, string Column, IServiceProvider serviceProvider)
    {
        _table = Table;
        _column = Column;
        _serviceProvider = serviceProvider;
    }

    public override string Name => "ExistsValidator";

    public override bool IsValid (ValidationContext<T> context, TProperty value)
    {
        context.MessageFormatter.AppendArgument("Column", _column);
        context.MessageFormatter.AppendArgument("Table", _table);
        context.MessageFormatter.AppendArgument("Value", value);

        UniqueValidator<T, TProperty> validator = new(_table, _column, _serviceProvider);
        return !validator.IsValid(context, value);
    }

    protected override string GetDefaultMessageTemplate (string errorCode) => "{Value} does not exist in column {Column} of table {Table}.";
}