using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Collections; // Добавляем для использования корутин

[RequireComponent(typeof(InputPlayer))]
public class DrawLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private LayerMask _planeLayerMask;
    private InputPlayer _inputPlayer;

    private bool _isDrawing = false;
    private bool _isOverDrawZone = false;
    private Coroutine _clearCoroutine; // Для отслеживания корутины очистки

    public List<Vector3> MousePositionList { get; private set; }
    public event Action OnLineDrawn;
    public bool IsDrawing => _isDrawing;

    private void Awake()
    {
        _inputPlayer = GetComponent<InputPlayer>();
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, 0.3f);
        _lineRenderer.positionCount = 0;

        MousePositionList = new List<Vector3>();
    }

    private void Start()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
    }

    private void Update()
    {
        CheckDrawLine();
        CheckDrawZone();
    }

    public void Init(LayerMask layerMask)
    {
        _planeLayerMask = layerMask;
    }

    private void CheckDrawZone()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _isOverDrawZone = Physics.Raycast(ray, Mathf.Infinity, _planeLayerMask);
    }

    private void DrawVector()
    {
        if (!_isOverDrawZone) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _planeLayerMask))
        {
            Vector3 hitPoint = hit.point;

            if (MousePositionList.Count == 0 ||
                (MousePositionList[MousePositionList.Count - 1] - hitPoint).sqrMagnitude > 0.01f)
            {
                MousePositionList.Add(hitPoint);
                _lineRenderer.positionCount = MousePositionList.Count;
                _lineRenderer.SetPosition(MousePositionList.Count - 1, hitPoint);
            }
        }
    }

    private void Clear()
    {
        MousePositionList.Clear();
        _lineRenderer.positionCount = 0;
    }

    private void CheckDrawLine()
    {
        if (_inputPlayer.Draw())
        {
            if (!_isDrawing && _isOverDrawZone)
            {
                // Если есть активная корутина очистки, останавливаем её
                if (_clearCoroutine != null)
                {
                    StopCoroutine(_clearCoroutine);
                    _clearCoroutine = null;
                }
                Clear();
                _isDrawing = true;
            }

            if (_isDrawing)
            {
                DrawVector();
            }
        }
        else if (_isDrawing) // Отпустили кнопку мыши и закончили рисование
        {
            _isDrawing = false;

            if (MousePositionList.Count > 1)
            {
                Vector3 firstPoint = MousePositionList[0];
                Vector3 lastPoint = MousePositionList[MousePositionList.Count - 1];

                MousePositionList.Clear();
                MousePositionList.Add(firstPoint);
                MousePositionList.Add(lastPoint);

                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, firstPoint);
                _lineRenderer.SetPosition(1, lastPoint);

                OnLineDrawn?.Invoke();

                // Запускаем корутину для очистки линии через 1 секунду
                _clearCoroutine = StartCoroutine(ClearAfterDelay(1f));
            }
            else
            {
                Clear();
            }
        }
    }

    // Корутина для очистки линии с задержкой
    private IEnumerator ClearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Clear();
        _clearCoroutine = null;
    }
}