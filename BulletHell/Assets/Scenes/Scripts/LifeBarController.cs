using UnityEngine;

public class LifeBarController : MonoBehaviour
{
    public Sprite[] lifeSprites; // 0 = 3 vidas, 1 = 2 vidas, 2 = 1 vida, 3 = 0 vidas
    public SpriteRenderer spriteRenderer;
    public PlayerController player;

    void Update()
    {
        switch (player.CurrentHealth)
        {
            case 3:
                spriteRenderer.sprite = lifeSprites[0];
                break;
            case 2:
                spriteRenderer.sprite = lifeSprites[1];
                break;
            case 1:
                spriteRenderer.sprite = lifeSprites[2];
                break;
            case 0:
                spriteRenderer.sprite = lifeSprites[3];
                break;
        }
    }
}