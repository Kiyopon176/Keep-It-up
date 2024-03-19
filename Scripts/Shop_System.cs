using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop_System : MonoBehaviour
{
    public List<Skin> SkinsList = new List<Skin>();
    public List<Sprite> SkinsSprites = new List<Sprite>();
    public Sprite ActiveSkin;
    public List<Skin> purchasedItems;
    public List<Skin> purchasingSequence;
    

    private const string SkinsListKey = "SkinsList";
    private const string PurchasedItemsKey = "PurchasedItems";

    public void UpdateSkins(Skin skin)
    {
        foreach (var skin2 in purchasingSequence)
        {
            if (skin2 != SkinsList[^1] && skin2 != skin)
            {
                skin2.StateText.text = "Equip";
                skin2.isActiveSkin = false;
            }

            if (skin2 == skin)
            {
                
            }
        }
    }
    private void Start()
    {
        ActiveSkin = UIManager.ActiveSkin;
        LoadData();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(SkinsListKey, JsonUtility.ToJson(SkinsList));
        PlayerPrefs.SetString(PurchasedItemsKey, JsonUtility.ToJson(purchasedItems));
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(PurchasedItemsKey))
        {
            string purchasedItemsJson = PlayerPrefs.GetString(PurchasedItemsKey);
            purchasedItems = JsonUtility.FromJson<List<Skin>>(purchasedItemsJson);
        }
    }

    public void AddSkinToPurchasedItems(Skin skin)
    {
        purchasedItems.Add(skin);
        UIManager.ActiveSkin = skin.SkinSprite;
        SaveData();
    }

    public void RemoveSkinFromPurchasedItems(Skin skin)
    {
        purchasedItems.Remove(skin);
        SaveData();
    }
}