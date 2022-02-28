using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereDir : MonoBehaviour
{
    private Vector3 _dir = Vector3.up;
    private Vector3 _nextDir = Vector3.up;

    [SerializeField] private float _changeTime;
    
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _changeSpeed = 1;

    private void Start()
    {
        var randomDir = Random.insideUnitSphere;
        _nextDir = (_nextDir + randomDir).normalized;

        _changeTime = 0;
    }

    private void Update()
    {
        _changeTime += Time.deltaTime * _changeSpeed;
        _dir = Vector3.MoveTowards(_dir, _nextDir, Time.deltaTime * _speed).normalized;
        
        if (_changeTime >= 1f)
        {
            var randomDir = Random.insideUnitSphere;
            _nextDir = (_nextDir + randomDir).normalized;

            _changeTime = 0;
        }

        DebugExtension.DebugWireSphere(transform.position, Color.red, 1);

        DebugExtension.DebugArrow(transform.position, _dir, Color.red);
        DebugExtension.DebugArrow(transform.position, _nextDir, Color.blue);
    }
}