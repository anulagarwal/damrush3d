using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
   /* public static AdManager Instance = null;
    private InterstitialAd interstitial = null;
    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        RequestInterstitial();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }


    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1149253882244477/3792313924";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-1149253882244477/1610354780";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
    }


    public void ShowInterstital()
    {
        if (this.interstitial.IsLoaded())
        {
            //this.interstitial.Show();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
