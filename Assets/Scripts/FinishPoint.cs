using System.Collections;
using TMPro;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject FinishPanel, finishParticles;
    public static bool GameFinished = false;

   // public GameObject MainCamera, FinishCamera;
    public GameObject DollarTryObjects; // Reference to the object whose children need to be disabled

    public TextMeshProUGUI earnedValueText,currentLevelEarnedText;
    void Start()
    {
        GameFinished = false;
        finishParticles.SetActive(false);
        FinishPanel.SetActive(false);

        earnedValueText.text = PlayerPrefs.GetInt("Dollars").ToString();
        currentLevelEarnedText.text = PlayerPrefs.GetInt("CurrentLevelDollars").ToString();
        //MainCamera.SetActive(true);
        //FinishCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate FinishPanel
            FinishPanel.SetActive(true);
            // Activate FinishCamera
           // FinishCamera.SetActive(true);
           // MainCamera.SetActive(true);

            // Assuming FinishParticles is a child GameObject of the player GameObject
            if (finishParticles != null)
            {
                finishParticles.SetActive(true);
            }

            // Call SetFinishTrue with a delay
            Invoke("SetFinishTrue", 0f);

            // Disable children of DollarTryObjects with delay
            StartCoroutine(DisableChildrenWithDelay());
        }
    }

    IEnumerator DisableChildrenWithDelay()
    {
        yield return new WaitForSeconds(0.2f);

        // Disable each child object with a delay
        foreach (Transform child in DollarTryObjects.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f); // Delay before disabling the next child
            }
        }
    }

    public void SetFinishTrue()
    {
        GameFinished = true;
        
        currentLevelEarnedText.text= PlayerPrefs.GetInt("CurrentLevelDollars").ToString();
        PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + PlayerPrefs.GetInt("CurrentLevelDollars"));
        earnedValueText.text = PlayerPrefs.GetInt("Dollars").ToString();
        PlayerPrefs.SetInt("LevelNumber", PlayerPrefs.GetInt("LevelNumber") + 1);
        PlayerPrefs.SetInt("FakeLevel", PlayerPrefs.GetInt("FakeLevel") + 1);

       
    }
}
