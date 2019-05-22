using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        SKIN,
        BOOSTER
    }

    public ItemType type;
    public string itemName;
    public int price;
    public Button buyButton;
    public Button activateButton;
    bool active;
    bool bought;
    string delimeter = "_";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetActive(bool activeState)
    {
            active = activeState;
            activateButton.gameObject.SetActive(bought);
            activateButton.interactable = !active;
    }
    

    public bool IsActive()
    {
        return active;
    }

    public void SetBought(bool boughtState, int coins)
    {
        bought = boughtState;
        buyButton.gameObject.SetActive(!boughtState);
        buyButton.interactable = (price <= coins);
    }

    public bool IsBought()
    {
        return bought;
    }

    public string GetFullName()
    {
        string fullName = type.ToString();
        fullName += delimeter + itemName;
        return fullName;
    }

    public bool IsEqual(string fullName)
    {
        bool equal = false;
        string[] partsOfName = fullName.Split(delimeter.ToCharArray()[0]);
        ItemType parsedType = (ItemType)Enum.Parse(typeof(ItemType), partsOfName[0]);
        if ((parsedType == type)&&(partsOfName[1] == itemName))
        {
            equal = true;
        }
        return equal;
    }

    public bool IsEqual(ItemType itemType, string name)
    {
        bool equal = false;
        if (type == itemType && name == itemName)
        {
            equal = true;
        }
        return equal;
    }

    public bool IsEqualSkin(string skinName)
    {
        bool equal = false;
        if (type == ItemType.SKIN && itemName == skinName)
        {
            equal = true;
        }
        return equal;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
