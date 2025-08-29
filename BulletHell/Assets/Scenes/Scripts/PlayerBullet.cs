using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 10f;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.y) > 20)
        {
            BulletManager.Instance.RemoveBullet(gameObject);
        }
    }
}