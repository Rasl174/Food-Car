using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    [SerializeField] private GameObject _nextLeftTurn;
    [SerializeField] private GameObject _nextRightTurn;
    [SerializeField] private int _currentLeftTurnIndex;
    [SerializeField] private int _currentRightTurnIndex;

    public int CurrentLeftIndex => _currentLeftTurnIndex;
    public int CurrentRightIndex => _currentRightTurnIndex;

    public void ActivateNextLeftTurn()
    {
        _nextLeftTurn.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ActivateNextRightTurn()
    {
        _nextRightTurn.SetActive(true);
        gameObject.SetActive(false);
    }
}
