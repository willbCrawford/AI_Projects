using Project_3.GameEvents.Events;
using Project_3.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameEvents.Events
{
    [CreateAssetMenu(fileName = "New City List Event", menuName = "Custom Events/City List Event")]
    public class CityListEvent : BaseGameEvent<List<IChromosome<City>>>
    {
    }
}
