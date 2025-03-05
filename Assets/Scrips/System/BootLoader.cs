using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        yield return new WaitForSeconds(1);
        ConfigManager.Instance.InitConfig(InitData);
       
     
    }
    private void InitData()
    {
        DataController.Instance.InitData(() =>
        {
            GameManager.Instance.cur_cf_mission = ConfigManager.Instance.configMission.GetRecordBykeySearch(1);
            LoadSceneManager.Instance.LoadSceneByName("Ingame", LoadSceneDone);
        });
    }
   

    public void LoadSceneDone()
    {
        FarmManager.Instance.playGame = true;
        ViewManager.Instance.SwitchView(ViewIndex.IngameView);
    }
}
