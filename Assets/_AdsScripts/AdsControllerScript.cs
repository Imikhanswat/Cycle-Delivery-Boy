using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using System.Collections;


public class AdsControllerScript : MonoBehaviour
{

    #region Instance

    // Static singleton instance
    private static AdsControllerScript instance;
    public static AdsControllerScript Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AdsControllerScript");
                instance = obj.AddComponent<AdsControllerScript>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    #endregion


    
    private BannerView bannerView;
    private BannerView rectbannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    

   
    public string AdmobBannerID = null;
    public string AdmobRectBannerID = null;
    public string AdmobInterstitialID = null;
    public string AdmobRewardedID = null;
    public string AdmobRewardedInterstitialID = null;

    static bool isInitialized = false;


    public void InitializeAds()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            try
            {
                MobileAds.SetiOSAppPauseOnBackground(true);

                //List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

                // Add some test device IDs (replace with your own device IDs).
                //#if UNITY_IPHONE
                //        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
                //#elif UNITY_ANDROID
                //                deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
                //#endif

                // Configure TagForChildDirectedTreatment and test device IDs.
                RequestConfiguration requestConfiguration =
                    new RequestConfiguration.Builder().build();
                MobileAds.SetRequestConfiguration(requestConfiguration);

                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(HandleInitCompleteAction);

                // Listen to application foreground / background events.
                //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;

            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }

        }
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Debug.Log("Initialization complete.");

        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // the main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //statusText.text = "Initialization complete.";
            //RequestAndLoadAppOpenAd();
            AppOpenAdd.instance.LoadAppOpenAd();

        });


        StartCoroutine(RequestBannerAd(1));
      //  StartCoroutine(RequestRectBannerAd(1));
        StartCoroutine(RequestAndLoadInterstitialAd(2));
        StartCoroutine(RequestAndLoadRewardedAd(5));
        //StartCoroutine(RequestAndLoadRewardedInterstitialAd(3));
    }


    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion


    
    #region BANNER ADS

    IEnumerator RequestBannerAd(int time)
    {
        yield return new WaitForSeconds(time);
        PrintStatus("Requesting Banner ad.");

        // These ad units are configured to always serve test ads.
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif

        string adUnitId = AdmobBannerID;

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top); //ad position

        // Add Event Handlers
        bannerView.OnBannerAdLoaded += () =>
        {
            PrintStatus("Banner ad loaded.");
            //OnAdLoadedEvent.Invoke();
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            PrintStatus("Banner ad failed to load with error: "+ error.GetMessage());
            //OnAdFailedToLoadEvent.Invoke();
        };
        bannerView.OnAdImpressionRecorded += () =>
        {
            PrintStatus("Banner ad recorded an impression.");
        };
        bannerView.OnAdClicked += () =>
        {
            PrintStatus("Banner ad recorded a click.");
        };
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            PrintStatus("Banner ad opening.");
            //OnAdOpeningEvent.Invoke();
        };
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            PrintStatus("Banner ad closed.");
            //OnAdClosedEvent.Invoke();
        };
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Banner ad received a paid event.",
                                        adValue.CurrencyCode,
                                        adValue.Value);
            PrintStatus(msg);
        };

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
        bannerView.Hide();
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }


    public void HideBannerAd()
    {
        if(bannerView != null)
        {
            bannerView.Hide();
        }
    }

    public void ShowBannerAd()
    {
        if(PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if(bannerView != null)
            {
                bannerView.Show();
            }
            else
            {
                StartCoroutine(RequestBannerAd(1));
            }
        }
    }

    #endregion


    #region Rect BANNER ADS

    IEnumerator RequestRectBannerAd(int time)
    {
        yield return new WaitForSeconds(time);
        PrintStatus("Requesting Banner ad.");

        // These ad units are configured to always serve test ads.
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif

        string adUnitId = AdmobBannerID;

        // Clean up banner before reusing
        if (rectbannerView != null)
        {
            rectbannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        rectbannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.BottomLeft);

        // Add Event Handlers
        rectbannerView.OnBannerAdLoaded += () =>
        {
            PrintStatus("Banner ad loaded.");
            //OnAdLoadedEvent.Invoke();
        };
        rectbannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            PrintStatus("Banner ad failed to load with error: " + error.GetMessage());
            //OnAdFailedToLoadEvent.Invoke();
        };
        rectbannerView.OnAdImpressionRecorded += () =>
        {
            PrintStatus("Banner ad recorded an impression.");
        };
        rectbannerView.OnAdClicked += () =>
        {
            PrintStatus("Banner ad recorded a click.");
        };
        rectbannerView.OnAdFullScreenContentOpened += () =>
        {
            PrintStatus("Banner ad opening.");
            //OnAdOpeningEvent.Invoke();
        };
        rectbannerView.OnAdFullScreenContentClosed += () =>
        {
            PrintStatus("Banner ad closed.");
            //OnAdClosedEvent.Invoke();
        };
        rectbannerView.OnAdPaid += (AdValue adValue) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Banner ad received a paid event.",
                                        adValue.CurrencyCode,
                                        adValue.Value);
            PrintStatus(msg);
        };

        // Load a banner ad
        rectbannerView.LoadAd(CreateAdRequest());
        rectbannerView.Hide();
    }

    public void DestroyRectBannerAd()
    {
        if (rectbannerView != null)
        {
            rectbannerView.Destroy();
        }
    }


    public void HideRectBannerAd()
    {
        if (rectbannerView != null)
        {
            rectbannerView.Hide();
        }
    }

    public void ShowRectBannerAd()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (rectbannerView != null)
            {
                rectbannerView.Show();
            }
            else
            {
                StartCoroutine(RequestRectBannerAd(1));
            }
        }
    }

    #endregion

    void ApopenAd()
    {
        AppOpenAdd.isShowingAppOpenAd = false;
    }

    #region INTERSTITIAL ADS

    IEnumerator RequestAndLoadInterstitialAd(int time)
    {
        yield return new WaitForSeconds(time);
        PrintStatus("Requesting Interstitial ad.");
        //AppOpenAdd.isShowingAppOpenAd = true;
        //Invoke("ApopenAd", 7f);
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif

        string adUnitId = AdmobInterstitialID;

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        // Load an interstitial ad
        InterstitialAd.Load(adUnitId, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Interstitial ad failed to load with error: " +
                        loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Interstitial ad failed to load.");
                    return;
                }

                PrintStatus("Interstitial ad loaded.");
                interstitialAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Interstitial ad opening.");
                    //OnAdOpeningEvent.Invoke();
                    //FirebaseAnalytics.LogEvent("Ads_Impression");
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("Interstitial ad closed.");
                    //OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Interstitial ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Interstitial ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Interstitial ad failed to show with error: " +
                                error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Interstitial ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    PrintStatus(msg);
                };
            });
    }

    public void ShowInterstitialAd()
    {
        AppOpenAdd.isShowingAppOpenAd = true;
        Invoke("ApopenAd", 7f);
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            StartCoroutine(RequestAndLoadInterstitialAd(2));
        }
        else
        {
            PrintStatus("Interstitial ad is not ready yet.");
            StartCoroutine(RequestAndLoadInterstitialAd(2));
        }
    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion


    void GiveRewards(double rewardType)
    {
        Debug.Log("GiveRewards");

        if(rewardType == 1)
        {
            //GameManager.instance.ActiveBombInt = GameManager.instance.ActiveBombInt + 1;
            //GameManager.instance.bombText.text = (GameManager.instance.ActiveBombInt).ToString();
            //PlayerPrefs.SetInt("CurrentLevelDollars", PlayerPrefs.GetInt("CurrentLevelDollars") + 20);
           
            DeliveryBoxScript.instance.ClosePanel();
            DeliveryBoxScript.instance.ShowRewardAchivementText();
        }
        //if(rewardType == 2)
        //{
        //    //GameManager.instance.ActiveHammerInt = GameManager.instance.ActiveHammerInt + 1;
        //    //GameManager.instance.hammerText.text = (GameManager.instance.ActiveHammerInt).ToString();
        //}
        //if (rewardType == 3)
        //{
        //    //GameManager.instance.ActiveDicesInt = GameManager.instance.ActiveDicesInt + 1;
        //    //GameManager.instance.dicesText.text = (GameManager.instance.ActiveDicesInt).ToString();
        //}
        //if (rewardType == 4)
        //{
        //    //GameManager.instance.ActiveTrashInt = GameManager.instance.ActiveTrashInt + 1;
        //    //GameManager.instance.trashText.text = (GameManager.instance.ActiveTrashInt).ToString();
        //}
        //if (rewardType == 5)
        //{
        //    PlayerPrefs.SetInt("SPECIALPOINTS", 100);
        //}
    }





    #region REWARDED ADS

    IEnumerator RequestAndLoadRewardedAd(int time)
    {
        yield return new WaitForSeconds(time);
        PrintStatus("Requesting Rewarded ad.");
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif
        //AppOpenAdd.isShowingAppOpenAd = true;
        //Invoke("ApopenAd", 7f);
        string adUnitId = AdmobRewardedID;

        // create new rewarded ad instance
        RewardedAd.Load(adUnitId, CreateAdRequest(),
            (RewardedAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Rewarded ad failed to load with error: " +
                                loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Rewarded ad failed to load.");
                    return;
                }

                PrintStatus("Rewarded ad loaded.");
                rewardedAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Rewarded ad opening.");
                    //OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("Rewarded ad closed.");
                    //OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Rewarded ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Rewarded ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Rewarded ad failed to show with error: " +
                               error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Rewarded ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    PrintStatus(msg);
                };
            });
    }

    public void ShowRewardedAd(double rewardType)
    {
        AppOpenAdd.isShowingAppOpenAd = true;
        Invoke("ApopenAd", 7f);
        if (rewardedAd != null)
        {
            rewardedAd.Show((Reward reward) =>
            {
                GiveRewards(rewardType);
                PrintStatus("Rewarded ad granted a reward: " + reward.Amount);
                StartCoroutine(RequestAndLoadRewardedAd(30));


            });
        }
        else
        {
            PrintStatus("Rewarded ad is not ready yet.");
            StartCoroutine(RequestAndLoadRewardedAd(1));
        }
    }

    IEnumerator RequestAndLoadRewardedInterstitialAd(int time)
    {
        yield return new WaitForSeconds(time);
        PrintStatus("Requesting Rewarded Interstitial ad.");

        // These ad units are configured to always serve test ads.
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //            string adUnitId = "ca-app-pub-3940256099942544/5354046379";
        //#elif UNITY_IPHONE
        //            string adUnitId = "ca-app-pub-3940256099942544/6978759866";
        //#else
        //            string adUnitId = "unexpected_platform";
        //#endif
        //AppOpenAdd.isShowingAppOpenAd = true;
        //Invoke("ApopenAd", 7f);
        string adUnitId = AdmobRewardedInterstitialID;

        // Create a rewarded interstitial.
        RewardedInterstitialAd.Load(adUnitId, CreateAdRequest(),
            (RewardedInterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Rewarded interstitial ad failed to load with error: " +
                                loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Rewarded interstitial ad failed to load.");
                    return;
                }

                PrintStatus("Rewarded interstitial ad loaded.");
                rewardedInterstitialAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Rewarded interstitial ad opening.");
                    //OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("Rewarded interstitial ad closed.");
                    //SpinWheelScript.instace.Rotete();
                    //SpinWheelScript.watchBool = true;
                    //OnAdClosedEvent.Invoke();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Rewarded interstitial ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Rewarded interstitial ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Rewarded interstitial ad failed to show with error: " +
                                error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                                "Rewarded interstitial ad received a paid event.",
                                                adValue.CurrencyCode,
                                                adValue.Value);
                    PrintStatus(msg);
                };
            });
    }

    public void ShowRewardedInterstitialAd(double rewardType)
    {
        AppOpenAdd.isShowingAppOpenAd = true;
        Invoke("ApopenAd", 7f);
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                GiveRewards(rewardType);
               // LevelUnlockReward();
                PrintStatus("Rewarded interstitial granded a reward: " + reward.Amount);
                StartCoroutine(RequestAndLoadRewardedInterstitialAd(5));
            });
        }
        else
        {
            PrintStatus("Rewarded interstitial ad is not ready yet.");
            StartCoroutine(RequestAndLoadRewardedInterstitialAd(1));
        }
    }

    #endregion


    #region AD INSPECTOR

    public void OpenAdInspector()
    {
        PrintStatus("Opening Ad inspector.");

        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                PrintStatus("Ad inspector failed to open with error: " + error);
            }
            else
            {
                PrintStatus("Ad inspector opened successfully.");
            }
        });
    }

    #endregion

    private void PrintStatus(string message)
    {
        Debug.Log(message);
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //statusText.text = message;
        });
    }


}
