using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FarmManager : BYSingletonMono<FarmManager>
{
    public bool isPlanting = false;
    public InventoryItem selectedPlant;
    public List<PlotManager> list = new List<PlotManager>();
    public bool playGame = false;
    void Start()
    {
        List<PlantDataItem> datas = DataController.Instance.GetPlant();
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < datas.Count; j++)
                if (i == datas[j].id - 1)
                {
                    list[i].SetUpUI(datas[j]);
                    list[i].plotIndex = datas[j].id;
                    break;
                }
        }
    }
    public void SelectPlant(InventoryItem newPlant)
    {
        if (newPlant.cf.Name.Equals("Fields") || newPlant.cf.Name.Equals("Worker"))
            return;
        if (selectedPlant == newPlant)
        {
            selectedPlant = null;
            isPlanting = false;   
        }
        else
        {
            selectedPlant = newPlant;
            isPlanting = true;
        }
    }
}
