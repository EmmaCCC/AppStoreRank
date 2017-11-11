using System;
using System.Collections.Generic;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Auth
{
    public class Identity : IIdentity
    {
        Account _user;
        public Identity(Account user)
        {
            _user = user;
        }

        public Identity()
        {
            _user = new Account();
        }

        public IDictionary<string, object> GetTokenData()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Type", _user.Type);
            dic.Add("UserName", _user.UserName);
            return dic;
        }

        public string GetName()
        {
            return _user.UserName;
        }

        public string GetId()
        {
            return _user.Id.ToString();
        }

        public string[] GetRoles()
        {
            return new string[] { ((int)_user.Type).ToString() };
        }


        public string GetRandomCode()
        {
            return Guid.NewGuid().ToString("N");
        }

        public object GetUser()
        {
            return _user;
        }


    }
}
