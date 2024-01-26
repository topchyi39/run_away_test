using System.Collections;
using Cinemachine;
using Scripts.Enemies;
using Scripts.Input;
using Scripts.Player;
using Scripts.UI;
using Scripts.UI.Gameplay;
using Scripts.UI.MVP;
using UnityEngine;

namespace Scripts.GameSystem
{
    public class Game : MonoBehaviour, IModel
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private Player.Player playerPrefab;
        
        [SerializeField] private Transform worldContent;
        [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;

        private UIManager _uiManager;
        private GamePresenter _presenter;
        private TouchScreenInput _input;
        private CameraSystem _cameraSystem;
        private Timer _timer = new ();
        
        private Player.Player _playerInstance;
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            
            _uiManager = UIManager.Instance;
            _presenter = new GamePresenter(this);
            
            _uiManager.Bind<GameScreen>(_presenter);

            _cameraSystem = new CameraSystem(mainVirtualCamera);
            _input = new TouchScreenInput();
            
            
            StartGame();
        }
        

        public void RestartGame()
        {
            enemyManager.ClearAll();
            Destroy(_playerInstance.gameObject);
            _presenter.HideGameOver();   
            StartGame();
        }
        
        private void StartGame()
        {
            _input.Enable();
            
            _playerInstance = Instantiate(playerPrefab, worldContent);
            _playerInstance.Init(_input, _cameraSystem);

            enemyManager.SetPlayer(_playerInstance);
            enemyManager.SpawnEnemies();
            
            _timer.Start();
            _presenter.ShowGameplay();
            
            StartCoroutine(GameRoutine());
        }

        private IEnumerator GameRoutine()
        {
            
            while (_playerInstance.IsAlive)
            {
                yield return new WaitForEndOfFrame();
                _timer.Tick(Time.deltaTime);
                _presenter.UpdateTimer(_timer.EvaluateTime);
            }
            
            _timer.Stop();
            _input.Disable();
            enemyManager.StopAllEnemies();
            _presenter.ShowGameOver(_timer.EvaluateTime, _playerInstance.KillReason);
        }
    }
}