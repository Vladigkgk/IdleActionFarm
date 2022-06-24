using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.PlayerInput
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInputContoller : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _speedChangeRange;
        [SerializeField] private float _rotationSmoothTime;
        [SerializeField] private AudioClip _landClip;
        [SerializeField] private AudioClip[] _footStepClips;

        private PlayerController _player;
        private CharacterController _controller;
        private Vector2 _move;
        private float _speed;
        private float _targetRotation = 0f;
        private float _rotationVelocity;

        private static int SpeedId => Animator.StringToHash("Speed");

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        private void Start()
        {
            _player = GetComponent<PlayerController>();
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float targetSpeed = _moveSpeed;

            if (_move == Vector2.zero) targetSpeed = 0f;

            float currentHorizontalVelocity = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;
            float speedOffset = 0.1f;

            if (currentHorizontalVelocity < targetSpeed - speedOffset || currentHorizontalVelocity > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalVelocity, targetSpeed, Time.deltaTime * _speedChangeRange);

                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            Vector3 inputDiretion = new Vector3(_move.x, 0f, _move.y).normalized;

            if (_move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDiretion.x, inputDiretion.z) * Mathf.Rad2Deg;
                float rotaion = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

                transform.rotation = Quaternion.Euler(0f, rotaion, 0f);
            }

            Vector3 targetDiretion = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

            _controller.Move(_speed * Time.deltaTime * targetDiretion.normalized);

            _player.Animator.SetFloat(SpeedId, _speed);
        }

        public void MoveInput(Vector2 newMoveDiretion)
        {
            _move = newMoveDiretion;
        }

    }
}

