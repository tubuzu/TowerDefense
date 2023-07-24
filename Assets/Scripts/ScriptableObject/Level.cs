// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave_Related;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int startHealth;
    public int startMoney;
    public List<Wave> waves;
}
