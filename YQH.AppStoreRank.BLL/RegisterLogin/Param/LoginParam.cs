using System.ComponentModel.DataAnnotations;

namespace YQH.AppStoreRank.BLL.RegisterLogin.Param
{
    public class LoginParam
    {
        [MinLength(10,ErrorMessage ="必须大于10")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }

    }
}

