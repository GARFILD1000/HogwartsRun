using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUnloaderAsync : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    bool gameSceneUnloaded;
    AsyncOperation unloadingGameScene;
    void Start()
    {
        //StartCoroutine(LoadScene());
    }

    public void SetActiveScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    

    // Update is called once per frame
    void Update()
    {

    }
}

