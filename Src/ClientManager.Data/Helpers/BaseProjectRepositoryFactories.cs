using System;
using System.Collections.Generic;
using System.Data.Entity;
using Framework.Data.EntityFramework.Helpers;
using ClientManager.Data.Interfaces;
using ClientManager.Data.Repository;

namespace ClientManager.Data.Helpers
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
