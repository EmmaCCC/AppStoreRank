using System;
using System.Collections.Generic;
using YQH.AppStoreRank.Data.Models;

namespace YQH.AppStoreRank.BLL.Auth
{
    public class Identity : IAuthIdentity
    {
        Account _user;
        private Dictionary<string, object> _dic = new Dictionary<string, object>();
        public Identity(Account user)
        {
            _user = user;
            InitData();
        }

        public Identity()
        {
            _user = new Account();
            InitData();
        }

        private void InitData()
        {
            _dic.Add("Type", _user.Type);
            _dic.Add("UserName", _user.UserName);

        }
        public IDictionary<string, object> GetTokenData()
        {
            return _dic;
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
