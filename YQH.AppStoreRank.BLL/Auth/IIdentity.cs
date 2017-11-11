using System.Collections.Generic;

namespace YQH.AppStoreRank.BLL.Auth
{
    public interface IIdentity
    {
        IDictionary<string, object> GetTokenData();

        string GetName();

        string GetId();

        string[] GetRoles();

        object GetUser();

    }
} 
