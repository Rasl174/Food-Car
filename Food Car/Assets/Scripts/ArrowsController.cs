using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowsController : MonoBehaviour
{
    [SerializeField] private GameObject[] _leftArrows;
    [SerializeField] private GameObject[] _rightArrows;
    [SerializeField] private TruckMovement _truckMovement;
    [SerializeField] private float _colorTimeChenger;
    [SerializeField] private Material _material;

    private int _crossWalksCount;
    private Color _startColor;
    private Color _endColor;

    private void Start()
    {
        _startColor = _material.color;
        _endColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0);
        for (int i = 1; i < _leftArrows.Length; i++)
        {
            Renderer leftArrowRender = _leftArrows[i].GetComponent<Renderer>();
            Renderer rightArrowRender = _rightArrows[i].GetComponent<Renderer>();
            leftArrowRender.material.color = _endColor;
            rightArrowRender.material.color = _endColor;
        }

    }

    private void Update()
    {
        Renderer leftArrowRender = _leftArrows[_crossWalksCount].GetComponent<Renderer>();
        Renderer rightArrowRender = _rightArrows[_crossWalksCount].GetComponent<Renderer>();

        if (_truckMovement.IsLeftWay)
        {
            leftArrowRender.material.DOColor(_startColor, _colorTimeChenger);
            rightArrowRender.material.DOColor(_endColor, _colorTimeChenger);
        }
        else if (_truckMovement.IsRightWay)
        {
            rightArrowRender.material.DOColor(_startColor, _colorTimeChenger);
            leftArrowRender.material.DOColor(_endColor, _colorTimeChenger);
        }
    }

    public void ChangeCrosswalkCount()
    {
        _leftArrows[_crossWalksCount].SetActive(false);
        _rightArrows[_crossWalksCount].SetActive(false);
        _crossWalksCount++;
    }
}
