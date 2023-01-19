using System.Collections;
using ButtonExtension.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonExtension
{
    [RequireComponent(typeof(Image))]
    public class ButtonColorAnimation : ButtonAnimation<ButtonColorSettings>
    {
        [SerializeField] private Image _image;

        private Coroutine _colorRoutine;
        
        protected override void Reset() 
        {
            base.Reset();
            _image = GetComponent<Image>();
        }

        protected override void OnStateChanged(ButtonState buttonState)
        {
            if (Settings.IsAvailableState(buttonState))
                SwapColor(Settings.GetScaleSettings<ColorSettings>(buttonState));
        }

        private void SwapColor(ColorSettings settings)
        {
            if (_colorRoutine != null)
                StopCoroutine(_colorRoutine);
            
            _colorRoutine = StartCoroutine(DoColor());
            
            IEnumerator DoColor()
            {
                Color color = _image.color;
                float currentDuration = 0;
                
                while (currentDuration < settings.Duration)
                {
                    _image.color = Color.Lerp(color, settings.Color, settings.Curve.Evaluate(currentDuration / settings.Duration));
                    currentDuration += Time.unscaledDeltaTime;
                    
                    yield return null;
                }
                
                _image.color = settings.Color;
            }
        }
    }
}