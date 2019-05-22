using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += this.OnLoadCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {

        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        UnityEngine.Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);
    }
}
