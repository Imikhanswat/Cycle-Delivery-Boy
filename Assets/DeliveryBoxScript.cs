using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryBoxScript : MonoBehaviour
{
    public GameObject deliveryBoxesPanel;
    // public Button closeButton;
    public GameObject player;
    private BicycleController bicycleController;
    public GameObject girl1, girl2;
    private bool once = true;
    public GameObject RewardClaimedText;
    public GameObject DeliveryBoxes;

    public static DeliveryBoxScript instance;



    private void Start()
    {
        instance = this;
        RewardClaimedText.SetActive(false);

        girl2.SetActive(false);
        // Disable the delivery boxes panel at the start
        deliveryBoxesPanel.SetActive(false);

        // Get the BicycleController component attached to the player GameObject
        bicycleController = player.GetComponent<BicycleController>();
       // DeliveryBoxes.SetActive(false);

        // Add a listener to the close button
        // closeButton.onClick.AddListener(ClosePanel);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the delivery box
        if (other.CompareTag("Player") && once)
        {
            // Call the UpdateInput function with isActive = false
            bicycleController.UpdateInput(false);
            bicycleController.Padel.SetBool("Padel", false);
            bicycleController.Cycling.SetBool("Cycling", false);
            BicycleController.IfAnyPanelActive = true;






            // Show the delivery boxes panel
            deliveryBoxesPanel.SetActive(true);
            once = false;

            Material material = this.gameObject.GetComponent<Renderer>().material;

            // Get the current color from the material
            Color currentColor = material.color;

            // Set the alpha value to zero
            currentColor.a = 0f;

            // Assign the modified color back to the material
            material.color = currentColor;
        }
    }

    public void ClosePanel()
    {
        // Hide the delivery boxes panel when the close button is clicked
        // girl2.SetActive(true);
        BicycleController.IfAnyPanelActive = false;
        girl1.SetActive(false);
        deliveryBoxesPanel.SetActive(false);
        once = true;
        // Enable the BicycleController component



        // Call the UpdateInput function with isActive = true
        // bicycleController.UpdateInput(true);
    }
    public void ShowRewardedAd() {
        AdsControllerScript.Instance.ShowRewardedAd(1);
    }
    public void ShowRewardAchivementText() {
        PlayerPrefs.SetInt("CurrentLevelDollars", PlayerPrefs.GetInt("CurrentLevelDollars") + 20);
        RewardClaimedText.SetActive(true);
        DeliveryBoxes.SetActive(false);
        Invoke("DelayToHideText", 2.5f);
    }
    public void DelayToHideText() {
        RewardClaimedText.SetActive(false);
    }
    
}
