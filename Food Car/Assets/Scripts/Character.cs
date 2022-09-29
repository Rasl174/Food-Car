using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    private ParticleSystem _smile;
    private RingColorChanger _ringColorChanger;
    private FoodSpawner _foodSpawner;
    private FoodMovement _foodMovement;
    private WayPointMovement _wayPointMovement;
    private Animator _animator;
    private Waypoint_Indicator _indicator;
    private bool _isHungry = true;
    private TextObject _text;
    private ButtonsEnabled _buttonEnabled;

    public bool IsHungry => _isHungry;

    private void Start()
    {
        _buttonEnabled = FindObjectOfType<ButtonsEnabled>();
        _smile = GetComponentInChildren<ParticleSystem>();
        _text = GetComponentInChildren<TextObject>();
        _ringColorChanger = FindObjectOfType<RingColorChanger>();
        _foodSpawner = FindObjectOfType<FoodSpawner>();
        _wayPointMovement = GetComponent<WayPointMovement>();
        _indicator = GetComponent<Waypoint_Indicator>();
        _animator = GetComponent<Animator>();
        _text.gameObject.SetActive(false);
        _smile.Stop();
    }

    private void Update()
    {
        _text.gameObject.transform.LookAt(Camera.main.transform);
        if(_foodSpawner.Food.Count > 0)
        {
            _foodMovement = FindObjectOfType<FoodMovement>();
            if(_foodMovement.transform.position == transform.position)
            {
                _smile.Play();
                _text.gameObject.SetActive(true);
                _isHungry = false;
                _indicator.enabled = false;
                _foodSpawner.Remove();
                StartCoroutine(WaypointEnabled());
            }
        }
    }

    private IEnumerator WaypointEnabled()
    {
        yield return new WaitForSeconds(0.5f);
        _buttonEnabled.ActivateButton();
        _wayPointMovement.enabled = true;
        _ringColorChanger.SetYellowColor();
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
