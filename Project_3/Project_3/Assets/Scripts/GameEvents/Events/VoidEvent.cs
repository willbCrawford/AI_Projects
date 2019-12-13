using UnityEngine;

namespace Project_3.GameEvents.Events
{
    [CreateAssetMenu(fileName ="New Void Event", menuName = "Custom Events/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}
