using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bank : MonoBehaviour
{
    public int interest;
    public float interestTime;
    [SerializeField]
    private ParticleSystem effect;
    public void Start()
    {
        StartCoroutine(PlusInterest());
    }
    IEnumerator  PlusInterest()
    {
        yield return new WaitForSeconds(interestTime);
        PlayerStats.Money += interest;
        effect.Play();
        yield return new WaitForSeconds(1f);
        effect.Stop();
        yield return StartCoroutine(PlusInterest());
    }
}
