using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStarter : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    bool gameSceneLoaded;
    AsyncOperation loadingGameScene;
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        gameSceneLoaded = false;
        loadingGameScene =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadingGameScene.allowSceneActivation = false;
        while (loadingGameScene.progress < 0.9f)
        {
            var scaledPerc = 0.5f * loadingGameScene.progress / 0.9f;
            yield return null;
        }
        gameSceneLoaded = true;
        print("Scene '"+sceneName+"' - loaded!");
        StartScene();
    }

    public void StartScene()
    {
        
        StartCoroutine(WaitGameSceneLoading());
    }

    IEnumerator WaitGameSceneLoading()
    {
        while (!gameSceneLoaded)
        {
            yield return null;
        }
        loadingGameScene.allowSceneActivation = true;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
