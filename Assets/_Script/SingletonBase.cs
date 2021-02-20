using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    public static T Instance { get; set; }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
    }
}