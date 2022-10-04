using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    private Animator anim;
    private int animState;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    public int AnimState
    {
        get { return animState; }
        set { 
            animState = value;
            anim.SetInteger("AnimationPar", animState);
        }
    }
}
