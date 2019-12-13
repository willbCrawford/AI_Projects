using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAlgorithm : MonoBehaviour
{
    private bool AbleToBeStarted = false;
    public void OnButtonClick()
    {
        Debug.Log("Button got clicked");
        if (AbleToBeStarted)
        {
            SceneManager.LoadScene(1);

        }
    }

    public void OnFileSuplied(string FilePath)
    {
        if (FilePath.Length != 0)
            AbleToBeStarted = true;
    }
}
