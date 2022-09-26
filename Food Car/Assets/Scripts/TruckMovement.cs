using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TruckMovement : MonoBehaviour
{
    [SerializeField] private Transform _forwardPoint;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private float _forvardMoveTime;
    [SerializeField] private float _offsetTime;
    [SerializeField] private float _turnTime;
    [SerializeField] private float _turnTimeTuning;
    [SerializeField] private TruckAnimationController _animationController;
    [SerializeField] private ArrowsController _arrowsController;

    private bool _isLeftWay = false;
    private bool _isRightWay = true;
    private bool _isMove = false;

    public bool IsLeftWay => _isLeftWay;
    public bool IsRightWay => _isRightWay;
    public bool IsMove => _isMove;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Turn>(out Turn turn))
        {
            if (_isLeftWay)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, -90, 0), _turnTime);
            }
            else if (_isRightWay)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 90, 0), _turnTime);
            }
        }
        if(other.TryGetComponent<TurnLeft>(out TurnLeft turnLeft))
        {
            if (_isLeftWay)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, -90, 0), _turnTime);
            }
            else
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, -90, 0), _turnTime + _turnTimeTuning);
            }
        }
        if(other.TryGetComponent<TurnRight>(out TurnRight turnRight))
        {
            if (_isRightWay)
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 90, 0), _turnTime);
            }
            else
            {
                transform.DORotate(transform.eulerAngles + new Vector3(0, 90, 0), _turnTime + _turnTimeTuning);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Turn>(out Turn turn))
        {
            if (_isLeftWay)
            {
                _arrowsController.SetCrosswalkIndex(turn.CurrentLeftIndex);
                turn.ActivateNextLeftTurn();
            }
            else if (_isRightWay)
            {
                _arrowsController.SetCrosswalkIndex(turn.CurrentRightIndex);
                turn.ActivateNextRightTurn();
            }
        }
        else if(other.TryGetComponent<TurnLeft>(out TurnLeft turnLeft))
        {
            _arrowsController.SetCrosswalkIndex(turnLeft.CurrentIndex);
            turnLeft.ActivateNextTurn();
        }
        else if(other.TryGetComponent<TurnRight>(out TurnRight turnRight))
        {   
            _arrowsController.SetCrosswalkIndex(turnRight.CurrentIndex);
            turnRight.ActivateNextTurn();
        }
    }

    private IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(1f);
        _isMove = false;
    }

    private IEnumerator ChangeLineLeft()
    {
        yield return new WaitForSeconds(0.8f);
        _isLeftWay = true;
    }

    private IEnumerator ChangeLineRight()
    {
        yield return new WaitForSeconds(0.8f);
        _isRightWay = true;
    }


    public void Move()
    {
        if(_isLeftWay || _isRightWay)
        {
            _animationController.StartDriveAnimation();
            transform.DOMove(_forwardPoint.position, _forvardMoveTime);
            _isMove = true;
        }
    }

    public void Stop()
    {
        _animationController.StartStopAnimation();
        StartCoroutine(StopTimer());
    }

    public void MoveLeft()
    {
        _isRightWay = false;
        transform.DOMove(_leftPoint.position, _offsetTime);
        _animationController.StartAnimationTurnLeft();
        StartCoroutine(ChangeLineLeft());
    }

    public void MoveRight()
    {
        _isLeftWay = false;
        transform.DOMove(_rightPoint.position, _offsetTime);
        _animationController.StartAnimationTurnRight();
        StartCoroutine(ChangeLineRight());
    }
}
