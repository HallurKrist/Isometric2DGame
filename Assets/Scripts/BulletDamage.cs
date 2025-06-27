using UnityEngine;
using UnityEngine.EventSystems;

public class BulletDamage : MonoBehaviour
{
    private float timeLeft = 0.0f;
    private Vector2 direction = Vector2.zero;
    private float speed = 1.0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.0f )
        {
            gameObject.SetActive( false );
            return;
        }
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<HealthController>().TakeDamage();
        }
    }

    public void ShootBullet(Vector3 StartPosition, Vector2 flightDirection,  float bulletSpeed, float duration)
    {
        transform.position = StartPosition;
        timeLeft = duration;
        direction = flightDirection;
        speed = bulletSpeed;
    }
}
