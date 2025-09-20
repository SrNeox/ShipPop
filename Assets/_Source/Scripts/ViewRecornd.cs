using TMPro;
using UnityEngine;
using YG;

public class ViewRecornd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text.SetText($"{YG2.saves.Score}");
    }
}
