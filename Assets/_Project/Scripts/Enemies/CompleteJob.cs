using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Scripts.Enemies
{
    [BurstCompile]
    public struct CompleteJob : IJobParallelForTransform
    {
        [WriteOnly]  public NativeArray<Vector3> Positions;
        
        [BurstCompile]
        public void Execute(int index, TransformAccess transform)
        {
            Positions[index] = transform.position;
        }
    }
}