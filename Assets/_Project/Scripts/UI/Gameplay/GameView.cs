using System;
using Scripts.UI.MVP;
using Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.UI.Gameplay
{
    public class GameView : View<GamePresenter>
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private CanvasGroup gameplay;
        [SerializeField] private TMP_Text timerText;
        
        [SerializeField] private CanvasGroup gameOver;
        [SerializeField] private TMP_Text resultTMP;
        [SerializeField] private string resultFormat;
        [SerializeField] private TMP_Text killReasonTMP;
        [SerializeField] private string killReasonFormat;
        [SerializeField] private Button restart;
        

        private void Awake()
        {
            gameplay.alpha = 0;
            gameplay.blocksRaycasts = false;
            
            gameOver.alpha = 0;
            gameOver.blocksRaycasts = false;
            restart.onClick.AddListener(RestartClicked);
        }

        public void UpdateTimer(string time)
        {
            timerText.text = time;
        }

        public void ShowGameOver(string time, string killReason)
        {
            resultTMP.text = string.Format(resultFormat, time);
            killReasonTMP.text = string.Format(killReasonFormat, killReason);
            
            StartCoroutine(gameplay.Fade(false, fadeDuration));
            StartCoroutine(gameOver.Fade(true, fadeDuration));
        }

        public void HideGameOver()
        {
            StartCoroutine(gameplay.Fade(true, fadeDuration));
            StartCoroutine(gameOver.Fade(false, fadeDuration));
        }

        public void ShowGameplay()
        {            
            StartCoroutine(gameplay.Fade(true, fadeDuration));
        }

        private void RestartClicked() => Presenter?.Restart();
    }
}