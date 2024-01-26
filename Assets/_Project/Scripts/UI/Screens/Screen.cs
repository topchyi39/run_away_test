using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.UI.MVP;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Screens
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster), typeof(CanvasGroup))]
    public class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration;
        
        private Dictionary<Type, IView> _views;

        private void OnValidate()
        {
            canvasGroup ??= GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            _views = GetComponentsInChildren<IView>().ToDictionary(item => item.GetType(), item => item);
        }

        public void Bind(IPresenter presenter)
        {
            if (!_views.TryGetValue(presenter.ViewType, out var view)) return;
            
            presenter.Bind(view);
            view.Bind(presenter);
        }

        public void Show()
        {
            StopAllCoroutines();
            StartCoroutine(canvasGroup.Fade(true, fadeDuration));
        }

        public void Hide()
        {
            StopAllCoroutines();
            StartCoroutine(canvasGroup.Fade(false, fadeDuration));
        }
    }
}