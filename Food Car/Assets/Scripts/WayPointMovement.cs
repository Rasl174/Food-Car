using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private float _speed;

    private int _currentPoint;

    private void Update()
    {
        Transform target = _positions[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        transform.LookAt(target);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _positions.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}
