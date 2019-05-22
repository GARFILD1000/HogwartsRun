using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAsyncStarter : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    bool gameSceneLoaded;
    AsyncOperation loadingGameScene;
    void Start()
    {
        //StartCoroutine(LoadScene());
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        gameSceneLoaded = false;
        loadingGameScene =  SceneManager.LoadSceneAsync(sceneName);
       
        loadingGameScene.allowSceneActivation = false;
        print(sceneName+" Loading Started...");
        while (loadingGameScene.progress < 0.9f)
        {
            var scaledPerc = 0.5f * loadingGameScene.progress / 0.9f;
            print(sceneName + " Loading..." + scaledPerc);
            yield return null;
        }
        print(sceneName + " Loaded!");
        gameSceneLoaded = true;
        StartScene();
    }

    public void StartScene()
    {
        StartCoroutine(WaitGameSceneLoading());
    }

    IEnumerator WaitGameSceneLoading()
    {
        print("Starting "+ sceneName +"...");
        while (!gameSceneLoaded)
        {
            yield return null;
        }
        loadingGameScene.allowSceneActivation = true;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        print(sceneName + "Started!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
