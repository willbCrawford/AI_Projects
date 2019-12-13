using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project_3.GameEvents.Events
{
    [CreateAssetMenu(fileName = "New String Event", menuName = "Custom Events/String Event")]
    public class StringEvent : BaseGameEvent<string> { }
}
