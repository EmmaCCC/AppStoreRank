namespace YQH.AppStoreRank.BLL.Auth
{
    public class TokenModel
    {
        public string Id
        {
            get;
            set;
        }
        public string Token
        {
            get;
            set;
        }

        public string RefreshToken
        {
            get;
            set;
        }

        public int Expires
        {
            get;
            set;
        }
    }
}
