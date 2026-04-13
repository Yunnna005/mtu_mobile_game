using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [Header("Game IDs")]
    [SerializeField] private string _androidGameId = "6086609";
    [SerializeField] private bool _testMode = true;
    [SerializeField] private string _androidBannerId = "Banner_Android";

    private string _gameId;
    private string _adUnitId;

    void Awake()
    {
        InitializeAds();
    }

    void InitializeAds()
    {
        _gameId = _androidGameId;
        _adUnitId = _androidBannerId;

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads Initialized");
        ShowBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Initialization Failed: {error} - {message}");
    }

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

}