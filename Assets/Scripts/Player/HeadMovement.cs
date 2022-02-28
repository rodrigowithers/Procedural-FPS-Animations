using System;
using UnityEngine;

namespace Player
{
    public class HeadMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] _heads;

        [Space] [SerializeField] private float _speed = 1;

        [SerializeField, Range(-90, 90)] private float
            _verticalLimitDown = -90,
            _verticalLimitUp = 90;

        private Vector2 _input;
        private Vector2 _angles;

        public Vector3 Direction => _heads[0].forward;
        public Vector3 NormalizedForward
        {
            get
            {
                var normalized = _heads[0].forward;
                normalized.y = 0;
                normalized.Normalize();
                
                return normalized;
            }
        }
        
        public Vector3 NormalizedRight
        {
            get
            {
                var normalized = _heads[0].right;
                normalized.y = 0;
                normalized.Normalize();
                
                return normalized;
            }
        }

        private void Start()
        {
            _angles = Vector2.zero;
        }

        private void Update()
        {
            _input = new Vector2(Input.GetAxis("Aim Horizontal"), Input.GetAxis("Aim Vertical")) * _speed;
            _angles.x -= _input.y;
            _angles.y += _input.x;

            _angles.x = Mathf.Clamp(_angles.x, _verticalLimitDown + 0.01f, _verticalLimitUp - 0.01f);
        }

        private void LateUpdate()
        {
            foreach (var head in _heads)
            {
                head.localRotation = Quaternion.Euler(_angles.x, _angles.y, 0);
            }
        }

        private void OnDrawGizmos()
        {
            var centerPosition = transform.position;
            centerPosition.y = 0.1f;

            DebugExtension.DrawCircle(centerPosition, Vector3.up, Color.blue);
            DebugExtension.DrawArrow(centerPosition, Direction, Color.red);
            DebugExtension.DrawArrow(centerPosition, NormalizedForward, Color.green);
        }
    }
}