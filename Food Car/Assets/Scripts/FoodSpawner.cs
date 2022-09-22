using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private GameObject _hotDog;
    [SerializeField] private Vector3 _finishTuningPosition;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private float _moveTime;
    [SerializeField] private Transform _foodSellPoint;
    [SerializeField] private float _jumpSpeed;

    private List<GameObject> _food = new List<GameObject>();

    public List<GameObject> Food => _food;

    private IEnumerator FoodMoveToTarget(GameObject food, Transform character)
    {
        while(food.transform.position == character.position)
        {
            food.transform.position = Vector3.MoveTowards(food.transform.position, character.position, _jumpSpeed * Time.deltaTime);
        }
        yield return null;
    }

    public void Spawn()
    {
        int money = int.Parse(_money.text);
        int cost = int.Parse(_cost.text);

        if (money >= cost)
        {
            money -= cost;
            _money.text = money.ToString();
            var hotDog = Instantiate(_hotDog, _spawnPosition);
            hotDog.transform.DOMove(_finishPosition.position, _moveTime);
            _finishPosition.position += _finishTuningPosition;
            _food.Add(hotDog);
        }
    }

    public void Remove()
    {
        int money = int.Parse(_money.text);
        money += 20;
        _money.text = money.ToString();
        var hotDog = _food[_food.Count - 1];
        _finishPosition.position -= _finishTuningPosition;
        _food.Remove(_food[_food.Count - 1]);
        Destroy(hotDog);
    }

    public void SetCurrentFood(out GameObject currentFood, out FoodMovement foodMovement)
    {
        currentFood = _food[_food.Count - 1];
        foodMovement = currentFood.GetComponent<FoodMovement>();
    }
}
