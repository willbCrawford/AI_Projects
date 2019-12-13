using UnityEngine;

namespace Assets.Scripts
{
    class QuitButton : MonoBehaviour
    {
        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}
