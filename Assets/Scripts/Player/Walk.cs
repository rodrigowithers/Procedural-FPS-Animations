using UnityEngine;

namespace Player
{
    public class Walk : MonoBehaviour
    {
        public CharacterController Controller;
        public HeadMovement Head;

        public float Speed = 1;
        public float JumpHeight = 3;

        private Vector3 _move;

        private bool _jump;
        
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _move = Head.NormalizedForward * movement.z + Head.NormalizedRight * movement.x;
            _move = Vector3.ClampMagnitude(_move, 1);

            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
            }
        }

        private void FixedUpdate()
        {
            if (_jump)
            {
                _jump = false;
                _move.y += Mathf.Sqrt(JumpHeight * -3.0f * Physics.gravity.y);
            }

            _move += Physics.gravity * Time.deltaTime;
            Controller.SimpleMove(_move * (Input.GetKey(KeyCode.LeftShift) ? Speed * 2 : Speed));
        }

        private void OnDrawGizmos()
        {
            var centerPosition = transform.position;
            centerPosition.y = 0.1f;
            
            DebugExtension.DrawCone(centerPosition, transform.forward, 10f);
        }
    }
}