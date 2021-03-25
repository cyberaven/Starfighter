using UnityEngine.Events;

namespace Core
{
    public class CoreEvent: UnityEvent { }
    public class AxisValueEvent: UnityEvent<string, float> { }
}