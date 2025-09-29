using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PoolBullet _poolBullet;
    private float _speed;
    private float _damage;
    private GameObject _shooter;
    private bool _isArcShot = false;
    private Vector3 _arcTarget;
    private float _arcHeight;
    private float _arcProgress;
    private Vector3 _arcStartPosition;
    private GameObject _landingIndicator;
    private float _arcDuration;

    private DrawLine _drawLine; 

    private void Start()
    {
        _poolBullet = FindObjectOfType<PoolBullet>();
        _drawLine = FindObjectOfType<DrawLine>();
    }

    private void Update()
    {
        if (_isArcShot)
            MoveArc();
        else
            Move();

        CheckLineIntersection();
    }

    public void Init(Transform position, float damage, float speedBullet, GameObject shooter)
    {
        _speed = speedBullet;
        _damage = damage;
        transform.SetPositionAndRotation(position.position, position.rotation);
        _isArcShot = false;
        _shooter = shooter;
    }

    public void InitArc(Transform position, float damage, float duration, Vector3 target, float height, GameObject indicator, GameObject shooter)
    {
        _damage = damage;
        _arcDuration = duration;
        transform.position = position.position;
        _isArcShot = true;
        _arcTarget = target;
        _arcHeight = height;
        _arcProgress = 0f;
        _arcStartPosition = transform.position;
        _landingIndicator = indicator;
        _shooter = shooter;
    }

    private void Move() => transform.Translate(_speed * Time.deltaTime * Vector3.forward, Space.Self);

    private void MoveArc()
    {
        _arcProgress += Time.deltaTime / _arcDuration;

        if (_arcProgress < 1.0f)
        {
            Vector3 currentPos = Vector3.Lerp(_arcStartPosition, _arcTarget, _arcProgress);
            currentPos.y += _arcHeight * Mathf.Sin(_arcProgress * Mathf.PI);
            transform.position = currentPos;
        }
        else
        {
            transform.position = _arcTarget;

            if (_landingIndicator != null)
                Destroy(_landingIndicator);

            if (_poolBullet != null)
                _poolBullet.ReturnObject(this);
        }
    }

    private void CheckLineIntersection()
    {
        if (_drawLine == null) return;

        Vector3? lastPoint = _drawLine.GetLastPoint();
        if (lastPoint.HasValue)
        {
            // ���� ���� ������ � ��������� ����� �����
            if (Vector3.Distance(transform.position, lastPoint.Value) < 0.2f)
            {
                _drawLine.Clear();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _shooter) return;

        if (_isArcShot && _landingIndicator != null)
            Destroy(_landingIndicator);

        if (collider.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            if (_poolBullet != null)
                _poolBullet.ReturnObject(this);
        }

        if (collider.TryGetComponent(out Bullet bullet))
        {
            if (_poolBullet != null)
                _poolBullet.ReturnObject(this);
        }
    }
}
