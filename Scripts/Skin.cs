using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skin : MonoBehaviour
{
    public Sprite SkinSprite;
    public int price;
    public TMP_Text PriceText,StateText;
    public bool is_bought = false;
    public bool isActiveSkin = false;
    public Image Lock;
    [SerializeField] private Sprite openLock, closeLock;

    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private Shop_System shopSystem;

    private void Start()
    {
        for (int i = 0; i < shopSystem.SkinsList.Count; i++)
        {
            if (shopSystem.ActiveSkin == this.SkinSprite)
            {
                isActiveSkin = true;
                break;
            }
        }

        PriceText.text = "Price" + price.ToString();
        foreach (var obj in shopSystem.purchasedItems)
        {
            if (obj == this)
            {
                is_bought = true;
            }
        }
        if (is_bought)
        {
            StateText.text = isActiveSkin ? "Equipped" : "Equip";
            Lock.sprite = openLock;
        }
        else
        {
            StateText.text = "Buy";
            Lock.sprite = closeLock;
        }
    }

    public void buy()
    {
        if (is_bought && !isActiveSkin)
        {
            shopSystem.UpdateSkins(this);
            UIManager.ActiveSkin = this.SkinSprite;
            isActiveSkin = true;
            StateText.text = "Equipped";
        }
        else if(uiManager.overAllScore >= price && !is_bought)
        {
            shopSystem.UpdateSkins(this);
            shopSystem.purchasingSequence.Add(this);
            UIManager.ActiveSkin = this.SkinSprite;
            isActiveSkin = true;
            StateText.text = "Equipped";
            uiManager.overAllScore -= price;
            shopSystem.AddSkinToPurchasedItems(this);
            is_bought = true;
            UpdateSkinState();
        }
    }

    private void UpdateSkinState()
    {
        if (is_bought)
        {
            StateText.text = isActiveSkin ? "Equipped" : "Equip";
            Lock.sprite = openLock;
        }
        else
        {
            StateText.text = "Buy";
            Lock.sprite = closeLock;
        }
    }
}