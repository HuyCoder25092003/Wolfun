using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    public ConfigIventory configInventory;
    public ConfigShop configShop;
    public ConfigMission configMission;
    public void InitConfig(Action callback)
    {
        StartCoroutine(ProgressLoadConfig(callback));
    }
    IEnumerator ProgressLoadConfig(Action callback)
    {
        configShop = Resources.Load("Config/ConfigShop", typeof(ScriptableObject)) as ConfigShop;
        yield return new WaitUntil(() => configShop != null);

        configInventory = Resources.Load("Config/ConfigIventory", typeof(ScriptableObject)) as ConfigIventory;
        yield return new WaitUntil(() => configInventory != null);
   
        configMission = Resources.Load("Config/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);

        callback?.Invoke();
    }
}
