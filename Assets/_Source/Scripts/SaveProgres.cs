using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class SaveProgres : MonoBehaviour
{
    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    private void Start() => _button.onClick.AddListener(CompleteRound);

    public void CompleteRound()
    {
        if (YG2.reviewCanShow)
        {
            YG2.ReviewShow();
        }
    }
}
