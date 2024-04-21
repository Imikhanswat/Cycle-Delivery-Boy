using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System.Collections;
using System.Threading;

public class AppOpenAdd : MonoBehaviour
{

    public static bool isShowingAppOpenAd = false;

    AppOpenAd appOpenAd;
    public string _adUnitId;


    // Use this flag to ensure only one instance of the script exists
    private static bool created = false;
    public static AppOpenAdd instance = null;

    void Awake()
    {
        instance = this;
        if (!created)
        {
            // If this is the first instance, make it persistent
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(this.gameObject);
            return;
        }

        // Other initialization code can go here
    }

    // Start is called before the first frame update
    void Start()
    {
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        
    }

    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded.
        if (!paused && !isShowingAppOpenAd)
        {
            ShowAppOpenAd();
        }
    }

    private void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyAd()
    {
        if (appOpenAd != null)
        {
            Debug.Log("Destroying app open ad.");
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        // Inform the UI that the ad is not ready.
       // AdLoadedStatus?.SetActive(false);
    }
    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            DestroyAd();
        }

        Debug.Log("Loading app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        AppOpenAd.Load(_adUnitId, adRequest, (AppOpenAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                Debug.LogError("App open ad failed to load an ad with error : "
                                + error);
                return;
            }

            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                Debug.LogError("Unexpected error: App open ad load event fired with " +
                               " null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("App open ad loaded with response : " + ad.GetResponseInfo());
            appOpenAd = ad;

            // App open ads can be preloaded for up to 4 hours.
            //_expireTime = DateTime.Now + TIMEOUT;

            // Register to ad events to extend functionality.
            RegisterEventHandlers(ad);

            // Inform the UI that the ad is ready.
            //AdLoadedStatus?.SetActive(true);
        });
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
            LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
            LoadAppOpenAd();

        };
    }

    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            StartCoroutine(ShowAppOpenAdWithDelay());
        }
    }

    IEnumerator ShowAppOpenAdWithDelay()
    {
        yield return new WaitForSeconds(1.3f);
        ShowAppOpenAd();
    }

    public void ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
            LoadAppOpenAd();
        }
        
    }
}