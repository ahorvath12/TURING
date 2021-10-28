using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character",  order = 1)]
public class Character : ScriptableObject
{
    public TextAsset twineText;
    public bool isHuman;
    public string name;
    public string descText;

    public string GetInfo()
    {
        return name + "\n" + descText;
    }
}
