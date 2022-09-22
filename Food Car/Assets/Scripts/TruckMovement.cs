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

    private IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(1f);
        _isMove = false;
    }

    public void Move()
    {
        transform.DOMove(_forwardPoint.position, _forvardMoveTime);
        _isMove = true;
    }

    public void Stop()
    {
        StartCoroutine(StopTimer());
    }

    public void MoveLeft()
    {
        _isLeftWay = true;
        _isRightWay = false;
        transform.DOMove(_leftPoint.position, _offsetTime);
    }

    public void MoveRight()
    {
        _isLeftWay = false;
        _isRightWay = true;
        transform.DOMove(_rightPoint.position, _offsetTime);
    }
}
