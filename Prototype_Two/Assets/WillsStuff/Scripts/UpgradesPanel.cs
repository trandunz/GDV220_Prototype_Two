using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] SelectUpgradePanel OxygenUpgradePanel;
    [SerializeField] SelectUpgradePanel DashUpgradePanel;
    [SerializeField] SelectUpgradePanel TetherUpgradePanel;

    void OnEnable()
    {
        SetAllGray(OxygenUpgradePanel);
        OxygenUpgradePanel.SetVisible();
    }

    public List<int> GetAllPrices()
    {
        List<int> Prices = new List<int>();
        foreach (var description in GetComponentsInChildren<UpgradeDescription>())
        {
            Prices.Add(description.Price);
        }
        return Prices;
    }

    public List<int> GetAllLevels()
    {
        List<int> levels = new List<int>();
        foreach (var description in GetComponentsInChildren<UpgradeDescription>())
        {
            levels.Add(description.UpgradePanel.GetLevel());
        }
        return levels;
    }

    public void SetAllGray(SelectUpgradePanel _toAvoid)
    {
        if (_toAvoid != OxygenUpgradePanel)
            OxygenUpgradePanel.SetDull();
        if (_toAvoid != DashUpgradePanel)
            DashUpgradePanel.SetDull();
        if (_toAvoid != TetherUpgradePanel)
            TetherUpgradePanel.SetDull();
    }
}
