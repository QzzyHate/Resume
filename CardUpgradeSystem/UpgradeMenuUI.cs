using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeMenuUI : MonoBehaviour
{
    [SerializeField] private UpgradeSystem _upgradeSystem;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private player_level_system _playerLevelSystem;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject _upgradeMenu;

    private pause_menu _pauseMenu;
    private cursor_manager _cursorManager;
    private bool _isUpgradeMenuOpen = false;
    private float _lastTabPressTime;
    private List<PlayerUpgrade> _currentUpgrades;

    private void Start()
    {
        _upgradeMenu.SetActive(false);
        _pauseMenu = GameObject.FindGameObjectWithTag("Player").GetComponent<pause_menu>();
        _currentUpgrades = new List<PlayerUpgrade>();
        _cursorManager = FindObjectOfType<cursor_manager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _playerLevelSystem.CanUpgrade() && !pause_menu.isPaused)
        {
            ToggleUpgradeMenu();
        }else if (Time.timeScale == 0 && _isUpgradeMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Time.realtimeSinceStartup - _lastTabPressTime > 0.5f) //Time.realtimeSinceStartup - время со старта игры, работает не зависимо от timeScale
            {
                _lastTabPressTime = Time.realtimeSinceStartup;
                CloseUpgradeMenu();
            }
        }
    }

    private void ToggleUpgradeMenu()
    {
        if (!_isUpgradeMenuOpen)
            OpenUpgradeMenu();
        else
            CloseUpgradeMenu();
    }

    public void OpenUpgradeMenu()
    {
        _isUpgradeMenuOpen = true;
        _upgradeMenu.SetActive(true);
        _cursorManager.SetMenuCursor();
        _pauseMenu.PauseGame();

        // При первом открытии или если все токены были потрачены, генерируем новые карточки
        if (_currentUpgrades.Count == 0)
            _currentUpgrades = _upgradeSystem.GetRandomUpgrades();

        DisplayCards(_currentUpgrades);
    }

    private void DisplayCards(List<PlayerUpgrade> upgrades)
    {
        // Очищаем старые карточки
        foreach (Transform child in _cardContainer)
            Destroy(child.gameObject);

        // Добавляем новые карточки в контейнер
        for (int i = 0; i < upgrades.Count; i++)
        {
            GameObject card = Instantiate(_cardPrefab, _cardContainer);
            card.GetComponent<UpgradeCard>().Setup(upgrades[i], this);

            RectTransform cardTransform = card.GetComponent<RectTransform>();
            cardTransform.anchoredPosition = new Vector2(i * 400, 0);
        }
    }

    public void OnUpgradeSelected(PlayerUpgrade selectedUpgrade)
    {
        if (_playerLevelSystem.CanUpgrade())
        {
            _playerStats.ApplyUpgrade(selectedUpgrade);
            _playerLevelSystem.UseUpgradeToken();

            if (_playerLevelSystem.CanUpgrade())
            {
                // Генерируем новые карточки, если еще остались токены
                _currentUpgrades = _upgradeSystem.GetRandomUpgrades();
                DisplayCards(_currentUpgrades);
            }
            else
            {
                // Если токены закончились, очищаем текущие карточки
                _currentUpgrades.Clear();
                CloseUpgradeMenu();
            }
        }
    }

    public void CloseUpgradeMenu()
    {
        _isUpgradeMenuOpen = false;
        _upgradeMenu.SetActive(false);
        _cursorManager.SetDefaultCursor();
        _pauseMenu.ResumeGame();
    }
}