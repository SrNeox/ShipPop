using UnityEngine;

public class SelectShip : MonoBehaviour
{
    private int _firstShip = 0; 
    private int _secondShip = 1; 
    private int _thirdShip = 2; 

    public void SelectFirstShip()
    {
        SelectedShip.Select(_firstShip);
    }

    public void SelectSecondShip()
    {
        SelectedShip.Select(_secondShip);
    }

    public void SelectThirdShip()
    {
        SelectedShip.Select(_thirdShip);
    }
}
