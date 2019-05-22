using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool canPlay;
    public GameObject resultObject;
    public RoadSpawner roadSpawner;
    public CharacterMovement characterMovement;
    public SaveManager saveManager;
    public SceneAsyncStarter backToMenuController;
    public float startMoveSpeed;
    public float maxMoveSpeed;
    public float moveSpeed;
    
    public Text coinsText;
    public int coins = 0;
    public Text scoreText;
    public int score = 0;
    float scoreFloat = 0;
    
    public Skin[] skins;
    public string defaultSkinName;
    void Start()
    {
        AudioManager.Instance.PlayBackground(AudioManager.Instance.gameBackgroundSound);
        StartGame();
    }

    void Update()
    {
        if (canPlay)
        {
            AddScore(Time.deltaTime * moveSpeed);
            if (moveSpeed < maxMoveSpeed)
            {
                moveSpeed += 0.001f;
            }
        }
    }

    public void StartGame()
    {
        LoadGame();
        canPlay = true;
        moveSpeed = startMoveSpeed;
        score = 0;
        scoreFloat = 0;
        roadSpawner.StartGame();
        resultObject.SetActive(false);
        characterMovement.Respawn();
    }

    void LoadGame()
    {
        coins = saveManager.LoadCoins();
        bool skinNameLoaded = false;
        string skinName = saveManager.LoadSkin();
        if (skinName.Equals(""))
        {
            skinName = defaultSkinName;
        }
        foreach (Skin x in skins){
            if(x.skinName.Equals(skinName))
            {
                x.ShowSkin();
                characterMovement.skinAnimator = x.skinAnimator;
            }
            else
            {
                x.HideSkin();
            }
        }
         
    }

    void SaveGame()
    {
        saveManager.SaveCoins(coins);
        saveManager.SaveToMemory();
    }

    public void ShowResult()
    {
        SaveGame();
        resultObject.SetActive(true);
        
    }

    public void AddCoins(int newCoins)
    {
        coins += newCoins;
        coinsText.text = coins.ToString();
    } 

    public void AddScore(int newScore)
    {
        score += newScore;
        scoreText.text = score.ToString();
    }

    public void AddScore(float newScore)
    {
        scoreFloat += newScore;
        score = (int) scoreFloat;
        scoreText.text = score.ToString();
    }
    
    public void BackToMenu()
    {
        SaveGame();
        backToMenuController.LoadScene();
    }
}
