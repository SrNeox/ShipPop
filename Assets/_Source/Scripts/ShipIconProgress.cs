using UnityEngine;
using UnityEngine.UI;

public class ShipIconProgress : MonoBehaviour
{
    private const string NameAnimTrigger = "Play";
    private const string NameAnimTrigger2 = "Return";

    [SerializeField] private Animator[] _shipIcon;
    [SerializeField] private RawImage[] _shipIcon2;
    [SerializeField] private Image _icon;

    private Health _health;
    private Vector3 _startPosition;
    private int _currnetShipIcon = 0;

    public Health Health => _health ??= SearchHealth();

    private void Start()
    {
        _startPosition = _icon.transform.position;
        MoveImageTarget();
        _health = SearchHealth();
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.HealthOver -= PlayAnimation;
    }

    private void PlayAnimation()
    {
        Reset();

        if (_currnetShipIcon < _shipIcon.Length)
        {
            _shipIcon[_currnetShipIcon].SetTrigger(NameAnimTrigger);
            _currnetShipIcon++;
            _health.HealthOver -= PlayAnimation;
            MoveImageTarget();
            SearchHealth();
        }

        Invoke(nameof(Reset), 1);
    }

    private void Reset()
    {
        if (_currnetShipIcon == _shipIcon.Length)
        {
            _currnetShipIcon = 0;

            foreach (var ship in _shipIcon)
            {
                ship.SetTrigger(NameAnimTrigger2);
            }
        }
    }

    private Health SearchHealth()
    {
        EnemyMover enemy = FindAnyObjectByType<EnemyMover>();
        Health health = enemy.GetComponent<Health>();

        health.HealthOver += PlayAnimation;

        return health;
    }

    private void MoveImageTarget()
    {
        if (_currnetShipIcon < _shipIcon.Length)
            _icon.transform.position = _shipIcon2[_currnetShipIcon].transform.position;
        else
            _icon.transform.position = _startPosition;
    }
}
