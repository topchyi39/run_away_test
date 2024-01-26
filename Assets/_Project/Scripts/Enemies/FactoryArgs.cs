using UnityEngine;

namespace Scripts.Enemies
{
    public struct FactoryArgs<T> where T : Enemy
    {
        public readonly T ChaserPrefab;
        public readonly ParticleSystem ParticlePrefab;
        public readonly Transform Parent;
        public readonly float SpawnDelay;
        public readonly float RandomPositionRange;

        public FactoryArgs(T prefab, ParticleSystem particlePrefab, Transform parent, float spawnDelay, float randomPositionRange)
        {
            ChaserPrefab = prefab;
            ParticlePrefab = particlePrefab;
            Parent = parent;
            SpawnDelay = spawnDelay;
            RandomPositionRange = randomPositionRange;
        }
    }
}