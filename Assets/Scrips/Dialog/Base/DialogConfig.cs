using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    PauseDialog=1,
    WinDialog = 2, 

}
public class DialogParam
{

}
public class WinDialogParam : DialogParam
{
    public ConfigMissionRecord cf_mission;
}
public class DialogConfig 
{
    public static DialogIndex[] dialogIndices =
    {
        DialogIndex.PauseDialog,
        DialogIndex.WinDialog,
    };
}
