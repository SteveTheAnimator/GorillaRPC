using System;
using System.Collections.Generic;
using System.Reflection;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

namespace GorillaRPC.Types
{
    public abstract class GorillaRPCBehaviour : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        private static readonly Dictionary<byte, Dictionary<string, MethodInfo>> _eventCodeToMethodMap = new Dictionary<byte, Dictionary<string, MethodInfo>>();
        private static readonly Dictionary<byte, Dictionary<string, MonoBehaviour>> _eventCodeToTargetInstances = new Dictionary<byte, Dictionary<string, MonoBehaviour>>();
        public static readonly List<int> RestrictedRPCEventCodes = new List<int>() { 0, 1, 2, 3, 4, 5, 8, 9, 50, 51, 176, 199 };

        protected virtual void Awake()
        {
            RegisterRPCMethods();
        }

        private void RegisterRPCMethods()
        {
            var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<GorillaRPCAttribute>();
                if (attr != null)
                {
                    byte eventCode = !RestrictedRPCEventCodes.Contains(attr.RpcEventCode) ? attr.RpcEventCode : throw new Exception($"RPC event code {attr.RpcEventCode} is already being used.");
                    string methodName = attr.MethodName;

                    if (!_eventCodeToMethodMap.ContainsKey(eventCode))
                    {
                        _eventCodeToMethodMap[eventCode] = new Dictionary<string, MethodInfo>();
                        _eventCodeToTargetInstances[eventCode] = new Dictionary<string, MonoBehaviour>();
                    }

                    if (_eventCodeToMethodMap[eventCode].ContainsKey(methodName))
                        throw new Exception($"Duplicate GorillaRPC method name: {methodName} for event code: {eventCode}");

                    _eventCodeToMethodMap[eventCode][methodName] = method;
                    _eventCodeToTargetInstances[eventCode][methodName] = this;
                }
            }
        }

        public static void Call(string methodName, object[] args, GorillaRPCBehaviour behaviour, byte rpcEventCode, ReceiverGroup receivers = ReceiverGroup.Others)
        {
            object[] payload = new object[] { methodName, args };
            RaiseEventOptions options = new RaiseEventOptions() { Receivers = receivers };
            SendOptions sendOptions = new SendOptions() { Reliability = true };

            PhotonNetwork.RaiseEvent(rpcEventCode, payload, options, sendOptions);
        }

        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            if (!_eventCodeToMethodMap.ContainsKey(eventCode)) return;

            var data = (object[])photonEvent.CustomData;
            string methodName = data[0] as string;
            object[] args = data[1] as object[];

            if (_eventCodeToMethodMap[eventCode].TryGetValue(methodName, out var method))
            {
                var target = _eventCodeToTargetInstances[eventCode][methodName];
                method.Invoke(target, args);
            }
            else
            {
                Debug.LogWarning($"[GorillaRPC] Unknown method '{methodName}' for event code {eventCode}");
            }
        }

        private void OnEnable() => PhotonNetwork.AddCallbackTarget(this);
        private void OnDisable() => PhotonNetwork.RemoveCallbackTarget(this);
    }
}