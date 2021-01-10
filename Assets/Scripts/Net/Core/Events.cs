using System.Net;
using Client;
using Net.Interfaces;
using Net.PackageData;
using Net.PackageData.EventsData;
using Net.Packages;
using UnityEngine.Events;

namespace Net.Core
{
    public class PackageEvent : UnityEvent<AbstractPackage> { }
    public class ConnectPackageEvent : UnityEvent<ConnectPackage> { }
    public class EventPackageEvent : UnityEvent<EventPackage> { }
    public class StatePackageEvent : UnityEvent<StatePackage> { }
    public class AcceptPackageEvent : UnityEvent<AcceptPackage> { }
    public class DeclinePackageEvent : UnityEvent<DeclinePackage> { }
    
    public class StateDataEvent : UnityEvent<StateData> { }
    public class EventDataEvent : UnityEvent<EventData> { }
    public class ConnectDataEvent : UnityEvent<ConnectData> { }
    
    public class IpAddressEvent : UnityEvent<IPAddress> { }
    
    public class PlayerMovementEvent: UnityEvent<IPAddress, MovementEventData> { }

}
