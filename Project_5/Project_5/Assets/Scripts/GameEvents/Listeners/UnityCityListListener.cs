using Assets.Scripts.GameEvents.Events;
using Assets.Scripts.GameEvents.UnityEvents;
using Project_3.GameEvents.Listeners;
using Project_3.Scripts;
using System.Collections.Generic;

namespace Assets.Scripts.GameEvents.Listeners
{
    class UnityCityListListener : BaseGameEventListener<List<IChromosome<City>>, CityListEvent, UnityCityListEvent>
    {
    }
}
