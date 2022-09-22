using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform[] _positions;
    private int _currentPoint;

    private void Awake()
    {
        enabled = false;
        _positions = new Transform[2];
    }

    private void Update()
    {
        Transform target = _positions[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;
        }
    }

    public void SetPositions(Transform jumpPosition, Transform character)
    {
        enabled = true;
        _positions[0] = jumpPosition;
        _positions[1] = character;
    }
}
