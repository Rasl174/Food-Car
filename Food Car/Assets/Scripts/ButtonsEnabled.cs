using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsEnabled : MonoBehaviour
{
    [SerializeField] private Button[] _foodButtons;
    [SerializeField] private TruckMovement _truckMovement;

    private void Update()
    {
        if (_truckMovement.IsMove)
        {
            DeactivateButtons();
        }
    }

    public void ActivateButtons()
    {
        foreach (var button in _foodButtons)
        {
            button.enabled = true;
        }
    }

    public void DeactivateButtons()
    {
        foreach (var button in _foodButtons)
        {
            button.enabled = false;
        }
    }
}
