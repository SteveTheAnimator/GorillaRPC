using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace GorillaRPC.Types
{
    public static class GorillaNetSyncHandler
    {
        public static void Sync(IGorillaNetObservable observable, Hashtable photonCustomProps)
        {
            var customPropsToSend = new Dictionary<string, object>();
            observable.OnGorillaNetSerialize(customPropsToSend);

            foreach (var kvp in customPropsToSend)
            {
                photonCustomProps[kvp.Key] = kvp.Value;
            }
        }

        public static void Deserialize(IGorillaNetObservable observable, Hashtable photonCustomProps)
        {
            var receivedProps = new Dictionary<string, object>();

            foreach (DictionaryEntry entry in photonCustomProps)
            {
                receivedProps[entry.Key.ToString()] = entry.Value;
            }

            observable.OnGorillaNetDeserialize(receivedProps);
        }
    }
}
