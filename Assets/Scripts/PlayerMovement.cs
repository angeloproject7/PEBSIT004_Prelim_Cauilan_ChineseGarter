using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float mobileInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        float finalInput = keyboardInput;

        if (Mathf.Abs(mobileInput) > 0.01f)
        {
            finalInput = mobileInput;
        }

        rb.linearVelocity = new Vector2(
            finalInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    public void SetMobileMove(float direction)
    {
        mobileInput = direction;
    }

    public void StopMobileMove()
    {
        mobileInput = 0f;
    }
}