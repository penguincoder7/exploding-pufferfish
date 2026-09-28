using Newtonsoft.Json.Bson;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{

    private int highestLevel;
    public GameObject buttons;
    private bool canClick = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);
        for (int i = 1; i <= buttons.transform.childCount; i++)
        {
            Transform button = buttons.transform.GetChild(i - 1);
            if (i > highestLevel)
            {
                button.gameObject.SetActive(false);
            } else
            {
                button.gameObject.SetActive(true);
            }
        }

    }

    public void BackButtonPressed()
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
