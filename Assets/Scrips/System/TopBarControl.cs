using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class TopBarControl : MonoBehaviour
{
    public Text gold_lb;
    [SerializeField] RectTransform parent;
    public GameObject leftObj;
    Tween tween_gold,tween_farm_equip; 
    [SerializeField] float moveUp;
    [SerializeField] float moveDown;
    [SerializeField] float durationMoveUp;
    [SerializeField] float durationMoveDown;
    public Image uiFarmEquip;
    public Text valueFarmEquipTxt;
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        ViewManager.Instance.OnViewShow += ViewManager_OnViewShow;
        ViewManager.Instance.OnViewHide += ViewManager_OnViewHide;
        DialogManager.Instance.OnDialogShow += DialogManager_OnDialogShow;
        DialogManager.Instance.OnDialogHide += DialogManager_OnDialogHide;
    }
    void SettingsParent(bool playGame)
    {
        if (playGame)
            parent.DOAnchorPosY(moveDown, durationMoveDown);
        else
            parent.DOAnchorPosY(moveUp, durationMoveUp);
    }
    private void DialogManager_OnDialogHide(BaseDialog obj)
    {
        SettingsParent(false);
    }

    private void DialogManager_OnDialogShow(BaseDialog obj)
    {
        SettingsParent(true);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 1)
        {
            gold_lb.text = $"{DataController.Instance.GetGold()}$";
            uiFarmEquip.sprite = SpriteLibControl.Instance.GetSpriteByName("Axe");
            valueFarmEquipTxt.text = $"Level {DataController.Instance.GetAxe()}";
            DataTrigger.RegisterValueChange(DataSchema.GOLD, DataGoldChange);
            DataTrigger.RegisterValueChange(DataSchema.FarmEquip, DataFarmEquipChange);
        }
    }
    void DataFarmEquipChange(object data)
    {
        int newFarmEquip = (int)data;
        int curFarmEquip = DataController.Instance.GetAxe();
        tween_farm_equip?.Kill();
        tween_farm_equip = DOTween.To(() => curFarmEquip, x => curFarmEquip = x, newFarmEquip, 0.5f).OnUpdate(() =>
        {
            valueFarmEquipTxt.text = $"Level {curFarmEquip}";
        });
    }
    void DataGoldChange(object data)
    {
        int new_gold = (int)data;
        int cur_gold = DataController.Instance.GetGold();
        tween_gold?.Kill();
        tween_gold = DOTween.To(() => cur_gold, x => cur_gold = x, new_gold, 0.5f).OnUpdate(() =>
        {
            gold_lb.text = $"{cur_gold}$";
        });
    }
    void ViewManager_OnViewHide(BaseView obj)
    {
        //if (obj.viewIndex == ViewIndex.IngameView || obj.viewIndex == ViewIndex.InventoryView)
        //{
        //    parent.DOAnchorPosY(500, 1);
        //    leftObj.SetActive(false);
        //}
    }
    private void ViewManager_OnViewShow(BaseView obj)
    {
        if (obj.viewIndex == ViewIndex.IngameView || obj.viewIndex == ViewIndex.InventoryView)
        {
            SettingsParent(true);
        }
        else if(obj.viewIndex == ViewIndex.ShopView)
        {
            SettingsParent(false);
        }
    }
}
