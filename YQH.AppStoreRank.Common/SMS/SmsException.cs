using System;
using System.Runtime.Serialization;

namespace YQH.Tourism.Common.SMS
{
    /// <summary>
    /// TOP客户端异常。
    /// </summary>
    public class SmsException : Exception
    {
        private string errorCode;
        private string errorMsg;

        public SmsException()
            : base()
        {
        }

        public SmsException(string message)
            : base(message)
        {
        }

        protected SmsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SmsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SmsException(string errorCode, string errorMsg)
            : base(errorCode + ":" + errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public string ErrorCode
        {
            get { return this.errorCode; }
        }

        public string ErrorMsg
        {
            get { return this.errorMsg; }
        }
    }
}
