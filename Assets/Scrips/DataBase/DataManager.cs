using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : BYSingletonMono<DataManager>
{
    public List<PlantDataItem> plantDataItems;
    public PlayerData InitData()
    {
        PlayerData playerData = new();
        PlayerInfo info = new()
        {
            nickname = "Huy",
            level = 1
        };
        playerData.info = info;

        PlayerInventory inventory = new()
        {
            gold = 100,
            fields = 3,
            tomatoSeed = 10,
            tomato = 0,
            blueberry = 0,
            blueberrySeed = 10,
            cow = 2,
            worker = 1,
            strawberry = 0,
            strawberrySeed = 2,
            levelFarmEquip = 1,
            plantItemsData = plantDataItems

        };
        playerData.inventory = inventory;

        PlayerMissionData missionData = new()
        {
            currentMission = 1,
        };
        playerData.missionData = missionData;
        return playerData;
    }
}
