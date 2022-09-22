using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColorChanger : MonoBehaviour
{
    [SerializeField] private Color _changeColor;

    private MeshRenderer _ringMesh;
    private Color _startColor;

    private void Start()
    {
        _ringMesh = GetComponent<MeshRenderer>();
        _startColor = _ringMesh.material.color;
    }

    public void ChangeForGreen()
    {
        _ringMesh.material.color = _changeColor;
    }

    public void SetYellowColor()
    {
        _ringMesh.material.color = _startColor;
    }
}
