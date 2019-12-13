using UnityEngine;

namespace Assets.Scripts
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField] GameObject UIElement;

        public void OnContinuePressed()
        {
            UIElement.SetActive(false);
        }
    }
}
