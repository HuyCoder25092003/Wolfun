using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigIventoryRecord
{
    [SerializeField] int id;
    public int Id => id;

    [SerializeField] string name;
    public string Name => name;

    [SerializeField] string icon;
    public string Icon => icon;

    [SerializeField] int value;
    public int Value => value;

    [SerializeField] int valueHaveBuy;
    public int ValueHaveBuy => valueHaveBuy;

    [SerializeField] int plantStage;
    public int PlantStage => plantStage;

    [SerializeField] float timeFinished;
    public float TimeFinished => timeFinished;

    [SerializeField] int valueBorrow;
    public int ValueBorrow => valueBorrow;

    [SerializeField] float timeDestroy;
    public float TimeDestroy => timeDestroy;

    [SerializeField] int valueProduct;
    public int ValueHaveProduct => valueProduct;
}

public class ConfigIventory : BYDataTable<ConfigIventoryRecord>
{
    public override ConfigCompare<ConfigIventoryRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigIventoryRecord>("id");
        return configCompare;
    }
}
