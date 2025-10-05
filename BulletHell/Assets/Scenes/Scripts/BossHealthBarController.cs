using UnityEngine;

public class BossHealthBarController : MonoBehaviour
{
    public Transform healthFill; // referencia al rectángulo rojo
    public BossController boss; // referencia al script del jefe

    private Vector3 originalScale;

    void Start()
    {
        originalScale = healthFill.localScale;
    }

    void Update()
    {
        float healthFraction = (float)boss.CurrentHealth / boss.maxHealth;
        healthFill.localScale = new Vector3(originalScale.x * healthFraction, originalScale.y, originalScale.z);
    }
}