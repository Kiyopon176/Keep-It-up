using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop_System : MonoBehaviour
{
    public List<Skin> SkinsList;
    public List<int> purchasedSkinsList;
    private const string purchasedListKey = "PurchasedList";

    private void Start()
    {
        if (PlayerPrefs.GetString(purchasedListKey) != "")
        {
            LoadPurchasedList();
        }
    }

    public void UpdateLists(Skin skin)
    {
        if (!purchasedSkinsList.Contains(skin.SkinID))
        {
            purchasedSkinsList.Add(skin.SkinID);
        }
        Save();
    }

    public void UpdateStateTexts(Skin skin)
    {
        foreach (var skin1 in SkinsList)
        {
            if (skin1 != skin)
            {
                skin1.stateText.text = "Equip";
            }
        }
    }

    private void Save()
    {
        string numbersString = string.Join(",", purchasedSkinsList);

        PlayerPrefs.SetString(purchasedListKey, numbersString);
        PlayerPrefs.Save();

        Debug.Log("Numbers saved.");
    }

    public void LoadPurchasedList()
    {
        string numbersString = PlayerPrefs.GetString(purchasedListKey);
        print(numbersString);
        string[] numberStrings = numbersString.Split(',');

        foreach (string str in numberStrings)
        {
            if (int.TryParse(str, out var number))
            {
                purchasedSkinsList.Add(number);
            }
            else
            {    
                Debug.LogError("Failed to parse: " + str);
            }
        }
    }
}