using UnityEngine.Events;
using Server.Interfaces;
using Server.PackageData;
using Server.Packages;

namespace Server.Core
{
    public class PackageEvent : UnityEvent<IPackage> { }
    public class ConnectPackageEvent : UnityEvent<ConnectPackage> { }
    public class EventPackageEvent : UnityEvent<EventPackage> { }
    public class StatePackageEvent : UnityEvent<StatePackage> { }

    public class StateDataEvent : UnityEvent<StateData> { }
    public class EventDataEvent : UnityEvent<EventData> { }
    public class ConnectDataEvent : UnityEvent<ConnectData> { }

}
