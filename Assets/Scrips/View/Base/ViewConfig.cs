using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView=1,
    ShopView = 2,
    IngameView = 3,
    InventoryView = 4,
}
public class ViewParam
{

}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices = {
        ViewIndex.EmptyView, 
        ViewIndex.ShopView,
        ViewIndex.IngameView,
        ViewIndex.InventoryView,
    };
}
 