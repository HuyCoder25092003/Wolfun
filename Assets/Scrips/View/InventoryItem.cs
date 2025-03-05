using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Image icon;
    public Text nameTxt, valueTxt;
    public ConfigIventoryRecord cf;
    public void SetUp(ConfigIventoryRecord itemRecord)
    {
        cf = itemRecord;
        nameTxt.text = cf.Name;
        valueTxt.text = $"{DataController.Instance.GetItem(cf.Name)}";
        icon.overrideSprite = SpriteLibControl.Instance.GetSpriteByName(cf.Icon);
    }
    public void UsePlant()
    {
        if(DataController.Instance.GetItem(cf.Name) > 0)
            FarmManager.Instance.SelectPlant(this);
    }
}
