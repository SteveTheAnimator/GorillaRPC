using System;

namespace GorillaRPC.Types
{
    /// <summary>
    /// Attribute to mark methods for Gorilla RPC (Remote Procedure Call) functionality.
    /// </summary>
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