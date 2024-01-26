using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Scripts.Enemies
{
    [BurstCompile]
    public struct MoveJob : IJobParallelForTransform
    {
        [ReadOnly] public NativeArray<Vector3> Positions;
        [ReadOnly] public int EnemiesCount;
        [ReadOnly] public Vector3 PlayerPosition;
        [ReadOnly] public float Speed;
        [ReadOnly] public float Range;
        [ReadOnly] public float DeltaTime;

        [BurstCompile]
        public void Execute(int index, TransformAccess transform)
        {
            var position = transform.position;
            var direction = (PlayerPosition - position).normalized;
            direction.y = 0;
            
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = position + GetVelocity(index, position, direction);
        }

        private Vector3 GetVelocity(int index, Vector3 position, Vector3 direction)
        {
            for (var i = 0; i < Positions.Length; i++)
            {
                if (i == index) continue;

                var neighbourPosition = Positions[i];

                var distance = Vector3.Distance(position, neighbourPosition);
                if (distance < Range)
                {
                    direction += (position - neighbourPosition).normalized * 0.2f;
                }
                
            }

            direction.y = 0;

            var velocity = direction * (Speed * DeltaTime);
            return velocity;
        }
    }
}