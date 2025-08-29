using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Coroutine currentMovement;

    void Start()
    {
        StartCoroutine(AttackPatternCycle());
    }

    // Para que el jefe haga sus patrones de ataque
    IEnumerator AttackPatternCycle()
    {
    while (true) // ← Esto hace que se repita infinitamente
    {
        yield return StartCoroutine(Pattern1());
        yield return StartCoroutine(Pattern2());
        yield return StartCoroutine(Pattern3());
    }
    }

    // Patrón de ataque 1
    IEnumerator Pattern1()
    {
        currentMovement = StartCoroutine(MovePattern1());
        float timer = 0f;
        while (timer < 10f)
        {
            int bulletCount = 15;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * (360f / bulletCount);
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;
                SpawnBullet(dir, Color.red);
            }
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
        }
        StopCoroutine(currentMovement);
    }

    // Patrón de ataque 2
    IEnumerator Pattern2()
    {
        currentMovement = StartCoroutine(MovePattern2());
        float timer = 0f;
        float angle = 0f;
        while (timer < 10f)
        {
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;
            SpawnBullet(dir, Color.green);
            angle += 15f;
            yield return new WaitForSeconds(0.05f);
            timer += 0.1f;
        }
        StopCoroutine(currentMovement);
    }

    // Patrón de ataque 3
    IEnumerator Pattern3()
    {
        currentMovement = StartCoroutine(MovePattern3());
        float timer = 0f;
        while (timer < 10f)
        {
            for (int i = 0; i < 8; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                Vector3 spawnPos = transform.position + offset;
                Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                SpawnBullet(dir, new Color(1f, 0.85f, 3f), spawnPos);
            }
            yield return new WaitForSeconds(0.2f);
            timer += 0.2f;
        }
        StopCoroutine(currentMovement);
    }

    void SpawnBullet(Vector3 direction, Color color, Vector3? position = null)
    {
        Vector3 spawnPos = position ?? transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = direction;
        bullet.GetComponent<SpriteRenderer>().color = color;
        BulletManager.Instance.AddBullet(bullet);
    }

    IEnumerator MovePattern1()
    {
        float speed = 4f;
        Vector3 left = new Vector3(-4f, transform.position.y, transform.position.z);
        Vector3 right = new Vector3(4f, transform.position.y, transform.position.z);

        while (true)
        {
            while (Vector3.Distance(transform.position, left) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, left, speed * Time.deltaTime);
                speed = Random.Range(4f, 5.5f); // velocidad variable
                yield return null;
            }

            while (Vector3.Distance(transform.position, right) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, right, speed * Time.deltaTime);
                speed = Random.Range(3f, 5.5f);
                yield return null;
            }
        }
    }

    IEnumerator MovePattern2()
    {
        float time = 0f;
        float amplitudeX = 5f;
        float amplitudeY = 1f;
        float frequency = 2f;

        while (true)
        {
            float x = Mathf.Sin(time * frequency) * amplitudeX;
            float y = Mathf.Cos(time * frequency) * amplitudeY;
            transform.position = new Vector3(x, y, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MovePattern3()
    {
        float speed = 3.5f;
        float direction = 7f;
        float verticalAmplitude = 1.5f;      // más altura
        float verticalFrequency = 3f;      // velocidad de oscilación vertical

        Vector3 startPos = transform.position;

        while (true)
        {
            // Movimiento lateral
            float x = transform.position.x + direction * speed * Time.deltaTime;

            // Movimiento vertical oscilante
            float y = startPos.y + Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;

            transform.position = new Vector3(x, y, transform.position.z);

            // Invertir dirección si llega a los bordes
            if (transform.position.x > 4f || transform.position.x < -4f)
            {
                direction *= -1f;
            }

            yield return null;
        }
    }
}