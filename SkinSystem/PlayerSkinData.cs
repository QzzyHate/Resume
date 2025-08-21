using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "Skins/Player skins")]
public class PlayerSkinData : ScriptableObject
{
    [SerializeField] private int _skinId;
    [SerializeField] private string _skinName;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _unlockedSkin;
    [SerializeField] private Sprite _lockedSkin;

    public int SkinId => _skinId;
    public string SkinName => _skinName;
    public int Price => _price;
    public Sprite UnlockedSkin => _unlockedSkin;
    public Sprite LockedSkin => _lockedSkin;
}