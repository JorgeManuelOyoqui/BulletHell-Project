using UnityEngine;
using TMPro; // ← Importante para usar TextMeshPro
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject victorySprite;
    public GameObject defeatSprite;

    public TextMeshProUGUI bossHealthText;

    public MusicManager musicManager;

    public SFXManager sfxManager;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        victorySprite.gameObject.SetActive(false);
        defeatSprite.gameObject.SetActive(false);
    }
    
    public void ShowVictory()
    {
        musicManager.PlayVictoryMusic();
        victorySprite.gameObject.SetActive(true);
    }

    public void ShowDefeat()
    {
        musicManager.PlayDefeatMusic();
        defeatSprite.gameObject.SetActive(true);
    }

    public void UpdateBossHealth(int current, int max)
    {
        bossHealthText.text = $"{current} / {max}";
    }

}
