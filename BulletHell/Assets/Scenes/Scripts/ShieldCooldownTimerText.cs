using UnityEngine;
using TMPro;

public class ShieldCooldownTimerText : MonoBehaviour
{
    public TextMeshProUGUI timerText; // o TextMesh si es 2D
    public PlayerController player;

    void Update()
    {
        if (!player.shieldActive)
        {
            int timeLeft = Mathf.CeilToInt(player.shieldCooldown - player.shieldTimer);
            timerText.text = timeLeft.ToString();
        }
        else
        {
            timerText.text = "";
        }
    }
}
