using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace GorillaRPC.Types
{
    /// <summary>
    /// Handles synchronization of IGorillaNetObservable objects with Photon custom properties.
    /// </summary>
    public static class GorillaNetSyncHandler
    {
        /// <summary>
        /// Synchronizes the state of an IGorillaNetObservable object with Photon custom properties.
        /// </summary>
        /// <param name="observable"></param>
        /// <param name="photonCustomProps"></param>
        public static void Sync(IGorillaNetObservable observable, ref Hashtable photonCustomProps)
        {
            var customPropsToSend = new Dictionary<string, object>();
            observable.OnGorillaNetSerialize(customPropsToSend);

            foreach (var kvp in customPropsToSend)
            {
                photonCustomProps[kvp.Key] = kvp.Value;
            }
        }
        /// <summary>
        /// Deserializes the state of an IGorillaNetObservable object from Photon custom properties.
        /// </summary>
        /// <param name="observable"></param>
        /// <param name="photonCustomProps"></param>
        public static void Deserialize(IGorillaNetObservable observable, ref Hashtable photonCustomProps)
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
