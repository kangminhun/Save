using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaterial : MonoBehaviour
{
    private Material m_Material;

    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        // Change the Color of the GameObject when the mouse hovers over it
        m_Material.color = Color.red;
    }

    private void OnDestroy()
    {
        Destroy(m_Material);
    }
}
