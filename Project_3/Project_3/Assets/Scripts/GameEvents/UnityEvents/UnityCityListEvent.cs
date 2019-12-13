using Project_3.Scripts;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets.Scripts.GameEvents.UnityEvents
{
    [System.Serializable] public class UnityCityListEvent : UnityEvent<List<City>>
    {
    }
}
