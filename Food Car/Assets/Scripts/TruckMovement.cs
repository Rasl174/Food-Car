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
    [SerializeField] private TruckAnimationController _animationController;
    [SerializeField] private ArrowsController _arrowsController;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnSpeedTuning;

    private Vector3 _targetRotation;
    private bool _isLeftWay = false;
    private bool _isRightWay = true;
    private bool _isMove = false;
    private bool _isTurnLeft = false;
    private bool _isTurnRight = false;
    private bool _isTurn = false;

    public bool IsLeftWay => _isLeftWay;
    public bool IsRightWay => _isRightWay;
    public bool IsMove => _isMove;

    private void Update()
    {
        if (_isTurnLeft)
        {
            if (transform.eulerAngles.y > 340)
            {
                _targetRotation.y = 270;
            }
            if (_isLeftWay && _isMove)
            {
                Turn(_turnSpeed + _turnSpeedTuning);
            }
            else if (_isRightWay && _isMove)
            {
                Turn(_turnSpeed);
            }
        }
        else if (_isTurnRight)
        {
            if(transform.eulerAngles.y < 20 && _targetRotation.y == 360)
            {
                _targetRotation.y = 0;
            }
            if (_isLeftWay && _isMove)
            {
                Turn(_turnSpeed);
            }
            else if (_isRightWay && _isMove)
            {
                Turn(_turnSpeed + _turnSpeedTuning);
            }
        }
        else if (_isTurn)
        {
            if(_isLeftWay)
            {
                if (transform.eulerAngles.y > 340)
                {
                    _targetRotation.y = 270;
                }
                if (_isMove)
                {
                    Turn(_turnSpeed + _turnSpeedTuning);
                }
            }
            if (_isRightWay)
            {
                if (transform.eulerAngles.y < 20 && _targetRotation.y == 360)
                {
                    _targetRotation.y = 0;
                }
                if (_isMove)
                {
                    Turn(_turnSpeed + _turnSpeedTuning);
                }
            }
        }
    }

    private void Turn(float speed)
    {
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, _targetRotation, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Turn>(out Turn turn))
        {
            _isTurn = turn;
            if (_isLeftWay)
            {
                _targetRotation.y = transform.eulerAngles.y - 90;
            }
            else if (_isRightWay)
            {
                _targetRotation.y = transform.eulerAngles.y + 90;
            }
        }
        if (other.TryGetComponent<TurnLeft>(out TurnLeft turnLeft))
        {
            _isTurnLeft = true;
            _targetRotation.y = transform.eulerAngles.y - 90;
        }
        if (other.TryGetComponent<TurnRight>(out TurnRight turnRight))
        {
            _isTurnRight = true;
            _targetRotation.y = transform.eulerAngles.y + 90;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Turn>(out Turn turn))
        {
            _isTurn = false;
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
            _isTurnLeft = false;
        }
        else if(other.TryGetComponent<TurnRight>(out TurnRight turnRight))
        {   
            _arrowsController.SetCrosswalkIndex(turnRight.CurrentIndex);
            turnRight.ActivateNextTurn();
            _isTurnRight = false;
        }
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
        _isMove = false;
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
