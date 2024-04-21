using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDelivery : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject girl1, girl2;
    void Start()
    {
        girl1.SetActive(true);
        girl2.SetActive(false);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the delivery box
        if (other.CompareTag("Player"))
        {
            girl2.SetActive(true);
            girl1.SetActive(false);
        }
    }
}
