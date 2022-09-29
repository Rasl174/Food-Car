using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsEnabled : MonoBehaviour
{
    [SerializeField] private Button _buttonHotDog;
    [SerializeField] private TruckMovement _truckMovement;

    private void Update()
    {
        if (_truckMovement.IsMove)
        {
            _buttonHotDog.enabled = false;
        }
    }

    public void ActivateButton()
    {
        _buttonHotDog.enabled = true;
    }

    public void DeactivateButton()
    {
        _buttonHotDog.enabled = false;
    }
}
