using GorillaRPC.Types;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GorillaRPC.Examples
{
    public class Example1 : GorillaRPCBehaviour
    {
        [GorillaRPC("PrintMessage", 101)]
        private void PrintMessage(string message)
        {
            Debug.Log($"[Remote RPC] Message: {message}");
        }

        public void Send()
        {
            Call(nameof(PrintMessage), new object[] { "Hello, World!" }, this, 101, Photon.Realtime.ReceiverGroup.All);
        }
    }
}
