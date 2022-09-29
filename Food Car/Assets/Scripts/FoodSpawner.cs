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

    public void Spawn()
    {
        int money = int.Parse(_money.text);
        int cost = int.Parse(_cost.text);

        if (money >= cost && _spawned == false && _food.Count < _maxFoodCount && _truckMovement.IsWayChanged == false)
        {
            _spawned = true;
            money -= cost;
            _money.text = money.ToString();
            var hotDog = Instantiate(_hotDog, _spawnPosition);
            hotDog.transform.DOMove(_finishPosition.position, _moveTime);
            _finishPosition.position += _finishTuningPosition;
            _food.Add(hotDog);
            StartCoroutine(SpawnedTimer());
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
