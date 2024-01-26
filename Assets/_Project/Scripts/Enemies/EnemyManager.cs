using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Serialization;

namespace Scripts.Enemies
{
    
    public class EnemyManager : MonoBehaviour
    {
        [Header("Chaser Enemy")]
        [SerializeField] private Chaser chaserPrefab;
        [SerializeField] private ParticleSystem particlePrefab;
        [SerializeField] private float avoidRange;
        [SerializeField] private float speed = 4.5f;
        [SerializeField] private int maxCapacity = 100;
        [SerializeField] private float spawnCooldown = 5f;
        [SerializeField] private float spawnDelay = 2f;
        
        [Header("Mine Enemy")]
        [SerializeField] private Mine minePrefab;
        [SerializeField] private int mineCount = 100;
        
        [Space(10f)] 
        [SerializeField] private float randomSpawnRange = 20f;

        

        private int _lastEnemyIndex;
        private float _currentCooldown;
        private Player.Player _player;
        
        private EnemyFactory<Chaser> _chaserFactory;
        private EnemyFactory<Mine> _mineFactory;

        private IEnumerator _chaserSpawnRoutine;
        private IEnumerator _lastSpawnChaserRoutine;
        
        private List<Mine> _mines = new ();
        private List<Chaser> _chasers = new ();

        private NativeArray<Vector3> _enemyPositions;
        private TransformAccessArray _transformAccess;
        
        private void Awake()
        {
            CreateFactories();
            InitArrays();
        }

        private void OnDestroy()
        {
            DisposeArrays();
        }

        private IEnumerator SpawnChasers()
        {
            for (; _player is { IsAlive: true } && _lastEnemyIndex < maxCapacity;)
            {
                yield return new WaitForEndOfFrame();
                _currentCooldown -= Time.deltaTime;

                if (_currentCooldown <= 0)
                {
                    _lastSpawnChaserRoutine = _chaserFactory.CreateEnemyWithDelay(enemy =>
                    {
                        enemy.SetRunningState(true);
                        _chasers.Add(enemy);
                        _transformAccess.Add(enemy.transform);
                    });
                    
                    StartCoroutine(_lastSpawnChaserRoutine);
                
                    _lastEnemyIndex++;
                    _currentCooldown = spawnCooldown;
                }
            }

            if (_lastSpawnChaserRoutine != null)
            {
                StopCoroutine(_lastSpawnChaserRoutine);
                _chaserFactory.Stop();
            }
        }

        private void LateUpdate()
        {
            if (_player is not { IsAlive : true }) return;
            
            var moveJob = new MoveJob
            {
                Positions = _enemyPositions,
                PlayerPosition = _player.transform.position,
                Speed = speed,
                Range = avoidRange,
                DeltaTime = Time.deltaTime
            };

            var completeJob = new CompleteJob
            {
                Positions = _enemyPositions
            };

            var completeHandle = completeJob.Schedule(_transformAccess);
            var moveHandle = moveJob.Schedule(_transformAccess, completeHandle);
            moveHandle.Complete();
        }

        public void SetPlayer(Player.Player player)
        {
            _player = player;
        }

        public void SpawnEnemies()
        {
            _currentCooldown = spawnCooldown;
            
            for (var i = 0; i < mineCount; i++)
            {
                var mineInstance = _mineFactory.CreateEnemy();
                _mines.Add(mineInstance);
            }

            _chaserSpawnRoutine = SpawnChasers();
            StartCoroutine(_chaserSpawnRoutine);
        }

        public void ClearAll()
        {
            if (_chaserSpawnRoutine != null) StopCoroutine(_chaserSpawnRoutine);
            
            foreach (var mine in _mines)
            {
                _mineFactory.ReleaseEnemy(mine);
            }
            
            _mines.Clear();

            foreach (var chaser in _chasers)
            {
                _chaserFactory.ReleaseEnemy(chaser);
            }
            
            _chasers.Clear();
            
            _transformAccess.Dispose();
            _transformAccess = new TransformAccessArray(maxCapacity);
        }

        private void CreateFactories()
        {
            var chaserArgs = new FactoryArgs<Chaser>
            (
                chaserPrefab,
                particlePrefab,
                transform,
                spawnDelay,
                randomSpawnRange
            );


            _chaserFactory = new EnemyFactory<Chaser>(chaserArgs);
            
            var mineArgs = new FactoryArgs<Mine>
            (
                minePrefab,
                null,
                transform,
                0,
                randomSpawnRange
            );


            _chaserFactory = new EnemyFactory<Chaser>(chaserArgs);
            _mineFactory = new EnemyFactory<Mine>(mineArgs);
        }

        private void InitArrays()
        {
            _enemyPositions = new NativeArray<Vector3>(maxCapacity, Allocator.Persistent);
            _transformAccess = new TransformAccessArray(maxCapacity);
        }

        private void DisposeArrays()
        {
            _enemyPositions.Dispose();
            _transformAccess.Dispose();
        }

        public void StopAllEnemies()
        {
            foreach (var chaser in _chasers)
            {
                chaser.SetRunningState(false);
            }
        }
    }
}