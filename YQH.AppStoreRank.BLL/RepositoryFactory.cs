using YQH.AppStoreRank.Data;

namespace YQH.AppStoreRank.BLL
{
    public class RepositoryFactory
    {
        public static IBaseRepository GetDataAccess()
        {
            return new BaseRepository();
        }

    }
}
