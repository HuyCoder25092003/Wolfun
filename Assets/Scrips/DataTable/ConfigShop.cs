using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ConfigShopRecord
{
    [SerializeField]
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }

    [SerializeField]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
   
    [SerializeField]
    private string image;
    public string Image
    {
        get
        {
            return image;
        }
    }

    [SerializeField]
    private string price;
    public string Price
    {
        get
        {
            return price;
        }
    }
   
    [SerializeField]
    private int value;
    public int Value
    {
        get
        {
            return value;
        }
    }

}
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override ConfigCompare<ConfigShopRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigShopRecord>("id");
        return configCompare;
    }

   
}
