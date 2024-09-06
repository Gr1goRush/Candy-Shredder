using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiView : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private float _positionY;
    [SerializeField] private float _speed;

    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        var newPosition = Vector3.Lerp(_transform.position, _transform.position + Vector3.down, _speed);
        _transform.position = newPosition;
    }

    private void OnBecameInvisible()
    {
        _transform.position = new Vector3(_transform.position.x, _positionY, _transform.position.z);
    }
}
