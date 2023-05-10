namespace Hotel.DataAccess.Specifications;

internal interface IEntitySpecification<TEntity> where TEntity : class
{
    IEnumerable<string> IsNotValidBecause(TEntity entity);
    void ThrowErrorIfNotValid(TEntity entity);
}
