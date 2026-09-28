using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUIManager : MonoBehaviour
{

    private int nextLevel = 0;
    private bool canClick = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void DisplayLevel(int level)
    {
        Transform levelText = transform.Find("LevelText");
        levelText.GetComponent<TextMeshProUGUI>().text = "LEVEL: " + level.ToString();
    }

    public void DisplayResultScreen(bool win, int nextLevel, bool final)
    {
        Transform resultPanel = transform.Find("ResultPanel");
        Transform nextButton = resultPanel.Find("NextButton");
        Transform resultText = resultPanel.Find("ResultText");
        if (!win)
        {
            resultText.GetComponent<TextMeshProUGUI>().text = "LEVEL FAILED";
            nextButton.gameObject.SetActive(false);
        }
        else if (final)
        {
            resultText.GetComponent<TextMeshProUGUI>().text = "GAME BEATEN";
            nextButton.gameObject.SetActive(false);
        }
        this.nextLevel = nextLevel;
        canClick = true;
        resultPanel.gameObject.SetActive(true);
    }

    public void NextButtonClicked()
    {
        if (!canClick) return;
        canClick = false;
        Debug.Log("Go to next level");
        SceneManager.LoadScene("Level " + nextLevel.ToString());
    }

    public void RetryButtonClicked()
    {
        if (!canClick) return;
        canClick = false;
        Debug.Log("Restart level");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void levelSelectButtonClicked()
    {
        if (!canClick) return;
        canClick = false;
        Debug.Log("Level select button pressed");
        SceneManager.LoadScene("Level Select");
    }

    public void TitleButtonClicked()
    {
        if (!canClick) return;
        canClick = false;
        Debug.Log("Go to title screen");
        SceneManager.LoadScene("Title Screen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
