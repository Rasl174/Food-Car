using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Truck _truck;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _truck.Move();
        }
        else if (Input.GetKeyDown(KeyCode.A) && _truck.IsRightWay)
        {
            _truck.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) && _truck.IsLeftWay)
        {
            _truck.MoveRight();
        }
    }
}
