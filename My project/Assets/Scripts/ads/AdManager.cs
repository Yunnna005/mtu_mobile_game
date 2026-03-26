using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;
    private AppOpenAd appOpenAd;
    private DateTime loadTime;
    private bool isShowingAd = false;

    private string adUnitId = "ca-app-pub-3940256099942544/9257395921";
    private string bannerIdTest = "ca-app-pub-3940256099942544/6300978111";

    public static AdManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

            Debug.Log("Google Mobile Ads initialization complete.");
            LoadAppOpenAd();
            LoadBanner();
        });
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    public void LoadBanner()
    {
        if (bannerView != null) bannerView.Destroy();

        bannerView = new BannerView(bannerIdTest, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(new AdRequest());
    }

    private void OnAppStateChanged(AppState state)
    {
        if (state == AppState.Foreground)
        {
            ShowAppOpenAd();
        }
    }

    public void LoadAppOpenAd()
    {
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        AdRequest adRequest = new AdRequest();

        AppOpenAd.Load(adUnitId, adRequest, (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogError("App Open ad failed to load: " + error);
                return;
            }

            Debug.Log("App Open ad loaded successfully.");
            appOpenAd = ad;
            loadTime = DateTime.UtcNow;

            RegisterEventHandlers(ad);
        });
    }

    public void ShowAppOpenAd()
    {
        if (IsAdAvailable && !isShowingAd)
        {
            isShowingAd = true;
            appOpenAd.Show();
        }
        else
        {
            LoadAppOpenAd();
        }
    }

    private bool IsAdAvailable
    {
        get
        {
            return appOpenAd != null
                && (DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App Open ad closed.");
            isShowingAd = false;
            LoadAppOpenAd(); // Preload the next one
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App Open ad failed to show: " + error);
            isShowingAd = false;
            LoadAppOpenAd();
        };
    }

    void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }
}

