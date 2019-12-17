using Abp;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Http.Client
{
    [Serializable]
    public class AbpRemoteCallException : AbpException
    {
        public string Code => Error?.Code;

        public string Details => Error?.Details;

        public RemoteServiceErrorInfo Error { get; set; }

        public AbpRemoteCallException()
        {

        }

        public AbpRemoteCallException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public AbpRemoteCallException(RemoteServiceErrorInfo error)
            : base(error.Message)
        {
            Error = error;
        }
    }
}
