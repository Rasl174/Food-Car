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
    [SerializeField] private UserInput _userInput;
    [SerializeField] private MouseInput _mouseInput;

    private FoodSpawner _foodSpawner;
    private TruckMovement _truckMovement;
    private float _characterRunSpeed = 5;

    private void Start()
    {
        _foodSpawner = GetComponentInParent<FoodSpawner>();
        _truckMovement = GetComponentInParent<TruckMovement>();
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
                    SellFoodOnSide(other, _leftSellSide, character);
                }
                else if (_truckMovement.IsRightWay)
                {
                    SellFoodOnSide(other, _rightSellSide, character);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _ringColorChanger.SetYellowColor();
    }

    private void SellFoodOnSide(Collider characterCollider, Transform sellSide, Character character)
    {
        characterCollider.gameObject.transform.LookAt(sellSide);
        characterCollider.transform.position = Vector3.MoveTowards(characterCollider.transform.position, sellSide.position, _characterRunSpeed * Time.deltaTime);

        if (characterCollider.gameObject.transform.position == sellSide.position)
        {
            characterCollider.gameObject.transform.LookAt(_truckMovement.transform);
            character.PlayTakeAnimation();
            _foodSpawner.SetCurrentFood(out GameObject current, out FoodMovement foodMovement);
            foodMovement.SetPositions(_jumpPosition, characterCollider.transform);
            characterCollider.enabled = false;
        }
    }
}
