using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PanelController : MonoBehaviour
{
    public Image loadingImage;
    public GameObject firstPanel;
    public GameObject secondPanel;

    private bool loadingComplete = false;

    private void Start()
    {
        // PlayerPrefs.SetInt("LevelNumber", 5);
        //PlayerPrefs.SetInt("FakeLevel", 5);
        // Enable the first panel by default
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Start the loading process
        
        StartCoroutine(LoadingProcess());
    }

    private System.Collections.IEnumerator LoadingProcess()
    {
        float timer = 0f;
        float duration = 3f; // 3 seconds to fill the loading image

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            loadingImage.fillAmount = progress;
            yield return null;
        }

        // Loading complete
        loadingComplete = true;

        // Disable first panel and enable second panel
        //firstPanel.SetActive(false);
       // secondPanel.SetActive(true);

        // Wait for 2 seconds before starting scene 1
        yield return new WaitForSeconds(0.2f);
        //AppOpenAdd.instance.ShowAppOpenAd();

        // Load scene 1
        LoadScene(1);
    }

    private void LoadScene(int sceneIndex)
    {
        // Replace this with your own scene loading logic
        Debug.Log("Loading scene " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}
