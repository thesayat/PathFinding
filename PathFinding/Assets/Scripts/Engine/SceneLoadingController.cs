using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingController : MonoBehaviour
{
    public bool IsLoading { get; private set; }
    public float Progress { get; private set; }
    public System.Action<GameScenes> onLoadingComplete = delegate { };
    public System.Action<GameScenes> onLoadingBegin = delegate { };

    public static SceneLoadingController Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        IsLoading = false;
        Progress = 0f;
    }

    public void LoadMainScene(GameScenes scene)
    {
        StartCoroutine(SceneLoad(GameScenes.Main));
    }

    public void LoadScene(GameScenes scene, bool withUnload)
    {
        StartCoroutine(SceneLoad(scene, withUnload));
    }

    IEnumerator SceneLoad(GameScenes scene, bool withUnload = false)
    {
        if (withUnload)
        {
            Scene sceneToUnload = SceneManager.GetActiveScene();
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneToUnload.name);
            while (unloadScene != null && !unloadScene.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        string sceneToLoad = GetSceneName(scene);
        Debug.Log($"costam {1}");
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);


        while (loading != null && !loading.isDone)
        {
            Progress = loading.progress;

            yield return new WaitForSeconds(0.2f);
        }

        Scene loadedScene = SceneManager.GetSceneByName(sceneToLoad);
        SceneManager.SetActiveScene(loadedScene);
    }

    private string GetSceneName(GameScenes scene)
    {
        switch (scene)
        {
            case GameScenes.Main:
                return "002_Main";
            default:
                return string.Empty;
        }
    }
}