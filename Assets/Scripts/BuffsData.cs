using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffsData", menuName = "Buffs Data")]
public class BuffsData : ScriptableObject
{
    public int intValue;
    public string stringValue;
    public GameObject prefabReference;
}
