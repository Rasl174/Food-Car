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

    private ButtonsEnabled _buttonsEnabled;
    private Vector3 _targetRotation;
    private bool _isLeftWay = false;
    private bool _isRightWay = true;
    private bool _isMove = false;
    private bool _isTurnLeft = false;
    private bool _isTurnRight = false;
    private bool _isTurn = false;
    private bool _onTurn = false;
    private bool _isWayChanged = false;

    public bool IsLeftWay => _isLeftWay;
    public bool IsRightWay => _isRightWay;
    public bool IsMove => _isMove;
    public bool OnTurn => _onTurn;
    public bool IsWayChanged => _isWayChanged;

    private void Start()
    {
        _buttonsEnabled = FindObjectOfType<ButtonsEnabled>();
    }

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
        if(other.TryGetComponent<BeforeTurn>(out BeforeTurn beforeTurn))
        {
            _onTurn = true;
        }
        if (other.TryGetComponent<Turn>(out Turn turn))
        {
            _isTurn = true;
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
            _onTurn = false;
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
            _onTurn = false;
            _arrowsController.SetCrosswalkIndex(turnLeft.CurrentIndex);
            turnLeft.ActivateNextTurn();
            _isTurnLeft = false;
        }
        else if(other.TryGetComponent<TurnRight>(out TurnRight turnRight))
        {
            _onTurn = false;
            _arrowsController.SetCrosswalkIndex(turnRight.CurrentIndex);
            turnRight.ActivateNextTurn();
            _isTurnRight = false;
        }
    }

    private IEnumerator ChangeLineLeft()
    {
        _isWayChanged = true;
        yield return new WaitForSeconds(0.8f);
        _isLeftWay = true;
        _isWayChanged = false;
    }

    private IEnumerator ChangeLineRight()
    {
        _isWayChanged = true;
        yield return new WaitForSeconds(0.8f);
        _isRightWay = true;
        _isWayChanged = false;
    }


    public void Move()
    {
        if(_isLeftWay || _isRightWay)
        {
            _buttonsEnabled.DeactivateButton();
            _animationController.StartDriveAnimation();
            transform.position = Vector3.MoveTowards(transform.position, _forwardPoint.position, _forvardMoveTime * Time.deltaTime);
            _isMove = true;
        }
    }

    public void Stop()
    {
        _buttonsEnabled.ActivateButton();
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
