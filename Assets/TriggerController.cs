using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering GameObject has a specific tag, you can customize this condition as needed
        if (other.CompareTag("Player"))
        {
            // Enable the objectToEnable GameObject
            objectToEnable.SetActive(true);
        }
    }
}
