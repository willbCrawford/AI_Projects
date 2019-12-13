using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserKeyboardInput : MonoBehaviour
{
    [SerializeField] GameObject UIElement;
    // Start is called before the first frame update
    void Start()
    {
        UIElement.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UIElement.SetActive(true);
        }
    }
}
