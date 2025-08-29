using UnityEngine;
using TMPro; // ← Importante para usar TextMeshPro
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    public TextMeshProUGUI bossBulletText;
    public TextMeshProUGUI playerBulletText;

    private List<GameObject> bossBullets = new List<GameObject>();
    private List<GameObject> playerBullets = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void AddBullet(GameObject bullet)
    {
        if (bullet == null) return;

        if (bullet.CompareTag("BossBullet"))
        {
            bossBullets.Add(bullet);
        }
        else if (bullet.CompareTag("PlayerBullet"))
        {
            playerBullets.Add(bullet);
        }

        UpdateUI();
    }

    public void RemoveBullet(GameObject bullet)
    {
        if (bullet == null) return;

        if (bossBullets.Contains(bullet))
        {
            bossBullets.Remove(bullet);
        }
        else if (playerBullets.Contains(bullet))
        {
            playerBullets.Remove(bullet);
        }

        Destroy(bullet);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (bossBulletText != null)
            bossBulletText.text = "Balas jefe: " + bossBullets.Count;

        if (playerBulletText != null)
            playerBulletText.text = "Balas jugador: " + playerBullets.Count;
    }
}