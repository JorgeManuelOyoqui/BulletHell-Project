using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip battleMusic;
    public AudioClip victoryMusic;
    public AudioClip defeatMusic;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBattleMusic()
    {
        audioSource.clip = battleMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayVictoryMusic()
    {
        audioSource.Stop();
        audioSource.clip = victoryMusic;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayDefeatMusic()
    {
        audioSource.Stop();
        audioSource.clip = defeatMusic;
        audioSource.loop = false;
        audioSource.Play();
    }
}