using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 moveDirection = Vector2.zero;

    [SerializeField] private float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    public void OnMove(InputValue value)
    {
        moveDirection = RotateVector(value.Get<Vector2>(), -45);
        Debug.Log(moveDirection);
    }

    public Vector2 RotateVector(Vector2 vec, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float newX = vec.x * cos - vec.y * sin;
        float newY = vec.x * sin + vec.y * cos;

        return new Vector2(newX, newY);
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
