using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonsMovement : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    [SerializeField] private RectTransform _hidePosition;
    [SerializeField] private RectTransform _startPosition;

    private RectTransform _position;

    private void Start()
    {
        _position = GetComponent<RectTransform>();
    }

    public void Hide()
    {
        if(_position != _hidePosition)
        {
            _position.DOAnchorPos3D(_hidePosition.anchoredPosition3D, _moveTime);
        }
    }

    public void Show()
    {
        if(_position != _startPosition)
        {
            _position.DOAnchorPos3D(_startPosition.anchoredPosition3D, _moveTime);
        }
    }
}
