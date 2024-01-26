using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Scripts.Enemies
{
    public class EnemyFactory<T> where T : Enemy
    {
        private FactoryArgs<T> _args;
        
        private IObjectPool<T> _enemyPool;
        private ParticleSystem _spawnParticles;

        public EnemyFactory(FactoryArgs<T> args)
        {
            _args = args;
            
            _enemyPool = new ObjectPool<T>(Create, Get, Release);
        }

        public IEnumerator CreateEnemyWithDelay(Action<T> callback)
        {
            ValidateParticleObject();
            
            var randomPosition = GetRandomPosition();

            _spawnParticles.transform.position = randomPosition;
            _spawnParticles.Play();

            yield return new WaitForSeconds(_args.SpawnDelay);
            
            var enemy = _enemyPool.Get();
            enemy.transform.position = randomPosition;
            enemy.gameObject.SetActive(true);
            
            callback?.Invoke(enemy);
        }

        public T CreateEnemy()
        {
            var randomPosition = GetRandomPosition();
            
            var enemy = _enemyPool.Get();
            
            enemy.transform.position = randomPosition;
            enemy.gameObject.SetActive(true);
            
            return enemy;
        }

        public void ReleaseEnemy(T enemy)
        {
            _enemyPool.Release(enemy);
        }

        public void Stop()
        {
            if (_spawnParticles) _spawnParticles.Stop();
        }

        private Vector3 GetRandomPosition()
        {
            var randomPosition = Random.insideUnitSphere * _args.RandomPositionRange;
            randomPosition.y = 0;

            for (; randomPosition.magnitude < 2f; )
            {
                randomPosition = Random.insideUnitSphere * _args.RandomPositionRange;
                randomPosition.y = 0;
            }
            
            return randomPosition;
        }

        private void ValidateParticleObject()
        {
            if (!_spawnParticles)
            {
                _spawnParticles = Object.Instantiate(_args.ParticlePrefab);
            }
        }

        private T Create()
        {
            var enemyInstance = Object.Instantiate(_args.ChaserPrefab, _args.Parent);
            Release(enemyInstance);
            return enemyInstance;
        }

        private void Get(T enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private void Release(T enemy)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}