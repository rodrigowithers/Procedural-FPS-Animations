using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class RightHand : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _breathingCurve;
        [SerializeField] private Transform _followTransform;

        private Vector3 _initialEulerAngles;
        private Vector3 _initialPosition;

        private Vector3 _followDir = Vector3.forward;
        private Vector3 _prevFollowDir = Vector3.forward;

        private float _time;

        private Vector3 _dir = Vector3.up;
        private Vector3 _nextDir = Vector3.up;

        [SerializeField] private float _changeTime;

        [SerializeField] private float _speed = 1;
        [SerializeField] private float _changeSpeed = 1;

        [SerializeField] private float _followStrenght = 100;

        private void Start()
        {
            _initialPosition = transform.localPosition;
            _initialEulerAngles = transform.localEulerAngles;

            var randomDir = Random.insideUnitSphere;
            _nextDir = (_nextDir + randomDir).normalized;

            _changeTime = 0;

            _prevFollowDir = _followTransform.forward;
            _followDir = _prevFollowDir;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            _changeTime += Time.deltaTime * _changeSpeed;
            _dir = Vector3.MoveTowards(_dir, _nextDir, Time.deltaTime * _speed).normalized;

            if (_changeTime >= 1f)
            {
                var randomDir = Random.insideUnitSphere;
                _nextDir = (_nextDir + randomDir).normalized;

                _changeTime = 0;
            }

            // Get delta from Follow
            var prev = (Vector2) _prevFollowDir;
            var cur = (Vector2) _followTransform.localEulerAngles;
            var add = (cur - prev) * _followStrenght;
            _prevFollowDir = _followTransform.localEulerAngles;

            var newFollow = new Vector3(
                Mathf.Clamp(add.x, -10, 10),
                Mathf.Clamp(add.y, -10, 10),
                0);

            _followDir = Vector3.MoveTowards(_followDir, newFollow, 1);

            DebugExtension.DebugArrow(transform.position, _followTransform.right, Color.red);

            transform.localPosition = _initialPosition + new Vector3(0, _breathingCurve.Evaluate(_time), 0);
            transform.localEulerAngles =
                _initialEulerAngles + _followDir + _dir;
        }
    }
}