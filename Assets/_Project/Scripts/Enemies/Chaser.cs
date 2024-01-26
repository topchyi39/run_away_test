using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
    public class Chaser : Enemy
    {
        [SerializeField] private float caughtRadius;
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private Animator animator;
        
        private void OnValidate()
        {
            sphereCollider ??= GetComponent<SphereCollider>();
        }

        private void Awake()
        {
            sphereCollider ??= GetComponent<SphereCollider>();
            sphereCollider.radius = caughtRadius;
        }

        public void SetRunningState(bool state)
        {
            animator.SetBool("IsRunning", state);
        }
    }
}