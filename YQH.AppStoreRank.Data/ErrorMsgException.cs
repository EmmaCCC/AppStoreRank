using System;

namespace YQH.AppStoreRank.Data
{
    public class ErrorMsgException : Exception
    {
        public int Status = 1;
        public ErrorMsgException(string msg):base(msg)
        {
            
        }
    }
}
