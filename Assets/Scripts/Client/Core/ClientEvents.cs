using UnityEngine.Events;

namespace Client.Core
{
    public class ClientEvent : UnityEvent { }
    public class PlayerScriptEvent: UnityEvent<PlayerScript> { }
}