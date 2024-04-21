using UnityEngine;

public class MoneyBundle : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation
    public GameObject animatedMoneyPrefab; // Reference to the AnimatedMoney prefab
    

    private bool isCollected = false; // Flag to track if the bundle is collected

    public GameObject[] Bundles;
    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // Method to handle the player's collision with the money bundle
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            MoneyBundleController.instance.EnableNextMoneyBundle();
            animatedMoneyPrefab.SetActive(true);
            foreach (var bundle in Bundles)
            {

                bundle.gameObject.SetActive(false);
            }
            Invoke("DisableGameObject", 1.5f);
            PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 20);
            PlayerPrefs.SetInt("CurrentLevelDollars", PlayerPrefs.GetInt("CurrentLevelDollars") + 20);
        }
    }
    public void DisableGameObject() {

       Destroy(gameObject);
    }

    
   
}
