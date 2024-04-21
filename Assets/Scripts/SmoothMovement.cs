using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float initialDestroyTime = 18.0f;

    public float assignTimeAfterDestroy=18f;
    private Vector3 initialPosition;
    

    void Start()
    {
        // Save the initial position
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move the object in the -Z direction smoothly
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Reduce destroyTime
        initialDestroyTime -= Time.deltaTime;

        // Reset to initial position if destroyTime reaches zero
        if (initialDestroyTime <= 0)
        {
            // Reset to initial position
            transform.position = initialPosition;

            // Reset destroyTime to its initial value
            initialDestroyTime = assignTimeAfterDestroy; // You can change this back to your desired initial destroy time
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollidingObject")) {
            speed = 0.0f;
        }
    }
}
