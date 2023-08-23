using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AnimateRigidbody : MonoBehaviour
{
    private static readonly Vector3[] Directions = new[] {Vector3.left, Vector3.up, Vector3.right, Vector3.down};
    private Rigidbody _myRigidbody;
    private float _passedTime;
    private int _currentDir;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _passedTime += Time.fixedDeltaTime;

        while (_passedTime > 1)
        {
            _passedTime -= 1;
            _currentDir = (_currentDir + 1) % Directions.Length;
        }
        _myRigidbody.MovePosition(_myRigidbody.position + Directions[_currentDir] * (Time.deltaTime * 1f));
    }
}
