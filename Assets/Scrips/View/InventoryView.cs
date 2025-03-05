using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : BaseView
{
    public InventoryItem inventoryItem;
    public RectTransform context;
    List<InventoryItem> items = new List<InventoryItem>();
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        AddItemToInventory(ConfigManager.Instance.configInventory.records);
    }
    public void OnBackGame()
    {
        ViewManager.Instance.SwitchView(ViewIndex.IngameView);
    }
    void AddItemToInventory(List<ConfigIventoryRecord> records)
    {
        DestroyItem();
        foreach (ConfigIventoryRecord record in records)
        {
            InventoryItem item = Instantiate(inventoryItem);
            item.transform.SetParent(context,false);
            item.SetUp(record);
            items.Add(item);
        }
    }
    void DestroyItem()
    {
        if (items.Count == 0)
            return;
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }
    public override void OnShowView()
    {
        base.OnShowView();
        DataTrigger.RegisterValueChange(DataSchema.COW, UpdateData);
        DataTrigger.RegisterValueChange(DataSchema.BLUEBERRYSEED, UpdateData);
        DataTrigger.RegisterValueChange(DataSchema.STRAWBERRYSEED, UpdateData);
        DataTrigger.RegisterValueChange(DataSchema.WORKER, UpdateData);
        DataTrigger.RegisterValueChange(DataSchema.TOMATOSEED, UpdateData);
    }
    public override void OnHideView()
    {
        base.OnHideView();
        DataTrigger.UnRegisterValueChange(DataSchema.COW, UpdateData);
        DataTrigger.UnRegisterValueChange(DataSchema.BLUEBERRYSEED, UpdateData);
        DataTrigger.UnRegisterValueChange(DataSchema.STRAWBERRYSEED, UpdateData);
        DataTrigger.UnRegisterValueChange(DataSchema.WORKER, UpdateData);
        DataTrigger.UnRegisterValueChange(DataSchema.TOMATOSEED, UpdateData);
    }
    void UpdateData(object data)
    {
        AddItemToInventory(ConfigManager.Instance.configInventory.records);
    }
}