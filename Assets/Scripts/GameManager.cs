using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float chainDelay = 0.15f;         // delay before chained fish explode
    [SerializeField] int enemiesRemaining = 0;
    [SerializeField] int level = 1;
    [SerializeField] bool final;
    private float waitCheck = 1f;

    [SerializeField] ResultUIManager uIManager;
    private AudioSource explodeSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitCheck = enemiesRemaining * chainDelay + 0.25f;
        uIManager.DisplayLevel(level);
        explodeSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getChainDelay()
    {
        return chainDelay;
    }

    public void CreateExplosion(GameObject explosionEffect, Transform callerTransform, float explosionRadius, float visualExpandTime)
    {
        GameObject visual = Instantiate(explosionEffect, callerTransform.position, Quaternion.identity);
        visual.transform.localScale = Vector3.zero;
        StartCoroutine(ExpandVisual(visual.transform, explosionRadius, visualExpandTime));
    }

    private IEnumerator ExpandVisual(Transform visualTransform, float explosionRadius, float visualExpandTime)
    {
        float t = 0f;
        Vector3 targetScale = Vector3.one * (explosionRadius * 2f); // diameter, assuming prefab is 1 unit
        while (t < visualExpandTime)
        {
            t += Time.deltaTime;
            float pct = t / visualExpandTime;
            visualTransform.localScale = Vector3.Lerp(Vector3.zero, targetScale, pct);
            yield return null;
        }
        Destroy(visualTransform.gameObject, 0.3f); // linger briefly then clean up
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            float pct = t / 0.3f;
            Color tempColor = visualTransform.GetComponent<SpriteRenderer>().color;
            tempColor.a = 1 - pct;
            visualTransform.GetComponent<SpriteRenderer>().color = tempColor;
            yield return null;
        }
    }

    public void OnEnemyDied()
    {
        explodeSound.Play();
        enemiesRemaining--;
    }

    public void OnPlayerDied(bool instant)
    {
        if (instant)
        {
            StartCoroutine(Pause(0.4f));
        }
        else
        {
            explodeSound.Play();
            StartCoroutine(Pause(waitCheck));
        }
    }

    IEnumerator Pause(float time)
    {
        yield return new WaitForSeconds(time);
        bool win = enemiesRemaining == 0;
        if (win)
        {
            PlayerPrefs.SetInt("highestLevel", Mathf.Max(PlayerPrefs.GetInt("highestLevel"), level + 1));
        }
        uIManager.DisplayResultScreen(win, level + 1, final);
    }
}
