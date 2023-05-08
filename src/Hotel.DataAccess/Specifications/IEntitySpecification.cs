using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DataAccess.Specifications
{
    internal interface IEntitySpecification<TEntity> where TEntity : class
    {
        IEnumerable<string> IsNotValidBecause(TEntity entity);
        void ThrowErrorIfNotValid(TEntity entity);
    }
}
