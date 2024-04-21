using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
    public GameObject deliveryBox; // Delivery box object
    public float dragSpeed = 2f;   // Speed of dragging the delivery box
    public float stopDistance = 0.4f; // Distance threshold to stop the delivery box

    private bool hasTriggered = false; // Flag to ensure the delivery box is dragged only once

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered) // Check if player has triggered the collider and the action hasn't been done yet
        {
           

            // Pick up the delivery box
            Rigidbody boxRb = deliveryBox.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                boxRb.isKinematic = true; // Make the box kinematic so it can be moved manually
                deliveryBox.transform.parent = other.transform; // Attach the box to the player
            }

           // PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 20);
            PlayerPrefs.SetInt("CurrentLevelDollars", PlayerPrefs.GetInt("CurrentLevelDollars") + 20);

            // Set flag to prevent repeated dragging
            hasTriggered = true;
            EnableBundle();
           // Invoke("EnableBundle", 0.2f);
        }
    }

    void Update()
    {
        if (hasTriggered)
        {
            // Move the delivery box towards the trigger point
            Vector3 targetPosition = transform.position;
            Vector3 currentPosition = deliveryBox.transform.position;

            // Calculate the direction towards the target
            Vector3 moveDirection = (targetPosition - currentPosition).normalized;

            // Move the box towards the target
            deliveryBox.transform.position += moveDirection * dragSpeed * Time.deltaTime;

            // Check if the delivery box has reached close to the target
            if (Vector3.Distance(deliveryBox.transform.position, targetPosition) < stopDistance)
            {
                // Destroy the delivery box and the trigger point
                Destroy(deliveryBox);
                Destroy(gameObject); // Assuming the script is attached to the trigger point itself
            }
        }
    }
    public void EnableBundle()
    {
        MoneyBundleController.instance.EnableNextMoneyBundle();
    }

}
