using System.Collections;
using UnityEngine;

public class DropBoxes : MonoBehaviour
{
    public Transform[] deliveryHouses; // Array of delivery house positions
    public GameObject boxPrefab; // Prefab of the box to instantiate
    public float boxSpeed = 5f; // Speed of the moving boxes

    private bool hasDelivered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasDelivered)
        {
            hasDelivered = true;
            StartCoroutine(DeliverBoxes());
        }
    }

    IEnumerator DeliverBoxes()
    {
        // Instantiate boxes and move them to delivery houses
        foreach (Transform deliveryHouse in deliveryHouses)
        {
            GameObject box = Instantiate(boxPrefab, transform.position, Quaternion.identity);
            Vector3 targetPosition = deliveryHouse.position;

            // Move the box towards the delivery house
            while (Vector3.Distance(box.transform.position, targetPosition) > 0.1f)
            {
                box.transform.position = Vector3.MoveTowards(box.transform.position, targetPosition, Time.deltaTime * boxSpeed);
                yield return null;
            }

            

            // Destroy the box when it reaches the delivery house
            deliveryHouse.GetComponent<Animator>().Update(0f);
            deliveryHouse.GetComponent<Animator>().SetFloat("speed", 10f);
            deliveryHouse.GetComponent<Animator>().SetBool("idle", false);
            deliveryHouse.GetComponent<Animator>().SetBool("dance", true);
            Destroy(box);
        }
    }
}
