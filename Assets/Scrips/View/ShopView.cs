using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopView : BaseView
{
    public ShopViewItem prefab;
    public RectTransform context;
    [SerializeField] Text moneyTxt;
    Tweener tween_gold;
    List<ShopViewItem> shopViewItems = new List<ShopViewItem>();
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        moneyTxt.text = $"{DataController.Instance.GetGold()}$";
        AddItemToShop(ConfigManager.Instance.configShop.records);
    }
    void AddItemToShop(List<ConfigShopRecord> records)
    {
        DestroyItem();
        foreach (var record in records)
        {
            ShopViewItem item = Instantiate(prefab);
            item.transform.SetParent(context, false);
            item.SetUp(record);
            shopViewItems.Add(item);
        }
    }
    void DestroyItem()
    {
        if (shopViewItems.Count == 0)
            return;
        foreach (var item in shopViewItems)
        {
            Destroy(item.gameObject);
        }
        shopViewItems.Clear();
    }
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataSchema.GOLD, DataGoldChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.GOLD, DataGoldChange);
    }
    void DataGoldChange(object data)
    {
        int newGold = (int)data;
        int cur_gold = int.Parse(moneyTxt.text.Replace("$", "").Trim());

        tween_gold?.Kill();

        tween_gold = DOTween.To(() => cur_gold, x => cur_gold = x, newGold, 0.5f)
            .OnUpdate(() =>
            {
                moneyTxt.text = $"{cur_gold}$";
            });
    }
    public void OnBackGame()
    {
        ViewManager.Instance.SwitchView(ViewIndex.IngameView);
    }
}
