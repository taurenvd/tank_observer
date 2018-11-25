﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Object
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        Instance = GetComponent<T>();
    }
}
