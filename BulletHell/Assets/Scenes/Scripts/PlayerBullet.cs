using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10f;
    public int damage = 20;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.y) > 20)
        {
            BulletManager.Instance.RemoveBullet(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossController>()?.TakeDamage(damage);
            BulletManager.Instance.RemoveBullet(gameObject);
        }
    }

}