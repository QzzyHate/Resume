using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _titleText;
    [SerializeField] private GameObject _descriptionText;
    private PlayerUpgrade _assignedUpgrade;
    private UpgradeMenuUI _menuUI;

    public void Setup(PlayerUpgrade upgrade, UpgradeMenuUI menuUI)
    {
        _assignedUpgrade = upgrade;
        this._menuUI = menuUI;
        _iconImage.GetComponent<Image>().sprite = upgrade.icon;
        _titleText.GetComponent<TextMeshProUGUI>().text = upgrade.name;
        _descriptionText.GetComponent<TextMeshProUGUI>().text = upgrade.description;
    }

    public void OnSelectUpgrade()
    {
        _menuUI.OnUpgradeSelected(_assignedUpgrade);
    }
}