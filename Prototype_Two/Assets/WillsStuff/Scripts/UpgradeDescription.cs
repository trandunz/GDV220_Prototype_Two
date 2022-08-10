using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeDescription : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] TMPro.TextMeshProUGUI Description;
    [SerializeField] TMPro.TextMeshProUGUI PriceText;
    [SerializeField] Button UpgradeButton;
    public int Price;
    [SerializeField] int BasePrice;
    SelectUpgradePanel UpgradePanel;
    bool canUpgrade = true;
    MenuController menuController;

    private void Start()
    {
        menuController = FindObjectOfType<MenuController>();
        UpdatePriceText();
    }

    public void SetChildrenActive(bool _active)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(_active);
        }
    }


    private void Update()
    {
        Price = BasePrice  + BasePrice * UpgradePanel.GetLevel();
        UpdatePriceText();
        if (GemManager.instance.GetGemCount() >= Price && UpgradePanel.GetLevel() < 5)
        {
            UpgradeButton.interactable = true;
            canUpgrade = true;
        }
        else
        {
            UpgradeButton.interactable = false;
            canUpgrade = false;
        }
        if (canUpgrade)
            menuController.IsUpgradeAvailable = true;
    }

    public void Upgrade()
    {
        if (canUpgrade)
        {
            GemManager.instance.RemoveGems(Price);
            UpgradePanel.Upgrade();
            UpdatePriceText();
        }
    }
    public void SetUpgradePanel(SelectUpgradePanel _panel)
    {
        UpgradePanel = _panel;
    }

    void UpdatePriceText()
    {
        if (Price <= BasePrice * 5)
            PriceText.text = Price.ToString();
        else
            PriceText.text = "Max Level";
    }
}
