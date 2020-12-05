using System.Collections;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public System.Action onInitialized = delegate { };
    public static AppController Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadSetup();
    }

    void LoadSetup()
    {
#if LOG_ENGINE
        Debug.Log("AppController : Load Specific Setup");
#endif
        StartCoroutine(LoadSetupCoroutine());
    }

    IEnumerator LoadSetupCoroutine()
    {
#if LOG_ENGINE
        Debug.Log("AppController : Load Async Setup");
#endif
        yield return new WaitForEndOfFrame();
        OnInitialized();
#if LOG_ENGINE
        Debug.Log("AppController : Load Menu Scene");
#endif
        SceneLoadingController.Instance.LoadScene(GameScenes.Main, true);
    }

    void OnInitialized()
    {
#if LOG_ENGINE
        Debug.Log("AppController : Call OnInitialized Event");
#endif
        onInitialized();
    }
}