using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCustomEditor : MonoBehaviour
{
    public GameObject[] objects;
    public Color objectColor;
    public Material[] materials;
    public bool shoes;
    int num = 0;
    private void OnValidate()
    {
        if (!shoes)
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<SkinnedMeshRenderer>().material = materials[i];
            }
    }
    public void Customloafer()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (num != objects.Length)
            {
                objects[i].SetActive(false);
                objects[num].SetActive(true);
            }
        }
        num+=1;
    }
    public void ResetNum()
    {
        num = 0;
    }
    public void ColorChoice()
    {
        if(num!=0)
         materials[num-1].color = objectColor;
        else
            materials[num].color = objectColor;
    }
}
