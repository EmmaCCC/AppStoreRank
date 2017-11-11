using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Web;

namespace YQH.AppStoreRank.Data
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {

            DbContext dbContext = HttpContext.Current.Items["DbContext"] as DbContext;

            if (dbContext == null)
            {
                dbContext = new AppStoreRankContext();
                HttpContext.Current.Items["DbContext"] = dbContext;
            }

            return dbContext;
        }
    }
}
