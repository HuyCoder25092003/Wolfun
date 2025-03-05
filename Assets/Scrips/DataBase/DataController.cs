using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataController : BYSingletonMono<DataController>
{
    public DataModel dataModel;
    public void InitData(Action callback)
    {
        dataModel.InitData(callback);
    }
    public PlayerInfo GetPlayerInfo()
    {
        PlayerInfo info = dataModel.ReadData<PlayerInfo>(DataSchema.INFO);
        return info;
    }
    public List<PlantDataItem> GetPlant()
    {
        return dataModel.ReadData<List<PlantDataItem>>(DataSchema.PLANT);
    }
    public int GetGold()
    {
        return dataModel.ReadData<int>(DataSchema.GOLD);
    }
    public int GetFields()
    {
        return dataModel.ReadData<int>(DataSchema.FIELDS);
    }
    public int GetWorker()
    {
        return dataModel.ReadData<int>(DataSchema.WORKER);
    }
    public int GetTomatoSeed()
    {
        return dataModel.ReadData<int>(DataSchema.TOMATOSEED);
    }
    public int GetStrawberrySeed()
    {
        return dataModel.ReadData<int>(DataSchema.STRAWBERRYSEED);
    }
    public int GetBlueberrySeed()
    {
        return dataModel.ReadData<int>(DataSchema.BLUEBERRYSEED);
    }
    public int GetCow()
    {
        return dataModel.ReadData<int>(DataSchema.COW);
    }
    public int GetAxe()
    {
        return dataModel.ReadData<int>(DataSchema.FarmEquip);
    }
    public void AddGold(int number)
    {
        int gold = GetGold();
        gold += number;
        if (gold < 0)
            gold = 0;
        dataModel.UpdateData(DataSchema.GOLD, gold);
    }
    public void AddAxe(int number)
    {
        int value = GetAxe();
        value += number;
        if (value < 0)
            value = 0;
        dataModel.UpdateData(DataSchema.FarmEquip, value);
    }
    public void ReduceGold(int number)
    {
        int gold = GetGold();
        gold -= number;
        if (gold < 0)
            gold = 0;
        dataModel.UpdateData(DataSchema.GOLD, gold);
    }
    public void UpdateItem(ConfigShopRecord configShop)
    {
        switch (configShop.Name)
        {
            case "Fields":
                int value = GetFields();
                value += configShop.Value;
                dataModel.UpdateData(DataSchema.FIELDS, value);
                break;
            case "Worker":
                int value1 = GetWorker();
                value1 += configShop.Value;
                dataModel.UpdateData(DataSchema.WORKER, value1);
                break;
            case "Tomato":
                int value2 = GetTomatoSeed();
                value2 += configShop.Value;
                dataModel.UpdateData(DataSchema.TOMATOSEED, value2);
                break;
            case "Blueberry":
                int value3 = GetBlueberrySeed();
                value3 += configShop.Value;
                dataModel.UpdateData(DataSchema.BLUEBERRYSEED, value3);
                break;
            case "Strawberry":
                int value4 = GetStrawberrySeed();
                value4 += configShop.Value;
                dataModel.UpdateData(DataSchema.STRAWBERRYSEED, value4);
                break;
            case "Cow":
                int value5 = GetCow();
                value5 += configShop.Value;
                dataModel.UpdateData(DataSchema.COW, value5);
                break;
            case "Farm Equipment":
                int value6 = GetAxe();
                value6 += configShop.Value;
                dataModel.UpdateData(DataSchema.FarmEquip, value6);
                break;
            default:
                break;
        }
    }
    public void ReduceItem(ConfigIventoryRecord configInventory)
    {
        switch (configInventory.Name)
        {
            case "Fields":
                int value = GetFields();
                value -= 1;
                dataModel.UpdateData(DataSchema.FIELDS, value);
                break;
            case "Worker":
                int value1 = GetWorker();
                value1 -= 1;
                dataModel.UpdateData(DataSchema.WORKER, value1);
                break;
            case "Tomato":
                int value2 = GetTomatoSeed();
                value2 -= 1;
                dataModel.UpdateData(DataSchema.TOMATOSEED, value2);
                break;
            case "Blueberry":
                int value3 = GetBlueberrySeed();
                value3 -= 1;
                dataModel.UpdateData(DataSchema.BLUEBERRYSEED, value3);
                break;
            case "Strawberry":
                int value4 = GetStrawberrySeed();
                value4 -= 1;
                dataModel.UpdateData(DataSchema.STRAWBERRYSEED, value4);
                break;
            case "Cow":
                int value5 = GetCow();
                value5 -= 1;
                dataModel.UpdateData(DataSchema.COW, value5);
                break;
            default:
                break;
        }
    }
    public int GetItem(string name)
    {
        switch (name)
        {
            case "Fields":
                return GetFields();
            case "Worker":
                return GetWorker();
            case "Tomato":
                return GetTomatoSeed();
            case "Blueberry":
                return GetBlueberrySeed();
            case "Strawberry":
                return GetStrawberrySeed();
            case "Cow":
                return GetCow();
            default:
                break;
        }
        return 0;
    }
    public void UpdatePlantData(int index, float time, bool isPlanted, int plantStage, string name, float timeDestroy, bool isDestroy)
    {
        List<PlantDataItem> planItems = GetPlant();
        for (int i = 0; i < planItems.Count; i++)
        {
            if (planItems[i].id == index)
            {
                planItems[i].timerFinished = time;
                planItems[i].isPlanted = isPlanted;
                planItems[i].plantStage = plantStage;
                planItems[i].name = name;
                planItems[i].timerDestroy = timeDestroy;
                planItems[i].isDestroy = isDestroy;
                break;
            }
        }
        dataModel.UpdateData(DataSchema.PLANT, planItems);
    }
}
