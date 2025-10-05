using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 5f;
    public int damage = 3;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            BulletManager.Instance.RemoveBullet(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>()?.TakeDamage(damage);
                BulletManager.Instance.RemoveBullet(gameObject);
            }
        }

}