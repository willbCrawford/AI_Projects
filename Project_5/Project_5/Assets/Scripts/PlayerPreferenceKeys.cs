

using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "PlayerPref File", menuName = "Create new PlayerPrefence Keys")]
    class PlayerPreferenceKeys : ScriptableObject
    {
        [SerializeField] public string FilePathSupplied;
        [SerializeField] public string TestLength;
    }
}
