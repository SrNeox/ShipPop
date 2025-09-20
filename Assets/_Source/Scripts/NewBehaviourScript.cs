using UnityEngine;
using YG;

public class sad : MonoBehaviour
{
    [SerializeField] private int sETSOCER;

    private void Awake()
    {
        YG2.saves.Score = sETSOCER;
    }
}
