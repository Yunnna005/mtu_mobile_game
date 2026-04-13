using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.InputSystem;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [Header("Game IDs")]
    [SerializeField] private string _androidGameId = "6086609";
    [SerializeField] private bool _testMode = true;

    [SerializeField] private string _androidBannerId = "Banner_Android";
    [SerializeField] private string _androidInterstitialId = "Interstitial_Android";

    private string _gameId;
    private string _adUnitId;
    private string _interstitialAdUnitId;

    private bool _interstitialLoaded = false;

    public static AdsInitializer Instance;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        InitializeAds();
    }

    void Update()
    {
        // for testing
        if (_testMode)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                ShowInterstitial();
            }
        }
    }

    void InitializeAds()
    {
        _gameId = _androidGameId;
        _adUnitId = _androidBannerId;
        _interstitialAdUnitId = _androidInterstitialId;


        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads Initialized");
        ShowBanner();
        LoadInterstitial();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Initialization Failed: {error} - {message}");
    }

    #region Banner Ad
    public void ShowBanner()
    {
        Debug.Log("Showing Banner Ad");

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_LEFT);

        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, loadOptions);
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner Loaded");

        BannerOptions showOptions = new BannerOptions
        {
            showCallback = OnBannerShown,
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden
        };

        Advertisement.Banner.Show(_adUnitId, showOptions);
    }

    void OnBannerError(string message)
    {
        Debug.Log("Banner Error: " + message);
    }

    void OnBannerShown()
    {
        Debug.Log("Banner Shown");
    }

    void OnBannerClicked()
    {
        Debug.Log("Banner Clicked");
    }

    void OnBannerHidden()
    {
        Debug.Log("Banner Hidden");
    }
    public void HideBanner()
    {
        Advertisement.Banner.Hide();
        Debug.Log("Banner Hidden");
    }
    #endregion

    #region Interstitial Ad
    public void LoadInterstitial()
    {
        Debug.Log("Loading Interstitial: " + _interstitialAdUnitId);
        Advertisement.Load(_interstitialAdUnitId, this);
    }

    public void ShowInterstitial()
    {
        if (_interstitialLoaded)
        {
            Debug.Log("Showing Interstitial: " + _interstitialAdUnitId);
            Advertisement.Show(_interstitialAdUnitId, this);
        }
        else
        {
            Debug.Log("Interstitial not ready");
            LoadInterstitial();
        }
    }

    #endregion

    #region IUnityAdsLoadListener (load)
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId == _interstitialAdUnitId) _interstitialLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Failed to load {adUnitId}: {error} - {message}");

        if (adUnitId == _interstitialAdUnitId) _interstitialLoaded = false;
    }
    #endregion

    #region IUnityAdsShowListener (show)

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log("Ad Started: " + adUnitId);

        if (adUnitId == _interstitialAdUnitId) _interstitialLoaded = false;
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Ad Clicked: " + adUnitId);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState state)
    {
        Debug.Log($"Ad Complete: {adUnitId} - {state}");

        // Preload next ad
        if (adUnitId == _interstitialAdUnitId) LoadInterstitial();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad Show Failed {adUnitId}: {error} - {message}");

        if (adUnitId == _interstitialAdUnitId) LoadInterstitial();
    }

    #endregion
}