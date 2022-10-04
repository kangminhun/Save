using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public GameObject HitBox;
    public void onAttackTrigger()
    {
        HitBox.SetActive(true);
    }
    public void offAttackTrigger()
    {
        HitBox.SetActive(false);
    }
}
