using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] SelectUpgradePanel OxygenUpgradePanel;
    [SerializeField] SelectUpgradePanel ShotUpgradePanel;
    [SerializeField] SelectUpgradePanel DashUpgradePanel;
    [SerializeField] SelectUpgradePanel TetherUpgradePanel;

    private void Start()
    {
        SetAllGray();
        OxygenUpgradePanel.SetVisible();
    }

    public void SetAllGray()
    {
        OxygenUpgradePanel.SetDull();
        ShotUpgradePanel.SetDull();
        DashUpgradePanel.SetDull();
        TetherUpgradePanel.SetDull();
    }
}
