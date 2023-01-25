using System.Collections;
using FancyButton.Data;
using UnityEngine;

namespace FancyButton.Core
{
    [AddComponentMenu("Fancy Button/Button Scale Animator")]
    public class ButtonScaleAnimator : ButtonAnimator<ButtonScaleSettings>
    {
        private Coroutine _scaleRoutine;

        protected override void OnStateChanged(ButtonState buttonState)
        {
            if (Settings.IsAvailableState(buttonState))
                Scale(Settings.GetScaleSettings<ScaleSettings>(buttonState));
        }

        private void Scale(ScaleSettings settings)
        {
            if (_scaleRoutine != null)
                StopCoroutine(_scaleRoutine);
        
            _scaleRoutine = StartCoroutine(DoScale());
        
            IEnumerator DoScale()
            {
                Vector3 targetScale = Vector3.one * settings.Value;
                Vector3 currentScale = transform.localScale;
                float currentDuration = 0;
            
                while (currentDuration < settings.Duration)
                {
                    transform.localScale = Vector3.Lerp(currentScale, targetScale, settings.Curve.Evaluate(currentDuration / settings.Duration));
                    currentDuration += Time.unscaledDeltaTime;
                    yield return null;
                }

                transform.localScale = targetScale;
            }
        }
    }
}