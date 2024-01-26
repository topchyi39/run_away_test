using System;
using Scripts.Input;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform cameraFollowObject;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerMovementData movementData;
        
        private TouchScreenInput _input;
        private PlayerMovement _movement;
        public bool IsAlive { get; private set; } = true;
        public string KillReason { get; private set; }
        private void OnValidate()
        {
            characterController ??= GetComponent<CharacterController>();
        }

        public void Init(TouchScreenInput input, CameraSystem cameraSystem)
        {
            _input = input;
            cameraSystem.SetFollowObject(cameraFollowObject);

            _movement = new PlayerMovement(_input, characterController, animator, movementData);
        }

        private void Update()
        {
            if (!IsAlive) return;
            
            _movement.Tick();
        }

        public void Kill(string reason)
        {
            IsAlive = false;
            KillReason = reason;
            _movement.Disable();
        }
    }
}