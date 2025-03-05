using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public int plotIndex = 0;
    [SerializeField] bool isPlanted = false; 
    [SerializeField] BoxCollider2D plantCollider;
    [SerializeField] SpriteRenderer plant;
    [SerializeField] int plantStage = 0;
    [SerializeField] float timer;
    ConfigIventoryRecord selectedItem;
    [SerializeField] TMP_Text timerTxt;
    [SerializeField] bool isDestroy = false;
    float multiplier = 1f;
    float current_timer = 0;
    private void Start()
    {
        multiplier = (DataController.Instance.GetAxe() >= 2) ? (1 - (DataController.Instance.GetAxe() - 1) * 0.1f) : 1f;
    }
    private void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataSchema.FarmEquip, UpdateMultiplier);
    }
    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.FarmEquip, UpdateMultiplier);
    }
    void UpdateMultiplier(object data)
    {
        multiplier = ((int)data >= 2) ? (1 - ((int)data - 1) * 0.1f) : 1f;
    }
    public void SetUpUI(PlantDataItem data)
    {
        if (data.name == "null" || data.timerDestroy == 0 && data.timerFinished == 0)
            return;
        foreach(var config in ConfigManager.Instance.configInventory.records)
        {
            if(config.Name.Equals(data.name))
            {
                selectedItem = config;
                isDestroy = data.isDestroy;
                if (!isDestroy)
                {
                    timer = data.timerFinished;
                    current_timer = selectedItem.TimeFinished * 60 * multiplier;
                }
                else
                {
                    timer = data.timerDestroy;
                    current_timer = selectedItem.TimeDestroy * 3600 * multiplier;
                }
                isPlanted = data.isPlanted = true;
                plantStage = data.plantStage;
                plant.gameObject.SetActive(true);
                UpdateTimerText();
                if (selectedItem.Name == "Cow")
                    UpdateAnimal();
                else
                    UpdatePlant();
                break;
            }
        }
        
    }
    void Update()
    {
        if (!FarmManager.Instance.playGame||!isPlanted || selectedItem == null)
            return;

        timer -= Time.deltaTime;
        UpdateTimerText();
        int newStage = Mathf.FloorToInt((1 - (timer / current_timer)) * selectedItem.PlantStage);
        newStage = Mathf.Clamp(newStage, 0, selectedItem.PlantStage - 1);
        if (selectedItem.Name == "Cow")
        {
            if (timer <= 0)
            {
                UpdateAnimal();
                timer = selectedItem.TimeDestroy * 3600 * multiplier;
                current_timer = timer;
            }
        }
        else
        {
            if(!isDestroy)
            {
                if (plantStage < newStage && newStage < selectedItem.PlantStage - 1)
                {
                    plantStage = newStage;
                    UpdatePlant();
                }
                else if (timer <= 0)
                {
                    plantStage = newStage = selectedItem.PlantStage - 1;
                    UpdatePlant();
                    timer = selectedItem.TimeDestroy * 3600 * multiplier;
                    current_timer = timer;
                    isDestroy = true;
                }
            }
        }
        if (timer <= 0)
        {
            if(isDestroy)
            {
                isDestroy = false;
                DataController.Instance.UpdatePlantData(plotIndex, 0, isPlanted, 0, 
                    selectedItem.Name,timer,isDestroy);
                DestroyPlant();
                timer = 0;
                current_timer = timer;
                return;
            }
        }
        if (selectedItem != null)
        {
            if(!isDestroy)
                DataController.Instance.UpdatePlantData(plotIndex, timer, isPlanted, plantStage,
                    selectedItem.Name, 0, false);
            else
                DataController.Instance.UpdatePlantData(plotIndex, 0, isPlanted, plantStage,
                    selectedItem.Name, timer, true);
        }
    }
    void OnMouseDown()
    {
        if (isPlanted)
        {
            if (selectedItem == null)
                return;
            if (plantStage == selectedItem.PlantStage - 1)
            {
                Harvest();
            }
        }
        else if (FarmManager.Instance.isPlanting && FarmManager.Instance.selectedPlant != null)
            Plant(FarmManager.Instance.selectedPlant.cf);
    }
    void UpdateTimerText()
    {
        if (timer <= 0)
        {
            timer = 0;
            timerTxt.text = "";
            return;
        }
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.CeilToInt(timer % 60);
        timerTxt.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
    void DestroyPlant()
    {
        plant.sprite = null;
        isDestroy = false;
        selectedItem = null;
        timer = 0;
        UpdateTimerText();
        FarmManager.Instance.selectedPlant = null;
        FarmManager.Instance.isPlanting = isPlanted = false;
        plantStage = 0;
    }
    void Harvest()
    {
        DataController.Instance.AddGold(selectedItem.ValueHaveProduct * selectedItem.ValueHaveBuy);
        DataController.Instance.UpdatePlantData(plotIndex,0, false, 0,
            selectedItem.Name,0,false);
        MissionManager.Instance.CheckConditionWin(DataController.Instance.GetGold());
        DestroyPlant();
    }
    void Plant(ConfigIventoryRecord item)
    {
        selectedItem = item;
        isPlanted = true;
        plant.gameObject.SetActive(true);
        if(selectedItem.Name.Equals("Cow"))
            UpdateAnimal();
        else
        {
            UpdatePlant(); 
            plantStage = 0;
        }
        timer = current_timer = selectedItem.TimeFinished * 60 * multiplier;
        DataController.Instance.ReduceItem(selectedItem);
        DataController.Instance.UpdatePlantData(plotIndex, timer, isPlanted, plantStage, 
            selectedItem.Name,0,false);

    }
    void UpdatePlant()
    {
        if (selectedItem.Name.Equals("Worker"))
        {
            plant.sprite = SpriteLibControl.Instance.GetSpriteByName(selectedItem.Name);
            return;
        }
        else if (selectedItem.Name.Equals("Fields"))
            plant.sprite = SpriteLibControl.Instance.GetSpriteByName(selectedItem.Name);
        else
        {
            plant.sprite = SpriteLibControl.Instance.GetSpriteByName(selectedItem.Name + $"_{plantStage}");
            UpdateCollider();
        }
    }
    void UpdateAnimal()
    {
        plant.sprite = SpriteLibControl.Instance.GetSpriteByName(selectedItem.Icon);
        UpdateCollider();
    }
    void UpdateCollider()
    {
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}
