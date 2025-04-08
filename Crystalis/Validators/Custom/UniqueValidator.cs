using Crystalis.Contexts;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Crystalis.Validators.Custom;

public class UniqueValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly string _column;
    private readonly string _ignoredColumn;
    private readonly string? _ignoredId;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _table;

    public UniqueValidator (string Table, string Column, IServiceProvider serviceProvider, string? ignoredId = null,
        string ignoredColumn = "Id")
    {
        _table = Table;
        _column = Column;
        _serviceProvider = serviceProvider;
        _ignoredId = ignoredId;
        _ignoredColumn = ignoredColumn;
    }

    public override string Name => "UniqueValidator";

    public override bool IsValid (ValidationContext<T> context, TProperty value)
    {
        context.MessageFormatter.AppendArgument("Column", _column);
        context.MessageFormatter.AppendArgument("Table", _table);

        string AdditionalQueryString = "";
        if (_ignoredId is not null) {
            AdditionalQueryString = $" AND \"{_ignoredColumn}\" != \'{_ignoredId}\'";
        }

        using (IServiceScope serviceScope = _serviceProvider.CreateScope()) {
            try {
                IServiceProvider scopeServiceProvider = serviceScope.ServiceProvider;
                DataContext? dataContext = scopeServiceProvider.GetService<DataContext>();
                //TODO: Make it nicer somehow
                string query =
                    $"SELECT CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) FROM \"{_table}\" WHERE \"{_column}\"::text = \'{value}\'" +
                    AdditionalQueryString;
                if (dataContext != null) {
                    IQueryable<bool> result = dataContext.Database.SqlQueryRaw<bool>(query);
                    return !result.AsEnumerable().First();
                }

                return false;
            }
            catch (Exception) {
                return false;
            }
        }
    }

    protected override string GetDefaultMessageTemplate (string errorCode) =>
        "{PropertyName} exists column in {Column} of table {Table}.";
}