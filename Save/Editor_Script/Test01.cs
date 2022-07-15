using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    public GameObject game;
    public GameObject clone;
    public Color color;
    public float de;
    public void testInstace()
    {
        clone=Instantiate(game, transform.position+new Vector3(0,0, de), transform.rotation);
        clone.GetComponent<MeshRenderer>().material.color = color;
    }
}
