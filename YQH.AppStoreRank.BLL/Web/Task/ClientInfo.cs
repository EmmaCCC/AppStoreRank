using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YQH.AppStoreRank.BLL.Web.Task
{
    public class ClientInfo
    {
        public Guid UserId { get; set; }

        public string IDFA { get; set; }

        public string IpAddress { get; set; }

    }
}
