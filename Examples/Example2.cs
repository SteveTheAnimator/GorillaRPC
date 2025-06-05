using ExitGames.Client.Photon;
using GorillaRPC.Types;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace GorillaRPC.Examples
{
    public class Example2 : MonoBehaviourPunCallbacks, IGorillaNetObservable
    {
        public int health;

        public void OnGorillaNetSerialize(Dictionary<string, object> customPropsToSend)
        {
            customPropsToSend["health"] = health;
        }

        public void OnGorillaNetDeserialize(Dictionary<string, object> receivedProps)
        {
            if (receivedProps.TryGetValue("health", out var h)) health = (int)h;
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            GorillaNetSyncHandler.Deserialize(this, ref changedProps);
        }

        public void PushSync()
        {
            Hashtable hash = new Hashtable();
            GorillaNetSyncHandler.Sync(this, ref hash);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
}
