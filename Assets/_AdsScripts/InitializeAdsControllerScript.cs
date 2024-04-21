using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAdsControllerScript : MonoBehaviour
{
   // public string AdmobAppOpenID = null;
    public string AdmobBannerID = null;
    public string AdmobRectBannerID = null;
    public string AdmobInterstitialID = null;
    public string AdmobRewardedID = null;
    public string AdmobRewardedInterstitialID = null;

    //public string myUnityGameID = null;
    //public string myUnityRewadedID = null;

    //string testAdmobAppID =      "ca-app-pub-5453965874578277~4128806100";
    string testAdmobAppOpenID = "ca-app-pub-3940256099942544/9257395921";
    string testAdmobBannerID = "ca-app-pub-3940256099942544/6300978111";
    string testAdmobRectBannerID = "ca-app-pub-3940256099942544/6300978111";
    string testAdmobInterstitialID = "ca-app-pub-3940256099942544/1033173712";
    string testAdmobRewardedInterstitialID = "ca-app-pub-3940256099942544/5354046379";
    string testAdmobRewardedID = "ca-app-pub-3940256099942544/5224354917";

    //#if UNITY_ANDROID
    //    string testAdmobBannerID1 = "ca-app-pub-3940256099942544/6300978111";
    //#elif UNITY_IPHONE
    //        string testAdmobBannerID1 = "ca-app-pub-3940256099942544/2934735716";
    //#else
    //        string testAdmobBannerID1 = "unexpected_platform";
    //#endif

    public bool TestAds = false;



    void Awake()
    {

        if (!PlayerPrefs.HasKey("RemoveAds"))
        {
            PlayerPrefs.SetInt("RemoveAds", 0);
        }

        AdsControllerScript adsManager = AdsControllerScript.Instance;

        if (!TestAds)
        {
            //adsManager.AdmobAppID = myAdmobAppID;
          
            adsManager.AdmobBannerID = AdmobBannerID;
            adsManager.AdmobRectBannerID = AdmobRectBannerID;
            adsManager.AdmobInterstitialID = AdmobInterstitialID;
            adsManager.AdmobRewardedInterstitialID = AdmobRewardedInterstitialID;
            adsManager.AdmobRewardedID = AdmobRewardedID;
            //adsController.UnityRewadedID = myUnityRewadedID;
            //adsController.UnityGameID = myUnityGameID;

        }
        else
        {
            //adsManager.AdmobAppID = testAdmobAppID;
          
            adsManager.AdmobBannerID = testAdmobBannerID;
            adsManager.AdmobRectBannerID = testAdmobRectBannerID;
            adsManager.AdmobInterstitialID = testAdmobInterstitialID;
            adsManager.AdmobRewardedInterstitialID = testAdmobRewardedInterstitialID;
            adsManager.AdmobRewardedID = testAdmobRewardedID;
            //adsController.UnityRewadedID = myUnityRewadedID;
            //adsController.UnityGameID = myUnityGameID;

        }

        adsManager.InitializeAds();

    }


    public void ShowBanner()
    {
        AdsControllerScript.Instance.ShowBannerAd();
    }

    public void ShowInterstitial()
    {
        AdsControllerScript.Instance.ShowInterstitialAd();
    }

    public void ShowRewarded()
    {
        AdsControllerScript.Instance.ShowRewardedAd(1);
    }

    public void ShowRewardedInterstitial()
    {
        AdsControllerScript.Instance.ShowRewardedInterstitialAd(2);
    }
}
