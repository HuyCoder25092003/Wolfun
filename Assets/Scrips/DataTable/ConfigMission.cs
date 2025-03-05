using System;
using UnityEngine;
[Serializable]
public class ConfigMissionRecord
{
    [SerializeField] int id;
    public int ID => id; 

    [SerializeField] int conditionWin;
    public int ConditionWin => conditionWin;
}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override ConfigCompare<ConfigMissionRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigMissionRecord>("id");
        return configCompare;
    }
}
