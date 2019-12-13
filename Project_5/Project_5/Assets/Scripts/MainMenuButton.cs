using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class MainMenuButton : MonoBehaviour
    {
        public void OnMainMenuPressed()
        {
            SceneManager.LoadScene(0);
        }

    }
}
