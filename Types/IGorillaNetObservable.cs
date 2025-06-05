using System.Collections.Generic;

namespace GorillaRPC.Types
{
    public interface IGorillaNetObservable
    {
        void OnGorillaNetSerialize(Dictionary<string, object> customPropsToSend);
        void OnGorillaNetDeserialize(Dictionary<string, object> receivedProps);
    }
}
