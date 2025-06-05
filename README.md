# GorillaRPC
## A gorilla tag library that lets you easily create RPCs without the downsides of bans

## Installation
 
1. Reference the **GorillaRPC** mod (`GorillaRPC.dll`) in your mod.
2. Make sure this is above the **BepInPlugin** attribute:
```cs
[BepInDependency("Steve.GorillaRPC")]
```

---

## Usage Guide

### Step 1: Create Your RPC Handler

```cs
using GorillaRPC.Types;
using UnityEngine;

public class YourRPCClass : GorillaRPCBehaviour
{
    // Your RPC methods go here
}
```

---

### Step 2: Define RPC Methods

RPC methods require the `[GorillaRPC]` attribute.

```cs
[GorillaRPC("MethodName", 101)]
private void YourMethod(string param1, int param2)
{
    Debug.Log($"Received RPC call with {param1} and {param2}");
    // Handle the RPC here
}
```

####  Required:
- Method name (string)
- Event code (byte: **0–255**)

#### Reserved event codes:
`0, 1, 2, 3, 4, 5, 8, 9, 50, 51, 176, 199` — Automatically blocked

---

### Step 3: Trigger RPC Methods

Call the method across the network:

```cs
Call(
    nameof(YourMethod),                    // Method name
    new object[] { "Hello", 42 },          // Parameters
    this,                                  // Target behavior
    101,                                   // Event code (must match attribute)
    Photon.Realtime.ReceiverGroup.Others   // Receiver group
);
```

#### Receiver Groups:
- `ReceiverGroup.Others` - All other players (not you)
- `ReceiverGroup.All` - Everyone, including you
- `ReceiverGroup.MasterClient` - Only the Master Client

---

### Step 4: Add Your RPC Handler to a GameObject

```cs
// Create a new GameObject with your RPC behavior
var rpcObject = new GameObject("RPCHandler").AddComponent<YourRPCClass>();

// Or add to an existing GameObject
gameObject.AddComponent<YourRPCClass>();
```

---

## Complete Example

```cs
using GorillaRPC.Types;
using Photon.Pun;
using UnityEngine;

public class PlayerMessage : GorillaRPCBehaviour
{
    [GorillaRPC("DisplayMessage", 120)]
    private void DisplayMessage(string message)
    {
        Debug.Log($"[Remote] {message}");
    }

    public void SendMessageToAll(string message)
    {
        Call(
            nameof(DisplayMessage),
            new object[] { message },
            this,
            120,
            Photon.Realtime.ReceiverGroup.All
        );
    }
}
```

---

## Important Notes

- RPC methods are **auto-registered** on `Awake()`.
- Each RPC method must have a **unique event code + method name** pair.
- RPC parameters must be **Photon-serializable**.

---
