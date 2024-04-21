using UnityEngine;

public class MoneyBundleController : MonoBehaviour
{
    public GameObject[] moneyBundle;
    private int currentIndex = 0;
public static MoneyBundleController instance;
    void Start()
    {
        instance=this;
        // Disable all money bundles at the start
        DisableAllMoneyBundles();
    }

    // Function to enable the next money bundle
    public void EnableNextMoneyBundle()
    {
        // Check if there are still bundles left to enable
        if (currentIndex < moneyBundle.Length)
        {
            // Enable the next bundle
            moneyBundle[currentIndex].SetActive(true);
            // Increment the index for the next call
            currentIndex++;
        }
        else
        {
            Debug.Log("No more money bundles left to enable.");
        }
        if (Settings.vibrationEnabled) {
            Handheld.Vibrate();
        }
    }

    // Function to disable all money bundles
    private void DisableAllMoneyBundles()
    {
        foreach (GameObject bundle in moneyBundle)
        {
            bundle.SetActive(false);
        }
    }
}
