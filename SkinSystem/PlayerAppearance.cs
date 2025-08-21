using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private List<PlayerSkinData> _availableSkins;
    private const int _defaultSkinId = 0;

    public static PlayerAppearance Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        int selectedSkinId = PlayerPrefs.GetInt("SelectedSkinId", _defaultSkinId);
    }

    public void ApplySkinOrDefault(int skinId)
    {

        if (_availableSkins == null || _availableSkins.Count == 0)
        {
            return;
        }

        PlayerSkinData skin = _availableSkins.Find(s => s.SkinId == skinId);
        if (skin == null)
        {
        }
        else
        {
            bool unlocked = PlayerInventory.Instance.IsSkinUnlocked(skin.SkinId);

            if (unlocked)
            {
                SetSkin(skin);
                return;
            }
        }

        PlayerSkinData defaultSkin = _availableSkins.Find(s => s.SkinId == _defaultSkinId);
        if (defaultSkin == null)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedSkinId", _defaultSkinId);
        PlayerPrefs.Save();

        SetSkin(defaultSkin);
    }

    public void SetSkinPreview(int id)
    {
        PlayerSkinData skin = _availableSkins.Find(s => s.SkinId == id);
        if (skin != null)
            _playerSprite.sprite = skin.UnlockedSkin;
    }

    public void SetLockedSkinPreview(Sprite lockedSkin)
    {
        if (_playerSprite != null && lockedSkin != null)
            _playerSprite.sprite = lockedSkin;
    }

    public void SetSkin(PlayerSkinData skin)
    {
        _playerSprite.sprite = skin.UnlockedSkin;
    }
}