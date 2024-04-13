using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    [SerializeField] public int SkinID;
    [SerializeField] private bool isBought;
    [SerializeField] private string spriteResourcePath;
    [SerializeField] private TMP_Text priceText;

    public TMP_Text stateText;
    public Shop_System ShopSystem;

    private Button childButton;
    private TMP_Text childText;
    private Image childImage;
    [SerializeField] private int price;
    
    private void Start()
    {
        Transform firstChild = transform.Find("BuyButton");
        Transform priceComponent = transform.Find("Price");

        priceText = priceComponent.GetComponent<TMP_Text>();
            
        priceText.text = "Price: " + price.ToString();
        if (firstChild != null)
        {
            Transform secondChild = firstChild.Find("StateText");
            stateText = secondChild.GetComponent<TMP_Text>();
        }

        LoadSprite();
        isBought = IsSkinBought(SkinID);
        if (isBought)
        {
            print("is bought");
            UpdateState();
        }
    }

    public void buy()
    {
        if (!isBought && PlayerPrefs.GetInt("overAllScore") >= price)
        {
            ShopSystem.UpdateLists(this);
            PlayerPrefs.SetInt("overAllScore", PlayerPrefs.GetInt("overAllScore") - price);
            isBought = true;
            GamePlay.OnBought?.Invoke();
            UpdateState();
        }
        else if(isBought)
        {
            PlayerPrefs.SetString("ActiveSkinPath", spriteResourcePath);
            ShopSystem.UpdateStateTexts(this);
            UpdateState();
        }
    }

    private bool IsSkinBought(int skinID)
    {
        return ShopSystem.purchasedSkinsList.Contains(skinID);
    }

    void UpdateState()
    {
        if (PlayerPrefs.GetString("ActiveSkinPath") == spriteResourcePath)
        {
            stateText.text = "Equipped";
        }
        else
        {
            stateText.text = "Equip";
        }
    }

    void LoadSprite()
    {
        spriteResourcePath = "Skins/" + this.name;
        Transform childTransform = transform.Find("Skin");

        if (childTransform != null)
        {
            childImage = childTransform.GetComponent<Image>();
        }

        childImage.sprite = Resources.Load<Sprite>(spriteResourcePath);

    }
}