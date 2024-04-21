using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Store the initial state of sound
    private bool soundEnabled = true;
    public static bool vibrationEnabled = true;

    public Button soundOnButton, SoundOffButton;
    public Button vibrationOnButton, vibrationOffButton;

   

    public GameObject SettingPanel;

    // Start is called before the first frame update
    void Start()
    {
        SettingPanel.SetActive(false);
        // Load the saved state of sound and vibration
        if (PlayerPrefs.GetInt("Sounds") == 0)
        {
            soundEnabled = false;
            AudioListener.pause = true;
            soundOnButton.interactable = true;
            SoundOffButton.interactable = false;
        }
        else
        {
            soundEnabled = true;
            AudioListener.pause = false;
            soundOnButton.interactable = false;
            SoundOffButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("Vibration") == 0)
        {
            vibrationEnabled = false;
            vibrationOnButton.interactable = true;
            vibrationOffButton.interactable = false;
        }
        else
        {
            vibrationEnabled = true;
            vibrationOnButton.interactable = false;
            vibrationOffButton.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to mute all sounds
    public void OffSound()
    {
        soundEnabled = false;
        AudioListener.pause = true;
        PlayerPrefs.SetInt("Sounds", 0);

        soundOnButton.interactable = true;
        SoundOffButton.interactable = false;
    }

    // Function to unmute all sounds
    public void EnableSound()
    {
        soundEnabled = true;
        AudioListener.pause = false;

        soundOnButton.interactable = false;
        SoundOffButton.interactable = true;
        PlayerPrefs.SetInt("Sounds", 1);
    }

    // Function to enable vibration
    public void EnableVibration()
    {
        vibrationEnabled = true;
        vibrationOnButton.interactable = false;
        vibrationOffButton.interactable = true;
        PlayerPrefs.SetInt("Vibration", 1);
        Handheld.Vibrate();
    }

    // Function to disable vibration
    public void DisableVibration()
    {
        vibrationEnabled = false;
        vibrationOnButton.interactable = true;
        vibrationOffButton.interactable = false;
        PlayerPrefs.SetInt("Vibration", 0);
    }

    public void EnableSettingPanel()
    {
        SettingPanel.SetActive(true);
        NotifyBikeInputStatus(true); // Notify BicycleController to stop accepting input
    }

    public void DisableSettingPanel()
    {
        SettingPanel.SetActive(false);
        NotifyBikeInputStatus(false); // Notify BicycleController to resume accepting input
    }

    private void NotifyBikeInputStatus(bool isEnabled)
    {
        // Find the BicycleController script in the scene and call its UpdateInput method
        BicycleController.IfAnyPanelActive = isEnabled;

    }
}
