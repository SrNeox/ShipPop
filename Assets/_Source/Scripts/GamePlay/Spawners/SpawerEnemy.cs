using _Source.Scripts.GamePlay.StaticClass.BankResources;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

public class SpawerEnemy : MonoBehaviour
{
    [Inject] private PoolEnemies _poolEnemies;
    [Inject] private Container _container;

    [SerializeField] private Transform[] _movePoint;
    [SerializeField] private ScoreTable _scoreTable;
    [SerializeField] private EnemyShip _shipBoss;

    private Health _healthEnemy;
    private EnemyShip _enemyShip;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (LocalBank.TakeScoreBoss() != 0)
        {
            _enemyShip = _poolEnemies.GetObject();
            _enemyShip.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
        else
        {
            CreateShipBoss();
        }

        InitEnemy();
    }

    private void ReturnShip()
    {
        _healthEnemy.HealthOver -= ReturnShip;
        _enemyShip.RestoreHealth();
        _enemyShip.Buff();
        _poolEnemies.ReturnObject(_enemyShip);



        Spawn();
    }

    private void InitEnemy()
    {
        AttributeInjector.Inject(_enemyShip, _container);

        _healthEnemy = _enemyShip.GetComponent<Health>();

        _healthEnemy.HealthOver += ReturnShip;
        _scoreTable.Init(_healthEnemy);

        if (_enemyShip.MovePoints == null)
        {
            _enemyShip.SetMovePoints(_movePoint);
        }

        _enemyShip.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    private void CreateShipBoss()
    {
        _enemyShip = Instantiate(_shipBoss);
        _enemyShip.transform.SetPositionAndRotation(transform.position, transform.rotation);

        _healthEnemy = _enemyShip.GetComponent<Health>();
        _healthEnemy.HealthOver += DestroyBoss;
    }

    private void DestroyBoss()
    {
        _healthEnemy.HealthOver -= DestroyBoss;
        _enemyShip.Buff();
        Destroy(_enemyShip.gameObject);
    }
}
