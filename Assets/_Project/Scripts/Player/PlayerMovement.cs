using Scripts.Input;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerMovement
    {
        private float _time;
        private float _lerpValue;
        private Quaternion _cachedRotation;
            
        private readonly CharacterController _playerController;
        private readonly Animator _characterAnimator;
        private readonly TouchScreenInput _input;
        private readonly PlayerMovementData _data;
        
        public PlayerMovement(TouchScreenInput input, CharacterController playerController, Animator animator,
            PlayerMovementData playerMovementData)
        {
            _input = input;
            _characterAnimator = animator;
            _playerController = playerController;
            _data = playerMovementData;
        }
        
        public void Tick()
        {
            var axis = _input.MoveAxis;

            if (axis == Vector2.zero)
            {
                _characterAnimator.SetBool("IsRunning", false);
                ResetTime();
                return;
            }
            
            _characterAnimator.SetBool("IsRunning", true);
            LerpMultiplier();
            Move(axis);
            Rotate(axis);
        }

        private void ResetTime()
        {
            _time = 0f;
            _lerpValue = 0f;
            _cachedRotation = _playerController.transform.rotation;
        }

        private void LerpMultiplier()
        {
            _time += Time.deltaTime;
            _lerpValue = Mathf.Clamp01(_time / _data.LerpRotationDuration);
        }

        private void Move(Vector2 axis)
        {
            var velocity = new Vector3(axis.x, 0, axis.y) * (Time.deltaTime * _data.Speed);
            _playerController.Move(velocity);
        }

        private void Rotate(Vector2 axis)
        {
            var direction = new Vector3(axis.x, 0, axis.y);
            var lookRotation = Quaternion.LookRotation(direction);

            var rotation = _data.LerpRotationOnStart
                ? Quaternion.Slerp(_cachedRotation, lookRotation, _lerpValue)
                : lookRotation;
            
            _playerController.transform.rotation = rotation;
        }

        public void Disable()
        {
            _characterAnimator.SetBool("IsRunning", false);
            ResetTime();
        }
    }
}