using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private TruckMovement _truckMovement;
    [SerializeField] private ButtonsMovement _foodButtons;

    private bool _pressedDrive = false;

    private void Update()
    {
        if (_pressedDrive)
        {
            _truckMovement.Move();
            _foodButtons.Hide();
        }
    }

    public void OnDriveDown()
    {
        _pressedDrive = true;
    }

    public void OnDriveUp()
    {
        _pressedDrive = false;
        _truckMovement.Stop();
        _foodButtons.Show();
    }

    public void OnTurnLeftDown()
    {
        if (_truckMovement.IsRightWay)
        {
            _truckMovement.MoveLeft();
        }
    }
    
    public void OnTurnRightDown()
    {
        if (_truckMovement.IsLeftWay)
        {
            _truckMovement.MoveRight();
        }
    }
}
