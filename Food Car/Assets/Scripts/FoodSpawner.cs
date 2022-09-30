using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _costHotDog;
    [SerializeField] private TMP_Text _costHamburger;
    [SerializeField] private TMP_Text _costPizza;
    [SerializeField] private GameObject _hotDog;
    [SerializeField] private GameObject _hamburger;
    [SerializeField] private GameObject _pizza;
    [SerializeField] private Vector3 _finishTuningPosition;
    [SerializeField] private Transform _spawnHotDogPosition;
    [SerializeField] private Transform _spawnHamburgerPosition;
    [SerializeField] private Transform _spawnPizzaPosition;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private float _moveTime;

    private TruckMovement _truckMovement;
    private List<GameObject> _food = new List<GameObject>();
    private bool _spawned = false;
    private int _maxFoodCount = 6;

    public bool Spawned => _spawned;

    public List<GameObject> Food => _food;

    private void Start()
    {
        _truckMovement = FindObjectOfType<TruckMovement>();
    }

    private IEnumerator SpawnedTimer()
    {
        yield return new WaitForSeconds(_moveTime + 0.1f);
        _spawned = false;
    }

    public void SpawnHotdog()
    {
        int money = int.Parse(_money.text);
        int cost = int.Parse(_costHotDog.text);

        if (money >= cost && _spawned == false && _food.Count < _maxFoodCount && _truckMovement.IsWayChanged == false)
        {
            _spawned = true;
            money -= cost;
            _money.text = money.ToString();
            var hotDog = Instantiate(_hotDog, _spawnHotDogPosition);
            hotDog.transform.DOMove(_finishPosition.position, _moveTime);
            _finishPosition.position += _finishTuningPosition;
            _food.Add(hotDog);
            StartCoroutine(SpawnedTimer());
        }
    }

    public void SpawnHamburger()
    {
        int money = int.Parse(_money.text);
        int cost = int.Parse(_costHamburger.text);

        if (money >= cost && _spawned == false && _food.Count < _maxFoodCount && _truckMovement.IsWayChanged == false)
        {
            _spawned = true;
            money -= cost;
            _money.text = money.ToString();
            var hamburger = Instantiate(_hamburger, _spawnHotDogPosition);
            hamburger.transform.DOMove(_finishPosition.position, _moveTime);
            _finishPosition.position += _finishTuningPosition;
            _food.Add(hamburger);
            StartCoroutine(SpawnedTimer());
        }
    }

    public void SpawnPizza()
    {
        int money = int.Parse(_money.text);
        int cost = int.Parse(_costPizza.text);

        if (money >= cost && _spawned == false && _food.Count < _maxFoodCount && _truckMovement.IsWayChanged == false)
        {
            _spawned = true;
            money -= cost;
            _money.text = money.ToString();
            var pizza = Instantiate(_pizza, _spawnHotDogPosition);
            pizza.transform.DOMove(_finishPosition.position, _moveTime);
            _finishPosition.position += _finishTuningPosition;
            _food.Add(pizza);
            StartCoroutine(SpawnedTimer());
        }
    }


    public void Remove()
    {
        int money = int.Parse(_money.text);
        var currentFood = _food[_food.Count - 1];
        if(currentFood.tag == "Hotdog")
        {
            money += 20;
        }
        else if(currentFood.tag == "Hamburger")
        {
            money += 200;
        }
        else if(currentFood.tag == "Pizza")
        {
            money += 700;
        }
        _money.text = money.ToString();
        _finishPosition.position -= _finishTuningPosition;
        _food.Remove(_food[_food.Count - 1]);
        Destroy(currentFood);
    }

    public void SetCurrentFood(out GameObject currentFood, out FoodMovement foodMovement)
    {
        currentFood = _food[_food.Count - 1];
        foodMovement = currentFood.GetComponent<FoodMovement>();
    }

    public bool CheckLastFood(GameObject character)
    {
        if(character.tag == _food[_food.Count - 1].tag)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
