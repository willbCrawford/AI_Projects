using Project_3.GameEvents.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Project_3.Scripts
{
    public class TextField : MonoBehaviour
    {
        [SerializeField] private StringEvent onFileSupplied;
        [SerializeField] private string PlayerPrefKey;

        // Start is called before the first frame update
        void Start()
        {
            var input = gameObject.GetComponent<InputField>();
            var se = new InputField.SubmitEvent();
            se.AddListener(SubmitName);
            input.onEndEdit = se;
        }

        private void SubmitName(string arg0)
        {
            Debug.Log(arg0 + " was Supplied as File Input");
            PlayerPrefs.SetString(PlayerPrefKey, arg0);

            onFileSupplied.Raise(arg0);
        }
    }

}