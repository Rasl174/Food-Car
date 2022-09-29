using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private TruckMovement _truckMovement;
    [SerializeField] private ButtonsMovement _foodButtons;
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private Image[] _images;

    private bool _pressedDrive = false;

    private void Update()
    {
        if (_pressedDrive && _foodSpawner.Spawned == false)
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
        if (_truckMovement.IsRightWay && _truckMovement.OnTurn == false && _foodSpawner.Spawned == false)
        {
            _truckMovement.MoveLeft();
        }
    }
    
    public void OnTurnRightDown()
    {
        if (_truckMovement.IsLeftWay && _truckMovement.OnTurn == false && _foodSpawner.Spawned == false)
        {
            _truckMovement.MoveRight();
        }
    }

    public void ActivateImages()
    {
        foreach (var image in _images)
        {
            image.enabled = true;
        }
    }

    public void DeactivateImages()
    {
        foreach (var image in _images)
        {
            image.enabled = false;
        }
    }
}
