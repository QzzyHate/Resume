using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private UpgradesDatabase _upgradesDatabase;
    [SerializeField] private int _cardsToDisplay = 3;

    public List<PlayerUpgrade> GetRandomUpgrades()
    {
        List<PlayerUpgrade> randomUpgrades = new List<PlayerUpgrade>();

        //Юзаем while, через for даёт ошибку на много строк
        while (randomUpgrades.Count < _cardsToDisplay)
        {
            PlayerUpgrade randomUpgrade = _upgradesDatabase.AllUpgrades[Random.Range(0, _upgradesDatabase.AllUpgrades.Count)];

            // Проверяем, что карты нет в списке
            if (!randomUpgrades.Contains(randomUpgrade))
                randomUpgrades.Add(randomUpgrade);
        }
        return randomUpgrades;
    }
}
