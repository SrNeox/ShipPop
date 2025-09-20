using TMPro;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;

    private Health _health;

    private void Awake()
    {
        LocalBank.ResetScore();
    }

    private void OnEnable()
    {
        Debug.Log("Отработал на гейм овер");
        UpdateScoreText();

        if (_health != null)
            _health.HealthOver += UpdateScoreText;
    }

    private void OnDisable()
    {
        if (_health != null)
            _health.HealthOver -= UpdateScoreText;
    }

    private void UpdateScoreText()
    {
        _textScore.text = $"{LocalBank.Score}";
    }

    public void Init(Health health)
    {
        if (_health != null)
        {
            _health.HealthOver -= UpdateScoreText;
        }

        _health = health;
        _health.HealthOver += UpdateScoreText;

        UpdateScoreText();
    }
}