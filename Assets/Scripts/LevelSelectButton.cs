using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] int levelDest = 0;
    private bool canClick = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelButtonClicked()
    {
        if (!canClick) return;
        canClick = false;
        Debug.Log("Go to level " + levelDest.ToString());
        SceneManager.LoadScene("Level " + levelDest.ToString());
    }
}
