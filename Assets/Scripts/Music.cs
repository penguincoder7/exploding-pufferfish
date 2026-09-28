using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;
    private static Music instance;

    private void Awake()
    {
        // Check if an instance already exists in the scene
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}