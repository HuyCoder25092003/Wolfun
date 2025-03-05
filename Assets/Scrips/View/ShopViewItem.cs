using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopViewItem : MonoBehaviour
{
    public Image icon;
    public Text nameTxt, valueTxt;
    public ConfigShopRecord cf;
    public void SetUp(ConfigShopRecord configShopRecord)
    {
        cf = configShopRecord;
        nameTxt.text = cf.Name;
        icon.overrideSprite = SpriteLibControl.Instance.GetSpriteByName(cf.Image);
        valueTxt.text = $"{cf.Price}$";
    }
    public void OnBuy()
    {
        if (DataController.Instance.GetGold() >= int.Parse(cf.Price))
        {
            DataController.Instance.ReduceGold(int.Parse(cf.Price));
            DataController.Instance.UpdateItem(cf);
        }
    }
}
