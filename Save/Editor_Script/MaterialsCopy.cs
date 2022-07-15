using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsCopy : MonoBehaviour
{
    public Material[] objectMaterial;
    public Material[] skin;
    public void OnValidate()
    {
        for (int i = 0; i < skin.Length; i++)
        {
            skin[i].color = objectMaterial[i].color;
        }
    }
}
