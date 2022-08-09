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
    int Price;
    [SerializeField] int BasePrice;
    SelectUpgradePanel UpgradePanel;

    private void Start()
    {
        UpdatePriceText();
    }

    private void Update()
    {
        Price = BasePrice * UpgradePanel.GetLevel();
        if (GemManager.instance.GetGemCount() >= Price && UpgradePanel.GetLevel() < 5)
        {
            UpgradeButton.interactable = true;
        }
        else
        {
            UpgradeButton.interactable = false;
        }
    }

    public void Upgrade()
    {
        if (GemManager.instance.GetGemCount() >= Price)
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
        PriceText.text = Price.ToString();
    }
}
