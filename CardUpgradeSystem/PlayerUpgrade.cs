using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/PlayerUpgrade")]
public class PlayerUpgrade : ScriptableObject
{
    public string name;
    public Sprite icon;
    public string description;
    public UpgradeType type;
    public float multiplier;

    public void ApplyUpgrade(PlayerStats playerStats)
    {
        switch (type)
        {
            case UpgradeType.IncreaseMaxHealth:
                playerStats.IncreaseMaxHealth(multiplier);
                break;
            case UpgradeType.IncreaseWeaponDamage:
                playerStats.IncreaseWeaponDamage(multiplier);
                break;
            case UpgradeType.IncreaseDashDuration:
                playerStats.IncreaseDashDuration(multiplier);
                break;
            case UpgradeType.ReduceDashCooldown:
                playerStats.ReduceDashCooldown(multiplier);
                break;
            case UpgradeType.ReduceReloadTime:
                playerStats.ReduceReloadTime(multiplier);
                break;
            case UpgradeType.IcnreaseNumberOfAmmo:
                playerStats.IcnreaseNumberOfAmmo(multiplier);
                break;
            case UpgradeType.IncreaseArmor:
                playerStats.IncreaseArmor(multiplier);
                break;
            case UpgradeType.IncreaseBulletSpeed:
                playerStats.IncreaseBulletSpeed(multiplier);
                break;
            case UpgradeType.IncreaseWeaponFireRate:
                playerStats.IncreaseWeaponFireRate(multiplier);
                break;
            case UpgradeType.IncreasePlayerMoveSpeed:
                playerStats.IncreasePlayerMoveSpeed(multiplier);
                break;
            case UpgradeType.ReduceMeleeCooldown:
                playerStats.ReduceMeleeCooldown(multiplier);
                break;
        }
    }
}

public enum UpgradeType
{
    IncreaseMaxHealth,
    IncreaseWeaponDamage,
    IncreaseDashDuration,
    ReduceDashCooldown,
    ReduceReloadTime,

    IcnreaseNumberOfAmmo,
    IncreaseArmor,
    IncreaseBulletSpeed,
    IncreaseWeaponFireRate,
    IncreasePlayerMoveSpeed,
    ReduceMeleeCooldown
}