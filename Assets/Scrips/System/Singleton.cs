using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BYSingletonMono<T>: MonoBehaviour where T: MonoBehaviour
{
    private static T singleton;
    public static T Instance
    {
        private set { }
        get
        {
            if(BYSingletonMono<T>.singleton==null)
            {
                BYSingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
                if(BYSingletonMono<T>.singleton == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "@[" + typeof(T).Name + "]";
                    BYSingletonMono<T>.singleton = obj.AddComponent<T>();

                }
            }
            return BYSingletonMono<T>.singleton;
        }
    }
    private void Reset()
    {
        gameObject.name = typeof(T).Name;
    }
}