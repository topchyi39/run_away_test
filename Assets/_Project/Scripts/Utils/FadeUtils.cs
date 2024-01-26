using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Utils
{
    public static class FadeUtils
    {
        public static IEnumerator Fade(this CanvasGroup canvasGroup, bool fadeIn, float duration)
        {
            var time = 0f;
            var startValue = canvasGroup.alpha;
            var endValue = fadeIn ? 1f : 0f;
            
            canvasGroup.blocksRaycasts = fadeIn;
            
            for (; time <= duration; )
            {
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, time / duration);
                
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }

            canvasGroup.alpha = endValue;
        }
    }
}