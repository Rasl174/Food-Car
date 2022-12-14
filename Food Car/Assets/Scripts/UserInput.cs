using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private TruckMovement _truck;
    [SerializeField] private ButtonsMovement _buttons;
    [SerializeField] private FoodSpawner _foodSpawner;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && _foodSpawner.Spawned == false)
        {
            _buttons.Hide();
            _truck.Move();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            _buttons.Show();
            _truck.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.A) && _truck.IsRightWay && _truck.OnTurn == false && _foodSpawner.Spawned == false)
        {
            _truck.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) && _truck.IsLeftWay && _truck.OnTurn == false && _foodSpawner.Spawned == false)
        {
            _truck.MoveRight();
        }
    }
}
