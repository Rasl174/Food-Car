using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SellFood : MonoBehaviour
{
    [SerializeField] private RingColorChanger _ringColorChanger;
    [SerializeField] private Transform _leftSellSide;
    [SerializeField] private Transform _rightSellSide;
    [SerializeField] private Transform _jumpPosition;
    [SerializeField] private ButtonsEnabled _button;

    private FoodSpawner _foodSpawner;
    private TruckMovement _truckMovement;
    private UserInput _userInput;
    private MouseInput _mouseInput;

    private void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _foodSpawner = GetComponentInParent<FoodSpawner>();
        _truckMovement = GetComponentInParent<TruckMovement>();
        _mouseInput = FindObjectOfType<MouseInput>();
    }

    private void OnTriggerStay(Collider other)
    {
        other.TryGetComponent<WayPointMovement>(out WayPointMovement wayPointMovement);
        other.TryGetComponent<Character>(out Character character);
        if (_foodSpawner.Food.Count > 0 &&  _foodSpawner.CheckLastFood(other.gameObject) && character.IsHungry)
        {
            _button.DeactivateButtons();
            _ringColorChanger.ChangeForGreen();
            if (_truckMovement.IsMove == false)
            {
                character.PlayRunAnimation();
                wayPointMovement.enabled = false;
                _userInput.enabled = false;
                _mouseInput.DeactivateImages();
                if (_truckMovement.IsLeftWay)
                {
                    other.gameObject.transform.LookAt(_leftSellSide);
                    other.transform.position = Vector3.MoveTowards(other.transform.position, _leftSellSide.position, 5 * Time.deltaTime);
                    if (other.gameObject.transform.position == _leftSellSide.position)
                    {
                        other.gameObject.transform.LookAt(_truckMovement.transform);
                        character.PlayTakeAnimation();
                        _foodSpawner.SetCurrentFood(out GameObject current, out FoodMovement foodMovement);
                        foodMovement.SetPositions(_jumpPosition, other.transform);
                        other.enabled = false;
                    }
                }
                else if (_truckMovement.IsRightWay)
                {
                    other.gameObject.transform.LookAt(_rightSellSide);
                    other.transform.position = Vector3.MoveTowards(other.transform.position, _rightSellSide.position, 5 * Time.deltaTime);
                    if (other.gameObject.transform.position == _rightSellSide.position)
                    {
                        other.gameObject.transform.LookAt(_truckMovement.transform);
                        character.PlayTakeAnimation();
                        _foodSpawner.SetCurrentFood(out GameObject current, out FoodMovement foodMovement);
                        foodMovement.SetPositions(_jumpPosition, other.transform);
                        other.enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _ringColorChanger.SetYellowColor();
    }
}
