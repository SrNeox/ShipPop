using UnityEngine;

public class TryShoot : MonoBehaviour
{
    [SerializeField] private int _delayTime = 3;
    [SerializeField] private int _shootTime = 3;
    [SerializeField] private float _speedScale = 0.3f;
    [SerializeField] private float _speedTransformAim = 15f;
    [SerializeField] private AimPrefab _aimPrefab;
    [SerializeField] private GameObject _hintCanvas;

    private DrawLine _drawLine;
    private Player _player;
    private EnemyMover _enemyMover;
    private EnemyShoot _enemyShoot;
    private Transform _currnetTarget;
    private Health _enemyHealth;

    private float _currentTime;
    private bool _isShootingPhase;
    private float _idleTime = 0f;
    private bool _hintShown = false;
    private bool _hasHintCanvas;

    public Player Player => _player ??= FindObjectOfType<Player>();
    public DrawLine DrawLine => _drawLine ??= FindObjectOfType<DrawLine>();

    private void Start()
    {
        _hasHintCanvas = _hintCanvas != null;

        SetAimOnTarget(Player.transform);
        StartWaitingPhase();
        FindAndSubscribeToEnemyHealth();
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            if (_isShootingPhase)
            {
                StartWaitingPhase();
                SetAimOnTarget(Player.transform);
            }
            else
            {
                _enemyMover = FindObjectOfType<EnemyMover>();
                StartShootingPhase();
                SetAimOnTarget(_enemyMover.transform);
            }
        }

        MoveAimTarget();
        CheckIdleTime();
    }

    private void CheckIdleTime()
    {
        if (_isShootingPhase)
        {
            if (DrawLine.IsDrawing)
            {
                _idleTime = 0f;
                if (_hintShown && _hasHintCanvas)
                {
                    _hintCanvas.SetActive(false);
                    _hintShown = false;
                }
            }
            else
            {
                _idleTime += Time.deltaTime;

                if (_idleTime > 2f && !_hintShown && _hasHintCanvas)
                {
                    _hintCanvas.SetActive(true);
                    _hintShown = true;
                }
            }
        }
        else
        {
            _idleTime = 0f;
            if (_hintShown && _hasHintCanvas)
            {
                _hintCanvas.SetActive(false);
                _hintShown = false;
            }
        }
    }

    private void StartWaitingPhase()
    {
        _isShootingPhase = false;
        _currentTime = _delayTime;

        if (DrawLine != null)
            DrawLine.enabled = false;

        if (_enemyShoot != null)
            _enemyShoot.enabled = true;

        _idleTime = 0f;
        if (_hasHintCanvas)
        {
            _hintCanvas.SetActive(false);
            _hintShown = false;
        }
    }

    private void StartShootingPhase()
    {
        _isShootingPhase = true;
        _currentTime = _shootTime;

        if (DrawLine != null)
            DrawLine.enabled = true;

        if (_enemyShoot != null)
            _enemyShoot.enabled = false;

        _idleTime = 0f;

        if (_hasHintCanvas)
        {
            _hintCanvas.SetActive(false);
            _hintShown = false;
        }
    }

    private void SetAimOnTarget(Transform target)
    {
        if (_aimPrefab == null) return;

        _aimPrefab.transform.SetParent(null);
        _currnetTarget = target;
    }

    private void MoveAimTarget()
    {
        if (_aimPrefab == null || _currnetTarget == null) return;

        _aimPrefab.transform.position = Vector3.Lerp(_aimPrefab.transform.position, new Vector3(_currnetTarget.position.x, _aimPrefab.transform.position.y, _currnetTarget.position.z), _speedTransformAim * Time.deltaTime);
    }

    private void FindAndSubscribeToEnemyHealth()
    {
        _enemyShoot = FindObjectOfType<EnemyShoot>();

        if (_enemyShoot != null)
        {
            _enemyHealth = _enemyShoot.GetComponent<Health>();
            if (_enemyHealth != null)
                _enemyHealth.HealthOver += OnEnemyDie;
        }
    }

    private void OnEnemyDie()
    {
        SetAimOnTarget(Player.transform);
        StartWaitingPhase();

        if (_enemyHealth != null)
        {
            _enemyHealth.HealthOver -= OnEnemyDie;
            _enemyHealth = null;
        }

        FindAndSubscribeToEnemyHealth();
    }

    private void OnDestroy()
    {
        if (_enemyHealth != null)
            _enemyHealth.HealthOver -= OnEnemyDie;
    }
}