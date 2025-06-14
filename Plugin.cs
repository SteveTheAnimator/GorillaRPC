﻿using System;
using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

namespace GorillaRPC
{
    /// <summary>
    /// GorillaRPC Plugin
    /// </summary>
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        #region Test Booleans  
        public bool TestExample1 = false;
        public bool TestExample2 = false;
        #endregion

        public void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameLoaded);
            NetworkSystem.Instance.OnJoinedRoomEvent += OnJoinedRoom;
            NetworkSystem.Instance.OnReturnedToSinglePlayer += OnLeftRoom;
        }

        public void OnGameLoaded()
        {
            Debug.Log("[GorillaRPC] Game loaded successfully. Initializing RPC system.");
        }

        public void OnJoinedRoom()
        {
            Debug.Log("[GorillaRPC] Joined room successfully.");

            #region Examples
            #region Example 1
            if (TestExample1)
            {
                try
                {
                    var example1 = new GameObject("Example1").AddComponent<Examples.Example1>();
                    example1.Send();
                    Debug.Log("[GorillaRPC] Example1 component added successfully.");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[GorillaRPC] Failed to add Example1 component: {ex.Message}");
                }
            }
            #endregion
            #region Example 2
            if (TestExample2)
            {
                try
                {
                    var example2 = new GameObject("Example2").AddComponent<Examples.Example2>();
                    example2.health = 100;    
                    example2.PushSync();

                    Debug.Log("[GorillaRPC] Example2 component added successfully.");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[GorillaRPC] Failed to add Example2 component: {ex.Message}");
                }
            }
            #endregion
            #endregion
        }

        public void OnLeftRoom()
        {
            Debug.Log("[GorillaRPC] Left room successfully.");
        }
    }

    public class PluginInfo
    {
        internal const string
            GUID = "Steve.GorillaRPC",      
            Name = "GorillaRPC",      
            Version = "1.0.0";      
    }
}
