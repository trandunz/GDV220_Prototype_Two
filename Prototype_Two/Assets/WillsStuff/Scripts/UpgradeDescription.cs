using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeDescription : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] TMPro.TextMeshProUGUI Description;
    [SerializeField] TMPro.TextMeshProUGUI PriceText;
    [SerializeField] int Price;
    SelectUpgradePanel UpgradePanel;

    private void Start()
    {
        UpdatePriceText();
    }
    public void Upgrade()
    {
        UpgradePanel.Upgrade();
        UpdatePriceText();
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
