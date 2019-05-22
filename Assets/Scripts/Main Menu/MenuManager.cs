using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SceneAsyncStarter gameSceneStarter;
    public SceneAsyncStarter shopSceneStarter;
    public SceneAsyncStarter optionsSceneStarter;
    public ExitController exitController;
    public GameObject menuCamera;
    public SaveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBackground(AudioManager.Instance.menuBackgroundSound);
        AudioManager.Instance.SetSoundEnable(saveManager.LoadSoundEnable());
        AudioManager.Instance.RefreshSoundState();

        if (saveManager.LoadSkin() == "")
        {
            saveManager.SaveSkin("Harry");
            saveManager.SaveBoughtStuff(saveManager.LoadBoughtStuff()+" SKIN_Harry");
        }
    }

    public void Game()
    {
        gameSceneStarter.LoadScene();
    } 

    public void Shop()
    {
        shopSceneStarter.LoadScene();
    }

    public void Settings()
    {
        optionsSceneStarter.LoadScene();
    }

    public void Exit()
    {
        exitController.Exit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
