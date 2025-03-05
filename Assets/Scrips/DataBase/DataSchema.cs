using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSchema 
{
    public const string INFO = "info";
    public const string INVENTORY = "inventory";
    public const string GOLD = "inventory/gold";
    public const string FIELDS = "inventory/fields";
    public const string WORKER = "inventory/worker";
    public const string TOMATOSEED = "inventory/tomatoSeed";
    public const string TOMATO = "inventory/tomato";
    public const string STRAWBERRYSEED = "inventory/strawberrySeed";
    public const string STRAWBERRY = "inventory/strawberry";
    public const string BLUEBERRYSEED = "inventory/blueberrySeed";
    public const string BLUEBERRY = "inventory/blueberry";
    public const string COW = "inventory/cow";
    public const string GALLON = "inventory/gallon";
    public const string FarmEquip = "inventory/levelFarmEquip";
    public const string PLANT = "inventory/plantItemsData";
}

[Serializable]
public class PlayerData
{
    [SerializeField]
    public PlayerInfo info;
    [SerializeField]
    public PlayerInventory inventory;
    public PlayerMissionData missionData;
}
[Serializable]
public class PlayerInfo
{
    public string nickname;
    public int level;
}
[Serializable]
public class PlayerInventory
{
    public int gold;
    public int fields;
    public int worker;
    public int tomatoSeed;
    public int tomato;
    public int blueberrySeed;
    public int blueberry;
    public int strawberrySeed;
    public int strawberry;
    public int cow;
    public int gallon;
    public int levelFarmEquip;
    [SerializeField] public List<PlantDataItem> plantItemsData;
}
[Serializable]
public class PlayerMissionData
{
    public int currentMission;
}
[Serializable]
public class PlantDataItem
{
    public int id;
    public int plantStage;
    public float timerFinished, timerDestroy;
    public string name = null;
    public bool isPlanted = false, isDestroy = false; 
}
