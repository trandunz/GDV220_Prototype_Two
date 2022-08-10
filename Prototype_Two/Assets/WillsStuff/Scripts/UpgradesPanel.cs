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
