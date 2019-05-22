using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int coins = 0;
    public SceneAsyncStarter backToMenuController;
    public Text coinsText;
    public ShopItem[] shopItems;

    string delimeter = " ";


    public SaveManager saveManager;

    void Start()
    {
        LoadShop();
    }

    public void LoadShop(){
        coins = saveManager.LoadCoins();
        coinsText.text = coins.ToString();

        string boughtStuffString = saveManager.LoadBoughtStuff();
        string[] boughtStuffs = boughtStuffString.Split(delimeter.ToCharArray()[0]);
        for (int i = 0; i < shopItems.Length; i++)
        {
            bool bought = false;
            foreach (string x in boughtStuffs)
            {
                if (x.Equals(shopItems[i].GetFullName()))
                {
                    bought = true;
                }
            }
            shopItems[i].SetBought(bought, coins);
        }

        string activeSkin = saveManager.LoadSkin();
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].IsEqualSkin(activeSkin))
            {
                shopItems[i].SetBought(true, coins);
                shopItems[i].SetActive(true);
            }
            else if (shopItems[i].type == ShopItem.ItemType.SKIN)
            {
                shopItems[i].SetActive(false);
            }
        }
        print("Магазин загружен");
    }

    public void SaveShop()
    {
        saveManager.SaveCoins(coins);

        string boughtStuff = "";
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].IsBought())
            {
                boughtStuff += shopItems[i].GetFullName() + " ";
            }
            if (shopItems[i].IsActive() && shopItems[i].type == ShopItem.ItemType.SKIN)
            {
                saveManager.SaveSkin(shopItems[i].itemName);
            }
        }
        if (boughtStuff.LastIndexOf(" ") >= 0)
        {
            boughtStuff.Remove(boughtStuff.LastIndexOf(" "), 1);
        }
        saveManager.SaveBoughtStuff(boughtStuff);
        saveManager.SaveToMemory();
        print("Магазин сохранён");
    }
    
    public void Buy(ShopItem shopItem)
    {
        shopItem.SetBought(true, coins);
        if (shopItem.IsBought())
        {
            coins -= shopItem.price;
        }
        SaveShop();
        LoadShop();
    }

    public void Activate(ShopItem shopItem)
    {
        foreach (ShopItem x in shopItems){
                x.SetActive(false);
        }
        shopItem.SetActive(true);
        SaveShop();
        LoadShop();
    }

    public void BackToMenu()
    {
        backToMenuController.LoadScene();
    }

    



    void Update()
    {

    }

}
