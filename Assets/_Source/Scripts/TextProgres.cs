using TMPro;
using UnityEngine;
using YG;

public class TextProgres : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private DiscovererShips _ships;

    private void Start()
    {
        _ships = GetComponentInParent<DiscovererShips>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        ShowProgres();
    }

    private void ShowProgres()
    {
        if (YG2.saves.Score < _ships.TakeTargetScore())
        {
            _text.text = $"{YG2.saves.Score} / {_ships.TakeTargetScore()}";
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
