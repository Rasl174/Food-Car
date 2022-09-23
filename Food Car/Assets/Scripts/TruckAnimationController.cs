using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartDriveAnimation()
    {
        _animator.Play("Truck Drive");
    }

    public void StartStopAnimation()
    {
        _animator.Play("Truck Stop");
    }
}
