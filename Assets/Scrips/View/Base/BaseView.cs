using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    private BaseViewAnimation viewAnimation;
    public ViewIndex viewIndex;
    private void Awake()
    {
        viewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
    }
    // Start is called before the first frame update
    public virtual void Setup(ViewParam param)
    {

    }
    private void HideView(Action callback)
    {
        viewAnimation.OnHideAnimation(() =>
        {
            gameObject.SetActive(false);

            OnHideView();
            callback?.Invoke();
        });
       
    }
    public virtual void OnShowView()
    {

    }
    private void ShowView(object val)
    {
        viewAnimation.OnShowAnimation(() =>
        {
            Action callback = (Action)val;
            OnShowView();

            callback?.Invoke();
        });
       
    }
    public virtual void OnHideView()
    {

    }
  
}
