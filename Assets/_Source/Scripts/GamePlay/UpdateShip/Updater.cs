using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;
    [SerializeField] private Button _button;

    private int _priceUpdate = 5;

    private void Awake()
    {
        _button.onClick.AddListener(LevelUp);
    }

    public void LevelUp()
    {
        if(BankResource.Coin > _priceUpdate)
        {
            UpdatePlayerStats();
            _priceUpdate++;
        }
    }

    private void UpdatePlayerStats()
    {
        _playerInfo.SetMaxHealth(_playerInfo.MaxHealth + 10);
        _playerInfo.SetSpeed(_playerInfo.Speed + 0.05f);
        _playerInfo.SetShootDelay(_playerInfo.ShootDelay + 0.95f);
        _playerInfo.SetSpeedBullet(_playerInfo.SpeedBullet + 0.5f);
        _playerInfo.SetDamage(_playerInfo.Damage + 10);
        _playerInfo.SetLevel();
    }
}
