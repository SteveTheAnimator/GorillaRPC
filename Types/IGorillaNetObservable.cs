using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;

namespace GorillaRPC.Types
{
    /// <summary>
    /// Interface for objects that can be observed and synchronized over GorillaNet.
    /// </summary>
    public interface IGorillaNetObservable
    {
        /// <summary>
        /// Called to serialize the state of the object into a dictionary of custom properties.
        /// </summary>
        /// <param name="customPropsToSend"></param>
        void OnGorillaNetSerialize(Dictionary<string, object> customPropsToSend);

        /// <summary>
        /// Called to deserialize the state of the object from a dictionary of received properties.
        /// </summary>
        /// <param name="receivedProps"></param>
        void OnGorillaNetDeserialize(Dictionary<string, object> receivedProps);
    }
}
