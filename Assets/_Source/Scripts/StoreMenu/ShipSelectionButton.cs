using UnityEngine;
using UnityEngine.UI;

public class ShipSelectionButton : MonoBehaviour
{
    [SerializeField] private ShipMenu[] _ships;
    [SerializeField] private Color _backGroundColor = new Color(1f, 1f, 1f, 118f / 255f);
    [SerializeField] private Color _backGroundColorSelect = Color.white;

    private Image[] _backgroundImages;

    private void Start()
    {
        _backgroundImages = new Image[_ships.Length];

        for (int i = 0; i < _ships.Length; i++)
        {
            if (_ships[i] == null) continue;

            _backgroundImages[i] = _ships[i].GetComponentInChildren<Image>();

            if (_backgroundImages[i] != null)
            {
                _backgroundImages[i].color = _backGroundColor;
            }
        }

        Select(0); 
    }

    public void Select(int numberShip)
    {
        Debug.Log(numberShip);

        for (int i = 0; i < _ships.Length; i++)
        {
            if (_backgroundImages[i] == null) continue;

            _backgroundImages[i].color = (i == numberShip)
                ? _backGroundColorSelect
                : _backGroundColor;
        }

        SelectedShip.Select(numberShip);
    }
}
