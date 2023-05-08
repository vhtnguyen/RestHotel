using System.Linq.Expressions;
using System.Reflection;

namespace Hotel.Shared.Helpers;

public static class EntityHepler
{
    public static TEntity BindId<TEntity>(this TEntity model, Expression<Func<TEntity, Guid>> predicate)
        => model.Bind(predicate, Guid.NewGuid());   

    public static TEntity Bind<TEntity, TValue>(this TEntity model, Expression<Func<TEntity, TValue>> predicate, TValue value)
    {
        var memberExpression = predicate.Body as MemberExpression;
        if (memberExpression == null)
        {
            memberExpression = ((UnaryExpression)predicate.Body).Operand as MemberExpression;
        }

        var property = memberExpression?.Member.Name.ToLowerInvariant();
        var field = model?.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public)
            .SingleOrDefault(
                field => field.Name.ToLowerInvariant().StartsWith($"<{property}>"));

        if (field == null)
        {
            return model;
        }

        field.SetValue(model, value);
        return model;
    }
}
