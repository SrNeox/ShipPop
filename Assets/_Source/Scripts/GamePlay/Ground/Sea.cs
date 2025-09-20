using UnityEngine;

public class Sea : MonoBehaviour
{
    [SerializeField] private Renderer _seaRender;
    [SerializeField]private Vector2 _targetValue;

    private void Update()
    {
        _seaRender.material.mainTextureOffset += _targetValue;  
    }
}
