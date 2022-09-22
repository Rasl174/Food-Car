using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] private ParticleSystem _smile;

    private RingColorChanger _ringColorChanger;
    private FoodSpawner _foodSpawner;
    private FoodMovement _foodMovement;
    private WayPointMovement _wayPointMovement;
    private Animator _animator;
    private Waypoint_Indicator _indicator;
    private bool _isHungry = true;
    private TextObject _text;

    public bool IsHungry => _isHungry;

    private void Start()
    {
        _text = GetComponentInChildren<TextObject>();
        _ringColorChanger = FindObjectOfType<RingColorChanger>();
        _foodSpawner = FindObjectOfType<FoodSpawner>();
        _wayPointMovement = GetComponent<WayPointMovement>();
        _indicator = GetComponent<Waypoint_Indicator>();
        _animator = GetComponent<Animator>();
        _smile.Stop();
    }

    private void Update()
    {
        _foodMovement = FindObjectOfType<FoodMovement>();
        _text.gameObject.transform.LookAt(Camera.main.transform);
        if(_foodMovement.transform.position == transform.position)
        {
            _isHungry = false;
            _indicator.enabled = false;
            _foodSpawner.Remove();
            StartCoroutine(WaypointEnabled());
        }
    }

    private IEnumerator WaypointEnabled()
    {
        yield return new WaitForSeconds(0.8f);
        _wayPointMovement.enabled = true;
        _ringColorChanger.SetYellowColor();
    }

    public void Buing()
    {
        _indicator.enabled = false;
        //_moneyTexture.SetActive(true);
        //_smile.Play();
    }

    public void PlayRunAnimation()
    {
        _animator.Play("Run");
    }

    public void PlayTakeAnimation()
    {
        _animator.Play("Take");
    }
}
