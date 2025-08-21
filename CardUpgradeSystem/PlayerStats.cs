using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private player_health_system _playerHealthSystem;
    [SerializeField] private player_controller _playerController;
    [SerializeField] private passive_sixth_reduce_damage _passiveSixthReduceDamage;
    [SerializeField] private switch_abilities _switchAbilities;
    [SerializeField] private GameObject _currentWeapon;
    private player_range_attack _currentWeaponComponent;

    private void Start()
    {
        _currentWeapon = GameObject.Find("Range_weapon");
        _currentWeaponComponent = _currentWeapon.GetComponent<player_range_attack>();
        var gunsmithShop = FindObjectOfType<gunsmith_shop>();
        if (gunsmithShop != null)
            gunsmithShop.OnWeaponChanged += UpgradeCurrentWeaponComponent;
    }

    private void UpgradeCurrentWeaponComponent(GameObject newWeapon)
    {
        _currentWeaponComponent = newWeapon.GetComponent<player_range_attack>();
    }

    public void ApplyUpgrade(PlayerUpgrade upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.IncreaseMaxHealth:
                IncreaseMaxHealth(upgrade.multiplier);
                break;
            case UpgradeType.IncreaseWeaponDamage:
                IncreaseWeaponDamage(upgrade.multiplier);
                break;
            case UpgradeType.IncreaseDashDuration:
                IncreaseDashDuration(upgrade.multiplier);
                break;
            case UpgradeType.ReduceDashCooldown:
                ReduceDashCooldown(upgrade.multiplier);
                break;
            case UpgradeType.ReduceReloadTime:
                ReduceReloadTime(upgrade.multiplier);
                break;
            case UpgradeType.IcnreaseNumberOfAmmo:
                IcnreaseNumberOfAmmo(upgrade.multiplier);
                break;
            case UpgradeType.IncreaseArmor:
                IncreaseArmor(upgrade.multiplier);
                break;
            case UpgradeType.IncreaseBulletSpeed:
                IncreaseBulletSpeed(upgrade.multiplier);
                break;
            case UpgradeType.IncreaseWeaponFireRate:
                IncreaseWeaponFireRate(upgrade.multiplier);
                break;
            case UpgradeType.IncreasePlayerMoveSpeed:
                IncreasePlayerMoveSpeed(upgrade.multiplier);
                break;
            case UpgradeType.ReduceMeleeCooldown:
                ReduceMeleeCooldown(upgrade.multiplier);
                break;
        }
    }

    public void IncreaseMaxHealth(float multiplier) => _playerHealthSystem.IncreaseMaxHealth(multiplier);
    public void IncreaseWeaponDamage(float multiplier) => _currentWeaponComponent.IncreaseWeaponDamage(multiplier);
    public void IncreaseDashDuration(float multiplier) => _playerController.IncreaseDashDuration(multiplier);
    public void ReduceDashCooldown(float multiplier) => _playerController.ReduceDashCoolDown(multiplier);
    public void ReduceReloadTime(float multiplier) => _currentWeaponComponent.ReduceReloadTime(multiplier);
    public void IcnreaseNumberOfAmmo(float multiplier) => _currentWeaponComponent.IcnreaseNumberOfAmmo(multiplier);
    public void IncreaseArmor(float multiplier) => _passiveSixthReduceDamage.IncreaseArmor(multiplier);
    public void IncreaseBulletSpeed(float multiplier) => _currentWeaponComponent.IncreaseBulletSpeed(multiplier);
    public void IncreaseWeaponFireRate(float multiplier) => _currentWeaponComponent.IncreaseWeaponFireRate(multiplier);
    public void IncreasePlayerMoveSpeed(float multiplier) => _playerController.IncreasePlayerMoveSpeed(multiplier);
    public void ReduceMeleeCooldown(float multiplier) => _switchAbilities.ReduceMeleeCooldown(multiplier);
}