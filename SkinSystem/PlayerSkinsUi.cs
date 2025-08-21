using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinsUi : MonoBehaviour
{
    [SerializeField] private PlayerAppearance _playerAppearance;
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private List<PlayerSkinData> _allSkins;

    private int _currentIndex = 0;
    private int _selectedSkinId = -1;

    private const string SelectedSkinKey = "Selected_Skin";

    public static PlayerSkinsUi Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadSelectedSkin();
        ShowSkin(_currentIndex);

        _leftArrow.onClick.AddListener(() => ChangeSkin(-1));
        _rightArrow.onClick.AddListener(() => ChangeSkin(1));
        _buyButton.onClick.AddListener(TryBuySkin);
    }

    private void ChangeSkin(int direction)
    {
        _currentIndex += direction;

        if (_currentIndex < 0) _currentIndex = _allSkins.Count - 1;
        else if(_currentIndex >= _allSkins.Count) _currentIndex = 0;

        ShowSkin(_currentIndex);
    }

    private void ShowSkin(int index)
    {
        PlayerSkinData skinData = _allSkins[index];
        bool unlocked = PlayerInventory.Instance.IsSkinUnlocked(skinData.SkinId);

        if (unlocked)
        {
            _buyButton.gameObject.SetActive(false);
            _price.gameObject.SetActive(false);
            _selectedSkinId = skinData.SkinId;
            _playerAppearance.SetSkin(skinData);
            SaveSelectedSkin();

            FindObjectOfType<PlayerAppearance>().SetSkinPreview(_selectedSkinId);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
            _price.gameObject.SetActive(true);
            _price.text = $"{skinData.Price} Монет";
            _playerAppearance.SetLockedSkinPreview(skinData.LockedSkin);
        }
    }

    private void TryBuySkin()
    {
        PlayerSkinData skinData = _allSkins[_currentIndex];

        if (PlayerInventory.Instance.GetPlayerMoney() >= skinData.Price)
        {
            PlayerInventory.Instance.UseMoney(skinData.Price);
            PlayerInventory.Instance.UnlockSkin(skinData.SkinId);
            ShowSkin(_currentIndex);
        }
        else
        {
        }
    }

    private void SaveSelectedSkin()
    {
        PlayerPrefs.SetInt(SelectedSkinKey, _selectedSkinId);
        PlayerPrefs.Save();
    }

    private void LoadSelectedSkin()
    {
        _selectedSkinId = PlayerPrefs.GetInt(SelectedSkinKey, 0);
        _currentIndex = _allSkins.FindIndex(s =>  s.SkinId == _selectedSkinId);
        if(_currentIndex == -1) _currentIndex = 0;
    }

    public int GetEquippedSkin() => _selectedSkinId;
}