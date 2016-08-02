using System;
using System.Collections.Generic;
using System.Data.Entity;
using Framework.Data.EntityFramework.Helpers;
using BaseProject.Data.Interfaces;
using BaseProject.Data.Repository;

namespace BaseProject.Data.Helpers
{
    public class BaseProjectRepositoryFactories : RepositoryFactories
    {
        protected override IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
             {
                  //{typeof(IDemoRepository), dbContext => new DemoRepository(dbContext)}
              };
        }
    }
}
