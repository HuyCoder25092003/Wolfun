using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DialogManager : BYSingletonMono<DialogManager>
{
    public event Action<BaseDialog> OnDialogHide;
    public event Action<BaseDialog> OnDialogShow;
    public RectTransform anchor_dl;
    private Dictionary<DialogIndex, BaseDialog> dic = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> ls_dialog = new List<BaseDialog>();
    public BaseDialog curDialog = null;
    void Start()
    {
        foreach(DialogIndex index in DialogConfig.dialogIndices)
        {
            string name_dialog = index.ToString();
            GameObject dl_obj = Instantiate(Resources.Load("Dialog/" + name_dialog, typeof(GameObject))) as GameObject;
            dl_obj.transform.SetParent(anchor_dl, false);

            BaseDialog dl = dl_obj.GetComponent<BaseDialog>();
            dic.Add(index, dl);
            dl_obj.SetActive(false);
        }
    }
    public void ShowDialog(DialogIndex index,DialogParam param=null,Action callback=null)
    {
        BaseDialog dl = dic[index];

        Action cb = () =>
        {
            callback?.Invoke();
        };
        dl.gameObject.SetActive(true);
        dl.Setup(param);
        OnDialogShow?.Invoke(dl);
        dl.SendMessage("ShowDialog", (object)cb, SendMessageOptions.RequireReceiver);
        if(!ls_dialog.Contains(dl))
        {
            ls_dialog.Add(dl);
        }
    }
 

    public void HideDialog(DialogIndex index,  Action callback=null)
    {
        BaseDialog dl = dic[index];

        Action cb = () =>
        {
            callback?.Invoke();
            dl.gameObject.SetActive(false); 
            OnDialogHide?.Invoke(dl);

        };
        dl.SendMessage("HideDialog", (object)cb, SendMessageOptions.RequireReceiver);
        if (ls_dialog.Contains(dl))
        {
            ls_dialog.Remove(dl);
        }
    }
    public void HideAllDialog()
    {
        foreach(BaseDialog dl in ls_dialog)
        {
            Action cb = () =>
            {
                dl.gameObject.SetActive(false);
                OnDialogHide?.Invoke(dl);
            };
            dl.SendMessage("HideDialog", (object)cb, SendMessageOptions.RequireReceiver);
        }
        ls_dialog.Clear();
    }
   
}
