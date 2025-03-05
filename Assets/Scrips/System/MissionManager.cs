using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : BYSingletonMono<MissionManager>
{
    public ConfigMissionRecord cf_mission;
    void Start()
    {
        cf_mission = GameManager.Instance.cur_cf_mission;
    }
    public void CheckConditionWin(int gold)
    {
        Debug.LogError($"gold :{gold}");
        if (gold >= cf_mission.ConditionWin)
        {
            WinDialogParam param = new()
            {
                cf_mission = cf_mission
            };
            DialogManager.Instance.ShowDialog(DialogIndex.WinDialog,param);
        }
    }
}
