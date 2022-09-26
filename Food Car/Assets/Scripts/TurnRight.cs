using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : MonoBehaviour
{
    [SerializeField] private GameObject _nextTurn;
    [SerializeField] private int _currentTurnIndex;

    public int CurrentIndex => _currentTurnIndex;

    public void ActivateNextTurn()
    {
        _nextTurn.SetActive(true);
        gameObject.SetActive(false);
    }
}
