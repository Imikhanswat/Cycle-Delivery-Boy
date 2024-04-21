using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed at which the ball moves

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Make the ball move forward continuously
        rb.velocity = transform.forward * moveSpeed;
    }
}
