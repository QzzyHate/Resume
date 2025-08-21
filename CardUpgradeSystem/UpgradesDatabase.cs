using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesDatabase", menuName = "Upgrades/UpgradesDatabase")]
public class UpgradesDatabase : ScriptableObject
{
    public List<PlayerUpgrade> AllUpgrades;
}