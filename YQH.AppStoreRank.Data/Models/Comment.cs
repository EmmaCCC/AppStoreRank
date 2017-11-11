using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YQH.AppStoreRank.Data.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
