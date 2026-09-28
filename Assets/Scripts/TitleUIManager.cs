using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void startButtonClicked()
    {
        Debug.Log("Start button pressed");
        SceneManager.LoadScene("Level 1");
    }

    public void levelSelectButtonClicked()
    {
        Debug.Log("Level select button pressed");
        SceneManager.LoadScene("Level Select");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
