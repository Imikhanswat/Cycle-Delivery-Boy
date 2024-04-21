using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fillImage;
    public TextMeshProUGUI levelText;

    private float totalFillValue;
    private int levelNumber;

    public GameObject[] Levels;
    
    void Start()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetInt("CurrentLevelDollars", 0);
        if (PlayerPrefs.GetInt("LevelNumber") == 0) {
            PlayerPrefs.SetInt("LevelNumber", 1);
            PlayerPrefs.SetInt("FakeLevel", 1);
        }
      
        foreach (GameObject level in Levels)
        {
            level.SetActive(false);
        }
        levelNumber = PlayerPrefs.GetInt("LevelNumber", 1);
        Levels[levelNumber-1].SetActive(true); 

        
       // levelText.text = levelNumber.ToString();
        levelText.text = PlayerPrefs.GetInt("FakeLevel").ToString();

        // Calculate total fill value based on level
        totalFillValue = 1.0f / 3 * levelNumber;
        fillImage.fillAmount = 0f; // Start with empty fill

        AdsControllerScript.Instance.ShowBannerAd();
    }
    public void FillImage(float value)
    {
        // Fill the image based on the total fill value
        fillImage.fillAmount += value;
        
        // If image is filled completely, load the next level
        if (fillImage.fillAmount >= 1.0f)
        {
            //LoadNextLevel();
        Debug.Log("Image filled");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        AdsControllerScript.Instance.ShowInterstitialAd();
        int currentLevel = PlayerPrefs.GetInt("LevelNumber");

        // If the current level is less than or equal to 5, reload the current scene
        if (currentLevel <= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Set the level number to 1, indicating the start of the fake levels
            PlayerPrefs.SetInt("LevelNumber", 1);

            // Increment the fake level number
            int fakeLevel = PlayerPrefs.GetInt("FakeLevel");

            // If the fake level reaches 6, set it back to 6 and reload the scene
            if (fakeLevel == 6)
            {
                PlayerPrefs.SetInt("FakeLevel", 6);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                // Set the new fake level and reload the scene
               fakeLevel= PlayerPrefs.GetInt("FakeLevel") + 1;
                PlayerPrefs.SetInt("FakeLevel", fakeLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
