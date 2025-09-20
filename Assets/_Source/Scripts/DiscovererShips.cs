using UnityEngine;
using UnityEngine.UI;
using YG;

public class DiscovererShips : MonoBehaviour
{
    [SerializeField] private int _targetScoreUnlock;

    private RawImage _rawImage;
    private Button _button;

    private void Start()
    {
        _rawImage = GetComponentInChildren<RawImage>();
        _button = GetComponentInChildren<Button>();

        UnLockShips();
    }

    public int TakeTargetScore() => _targetScoreUnlock;

    private void UnLockShips()
    {
        if (_targetScoreUnlock <= YG2.saves.Score)
        {
            OnActive();

        }
        else
        {
            OffActive();
        }
    }

    private void OnActive()
    {
        _button.enabled = true;
        _rawImage.color = Color.white;
        Destroy(this);
    }

    private void OffActive()
    {
        _button.enabled = false;
        _rawImage.color = Color.black;
    }
}
