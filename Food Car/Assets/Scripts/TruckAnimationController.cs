using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAnimationController : MonoBehaviour
{
    private Animator _animator;
    private const string _drive = "Truck Drive";
    private const string _stop = "Truck Stop";
    private const string _turnLeft = "Turn Left";
    private const string _turnRight = "Turn Right";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartDriveAnimation()
    {
        _animator.Play(_drive);
    }

    public void StartStopAnimation()
    {
        _animator.Play(_stop);
    }

    public void StartAnimationTurnLeft()
    {
        _animator.Play(_turnLeft);
    }

    public void StartAnimationTurnRight()
    {
        _animator.Play(_turnRight);
    }
}
