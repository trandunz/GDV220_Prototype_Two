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
    [SerializeField] int Price;
    SelectUpgradePanel UpgradePanel;

    private void Start()
    {
        UpdatePriceText();
    }

    private void Update()
    {
        if (GemManager.instance.GetGemCount() >= Price)
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
