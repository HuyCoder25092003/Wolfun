using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameView : BaseView
{
    //[SerializeField] Text moneyTxt; 
    //Tweener tween_gold;
    public override void Setup(ViewParam param)
    {
        base.Setup(param); 
        //moneyTxt.text = $"{DataController.Instance.GetGold()}$";
    }
    public void OnShop()
    {
        ViewManager.Instance.SwitchView(ViewIndex.ShopView);
    }
    public void OnInventory()
    {
        ViewManager.Instance.SwitchView(ViewIndex.InventoryView);
    }
    //public override void OnShowView()
    //{
    //    DataTrigger.RegisterValueChange(DataSchema.GOLD, DataGoldChange);
    //}
    //public override void OnHideView()
    //{
    //    DataTrigger.UnRegisterValueChange(DataSchema.GOLD, DataGoldChange);
    //}
    //void DataGoldChange(object data)
    //{
    //    int newGold = (int)data;
    //    int cur_gold = int.Parse(moneyTxt.text.Replace("$", "").Trim());

    //    tween_gold?.Kill();

    //    tween_gold = DOTween.To(() => cur_gold, x => cur_gold = x, newGold, 0.5f)
    //        .OnUpdate(() =>
    //        {
    //            moneyTxt.text = $"{cur_gold}$";
    //        });
    //}
}
