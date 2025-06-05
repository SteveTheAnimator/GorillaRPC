using System;

namespace GorillaRPC.Types
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GorillaRPCAttribute : Attribute
    {
        public string MethodName { get; }
        public byte RpcEventCode { get; }

        public GorillaRPCAttribute(string methodName, byte rpcEventCode)
        {
            MethodName = methodName;
            RpcEventCode = rpcEventCode;
        }
    }
}