using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestory : MonoBehaviour
{
    [HideInInspector]
    public Inventory inventory;
    [HideInInspector]
    public MainSlot mainSlot;
    public static Dontdestory instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        inventory = GetComponent<Inventory>();
        mainSlot = GetComponent<MainSlot>();
    }
}

