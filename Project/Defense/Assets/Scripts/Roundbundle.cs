using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Roundbundle", menuName = "Roundbundle")]
public class Roundbundle : ScriptableObject
{
    public RoundScriptable[] rounds;
    public int count=0;
}
