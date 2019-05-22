using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public string LoadSkin()
    {
        if (PlayerPrefs.HasKey("Skin"))
        {
            string skinName = PlayerPrefs.GetString("Skin","");
            return skinName;
        }
        else
        {
            return "";
        }
    }

    public int LoadCoins()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            int coinsCount = PlayerPrefs.GetInt("Coins", 0);
            return coinsCount;
        }
        else
        {
            return 0;
        }
    }

    public int LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            int scoreCount = PlayerPrefs.GetInt("Score", 0);
            return scoreCount;
        }
        else
        {
            return 0;
        }
    }
    
    public string LoadBoughtStuff()
    {
        if (PlayerPrefs.HasKey("BoughtStuff"))
        {
            string boughtStuff = PlayerPrefs.GetString("BoughtStuff", "");
            return boughtStuff;
        }
        else
        {
            return "";
        }
    }

    public bool LoadSoundEnable()
    {
        if (PlayerPrefs.HasKey("SoundEnable"))
        {
            int soundEnable = PlayerPrefs.GetInt("SoundEnable", 1);
            return soundEnable == 1;
        }
        else
        {
            return true;
        }
    }

    public void SaveSkin(string skinName)
    {
        PlayerPrefs.SetString("Skin", skinName);
    }

    public void SaveCoins(int coinsCount)
    {
        PlayerPrefs.SetInt("Coins", coinsCount);
    }

    public void SaveScore(int scoreCount)
    {
        PlayerPrefs.SetInt("Score", scoreCount);
    }

    public void SaveBoughtStuff(string boughtStuff)
    {
        PlayerPrefs.SetString("BoughtStuff", boughtStuff);
    }

    public void SaveSoundEnable(bool soundEnable)
    {
        PlayerPrefs.SetInt("SoundEnable", (soundEnable)? 1 : 0);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveToMemory()
    {
        PlayerPrefs.Save();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
