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

    private void Awake()
    {
        SetAllGray(OxygenUpgradePanel);
        OxygenUpgradePanel.SetVisible();
    }

    public void SetAllGray(SelectUpgradePanel _toAvoid)
    {
        if (_toAvoid != OxygenUpgradePanel)
            OxygenUpgradePanel.SetDull();
        if (_toAvoid != ShotUpgradePanel)
            ShotUpgradePanel.SetDull();
        if (_toAvoid != DashUpgradePanel)
            DashUpgradePanel.SetDull();
        if (_toAvoid != TetherUpgradePanel)
            TetherUpgradePanel.SetDull();
    }


}
