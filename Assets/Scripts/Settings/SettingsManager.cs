using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject acceptScreen;
    public SaveManager saveManager;
    public SceneAsyncStarter backToMenuController;
    public Toggle soundToggle;

    // Start is called before the first frame update
    void Start()
    {
        soundToggle.isOn = saveManager.LoadSoundEnable();
        SetSoundEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearAll()
    {
        saveManager.ClearAllData();
        saveManager.SaveToMemory();
        acceptScreen.SetActive(false);
    }

    public void DontClearAll()
    {
        acceptScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        saveManager.SaveSoundEnable(soundToggle.isOn);
        backToMenuController.LoadScene();
    }

    public void ShowAcceptScreen()
    {
        acceptScreen.SetActive(true);
    }
    
    public void SetSoundEnable()
    {
        AudioManager.Instance.SetSoundEnable(soundToggle.isOn);
        AudioManager.Instance.RefreshSoundState();
    }
}
